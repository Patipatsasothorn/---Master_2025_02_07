using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ReturnStockModel;

    public class RSTRPagetwContextStockOut : DbContext


    {
    public RSTRPagetwContextStockOut() { }

    public RSTRPagetwContextStockOut(DbContextOptions<RSTRPagetwContextStockOut> options) : base(options) { }

    public virtual DbSet<RSTRPageModelStockOut> RSTRPageModelStockOuts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<RSTRPageModelStockOut>().HasNoKey().ToView("returnstockout");
    }


}


