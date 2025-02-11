using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel
{
    public class lotnumModel 
    {
        public string LotNum { get; set; } = null!;
        public Decimal OnhandQty { get; set; }

    }
}
