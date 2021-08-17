using System.Threading.Tasks;
using Application.Interfaces;

namespace Persistence.Seeding
{
    public abstract class ContextSeeder
    {
        public abstract Task<bool> SeedDatabase(IDjValetingContext context);
    }
}