using System.ComponentModel.DataAnnotations;

namespace BPI_UserSettings.Models
{
    public class ReasonModel
    {
        [Key]
        public string DataCode { get; set; }
        public string Description { get; set; }
        public long? RowId { get; set; }
    }
}
