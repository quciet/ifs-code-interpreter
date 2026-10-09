using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{
    [Table("AllProvinces")]
    internal partial class AllProvinces
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("AllProvinces")]
        public bool AllProvs { get; set; }
        [Column("HistSeries")]
        public string HistSeries { get; set; }
        [Column("LocationX")]
        public int LocationX { get; set; }
        [Column("LocationY")]
        public int LocationY { get; set; }
    }

    [Table("Regionalization")]
    internal partial class Regionalization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region")]
        public string Region { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Province")]
        public string Province { get; set; }
    }

    internal partial class ProvincesContext
    {
        internal virtual DbSet<AllProvinces> AllProvinces { get; set; }
        internal virtual DbSet<Regionalization> Regionalization { get; set; }
    }

}
