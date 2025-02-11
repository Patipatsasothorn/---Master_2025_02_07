using Microsoft.AspNetCore.Mvc;

namespace BPI_BillOfLading.Models.StockOutModel;

// Model สำหรับรับข้อมูล
public class CsvRequest
{
    public string CsvData { get; set; }
    public string FileName { get; set; }
}
