using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel;

public class RTableBillModelStockOut
{
    public string PartNum { get; set; } = null!;

    public string PartDescription { get; set; } = null!;
    public Decimal Qty { get; set; }
    public string Unit { get; set; } = null!;
    public Decimal OnhandQty { get; set; }
    public Decimal TotalQty { get; set; }
    public string DimCode { get; set; } = null!;
    public string LotNum { get; set; }
    public long DetailID { get; set; }
    public long PaidId { get; set; }
    public string Plant { get; set; }
    public string Reason { get; set; }
    public Decimal ConvU { get; set; }
    public Decimal ConDim { get; set; }
    public Decimal ReturnQTY { get; set; }
    public string returnunit { get; set; } = null!;
    public string WBDescription { get; set; } = null!;
    public string UOMDesc { get; set; } = null!;
    public string UOMre { get; set; } = null!;


    public string Bin { get; set; } = null!;
}
