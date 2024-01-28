using System.Collections;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Selu383.SP24.Tests.Helpers;
using Selu383.SP24.Api.Entities;

namespace Selu383.SP24.Tests;


namespace Selu383.SP24.Api.Data
{
    public sealed class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>()
                .Property(x => x.Id)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Name)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Address)
                .IsRequired();
        }
    }
}