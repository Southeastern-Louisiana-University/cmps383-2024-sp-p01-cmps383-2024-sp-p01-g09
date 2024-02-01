using Selu383.SP24.Api.Entities;
using Microsoft.EntityFrameworkCore;




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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Id)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Hotel>()
                .Property(x => x.Address)
                .IsRequired();
            
        }
    }
}
