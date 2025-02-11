using System.ComponentModel.DataAnnotations;

namespace BPI_PickUp.Models
{
    public class PartModel
    {
        public string PartNum { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Warehouse { get; set; }
        public string Bin { get; set; }
    }
}
