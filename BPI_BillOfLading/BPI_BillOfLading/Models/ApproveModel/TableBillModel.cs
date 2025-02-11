using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.context
{
    public class TableBillModel 
    {
        public string PartNum { get; set; } = null!;

        public string PartDescription { get; set; } = null!;
        public Decimal Qty { get; set; } 
        public string Unit { get; set; } = null!;
        public string WareHouse { get; set; } = null!;
        public string Bin { get; set; } = null!;

    }
}
