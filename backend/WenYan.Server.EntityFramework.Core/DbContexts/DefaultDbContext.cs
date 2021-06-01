using Furion.DatabaseAccessor;

using Microsoft.EntityFrameworkCore;

namespace WenYan.Server.EntityFramework.Core
{
    [AppDbContext("DefaultConnection",DbProvider.SqlServer)]
    public class DefaultDbContext : AppDbContext<DefaultDbContext>
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {

        }
    }
}
