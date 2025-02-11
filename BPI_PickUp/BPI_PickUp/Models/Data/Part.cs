using System;
using System.Collections.Generic;

namespace BPI_PickUp.Models.Data;

public partial class Part
{
    public string Company { get; set; } = null!;

    public string PartNum { get; set; } = null!;

    public string SearchWord { get; set; } = null!;

    public string PartDescription { get; set; } = null!;

    public string ClassId { get; set; } = null!;

    public string Ium { get; set; } = null!;

    public string Pum { get; set; } = null!;

    public string TypeCode { get; set; } = null!;

    public bool NonStock { get; set; }

    public decimal PurchasingFactor { get; set; }

    public decimal UnitPrice { get; set; }

    public string PricePerCode { get; set; } = null!;

    public decimal InternalUnitPrice { get; set; }

    public string InternalPricePerCode { get; set; } = null!;

    public string ProdCode { get; set; } = null!;

    public string MfgComment { get; set; } = null!;

    public string PurComment { get; set; } = null!;

    public string CostMethod { get; set; } = null!;

    public string UserChar1 { get; set; } = null!;

    public string UserChar2 { get; set; } = null!;

    public string UserChar3 { get; set; } = null!;

    public string UserChar4 { get; set; } = null!;

    public DateTime? UserDate1 { get; set; }

    public DateTime? UserDate2 { get; set; }

    public DateTime? UserDate3 { get; set; }

    public DateTime? UserDate4 { get; set; }

    public decimal UserDecimal1 { get; set; }

    public decimal UserDecimal2 { get; set; }

    public decimal UserDecimal3 { get; set; }

    public decimal UserDecimal4 { get; set; }

    public int UserInteger1 { get; set; }

    public int UserInteger2 { get; set; }

    public string TaxCatId { get; set; } = null!;

    public bool InActive { get; set; }

    public int LowLevelCode { get; set; }

    public bool Method { get; set; }

    public bool TrackLots { get; set; }

    public bool TrackDimension { get; set; }

    public string DefaultDim { get; set; } = null!;

    public bool TrackSerialNum { get; set; }

    public string CommodityCode { get; set; } = null!;

    public string WarrantyCode { get; set; } = null!;

    public bool PhantomBom { get; set; }

    public string SalesUm { get; set; } = null!;

    public decimal SellingFactor { get; set; }

    public decimal MtlBurRate { get; set; }

    public decimal NetWeight { get; set; }

    public bool UsePartRev { get; set; }

    public int PartsPerContainer { get; set; }

    public decimal PartLength { get; set; }

    public decimal PartWidth { get; set; }

    public decimal PartHeight { get; set; }

    public int LotShelfLife { get; set; }

    public bool WebPart { get; set; }

    public bool RunOut { get; set; }

    public string SubPart { get; set; } = null!;

    public decimal Diameter { get; set; }

    public decimal Gravity { get; set; }

    public bool OnHold { get; set; }

    public DateOnly? OnHoldDate { get; set; }

    public string OnHoldReasonCode { get; set; } = null!;

    public string AnalysisCode { get; set; } = null!;

    public bool GlobalPart { get; set; }

    public string MtlAnalysisCode { get; set; } = null!;

    public bool GlobalLock { get; set; }

    public decimal IssuppUnitsFactor { get; set; }

    public string PdmobjId { get; set; } = null!;

    public string ImageFileName { get; set; } = null!;

    public string IsorigCountry { get; set; } = null!;

    public string Snprefix { get; set; } = null!;

    public string Snformat { get; set; } = null!;

    public string SnbaseDataType { get; set; } = null!;

    public bool Constrained { get; set; }

    public string Upccode1 { get; set; } = null!;

    public string Upccode2 { get; set; } = null!;

    public string Upccode3 { get; set; } = null!;

    public string Edicode { get; set; } = null!;

    public bool WebInStock { get; set; }

    public bool ConsolidatedPurchasing { get; set; }

    public string PurchasingFactorDirection { get; set; } = null!;

    public string SellingFactorDirection { get; set; } = null!;

    public bool RecDocReq { get; set; }

    public int Mdpv { get; set; }

    public bool ShipDocReq { get; set; }

    public string ReturnableContainer { get; set; } = null!;

    public decimal NetVolume { get; set; }

    public bool QtyBearing { get; set; }

    public string NaftaorigCountry { get; set; } = null!;

    public string Naftaprod { get; set; } = null!;

