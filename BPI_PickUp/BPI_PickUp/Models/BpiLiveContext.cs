using System;
using System.Collections.Generic;
using BPI_PickUp.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BPI_PickUp.Models;

public partial class BpiLiveContext : DbContext
{
    public BpiLiveContext()
    {
    }

    public BpiLiveContext(DbContextOptions<BpiLiveContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Plant> Plants { get; set; }
    public virtual DbSet<Reason> Reasons { get; set; }
    public virtual DbSet<Part> Parts { get; set; }
    public virtual DbSet<PartWhse> PartWhses { get; set; }
    public virtual DbSet<PartBin> PartBins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:PickUpConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plant>(entity =>
        {
            entity.HasKey(e => new { e.Company, e.Plant1 });

            entity.ToTable("Plant", "Erp", tb => tb.HasTrigger("TR_Plant_ChangeCapture"));

            entity.HasIndex(e => new { e.Company, e.SysRevId }, "IX_Plant_CompanySysRevID");

            entity.HasIndex(e => new { e.Company, e.Name, e.Plant1 }, "IX_Plant_NamePlant");

            entity.HasIndex(e => e.SysRowId, "IX_Plant_SysIndex").IsUnique();

            entity.Property(e => e.Company)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.Plant1)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("Plant");
            entity.Property(e => e.Acwpercentage).HasColumnName("ACWPercentage");
            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Address3)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Agapartment)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("AGApartment");
            entity.Property(e => e.AgdefaultInvoicingPoint)
                .HasMaxLength(4)
                .HasDefaultValue("")
                .HasColumnName("AGDefaultInvoicingPoint");
            entity.Property(e => e.AgextraStreetNumber)
                .HasMaxLength(1)
                .HasDefaultValue("")
                .HasColumnName("AGExtraStreetNumber");
            entity.Property(e => e.Agfloor)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("AGFloor");
            entity.Property(e => e.AglocationCode)
                .HasMaxLength(5)
                .HasDefaultValue("")
                .HasColumnName("AGLocationCode");
            entity.Property(e => e.Agneighborhood)
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasColumnName("AGNeighborhood");
            entity.Property(e => e.AgprovinceCode)
                .HasMaxLength(4)
                .HasDefaultValue("")
                .HasColumnName("AGProvinceCode");
            entity.Property(e => e.Agstreet)
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasColumnName("AGStreet");
            entity.Property(e => e.AgstreetNumber)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("AGStreetNumber");
            entity.Property(e => e.AllowShipAction)
                .HasMaxLength(4)
                .HasDefaultValue("NONE");
            entity.Property(e => e.BranchId)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("BranchID");
            entity.Property(e => e.BusTypeDesc1)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.BusTypeDesc2)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.BusinessTypeCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.BwschedStartTime).HasColumnName("BWSchedStartTime");
            entity.Property(e => e.CalendarId)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("CalendarID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.CommentText).HasDefaultValue("");
            entity.Property(e => e.DefStationId)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("DefStationID");
            entity.Property(e => e.FaxNum)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.ForceFftime).HasColumnName("ForceFFTime");
            entity.Property(e => e.ForceSstime).HasColumnName("ForceSSTime");
            entity.Property(e => e.IncRcparams).HasColumnName("IncRCParams");
            entity.Property(e => e.Incstnumber)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("INCSTNumber");
            entity.Property(e => e.Ineccnumber)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("INECCNumber");
            entity.Property(e => e.InexCommissionRate)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("INExCommissionRate");
            entity.Property(e => e.InexciseDivision)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("INExciseDivision");
            entity.Property(e => e.InexciseRange)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("INExciseRange");
            entity.Property(e => e.Instregistration)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("INSTRegistration");
            entity.Property(e => e.IntaxRegistrationId)
                .HasMaxLength(15)
                .HasDefaultValue("")
                .HasColumnName("INTaxRegistrationID");
            entity.Property(e => e.Intinnumber)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("INTINNumber");
            entity.Property(e => e.Isregion)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("ISRegion");
            entity.Property(e => e.MaintPlant)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.ManagerName)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.MaxLateDaysPorel)
                .HasDefaultValue(2)
                .HasColumnName("MaxLateDaysPORel");
            entity.Property(e => e.MinimizeWip).HasColumnName("MinimizeWIP");
            entity.Property(e => e.Mxmunicipio)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("MXMunicipio");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.NextUnfirmJob).HasDefaultValue("");
            entity.Property(e => e.NextUnfirmTfline)
                .HasDefaultValue("")
                .HasColumnName("NextUnfirmTFLine");
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.PlantCostId)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("PlantCostID");
            entity.Property(e => e.RcutHorz).HasColumnName("RCutHorz");
            entity.Property(e => e.ReqTransferHeader).HasDefaultValue(true);
            entity.Property(e => e.SchedulingDirection)
                .HasMaxLength(15)
                .HasDefaultValue("End");
            entity.Property(e => e.SchedulingSendAhead)
                .HasMaxLength(8)
                .HasDefaultValue("S");
            entity.Property(e => e.SendToFsa).HasColumnName("SendToFSA");
            entity.Property(e => e.SiteCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.SiteDesc1)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.SiteDesc2)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.SiteType)
                .HasMaxLength(2)
                .HasDefaultValue("");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.SysRevId)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SysRevID");
            entity.Property(e => e.SysRowId)
                .HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid())+CONVERT([binary](6),getutcdate())))")
                .HasColumnName("SysRowID");
            entity.Property(e => e.TaxId)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("TaxID");
            entity.Property(e => e.TimeZoneAdjustForDst).HasColumnName("TimeZoneAdjustForDST");
            entity.Property(e => e.TimeZoneId)
                .HasMaxLength(35)
                .HasDefaultValue("")
                .HasColumnName("TimeZoneID");
            entity.Property(e => e.Us1099address1)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099Address1");
            entity.Property(e => e.Us1099address2)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099Address2");
            entity.Property(e => e.Us1099address3)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099Address3");
            entity.Property(e => e.Us1099city)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099City");
            entity.Property(e => e.Us1099contactPerson)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099ContactPerson");
            entity.Property(e => e.Us1099emailAddress)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099EMailAddress");
            entity.Property(e => e.Us1099faxNum)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("US1099FaxNum");
            entity.Property(e => e.Us1099name1)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099Name1");
            entity.Property(e => e.Us1099name2)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099Name2");
            entity.Property(e => e.Us1099nameControl)
                .HasMaxLength(4)
                .HasDefaultValue("")
                .HasColumnName("US1099NameControl");
            entity.Property(e => e.Us1099officeCode)
                .HasMaxLength(4)
                .HasDefaultValue("")
                .HasColumnName("US1099OfficeCode");
            entity.Property(e => e.Us1099payersTin)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099PayersTIN");
            entity.Property(e => e.Us1099phoneNum)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("US1099PhoneNum");
            entity.Property(e => e.Us1099state)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("US1099State");
            entity.Property(e => e.Us1099transControlCode)
                .HasMaxLength(5)
                .HasDefaultValue("")
                .HasColumnName("US1099TransControlCode");
            entity.Property(e => e.Us1099zip)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("US1099ZIP");
            entity.Property(e => e.UseLeadTimeDos).HasColumnName("UseLeadTimeDOS");
            entity.Property(e => e.Zip)
                .HasMaxLength(10)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => new { e.Company, e.ReasonType, e.ReasonCode });

            entity.ToTable("Reason", "Erp", tb => tb.HasTrigger("TR_Reason_ChangeCapture"));

            entity.HasIndex(e => e.SysRowId, "IX_Reason_SysIndex").IsUnique();

            entity.HasIndex(e => new { e.Company, e.ReasonType, e.Description }, "IX_Reason_TypeDescription").IsUnique();

            entity.Property(e => e.Company)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.ReasonType)
                .HasMaxLength(2)
                .HasDefaultValue("");
            entity.Property(e => e.ReasonCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.DmracceptInv)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptInv");
            entity.Property(e => e.DmracceptMtl)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptMtl");
            entity.Property(e => e.DmracceptOpr)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptOpr");
            entity.Property(e => e.DmracceptSub)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptSub");
            entity.Property(e => e.DmrrejInv)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejInv");
            entity.Property(e => e.DmrrejMtl)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejMtl");
            entity.Property(e => e.DmrrejOpr)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejOpr");
            entity.Property(e => e.DmrrejSub)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejSub");
            entity.Property(e => e.ExternalMeslastSync)
                .HasColumnType("datetime")
                .HasColumnName("ExternalMESLastSync");
            entity.Property(e => e.ExternalMessyncRequired).HasColumnName("ExternalMESSyncRequired");
            entity.Property(e => e.InspFailInv).HasDefaultValue(true);
            entity.Property(e => e.InspFailMtl).HasDefaultValue(true);
            entity.Property(e => e.InspFailOpr).HasDefaultValue(true);
            entity.Property(e => e.InspFailSub).HasDefaultValue(true);
            entity.Property(e => e.JdfworkType)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("JDFWorkType");
            entity.Property(e => e.NonConfInv).HasDefaultValue(true);
            entity.Property(e => e.NonConfMtl).HasDefaultValue(true);
            entity.Property(e => e.NonConfOpr).HasDefaultValue(true);
            entity.Property(e => e.NonConfOther).HasDefaultValue(true);
            entity.Property(e => e.NonConfSub).HasDefaultValue(true);
            entity.Property(e => e.Qacause)
                .HasDefaultValue(true)
                .HasColumnName("QACause");
            entity.Property(e => e.QacorrectiveAct)
                .HasDefaultValue(true)
                .HasColumnName("QACorrectiveAct");
            entity.Property(e => e.Scrap).HasDefaultValue(true);
            entity.Property(e => e.SysRevId)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SysRevID");
            entity.Property(e => e.SysRowId)
                .HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid())+CONVERT([binary](6),getutcdate())))")
                .HasColumnName("SysRowID");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => new { e.Company, e.PartNum });

            entity.ToTable("Part", "Erp", tb => tb.HasTrigger("TR_Part_ChangeCapture"));

            entity.HasIndex(e => new { e.Company, e.Snmask }, "IX_Part_CompSNMask");

            entity.HasIndex(e => new { e.Company, e.SysRevId }, "IX_Part_CompanySysRevID");

            entity.HasIndex(e => new { e.Company, e.InActive, e.PartNum }, "IX_Part_InactivePartNum");

            entity.HasIndex(e => new { e.Company, e.TrackLots, e.TypeCode, e.PartNum }, "IX_Part_LotTypePart");

            entity.HasIndex(e => new { e.Company, e.TrackLots, e.TypeCode, e.SearchWord, e.PartNum }, "IX_Part_LotTypeWord");

            entity.HasIndex(e => new { e.Company, e.Method, e.LowLevelCode }, "IX_Part_MethodLowLevel");

            entity.HasIndex(e => new { e.Company, e.Method, e.PartNum }, "IX_Part_MethodPart");

            entity.HasIndex(e => new { e.Company, e.Method, e.SearchWord, e.PartNum }, "IX_Part_MethodWord");

            entity.HasIndex(e => new { e.Company, e.ClassId }, "IX_Part_PartClass");

            entity.HasIndex(e => new { e.Company, e.ProdCode, e.PartNum }, "IX_Part_ProdCode");

            entity.HasIndex(e => new { e.Company, e.SearchWord, e.PartNum }, "IX_Part_SearchWord");

            entity.HasIndex(e => e.SysRowId, "IX_Part_SysIndex").IsUnique();

            entity.HasIndex(e => new { e.Company, e.TrackSerialNum, e.TypeCode, e.PartNum }, "IX_Part_TrackSerialNumPart");

            entity.HasIndex(e => new { e.Company, e.TrackSerialNum, e.TypeCode, e.SearchWord }, "IX_Part_TrackSerialNumWord");

            entity.HasIndex(e => new { e.Company, e.TypeCode, e.PartNum }, "IX_Part_TypePart");

            entity.HasIndex(e => new { e.Company, e.TypeCode, e.SearchWord, e.PartNum }, "IX_Part_TypeSearch");

            entity.HasIndex(e => new { e.Company, e.UomclassId, e.PartNum }, "IX_Part_UOMClass");

            entity.HasIndex(e => new { e.Company, e.Upccode1 }, "IX_Part_UPCCode1");

            entity.HasIndex(e => new { e.Company, e.Upccode2 }, "IX_Part_UPCCode2");

            entity.HasIndex(e => new { e.Company, e.Upccode3 }, "IX_Part_UPCCode3");

            entity.Property(e => e.Company)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.PartNum)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Aesexp)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("AESExp");
            entity.Property(e => e.AgproductMark).HasColumnName("AGProductMark");
            entity.Property(e => e.AguseGoodMark).HasColumnName("AGUseGoodMark");
            entity.Property(e => e.AnalysisCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.AttBatch)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttBeforeDt)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttCureDt)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttExpDt)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttFirmware)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttHeat)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttIsorigCountry)
                .HasMaxLength(3)
                .HasDefaultValue("N")
                .HasColumnName("AttISOrigCountry");
            entity.Property(e => e.AttMfgBatch)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttMfgDt)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttMfgLot)
                .HasMaxLength(3)
                .HasDefaultValue("N");
            entity.Property(e => e.AttrClassId)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("AttrClassID");
            entity.Property(e => e.BasePartNum)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.Bolclass)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("BOLClass");
            entity.Property(e => e.ChangedOn).HasColumnType("datetime");
            entity.Property(e => e.ClassId)
                .HasMaxLength(4)
                .HasDefaultValue("")
                .HasColumnName("ClassID");
            entity.Property(e => e.Cnbonded).HasColumnName("CNBonded");
            entity.Property(e => e.CncodeVersion)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("CNCodeVersion");
            entity.Property(e => e.CnhasPreferentialTreatment).HasColumnName("CNHasPreferentialTreatment");
            entity.Property(e => e.CnpreferentialTreatmentContent)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("CNPreferentialTreatmentContent");
            entity.Property(e => e.CnproductName)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("CNProductName");
            entity.Property(e => e.Cnspecification)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("CNSpecification");
            entity.Property(e => e.CntaxCategoryCode)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("CNTaxCategoryCode");
            entity.Property(e => e.Cnweight)
                .HasColumnType("decimal(17, 5)")
                .HasColumnName("CNWeight");
            entity.Property(e => e.CnweightUom)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("CNWeightUOM");
            entity.Property(e => e.CnzeroTaxRateMark)
                .HasMaxLength(1)
                .HasDefaultValue("")
                .HasColumnName("CNZeroTaxRateMark");
            entity.Property(e => e.CommentText).HasDefaultValue("");
            entity.Property(e => e.CommercialBrand)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialCategory)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialColor)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialSize1)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialSize2)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialStyle)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialSubBrand)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommercialSubCategory)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CommodityCode)
                .HasMaxLength(15)
                .HasDefaultValue("");
            entity.Property(e => e.CommoditySchemeId)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("CommoditySchemeID");
            entity.Property(e => e.CommoditySchemeVersion)
                .HasMaxLength(40)
                .HasDefaultValue("");
            entity.Property(e => e.Condition)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.CostMethod)
                .HasMaxLength(2)
                .HasDefaultValue("A");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(75)
                .HasDefaultValue("");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Csfcj5).HasColumnName("CSFCJ5");
            entity.Property(e => e.Csflmw).HasColumnName("CSFLMW");
            entity.Property(e => e.Dedenomination)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("DEDenomination");
            entity.Property(e => e.DefaultAttributeSetId).HasColumnName("DefaultAttributeSetID");
            entity.Property(e => e.DefaultDim)
                .HasMaxLength(6)
                .HasDefaultValue("");
            entity.Property(e => e.DeinternationalSecuritiesId)
                .HasMaxLength(12)
                .HasDefaultValue("")
                .HasColumnName("DEInternationalSecuritiesID");
            entity.Property(e => e.DeisInvestment).HasColumnName("DEIsInvestment");
            entity.Property(e => e.DeisSecurityFinancialDerivative).HasColumnName("DEIsSecurityFinancialDerivative");
            entity.Property(e => e.DeisServices).HasColumnName("DEIsServices");
            entity.Property(e => e.DepayStatCode)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("DEPayStatCode");
            entity.Property(e => e.Diameter).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.DiameterInside).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.DiameterOutside).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.DiameterUm)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("DiameterUM");
            entity.Property(e => e.DiffPrc2PrchUom).HasColumnName("DiffPrc2PrchUOM");
            entity.Property(e => e.DualUomclassId)
                .HasMaxLength(12)
                .HasDefaultValue("")
                .HasColumnName("DualUOMClassID");
            entity.Property(e => e.Durometer)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.Eccnnumber)
                .HasMaxLength(25)
                .HasDefaultValue("")
                .HasColumnName("ECCNNumber");
            entity.Property(e => e.Edicode)
                .HasMaxLength(15)
                .HasDefaultValue("")
                .HasColumnName("EDICode");
            entity.Property(e => e.EngineeringAlert)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.EstimateId)
                .HasMaxLength(256)
                .HasDefaultValue("")
                .HasColumnName("EstimateID");
            entity.Property(e => e.EstimateOrPlan)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.ExpLicNumber)
                .HasMaxLength(25)
                .HasDefaultValue("");
            entity.Property(e => e.ExpLicType)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.ExternalCrmlastSync)
                .HasColumnType("datetime")
                .HasColumnName("ExternalCRMLastSync");
            entity.Property(e => e.ExternalCrmpartId)
                .HasMaxLength(200)
                .HasDefaultValue("")
                .HasColumnName("ExternalCRMPartID");
            entity.Property(e => e.ExternalCrmsyncRequired).HasColumnName("ExternalCRMSyncRequired");
            entity.Property(e => e.ExternalId)
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasColumnName("ExternalID");
            entity.Property(e => e.ExternalMeslastSync)
                .HasColumnType("datetime")
                .HasColumnName("ExternalMESLastSync");
            entity.Property(e => e.ExternalMessyncRequired).HasColumnName("ExternalMESSyncRequired");
            entity.Property(e => e.ExternalSchemeId)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("ExternalSchemeID");
            entity.Property(e => e.FairMarketValue).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.Fsaequipment).HasColumnName("FSAEquipment");
            entity.Property(e => e.Fsaitem).HasColumnName("FSAItem");
            entity.Property(e => e.FsassetClassCode)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("FSAssetClassCode");
            entity.Property(e => e.FspricePerCode)
                .HasMaxLength(2)
                .HasDefaultValue("E")
                .HasColumnName("FSPricePerCode");
            entity.Property(e => e.FssalesUnitPrice)
                .HasColumnType("decimal(17, 5)")
                .HasColumnName("FSSalesUnitPrice");
            entity.Property(e => e.Gravity).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.GrossWeight).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.GrossWeightUom)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("GrossWeightUOM");
            entity.Property(e => e.HazClass)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.HazGvrnmtId)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("HazGvrnmtID");
            entity.Property(e => e.HazPackInstr).HasDefaultValue("");
            entity.Property(e => e.HazSub)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.HazTechName)
                .HasMaxLength(40)
                .HasDefaultValue("");
            entity.Property(e => e.Hts)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("HTS");
            entity.Property(e => e.ImageFileName)
                .HasMaxLength(256)
                .HasDefaultValue("");
            entity.Property(e => e.ImageId)
                .HasMaxLength(256)
                .HasDefaultValue("")
                .HasColumnName("ImageID");
            entity.Property(e => e.InchapterId)
                .HasMaxLength(12)
                .HasDefaultValue("")
                .HasColumnName("INChapterID");
            entity.Property(e => e.InternalPricePerCode)
                .HasMaxLength(2)
                .HasDefaultValue("E");
            entity.Property(e => e.InternalUnitPrice).HasColumnType("decimal(17, 5)");
            entity.Property(e => e.IsorigCountry)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("ISOrigCountry");
            entity.Property(e => e.Isregion)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("ISRegion");
            entity.Property(e => e.IssuppUnitsFactor)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 8)")
                .HasColumnName("ISSuppUnitsFactor");
            entity.Property(e => e.Ium)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("IUM");
            entity.Property(e => e.LcnrvestimatedUnitPrice)
                .HasColumnType("decimal(17, 3)")
                .HasColumnName("LCNRVEstimatedUnitPrice");
            entity.Property(e => e.Lcnrvreporting).HasColumnName("LCNRVReporting");
            entity.Property(e => e.LocationFormatId)
                .HasMaxLength(12)
                .HasDefaultValue("")
                .HasColumnName("LocationFormatID");
            entity.Property(e => e.LocationIdnumReq).HasColumnName("LocationIDNumReq");
            entity.Property(e => e.LotAppendDate)
                .HasMaxLength(10)
                .HasDefaultValue("");
            entity.Property(e => e.LotNxtNum).HasDefaultValue(1);
            entity.Property(e => e.LotPrefix)
                .HasMaxLength(20)
                .HasDefaultValue("");
            entity.Property(e => e.LotSeqId)
                .HasMaxLength(12)
                .HasDefaultValue("")
                .HasColumnName("LotSeqID");
            entity.Property(e => e.Mdpv).HasColumnName("MDPV");
            entity.Property(e => e.MfgComment).HasDefaultValue("");
            entity.Property(e => e.MobilePart).HasDefaultValue(true);
            entity.Property(e => e.MtlAnalysisCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.MtlBurRate).HasColumnType("decimal(10, 5)");
            entity.Property(e => e.MxcustomsDuty)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("MXCustomsDuty");
            entity.Property(e => e.MxcustomsUmfrom)
                .HasMaxLength(1)
                .HasDefaultValue("")
                .HasColumnName("MXCustomsUMFrom");
            entity.Property(e => e.MxprodServCode)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("MXProdServCode");
            entity.Property(e => e.NaftaorigCountry)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("NAFTAOrigCountry");
            entity.Property(e => e.Naftapref)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("NAFTAPref");
            entity.Property(e => e.Naftaprod)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("NAFTAProd");
            entity.Property(e => e.NetVolume).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.NetVolumeUom)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("NetVolumeUOM");
            entity.Property(e => e.NetWeight).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.NetWeightUom)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("NetWeightUOM");
            entity.Property(e => e.OnHoldReasonCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.OwnershipStatus)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.PartDescription).HasDefaultValue("");
            entity.Property(e => e.PartHeight).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.PartLength).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.PartLengthWidthHeightUm)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("PartLengthWidthHeightUM");
            entity.Property(e => e.PartSpecificPackingUom).HasColumnName("PartSpecificPackingUOM");
            entity.Property(e => e.PartWidth).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.PdmobjId)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("PDMObjID");
            entity.Property(e => e.PedetrGoodServiceCode)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("PEDetrGoodServiceCode");
            entity.Property(e => e.PeproductServiceCode)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("PEProductServiceCode");
            entity.Property(e => e.Pesunattype)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("PESUNATType");
            entity.Property(e => e.PesunattypeCode)
                .HasMaxLength(2)
                .HasDefaultValue("")
                .HasColumnName("PESUNATTypeCode");
            entity.Property(e => e.Pesunatuom)
                .HasMaxLength(100)
                .HasDefaultValue("")
                .HasColumnName("PESUNATUOM");
            entity.Property(e => e.Pesunatuomcode)
                .HasMaxLength(3)
                .HasDefaultValue("")
                .HasColumnName("PESUNATUOMCode");
            entity.Property(e => e.PhantomBom).HasColumnName("PhantomBOM");
            entity.Property(e => e.PhotoFile)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.PricePerCode)
                .HasMaxLength(2)
                .HasDefaultValue("E");
            entity.Property(e => e.PricingFactor).HasColumnType("decimal(14, 5)");
            entity.Property(e => e.PricingUom)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("PricingUOM");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.Pum)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("PUM");
            entity.Property(e => e.PurComment).HasDefaultValue("");
            entity.Property(e => e.PurchasingFactor)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(14, 4)");
            entity.Property(e => e.PurchasingFactorDirection)
                .HasMaxLength(1)
                .HasDefaultValue("D");
            entity.Property(e => e.QtyBearing).HasDefaultValue(true);
            entity.Property(e => e.RcoverThreshold)
                .HasColumnType("decimal(17, 3)")
                .HasColumnName("RCOverThreshold");
            entity.Property(e => e.RcunderThreshold)
                .HasColumnType("decimal(17, 3)")
                .HasColumnName("RCUnderThreshold");
            entity.Property(e => e.RefCategory)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.ReturnableContainer)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.RevChargeMethod)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.SaftprodCategory)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("SAFTProdCategory");
            entity.Property(e => e.SalesUm)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("SalesUM");
            entity.Property(e => e.SchedBcode)
                .HasMaxLength(12)
                .HasDefaultValue("");
            entity.Property(e => e.SearchWord)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.SellingFactor)
                .HasDefaultValue(1m)
                .HasColumnType("decimal(18, 8)");
            entity.Property(e => e.SellingFactorDirection)
                .HasMaxLength(1)
                .HasDefaultValue("D");
            entity.Property(e => e.SendToFsa).HasColumnName("SendToFSA");
            entity.Property(e => e.SnbaseDataType)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("SNBaseDataType");
            entity.Property(e => e.Snformat)
                .HasMaxLength(80)
                .HasDefaultValue("")
                .HasColumnName("SNFormat");
            entity.Property(e => e.SnlastUsedSeq)
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasColumnName("SNLastUsedSeq");
            entity.Property(e => e.Snmask)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("SNMask");
            entity.Property(e => e.SnmaskExample)
                .HasMaxLength(80)
                .HasDefaultValue("")
                .HasColumnName("SNMaskExample");
            entity.Property(e => e.SnmaskPrefix)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("SNMaskPrefix");
            entity.Property(e => e.SnmaskSuffix)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("SNMaskSuffix");
            entity.Property(e => e.Snprefix)
                .HasMaxLength(30)
                .HasDefaultValue("")
                .HasColumnName("SNPrefix");
            entity.Property(e => e.Specification)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.SubPart)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.SyncToExternalCrm).HasColumnName("SyncToExternalCRM");
            entity.Property(e => e.SysRevId)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SysRevID");
            entity.Property(e => e.SysRowId)
                .HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid())+CONVERT([binary](6),getutcdate())))")
                .HasColumnName("SysRowID");
            entity.Property(e => e.TaxCatId)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("TaxCatID");
            entity.Property(e => e.Thickness).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.ThicknessMax).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.ThicknessUm)
                .HasMaxLength(6)
                .HasDefaultValue("")
                .HasColumnName("ThicknessUM");
            entity.Property(e => e.TypeCode)
                .HasMaxLength(2)
                .HasDefaultValue("P");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(17, 5)");
            entity.Property(e => e.UomclassId)
                .HasMaxLength(12)
                .HasDefaultValue("")
                .HasColumnName("UOMClassID");
            entity.Property(e => e.Upccode1)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("UPCCode1");
            entity.Property(e => e.Upccode2)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("UPCCode2");
            entity.Property(e => e.Upccode3)
                .HasMaxLength(20)
                .HasDefaultValue("")
                .HasColumnName("UPCCode3");
            entity.Property(e => e.UseHtsdesc).HasColumnName("UseHTSDesc");
            entity.Property(e => e.UsePartRev).HasDefaultValue(true);
            entity.Property(e => e.UserChar1)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.UserChar2)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.UserChar3)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.UserChar4)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.UserDate1).HasColumnType("datetime");
            entity.Property(e => e.UserDate2).HasColumnType("datetime");
            entity.Property(e => e.UserDate3).HasColumnType("datetime");
            entity.Property(e => e.UserDate4).HasColumnType("datetime");
            entity.Property(e => e.UserDecimal1).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UserDecimal2).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.UserDecimal3).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.UserDecimal4).HasColumnType("decimal(15, 5)");
            entity.Property(e => e.WarrantyCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
        });

        modelBuilder.Entity<PartWhse>(entity =>
        {
            entity.HasKey(e => new { e.Company, e.PartNum, e.WarehouseCode });

            entity.ToTable("PartWhse", "Erp", tb => tb.HasTrigger("TR_PartWhse_ChangeCapture"));

            entity.HasIndex(e => new { e.Company, e.PartNum, e.CountedDate, e.WarehouseCode }, "IX_PartWhse_CountedDateWhse");

            entity.HasIndex(e => new { e.Company, e.Kbcode }, "IX_PartWhse_KBCode");

            entity.HasIndex(e => new { e.Company, e.Kbponum, e.Kbpoline }, "IX_PartWhse_KBPONum");

            entity.HasIndex(e => new { e.Company, e.Kbplant }, "IX_PartWhse_KBPlant");

            entity.HasIndex(e => new { e.Company, e.KbwarehouseCode, e.KbbinNum }, "IX_PartWhse_KBWhsebin");

            entity.HasIndex(e => new { e.Company, e.WarehouseCode, e.SystemAbc, e.LastCcdate }, "IX_PartWhse_LastCCDate");

            entity.HasIndex(e => new { e.Company, e.PartNum, e.WarehouseCode, e.SystemAbc }, "IX_PartWhse_PartWhsABC");

            entity.HasIndex(e => e.SysRowId, "IX_PartWhse_SysIndex").IsUnique();

            entity.Property(e => e.Company)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.PartNum)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.AllocatedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.BuyToOrderQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.DemandQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobAllocatedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobDemandQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobPickedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobPickingQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobReservedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.KbbinNum)
                .HasMaxLength(10)
                .HasDefaultValue("")
                .HasColumnName("KBBinNum");
            entity.Property(e => e.Kbcode)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("KBCode");
            entity.Property(e => e.Kbplant)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("KBPlant");
            entity.Property(e => e.Kbpoline).HasColumnName("KBPOLine");
            entity.Property(e => e.Kbponum).HasColumnName("KBPONUM");
            entity.Property(e => e.Kbqty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("KBQty");
            entity.Property(e => e.KbwarehouseCode)
                .HasMaxLength(8)
                .HasDefaultValue("")
                .HasColumnName("KBWarehouseCode");
            entity.Property(e => e.LastCcdate).HasColumnName("LastCCDate");
            entity.Property(e => e.ManualAbc).HasColumnName("ManualABC");
            entity.Property(e => e.MaximumQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.MinAbc)
                .HasMaxLength(1)
                .HasDefaultValue("");
            entity.Property(e => e.MinimumQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.NonNettableQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.OnHandQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.PcntTolerance).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.PickedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.PickingQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.QtyAdjTolerance).HasColumnType("decimal(18, 8)");
            entity.Property(e => e.QtyTolerance).HasColumnType("decimal(11, 0)");
            entity.Property(e => e.ReservedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SafetyQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesAllocatedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesDemandQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesPickedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesPickingQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesReservedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SysRevId)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SysRevID");
            entity.Property(e => e.SysRowId)
                .HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid())+CONVERT([binary](6),getutcdate())))")
                .HasColumnName("SysRowID");
            entity.Property(e => e.SystemAbc)
                .HasMaxLength(1)
                .HasDefaultValue("");
            entity.Property(e => e.TfordAllocatedQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdAllocatedQty");
            entity.Property(e => e.TfordDemandQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdDemandQty");
            entity.Property(e => e.TfordPickedQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdPickedQty");
            entity.Property(e => e.TfordPickingQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdPickingQty");
            entity.Property(e => e.TfordReservedQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdReservedQty");
            entity.Property(e => e.UnfirmJobDemandQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.ValueTolerance).HasColumnType("decimal(11, 0)");
        });

        modelBuilder.Entity<PartBin>(entity =>
        {
            entity.HasKey(e => new { e.Company, e.PartNum, e.WarehouseCode, e.BinNum, e.DimCode, e.LotNum, e.Pcid, e.AttributeSetId });

            entity.ToTable("PartBin", "Erp", tb => tb.HasTrigger("TR_PartBin_ChangeCapture"));

            entity.HasIndex(e => new { e.Company, e.SysRevId }, "IX_PartBin_CompanySysRevID");

            entity.HasIndex(e => new { e.Company, e.PartNum, e.WarehouseCode, e.BinNum }, "IX_PartBin_IDXPartBinWhse");

            entity.HasIndex(e => new { e.Company, e.PartNum, e.WarehouseCode, e.DimCode, e.LotNum, e.BinNum, e.Pcid, e.AttributeSetId }, "IX_PartBin_PartWhsDimLotBin");

            entity.HasIndex(e => new { e.Company, e.PartNum, e.WarehouseCode, e.LotNum, e.DimCode, e.BinNum, e.Pcid, e.AttributeSetId }, "IX_PartBin_PartWhsLotDimBin");

            entity.HasIndex(e => e.SysRowId, "IX_PartBin_SysIndex").IsUnique();

            entity.Property(e => e.Company)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.PartNum)
                .HasMaxLength(50)
                .HasDefaultValue("");
            entity.Property(e => e.WarehouseCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.BinNum)
                .HasMaxLength(10)
                .HasDefaultValue("");
            entity.Property(e => e.DimCode)
                .HasMaxLength(6)
                .HasDefaultValue("");
            entity.Property(e => e.LotNum)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.Pcid)
                .HasMaxLength(40)
                .HasDefaultValue("")
                .HasColumnName("PCID");
            entity.Property(e => e.AttributeSetId).HasColumnName("AttributeSetID");
            entity.Property(e => e.AllocatedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobAllocatedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobPickedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.JobPickingQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.OnhandQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.PackedQty).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.QtyPerPiece).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.RevisionNum)
                .HasMaxLength(12)
                .HasDefaultValue("");
            entity.Property(e => e.SalesAllocatedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesPickedQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SalesPickingQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SendToFsa).HasColumnName("SendToFSA");
            entity.Property(e => e.ShippingQty).HasColumnType("decimal(22, 8)");
            entity.Property(e => e.SysRevId)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SysRevID");
            entity.Property(e => e.SysRowId)
                .HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid())+CONVERT([binary](6),getutcdate())))")
                .HasColumnName("SysRowID");
            entity.Property(e => e.TfordAllocatedQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdAllocatedQty");
            entity.Property(e => e.TfordPickedQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdPickedQty");
            entity.Property(e => e.TfordPickingQty)
                .HasColumnType("decimal(22, 8)")
                .HasColumnName("TFOrdPickingQty");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
