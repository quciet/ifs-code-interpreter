using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{
    [Table("Automobiles")]
    internal partial class Automobiles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public string FIPS_CODE { get; set; }
        [Column("CarsTot")]
        public float? CarsTot { get; set; }
        [Column("TrucksTot")]
        public float? TrucksTot { get; set; }
        [Column("VehiclesTot")]
        public float? VehiclesTot { get; set; }
        [Column("Mini")]
        public float? Mini { get; set; }
        [Column("Small")]
        public float? Small { get; set; }
        [Column("LowMed")]
        public float? LowMed { get; set; }
        [Column("Interm")]
        public float? Interm { get; set; }
        [Column("UpMed")]
        public float? UpMed { get; set; }
        [Column("Large")]
        public float? Large { get; set; }
        [Column("High")]
        public float? High { get; set; }
        [Column("LFamWag")]
        public float? LFamWag { get; set; }
        [Column("SFamWag")]
        public float? SFamWag { get; set; }
        [Column("SportL")]
        public float? SportL { get; set; }
        [Column("SportH")]
        public float? SportH { get; set; }
        [Column("PCD")]
        public float? PCD { get; set; }
        [Column("SPickup")]
        public float? SPickup { get; set; }
        [Column("SVan")]
        public float? SVan { get; set; }
        [Column("MPickup")]
        public float? MPickup { get; set; }
        [Column("LPickup")]
        public float? LPickup { get; set; }
        [Column("SUtility")]
        public float? SUtility { get; set; }
        [Column("MUtility")]
        public float? MUtility { get; set; }
        [Column("LUtility")]
        public float? LUtility { get; set; }
        [Column("MVan")]
        public float? MVan { get; set; }
        [Column("LVan")]
        public float? LVan { get; set; }
        [Column("LightComl")]
        public float? LightComl { get; set; }
        [Column("MedHeavy")]
        public float? MedHeavy { get; set; }
    }

    internal partial class IFsCoVaTraContext
    {
        internal virtual DbSet<Automobiles> Automobiles { get; set; }
    }
}
