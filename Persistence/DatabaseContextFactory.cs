using Microsoft.EntityFrameworkCore;
using Persistence.Infrastructure;

namespace Persistence
{
    public class DatabaseContextFactory : DesignTimeDbContextFactoryBase<DjValetingContext>
    {
        protected override DjValetingContext CreateNewInstance(DbContextOptions<DjValetingContext> options)
        {
            DjValetingContext djValetingContext = new(options);
            djValetingContext.Database.EnsureCreated();

            return djValetingContext;
        }
    }
}