using Microsoft.EntityFrameworkCore;
namespace SHPEGPCActionItemsDashboard.DataAccess
{
    public class LocalDatabaseContext: DbContext
    {
        public LocalDatabaseContext(DbContextOptions<LocalDatabaseContext> options)
            : base(options)
        {
        }
    }
}
