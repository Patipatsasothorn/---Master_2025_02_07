using System;
using System.Collections.Generic;

namespace BPI_UserSettings.Models.Data;

public partial class Reason
{
    public string Company { get; set; } = null!;

    public string ReasonType { get; set; } = null!;

    public string ReasonCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool DmrrejOpr { get; set; }

    public bool DmrrejMtl { get; set; }

    public bool DmrrejSub { get; set; }

    public bool DmrrejInv { get; set; }

    public bool DmracceptOpr { get; set; }

    public bool DmracceptMtl { get; set; }

    public bool DmracceptSub { get; set; }

    public bool DmracceptInv { get; set; }

    public bool InspFailOpr { get; set; }

    public bool InspFailMtl { get; set; }

    public bool InspFailSub { get; set; }

    public bool InspFailInv { get; set; }

    public bool Scrap { get; set; }

    public bool NonConfOpr { get; set; }

    public bool NonConfMtl { get; set; }

    public bool NonConfSub { get; set; }

    public bool NonConfOther { get; set; }

    public bool Qacause { get; set; }

    public bool QacorrectiveAct { get; set; }

    public bool InvAdjCountDiscrepancy { get; set; }

    public bool Passed { get; set; }

    public bool GlobalReason { get; set; }

    public bool GlobalLock { get; set; }

    public string JdfworkType { get; set; } = null!;

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public bool NonConfInv { get; set; }

    public bool ExternalMessyncRequired { get; set; }

    public DateTime? ExternalMeslastSync { get; set; }
}
