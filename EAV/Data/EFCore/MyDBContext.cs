using Microsoft.EntityFrameworkCore;
using System;

namespace EAV.Models.EFCore
{
    public class MyDBContext : DbContext
    {
        public MyDBContext (DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductProperty<int>>()
                   .HasKey(p => new { p.ProductId, p.PropertyId });

            modelBuilder.Entity<ProductProperty<decimal>>()
                   .HasKey(p => new { p.ProductId, p.PropertyId });

            modelBuilder.Entity<ProductProperty<string>>()
                   .HasKey(p => new { p.ProductId, p.PropertyId });

            modelBuilder.Entity<ProductProperty<DateTime>>()
                   .HasKey(p => new { p.ProductId, p.PropertyId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products{ get; set; }
        public DbSet<Property> Properties { get; set; }
        public virtual DbSet<ProductProperty<int>> Properties_int { get; set; }
        public virtual DbSet<ProductProperty<decimal>> Properties_decimal { get; set; }
        public virtual DbSet<ProductProperty<string>> Properties_string { get; set; }
        public virtual DbSet<ProductProperty<DateTime>> Properties_dateTime { get; set; }
    }
}
