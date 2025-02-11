using System;
using System.Collections.Generic;

namespace BPI_PickUp.Models.Data;

public partial class BolDocHead
{
    public string Company { get; set; } = null!;

    public string Plant { get; set; } = null!;

    public long DocId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DocDate { get; set; }

    public long CreateBy { get; set; }

    public string? Reason { get; set; }

    public string Dep { get; set; } = null!;

    public DateTime? ReqDate { get; set; }

    public string? Remark { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? UpdateBy { get; set; }
}
