using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class WhseBin
{
    public string Company { get; set; } = null!;

    public string WarehouseCode { get; set; } = null!;

    public string BinNum { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool NonNettable { get; set; }

    public string SizeId { get; set; } = null!;

    public string ZoneId { get; set; } = null!;

    public int BinSeq { get; set; }

    public string BinType { get; set; } = null!;

    public int CustNum { get; set; }

    public int VendorNum { get; set; }

    public string Aisle { get; set; } = null!;

    public string Face { get; set; } = null!;

    public int Elevation { get; set; }

    public decimal MaxFill { get; set; }

    public decimal PctFillable { get; set; }

    public bool InActive { get; set; }

    public bool Portable { get; set; }

    public bool Replenishable { get; set; }

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public bool SendToFsa { get; set; }

    public Guid? ForeignSysRowId { get; set; }

    public byte[]? UdSysRevId { get; set; }

    public decimal? Number01 { get; set; }

    public decimal? Number02 { get; set; }

    public decimal? Number03 { get; set; }

    public decimal? Number04 { get; set; }

    public decimal? Number05 { get; set; }

    public string? Character01 { get; set; }

    public string? Character02 { get; set; }

    public string? Character03 { get; set; }

    public string? Character04 { get; set; }

    public string? Character05 { get; set; }

    public bool? CheckBox01 { get; set; }

    public bool? CheckBox02 { get; set; }

    public bool? CheckBox03 { get; set; }

    public bool? CheckBox04 { get; set; }

    public bool? CheckBox05 { get; set; }

    public DateTime? Date01 { get; set; }

    public DateTime? Date02 { get; set; }

    public DateTime? Date03 { get; set; }

    public DateTime? Date04 { get; set; }

    public DateTime? Date05 { get; set; }

    public DateTime? AdtTimeToSiteFromC { get; set; }

    public string? AdtTimeToSiteToC { get; set; }

    public DateTime? AdtTimeToSiteTo01C { get; set; }

    public string? AdtTimeToSiteRemarkC { get; set; }

    public decimal? AdtTimeTositeFrom01C { get; set; }

    public decimal? AdtTimeTositeTo02C { get; set; }

    public string? AdtTimeToSiteFrom05C { get; set; }

    public string? AdtTimeToSiteTo05C { get; set; }
}
