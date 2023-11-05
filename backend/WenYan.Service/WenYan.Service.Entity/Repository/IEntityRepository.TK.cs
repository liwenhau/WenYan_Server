namespace WenYan.Service.Entity
{
    public interface IEntityRepository<T, K> : IBaseRepository<T> where T : BaseEntity<K>, new()
    {
        Task<List<T>> GetListAsync(List<K> ids, bool noTracking = false);

        Task<T?> GetAsync(K id, bool noTracking = false);

        Task<int> DeleteAsync(K id);
        Task<int> DeleteAsync(List<K> ids);

        Task<int> AddOrUpdateAsync(T entity);

        Task<int> UpdateAsync(K id, object obj);
    }
    public interface IEntityRepository<T> : IEntityRepository<T, string> where T : BaseEntity, new()
    {

    }
}
