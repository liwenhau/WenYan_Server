namespace WenYan.Service.Business
{
    public class Sys_RoleMenuBusiness : BaseRepository<Sys_RoleMenu>, ISys_RoleMenuBusiness
    {
        public Sys_RoleMenuBusiness(GDbContext context) : base(context)
        {

        }

        /// <summary>
        /// 根据角色id获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<string>> GetRolePermissionAsync(string roleId)
        {
            return await this.GetQueryable(false)
                .Where(x => x.RoleId == roleId)
                .Select(x => x.MenuId)
                .ToListAsync();
        }

        /// <summary>
        /// 更新角色权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public async Task<int> SaveRolePermissionAsync(string roleId, List<string> permission)
        {
            var dbMenus = await this.GetListAsync(x => x.RoleId == roleId);
            var delMenus = dbMenus.Where(x => !permission.Contains(x.MenuId)).ToList();
            if (delMenus.Count > 0)
                await this.DeleteAsync(delMenus);
            var exceptMenuIds = permission.Except(dbMenus.Select(s => s.MenuId));
            var newMenus = exceptMenuIds.Select(r => new Sys_RoleMenu() { RoleId = roleId, MenuId = r }).ToList();
            if (newMenus.Count > 0)
                await this.AddAsync(newMenus);
            return permission.Count;
        }
    }
}
