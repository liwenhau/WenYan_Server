namespace WenYan.Service.Api.Controllers
{
    public class Sys_RoleController : BaseController
    {
        private ISys_RoleBusiness _bus { get; set; }
        private ISys_RoleMenuBusiness  _roleMenuBus { get; set; }
        public Sys_RoleController(ISys_RoleBusiness bus, ISys_RoleMenuBusiness roleMenuBus)
        {
            this._bus = bus;
            this._roleMenuBus = roleMenuBus;
        }

        /// <summary>
        /// 根据角色id获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Sys_Role> GetAsync(string id)
        {
            return await this._bus.GetAsync(id);
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Sys_Role>> GetAllAsync(PageInput<SearchVM> query)
        {
            return await this._bus.GetAllAsync(query);
        }

        /// <summary>
        /// 根据角色id获取权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<string>> GetRolePermissionAsync(string roleId)
        {
            return await this._roleMenuBus.GetRolePermissionAsync(roleId);
        }

        /// <summary>
        /// 更新角色权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SaveRolePermissionAsync(string roleId, List<string> permission)
        {
            return await this._roleMenuBus.SaveRolePermissionAsync(roleId, permission);
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SaveAsync(Sys_Role data)
        {
            this.InitBusEntity(data);
            return await this._bus.AddOrUpdateAsync(data);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids">数据主键</param>
        /// <returns></returns>
        [HttpDelete]
        public Task<int> DeleteAsync(List<string> ids)
        {
            return this._bus.DeleteAsync(ids);
        }
    }
}
