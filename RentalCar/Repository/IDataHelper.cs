public interface IDataHelper<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetEntityAsync(Guid id);
    Task<T?> GetEntityAsync(int id);
    IQueryable<T> GetQueryable();
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();
}
