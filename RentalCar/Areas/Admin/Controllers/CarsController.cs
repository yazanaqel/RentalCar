using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CarsController : Controller
    {
        private readonly IGenericRepository<Car> _contextCar;

        public CarsController(IGenericRepository<Car> contextCar)
        {
            _contextCar = contextCar;
        }

        // GET: Admin/Cars
        public async Task<IActionResult> Index()
        {
            return View(await _contextCar.GetAllAsync());
        }

        // GET: Admin/Cars/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _contextCar.GetAllAsync() == null)
            {
                return NotFound();
            }

            var car = await _contextCar.GetEntityAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Admin/Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EngineCapacity,Photo,Color,DailyFare,CarType")] Car car)
        {
            if (ModelState.IsValid)
            {
                car.Id = Guid.NewGuid();

				if (Request.Form.Files.Count > 0)
				{
					var file = Request.Form.Files.FirstOrDefault();

					using (var stream = new MemoryStream())
					{
						await file.CopyToAsync(stream);
						car.Photo = stream.ToArray();
					}
				}


				_contextCar.Insert(car);
                await _contextCar.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Admin/Cars/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null || _contextCar.GetAllAsync() == null)
            {
                return NotFound();
            }

            var car = await _contextCar.GetEntityAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Admin/Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,EngineCapacity,Photo,Color,DailyFare,CarType")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contextCar.Update(car);
                    await _contextCar.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Admin/Cars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _contextCar.GetAllAsync() == null)
            {
                return NotFound();
            }

            var car = await _contextCar.GetQueryable()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Admin/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_contextCar.GetAllAsync() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cars'  is null.");
            }
            var car = await _contextCar.GetEntityAsync(id);
            if (car != null)
            {
                _contextCar.Delete(car);
            }
            
            await _contextCar.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(Guid id)
        {
          return (_contextCar.GetQueryable()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
