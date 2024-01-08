namespace WenYan.Service.IBusiness
{
    public interface ISys_UserRoleBusiness : IBaseRepository<Sys_UserRole>
    {
        Task<int> AddAsync(string userId, List<string> roleIds);
    }
}
