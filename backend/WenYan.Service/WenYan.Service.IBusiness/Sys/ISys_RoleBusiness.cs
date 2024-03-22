namespace WenYan.Service.IBusiness
{
    public interface ISys_RoleBusiness : IBusRepository<Sys_Role>
    {
        Task<PageResult<Sys_Role>> GetAllAsync(PageInput<SearchVM> query);
    }
}
