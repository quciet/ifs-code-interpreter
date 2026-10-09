using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesGovtMil%GDPSIPRI")]
    internal partial class SeriesGovtMilGDPSIPRI : Series
    { }

    [Table("SeriesGovtMil%GDP")]
    internal partial class SeriesGovtMilGDP : Series
    { }

    [Table("SeriesGovtHl%GDP")]
    internal partial class SeriesGovtHlGDP : Series
    { }

    [Table("SeriesHealthExpTot%GDP")]
    internal partial class SeriesHealthExpTotGDP : Series
    { }

    [Table("SeriesHealthExpPriv%Tot")]
    internal partial class SeriesHealthExpPrivTot : Series
    { }

    [Table("SeriesGovtEdPub%GDP")]
    internal partial class SeriesGovtEdPubGDP : Series
    { }

    [Table("SeriesR&DGovt%GDP")]
    internal partial class SeriesRDGovtGDP : Series
    { }

    [Table("SeriesR&D%GDPWDI")]
    internal partial class SeriesRDGDPWDI : Series
    { }

    [Table("SeriesGovtCalcEdCen%GDP")]
    internal partial class SeriesGovtCalcEdCenGDP : Series
    { }

    [Table("SeriesGovtCalcEdTot%GDP")]
    internal partial class SeriesGovtCalcEdTotGDP : Series
    { }

    [Table("SeriesGovtCalcHealthCen%GDP")]
    internal partial class SeriesGovtCalcHealthCenGDP : Series
    { }

    [Table("SeriesGovtCalcHealthTot%GDP")]
    internal partial class SeriesGovtCalcHealthTotGDP : Series
    { }

    [Table("SeriesGovtMil%GDPWDI")]
    internal partial class SeriesGovtMilGDPWDI : Series
    { }

    internal partial class IFsHistSeriesContext
    {
        protected virtual DbSet<SeriesGovtMilGDPSIPRI> SeriesGovtMilGDPSIPRI { get; set; }
        protected virtual DbSet<SeriesGovtMilGDPWDI> SeriesGovtMilGDPWDI { get; set; }
        protected virtual DbSet<SeriesGovtMilGDP> SeriesGovtMilGDP { get; set; }
        protected virtual DbSet<SeriesGovtHlGDP> SeriesGovtHlGDP { get; set; }
        protected virtual DbSet<SeriesHealthExpTotGDP> SeriesHealthExpTotGDP { get; set; }
        protected virtual DbSet<SeriesHealthExpPrivTot> SeriesHealthExpPrivTot { get; set; }
        protected virtual DbSet<SeriesGovtEdPubGDP> SeriesGovtEdPubGDP { get; set; }

        protected virtual DbSet<SeriesRDGovtGDP> SeriesRDGovtGDP { get; set; }
        protected virtual DbSet<SeriesRDGDPWDI> SeriesRDGDPWDI { get; set; }
        protected virtual DbSet<SeriesGovtCalcEdCenGDP> SeriesGovtCalcEdCenGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcEdTotGDP> SeriesGovtCalcEdTotGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcHealthCenGDP> SeriesGovtCalcHealthCenGDP { get; set; }
        protected virtual DbSet<SeriesGovtCalcHealthTotGDP> SeriesGovtCalcHealthTotGDP { get; set; }
    }

}

