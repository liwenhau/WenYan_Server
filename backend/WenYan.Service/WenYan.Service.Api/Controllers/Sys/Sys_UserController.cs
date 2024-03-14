namespace WenYan.Service.Api.Controllers
{
    public class Sys_UserController : BaseController
    {
        private ISys_UserBusiness _bus { get; set; }

        public Sys_UserController(ISys_UserBusiness bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<UserInfoDto>> GetPageAsync(PageInput<UserQM> query)
        {
            return await _bus.GetPageAsync(query);
        }

        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserInfoM> GetAsync(string id)
        {
            return await _bus.GetUserInfoAsync(id);
        }

        /// <summary>
        /// 根据id获取用户详情信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserDetailM> GetUserDetailAsync(string id)
        {
            return await _bus.GetUserDetailAsync(id);
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

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<int> ChangePasswordAsync(ChangePwdDto data)
        {
            return await this._bus.ChangePasswordAsync(data);
        }
    }
}
