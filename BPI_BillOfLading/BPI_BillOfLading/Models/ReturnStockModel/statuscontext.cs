using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ReturnStockModel
{
    public class statuscontext : DbContext
    {
        public statuscontext() { }

        public statuscontext(DbContextOptions<statuscontext> options) : base(options) { }

        public virtual DbSet<statusmodel> statusmodels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แมปโมเดลกับ stored procedure
            modelBuilder.Entity<statusmodel>().HasNoKey().ToView("status");
        }
    }
}
