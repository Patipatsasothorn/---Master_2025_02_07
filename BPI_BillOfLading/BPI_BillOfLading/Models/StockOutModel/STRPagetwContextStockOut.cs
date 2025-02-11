using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;

    public class STRPagetwContextStockOut : DbContext


    {
    public STRPagetwContextStockOut() { }

    public STRPagetwContextStockOut(DbContextOptions<STRPagetwContextStockOut> options) : base(options) { }

    public virtual DbSet<STRPageModelStockOut> STRPageModelStockOuts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<STRPageModelStockOut>().HasNoKey().ToView("BillOfLading");
    }


}


