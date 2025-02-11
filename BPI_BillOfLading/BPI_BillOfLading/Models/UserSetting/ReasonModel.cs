using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models.UserSetting
{
    public class ReasonModel
    {
        [Key]
        public string DataCode { get; set; }
        public string Description { get; set; }
        public long? RowId { get; set; }
    }
}
