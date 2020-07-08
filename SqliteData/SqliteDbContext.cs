using System;
using System.Linq;
using Bogus;
using Bogus.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.FSharp.Core;

namespace CSharp.AspNetCore.Spa.Vuejs.SqliteData
{
    public static class FSharpOptionExtensions
    {
        public static T DefaultFromOption<T>(this FSharpOption<T> option)
            where T : IEquatable<T> =>
            (FSharpOption<T>.get_IsNone(option)
                ? default
                : option.Value)!;

        public static FSharpOption<T> ToOption<T>(this T source)
            where T : IEquatable<T> =>
            source.Equals(default)
                ? FSharpOption<T>.None
                : FSharpOption<T>.Some(source);
    }

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
            var recordCount = faker.Random.Int(500, 1000);
            var records = Enumerable
                .Range(1, recordCount)
                .Select(index => new SqliteRecord
                {
                    Id = index,
                    Integer = faker.Random.Long(),
                    Real = faker.Random.Double(),
                    NullableText = faker.Name.FullName().OrNull(faker, .2f),
                    NonNullableText = faker.Commerce.Product(),
                    FSharpOptionText = FSharpOption<string>.Some(faker.Vehicle.Manufacturer())
                        .OrDefault(faker, 0.5f, FSharpOption<string>.None)
                });

            modelBuilder
                .Entity<SqliteRecord>()
                .Property(x => x.FSharpOptionText)
                .IsRequired(false)
                .HasConversion(new ValueConverter<FSharpOption<string>?, string>(
                    option => option!.DefaultFromOption(),
                    value => value.ToOption()));

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
