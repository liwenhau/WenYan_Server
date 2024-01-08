using System.Security;

namespace WenYan.Service.Business
{
    public class Sys_UserRoleBusiness : BaseRepository<Sys_UserRole>, ISys_UserRoleBusiness, IScopedDependency
    {
        public Sys_UserRoleBusiness(GDbContext context) : base(context)
        {

        }

        public async Task<int> AddAsync(string userId, List<string> roleIds)
        {
            var dbUserRoles = await this.GetListAsync(x => x.UserId == userId);
            var delUserRoles = dbUserRoles.Where(x => !roleIds.Contains(x.RoleId)).ToList();
            if (delUserRoles.Count > 0)
                await this.DeleteAsync(delUserRoles);

            var exceptUserRoleIds = roleIds.Except(dbUserRoles.Select(x => x.RoleId)).ToList();

            var newUserRoles = exceptUserRoleIds.Select(s => new Sys_UserRole() { UserId = userId, RoleId = s }).ToList();
            if (newUserRoles.Count > 0)
                return await this.AddAsync(newUserRoles);
            return roleIds.Count;
        }
    }
}
