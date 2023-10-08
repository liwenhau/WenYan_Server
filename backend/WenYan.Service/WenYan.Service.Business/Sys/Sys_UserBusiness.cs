namespace WenYan.Service.Business
{
    public class Sys_UserBusiness : BaseRepository<Sys_User>, ISys_UserBusiness
    {
        public Sys_UserBusiness(GDbContext context) : base(context)
        {

        }
        public Task<Sys_User> LoginAsync(LoginM data)
        {
            throw new NotImplementedException();
        }
    }
}


