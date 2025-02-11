using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel;

    public class binbycolumnModel 
    {
    public string Bin { get; set; } = null!;
    public string WBDescription { get; set; } = null!;

    public Decimal OnhandQty { get; set; }

}

