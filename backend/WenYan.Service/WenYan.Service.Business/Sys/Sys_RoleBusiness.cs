namespace WenYan.Service.Business
{
    public class Sys_RoleBusiness : BusRepository<Sys_Role>, ISys_RoleBusiness, IScopedDependency
    {
        public Sys_RoleBusiness(GDbContext context) : base(context)
        {

        }

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PageResult<Sys_Role>> GetAllAsync(PageInput<SearchVM> query)
        {
            var search = query.Search;
            var queryable = this.GetQueryable<Sys_Role>(false);
            if (!search.KeyWord.IsNullOrEmpty())
                queryable = queryable.Where(x => x.Name.Contains(search.KeyWord) || x.Code.Contains(search.KeyWord));
            return await this.GetPageResultAsync(queryable, query);

        }
    }
}
