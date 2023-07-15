using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;
using RentalCar.PagesNav;
using System.Diagnostics;

namespace RentalCar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IDataHelper<Car> _contextCar;

	public HomeController(ILogger<HomeController> logger, IDataHelper<Car> contextCar)
    {
        _logger = logger;
        _contextCar = contextCar;
    }


    public async Task<IActionResult> Index(string searchString, string filtter, decimal dailyFare, int page = 1)
    {
        var model = await _contextCar.GetAllAsync();

        if (!String.IsNullOrEmpty(searchString))
        {
            model = model.Where
                (s => s.CarType.ToString().ToLower()
                .Contains(searchString.ToLower()))
                .ToList();
        }

		if (dailyFare > 0)
		{
			model = model.Where(c => c.DailyFare <= dailyFare).ToList();
		}

		if (!String.IsNullOrEmpty(filtter))
		{
			model = model.Where(c => c.CarType.ToString().Equals(filtter)).ToList();
		}

		var data = Paging(model, page);

		return View(data);
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
