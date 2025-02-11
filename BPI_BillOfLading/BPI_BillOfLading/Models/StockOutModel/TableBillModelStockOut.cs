using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel;

public class TableBillModelStockOut
{
    public string PartNum { get; set; } = null!;

    public string PartDescription { get; set; } = null!;
    public Decimal Qty { get; set; }
    public string Unit { get; set; } = null!;
    public Decimal TotalQty { get; set; }
    public long DetailID { get; set; }
    public long PaidId { get; set; }
    public string Plant { get; set; }
    public string Reason { get; set; }
    public Decimal ConvU { get; set; }
    public Decimal ConDim { get; set; }

    public string UOMDesc { get; set; } = null!;
    public string LotNum { get; set; } = null!;
    public string DimCode { get; set; } = null!;
    public string Bin { get; set; } = null!;
    public string WareHouse { get; set; } = null!;
    public string WHDescription { get; set; } = null!;
    public string WBDescription { get; set; } = null!;

    public string UOMOn { get; set; } = null!;
    public Decimal OnhandQty { get; set; }

}
