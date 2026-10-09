using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesGDPCurDol")]
    internal partial class SeriesGDPCurDol : Series
    { }

    [Table("SeriesEnProdOil")]
    internal partial class SeriesEnProdOil : Series
    { }

    [Table("SeriesEnProdOilBP")]
    internal partial class SeriesEnProdOilBP : Series
    { }

    [Table("SeriesEnProdGas")]
    internal partial class SeriesEnProdGas : Series
    { }

    [Table("SeriesEnProdGasBP")]
    internal partial class SeriesEnProdGasBP : Series
    { }

    [Table("SeriesEnProdCoal")]
    internal partial class SeriesEnProdCoal : Series
    { }

    [Table("SeriesEnProdCoalBP")]
    internal partial class SeriesEnProdCoalBP : Series
    { }

    [Table("SeriesEnConElec")]
    internal partial class SeriesEnConElec : Series
    { }

    [Table("SeriesEnProdHydroIEA")]
    internal partial class SeriesEnProdHydroIEA : Series
    { }

    [Table("SeriesEnConHydroBP")]
    internal partial class SeriesEnConHydroBP : Series
    { }

    [Table("SeriesEnProdNuclearIEA")]
    internal partial class SeriesEnProdNuclearIEA : Series
    { }

    [Table("SeriesEnConNucBP")]
    internal partial class SeriesEnConNucBP : Series
    { }

    [Table("SeriesEnProdGeoTherm")]
    internal partial class SeriesEnProdGeoTherm : Series
    { }

    [Table("SeriesEnProdSolar")]
    internal partial class SeriesEnProdSolar : Series
    { }

    [Table("SeriesEnProdTideWave")]
    internal partial class SeriesEnProdTideWave : Series
    { }

    [Table("SeriesEnConPhoto")]
    internal partial class SeriesEnConPhoto : Series
    { }

    [Table("SeriesEnConWind")]
    internal partial class SeriesEnConWind : Series
    { }

    [Table("SeriesEnExportsTotalIEA")]
    internal partial class SeriesEnExportsTotalIEA : Series
    { }

    [Table("SeriesEnExportsCoalIEA")]
    internal partial class SeriesEnExportsCoalIEA : Series
    { }

    [Table("SeriesEnExportsPeatIEA")]
    internal partial class SeriesEnExportsPeatIEA : Series
    { }

    [Table("SeriesEnExportsOilIEA")]
    internal partial class SeriesEnExportsOilIEA : Series
    { }

    [Table("SeriesEnExportsOilProductsIEA")]
    internal partial class SeriesEnExportsOilProductsIEA : Series
    { }

    [Table("SeriesEnExportsNatGasIEA")]
    internal partial class SeriesEnExportsNatGasIEA : Series
    { }

    [Table("SeriesEnImportsTotalIEA")]
    internal partial class SeriesEnImportsTotalIEA : Series
    { }

    [Table("SeriesEnImportsCoalIEA")]
    internal partial class SeriesEnImportsCoalIEA : Series
    { }

    [Table("SeriesEnImportsPeatIEA")]
    internal partial class SeriesEnImportsPeatIEA : Series
    { }

    [Table("SeriesEnImportsOilIEA")]
    internal partial class SeriesEnImportsOilIEA : Series
    { }

    [Table("SeriesEnImportsOilProductsIEA")]
    internal partial class SeriesEnImportsOilProductsIEA : Series
    { }

    [Table("SeriesEnImportsNatGasIEA")]
    internal partial class SeriesEnImportsNatGasIEA : Series
    { }

    [Table("SeriesEnProdGeothermIEA")]
    internal partial class SeriesEnProdGeothermIEA : Series
    { }

    [Table("SeriesEnProdSolarPhotoIEA")]
    internal partial class SeriesEnProdSolarPhotoIEA : Series
    { }

    [Table("SeriesEnProdSolarThermIEA")]
    internal partial class SeriesEnProdSolarThermIEA : Series
    { }

    [Table("SeriesEnProdTideWaveOceanIEA")]
    internal partial class SeriesEnProdTideWaveOceanIEA : Series
    { }

    [Table("SeriesEnProdWindIEA")]
    internal partial class SeriesEnProdWindIEA : Series
    { }

    [Table("SeriesEnProdBiodieselIEA")]
    internal partial class SeriesEnProdBiodieselIEA : Series
    { }

    [Table("SeriesEnProdBiogasIEA")]
    internal partial class SeriesEnProdBiogasIEA : Series
    { }

    [Table("SeriesEnProdCoalIEA")]
    internal partial class SeriesEnProdCoalIEA : Series
    { }

    [Table("SeriesEnProdOilIEA")]
    internal partial class SeriesEnProdOilIEA : Series
    { }

    [Table("SeriesEnProdNatGasIEA")]
    internal partial class SeriesEnProdNatGasIEA : Series
    { }

    //Reserve Data
    [Table("SeriesEnReserOil")]
    internal partial class SeriesEnReserOil : Series
    { }

    [Table("SeriesEnReserOilBP")]
    internal partial class SeriesEnReserOilBP : Series
    { }

    [Table("SeriesEnReserGas")]
    internal partial class SeriesEnReserGas : Series
    { }

    [Table("SeriesEnReserGasBP")]
    internal partial class SeriesEnReserGasBP : Series
    { }

    [Table("SeriesEnReserCoalBP")]
    internal partial class SeriesEnReserCoalBP : Series
    { }

    [Table("SeriesEnReserHyd")]
    internal partial class SeriesEnReserHyd : Series
    { }

    //mti 2024/08/29 start    

    [Table("SeriesEnPotentialHydro")]
    internal partial class SeriesEnPotentialHydro : Series
    { }

    [Table("SeriesEnPotentialWind")]
    internal partial class SeriesEnPotentialWind : Series
    { }

    [Table("SeriesEnPotentialSolarLandConstraints")]
    internal partial class SeriesEnPotentialSolar : Series
    { }

    [Table("SeriesEnPotentialGeothermalSustainable")]
    internal partial class SeriesEnPotentialGeothermal : Series
    { }

    //mti 2024/08/29 end

    [Table("SeriesEnReserOilBGR")]
    internal partial class SeriesEnReserOilBGR : Series
    { }

    [Table("SeriesEnReserGasBGR")]
    internal partial class SeriesEnReserGasBGR : Series
    { }

    //   Resources.  No data before most recent. Start there; adjust for reserves; add
    //   cumulative production for historical set up
    //  -----------------------------------------------------------------------------

    [Table("SeriesEnResorOilUSGS")]
    internal partial class SeriesEnResorOilUSGS : Series
    { }

    [Table("SeriesEnResorNGLUSGS")]
    internal partial class SeriesEnResorNGLUSGS : Series
    { }

    [Table("SeriesEnResorGasUSGS")]
    internal partial class SeriesEnResorGasUSGS : Series
    { }

    [Table("SeriesEnResorCoalBGRBBOE")]
    internal partial class SeriesEnResorCoalBGRBBOE : Series
    { }

    [Table("SeriesEnResorOilSandsBGR")]
    internal partial class SeriesEnResorOilSandsBGR : Series
    { }

    [Table("SeriesEnReserOilSandsBGR")]
    internal partial class SeriesEnReserOilSandsBGR : Series
    { }

    [Table("SeriesEnReserShaleOilBGR")]
    internal partial class SeriesEnReserShaleOilBGR : Series
    { }

    [Table("SeriesEnResorShaleOilBGR")]
    internal partial class SeriesEnResorShaleOilBGR : Series
    { }

    [Table("SeriesEnReserHeavyOilBGR")]
    internal partial class SeriesEnReserHeavyOilBGR : Series
    { }

    [Table("SeriesEnResorHeavyOilBGR")]
    internal partial class SeriesEnResorHeavyOilBGR : Series
    { }

    [Table("SeriesEnResorOilBGR")]
    internal partial class SeriesEnResorOilBGR : Series
    { }

    [Table("SeriesEnResorShaleGasBGR")]
    internal partial class SeriesEnResorShaleGasBGR : Series
    { }

    [Table("SeriesEnReserShaleGasBGR")]
    internal partial class SeriesEnReserShaleGasBGR : Series
    { }

    [Table("SeriesEnResorCBMBGR")]
    internal partial class SeriesEnResorCBMBGR : Series
    { }

    [Table("SeriesEnReserCBMBGR")]
    internal partial class SeriesEnReserCBMBGR : Series
    { }

    [Table("SeriesEnResorTightGasBGR")]
    internal partial class SeriesEnResorTightGasBGR : Series
    { }

    [Table("SeriesEnResorGasBGR")]
    internal partial class SeriesEnResorGasBGR : Series
    { }

    //jrs 2023/11/10
    [Table("SeriesFinalEnergyDemandIMAGELIFETP")]
    internal partial class SeriesFinalEnergyDemandIMAGELIFETP : Series
    { }
    [Table("SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGELIFETP")]
    internal partial class SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGELIFETP : Series
    { }
    [Table("SeriesEnergyProdByTypeTotalIMAGELIFETP")]
    internal partial class SeriesEnergyProdByTypeTotalIMAGELIFETP : Series
    { }
    [Table("SeriesFinalEnergyDemandIMAGETECHTP")]
    internal partial class SeriesFinalEnergyDemandIMAGETECHTP : Series
    { }
    [Table("SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGETECHTP")]
    internal partial class SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGETECHTP : Series
    { }
    [Table("SeriesEnergyProdByTypeTotalIMAGETECHTP")]
    internal partial class SeriesEnergyProdByTypeTotalIMAGETECHTP : Series
    { }
    //jrs 2023/11/30
    [Table("SeriesFinalEnergyDemandIMAGEREFTP")]
    internal partial class SeriesFinalEnergyDemandIMAGEREFTP : Series
    { }
    [Table("SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREFTP")]
    internal partial class SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREFTP : Series
    { }
    [Table("SeriesEnergyProdByTypeTotalIMAGEREFTP")]
    internal partial class SeriesEnergyProdByTypeTotalIMAGEREFTP : Series
    { }

    [Table("SeriesFinalEnergyDemandIMAGEREF1P5C")]
    internal partial class SeriesFinalEnergyDemandIMAGEREF1P5C : Series
    { }
    [Table("SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREF1P5C")]
    internal partial class SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREF1P5C : Series
    { }
    [Table("SeriesEnergyProdByTypeTotalIMAGEREF1P5C")]
    internal partial class SeriesEnergyProdByTypeTotalIMAGEREF1P5C : Series
    { }
    //jrs 2024/02/19
    [Table("SeriesExoENDEM")]
    internal partial class SeriesExoENDEM : Series
    { }
    [Table("SeriesExoENPOTHRENEW")]
    internal partial class SeriesExoENPOTHRENEW : Series
    { }
    [Table("SeriesExoENPTOTAL")]
    internal partial class SeriesExoENPTOTAL : Series
    { }


    internal partial class IFsHistSeriesContext
    {
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoENDEM> SeriesExoENDEM { get; set; }

        protected virtual DbSet<SeriesExoENPOTHRENEW> SeriesExoENPOTHRENEW { get; set; }
        protected virtual DbSet<SeriesExoENPTOTAL> SeriesExoENPTOTAL { get; set; }
        //jrs 2023/10/11
        protected virtual DbSet<SeriesFinalEnergyDemandIMAGELIFETP> SeriesFinalEnergyDemandIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGELIFETP> SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesEnergyProdByTypeTotalIMAGELIFETP> SeriesEnergyProdByTypeTotalIMAGELIFETP { get; set; }

        protected virtual DbSet<SeriesFinalEnergyDemandIMAGETECHTP> SeriesFinalEnergyDemandIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGETECHTP> SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesEnergyProdByTypeTotalIMAGETECHTP> SeriesEnergyProdByTypeTotalIMAGETECHTP { get; set; }
        //jrs 2023/11/30
        protected virtual DbSet<SeriesFinalEnergyDemandIMAGEREFTP> SeriesFinalEnergyDemandIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREFTP> SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesEnergyProdByTypeTotalIMAGEREFTP> SeriesEnergyProdByTypeTotalIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesFinalEnergyDemandIMAGEREF1P5C> SeriesFinalEnergyDemandIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREF1P5C> SeriesPrimaryEnergyNon_BiomassEnergyOthRenewIMAGEREF1P5C { get; set; }
        protected virtual DbSet<SeriesEnergyProdByTypeTotalIMAGEREF1P5C> SeriesEnergyProdByTypeTotalIMAGEREF1P5C { get; set; }



        protected virtual DbSet<SeriesGDPCurDol> GDPRPA { get; set; }
        protected virtual DbSet<SeriesEnProdOil> ENPOIL { get; set; }
        protected virtual DbSet<SeriesEnProdOilBP> ENPOILBP { get; set; }
        protected virtual DbSet<SeriesEnProdGas> ENPGAS { get; set; }
        protected virtual DbSet<SeriesEnProdGasBP> ENPGASBP { get; set; }
        protected virtual DbSet<SeriesEnProdCoal> ENPCOAL { get; set; }
        protected virtual DbSet<SeriesEnProdCoalBP> ENPCOALBP { get; set; }
        protected virtual DbSet<SeriesEnConElec> ENPELEC { get; set; }

        //mti 2015/10/01 - comment
        //the consumption table changed to the following
        //however, this is now same as the IEA production table opened later by JRS - need to commenting it out and revise code accordingly (MTI)
        protected virtual DbSet<SeriesEnProdHydroIEA> ENPHYD { get; set; }
        protected virtual DbSet<SeriesEnConHydroBP> ENPHYDBP { get; set; }
        protected virtual DbSet<SeriesEnProdNuclearIEA> ENPNUC { get; set; }
        protected virtual DbSet<SeriesEnConNucBP> ENPNUCBP { get; set; }
        protected virtual DbSet<SeriesEnProdGeoTherm> ENPGEO { get; set; }
        protected virtual DbSet<SeriesEnProdSolar> ENPSOLAR { get; set; }
        protected virtual DbSet<SeriesEnProdTideWave> ENPTIDEWAVE { get; set; }
        protected virtual DbSet<SeriesEnConPhoto> ENPPHOTO { get; set; }
        protected virtual DbSet<SeriesEnConWind> ENPWIND { get; set; }
        protected virtual DbSet<SeriesEnExportsTotalIEA> ENX { get; set; }
        protected virtual DbSet<SeriesEnExportsCoalIEA> ENXCOALIEA { get; set; }
        protected virtual DbSet<SeriesEnExportsPeatIEA> ENXPEATIEA { get; set; }
        protected virtual DbSet<SeriesEnExportsOilIEA> ENXOILIEA { get; set; }
        protected virtual DbSet<SeriesEnExportsOilProductsIEA> ENXOILPRIEA { get; set; }
        protected virtual DbSet<SeriesEnExportsNatGasIEA> ENXNATGASIEA { get; set; }
        protected virtual DbSet<SeriesEnImportsTotalIEA> ENM { get; set; }
        protected virtual DbSet<SeriesEnImportsCoalIEA> ENMCOAL { get; set; }
        protected virtual DbSet<SeriesEnImportsPeatIEA> ENMPEAT { get; set; }
        protected virtual DbSet<SeriesEnImportsOilIEA> ENMOIL { get; set; }
        protected virtual DbSet<SeriesEnImportsOilProductsIEA> ENMOILPROD { get; set; }
        protected virtual DbSet<SeriesEnImportsNatGasIEA> ENMNATGAS { get; set; }
        protected virtual DbSet<SeriesEnProdGeothermIEA> ENPGEOIEA { get; set; }
        protected virtual DbSet<SeriesEnProdSolarPhotoIEA> ENPPHOTOIEA { get; set; }
        protected virtual DbSet<SeriesEnProdSolarThermIEA> ENPSOLARIEA { get; set; }
        protected virtual DbSet<SeriesEnProdTideWaveOceanIEA> ENPTIDEWAVEIEA { get; set; }
        protected virtual DbSet<SeriesEnProdWindIEA> ENPWINDIEA { get; set; }
        protected virtual DbSet<SeriesEnProdBiodieselIEA> ENPBIODIESELIEA { get; set; }
        protected virtual DbSet<SeriesEnProdBiogasIEA> ENPBIOGASIEA { get; set; }
        protected virtual DbSet<SeriesEnProdCoalIEA> ENPCOALIEA { get; set; }
        protected virtual DbSet<SeriesEnProdOilIEA> ENPOILIEA { get; set; }
        protected virtual DbSet<SeriesEnProdNatGasIEA> ENPGASIEA { get; set; }

        protected virtual DbSet<SeriesEnReserOil> RESEROIL { get; set; }
        protected virtual DbSet<SeriesEnReserOilBP> RESEROILBP { get; set; }
        protected virtual DbSet<SeriesEnReserGas> RESERGAS { get; set; }
        protected virtual DbSet<SeriesEnReserGasBP> RESERGASBP { get; set; }
        protected virtual DbSet<SeriesEnReserCoalBP> RESERCOALBP { get; set; }
        protected virtual DbSet<SeriesEnReserHyd> RESERHYD { get; set; }

        //mti 2024/08/29 start  
        protected virtual DbSet<SeriesEnPotentialHydro> POTENTIALHYDRO { get; set; }
        protected virtual DbSet<SeriesEnPotentialWind> POTENTIALWIND { get; set; }
        protected virtual DbSet<SeriesEnPotentialSolar> POTENTIALSOLAR { get; set; }
        protected virtual DbSet<SeriesEnPotentialGeothermal> POTENTIALGEO { get; set; }

        //mti 2024/08/29 end
        protected virtual DbSet<SeriesEnReserOilBGR> RESEROILBGR { get; set; }
        protected virtual DbSet<SeriesEnReserGasBGR> RESERGASBGR { get; set; }

        protected virtual DbSet<SeriesEnResorOilUSGS> RESOROIL { get; set; }
        protected virtual DbSet<SeriesEnResorNGLUSGS> RESORNGL { get; set; }
        protected virtual DbSet<SeriesEnResorGasUSGS> RESORGAS { get; set; }
        protected virtual DbSet<SeriesEnResorCoalBGRBBOE> RESORCOAL { get; set; }
        protected virtual DbSet<SeriesEnResorOilSandsBGR> RESOROILSANDSBGR { get; set; }
        protected virtual DbSet<SeriesEnReserOilSandsBGR> RESEROILSANDSBGR { get; set; }
        protected virtual DbSet<SeriesEnReserShaleOilBGR> RESERSHALEOIL { get; set; }
        protected virtual DbSet<SeriesEnResorShaleOilBGR> RESORSHALEOIL { get; set; }
        protected virtual DbSet<SeriesEnReserHeavyOilBGR> RESERHEAVYOIL { get; set; }
        protected virtual DbSet<SeriesEnResorHeavyOilBGR> RESORHEAVYOIL { get; set; }
        protected virtual DbSet<SeriesEnResorOilBGR> RESOROILBGR { get; set; }
        protected virtual DbSet<SeriesEnResorShaleGasBGR> RESORSHALEGAS { get; set; }
        protected virtual DbSet<SeriesEnReserShaleGasBGR> RESERSHALEGAS { get; set; }
        protected virtual DbSet<SeriesEnResorCBMBGR> RESORCBM { get; set; }
        protected virtual DbSet<SeriesEnReserCBMBGR> RESERCBM { get; set; }
        protected virtual DbSet<SeriesEnResorTightGasBGR> RESORTIGHTGAS { get; set; }
        protected virtual DbSet<SeriesEnResorGasBGR> RESORGASBGR { get; set; }
        
    }

}