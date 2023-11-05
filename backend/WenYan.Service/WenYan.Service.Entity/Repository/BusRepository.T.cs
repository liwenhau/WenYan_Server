namespace WenYan.Service.Entity
{
    public class BusRepository<T, K> : EntityRepository<T, K>, IBusRepository<T, K> where T : BusEntity<K>, new()
    {
        public BusRepository(DbContext context) : base(context) { }

        public override async Task<int> DeleteAsync(T entity)
        {
            entity.Deleted = true;
            return await this.UpdateAsync(entity);
        }

        public override async Task<int> DeleteAsync(List<T> entitys)
        {
            foreach (var item in entitys)
            {
                item.Deleted = true;
            }
            return await this.UpdateAsync(entitys);
        }

        public override async Task<int> DeleteAsync(K id)
        {
            var entity = await this.GetAsync(id);
            return await this.DeleteAsync(entity);
        }
        public override async Task<int> DeleteAsync(List<K> ids)
        {
            var list = await this.GetListAsync(ids);
            return await this.DeleteAsync(list);
        }
    }
    public class BusRepository<T> : BusRepository<T, string>, IBusRepository<T>
    where T : BusEntity, new()
    {
        public BusRepository(DbContext context)
            : base(context)
        { }
    }
}
