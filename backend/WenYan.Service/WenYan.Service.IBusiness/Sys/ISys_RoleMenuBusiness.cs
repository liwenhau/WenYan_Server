namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public interface ISys_RoleMenuBusiness:IBaseRepository<Sys_RoleMenu>
    {
        /// <summary>
        /// 根据角色id获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<string>> GetRolePermissionAsync(string id);

        /// <summary>
        /// 更新角色权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<int> SaveRolePermissionAsync(string roleId, List<string> permission);
    }
}
