using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models.Data;

public partial class Holiday
{
    [Key]
    public string? Company { get; set; }

    public DateOnly? Holiday1 { get; set; }

    public string? Detail { get; set; }

    public DateTime? CreateDate { get; set; }

    public long? CreateBy { get; set; }
}
