using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;
using System.Xml.Linq;

namespace RentalCar.Repository;

public class CarRepository : IDataHelper<Car>
{
    private readonly ApplicationDbContext _dbContext;

    public CarRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Delete(Car entity)
    {
        _dbContext.Cars.Remove(entity);
    }

    public async Task<List<Car>> GetAllAsync()
    {
        return await _dbContext.Cars.AsNoTracking().ToListAsync();
    }

    public async Task<Car?> GetEntityAsync(Guid id)
    {
        return await _dbContext.Cars.FindAsync(id);
    }

    public async Task<Car?> GetEntityAsync(int id)
    {
        return await _dbContext.Cars.FindAsync(id);
    }

    public IQueryable<Car> GetQueryable()
    {
        return _dbContext.Cars;
    }

    public void Insert(Car entity)
    {
        _dbContext.Cars.Add(entity);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(Car entity)
    {
        _dbContext.Cars.Update(entity);
    }
}
