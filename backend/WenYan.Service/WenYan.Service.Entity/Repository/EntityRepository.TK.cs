namespace WenYan.Service.Entity
{
    public class EntityRepository<T, K> : BaseRepository<T>, IEntityRepository<T, K> where T : BaseEntity<K>, new()
    {
        public EntityRepository(DbContext context) : base(context) { }

        public async Task<int> AddOrUpdateAsync(T entity)
        {
            var dbEntity = await this.GetAsync(entity.Id);
            if (dbEntity != null)
            {
                this.DbContext.Entry(dbEntity).CurrentValues.SetValues(entity);
            }
            else
                this.DbContext.Add(entity);
            return await this.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync(K id)
        {
            var entity = await this.GetAsync(id);
            return await this.DeleteAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(List<K> ids)
        {
            var listEntity = await this.GetListAsync(ids);
            return await this.DeleteAsync(listEntity);
        }

        public virtual async Task<T?> GetAsync(K id, bool noTracking = false)
        {
            return await this.GetQueryable(noTracking).SingleOrDefaultAsync(w => w.Id.Equals(id));
        }

        public virtual async Task<List<T>> GetListAsync(List<K> ids, bool noTracking = false)
        {
            return await this.GetQueryable(noTracking).Where(w => ids.Contains(w.Id)).ToListAsync();
        }
        public override Task<int> UpdateAsync(T entity)
        {
            //var local = this.DbSet.Local.SingleOrDefault(e => e.Id.Equals(entity.Id));
            //if (local != null)
            //    this.DbContext.Entry(local).State = EntityState.Detached;
            ////this.DbContext.Attach(entity);
            //this.DbContext.Entry(entity).State = EntityState.Modified;
            this.DbContext.Entry<T>(entity).CurrentValues.SetValues(entity);
            return base.UpdateAsync(entity);
        }
        public virtual async Task<int> UpdateAsync(K id, object obj)
        {
            var entity = await this.DbContext.FindAsync<T>(id);
            this.DbContext.Entry<T>(entity).CurrentValues.SetValues(obj);
            //var listProps = obj.GetType().GetProperties().Select(s => s.Name).ToList();
            //var updateProp = entry.Metadata.FindProperties(listProps);
            //foreach (var prop in updateProp)
            //{
            //    var propEntry = entry.Property(prop.Name);
            //    propEntry.CurrentValue = obj.GetPropertyValue(prop.Name);
            //    propEntry.IsModified = true;
            //}
            return await this.DbContext.SaveChangesAsync();
            ///https://docs.microsoft.com/zh-cn/ef/core/saving/disconnected-entities
        }
    }

    public class EntityRepository<T> : EntityRepository<T, string>, IEntityRepository<T>
    where T : BaseEntity, new()
    {
        public EntityRepository(DbContext context)
            : base(context)
        { }
    }
}
