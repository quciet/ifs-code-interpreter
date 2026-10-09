using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesLaborSecInd%Tot")]
    internal partial class SeriesLaborSecIndTot : Series
    { }

    [Table("SeriesLaborSecSer%Tot")]
    internal partial class SeriesLaborSecSerTot : Series
    { }

    [Table("SeriesCulExComDum")]
    internal partial class SeriesCulExComDum : Series
    { }
    
    internal partial class IFsHistSeriesContext
    {
        protected virtual DbSet<SeriesLaborSecIndTot> SeriesLaborSecIndTot { get; set; }
        protected virtual DbSet<SeriesLaborSecSerTot> SeriesLaborSecSerTot { get; set; }
        protected virtual DbSet<SeriesCulExComDum> SeriesCulExComDum { get; set; }
        
    }
}