    public string Naftapref { get; set; } = null!;

    public string ExpLicType { get; set; } = null!;

    public string ExpLicNumber { get; set; } = null!;

    public string Eccnnumber { get; set; } = null!;

    public string Aesexp { get; set; } = null!;

    public string Hts { get; set; } = null!;

    public bool UseHtsdesc { get; set; }

    public string SchedBcode { get; set; } = null!;

    public bool HazItem { get; set; }

    public string HazTechName { get; set; } = null!;

    public string HazClass { get; set; } = null!;

    public string HazSub { get; set; } = null!;

    public string HazGvrnmtId { get; set; } = null!;

    public string HazPackInstr { get; set; } = null!;

    public string RevChargeMethod { get; set; } = null!;

    public decimal RcunderThreshold { get; set; }

    public decimal RcoverThreshold { get; set; }

    public string OwnershipStatus { get; set; } = null!;

    public string UomclassId { get; set; } = null!;

    public string Snmask { get; set; } = null!;

    public string SnmaskExample { get; set; } = null!;

    public string SnmaskSuffix { get; set; } = null!;

    public string SnmaskPrefix { get; set; } = null!;

    public string SnlastUsedSeq { get; set; } = null!;

    public bool UseMaskSeq { get; set; }

    public string NetWeightUom { get; set; } = null!;

    public string NetVolumeUom { get; set; } = null!;

    public bool LotBatch { get; set; }

    public bool LotMfgBatch { get; set; }

    public bool LotMfgLot { get; set; }

    public bool LotHeat { get; set; }

    public bool LotFirmware { get; set; }

    public bool LotBeforeDt { get; set; }

    public bool LotMfgDt { get; set; }

    public bool LotCureDt { get; set; }

    public bool LotExpDt { get; set; }

    public string LotPrefix { get; set; } = null!;

    public bool LotUseGlobalSeq { get; set; }

    public string LotSeqId { get; set; } = null!;

    public int LotNxtNum { get; set; }

    public int LotDigits { get; set; }

    public bool LotLeadingZeros { get; set; }

    public string LotAppendDate { get; set; } = null!;

    public bool BuyToOrder { get; set; }

    public bool DropShip { get; set; }

    public bool IsConfigured { get; set; }

    public bool ExtConfig { get; set; }

    public string RefCategory { get; set; } = null!;

    public bool Csfcj5 { get; set; }

    public bool Csflmw { get; set; }

    public decimal GrossWeight { get; set; }

    public string GrossWeightUom { get; set; } = null!;

    public string BasePartNum { get; set; } = null!;

    public string FsassetClassCode { get; set; } = null!;

    public decimal FssalesUnitPrice { get; set; }

    public string FspricePerCode { get; set; } = null!;

    public bool RcvInspectionReq { get; set; }

    public string EstimateId { get; set; } = null!;

    public string EstimateOrPlan { get; set; } = null!;

    public bool DiffPrc2PrchUom { get; set; }

    public bool DupOnJobCrt { get; set; }

    public decimal PricingFactor { get; set; }

    public string PricingUom { get; set; } = null!;

    public bool MobilePart { get; set; }

    public byte[] SysRevId { get; set; } = null!;

    public Guid SysRowId { get; set; }

    public bool AguseGoodMark { get; set; }

    public bool AgproductMark { get; set; }

    public string Isregion { get; set; } = null!;

    public string InchapterId { get; set; } = null!;

    public string Pesunattype { get; set; } = null!;

    public string Pesunatuom { get; set; } = null!;

    public bool DeisServices { get; set; }

    public bool DeisSecurityFinancialDerivative { get; set; }

    public string DeinternationalSecuritiesId { get; set; } = null!;

    public bool LinkToContract { get; set; }

    public bool DeisInvestment { get; set; }

    public string DepayStatCode { get; set; } = null!;

    public string Dedenomination { get; set; } = null!;

    public string PartLengthWidthHeightUm { get; set; } = null!;

    public string DiameterUm { get; set; } = null!;

    public decimal DiameterInside { get; set; }

    public decimal DiameterOutside { get; set; }

    public string ThicknessUm { get; set; } = null!;

    public decimal Thickness { get; set; }

    public decimal ThicknessMax { get; set; }

    public string Durometer { get; set; } = null!;

    public string Specification { get; set; } = null!;

    public string EngineeringAlert { get; set; } = null!;

    public string Condition { get; set; } = null!;

