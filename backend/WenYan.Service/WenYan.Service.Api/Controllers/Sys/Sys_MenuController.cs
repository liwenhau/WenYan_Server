namespace WenYan.Service.Api.Controllers
{
    public class Sys_MenuController : BaseController
    {
        private ISys_MenuBusiness Bus { get; set; }
        public Sys_MenuController(ISys_MenuBusiness bus)
        {
            Bus = bus;
        }

        /// <summary>
        /// 根据菜单id获取菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Sys_Menu> GetAsync(string id)
        {
            return await this.Bus.GetAsync(id);
        }

        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<UserMenuDto>> GetAllAsync(string name,string status)
        {
            return await this.Bus.GetAllAsync(name, status);
        }

        /// <summary>
        /// 保存菜单信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SaveAsync(Sys_Menu data)
        {
            this.InitBusEntity(data);
            if (data.ParentId.IsNullOrEmpty())
            {
                data.ParentId = null;
            }
            return await this.Bus.AddOrUpdateAsync(data);
        }
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<List<MenuTreeDto>> GetTreeAsync()
        {
            return this.Bus.GetTreeAsync();
        }
    }
}

