using Microsoft.EntityFrameworkCore.Metadata;

using System.Linq.Expressions;

namespace WenYan.Service.Entity
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        private IKey Key { get; set; }
        public BaseRepository(DbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<T>();
        }

        #region Get
        public virtual Task<T?> GetAsync(Expression<Func<T, bool>> predicate, bool noTracking = false)
        {
            return this.GetQueryable(noTracking).SingleOrDefaultAsync(predicate);
        }
        public virtual async Task<T?> GetAsync(params object[] keyValues)
        {
            return await this.DbSet.FindAsync(keyValues);
        }
        #endregion

        #region GetList
        public virtual Task<List<T>> GetListAsync(List<object> ids, bool noTracking = false)
        {
            return this.GetQueryable(noTracking).Where($"{this.Key.GetName()} in @0", ids).ToListAsync();
        }
        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, bool noTracking = false)
        {
            return this.GetQueryable(noTracking).Where(predicate).ToListAsync();
        }
        public virtual Task<List<T>> GetListAsync(IQueryable<T> queryable)
        {
            return queryable.ToListAsync();
        }
        public virtual Task<List<T>> GetPageListAsync(IQueryable<T> queryable, PageInput query)
        {
            return queryable.GetPageListAsync(query);
        }
        public virtual Task<PageResult<T>> GetPageResultAsync(IQueryable<T> queryable, PageInput query)
        {
            return queryable.GetPageResultAsync(query);
        }
        public virtual Task<List<T>> GetAll(bool noTracking = false)
        {
            return this.GetQueryable(noTracking).ToListAsync();
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

        #region Add,Delete,Update
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

        public virtual async Task<int> UpdateAsync(T entity)
        {
            this.DbSet.Update(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        public virtual async Task<int> UpdateAsync(List<T> entitys)
        {
            this.DbSet.UpdateRange(entitys);
            return await this.DbContext.SaveChangesAsync();
        }
        #endregion
        public virtual Task<int> SaveChangesAsync()
        {
#if DEBUG
            this.DbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(this.DbContext.ChangeTracker.DebugView.LongView);
#endif
            return this.DbContext.SaveChangesAsync();
        }
    }
}
