using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.context
{
    public class STRModel
    {
        public string Description { get; set; } = null!;

        public string DepName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime? ReqDate { get; set; }
        public string Remark { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime DocDate { get; set; }
        public string Plant { get; set; }


    }
}
