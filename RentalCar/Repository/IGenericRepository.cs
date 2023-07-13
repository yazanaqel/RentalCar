public interface IGenericRepository<TEntity>
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetEntityAsync(Guid id);
    Task<TEntity?> GetEntityAsync(int id);
    IQueryable<TEntity> GetQueryable();
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task SaveAsync();

}
