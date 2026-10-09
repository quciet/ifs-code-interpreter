using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesWaterwithdMunicipal")]
    internal partial class SeriesWaterwithdMunicipal : Series
    { }
    
    [Table("SeriesWaterwithdIndustrial")]
    internal partial class SeriesWaterwithdIndustrial : Series
    { }

    [Table("SeriesWaterwithdAgriculture")]
    internal partial class SeriesWaterwithdAgriculture : Series
    { }

    [Table("SeriesWaterResTotalRenewSurface")]
    internal partial class SeriesWaterResTotalRenewSurface : Series
    { }

    [Table("SeriesWaterResTotalRenewGround")]
    internal partial class SeriesWaterResTotalRenewGround : Series
    { }

    [Table("SeriesWaterResExploitSurface")]
    internal partial class SeriesWaterResExploitSurface : Series
    { }

    [Table("SeriesWaterResExploitGround")]
    internal partial class SeriesWaterResExploitGround : Series
    { }

    [Table("SeriesWaterSurfaceWithD")]
    internal partial class SeriesWaterSurfaceWithD : Series
    { }

    [Table("SeriesWaterGroundWithD")]
    internal partial class SeriesWaterGroundWithD : Series
    { }

    [Table("SeriesWaterFossilWithdrawal")]
    internal partial class SeriesWaterFossilWithdrawal : Series
    { }

    [Table("SeriesWaterResFossil")]
    internal partial class SeriesWaterResFossil : Series
    { }

    [Table("SeriesWaterResGroundAfrica")]
    internal partial class SeriesWaterResGroundAfrica : Series
    { }

    [Table("SeriesWastewaterProduced")]
    internal partial class SeriesWastewaterProduced : Series
    { }

    [Table("SeriesWasterwaterTreated")]
    internal partial class SeriesWasterwaterTreated : Series
    { }

    [Table("SeriesWastewaterTreatedReused")]
    internal partial class SeriesWastewaterTreatedReused : Series
    { }

    [Table("SeriesWaterResTotalExploit")]
    internal partial class SeriesWaterResTotalExploit : Series
    { }

    [Table("SeriesWaterResTotalRenew")]
    internal partial class SeriesWaterResTotalRenew : Series
    { }

    [Table("SeriesWaterResOverlap")]
    internal partial class SeriesWaterResOverlap : Series
    { }

    [Table("SeriesWaterDesalinated")]
    internal partial class SeriesWaterDesalinated : Series
    { }

    [Table("SeriesWaterTotalWithdSources")]
    internal partial class SeriesWaterTotalWithdSources : Series
    { }

    [Table("SeriesLandIrActual%Equip")]
    internal partial class SeriesLandIrActualEquip : Series
    { }

    [Table("SeriesWaterTotalWithdSector")]
    internal partial class SeriesWaterTotalWithdSector : Series
    { }

    internal partial class IFsHistSeriesContext
    {
        protected virtual DbSet<SeriesWaterwithdMunicipal> SeriesWaterwithdMunicipal { get; set; }
        protected virtual DbSet<SeriesWaterwithdIndustrial> SeriesWaterwithdIndustrial { get; set; }
        protected virtual DbSet<SeriesWaterwithdAgriculture> SeriesWaterwithdAgriculture { get; set; }
        protected virtual DbSet<SeriesWaterResTotalRenewSurface> SeriesWaterResTotalRenewSurface { get; set; }
        protected virtual DbSet<SeriesWaterResTotalRenewGround> SeriesWaterResTotalRenewGround { get; set; }
        protected virtual DbSet<SeriesWaterResExploitSurface> SeriesWaterResExploitSurface { get; set; }
        protected virtual DbSet<SeriesWaterResExploitGround> SeriesWaterResExploitGround { get; set; }
        protected virtual DbSet<SeriesWaterSurfaceWithD> SeriesWaterSurfaceWithD { get; set; }
        protected virtual DbSet<SeriesWaterGroundWithD> SeriesWaterGroundWithD { get; set; }
        protected virtual DbSet<SeriesWaterFossilWithdrawal> SeriesWaterFossilWithdrawal { get; set; }
        protected virtual DbSet<SeriesWaterResFossil> SeriesWaterResFossil { get; set; }
        protected virtual DbSet<SeriesWaterResGroundAfrica> SeriesWaterResGroundAfrica { get; set; }
        protected virtual DbSet<SeriesWastewaterProduced> SeriesWastewaterProduced { get; set; }
        protected virtual DbSet<SeriesWasterwaterTreated> SeriesWasterwaterTreated { get; set; }
        protected virtual DbSet<SeriesWastewaterTreatedReused> SeriesWastewaterTreatedReused { get; set; }
        protected virtual DbSet<SeriesWaterResTotalExploit> SeriesWaterResTotalExploit { get; set; }
        protected virtual DbSet<SeriesWaterResTotalRenew> SeriesWaterResTotalRenew { get; set; }
        protected virtual DbSet<SeriesWaterResOverlap> SeriesWaterResOverlap { get; set; }
        protected virtual DbSet<SeriesWaterDesalinated> SeriesWaterDesalinated { get; set; }
        protected virtual DbSet<SeriesWaterTotalWithdSources> SeriesWaterTotalWithdSources { get; set; }
        protected virtual DbSet<SeriesLandIrActualEquip> SeriesLandIrActualEquip { get; set; }
        protected virtual DbSet<SeriesWaterTotalWithdSector> SeriesWaterTotalWithdSector { get; set; }

    }

}