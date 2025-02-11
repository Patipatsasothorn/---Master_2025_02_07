using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BPI_PickUp.Models
{
    public class DocumentModel
    {
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
        public string Plant { get; set; }
        public string Reason { get; set; }
        public string Department { get; set; }
        public DateTime RequiredDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public List<PartModel> Parts { get; set; }
    }
}
