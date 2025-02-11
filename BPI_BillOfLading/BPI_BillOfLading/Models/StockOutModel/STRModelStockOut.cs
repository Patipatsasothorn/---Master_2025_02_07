using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel
{
    public class STRModelStockOut
    {
        public string Company { get; set; } = null!;
        public long DocId { get; set; } 
        public string DepName { get; set; } = null!;
        public string? UserName { get; set; } = null;
        public DateTime DocDate { get; set; }


    }
}

