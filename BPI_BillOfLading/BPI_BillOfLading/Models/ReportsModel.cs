using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models
{
    public class ReportsModel
    {
        public string Company { get; set; }        // ชื่อบริษัท
        [Key]
        public long DocId { get; set; }          // รหัสเอกสาร
        public DateTime DocDate { get; set; }         // วันที่สร้างเอกสาร
        public string UserName { get; set; }  // ชื่อผู้เบิก
        public string DepName { get; set; }     // หน่วยงาน
        //public string WarehouseDescription { get; set; }      // คลังสินค้า
        public DateTime ReqDate { get; set; } // วันที่ต้องการสินค้า
        //public int Sequence { get; set; }          // ลำดับ
        //public string BinNum { get; set; }            // BIN
        public string PartNum { get; set; }     // รหัสสินค้า
        public string PartDescription { get; set; }    // รายการสินค้า
        public decimal Qty { get; set; }          // จำนวน
        public string Unit { get; set; }           // หน่วย
    }
}
