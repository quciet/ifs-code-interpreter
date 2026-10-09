using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesEnvPMWDI")]
    internal partial class SeriesEnvPMWDI : Series
    { }
    
    [Table("SeriesEmissionsCarbonCDIAC")]
    internal partial class SeriesEmissionsCarbonCDIAC : Series
    { }

    [Table("SeriesEnvSolidFuels")]
    internal partial class SeriesEnvSolidFuels : Series
    { }

    [Table("SeriesEnvSolidFuelsImproved")]
    internal partial class SeriesEnvSolidFuelsImproved : Series
    { }

    [Table("SeriesEnvPrecipitation")]
    internal partial class SeriesEnvPrecipitation : Series
    { }

    [Table("SeriesEnvAvgAnnTempChg")]
    internal partial class SeriesEnvAvgAnnTempChg : Series
    { }

    [Table("SeriesEnvAvgAnnTemp")]
    internal partial class SeriesEnvAvgAnnTemp : Series
    { }

    [Table("SeriesEnvPrecipitationChg")]
    internal partial class SeriesEnvPrecipitationChg : Series
    { }

    //jrs 2022/06/08 
    [Table("SeriesForecastEnvWTempLandMeanRCP26")]
    internal partial class SeriesForecastEnvWTempLandMeanRCP26 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMeanRCP45")]
    internal partial class SeriesForecastEnvWTempLandMeanRCP45 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMeanRCP60")]
    internal partial class SeriesForecastEnvWTempLandMeanRCP60 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMeanRCP85")]
    internal partial class SeriesForecastEnvWTempLandMeanRCP85 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMedianRCP26")]
    internal partial class SeriesForecastEnvWTempLandMedianRCP26 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMedianRCP45")]
    internal partial class SeriesForecastEnvWTempLandMedianRCP45 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMedianRCP60")]
    internal partial class SeriesForecastEnvWTempLandMedianRCP60 : Series
    { }

    [Table("SeriesForecastEnvWTempLandMedianRCP85")]
    internal partial class SeriesForecastEnvWTempLandMedianRCP85 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP10RCP26")]
    internal partial class SeriesForecastEnvWTempLandP10RCP26 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP10RCP45")]
    internal partial class SeriesForecastEnvWTempLandP10RCP45 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP10RCP60")]
    internal partial class SeriesForecastEnvWTempLandP10RCP60 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP10RCP85")]
    internal partial class SeriesForecastEnvWTempLandP10RCP85 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP90RCP26")]
    internal partial class SeriesForecastEnvWTempLandP90RCP26 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP90RCP45")]
    internal partial class SeriesForecastEnvWTempLandP90RCP45 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP90RCP60")]
    internal partial class SeriesForecastEnvWTempLandP90RCP60 : Series
    { }

    [Table("SeriesForecastEnvWTempLandP90RCP85")]
    internal partial class SeriesForecastEnvWTempLandP90RCP85 : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP26")]
    internal partial class SeriesForecastEnvTempChgMedianRCP26 : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP26MA")]
    internal partial class SeriesForecastEnvTempChgMedianRCP26MA : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP45")]
    internal partial class SeriesForecastEnvTempChgMedianRCP45 : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP45MA")]
    internal partial class SeriesForecastEnvTempChgMedianRCP45MA : Series
    { }
    
    [Table("SeriesForecastEnvTempChgMedianRCP60")]
    internal partial class SeriesForecastEnvTempChgMedianRCP60 : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP60MA")]
    internal partial class SeriesForecastEnvTempChgMedianRCP60MA : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP85")]
    internal partial class SeriesForecastEnvTempChgMedianRCP85 : Series
    { }

    [Table("SeriesForecastEnvTempChgMedianRCP85MA")]
    internal partial class SeriesForecastEnvTempChgMedianRCP85MA : Series
    { }

    [Table("SeriesForecastEnvTempChgP10RCP26")]
    internal partial class SeriesForecastEnvTempChgP10RCP26 : Series
    { }

    [Table("SeriesForecastEnvTempChgP10RCP45")]
    internal partial class SeriesForecastEnvTempChgP10RCP45 : Series
    { }
    [Table("SeriesForecastEnvTempChgP10RCP60")]
    internal partial class SeriesForecastEnvTempChgP10RCP60 : Series
    { }
    [Table("SeriesForecastEnvTempChgP10RCP85")]
    internal partial class SeriesForecastEnvTempChgP10RCP85 : Series
    { }
    [Table("SeriesForecastEnvTempChgP90RCP26")]
    internal partial class SeriesForecastEnvTempChgP90RCP26 : Series
    { }
    [Table("SeriesForecastEnvTempChgP90RCP45")]
    internal partial class SeriesForecastEnvTempChgP90RCP45 : Series
    { }
    [Table("SeriesForecastEnvTempChgP90RCP60")]
    internal partial class SeriesForecastEnvTempChgP90RCP60 : Series
    { }
    [Table("SeriesForecastEnvTempChgP90RCP85")]
    internal partial class SeriesForecastEnvTempChgP90RCP85 : Series
    { }
    [Table("SeriesForecastEnvTempMedianRCP45")]
    internal partial class SeriesForecastEnvTempMedianRCP45 : Series
    { }
    //jrs 2022/10/17
    [Table("SeriesForecastEnvPrecipitationChgMedianRCP26")]
    internal partial class SeriesForecastEnvPrecipitationChgMedianRCP26 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgMedianRCP45")]
    internal partial class SeriesForecastEnvPrecipitationChgMedianRCP45: Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgMedianRCP60")]
    internal partial class SeriesForecastEnvPrecipitationChgMedianRCP60: Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgMedianRCP85")]
    internal partial class SeriesForecastEnvPrecipitationChgMedianRCP85 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP10RCP26")]
    internal partial class SeriesForecastEnvPrecipitationChgP10RCP26 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP10RCP45")]
    internal partial class SeriesForecastEnvPrecipitationChgP10RCP45 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP10RCP60")]
    internal partial class SeriesForecastEnvPrecipitationChgP10RCP60 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP10RCP85")]
    internal partial class SeriesForecastEnvPrecipitationChgP10RCP85 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP90RCP26")]
    internal partial class SeriesForecastEnvPrecipitationChgP90RCP26 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP90RCP45")]
    internal partial class SeriesForecastEnvPrecipitationChgP90RCP45 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP90RCP60")]
    internal partial class SeriesForecastEnvPrecipitationChgP90RCP60 : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgP90RCP85")]
    internal partial class SeriesForecastEnvPrecipitationChgP90RCP85 : Series
    { }
    //jrs 2023/05/19
    [Table("SeriesForecastEnvPrecipitationChgFLATunw")]
    internal partial class SeriesForecastEnvPrecipitationChgFLATunw : Series
    { }
    [Table("SeriesForecastEnvPrecipitationChgMedianRCP60unw")]
    internal partial class SeriesForecastEnvPrecipitationChgMedianRCP60unw : Series
    { }
    //jrs 2023/11/10
    [Table("SeriesEmissionsCarbonIMAGELIFETP")]
    internal partial class SeriesEmissionsCarbonIMAGELIFETP : Series
    { }

    [Table("SeriesEmissionsCarbonIMAGETECHTP")]
    internal partial class SeriesEmissionsCarbonIMAGETECHTP : Series
    { }
    //jrs 2023/11/30
    [Table("SeriesEmissionsCarbonIMAGEREFTP")]
    internal partial class SeriesEmissionsCarbonIMAGEREFTP : Series
    { }

    [Table("SeriesEmissionsCarbonIMAGEREF1P5C")]
    internal partial class SeriesEmissionsCarbonIMAGEREF1P5C : Series
    { }
    //jrs 2023/12/06
    [Table("SeriesCookstovesIMAGELIFETP")]
    internal partial class SeriesCookstovesIMAGELIFETP : Series
    { }
    [Table("SeriesCookstovesIMAGETECHTP")]
    internal partial class SeriesCookstovesIMAGETECHTP : Series
    { }
    [Table("SeriesCookstovesIMAGEREF")]
    internal partial class SeriesCookstovesIMAGEREF : Series
    { }
    [Table("SeriesCookstovesIMAGE1P5C")]
    internal partial class SeriesCookstovesIMAGE1P5C : Series
    { }
    //jrs 2024/02/19
    [Table("SeriesExoENVTPCHG")]
    internal partial class SeriesExoENVTPCHG : Series
    { }
    [Table("SeriesExoENVPRCHG")]
    internal partial class SeriesExoENVPRCHG : Series
    { }
    [Table("SeriesExoCARANN")]
    internal partial class SeriesExoCARANN : Series
    { }
    [Table("SeriesExoCOOKSTOVESTRADITIONAL")]
    internal partial class SeriesExoCOOKSTOVESTRADITIONAL : Series
    { }


    internal partial class IFsHistSeriesContext
    {
        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoENVTPCHG> SeriesExoENVTPCHG { get; set; }
        protected virtual DbSet<SeriesExoENVPRCHG> SeriesExoENVPRCHG { get; set; }
        protected virtual DbSet<SeriesExoCARANN> SeriesExoCARANN { get; set; }
        protected virtual DbSet<SeriesExoCOOKSTOVESTRADITIONAL> SeriesExoCOOKSTOVESTRADITIONAL { get; set; }

        //jrs 2023/11/10
        protected virtual DbSet<SeriesEmissionsCarbonIMAGELIFETP> SeriesEmissionsCarbonIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesEmissionsCarbonIMAGETECHTP> SeriesEmissionsCarbonIMAGETECHTP { get; set; }
        //jrs 2023/11/30
        protected virtual DbSet<SeriesEmissionsCarbonIMAGEREFTP> SeriesEmissionsCarbonIMAGEREFTP { get; set; }
        protected virtual DbSet<SeriesEmissionsCarbonIMAGEREF1P5C> SeriesEmissionsCarbonIMAGEREF1P5C { get; set; }
        //jrs 2023/12/06
        protected virtual DbSet<SeriesCookstovesIMAGELIFETP> SeriesCookstovesIMAGELIFETP { get; set; }
        protected virtual DbSet<SeriesCookstovesIMAGETECHTP> SeriesCookstovesIMAGETECHTP { get; set; }
        protected virtual DbSet<SeriesCookstovesIMAGEREF> SeriesCookstovesIMAGEREF { get; set; }
        protected virtual DbSet<SeriesCookstovesIMAGE1P5C> SeriesCookstovesIMAGE1P5C { get; set; }

        protected virtual DbSet<SeriesForecastEnvPrecipitationChgMedianRCP60unw> SeriesForecastEnvPrecipitationChgMedianRCP60unw { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgFLATunw> SeriesForecastEnvPrecipitationChgFLATunw { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP90RCP26> SeriesForecastEnvPrecipitationChgP90RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP90RCP45> SeriesForecastEnvPrecipitationChgP90RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP90RCP60> SeriesForecastEnvPrecipitationChgP90RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP90RCP85> SeriesForecastEnvPrecipitationChgP90RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP10RCP26> SeriesForecastEnvPrecipitationChgP10RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP10RCP45> SeriesForecastEnvPrecipitationChgP10RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP10RCP60> SeriesForecastEnvPrecipitationChgP10RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgP10RCP85> SeriesForecastEnvPrecipitationChgP10RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgMedianRCP26> SeriesForecastEnvPrecipitationChgMedianRCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgMedianRCP45> SeriesForecastEnvPrecipitationChgMedianRCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgMedianRCP60> SeriesForecastEnvPrecipitationChgMedianRCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvPrecipitationChgMedianRCP85> SeriesForecastEnvPrecipitationChgMedianRCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempMedianRCP45> SeriesForecastEnvTempMedianRCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP90RCP26> SeriesForecastEnvTempChgP90RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP90RCP45> SeriesForecastEnvTempChgP90RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP90RCP60> SeriesForecastEnvTempChgP90RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP90RCP85> SeriesForecastEnvTempChgP90RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP10RCP26> SeriesForecastEnvTempChgP10RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP10RCP45> SeriesForecastEnvTempChgP10RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP10RCP60> SeriesForecastEnvTempChgP10RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgP10RCP85> SeriesForecastEnvTempChgP10RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP26> SeriesForecastEnvTempChgMedianRCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP26MA> SeriesForecastEnvTempChgMedianRCP26MA { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP45> SeriesForecastEnvTempChgMedianRCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP45MA> SeriesForecastEnvTempChgMedianRCP45MA { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP60> SeriesForecastEnvTempChgMedianRCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP60MA> SeriesForecastEnvTempChgMedianRCP60MA { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP85> SeriesForecastEnvTempChgMedianRCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvTempChgMedianRCP85MA> SeriesForecastEnvTempChgMedianRCP85MA { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP90RCP26> SeriesForecastEnvWTempLandP90RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP90RCP45> SeriesForecastEnvWTempLandP90RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP90RCP60> SeriesForecastEnvWTempLandP90RCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP90RCP85> SeriesForecastEnvWTempLandP90RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP10RCP26> SeriesForecastEnvWTempLandP10RCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP10RCP45> SeriesForecastEnvWTempLandP10RCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandP10RCP60> SeriesForecastEnvWTempLandP10RCP60 { get; set; }

        protected virtual DbSet<SeriesForecastEnvWTempLandP10RCP85> SeriesForecastEnvWTempLandP10RCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMedianRCP26> SeriesForecastEnvWTempLandMedianRCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMedianRCP45> SeriesForecastEnvWTempLandMedianRCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMedianRCP60> SeriesForecastEnvWTempLandMedianRCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMedianRCP85> SeriesForecastEnvWTempLandMedianRCP85 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMeanRCP26> SeriesForecastEnvWTempLandMeanRCP26 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMeanRCP45> SeriesForecastEnvWTempLandMeanRCP45 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMeanRCP60> SeriesForecastEnvWTempLandMeanRCP60 { get; set; }
        protected virtual DbSet<SeriesForecastEnvWTempLandMeanRCP85> SeriesForecastEnvWTempLandMeanRCP85 { get; set; }
        protected virtual DbSet<SeriesEnvPMWDI> SeriesEnvPMWDI { get; set; }
        protected virtual DbSet<SeriesEmissionsCarbonCDIAC> SeriesEmissionsCarbonCDIAC { get; set; }
        protected virtual DbSet<SeriesEnvSolidFuels> SeriesEnvSolidFuels { get; set; }
        protected virtual DbSet<SeriesEnvSolidFuelsImproved> SeriesEnvSolidFuelsImproved { get; set; }
        protected virtual DbSet<SeriesEnvPrecipitation> SeriesEnvPrecipitation { get; set; }
        protected virtual DbSet<SeriesEnvAvgAnnTempChg> SeriesEnvAvgAnnTempChg { get; set; }
        protected virtual DbSet<SeriesEnvAvgAnnTemp> SeriesEnvAvgAnnTemp { get; set; }
        protected virtual DbSet<SeriesEnvPrecipitationChg> SeriesEnvPrecipitationChg { get; set; }        
    }

}