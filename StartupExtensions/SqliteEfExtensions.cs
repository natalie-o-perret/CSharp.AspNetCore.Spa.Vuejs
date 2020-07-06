using System;
using CSharp.AspNetCore.Spa.Vuejs.SqliteData;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
            keepAliveConnection.Disposed += (sender, args) => Console.WriteLine("Diposed!!!");
            services.AddDbContext<SqliteDbContext>(options =>
            {
                options.UseSqlite(keepAliveConnection);
            });
        }

        public static void UseSharedInMemorySqliteEf(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<SqliteDbContext>();
            var databaseCreator = (RelationalDatabaseCreator) dbContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
            databaseCreator.EnsureCreated();
            dbContext.SaveChanges();
        }
    }
}
