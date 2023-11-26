namespace WenYan.Service.IBusiness
{
    public interface ISys_MenuBusiness:IBusRepository<Sys_Menu>
    {
        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        Task<List<UserMenuDto>> GetAllAsync(string name,string status);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        Task<List<MenuTreeDto>> GetTreeAsync();
    }
}
