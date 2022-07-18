using Microsoft.EntityFrameworkCore;
using SFTPDownloadWorkerDemo.Entities;

namespace SFTPDownloadWorkerDemo.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("ServiceDatabase"));
        }

        public DbSet<Entities.File> Files { get; set; }
    }
}







