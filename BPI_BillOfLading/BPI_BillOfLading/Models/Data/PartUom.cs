using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class PartUom
{
    public string Company { get; set; } = null!;

    public string PartNum { get; set; } = null!;

    public string Uomcode { get; set; } = null!;

    public decimal ConvFactor { get; set; }

    public bool Active { get; set; }

    public bool TrackOnHand { get; set; }

    public decimal NetVolume { get; set; }

    public string NetVolumeUom { get; set; } = null!;

    public bool HasBeenUsed { get; set; }

    public string ConvOperator { get; set; } = null!;

    public bool WebUom { get; set; }

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }
}
