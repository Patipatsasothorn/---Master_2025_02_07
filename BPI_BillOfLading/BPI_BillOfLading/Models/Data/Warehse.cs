using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class Warehse
{
    public string Company { get; set; } = null!;

    public string WarehouseCode { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public string Address3 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Gldivision { get; set; } = null!;

    public int CountryNum { get; set; }

    public string Plant { get; set; } = null!;

    public int CcselectMethod { get; set; }

    public bool ExcludeInactive { get; set; }

    public bool ExcludeOnHold { get; set; }

    public bool ExcludeZeroQoh { get; set; }

    public bool ExcludeNegQoh { get; set; }

    public decimal LastSheetNum { get; set; }

    public decimal LastTagNum { get; set; }

    public string ManagerName { get; set; } = null!;

    public string DefRcvWhse { get; set; } = null!;

    public string DefRcvBin { get; set; } = null!;

    public string DefShipWhse { get; set; } = null!;

    public string DefShipBin { get; set; } = null!;

    public string PhoneNum { get; set; } = null!;

    public string FaxNum { get; set; } = null!;

    public string SalesRepCode { get; set; } = null!;

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public bool EnforcePkgControlRules { get; set; }

    public bool AllowBuildParent { get; set; }

    public bool IsHoldWarehouse { get; set; }

    public string WarehouseType { get; set; } = null!;

    public bool WarehouseTypeDefault { get; set; }

    public bool SendToFsa { get; set; }
}
