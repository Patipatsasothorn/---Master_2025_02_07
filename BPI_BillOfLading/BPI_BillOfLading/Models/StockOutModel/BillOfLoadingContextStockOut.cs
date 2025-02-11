using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;

    public class BillOfLoadingContextStockOut : DbContext
    {

        public BillOfLoadingContextStockOut() { }

        public BillOfLoadingContextStockOut(DbContextOptions<BillOfLoadingContextStockOut> options) : base(options) { }

        public virtual DbSet<BillOfLoadingModelStockOut> BillOfLoadingModelStockOuts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แมปโมเดลกับ stored procedure
            modelBuilder.Entity<BillOfLoadingModelStockOut>().HasNoKey().ToView("BPI_BillOfLoading_Wh");
        }
    }

