using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{
    [Table("HappyPercentW3")]
    internal partial class HappyPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("TrustPeoplePercentW3")]
    internal partial class TrustPeoplePercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("RespectAuthPercentW3")]
    internal partial class RespectAuthPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }
    
    [Table("SignedPetitPercentW3")]
    internal partial class SignedPetitPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    
    [Table("GodImprtPercentW3")]
    internal partial class GodImprtPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    
    [Table("HomoJustifPercentLatest")]
    internal partial class HomoJustifPercentLatest
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    
    [Table("AbortJustifPercentW3")]
    internal partial class AbortJustifPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }
    
    [Table("NationProudPercentW3")]
    internal partial class NationProudPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("PMMinMatPercentW3")]
    internal partial class PMMinMatPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("AutonPercentW3")]
    internal partial class AutonPercentW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("SurvSelfExpLoadsLatestW3")]
    internal partial class SurvSelfExpLoadsLatestW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("SurvSelfExpW4")]
    internal partial class SurvSelfExpW4
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Cohort1")]
        public float? Cohort1 { get; set; }
        [Column("Cohort2")]
        public float? Cohort2 { get; set; }
        [Column("Cohort3")]
        public float? Cohort3 { get; set; }
        [Column("Cohort4")]
        public float? Cohort4 { get; set; }
        [Column("Cohort5")]
        public float? Cohort5 { get; set; }
        [Column("Cohort6")]
        public float? Cohort6 { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("SurvSelfExpW3")]
    internal partial class SurvSelfExpW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Cohort1")]
        public float? Cohort1 { get; set; }
        [Column("Cohort2")]
        public float? Cohort2 { get; set; }
        [Column("Cohort3")]
        public float? Cohort3 { get; set; }
        [Column("Cohort4")]
        public float? Cohort4 { get; set; }
        [Column("Cohort5")]
        public float? Cohort5 { get; set; }
        [Column("Cohort6")]
        public float? Cohort6 { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("TradSecRatW5")]
    internal partial class TradSecRatW5
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Cohort1")]
        public float? Cohort1 { get; set; }
        [Column("Cohort2")]
        public float? Cohort2 { get; set; }
        [Column("Cohort3")]
        public float? Cohort3 { get; set; }
        [Column("Cohort4")]
        public float? Cohort4 { get; set; }
        [Column("Cohort5")]
        public float? Cohort5 { get; set; }
        [Column("Cohort6")]
        public float? Cohort6 { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("TradSecRatW4")]
    internal partial class TradSecRatW4
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Cohort1")]
        public float? Cohort1 { get; set; }
        [Column("Cohort2")]
        public float? Cohort2 { get; set; }
        [Column("Cohort3")]
        public float? Cohort3 { get; set; }
        [Column("Cohort4")]
        public float? Cohort4 { get; set; }
        [Column("Cohort5")]
        public float? Cohort5 { get; set; }
        [Column("Cohort6")]
        public float? Cohort6 { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("TradSecRatW3")]
    internal partial class TradSecRatW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Cohort1")]
        public float? Cohort1 { get; set; }
        [Column("Cohort2")]
        public float? Cohort2 { get; set; }
        [Column("Cohort3")]
        public float? Cohort3 { get; set; }
        [Column("Cohort4")]
        public float? Cohort4 { get; set; }
        [Column("Cohort5")]
        public float? Cohort5 { get; set; }
        [Column("Cohort6")]
        public float? Cohort6 { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    [Table("TradSecRatLoadsLatestW3")]
    internal partial class TradSecRatLoadsLatestW3
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("Country")]
        public string Country { get; set; }
        [Column("FIPS_CODE")]
        public int FIPS_CODE { get; set; }
        [Column("Total")]
        public float? Total { get; set; }
    }

    internal partial class IFsWVSCohortContext
    {
        internal virtual DbSet<HappyPercentW3> HappyPercentW3 { get; set; }
        internal virtual DbSet<TrustPeoplePercentW3> TrustPeoplePercentW3 { get; set; }
        internal virtual DbSet<RespectAuthPercentW3> RespectAuthPercentW3 { get; set; }
        internal virtual DbSet<SignedPetitPercentW3> SignedPetitPercentW3 { get; set; }
        internal virtual DbSet<GodImprtPercentW3> GodImprtPercentW3 { get; set; }
        internal virtual DbSet<HomoJustifPercentLatest> HomoJustifPercentLatest { get; set; }
        internal virtual DbSet<AbortJustifPercentW3> AbortJustifPercentW3 { get; set; }
        internal virtual DbSet<NationProudPercentW3> NationProudPercentW3 { get; set; }
        internal virtual DbSet<PMMinMatPercentW3> PMMinMatPercentW3 { get; set; }
        internal virtual DbSet<AutonPercentW3> AutonPercentW3 { get; set; }
        internal virtual DbSet<SurvSelfExpLoadsLatestW3> SurvSelfExpLoadsLatestW3 { get; set; }

        internal virtual DbSet<SurvSelfExpW4> SurvSelfExpW4 { get; set; }
        internal virtual DbSet<SurvSelfExpW3> SurvSelfExpW3 { get; set; }
        internal virtual DbSet<TradSecRatW5> TradSecRatW5 { get; set; }
        internal virtual DbSet<TradSecRatW4> TradSecRatW4 { get; set; }
        internal virtual DbSet<TradSecRatW3> TradSecRatW3 { get; set; }

        internal virtual DbSet<TradSecRatLoadsLatestW3> TradSecRatLoadsLatestW3 { get; set; }
        
    }
}
