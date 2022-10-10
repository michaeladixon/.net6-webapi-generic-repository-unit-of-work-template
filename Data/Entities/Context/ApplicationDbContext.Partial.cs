using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Entities.Context
{
    public partial class ApplicationDbContext :DbContext
    {
        public IConfigurationRoot _configuration { get; set; }


        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)

                .AddJsonFile("appsettings.json")
                .Build();

            var cns = _configuration.GetConnectionString("dbContext");
            var connection = new SqliteConnection(cns);
            options.UseSqlite(connection);
        }

    }
}
