using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{
    [Table("SeriesVehiclesper1000")]
    internal partial class SeriesVehiclesper1000 : Series
    { }


    internal partial class IFsHistSeriesContext
    {
        protected virtual DbSet<SeriesVehiclesper1000> SeriesVehiclesper1000 { get; set; }
    }
}
