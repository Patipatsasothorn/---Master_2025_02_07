using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ReportModel
{
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions<ReportContext> options) : base(options) { }

        public virtual DbSet<ReportModel> ReportModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportModel>().HasNoKey().ToView("rpt_PickUp");
        }
    }
}
