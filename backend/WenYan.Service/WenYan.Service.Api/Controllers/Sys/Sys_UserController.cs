namespace WenYan.Service.Api.Controllers
{
    public class Sys_UserController : BaseController
    {
        private ISys_UserBusiness Bus { get; set; }

        public Sys_UserController(ISys_UserBusiness bus)
        {
            Bus = bus;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<UserInfoDto>> GetPageAsync(PageInput<UserQM> query)
        {
            return await Bus.GetPageAsync(query);
        }

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserInfoM> GetAsync(string id)
        {
            return await Bus.GetUserInfoAsync(id);
        }

        /// <summary>
        /// 根据id获取用户详情信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDetailM> GetUserDetailAsync(string id)
        {
            return await Bus.GetUserDetailAsync(id);
        }

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> SaveAsync(UserInputDto data)
        {
            this.InitBusEntity(data);
            return await this.Bus.AddOrUpdateAsync(data);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="ids">数据主键</param>
        /// <returns></returns>
        [HttpDelete]
        public Task<int> DeleteAsync(List<string> ids)
        {
            return this.Bus.DeleteAsync(ids);
        }
    }
}
