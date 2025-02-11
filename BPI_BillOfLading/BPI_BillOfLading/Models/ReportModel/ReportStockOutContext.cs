using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ReportModel
{
    public class ReportStockOutContext : DbContext
    {
        public ReportStockOutContext(DbContextOptions<ReportStockOutContext> options) : base(options) { }

        public virtual DbSet<ReportStockOutModel> ReportStockOutModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportStockOutModel>().HasNoKey().ToView("reportStockout");
        }
    }
}