    public bool IsCompliant { get; set; }

    public bool IsRestricted { get; set; }

    public bool IsSafetyItem { get; set; }

    public string CommercialBrand { get; set; } = null!;

    public string CommercialSubBrand { get; set; } = null!;

    public string CommercialCategory { get; set; } = null!;

    public string CommercialSubCategory { get; set; } = null!;

    public string CommercialStyle { get; set; } = null!;

    public string CommercialSize1 { get; set; } = null!;

    public string CommercialSize2 { get; set; } = null!;

    public string CommercialColor { get; set; } = null!;

    public bool IsGiftCard { get; set; }

    public string PhotoFile { get; set; } = null!;

    public bool PartPhotoExists { get; set; }

    public string CommentText { get; set; } = null!;

    public bool PartSpecificPackingUom { get; set; }

    public string ImageId { get; set; } = null!;

    public string Cnspecification { get; set; } = null!;

    public bool SyncToExternalCrm { get; set; }

    public string ExternalCrmpartId { get; set; } = null!;

    public DateTime? ExternalCrmlastSync { get; set; }

    public bool ExternalCrmsyncRequired { get; set; }

    public string PesunattypeCode { get; set; } = null!;

    public string Pesunatuomcode { get; set; } = null!;

    public string CncodeVersion { get; set; } = null!;

    public string CntaxCategoryCode { get; set; } = null!;

    public bool CnhasPreferentialTreatment { get; set; }

    public string CnpreferentialTreatmentContent { get; set; } = null!;

    public string CnzeroTaxRateMark { get; set; } = null!;

    public int SubLevelCode { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? CreatedOn { get; set; }

    public string AttBatch { get; set; } = null!;

    public string AttMfgBatch { get; set; } = null!;

    public string AttMfgLot { get; set; } = null!;

    public string AttHeat { get; set; } = null!;

    public string AttFirmware { get; set; } = null!;

    public string AttBeforeDt { get; set; } = null!;

    public string AttMfgDt { get; set; } = null!;

    public string AttCureDt { get; set; } = null!;

    public string AttExpDt { get; set; } = null!;

    public bool DeferManualEntry { get; set; }

    public bool DeferPurchaseReceipt { get; set; }

    public bool DeferJobReceipt { get; set; }

    public bool DeferInspection { get; set; }

    public bool DeferQtyAdjustment { get; set; }

    public bool DeferInventoryMove { get; set; }

    public bool DeferShipments { get; set; }

    public bool DeferInventoryCounts { get; set; }

    public bool DeferAssetDisposal { get; set; }

    public bool DeferReturnMaterials { get; set; }

    public string MxprodServCode { get; set; } = null!;

    public DateTime? ChangedOn { get; set; }

    public string MxcustomsDuty { get; set; } = null!;

    public bool SendToFsa { get; set; }

    public bool ExternalMessyncRequired { get; set; }

    public DateTime? ExternalMeslastSync { get; set; }

    public bool Fsaitem { get; set; }

    public bool Fsaequipment { get; set; }

    public string Bolclass { get; set; } = null!;

    public decimal FairMarketValue { get; set; }

    public string SaftprodCategory { get; set; } = null!;

    public string AttrClassId { get; set; } = null!;

    public bool LocationIdnumReq { get; set; }

    public bool LocationTrackInv { get; set; }

    public bool LocationMtlView { get; set; }

    public bool Lcnrvreporting { get; set; }

    public decimal LcnrvestimatedUnitPrice { get; set; }

    public string MxcustomsUmfrom { get; set; } = null!;

    public string LocationFormatId { get; set; } = null!;

    public bool IsServices { get; set; }

    public string PedetrGoodServiceCode { get; set; } = null!;

    public string PeproductServiceCode { get; set; } = null!;

    public string DualUomclassId { get; set; } = null!;

    public string CnproductName { get; set; } = null!;

    public decimal Cnweight { get; set; }

    public string CnweightUom { get; set; } = null!;

    public bool Cnbonded { get; set; }

    public bool TrackInventoryAttributes { get; set; }

    public int DefaultAttributeSetId { get; set; }

    public string AttIsorigCountry { get; set; } = null!;

    public string ExternalSchemeId { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string CommoditySchemeId { get; set; } = null!;

    public string CommoditySchemeVersion { get; set; } = null!;

    public bool TrackInventoryByRevision { get; set; }

    public bool PlanningByRevision { get; set; }
}
