using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel;

public class onhandQtyModel 
{
    public string DimCode { get; set; } = null!;
    public Decimal OnhandQty { get; set; }
    public string UOMDesc { get; set; } = null!;
    public Decimal Convf { get; set; }

}
