namespace WenYan.Service.Entity
{
    public interface IBusRepository<T, K> : IEntityRepository<T, K> where T : BusEntity<K>, new()
    {
    }

    public interface IBusRepository<T> : IBusRepository<T, string>
        where T : BusEntity, new()
    {
    }
}
