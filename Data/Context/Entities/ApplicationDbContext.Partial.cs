using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.Context.Entities
{
    //Files named with xxx.partial are custom files that will not change when EF power tools is ran.
    //This is the base logic that will likely not be changed.
    public partial class ApplicationDbContext : DbContext, IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public static IConfiguration? Configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration? configuration)
            : base(new DbContextOptionsBuilder().UseSqlite(configuration.GetConnectionString("dbContext")).Options)
        {
            Configuration = configuration;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

          optionsBuilder.UseSqlite(Configuration.GetConnectionString("dbContext"));
 

        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("dbContext"));
            return new ApplicationDbContext(optionsBuilder.Options, Configuration);
        }


    }
}
