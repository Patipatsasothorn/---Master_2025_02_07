namespace BPI_BillOfLading.Models.ReportModel
{
    public class ReportModel
    {
        public string DocDate { get; set; }              // วันที่เบิก
        public long DocID { get; set; }   // เลขที่ใบเบิก
        public string DepName { get; set; }       // แผนก
        public string UserName { get; set; }        // คลัง
        //public string? Bin { get; set; }              // Bin
        public string PartNum { get; set; }         // รหัสสินค้า
        public string PartDescription { get; set; }      // รายละเอียด
        public string Qty { get; set; }            // จำนวน
        public string UOMDesc { get; set; }             // หน่วย
        public string ReqDate { get; set; }      // วันที่ต้องการ
        public string Description { get; set; }           // เหตุผลในการเบิก
        public string Status { get; set; }           // สถานะเอกสาร
        public string Remark { get; set; }             // หมายเหตุ
    }
}
