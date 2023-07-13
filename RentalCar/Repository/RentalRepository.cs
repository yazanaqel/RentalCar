using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Models;

namespace RentalRental.Repository;

public class RentalRepository : IDataHelper<Rental>
{
    private readonly ApplicationDbContext _dbContext;

    public RentalRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Delete(Rental entity)
    {
        _dbContext.Rentals.Remove(entity);
    }

    public async Task<List<Rental>> GetAllAsync()
    {
        return await _dbContext.Rentals.AsNoTracking().ToListAsync();
    }

    public async Task<Rental?> GetEntityAsync(Guid id)
    {
        return await _dbContext.Rentals.FindAsync(id);
    }

    public async Task<Rental?> GetEntityAsync(int id)
    {
        return await _dbContext.Rentals.FindAsync(id);
    }

    public IQueryable<Rental> GetQueryable()
    {
        return _dbContext.Rentals;
    }

    public void Insert(Rental entity)
    {
        _dbContext.Rentals.Add(entity);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(Rental entity)
    {
        _dbContext.Rentals.Update(entity);
    }
}
