namespace WenYan.Service.Api.Controllers
{
    public class Sys_RoleController : BaseController
    {
        private ISys_RoleBusiness Bus { get; set; }
        public Sys_RoleController(ISys_RoleBusiness bus)
        {
            this.Bus = bus;
        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Sys_Role>> GetAllAsync(PageInput<SearchVM> query)
        {
            return await this.Bus.GetAllAsync(query);
        }
    }
}
