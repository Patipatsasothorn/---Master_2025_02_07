using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel
{
    public class lotContext : DbContext
    {
        public lotContext() { }

        public lotContext(DbContextOptions<lotContext> options) : base(options) { }

        public virtual DbSet<lotnumModel> lotnumModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แมปโมเดลกับ stored procedure
            modelBuilder.Entity<lotnumModel>().HasNoKey().ToView("Lotnum");
        }
    }
}
