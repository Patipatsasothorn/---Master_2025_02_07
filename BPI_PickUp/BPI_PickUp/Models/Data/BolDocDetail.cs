using System;
using System.Collections.Generic;

namespace BPI_PickUp.Models.Data;

public partial class BolDocDetail
{
    public long DetailId { get; set; }

    public long? DocId { get; set; }

    public string? PartNum { get; set; }

    public decimal? Qty { get; set; }

    public string? Unit { get; set; }

    public string? WareHouse { get; set; }

    public string? Bin { get; set; }
}
