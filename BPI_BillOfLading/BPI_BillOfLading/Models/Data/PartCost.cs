using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class PartCost
{
    public string Company { get; set; } = null!;

    public string PartNum { get; set; } = null!;

    public string CostId { get; set; } = null!;

    public decimal AvgLaborCost { get; set; }

    public decimal AvgBurdenCost { get; set; }

    public decimal AvgMaterialCost { get; set; }

    public decimal AvgSubContCost { get; set; }

    public decimal AvgMtlBurCost { get; set; }

    public decimal StdLaborCost { get; set; }

    public decimal StdBurdenCost { get; set; }

    public decimal StdMaterialCost { get; set; }

    public decimal StdSubContCost { get; set; }

    public decimal StdMtlBurCost { get; set; }

    public decimal LastLaborCost { get; set; }

    public decimal LastBurdenCost { get; set; }

    public decimal LastMaterialCost { get; set; }

    public decimal LastSubContCost { get; set; }

    public decimal LastMtlBurCost { get; set; }

    public decimal FifoavgLaborCost { get; set; }

    public decimal FifoavgBurdenCost { get; set; }

    public decimal FifoavgMaterialCost { get; set; }

    public decimal FifoavgSubContCost { get; set; }

    public decimal FifoavgMtlBurCost { get; set; }

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public decimal TotalQtyAvg { get; set; }

    public bool ExternalMessyncRequired { get; set; }

    public DateTime? ExternalMeslastSync { get; set; }
}
