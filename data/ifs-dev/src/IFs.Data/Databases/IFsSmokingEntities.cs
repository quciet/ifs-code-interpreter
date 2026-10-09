using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{
    [Table("SmokingRateEstimatesReg")]
    internal partial class SmokingRateEstimatesReg
    {
        [Column("RegionName")]
        public string RegionName { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("RegionId")]
        public int RegionId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Year")]
        public int Year { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Sex")]
        public int Sex { get; set; }
        [Column("SmokingRate")]
        public double? SmokingRate { get; set; }
    }

    internal partial class IFsSmokingContext
    {
        internal virtual DbSet<SmokingRateEstimatesReg> SmokingRateEstimatesReg { get; set; }
    }
}
