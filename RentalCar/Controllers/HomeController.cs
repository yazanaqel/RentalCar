using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;
using System.Diagnostics;

namespace RentalCar.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
	private readonly IGenericRepository<Car> _contextCar;

	public HomeController(ILogger<HomeController> logger, IGenericRepository<Car> contextCar)
    {
        _logger = logger;
        _contextCar = contextCar;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _contextCar.GetAllAsync());
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
