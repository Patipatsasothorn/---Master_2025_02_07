using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.ApproveModel
{
    public class ApproveModel 
    {
        public string Company { get; set; } = null!;

        public string Plant { get; set; } = null!;
        public long DocID { get; set; }
        public string Status { get; set; } = null!;
        public DateTime DocDate { get; set; }
        public string DepName { get; set; } = null!;
        public string Name { get; set; } = null!;

        public long CreateBy { get; set; }
        public string Reason { get; set; } = null!;
        public string Dep { get; set; } = null!;
        public DateTime? ReqDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public long UpdateBy { get; set; }

        public string Remark { get; set; } = null!;


    }
}
