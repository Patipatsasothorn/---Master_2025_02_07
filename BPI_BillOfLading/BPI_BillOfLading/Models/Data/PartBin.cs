using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class PartBin
{
    public string Company { get; set; } = null!;

    public string PartNum { get; set; } = null!;

    public string WarehouseCode { get; set; } = null!;

    public string BinNum { get; set; } = null!;

    public decimal OnhandQty { get; set; }

    public string LotNum { get; set; } = null!;

    public string DimCode { get; set; } = null!;

    public decimal AllocatedQty { get; set; }

    public decimal SalesAllocatedQty { get; set; }

    public decimal SalesPickingQty { get; set; }

    public decimal SalesPickedQty { get; set; }

    public decimal JobAllocatedQty { get; set; }

    public decimal JobPickingQty { get; set; }

    public decimal JobPickedQty { get; set; }

    public decimal TfordAllocatedQty { get; set; }

    public decimal TfordPickingQty { get; set; }

    public decimal TfordPickedQty { get; set; }

    public decimal ShippingQty { get; set; }

    public decimal PackedQty { get; set; }

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public string Pcid { get; set; } = null!;

    public bool SendToFsa { get; set; }

    public int AttributeSetId { get; set; }

    public DateOnly? CountedDate { get; set; }

    public decimal QtyPerPiece { get; set; }

    public string RevisionNum { get; set; } = null!;
}
