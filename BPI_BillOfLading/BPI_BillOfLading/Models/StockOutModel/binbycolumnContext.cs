using BPI_BillOfLading.Models.StockOutModel;
using Microsoft.EntityFrameworkCore;

public class binbycolumnContext : DbContext
{
    public binbycolumnContext() { }

    // แก้ไข DbContextOptions ให้ใช้ประเภท binbycolumnContext
    public binbycolumnContext(DbContextOptions<binbycolumnContext> options) : base(options) { }

    public virtual DbSet<binbycolumnModel> binbycolumnModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<binbycolumnModel>().HasNoKey().ToView("binbycolumn");
    }
}
