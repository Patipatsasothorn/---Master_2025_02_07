using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models.PickUpModel
{
    public class DocumentModel
    {
        public string Company { get; set; }
        public string DocumentNumber { get; set; }
        public string Date { get; set; }
        public string Plant { get; set; }
        public string Reason { get; set; }
        public string Department { get; set; }
        public string RequiredDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public List<PartModel> Parts { get; set; }
        public string Username { get; set; }
        //public string Bypass { get; set; }
    }
}
