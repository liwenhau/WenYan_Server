namespace WenYan.Service.Business
{
    public class Sys_UserBusiness : BusRepository<Sys_User>, ISys_UserBusiness
    {
        private IServiceProvider SvcProvider { get; set; }
        public Sys_UserBusiness(GDbContext context, IServiceProvider svcProvider)
            : base(context)
        {
            SvcProvider = svcProvider;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Sys_User> LoginAsync(LoginM data)
        {
            var user = await this.GetAsync(x => x.UserName == data.UserName
            && x.Password == data.Password);
            _ = user ?? throw new Exception("用户名不存在或密码错误！");
            // 账号是否被冻结
            if (user.Status == "Disable")
                throw new Exception("用户名已被冻结，请联系管理员！"); ;
            return user;
        }

        /// <summary>
        /// 保存刷新token
        /// </summary>
        /// <param name="data"></param>
        /// <param name="refreshToken"></param>
        /// <param name="refreshHours"></param>
        /// <returns></returns>
        public async Task<int> SaveRefreshTokenAsync(Sys_User data, string refreshToken, int refreshHours)
        {
            var opSvc = this.SvcProvider.GetRequiredService<IOperator>();
            //更新RefreshToken
            data.RefreshToken = refreshToken;
            data.RefreshTokenExpiryTime = DateTime.Now.AddHours(refreshHours);
            data.ModifyTime = DateTime.Now;
            data.ModifyUserId = opSvc.UserId;
            return await this.UpdateAsync(data);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<UserInfoM> GetUserInfoAsync(string userId)
        {
            var user = await this.GetAsync(userId);
            var roleIds = await this.GetQueryable<Sys_UserRole>(true)
                .Where(w => w.UserId == userId)
                .Select(s => s.RoleId)
                .ToListAsync();
            var menuIds = await this.GetQueryable<Sys_RoleMenu>(true)
                .Where(w => roleIds.Contains(w.RoleId))
                .Select(s => s.MenuId)
                .ToListAsync();
            var roles = await this.GetQueryable<Sys_Role>(true)
                .Where(w => roleIds.Contains(w.Id))
                .Select(s => s.Code)
                .ToListAsync();
            var menus = await this.GetQueryable<Sys_Menu>(true)
                .Where(s => !string.IsNullOrEmpty(s.Permission))
                .Where(w => menuIds.Contains(w.Id))
                .ToListAsync();
            var permissions = menus.Select(s => s.Permission).ToList();
            return new UserInfoM()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Sex = user.Sex,
                Sign = user.Sign,
                Avatar = user.Avatar,
                Roles = roles,
                Permissions = permissions
            };
        }

        /// <summary>
        /// 获取当前用户菜单信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="isAllMenus">是否包含Type Button</param>
        /// <returns></returns>
        public async Task<List<UserMenuDto>> GetUserMenusAsync(string userId)
        {
            var user = await this.GetAsync(userId);
            _ = user ?? throw new("用户不存在!");
            if (user.Status == ConstDefaultConfig.Disable)
                throw new("用户被禁用!");

            var roleIds = await this.GetQueryable<Sys_UserRole>(true)
                .Where(w => w.UserId == userId)
                .Select(s => s.RoleId)
                .ToListAsync();
            var menuIds = await this.GetQueryable<Sys_RoleMenu>(true)
                .Where(w => roleIds.Contains(w.RoleId))
                .Select(s => s.MenuId)
                .ToListAsync();

            var menus = await this.GetQueryable<Sys_Menu>(true)
                .Where(w => menuIds.Contains(w.Id))
                .Where(w => w.Type != ConstDefaultConfig.Button)
                .Where(w => w.Status == ConstDefaultConfig.Enable)
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
                .ToListAsync();
            var treeMenus = TreeHelper.GetBaseTreeList(menus);
            return treeMenus;
        }


        /// <summary>
        /// 获取用户信息分页数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageResult<UserInfoDto>> GetPageAsync(PageInput<UserQM> query)
        {
            var search = query.Search;
            var queryable = this.GetQueryable<Sys_User>(false)
                .Include(x => x.Org)
                .AsSplitQuery();
            if (!search.KeyWord.IsNullOrEmpty())
                queryable = queryable.Where(x => x.Name.Contains(search.KeyWord)
                || x.Code.Contains(search.KeyWord)
                || x.UserName.Contains(search.KeyWord)
                || x.Remark.Contains(search.KeyWord)
                );
            if (!search.Status.IsNullOrEmpty())
                queryable = queryable.Where(x => x.Status == search.Status);
            if (search.Orgs.Count() > 0)
            {
                var orgBus = this.SvcProvider.GetRequiredService<ISys_OrgBusiness>();
                var orgIds = await orgBus.GetQueryable()
                    .Where(x => search.Orgs.Contains(x.ParentId))
                    .Select(x => x.Id)
                    .ToListAsync();
                var allOrgIds = orgIds.Union(search.Orgs).ToList();
                queryable = queryable.Where(x => allOrgIds.Contains(x.OrgId));
            }

            var userInfoQuery = queryable.Select(x => new UserInfoDto()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                UserName = x.UserName,
                Avatar = x.Avatar,
                Sex = x.Sex,
                Sign = x.Sign,
                Status = x.Status,
                OrgName = x.Org.Name,
                Remark = x.Remark,
                CreateTime = x.CreateTime,
                ModifyTime = x.ModifyTime,
                RefreshTokenExpiryTime = x.RefreshTokenExpiryTime,
            });
            return await userInfoQuery.GetPageResultAsync(query);
        }

        public async Task<UserDetailM> GetUserDetailAsync(string userId)
        {
            var user = await this.GetAsync(userId);
            var roleIds = await this.GetQueryable<Sys_UserRole>(true)
                .Where(w => w.UserId == userId)
                .Select(s => s.RoleId)
                .ToListAsync();
            return new UserDetailM()
            {
                Id = user.Id,
                Code = user.Code,
                Name = user.Name,
                UserName = user.UserName,
                Sex = user.Sex,
                Sign = user.Sign,
                Avatar = user.Avatar,
                Status = user.Status,
                Remark = user.Remark,
                OrgId = user.OrgId,
                RoleIds = roleIds
            };
        }

        public async Task<int> AddOrUpdateAsync(UserInputDto entity)
        {
            if (entity.Password.IsNullOrEmpty())
                entity.Password = ConstDefaultConfig.DefaultPwd;
            var dbEntity = await this.GetAsync(entity.Id, true);
            if (dbEntity != null)
            {
                entity.Password = dbEntity.Password;
                await this.UpdateAsync(entity);
            }
            else
                await this.AddAsync(entity);

            var userRoleSvc = this.SvcProvider.GetRequiredService<ISys_UserRoleBusiness>();
            return await userRoleSvc.AddAsync(entity.Id, entity.RoleIds);
        }
    }
}


