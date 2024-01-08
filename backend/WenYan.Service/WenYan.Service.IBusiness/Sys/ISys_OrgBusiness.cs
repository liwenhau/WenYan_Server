namespace WenYan.Service.IBusiness
{
    public interface ISys_OrgBusiness : IBusRepository<Sys_Org>,IScopedDependency
    {
        /// <summary>
        /// 获取所有组织信息
        /// </summary>
        /// <returns></returns>
        Task<List<OrgTreeDto>> GetAllAsync(string name, string status);
    }
}
