using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Persistence.Seeding
{
    public abstract class ContextSeeder
    {
        protected ContextSeeder(ILogger<ContextSeeder> logger)
        {
            Logger = logger;
        }
        protected ILogger<ContextSeeder> Logger { get; }
        
        public abstract Task<bool> SeedDatabase(IDjValetingContext context);
    }
}