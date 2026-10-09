using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{   [Table("SeriesSFPITFEthnicWarEv")]
    internal partial class SeriesSFPITFEthnicWarEv : Series
    { }
    [Table("SeriesSFPITFInternalWarEv")]
    internal partial class SeriesSFPITFInternalWarEv : Series
    { }
    [Table("SeriesSFPITFGenocideMag")]
    internal partial class SeriesSFPITFGenocideMag : Series
    { }
    [Table("SeriesSFPITFGenocideEv")]
    internal partial class SeriesSFPITFGenocideEv : Series
    { }
    [Table("SeriesSFPITFRegTranMag")]
    internal partial class SeriesSFPITFRegTranMag : Series
    { }
    [Table("SeriesSFPITFRegTranEv")]
    internal partial class SeriesSFPITFRegTranEv : Series
    { }
    [Table("SeriesSFPITFRevolWarMag")]
    internal partial class SeriesSFPITFRevolWarMag : Series
    { }
    [Table("SeriesSFPITFRevolWarEv")]
    internal partial class SeriesSFPITFRevolWarEv : Series
    { }
    [Table("SeriesSFPITFEthnicWarMag")]
    internal partial class SeriesSFPITFEthnicWarMag : Series
    { }
    [Table("SeriesSFPITFConsolidatedMag")]
    internal partial class SeriesSFPITFConsolidatedMag : Series
    { }
    [Table("SeriesSFPITFConsolidatedEv")]
    internal partial class SeriesSFPITFConsolidatedEv : Series
    { }
    [Table("SeriesSFPITFInternalWarEvProb")]
    internal partial class SeriesSFPITFInternalWarEvProb : Series
    { }
    [Table("SeriesSFPITFDomInstabilityEvProb")]
    internal partial class SeriesSFPITFDomInstabilityEvProb : Series
    { }
    [Table("SeriesSFPITFConsolidatedEvProb")]
    internal partial class SeriesSFPITFConsolidatedEvProb : Series
    { }
    [Table("SeriesSFPITFInternalWarY1Prob")]
    internal partial class SeriesSFPITFInternalWarY1Prob : Series
    { }
    [Table("SeriesSFPITFDomInstabilityY1Prob")]
    internal partial class SeriesSFPITFDomInstabilityY1Prob : Series
    { }
    [Table("SeriesSFPITFConsolidatedY1Prob")]
    internal partial class SeriesSFPITFConsolidatedY1Prob : Series
    { }
    [Table("SeriesSFPITFInternalWarMagAvg")]
    internal partial class SeriesSFPITFInternalWarMagAvg : Series
    { }
    [Table("SeriesSFPITFDomInstabilityMagAvg")]
    internal partial class SeriesSFPITFDomInstabilityMagAvg : Series
    { }
    [Table("SeriesSFPITFConsolidatedMagAvg")]
    internal partial class SeriesSFPITFConsolidatedMagAvg : Series
    { }
    [Table("SeriesDiploEmbassyOut")]
    internal partial class SeriesDiploEmbassyOut : Series
    { }
    [Table("SeriesDiploIGOWeightedTotal")]
    internal partial class SeriesDiploIGOWeightedTotal : Series
    { }
    [Table("SeriesDiploTreatyWeightedSign1Rat2")]
    internal partial class SeriesDiploTreatyWeightedSign1Rat2 : Series
    { }
    [Table("SeriesDiploINGOProgrammatic")]
    internal partial class SeriesDiploINGOProgrammatic : Series
    { }
    [Table("SeriesDiploDistanceToEurope")]
    internal partial class SeriesDiploDistanceToEurope : Series
    { }
    [Table("SeriesHTCtpi")]
    internal partial class SeriesHTCtpi : Series
    { }
    [Table("SeriesHTCgri")]
    internal partial class SeriesHTCgri : Series
    { }
    [Table("SeriesDiploINGOAdvocacy")]
    internal partial class SeriesDiploINGOAdvocacy : Series
    { }
    [Table("SeriesDiploEmbassyMaxGlobal")]
    internal partial class SeriesDiploEmbassyMaxGlobal : Series
    { }
    [Table("SeriesDiploIGOWeightMax")]
    internal partial class SeriesDiploIGOWeightMax : Series
    { }
    [Table("SeriesDiploTreatyWeightedSign1Rat2Max")]
    internal partial class SeriesDiploTreatyWeightedSign1Rat2Max : Series
    { }
    //jrs 2024/02/19
    [Table("SeriesExoSFINTLWARALL")]
    internal partial class SeriesExoSFINTLWARALL : Series
    { }
    //jrs 2024/05/17
    [Table("SeriesSFUCDPConsolidatedEv")]
    internal partial class SeriesSFUCDPConsolidatedEv : Series
    { }
    //dsb 2025/12/17
    [Table("SeriesSFCivilWarPardeeImpPop")]
    internal partial class SeriesSFCivilWarPardeeImpPop : Series
    { }
    //dsb 2026/1/8
    [Table("SeriesSFCivilWarPardeeConflictYear")]
    internal partial class SeriesSFCivilWarPardeeConflictYear : Series
    { }
    //dsb 2026/05/05
    [Table("SeriesSFCivilWarPardeeImpPopCivilWar")]
    internal partial class SeriesSFCivilWarPardeeImpPopCivilWar : Series
    { }

    internal partial class IFsHistSeriesContext
    {
        //dsb 2026/05/05
        protected virtual DbSet<SeriesSFCivilWarPardeeImpPopCivilWar> SeriesSFCivilWarPardeeImpPopCivilWar { get; set; }
        //dsb 2026/1/8
        protected virtual DbSet<SeriesSFCivilWarPardeeConflictYear> SeriesSFCivilWarPardeeConflictYear { get; set; }
        //dsb 2025/12/17
        protected virtual DbSet<SeriesSFCivilWarPardeeImpPop> SeriesSFCivilWarPardeeImpPop { get; set; }
        //jrs 2024/05/17
        protected virtual DbSet<SeriesSFUCDPConsolidatedEv> SeriesSFUCDPConsolidatedEv { get; set; }

        //jrs 2024/02/19
        protected virtual DbSet<SeriesExoSFINTLWARALL>
        SeriesExoSFINTLWARALL
        { get; set; }
        //protected virtual DbSet<SeriesWHO2012WaterUnImpFiltered> SeriesWHO2012WaterUnImpFiltered { get; set; }
        protected virtual DbSet<SeriesSFPITFEthnicWarEv>
        SeriesSFPITFEthnicWarEv { get; set; }
        
        protected virtual DbSet<SeriesSFPITFInternalWarEv>
        SeriesSFPITFInternalWarEv { get; set; }
        
        protected virtual DbSet<SeriesSFPITFGenocideMag>
        SeriesSFPITFGenocideMag { get; set; }
        
        protected virtual DbSet<SeriesSFPITFGenocideEv>
        SeriesSFPITFGenocideEv { get; set; }
        
        protected virtual DbSet<SeriesSFPITFRegTranMag>
        SeriesSFPITFRegTranMag { get; set; }
        
        protected virtual DbSet<SeriesSFPITFRegTranEv>
        SeriesSFPITFRegTranEv { get; set; }
        
        protected virtual DbSet<SeriesSFPITFRevolWarMag>
        SeriesSFPITFRevolWarMag { get; set; }
        
        protected virtual DbSet<SeriesSFPITFRevolWarEv>
        SeriesSFPITFRevolWarEv { get; set; }
        
        protected virtual DbSet<SeriesSFPITFEthnicWarMag>
        SeriesSFPITFEthnicWarMag { get; set; }
        
        protected virtual DbSet<SeriesSFPITFConsolidatedMag>
        SeriesSFPITFConsolidatedMag { get; set; }
        
        protected virtual DbSet<SeriesSFPITFConsolidatedEv>
        SeriesSFPITFConsolidatedEv { get; set; }
        
        protected virtual DbSet<SeriesSFPITFInternalWarEvProb>
        SeriesSFPITFInternalWarEvProb { get; set; }
        
        protected virtual DbSet<SeriesSFPITFDomInstabilityEvProb>
        SeriesSFPITFDomInstabilityEvProb { get; set; }
        
        protected virtual DbSet<SeriesSFPITFConsolidatedEvProb>
        SeriesSFPITFConsolidatedEvProb { get; set; }
        
        protected virtual DbSet<SeriesSFPITFInternalWarY1Prob>
        SeriesSFPITFInternalWarY1Prob { get; set; }
        
        protected virtual DbSet<SeriesSFPITFDomInstabilityY1Prob>
        SeriesSFPITFDomInstabilityY1Prob { get; set; }
        
        protected virtual DbSet<SeriesSFPITFConsolidatedY1Prob>
        SeriesSFPITFConsolidatedY1Prob { get; set; }
        
        protected virtual DbSet<SeriesSFPITFInternalWarMagAvg>
        SeriesSFPITFInternalWarMagAvg { get; set; }
        
        protected virtual DbSet<SeriesSFPITFDomInstabilityMagAvg>
        SeriesSFPITFDomInstabilityMagAvg { get; set; }
        
        protected virtual DbSet<SeriesSFPITFConsolidatedMagAvg>
        SeriesSFPITFConsolidatedMagAvg { get; set; }
        
        protected virtual DbSet<SeriesDiploEmbassyOut>
        SeriesDiploEmbassyOut { get; set; }
        
        protected virtual DbSet<SeriesDiploIGOWeightedTotal>
        SeriesDiploIGOWeightedTotal { get; set; }
        
        protected virtual DbSet<SeriesDiploTreatyWeightedSign1Rat2>
        SeriesDiploTreatyWeightedSign1Rat2 { get; set; }
        
        protected virtual DbSet<SeriesDiploINGOProgrammatic>
        SeriesDiploINGOProgrammatic { get; set; }
        
        protected virtual DbSet<SeriesDiploDistanceToEurope>
        SeriesDiploDistanceToEurope { get; set; }
        
        protected virtual DbSet<SeriesHTCtpi>
        SeriesHTCtpi { get; set; }
        
        protected virtual DbSet<SeriesHTCgri>
        SeriesHTCgri { get; set; }

        protected virtual DbSet<SeriesDiploINGOAdvocacy>
        SeriesDiploINGOAdvocacy{ get; set; }

        protected virtual DbSet<SeriesDiploEmbassyMaxGlobal>
        SeriesDiploEmbassyMaxGlobal
        { get; set; }

        protected virtual DbSet<SeriesDiploIGOWeightMax>
        SeriesDiploIGOWeightMax
        { get; set; }

        protected virtual DbSet<SeriesDiploTreatyWeightedSign1Rat2Max>
        SeriesDiploTreatyWeightedSign1Rat2Max
        { get; set; }
        
    }
}
