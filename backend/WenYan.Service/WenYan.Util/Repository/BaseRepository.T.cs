using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace WenYan.Service.Util
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        public BaseRepository(DbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<T>();
        }
        public virtual async Task<int> AddAsync(T entity)
        {
            this.DbSet.Add(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(List<T> entitys)
        {
            await this.DbSet.AddRangeAsync(entitys);
            return await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            this.DbSet.Remove(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(List<T> entitys)
        {
            this.DbSet.RemoveRange(entitys);
            return await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(params object[] keyValues)
        {
            var entity = await this.GetAsync(keyValues);
            return await this.DeleteAsync(entity);
        }

        public Task<int> DeleteAsync(List<object> ids)
        {
            throw new NotImplementedException();
        }

        #region Get
        public virtual Task<List<T>> GetAll(bool noTracking = false)
        {
            return this.GetQueryable(noTracking).ToListAsync();
        }
        public virtual Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool noTracking = false)
        {
            return this.GetQueryable(noTracking).SingleOrDefaultAsync(predicate);
        }
        public virtual async Task<T?> GetAsync(params object[] keyValues)
        {
            return await this.DbSet.FindAsync(keyValues);
        }
        #endregion

        #region Queryable
        public IQueryable<E> GetQueryable<E>(bool noTracking = false) where E : class
        {
            if (noTracking)
                return this.DbContext.Set<E>().AsNoTracking();
            else
                return this.DbContext.Set<E>();
        }
        public IQueryable<T> GetQueryable(bool noTracking = false)
        {
            if (noTracking)
                return this.DbSet.AsNoTracking();
            else
                return this.DbSet;
        }
        #endregion

        public Task<int> SaveChangesAsync()
        {
            return this.DbContext.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(List<T> entitys)
        {
            throw new NotImplementedException();
        }
    }
}
