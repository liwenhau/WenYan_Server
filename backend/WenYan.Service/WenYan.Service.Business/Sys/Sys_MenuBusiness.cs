namespace WenYan.Service.Business
{
    public class Sys_MenuBusiness : BusRepository<Sys_Menu>, ISys_MenuBusiness
    {
        public Sys_MenuBusiness(GDbContext context) : base(context)
        {

        }

        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserMenuDto>> GetAllAsync(string name, string status)
        {
            var menus = this.GetQueryable<Sys_Menu>(true)
                 .Select(s => new UserMenuDto
                 {
                     Id = s.Id,
                     ParentId = s.ParentId ?? "",
                     Path = s.Path ?? "",
                     Code = s.Code,
                     Component = s.Component ?? "",
                     Redirect = s.Redirect ?? "",
                     Type = s.Type,
                     Title = s.Name,
                     SvgIcon = s.SvgIcon ?? "",
                     Icon = s.Icon ?? "",
                     KeepAlive = s.IsKeepAlive,
                     Hidden = s.IsHide,
                     Affix = s.IsAffix,
                     Permission = s.Permission ?? "",
                     Sort = s.Seq,
                     Status = s.Status
                 })
                 .OrderBy(x => x.Sort)
                 .AsQueryable();
            if (!name.IsNullOrEmpty() || !status.IsNullOrEmpty())
            {
                return await menus
                 .WhereIf(!name.IsNullOrEmpty(), x => x.Title.Contains(name))
                 .WhereIf(!status.IsNullOrEmpty(), x => x.Status == status)
                 .ToListAsync();
            }
            var treeMenus = TreeHelper.GetBaseTreeList(menus.ToList());
            return treeMenus;
        }

        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuTreeDto>> GetTreeAsync()
        {
            var menus = await this.GetQueryable(true).Select(x => new MenuTreeDto
            {
                Id = x.Id,
                title = x.Name,
                ParentId = x.ParentId
            }).ToListAsync();
            return TreeHelper.GetBaseTreeList(menus);
        }
    }
}
