namespace WenYan.Service.IBusiness
{
    public interface ISys_RoleBusiness : IBusRepository<Sys_Role>, IScopedDependency
    {
        Task<PageResult<Sys_Role>> GetAllAsync(PageInput<SearchVM> query);
    }
}
