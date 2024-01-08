namespace WenYan.Service.IBusiness
{
    public interface ISys_UserBusiness: IBusRepository<Sys_User>,IScopedDependency
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="data">用户登录信息</param>
        /// <returns></returns>
        Task<Sys_User> LoginAsync(LoginM data);

        /// <summary>
        /// 保存刷新Token
        /// </summary>
        /// <param name="data">用户</param>
        /// <param name="refreshToken">刷新Token</param>
        /// <param name="refreshHours">刷新Token过期时间</param>
        /// <returns></returns>
        Task<int> SaveRefreshTokenAsync(Sys_User data,string refreshToken, int refreshHours);

        /// <summary>
        ///  获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<UserInfoM> GetUserInfoAsync(string userId);

        /// <summary>
        ///  获取用户详情
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<UserDetailM> GetUserDetailAsync(string userId);


        /// <summary>
        ///  获取用户菜单权限信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="isAllMenus">是否包含Type Button</param>
        /// <returns></returns>
        Task<List<UserMenuDto>> GetUserMenusAsync(string userId);

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<PageResult<UserInfoDto>> GetPageAsync(PageInput<UserQM> query);

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<int> AddOrUpdateAsync(UserInputDto input);
    }
}
