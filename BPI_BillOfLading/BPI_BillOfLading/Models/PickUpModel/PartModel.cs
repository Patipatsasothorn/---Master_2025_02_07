using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models.PickUpModel
{
    public class PartModel
    {
        public string PartNum { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string Warehouse { get; set; }
        public string Bin { get; set; }
    }
}
