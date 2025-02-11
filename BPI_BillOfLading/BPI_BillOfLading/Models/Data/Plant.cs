using System;
using System.Collections.Generic;

namespace BPI_BillOfLading.Models.Data;

public partial class Plant
{
    public string Company { get; set; } = null!;

    public string Plant1 { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public string Address2 { get; set; } = null!;

    public string Address3 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public int CountryNum { get; set; }

    public string PhoneNum { get; set; } = null!;

    public string FaxNum { get; set; } = null!;

    public string CommentText { get; set; } = null!;

    public int PlanningExceptionDays { get; set; }

    public string Isregion { get; set; } = null!;

    public string PlantCostId { get; set; } = null!;

    public int PrepTime { get; set; }

    public int KitTime { get; set; }

    public bool ReqTransferHeader { get; set; }

    public string CalendarId { get; set; } = null!;

    public string AllowShipAction { get; set; } = null!;

    public int AutoConfirmWindow { get; set; }

    public bool SingleLineOrder { get; set; }

    public int MaxOpStartDelay { get; set; }

    public int MaxOpLength { get; set; }

    public string DefStationId { get; set; } = null!;

    public int FiniteHorz { get; set; }

    public string NextUnfirmJob { get; set; } = null!;

    public string NextUnfirmTfline { get; set; } = null!;

    public bool AddlHdlgFlag { get; set; }

    public int RcutHorz { get; set; }

    public bool IncLeadTime { get; set; }

    public bool IncTransLeadTime { get; set; }

    public bool IncReceiveTime { get; set; }

    public bool IncKitTime { get; set; }

    public bool IncRcparams { get; set; }

    public int OverloadHorz { get; set; }

    public string SchedulingSendAhead { get; set; } = null!;

    public int UnfirmSeriesHorizon { get; set; }

    public int AutoFirmHorizon { get; set; }

    public string ManagerName { get; set; } = null!;

    public string BranchId { get; set; } = null!;

    public string MaintPlant { get; set; } = null!;

    public string SiteCode { get; set; } = null!;

    public string SiteDesc1 { get; set; } = null!;

    public string SiteDesc2 { get; set; } = null!;

    public string SiteType { get; set; } = null!;

    public string BusinessTypeCode { get; set; } = null!;

    public string BusTypeDesc1 { get; set; } = null!;

    public string BusTypeDesc2 { get; set; } = null!;

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public string AgdefaultInvoicingPoint { get; set; } = null!;

    public bool ForceSstime { get; set; }

    public bool ForceFftime { get; set; }

    public bool UseLeadTimeDos { get; set; }

    public bool AllowMinQty { get; set; }

    public bool IgnoreMtlConstraints { get; set; }

    public string AgprovinceCode { get; set; } = null!;

    public string AglocationCode { get; set; } = null!;

    public string Agneighborhood { get; set; } = null!;

    public string Agstreet { get; set; } = null!;

    public string AgstreetNumber { get; set; } = null!;

    public string AgextraStreetNumber { get; set; } = null!;

    public string Agfloor { get; set; } = null!;

    public string Agapartment { get; set; } = null!;

    public string Mxmunicipio { get; set; } = null!;

    public int MaxLateDaysPorel { get; set; }

    public string Ineccnumber { get; set; } = null!;

    public string InexciseRange { get; set; } = null!;

    public string InexciseDivision { get; set; } = null!;

    public string InexCommissionRate { get; set; } = null!;

    public string Intinnumber { get; set; } = null!;

    public string Incstnumber { get; set; } = null!;

    public string Instregistration { get; set; } = null!;

    public bool UseSchedulingMultiJob { get; set; }

    public bool AutoLoadChildJobs { get; set; }

    public bool AutoLoadParentJobs { get; set; }

    public bool MinimizeWip { get; set; }

    public string TimeZoneId { get; set; } = null!;

    public bool TimeZoneAdjustForDst { get; set; }

    public bool SyncReqBy { get; set; }

    public int Acwpercentage { get; set; }

    public int BwschedStartTime { get; set; }

    public string IntaxRegistrationId { get; set; } = null!;

    public bool SendToFsa { get; set; }

    public string SchedulingDirection { get; set; } = null!;

    public string Us1099payersTin { get; set; } = null!;

    public string Us1099nameControl { get; set; } = null!;

    public string Us1099officeCode { get; set; } = null!;

    public string Us1099contactPerson { get; set; } = null!;

    public string Us1099emailAddress { get; set; } = null!;

    public string Us1099faxNum { get; set; } = null!;

    public string Us1099phoneNum { get; set; } = null!;

    public string Us1099transControlCode { get; set; } = null!;

    public string Us1099name1 { get; set; } = null!;

    public string Us1099name2 { get; set; } = null!;

    public string Us1099address1 { get; set; } = null!;

    public string Us1099address2 { get; set; } = null!;

    public string Us1099address3 { get; set; } = null!;

    public string Us1099city { get; set; } = null!;

    public string Us1099state { get; set; } = null!;

    public string Us1099zip { get; set; } = null!;

    public string TaxId { get; set; } = null!;
}
