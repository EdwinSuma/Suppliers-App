using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.DataModel
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-RB1M56NU\\SQLEXPRESS;" +
                "Database=Entprog_Enrollment;Integrated Security=SSPI;" +
                "TrustServerCertificate=true");
        }

        public DbSet<Supplier> Suppliers { get; set; }
    }
}
