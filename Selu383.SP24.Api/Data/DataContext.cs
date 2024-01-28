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
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<HotelController> Hotels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelController>()
                .Property(h => h.Id)
                .IsRequired();

            modelBuilder.Entity<HotelController>()
                .Property(h => h.Name)
                .IsRequired();

            modelBuilder.Entity<HotelController>()
                .Property(h => h.Address)
                .IsRequired();
        }
    }
}