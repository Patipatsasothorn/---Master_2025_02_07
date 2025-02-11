using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;

    public class onhandQtycontext : DbContext
    {
        public onhandQtycontext() { }

        public onhandQtycontext(DbContextOptions<onhandQtycontext> options) : base(options) { }

        public virtual DbSet<onhandQtyModel> onhandQtyModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แมปโมเดลกับ stored procedure
            modelBuilder.Entity<onhandQtyModel>().HasNoKey().ToView("onhandQty");
        }
    }

