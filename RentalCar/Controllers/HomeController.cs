using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentalCar.Data;
using RentalCar.Models;
using RentalCar.PagesNav;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RentalCar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IDataHelper<Car> _contextCar;
	private readonly IMemoryCache _cache;

	public HomeController(ILogger<HomeController> logger, IDataHelper<Car> contextCar, IMemoryCache cache)
    {
        _logger = logger;
        _contextCar = contextCar;
		_cache = cache;
	}

	private async Task<List<Car>> GetCarsFromCacheAsync()
	{
		// Define a cache key
		var cacheKey = "Cars";

		// Try to get the list of cars from the cache
		if (!_cache.TryGetValue(cacheKey, out List<Car> cars))
		{
			// If not found in the cache, get the list of cars from the database
			cars = await _contextCar.GetAllAsync();

			// Define cache options
			var cacheOptions = new MemoryCacheEntryOptions()
			{
				// Set absolute expiration to 10 minutes
				AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),

				// Set priority to high
				Priority = CacheItemPriority.High,

				// Set size to 1 (optional)
				Size = 1
			};

			// Set the list of cars in the cache with the cache options
			_cache.Set(cacheKey, cars, cacheOptions);
		}

		// Return the list of cars to the view
		return cars;
	}


public async Task<IActionResult> Index(string searchString, string filtter, decimal dailyFare, int page = 1)
    {
        var model = await GetCarsFromCacheAsync();

		model = CarCompany(model, filtter);

		model = Search(model, searchString);

		model = DailyFare(model, dailyFare);

		var finalData = Paging(model, page);

		return View(finalData);
    }
	private List<Car> Search(List<Car> model, string searchString)
	{
		if (!string.IsNullOrEmpty(searchString))
		{
			model = model.Where
				(s => s.Color.ToLower()
				.Equals(searchString.ToLower()))
				.ToList();
		}
		return model;
	}
	private List<Car> DailyFare(List<Car> model, decimal dailyFare)
	{
		if (dailyFare > 0)
		{
			model = model.Where(c => c.DailyFare <= dailyFare).ToList();
		}
		return model;
	}
	private List<Car> CarCompany(List<Car> model, string filtter)
	{
		if (!string.IsNullOrEmpty(filtter))
		{
			model = model.Where(c => c.CarType.ToString().Equals(filtter)).ToList();
		}
		return model;
	}
	private List<Car> Paging(List<Car> model, int page)
    {
		const int pageSize = 3;

		if (page < 1)
			page = 1;

		int recsCount = model.Count();

		var pager = new Pager(recsCount, page, pageSize);

		int recSkip = (page - 1) * pageSize;

		var data = model.Skip(recSkip).Take(pager.PageSize).ToList();

		this.ViewBag.Pager = pager;

        return data;
	}

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
