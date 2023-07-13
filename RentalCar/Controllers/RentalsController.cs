using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.GenericRepository;
using RentalCar.Models;
using RentalCar.ViewModel;

namespace RentalCar.Controllers
{
	[Authorize(Roles = "User")]
	public class RentalsController : Controller
	{
		private readonly IGenericRepository<Rental> _contextRental;
		private readonly IGenericRepository<Car> _contextCar;
		private readonly UserManager<ApplicationUser> _userManager;

		public RentalsController(
			IGenericRepository<Rental> contextRental,
			IGenericRepository<Car> contextCar,
			UserManager<ApplicationUser> userManager)
		{
			_contextRental = contextRental;
			_contextCar = contextCar;
			_userManager = userManager;
		}
		// GET: Rentals
		public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);

			var rentals = new List<Rental>();

			if (_userManager.IsInRoleAsync(user, "Admin").Result)
			{
				rentals = await _contextRental.GetAllAsync();
			}
			else
			{
				rentals = await
				_contextRental.GetQueryable()
				.Where(x => x.UserId == user.Id)
				.ToListAsync();
			}

			if (rentals is not null)
			{
				var model = new List<UesrRentalsViewModel>();

				foreach (var item in rentals)
				{

					var rentalsCars = new UesrRentalsViewModel
					{
						Id = item.Id,
						From = item.From,
						To = item.To,
						IsWithDriver = item.IsWithDriver,
						Location = item.Location,
						UserId = item.UserId,
						CarId = item.CarId
					};

					model.Add(rentalsCars);
				}
				return View(model);
			}


			return View();
		}


		// GET: Rentals/Create
		public async Task<IActionResult> Create(Guid CarId)
		{
			ViewBag.CarId = CarId;
			return View();
		}

		// POST: Rentals/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,IsWithDriver,Location,From,To,CarId")] Rental rental)
		{
			var user = await _userManager.GetUserAsync(User);

			rental.UserId = user.Id;

			_contextRental.Insert(rental);
			await _contextRental.SaveAsync();
			return RedirectToAction(nameof(Index));
		}


		// GET: Rentals/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _contextRental.GetAllAsync() == null)
			{
				return NotFound();
			}

			var rental = await _contextRental.GetQueryable()
				.FirstOrDefaultAsync(m => m.Id == id);
			if (rental == null)
			{
				return NotFound();
			}

			return View(rental);
		}

		// POST: Rentals/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_contextRental.GetAllAsync() == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Rentals'  is null.");
			}
			var rental = await _contextRental.GetEntityAsync(id);
			if (rental != null)
			{
				_contextRental.Delete(rental);
			}

			await _contextRental.SaveAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool RentalExists(int id)
		{
			return (_contextRental.GetQueryable()?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
