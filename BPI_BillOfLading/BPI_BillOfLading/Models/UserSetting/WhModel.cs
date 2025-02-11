using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models.UserSetting
{
    public class WhModel
    {
        [Key]
        public long? RowId { get; set; }
        public string WarehouseCode { get; set; }
        public string Description { get; set; }

    }
}
