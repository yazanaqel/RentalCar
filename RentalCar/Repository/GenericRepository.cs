using Microsoft.EntityFrameworkCore;
using RentalCar.Data;

namespace RentalCar.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<TEntity?> GetEntityAsync(Guid id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }
    public async Task<TEntity?> GetEntityAsync(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }
    public IQueryable<TEntity> GetQueryable()
    {
        return _dbContext.Set<TEntity>();
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public void Insert(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }
    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

}