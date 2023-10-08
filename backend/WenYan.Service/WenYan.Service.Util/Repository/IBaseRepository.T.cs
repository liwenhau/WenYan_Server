using System.Linq.Expressions;

namespace WenYan.Service.Util
{
    /// <summary>
    /// 基础仓储接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class, new()
    {
        #region 查询
        IQueryable<E> GetQueryable<E>(bool noTracking = false) where E : class;
        IQueryable<T> GetQueryable(bool noTracking = false);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool noTracking = false);
        Task<List<T>> GetAll(bool noTracking = false);
        #endregion

        #region 增删改
        Task<int> AddAsync(T entity);
        Task<int> AddAsync(List<T> entitys);

        Task<int> DeleteAsync(T entity);
        Task<int> DeleteAsync(List<T> entitys);
        Task<int> DeleteAsync(params object[] keyValues);
        Task<int> DeleteAsync(List<object> ids);

        Task<int> UpdateAsync(T entity);
        Task<int> UpdateAsync(List<T> entitys);
        Task<int> SaveChangesAsync();
        #endregion
    }
}
