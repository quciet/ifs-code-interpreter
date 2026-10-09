using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFs.Data
{
    [Table("AX")]
    public partial class AX
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Year")]
        public int Year { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Region")]
        public int Region { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Row")]
        public int Row { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Column")]
        public int Column { get; set; }
        [Column("Value")]
        public float Value { get; set; }
    }

    [Table("SAMValues")]
    public partial class SAMValues
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Categories")]
        public string Categories { get; set; }
        [Column("Sectors")]
        public float Sectors { get; set; }
        [Column("Household")]
        public float Household { get; set; }
        [Column("Firms")]
        public float Firms { get; set; }
        [Column("Capital")]
        public float Capital { get; set; }
        [Column("Government")]
        public float Government { get; set; }
        [Column("ROW")]
        public float ROW { get; set; }
        [Column("Environment")]
        public float Environment { get; set; }
        [Column("Total")]
        public float Total { get; set; }
    }

    internal partial class SAMContext
    {
        internal virtual DbSet<AX> AX { get; set; }
        internal virtual DbSet<SAMValues> SAMValues { get; set; }
    }

}
