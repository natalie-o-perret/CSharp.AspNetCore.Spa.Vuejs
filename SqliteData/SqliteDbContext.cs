using System.Linq;

using Bogus;
using Bogus.Extensions;

using Microsoft.EntityFrameworkCore;

namespace CSharp.AspNetCore.Spa.Vuejs.SqliteData
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<SqliteRecord>? Records { get; set; }

        public SqliteDbContext() // : base()
        {
        }
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var faker = new Faker();
            var recordCount = faker.Random.Int(5_000, 10_000);
            var records = Enumerable
                .Range(0, recordCount)
                .Select(index =>
                    new SqliteRecord
                    {
                        Id = index,
                        Integer = faker.Random.Long(),
                        Real = faker.Random.Double(),
                        NullableText = faker.Name.FullName().OrNull(faker, .2f),
                        NonNullableText = faker.Commerce.Product()
                    });

            modelBuilder
                .Entity<SqliteRecord>()
                .HasKey(x => x.Id);
            modelBuilder
                .Entity<SqliteRecord>()
                .ToTable("Records")
                .HasData(records);
        }
    }
}
