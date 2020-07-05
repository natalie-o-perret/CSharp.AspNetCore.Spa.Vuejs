using CSharp.AspNetCore.Spa.Vuejs.SqliteData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CSharp.AspNetCore.Spa.Vuejs.StartupExtensions
{
    public static class SqliteEfExtensions
    {
        public static void AddSharedInMemorySqliteEf(this IServiceCollection services)
        {
            // See https://stackoverflow.com/a/56367786/4636721
            const string connectionString = "DataSource=myshareddb;mode=memory;cache=shared";
            SQLitePCL.Batteries.Init();
            var keepAliveConnection = new SqliteConnection(connectionString);
            keepAliveConnection.Open();

            services.AddDbContext<SqliteDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
        }
    }
}
