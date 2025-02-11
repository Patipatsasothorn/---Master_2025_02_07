using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel;

public class BillOfLoadingModelStockOut
{
    public long RowID { get; set; }
    public string WarehouseCode { get; set; } = null!;
    public string Description { get; set; } = null!;
}
