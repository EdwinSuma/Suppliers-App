using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DataModel
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table Mappings
            modelBuilder.Entity<Supplier>().ToTable("SuppliersINV");
            modelBuilder.Entity<Product>().ToTable("ProductsINV");
            modelBuilder.Entity<PurchaseOrderHeader>().ToTable("PurchaseOrderHeadersINV");
            modelBuilder.Entity<PurchaseOrderDetail>().ToTable("PurchaseOrderDetailsINV");

            // Product Date Precision
            modelBuilder.Entity<Product>()
                .Property(p => p.DateAdded)
                .HasColumnType("datetime2(7)");

            modelBuilder.Entity<Product>()
                .Property(p => p.DateModified)
                .HasColumnType("datetime2(7)");

            // PurchaseOrderDetail Price Precision
            modelBuilder.Entity<PurchaseOrderDetail>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Relationships for PurchaseOrderHeader
            modelBuilder.Entity<PurchaseOrderHeader>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.PurchaseOrderHeaders)
                .HasForeignKey(p => p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationships for PurchaseOrderDetail
            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasOne(d => d.Product)
                .WithMany(p => p.PurchaseOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasOne(d => d.PurchaseOrderHeader)
                .WithMany(h => h.PurchaseOrderDetails)
                .HasForeignKey(d => d.PurchaseOrderHeaderId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

    }
}
