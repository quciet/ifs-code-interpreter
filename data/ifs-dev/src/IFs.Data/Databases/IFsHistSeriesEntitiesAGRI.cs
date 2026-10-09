using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    //LAND DATA
    [Table("SeriesLandTotal")]
    internal partial class SeriesLandTotal : Series
    { }

    [Table("SeriesLandCrop")]
    internal partial class SeriesLandCrop : Series
    { }

    [Table("SeriesLandGrazing")]
    internal partial class SeriesLandGrazing : Series
    { }

    [Table("SeriesLandForest")]
    internal partial class SeriesLandForest : Series
    { }

    [Table("SeriesLandOther")]
    internal partial class SeriesLandOther : Series
    { }

    [Table("SeriesLandUrban&Built")]
    internal partial class SeriesLandUrbanBuilt : Series
    { }

    [Table("SeriesLandBuiltGFNCorine")]
    internal partial class SeriesLandBuiltGFNCorine : Series
    { }

    [Table("SeriesLandBuiltGFNGAEZ")]
    internal partial class SeriesLandBuiltGFNGAEZ : Series
    { }

    [Table("SeriesLandBuiltGFNglc")]
    internal partial class SeriesLandBuiltGFNglc : Series
    { }
    [Table("SeriesLandBuiltGFNSage")]
    internal partial class SeriesLandBuiltGFNSage : Series
    { }
    [Table("SeriesLandPotentialArable")]
    internal partial class SeriesLandPotentialArable : Series
    { }
    [Table("SeriesPotArableLandEitelbergMed")]
    internal partial class SeriesPotArableLandEitelbergMed : Series
    { }
    [Table("SeriesPotArableLandEitelbergLow")]
    internal partial class SeriesPotArableLandEitelbergLow : Series
    { }

    //Fish Calories
    [Table("SeriesAgFishCalPerCapPerDayCephalopodsFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayCrustaceansFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayDemersalFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayDemersalFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayFreshwaterFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayMarineFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayMarineFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayMolluscsFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayMolluscsFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayPelagicFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayPelagicFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayAqAnimalsFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayAqPlantsFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayBodyOilFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayBodyOilFAO : Series
    { }

    [Table("SeriesAgFishCalPerCapPerDayLiverOilFAO")]
    internal partial class SeriesAgFishCalPerCapPerDayLiverOilFAO : Series
    { }

    //Fish Protein
    [Table("SeriesAgFishProteinPerCapPerDayCephalopodsFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayCrustaceansFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayDemersalFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayDemersalFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayFreshwaterFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayMarineFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayMarineFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayMolluscsFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayMolluscsFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayPelagicFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayPelagicFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayAqAnimalsFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayAqPlantsFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayBodyOilFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayBodyOilFAO : Series
    { }

    [Table("SeriesAgFishProteinPerCapPerDayLiverOilFAO")]
    internal partial class SeriesAgFishProteinPerCapPerDayLiverOilFAO : Series
    { }

    //Fish Grams
    [Table("SeriesAgFishFoodSupplyPerCapPerDayCephalopodsFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayCrustaceansFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayDemersalFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayDemersalFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayFreshwaterFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayMarineFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayMarineFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayMolluscsFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayMolluscsFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayPelagicFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayPelagicFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayAqAnimalsFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayAqPlantsFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayBodyOilFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayBodyOilFAO : Series
    { }

    [Table("SeriesAgFishFoodSupplyPerCapPerDayLiverOilFAO")]
    internal partial class SeriesAgFishFoodSupplyPerCapPerDayLiverOilFAO : Series
    { }

    //Calories per capita
    [Table("SeriesAGCropCalPerCapPerDayFAO")]
    internal partial class SeriesAGCropCalPerCapPerDayFAO : Series
    { }

    [Table("SeriesAGMeatCalPerCapPerDayFAO")]
    internal partial class SeriesAGMeatCalPerCapPerDayFAO : Series
    { }

    [Table("SeriesMalnDietEnSup")]
    internal partial class SeriesMalnDietEnSup : Series
    { }

    //Malnutrition
    [Table("SeriesMalnChil%WeightWB")]
    internal partial class SeriesMalnChilWeightWB : Series
    { }

    [Table("SeriesMalnPop%WBOld")]
    internal partial class SeriesMalnPopWB : Series
    { }

    [Table("SeriesMalnPop%UN")]
    internal partial class SeriesMalnPopUN : Series
    { }

    [Table("SeriesMalnPop%")]
    internal partial class SeriesMalnPop : Series
    { }

    //PROTEIN PER CAPITA PER DAY
    [Table("SeriesAGCropProteinPerCapPerDayFAO")]
    internal partial class SeriesAGCropProteinPerCapPerDayFAO : Series
    { }

    [Table("SeriesAGMeatProteinPerCapPerDayFAO")]
    internal partial class SeriesAGMeatProteinPerCapPerDayFAO : Series
    { }

    //GRAMS PER CAPITA PER DAY
    [Table("SeriesAGCropFoodSupplyPerCapPerDayFAO")]
    internal partial class SeriesAGCropFoodSupplyPerCapPerDayFAO : Series
    { }

    [Table("SeriesAGMeatFoodSupplyPerCapPerDayFAO")]
    internal partial class SeriesAGMeatFoodSupplyPerCapPerDayFAO : Series
    { }

    //DEMAND/USE SIDE
    [Table("SeriesAgFishtoFoodCephalopodsFAO")]
    internal partial class SeriesAgFishtoFoodCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishtoFoodCrustaceansFAO")]
    internal partial class SeriesAgFishtoFoodCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishtoFoodDemersalFAO")]
    internal partial class SeriesAgFishtoFoodDemersalFAO : Series
    { }

    [Table("SeriesAgFishtoFoodFreshwaterFAO")]
    internal partial class SeriesAgFishtoFoodFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishtoFoodMarineFAO")]
    internal partial class SeriesAgFishtoFoodMarineFAO : Series
    { }

    [Table("SeriesAgFishtoFoodMolluscsFAO")]
    internal partial class SeriesAgFishtoFoodMolluscsFAO : Series
    { }

    [Table("SeriesAgFishtoFoodPelagicFAO")]
    internal partial class SeriesAgFishtoFoodPelagicFAO : Series
    { }

    [Table("SeriesAgFishtoFoodAqAnimalsFAO")]
    internal partial class SeriesAgFishtoFoodAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishtoFoodAqPlantsFAO")]
    internal partial class SeriesAgFishtoFoodAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishtoFoodBodyOilFAO")]
    internal partial class SeriesAgFishtoFoodBodyOilFAO : Series
    { }

    [Table("SeriesAgFishtoFoodLiverOilFAO")]
    internal partial class SeriesAgFishtoFoodLiverOilFAO : Series
    { }

    //Feed
    [Table("SeriesAgFishtoFeedCephalopodsFAO")]
    internal partial class SeriesAgFishtoFeedCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishtoFeedCrustaceansFAO")]
    internal partial class SeriesAgFishtoFeedCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishtoFeedDemersalFAO")]
    internal partial class SeriesAgFishtoFeedDemersalFAO : Series
    { }

    [Table("SeriesAgFishtoFeedFreshwaterFAO")]
    internal partial class SeriesAgFishtoFeedFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishtoFeedMarineFAO")]
    internal partial class SeriesAgFishtoFeedMarineFAO : Series
    { }

    [Table("SeriesAgFishtoFeedMolluscsFAO")]
    internal partial class SeriesAgFishtoFeedMolluscsFAO : Series
    { }

    [Table("SeriesAgFishtoFeedPelagicFAO")]
    internal partial class SeriesAgFishtoFeedPelagicFAO : Series
    { }

    [Table("SeriesAgFishtoFeedAqPlantsFAO")]
    internal partial class SeriesAgFishtoFeedAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishtoFeedBodyOilFAO")]
    internal partial class SeriesAgFishtoFeedBodyOilFAO : Series
    { }

    [Table("SeriesAgFishtoFeedLiverOilFAO")]
    internal partial class SeriesAgFishtoFeedLiverOilFAO : Series
    { }

    [Table("SeriesAgFishtoFeedMealFAO")]
    internal partial class SeriesAgFishtoFeedMealFAO : Series
    { }

    //Seed
    [Table("SeriesAgFishtoSeedCephalopodsFAO")]
    internal partial class SeriesAgFishtoSeedCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishtoSeedCrustaceansFAO")]
    internal partial class SeriesAgFishtoSeedCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishtoSeedDemersalFAO")]
    internal partial class SeriesAgFishtoSeedDemersalFAO : Series
    { }

    [Table("SeriesAgFishtoSeedFreshwaterFAO")]
    internal partial class SeriesAgFishtoSeedFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishtoSeedMarineFAO")]
    internal partial class SeriesAgFishtoSeedMarineFAO : Series
    { }

    [Table("SeriesAgFishtoSeedMolluscsFAO")]
    internal partial class SeriesAgFishtoSeedMolluscsFAO : Series
    { }

    [Table("SeriesAgFishtoSeedPelagicFAO")]
    internal partial class SeriesAgFishtoSeedPelagicFAO : Series
    { }

    [Table("SeriesAgFishtoSeedAqAnimalsFAO")]
    internal partial class SeriesAgFishtoSeedAqAnimalsFAO : Series
    { }

    //Other Uses
    [Table("SeriesAgFishtoOtherUtilCephalopodsFAO")]
    internal partial class SeriesAgFishtoOtherUtilCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilCrustaceansFAO")]
    internal partial class SeriesAgFishtoOtherUtilCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilDemersalFAO")]
    internal partial class SeriesAgFishtoOtherUtilDemersalFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilFreshwaterFAO")]
    internal partial class SeriesAgFishtoOtherUtilFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilMarineFAO")]
    internal partial class SeriesAgFishtoOtherUtilMarineFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilMolluscsFAO")]
    internal partial class SeriesAgFishtoOtherUtilMolluscsFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilPelagicFAO")]
    internal partial class SeriesAgFishtoOtherUtilPelagicFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilAqAnimalsFAO")]
    internal partial class SeriesAgFishtoOtherUtilAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilAqPlantsFAO")]
    internal partial class SeriesAgFishtoOtherUtilAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilBodyOilFAO")]
    internal partial class SeriesAgFishtoOtherUtilBodyOilFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilLiverOilFAO")]
    internal partial class SeriesAgFishtoOtherUtilLiverOilFAO : Series
    { }

    [Table("SeriesAgFishtoOtherUtilMealFAO")]
    internal partial class SeriesAgFishtoOtherUtilMealFAO : Series
    { }

    //CROPS and MEAT
    [Table("SeriesAGCroptoFoodFAO")]
    internal partial class SeriesAGCroptoFoodFAO : Series
    { }

    [Table("SeriesAGMeattoFoodFAO")]
    internal partial class SeriesAGMeattoFoodFAO : Series
    { }

    [Table("SeriesAGCroptoFoodManuFAO")]
    internal partial class SeriesAGCroptoFoodManuFAO : Series
    { }

    [Table("SeriesAGMeattoFoodManuFAO")]
    internal partial class SeriesAGMeattoFoodManuFAO : Series
    { }

    [Table("SeriesAGCroptoSeedFAO")]
    internal partial class SeriesAGCroptoSeedFAO : Series
    { }

    [Table("SeriesAGMeattoSeedFAO")]
    internal partial class SeriesAGMeattoSeedFAO : Series
    { }

    [Table("SeriesAGCroptoOtherUtilFAO")]
    internal partial class SeriesAGCroptoOtherUtilFAO : Series
    { }

    [Table("SeriesAGMeattoOtherUtilFAO")]
    internal partial class SeriesAGMeattoOtherUtilFAO : Series
    { }

    [Table("SeriesAGCroptoFeedFAO")]
    internal partial class SeriesAGCroptoFeedFAO : Series
    { }

    [Table("SeriesAGMeattoFeedFAO")]
    internal partial class SeriesAGMeattoFeedFAO : Series
    { }

    [Table("SeriesAGCroptoWasteFAO")]
    internal partial class SeriesAGCroptoWasteFAO : Series
    { }

    [Table("SeriesAGMeattoWasteFAO")]
    internal partial class SeriesAGMeattoWasteFAO : Series
    { }

    //Fish Supply Data
    [Table("SeriesAgFishProdCephalopodsFAO")]
    internal partial class SeriesAgFishProdCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishProdCrustaceansFAO")]
    internal partial class SeriesAgFishProdCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishProdDemersalFAO")]
    internal partial class SeriesAgFishProdDemersalFAO : Series
    { }

    [Table("SeriesAgFishProdFreshwaterFAO")]
    internal partial class SeriesAgFishProdFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishProdMarineFAO")]
    internal partial class SeriesAgFishProdMarineFAO : Series
    { }

    [Table("SeriesAgFishProdMolluscsFAO")]
    internal partial class SeriesAgFishProdMolluscsFAO : Series
    { }

    [Table("SeriesAgFishProdPelagicFAO")]
    internal partial class SeriesAgFishProdPelagicFAO : Series
    { }

    [Table("SeriesAgFishProdAqAnimalsFAO")]
    internal partial class SeriesAgFishProdAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishProdAqPlantsFAO")]
    internal partial class SeriesAgFishProdAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishProdBodyOilFAO")]
    internal partial class SeriesAgFishProdBodyOilFAO : Series
    { }

    [Table("SeriesAgFishProdLiverOilFAO")]
    internal partial class SeriesAgFishProdLiverOilFAO : Series
    { }

    [Table("SeriesAgFishProdMealFAO")]
    internal partial class SeriesAgFishProdMealFAO : Series
    { }

    [Table("SeriesAgFishImportsCephalopodsFAO")]
    internal partial class SeriesAgFishImportsCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishImportsCrustaceansFAO")]
    internal partial class SeriesAgFishImportsCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishImportsDemersalFAO")]
    internal partial class SeriesAgFishImportsDemersalFAO : Series
    { }

    [Table("SeriesAgFishImportsFreshwaterFAO")]
    internal partial class SeriesAgFishImportsFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishImportsMarineFAO")]
    internal partial class SeriesAgFishImportsMarineFAO : Series
    { }

    [Table("SeriesAgFishImportsMolluscsFAO")]
    internal partial class SeriesAgFishImportsMolluscsFAO : Series
    { }

    [Table("SeriesAgFishImportsPelagicFAO")]
    internal partial class SeriesAgFishImportsPelagicFAO : Series
    { }

    [Table("SeriesAgFishImportsAqAnimalsFAO")]
    internal partial class SeriesAgFishImportsAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishImportsAqPlantsFAO")]
    internal partial class SeriesAgFishImportsAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishImportsBodyOilFAO")]
    internal partial class SeriesAgFishImportsBodyOilFAO : Series
    { }

    [Table("SeriesAgFishImportsLiverOilFAO")]
    internal partial class SeriesAgFishImportsLiverOilFAO : Series
    { }

    [Table("SeriesAgFishImportsMealFAO")]
    internal partial class SeriesAgFishImportsMealFAO : Series
    { }

    [Table("SeriesAgFishExportsCephalopodsFAO")]
    internal partial class SeriesAgFishExportsCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishExportsCrustaceansFAO")]
    internal partial class SeriesAgFishExportsCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishExportsDemersalFAO")]
    internal partial class SeriesAgFishExportsDemersalFAO : Series
    { }

    [Table("SeriesAgFishExportsFreshwaterFAO")]
    internal partial class SeriesAgFishExportsFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishExportsMarineFAO")]
    internal partial class SeriesAgFishExportsMarineFAO : Series
    { }

    [Table("SeriesAgFishExportsMolluscsFAO")]
    internal partial class SeriesAgFishExportsMolluscsFAO : Series
    { }

    [Table("SeriesAgFishExportsPelagicFAO")]
    internal partial class SeriesAgFishExportsPelagicFAO : Series
    { }

    [Table("SeriesAgFishExportsAqAnimalsFAO")]
    internal partial class SeriesAgFishExportsAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishExportsAqPlantsFAO")]
    internal partial class SeriesAgFishExportsAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishExportsBodyOilFAO")]
    internal partial class SeriesAgFishExportsBodyOilFAO : Series
    { }

    [Table("SeriesAgFishExportsLiverOilFAO")]
    internal partial class SeriesAgFishExportsLiverOilFAO : Series
    { }

    [Table("SeriesAgFishExportsMealFAO")]
    internal partial class SeriesAgFishExportsMealFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyCephalopodsFAO")]
    internal partial class SeriesAgFishDomesticSupplyCephalopodsFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyCrustaceansFAO")]
    internal partial class SeriesAgFishDomesticSupplyCrustaceansFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyDemersalFAO")]
    internal partial class SeriesAgFishDomesticSupplyDemersalFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyFreshwaterFAO")]
    internal partial class SeriesAgFishDomesticSupplyFreshwaterFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyMarineFAO")]
    internal partial class SeriesAgFishDomesticSupplyMarineFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyMolluscsFAO")]
    internal partial class SeriesAgFishDomesticSupplyMolluscsFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyPelagicFAO")]
    internal partial class SeriesAgFishDomesticSupplyPelagicFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyAqAnimalsFAO")]
    internal partial class SeriesAgFishDomesticSupplyAqAnimalsFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyAqPlantsFAO")]
    internal partial class SeriesAgFishDomesticSupplyAqPlantsFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyBodyOilFAO")]
    internal partial class SeriesAgFishDomesticSupplyBodyOilFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyLiverOilFAO")]
    internal partial class SeriesAgFishDomesticSupplyLiverOilFAO : Series
    { }

    [Table("SeriesAgFishDomesticSupplyMealFAO")]
    internal partial class SeriesAgFishDomesticSupplyMealFAO : Series
    { }

    //FSJ
    [Table("SeriesAgFishAquaProdCephalopodsFSJ")]
    internal partial class SeriesAgFishAquaProdCephalopodsFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdCrustaceansFSJ")]
    internal partial class SeriesAgFishAquaProdCrustaceansFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdDemersalFSJ")]
    internal partial class SeriesAgFishAquaProdDemersalFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdFreshwaterFSJ")]
    internal partial class SeriesAgFishAquaProdFreshwaterFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdMarineFSJ")]
    internal partial class SeriesAgFishAquaProdMarineFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdMolluscsFSJ")]
    internal partial class SeriesAgFishAquaProdMolluscsFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdPelagicFSJ")]
    internal partial class SeriesAgFishAquaProdPelagicFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdAqAnimalsFSJ")]
    internal partial class SeriesAgFishAquaProdAqAnimalsFSJ : Series
    { }

    [Table("SeriesAgFishAquaProdAqPlantsFSJ")]
    internal partial class SeriesAgFishAquaProdAqPlantsFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdCephalopodsFSJ")]
    internal partial class SeriesAgFishCatchProdCephalopodsFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdCrustaceansFSJ")]
    internal partial class SeriesAgFishCatchProdCrustaceansFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdDemersalFSJ")]
    internal partial class SeriesAgFishCatchProdDemersalFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdFreshwaterFSJ")]
    internal partial class SeriesAgFishCatchProdFreshwaterFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdMarineFSJ")]
    internal partial class SeriesAgFishCatchProdMarineFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdMolluscsFSJ")]
    internal partial class SeriesAgFishCatchProdMolluscsFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdPelagicFSJ")]
    internal partial class SeriesAgFishCatchProdPelagicFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdAqAnimalsFSJ")]
    internal partial class SeriesAgFishCatchProdAqAnimalsFSJ : Series
    { }

    [Table("SeriesAgFishCatchProdAqPlantsFSJ")]
    internal partial class SeriesAgFishCatchProdAqPlantsFSJ : Series
    { }

    //CROP PRODUCTION
    [Table("SeriesAGCropProductionFAO")]
    internal partial class SeriesAGCropProductionFAO : Series
    { }

    [Table("SeriesAgProdCereals")]
    internal partial class SeriesAgProdCereals : Series
    { }

    [Table("SeriesAgProdRootsTub")]
    internal partial class SeriesAgProdRootsTub : Series
    { }

    [Table("SeriesAgProdPulses")]
    internal partial class SeriesAgProdPulses : Series
    { }

    [Table("SeriesAgProdVegMel")]
    internal partial class SeriesAgProdVegMel : Series
    { }

    [Table("SeriesAgProdFruitsExclMelons")]
    internal partial class SeriesAgProdFruitsExclMelons : Series
    { }

    //MEAT PRODUCTION
    [Table("SeriesAGMeatProductionFAO")]
    internal partial class SeriesAGMeatProductionFAO : Series
    { }

    [Table("SeriesAGProdMeat")]
    internal partial class SeriesAGProdMeat : Series
    { }

    [Table("SeriesAGBovineMeatProductionFAO")]
    internal partial class SeriesAGBovineMeatProductionFAO : Series
    { }

    [Table("SeriesAGMeatOtherProductionFAO")]
    internal partial class SeriesAGMeatOtherProductionFAO : Series
    { }

    [Table("SeriesAGMuttonandGoatMeatProductionFAO")]
    internal partial class SeriesAGMuttonandGoatMeatProductionFAO : Series
    { }

    [Table("SeriesAGPigmeatProductionFAO")]
    internal partial class SeriesAGPigmeatProductionFAO : Series
    { }

    [Table("SeriesAGPoultryMeatProductionFAO")]
    internal partial class SeriesAGPoultryMeatProductionFAO : Series
    { }

    //CROP and MEAT IMPORTS and EXPORTS
    [Table("SeriesAGCropImportsFAO")]
    internal partial class SeriesAGCropImportsFAO : Series
    { }

    [Table("SeriesAGCropExportsFAO")]
    internal partial class SeriesAGCropExportsFAO : Series
    { }

    [Table("SeriesAGCerealsIm")]
    internal partial class SeriesAGCerealsIm : Series
    { }

    [Table("SeriesAGPulsesIm")]
    internal partial class SeriesAGPulsesIm : Series
    { }

    [Table("SeriesAGFruVegIm")]
    internal partial class SeriesAGFruVegIm : Series
    { }

    [Table("SeriesAGCerealsEx")]
    internal partial class SeriesAGCerealsEx : Series
    { }

    [Table("SeriesAGPulsesEx")]
    internal partial class SeriesAGPulsesEx : Series
    { }

    [Table("SeriesAGFruVegEx")]
    internal partial class SeriesAGFruVegEx : Series
    { }

    [Table("SeriesAGMeatImportsFAO")]
    internal partial class SeriesAGMeatImportsFAO : Series
    { }

    [Table("SeriesAGMeatExportsFAO")]
    internal partial class SeriesAGMeatExportsFAO : Series
    { }

    [Table("SeriesAGMeatIm")]
    internal partial class SeriesAGMeatIm : Series
    { }

    [Table("SeriesAGMeatEx")]
    internal partial class SeriesAGMeatEx : Series
    { }

    //CROP and MEAT TOTAL DOMESTIC SUPPLY and NORMALIZE PRODUCTION, EXPORTS, and IMPORTS
    [Table("SeriesAGCropDomesticSupplyFAO")]
    internal partial class SeriesAGCropDomesticSupplyFAO : Series
    { }

    [Table("SeriesAGMeatDomesticSupplyFAO")]
    internal partial class SeriesAGMeatDomesticSupplyFAO : Series
    { }

    //TRADE
    [Table("SeriesAGCropImportQuantityFAOTrade")]
    internal partial class SeriesAGCropImportQuantityFAOTrade : Series
    { }

    [Table("SeriesAGCropExportQuantityFAOTrade")]
    internal partial class SeriesAGCropExportQuantityFAOTrade : Series
    { }

    [Table("SeriesAgCropImportValue%GDPFAOTrade")]
    internal partial class SeriesAgCropImportValueGDPFAOTrade : Series
    { }

    [Table("SeriesAgCropExportValue%GDPFAOTrade")]
    internal partial class SeriesAgCropExportValueGDPFAOTrade : Series
    { }

    [Table("SeriesAGMeatImportQuantityFAOTrade")]
    internal partial class SeriesAGMeatImportQuantityFAOTrade : Series
    { }

    [Table("SeriesAGMeatExportQuantityFAOTrade")]
    internal partial class SeriesAGMeatExportQuantityFAOTrade : Series
    { }

    [Table("SeriesAgMeatImportValue%GDPFAOTrade")]
    internal partial class SeriesAgMeatImportValueGDPFAOTrade : Series
    { }

    [Table("SeriesAgMeatExportValue%GDPFAOTrade")]
    internal partial class SeriesAgMeatExportValueGDPFAOTrade : Series
    { }

    [Table("SeriesAgFishImportQuantityFAOTrade")]
    internal partial class SeriesAgFishImportQuantityFAOTrade : Series
    { }

    [Table("SeriesAgFishExportQuantityFAOTrade")]
    internal partial class SeriesAgFishExportQuantityFAOTrade : Series
    { }

    [Table("SeriesAgFishImportValue%GDPFAOTrade")]
    internal partial class SeriesAgFishImportValueGDPFAOTrade : Series
    { }

    [Table("SeriesAgFishExportValue%GDPFAOTrade")]
    internal partial class SeriesAgFishExportValueGDPFAOTrade : Series
    { }

    //WATER RESOURCES
    [Table("SeriesWaterAnRenResources")]
    internal partial class SeriesWaterAnRenResources : Series
    { }

    [Table("SeriesWaterAnRenResourcesOld")]
    internal partial class SeriesWaterAnRenResourcesOld : Series
    { }

    [Table("SeriesWaterAnWithdrawals")]
    internal partial class SeriesWaterAnWithdrawals : Series
    { }

    //jrs 2020/12/10
    [Table("SeriesMalnCaloricDistribution")]
    internal partial class SeriesMalnCaloricDistribution : Series
    { }

    [Table("SeriesMalnMinNutrRequired")]
    internal partial class SeriesMalnMinNutrRequired : Series
    { }

    [Table("SeriesPotArableLandEitelbergHigh")]
    internal partial class SeriesPotArableLandEitelbergHigh : Series
    { }

    [Table("SeriesYieldIntWtMueller")]
    internal partial class SeriesYieldIntWtMueller : Series
    { }

    //jrs 2023/11/10
    [Table("SeriesAgriculturalProductionIMAGELIFETP")]
    internal partial class SeriesAgriculturalProductionIMAGELIFETP : Series
    { }
    [Table("SeriesLandcoverCroplandIMAGELIFETP")]
    internal partial class SeriesLandcoverCroplandIMAGELIFETP : Series
    { }
    [Table("SeriesYieldcerealCropYieldperHectareIMAGELIFETP")]
    internal partial class SeriesYieldcerealCropYieldperHectareIMAGELIFETP : Series
    { }
    [Table("SeriesFooddemandCaloriesIMAGELIFETP")]
    internal partial class SeriesFooddemandCaloriesIMAGELIFETP : Series
    { }
    [Table("SeriesAgriculturalProductionIMAGETECHTP")]
    internal partial class SeriesAgriculturalProductionIMAGETECHTP : Series
    { }
    [Table("SeriesLandcoverCroplandIMAGETECHTP")]
    internal partial class SeriesLandcoverCroplandIMAGETECHTP : Series
    { }
    [Table("SeriesYieldcerealCropYieldperHectareIMAGETECHTP")]
    internal partial class SeriesYieldcerealCropYieldperHectareIMAGETECHTP : Series
    { }
    [Table("SeriesFooddemandCaloriesIMAGETECHTP")]
    internal partial class SeriesFooddemandCaloriesIMAGETECHTP : Series
    { }

    //jrs 2023/11/30
    [Table("SeriesAgriculturalProductionIMAGEREFTP")]
    internal partial class SeriesAgriculturalProductionIMAGEREFTP : Series
    { }
    [Table("SeriesLandcoverCroplandIMAGEREFTP")]
    internal partial class SeriesLandcoverCroplandIMAGEREFTP : Series
    { }
    [Table("SeriesYieldcerealCropYieldperHectareIMAGEREFTP")]
    internal partial class SeriesYieldcerealCropYieldperHectareIMAGEREFTP : Series
    { }
    [Table("SeriesFooddemandCaloriesIMAGEREFTP")]
    internal partial class SeriesFooddemandCaloriesIMAGEREFTP : Series
    { }


    [Table("SeriesAgriculturalProductionIMAGEREF1P5C")]
    internal partial class SeriesAgriculturalProductionIMAGEREF1P5C : Series
    { }
    [Table("SeriesLandcoverCroplandIMAGEREF1P5C")]
    internal partial class SeriesLandcoverCroplandIMAGEREF1P5C : Series
    { }
    [Table("SeriesYieldcerealCropYieldperHectareIMAGEREF1P5C")]
    internal partial class SeriesYieldcerealCropYieldperHectareIMAGEREF1P5C : Series
    { }
    [Table("SeriesFooddemandCaloriesIMAGEREF1P5C")]
    internal partial class SeriesFooddemandCaloriesIMAGEREF1P5C : Series
    { }
    //jrs 2024/02/19
    [Table("SeriesExoLDCROP")]
    internal partial class SeriesExoLDCROP : Series
    { }
    [Table("SeriesExoYL")]
    internal partial class SeriesExoYL : Series
    { }
    //jrs 2024/07/16
    [Table("SeriesExoAGP")]
    internal partial class SeriesExoAGP : Series
    { }
    [Table("SeriesExoFDEM")]
    internal partial class SeriesExoFDEM : Series
    { }

    internal partial class IFsHistSeriesContext
    {
        //jrs 2024/07/16
        protected virtual DbSet<SeriesExoAGP> SeriesExoAGP { get; set; }
        protected virtual DbSet<SeriesExoAGP> SeriesExoFDEM { get; set; }
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoLDCROP> SeriesExoLDCROP { get; set; }
        protected virtual DbSet<SeriesExoYL> SeriesExoYL { get; set; }

        //jrs 2023/11/10
        protected virtual DbSet<SeriesAgriculturalProductionIMAGELIFETP> SeriesAgriculturalProductionIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesLandcoverCroplandIMAGELIFETP> SeriesLandcoverCroplandIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesYieldcerealCropYieldperHectareIMAGELIFETP> SeriesYieldcerealCropYieldperHectareIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesFooddemandCaloriesIMAGELIFETP> SeriesFooddemandCaloriesIMAGELIFETP { get; set; }

        protected virtual DbSet<SeriesAgriculturalProductionIMAGETECHTP> SeriesAgriculturalProductionIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesLandcoverCroplandIMAGETECHTP> SeriesLandcoverCroplandIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesYieldcerealCropYieldperHectareIMAGETECHTP> SeriesYieldcerealCropYieldperHectareIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesFooddemandCaloriesIMAGETECHTP> SeriesFooddemandCaloriesIMAGETECHTP { get; set; }
        //jrs 2023/11/30
        protected virtual DbSet<SeriesAgriculturalProductionIMAGEREFTP> SeriesAgriculturalProductionIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesLandcoverCroplandIMAGEREFTP> SeriesLandcoverCroplandIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesYieldcerealCropYieldperHectareIMAGEREFTP> SeriesYieldcerealCropYieldperHectareIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesFooddemandCaloriesIMAGEREFTP> SeriesFooddemandCaloriesIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesAgriculturalProductionIMAGEREF1P5C> SeriesAgriculturalProductionIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesLandcoverCroplandIMAGEREF1P5C> SeriesLandcoverCroplandIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesYieldcerealCropYieldperHectareIMAGEREF1P5C> SeriesYieldcerealCropYieldperHectareIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesFooddemandCaloriesIMAGEREF1P5C> SeriesFooddemandCaloriesIMAGEREF1P5C { get; set; }



        //jrs 2022/03/08
        protected virtual DbSet<SeriesMalnDietEnSup> SeriesMalnDietEnSup { get; set; }

        //wv 2020/12/11
        protected virtual DbSet<SeriesPotArableLandEitelbergHigh> SeriesPotArableLandEitelbergHigh { get; set; }
        protected virtual DbSet<SeriesYieldIntWtMueller> SeriesYieldIntWtMueller { get; set; }

        //jrs 2020/12/10
        protected virtual DbSet<SeriesMalnCaloricDistribution> SeriesMalnCaloricDistribution { get; set; }
        protected virtual DbSet<SeriesMalnMinNutrRequired> SeriesMalnMinNutrRequired { get; set; }
        

        //Land data
        protected virtual DbSet<SeriesLandTotal> LandTotal { get; set; }
        protected virtual DbSet<SeriesLandCrop> LD_Crop { get; set; }
        protected virtual DbSet<SeriesLandGrazing> LD_Grazing { get; set; }
        protected virtual DbSet<SeriesLandForest> LD_Forest { get; set; }
        protected virtual DbSet<SeriesLandOther> LD_Other { get; set; }
        protected virtual DbSet<SeriesLandUrbanBuilt> SeriesLandUrbanBuilt { get; set; }

        protected virtual DbSet<SeriesLandBuiltGFNCorine> LD_Urban { get; set; }
        protected virtual DbSet<SeriesLandBuiltGFNGAEZ> SeriesLandBuiltGFNGAEZ { get; set; }
        protected virtual DbSet<SeriesLandBuiltGFNglc> SeriesLandBuiltGFNglc { get; set; }
        protected virtual DbSet<SeriesLandBuiltGFNSage> SeriesLandBuiltGFNSage { get; set; }
        protected virtual DbSet<SeriesLandPotentialArable> SeriesLandPotentialArable { get; set; }
        protected virtual DbSet<SeriesPotArableLandEitelbergMed> SeriesPotArableLandEitelbergMed { get; set; }
        protected virtual DbSet<SeriesPotArableLandEitelbergLow> SeriesPotArableLandEitelbergLow { get; set; }

        //Fish Calories
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayCephalopodsFAO> SeriesAgFishCalPerCapPerDayCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayCrustaceansFAO> SeriesAgFishCalPerCapPerDayCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayDemersalFAO> SeriesAgFishCalPerCapPerDayDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayFreshwaterFAO> SeriesAgFishCalPerCapPerDayFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayMarineFAO> SeriesAgFishCalPerCapPerDayMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayMolluscsFAO> SeriesAgFishCalPerCapPerDayMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayPelagicFAO> SeriesAgFishCalPerCapPerDayPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayAqAnimalsFAO> SeriesAgFishCalPerCapPerDayAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayAqPlantsFAO> SeriesAgFishCalPerCapPerDayAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayBodyOilFAO> SeriesAgFishCalPerCapPerDayBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishCalPerCapPerDayLiverOilFAO> SeriesAgFishCalPerCapPerDayLiverOilFAO { get; set; }

        //Fish Protein
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayCephalopodsFAO> SeriesAgFishProteinPerCapPerDayCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayCrustaceansFAO> SeriesAgFishProteinPerCapPerDayCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayDemersalFAO> SeriesAgFishProteinPerCapPerDayDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayFreshwaterFAO> SeriesAgFishProteinPerCapPerDayFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayMarineFAO> SeriesAgFishProteinPerCapPerDayMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayMolluscsFAO> SeriesAgFishProteinPerCapPerDayMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayPelagicFAO> SeriesAgFishProteinPerCapPerDayPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayAqAnimalsFAO> SeriesAgFishProteinPerCapPerDayAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayAqPlantsFAO> SeriesAgFishProteinPerCapPerDayAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayBodyOilFAO> SeriesAgFishProteinPerCapPerDayBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProteinPerCapPerDayLiverOilFAO> SeriesAgFishProteinPerCapPerDayLiverOilFAO { get; set; }

        //Fish Grams 
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayCephalopodsFAO> SeriesAgFishFoodSupplyPerCapPerDayCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayCrustaceansFAO> SeriesAgFishFoodSupplyPerCapPerDayCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayDemersalFAO> SeriesAgFishFoodSupplyPerCapPerDayDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayFreshwaterFAO> SeriesAgFishFoodSupplyPerCapPerDayFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayMarineFAO> SeriesAgFishFoodSupplyPerCapPerDayMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayMolluscsFAO> SeriesAgFishFoodSupplyPerCapPerDayMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayPelagicFAO> SeriesAgFishFoodSupplyPerCapPerDayPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayAqAnimalsFAO> SeriesAgFishFoodSupplyPerCapPerDayAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayAqPlantsFAO> SeriesAgFishFoodSupplyPerCapPerDayAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayBodyOilFAO> SeriesAgFishFoodSupplyPerCapPerDayBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishFoodSupplyPerCapPerDayLiverOilFAO> SeriesAgFishFoodSupplyPerCapPerDayLiverOilFAO { get; set; }

        //Calories per capita 
        protected virtual DbSet<SeriesAGCropCalPerCapPerDayFAO> SeriesAGCropCalPerCapPerDayFAO { get; set; }
        protected virtual DbSet<SeriesAGMeatCalPerCapPerDayFAO> SeriesAGMeatCalPerCapPerDayFAO { get; set; }

        //Malnutrition
        protected virtual DbSet<SeriesMalnChilWeightWB> MALNCHP { get; set; }
        protected virtual DbSet<SeriesMalnPop> MALNPOPP { get; set; }
        protected virtual DbSet<SeriesMalnPopUN> SeriesMalnPopUN { get; set; }
        protected virtual DbSet<SeriesMalnPopWB> SeriesMalnPopWB { get; set; }

        //PROTEIN PER CAPITA PER DAY
        protected virtual DbSet<SeriesAGCropProteinPerCapPerDayFAO> PROTEINPC_Crop { get; set; }
        protected virtual DbSet<SeriesAGMeatProteinPerCapPerDayFAO> PROTEINPC_Meat { get; set; }

        //GRAMS PER CAPITA PER DAY
        protected virtual DbSet<SeriesAGCropFoodSupplyPerCapPerDayFAO> SeriesAGCropFoodSupplyPerCapPerDayFAO { get; set; }
        protected virtual DbSet<SeriesAGMeatFoodSupplyPerCapPerDayFAO> SeriesAGMeatFoodSupplyPerCapPerDayFAO { get; set; }

        //DEMAND/USE SIDE
        protected virtual DbSet<SeriesAgFishtoFoodCephalopodsFAO> SeriesAgFishtoFoodCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodCrustaceansFAO> SeriesAgFishtoFoodCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodDemersalFAO> SeriesAgFishtoFoodDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodFreshwaterFAO> SeriesAgFishtoFoodFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodMarineFAO> SeriesAgFishtoFoodMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodMolluscsFAO> SeriesAgFishtoFoodMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodPelagicFAO> SeriesAgFishtoFoodPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodAqAnimalsFAO> SeriesAgFishtoFoodAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodAqPlantsFAO> SeriesAgFishtoFoodAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodBodyOilFAO> SeriesAgFishtoFoodBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFoodLiverOilFAO> SeriesAgFishtoFoodLiverOilFAO { get; set; }
        //Feed
        protected virtual DbSet<SeriesAgFishtoFeedCephalopodsFAO> SeriesAgFishtoFeedCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedCrustaceansFAO> SeriesAgFishtoFeedCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedDemersalFAO> SeriesAgFishtoFeedDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedFreshwaterFAO> SeriesAgFishtoFeedFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedMarineFAO> SeriesAgFishtoFeedMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedMolluscsFAO> SeriesAgFishtoFeedMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedPelagicFAO> SeriesAgFishtoFeedPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedAqPlantsFAO> SeriesAgFishtoFeedAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedBodyOilFAO> SeriesAgFishtoFeedBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedLiverOilFAO> SeriesAgFishtoFeedLiverOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoFeedMealFAO> SeriesAgFishtoFeedMealFAO { get; set; }
        //Seed
        protected virtual DbSet<SeriesAgFishtoSeedCephalopodsFAO> SeriesAgFishtoSeedCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedCrustaceansFAO> SeriesAgFishtoSeedCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedDemersalFAO> SeriesAgFishtoSeedDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedFreshwaterFAO> SeriesAgFishtoSeedFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedMarineFAO> SeriesAgFishtoSeedMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedMolluscsFAO> SeriesAgFishtoSeedMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedPelagicFAO> SeriesAgFishtoSeedPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoSeedAqAnimalsFAO> SeriesAgFishtoSeedAqAnimalsFAO { get; set; }
        //Other Uses
        protected virtual DbSet<SeriesAgFishtoOtherUtilCephalopodsFAO> SeriesAgFishtoOtherUtilCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilCrustaceansFAO> SeriesAgFishtoOtherUtilCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilDemersalFAO> SeriesAgFishtoOtherUtilDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilFreshwaterFAO> SeriesAgFishtoOtherUtilFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilMarineFAO> SeriesAgFishtoOtherUtilMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilMolluscsFAO> SeriesAgFishtoOtherUtilMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilPelagicFAO> SeriesAgFishtoOtherUtilPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilAqAnimalsFAO> SeriesAgFishtoOtherUtilAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilAqPlantsFAO> SeriesAgFishtoOtherUtilAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilBodyOilFAO> SeriesAgFishtoOtherUtilBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilLiverOilFAO> SeriesAgFishtoOtherUtilLiverOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishtoOtherUtilMealFAO> SeriesAgFishtoOtherUtilMealFAO { get; set; }
        //CROPS and MEAT
        protected virtual DbSet<SeriesAGCroptoFoodFAO> SeriesAGCroptoFoodFAO { get; set; }
        protected virtual DbSet<SeriesAGMeattoFoodFAO> SeriesAGMeattoFoodFAO { get; set; }
        protected virtual DbSet<SeriesAGCroptoFoodManuFAO> SeriesAGCroptoFoodManuFAO { get; set; }
        protected virtual DbSet<SeriesAGMeattoFoodManuFAO> SeriesAGMeattoFoodManuFAO { get; set; }
        protected virtual DbSet<SeriesAGCroptoSeedFAO> SeriesAGCroptoSeedFAO { get; set; }
        protected virtual DbSet<SeriesAGMeattoSeedFAO> SeriesAGMeattoSeedFAO { get; set; }
        protected virtual DbSet<SeriesAGCroptoOtherUtilFAO> SeriesAGCroptoOtherUtilFAO { get; set; }
        protected virtual DbSet<SeriesAGMeattoOtherUtilFAO> SeriesAGMeattoOtherUtilFAO { get; set; }
        protected virtual DbSet<SeriesAGCroptoFeedFAO> SeriesAGCroptoFeedFAO { get; set; }
        protected virtual DbSet<SeriesAGMeattoFeedFAO> SeriesAGMeattoFeedFAO { get; set; }
        protected virtual DbSet<SeriesAGCroptoWasteFAO> SeriesAGCroptoWasteFAO { get; set; }
        protected virtual DbSet<SeriesAGMeattoWasteFAO> SeriesAGMeattoWasteFAO { get; set; }
        //Fish Supply Data
        protected virtual DbSet<SeriesAgFishProdCephalopodsFAO> SeriesAgFishProdCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdCrustaceansFAO> SeriesAgFishProdCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdDemersalFAO> SeriesAgFishProdDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdFreshwaterFAO> SeriesAgFishProdFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdMarineFAO> SeriesAgFishProdMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdMolluscsFAO> SeriesAgFishProdMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdPelagicFAO> SeriesAgFishProdPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdAqAnimalsFAO> SeriesAgFishProdAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdAqPlantsFAO> SeriesAgFishProdAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdBodyOilFAO> SeriesAgFishProdBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdLiverOilFAO> SeriesAgFishProdLiverOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishProdMealFAO> SeriesAgFishProdMealFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsCephalopodsFAO> SeriesAgFishImportsCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsCrustaceansFAO> SeriesAgFishImportsCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsDemersalFAO> SeriesAgFishImportsDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsFreshwaterFAO> SeriesAgFishImportsFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsMarineFAO> SeriesAgFishImportsMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsMolluscsFAO> SeriesAgFishImportsMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsPelagicFAO> SeriesAgFishImportsPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsAqAnimalsFAO> SeriesAgFishImportsAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsAqPlantsFAO> SeriesAgFishImportsAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsBodyOilFAO> SeriesAgFishImportsBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsLiverOilFAO> SeriesAgFishImportsLiverOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishImportsMealFAO> SeriesAgFishImportsMealFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsCephalopodsFAO> SeriesAgFishExportsCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsCrustaceansFAO> SeriesAgFishExportsCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsDemersalFAO> SeriesAgFishExportsDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsFreshwaterFAO> SeriesAgFishExportsFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsMarineFAO> SeriesAgFishExportsMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsMolluscsFAO> SeriesAgFishExportsMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsPelagicFAO> SeriesAgFishExportsPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsAqAnimalsFAO> SeriesAgFishExportsAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsAqPlantsFAO> SeriesAgFishExportsAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsBodyOilFAO> SeriesAgFishExportsBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsLiverOilFAO> SeriesAgFishExportsLiverOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishExportsMealFAO> SeriesAgFishExportsMealFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyCephalopodsFAO> SeriesAgFishDomesticSupplyCephalopodsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyCrustaceansFAO> SeriesAgFishDomesticSupplyCrustaceansFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyDemersalFAO> SeriesAgFishDomesticSupplyDemersalFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyFreshwaterFAO> SeriesAgFishDomesticSupplyFreshwaterFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyMarineFAO> SeriesAgFishDomesticSupplyMarineFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyMolluscsFAO> SeriesAgFishDomesticSupplyMolluscsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyPelagicFAO> SeriesAgFishDomesticSupplyPelagicFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyAqAnimalsFAO> SeriesAgFishDomesticSupplyAqAnimalsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyAqPlantsFAO> SeriesAgFishDomesticSupplyAqPlantsFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyBodyOilFAO> SeriesAgFishDomesticSupplyBodyOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyLiverOilFAO> SeriesAgFishDomesticSupplyLiverOilFAO { get; set; }
        protected virtual DbSet<SeriesAgFishDomesticSupplyMealFAO> SeriesAgFishDomesticSupplyMealFAO { get; set; }
        //FSJ
        protected virtual DbSet<SeriesAgFishAquaProdCephalopodsFSJ> SeriesAgFishAquaProdCephalopodsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdCrustaceansFSJ> SeriesAgFishAquaProdCrustaceansFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdDemersalFSJ> SeriesAgFishAquaProdDemersalFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdFreshwaterFSJ> SeriesAgFishAquaProdFreshwaterFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdMarineFSJ> SeriesAgFishAquaProdMarineFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdMolluscsFSJ> SeriesAgFishAquaProdMolluscsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdPelagicFSJ> SeriesAgFishAquaProdPelagicFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdAqAnimalsFSJ> SeriesAgFishAquaProdAqAnimalsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishAquaProdAqPlantsFSJ> SeriesAgFishAquaProdAqPlantsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdCephalopodsFSJ> SeriesAgFishCatchProdCephalopodsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdCrustaceansFSJ> SeriesAgFishCatchProdCrustaceansFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdDemersalFSJ> SeriesAgFishCatchProdDemersalFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdFreshwaterFSJ> SeriesAgFishCatchProdFreshwaterFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdMarineFSJ> SeriesAgFishCatchProdMarineFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdMolluscsFSJ> SeriesAgFishCatchProdMolluscsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdPelagicFSJ> SeriesAgFishCatchProdPelagicFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdAqAnimalsFSJ> SeriesAgFishCatchProdAqAnimalsFSJ { get; set; }
        protected virtual DbSet<SeriesAgFishCatchProdAqPlantsFSJ> SeriesAgFishCatchProdAqPlantsFSJ { get; set; }
        //CROP PRODUCTION
        protected virtual DbSet<SeriesAGCropProductionFAO> SeriesAGCropProductionFAO { get; set; }
        protected virtual DbSet<SeriesAgProdCereals> SeriesAgProdCereals { get; set; }
        protected virtual DbSet<SeriesAgProdRootsTub> SeriesAgProdRootsTub { get; set; }
        protected virtual DbSet<SeriesAgProdPulses> SeriesAgProdPulses { get; set; }
        protected virtual DbSet<SeriesAgProdVegMel> SeriesAgProdVegMel { get; set; }
        protected virtual DbSet<SeriesAgProdFruitsExclMelons> SeriesAgProdFruitsExclMelons { get; set; }
        //MEAT PRODUCTION
        protected virtual DbSet<SeriesAGMeatProductionFAO> SeriesAGMeatProductionFAO { get; set; }
        protected virtual DbSet<SeriesAGProdMeat> SeriesAGProdMeat { get; set; }
        protected virtual DbSet<SeriesAGBovineMeatProductionFAO> SeriesAGBovineMeatProductionFAO { get; set; }
        protected virtual DbSet<SeriesAGMeatOtherProductionFAO> SeriesAGMeatOtherProductionFAO { get; set; }
        protected virtual DbSet<SeriesAGMuttonandGoatMeatProductionFAO> SeriesAGMuttonandGoatMeatProductionFAO { get; set; }
        protected virtual DbSet<SeriesAGPigmeatProductionFAO> SeriesAGPigmeatProductionFAO { get; set; }
        protected virtual DbSet<SeriesAGPoultryMeatProductionFAO> SeriesAGPoultryMeatProductionFAO { get; set; }

        //CROP and MEAT IMPORTS and EXPORTS
        protected virtual DbSet<SeriesAGCropImportsFAO> SeriesAGCropImportsFAO { get; set; }
        protected virtual DbSet<SeriesAGCropExportsFAO> SeriesAGCropExportsFAO { get; set; }
        protected virtual DbSet<SeriesAGCerealsIm> SeriesAGCerealsIm { get; set; }
        protected virtual DbSet<SeriesAGPulsesIm> SeriesAGPulsesIm { get; set; }
        protected virtual DbSet<SeriesAGFruVegIm> SeriesAGFruVegIm { get; set; }
        protected virtual DbSet<SeriesAGCerealsEx> SeriesAGCerealsEx { get; set; }
        protected virtual DbSet<SeriesAGPulsesEx> SeriesAGPulsesEx { get; set; }
        protected virtual DbSet<SeriesAGFruVegEx> SeriesAGFruVegEx { get; set; }
        protected virtual DbSet<SeriesAGMeatImportsFAO> SeriesAGMeatImportsFAO { get; set; }
        protected virtual DbSet<SeriesAGMeatExportsFAO> SeriesAGMeatExportsFAO { get; set; }
        protected virtual DbSet<SeriesAGMeatIm> SeriesAGMeatIm { get; set; }
        //jrs 2025/04/29 used to incorrectly point to SeriesAGPoultryMeatProductionFAO
        protected virtual DbSet<SeriesAGMeatEx> SeriesAGMeatEx { get; set; }

        //CROP and MEAT TOTAL DOMESTIC SUPPLY and NORMALIZE PRODUCTION, EXPORTS, and IMPORTS
        protected virtual DbSet<SeriesAGCropDomesticSupplyFAO> SeriesAGCropDomesticSupplyFAO { get; set; }
        protected virtual DbSet<SeriesAGMeatDomesticSupplyFAO> SeriesAGMeatDomesticSupplyFAO { get; set; }

        //TRADE
        protected virtual DbSet<SeriesAGCropImportQuantityFAOTrade> SeriesAGCropImportQuantityFAOTrade { get; set; }
        protected virtual DbSet<SeriesAGCropExportQuantityFAOTrade> SeriesAGCropExportQuantityFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgCropImportValueGDPFAOTrade> SeriesAgCropImportValueGDPFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgCropExportValueGDPFAOTrade> SeriesAgCropExportValueGDPFAOTrade { get; set; }
        protected virtual DbSet<SeriesAGMeatImportQuantityFAOTrade> SeriesAGMeatImportQuantityFAOTrade { get; set; }
        protected virtual DbSet<SeriesAGMeatExportQuantityFAOTrade> SeriesAGMeatExportQuantityFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgMeatImportValueGDPFAOTrade> SeriesAgMeatImportValueGDPFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgMeatExportValueGDPFAOTrade> SeriesAgMeatExportValueGDPFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgFishImportQuantityFAOTrade> SeriesAgFishImportQuantityFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgFishExportQuantityFAOTrade> SeriesAgFishExportQuantityFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgFishImportValueGDPFAOTrade> SeriesAgFishImportValueGDPFAOTrade { get; set; }
        protected virtual DbSet<SeriesAgFishExportValueGDPFAOTrade> SeriesAgFishExportValueGDPFAOTrade { get; set; }

        //WATER RESOURCES
        protected virtual DbSet<SeriesWaterAnRenResources> SeriesWaterAnRenResources { get; set; }
        protected virtual DbSet<SeriesWaterAnRenResourcesOld> SeriesWaterAnRenResourcesOld { get; set; }
        protected virtual DbSet<SeriesWaterAnWithdrawals> SeriesWaterAnWithdrawals { get; set; }

    }
}
