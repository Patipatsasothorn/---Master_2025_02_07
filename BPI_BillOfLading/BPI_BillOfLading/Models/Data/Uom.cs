using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class Uom
{
    public string Company { get; set; } = null!;

    public string Uomcode { get; set; } = null!;

    public string Uomdesc { get; set; } = null!;

    public bool Active { get; set; }

    public string Uomsymbol { get; set; } = null!;

    public bool AllowDecimals { get; set; }

    public int NumOfDec { get; set; }

    public string Rounding { get; set; } = null!;

    public bool GlobalUom { get; set; }

    public bool GlobalLock { get; set; }

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public string Agafipcode { get; set; } = null!;

    public string Agcotcode { get; set; } = null!;

    public string Pesunatcode { get; set; } = null!;

    public string MxcustomsUom { get; set; } = null!;

    public string Mxsatcode { get; set; } = null!;

    public string PecommercialUom { get; set; } = null!;

    public string InternationalUomcode { get; set; } = null!;
}
