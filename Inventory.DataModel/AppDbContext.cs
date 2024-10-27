using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.DataModel
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-EDG1IN1\\SQLEXPRESS;Database=eisensy_csbentprog;User Id=eisensy_student;Password=Benilde@2020;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Supplier>().ToTable("SuppliersINV");
            modelBuilder.Entity<Product>().ToTable("ProductsINV");

            modelBuilder.Entity<Product>()
                .Property(p => p.DateAdded)
                .HasColumnType("datetime2(7)");

            modelBuilder.Entity<Product>()
                .Property(p => p.DateModified)
                .HasColumnType("datetime2(7)");
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
