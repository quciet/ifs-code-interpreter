using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{

    public abstract partial class BasecaseVariable
    {
        [Column("Value")]
        public float? Value { get; set; }
    }

    public abstract partial class BasecaseDimension
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Seq { get; set; }
        [Column("Name"), Required]
        public string Name { get; set; }
    }

    [Table("pop_base_case")]
    public partial class PopBasecase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [Column("PopulationMales")]
        public float? PopulationMales { get; set; }
        [Column("PopulationFemales")]
        public float? PopulationFemales { get; set; }

        // public TimeBucket Time { get; set; }
        // public RegionBucket Region { get; set; }
        // public CohortBucket Cohort { get; set; }
        // public GenderBucket Gender { get; set; }
    }

    [Table("fer_base_case")]
    public partial class FerBasecase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [Column("Fertility")]
        public float? Fertility { get; set; }
        // public TimeBucket Time { get; set; }
        // public RegionBucket Region { get; set; }
        // public CohortBucket Cohort { get; set; }
    }

    [Table("mor_base_case")]
    public partial class MorBasecase 
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [Column("MortalityMales")]
        public float? MortalityMales { get; set; }
        [Column("MortalityFemales")]
        public float? MortalityFemales { get; set; }
        // public TimeBucket Time { get; set; }
        // public RegionBucket Region { get; set; }
        // public CohortBucket Cohort { get; set; }
    }

    [Table("mor_det_base_case")]
    public partial class MorDetBaseCase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [Column("MortPerMalesComDis")]
        public float? MortPerMalesComDis { get; set; }
        [Column("MortPerFeMalesComDis")]
        public float? MortPerFeMalesComDis { get; set; }
        [Column("MortPerMalesNonComDis")]
        public float? MortPerMalesNonComDis { get; set; }
        [Column("MortPerFeMalesNonComDis")]
        public float? MortPerFeMalesNonComDis { get; set; }
        [Column("MortPerMalesInjuries")]
        public float? MortPerMalesInjuries { get; set; }
        [Column("MortPerFeMalesInjuries")]
        public float? MortPerFeMalesInjuries { get; set; }
        [Column("MaligNeoPlasMales")]
        public float? MaligNeoPlasMales { get; set; }
        [Column("MaligNeoPlasFemales")]
        public float? MaligNeoPlasFemales { get; set; }
        [Column("CardioVascMales")]
        public float? CardioVascMales { get; set; }
        [Column("CardioVascFemales")]
        public float? CardioVascFemales { get; set; }
        [Column("DigestiveMales")]
        public float? DigestiveMales { get; set; }
        [Column("DigestiveFemales")]
        public float? DigestiveFemales { get; set; }
        [Column("RespiratoryMales")]
        public float? RespiratoryMales { get; set; }
        [Column("RespiratoryFemales")]
        public float? RespiratoryFemales { get; set; }
        [Column("OtherG2Males")]
        public float? OtherG2Males { get; set; }
        [Column("OtherG2Females")]
        public float? OtherG2Females { get; set; }
        [Column("RoadTrafficAccMales")]
        public float? RoadTrafficAccMales { get; set; }
        [Column("RoadTrafficAccFemales")]
        public float? RoadTrafficAccFemales { get; set; }
        [Column("OtherunintInjMales")]
        public float? OtherunintInjMales { get; set; }
        [Column("OtherunintInjFemales")]
        public float? OtherunintInjFemales { get; set; }
        [Column("IntenInjMales")]
        public float? IntenInjMales { get; set; }
        [Column("IntenInjFemales")]
        public float? IntenInjFemales { get; set; }
        [Column("DiabetesMales")]
        public float? DiabetesMales { get; set; }
        [Column("DiabetesFemales")]
        public float? DiabetesFemales { get; set; }
        [Column("AIDSMales")]
        public float? AIDSMales { get; set; }
        [Column("AIDSFemales")]
        public float? AIDSFemales { get; set; }
        [Column("ComDisNoAIDSMales")]
        public float? ComDisNoAIDSMales { get; set; }
        [Column("ComDisNoAIDSFemales")]
        public float? ComDisNoAIDSFemales { get; set; }
        [Column("DiarrheaMales")]
        public float? DiarrheaMales { get; set; }
        [Column("DiarrheaFemales")]
        public float? DiarrheaFemales { get; set; }
        [Column("MalariaMales")]
        public float? MalariaMales { get; set; }
        [Column("MalariaFemales")]
        public float? MalariaFemales { get; set; }
        [Column("RespInfMales")]
        public float? RespInfMales { get; set; }
        [Column("RespInfFemales")]
        public float? RespInfFemales { get; set; }
        [Column("MentalHealthMales")]
        public float? MentalHealthMales { get; set; }
        [Column("MentalHealthFemales")]
        public float? MentalHealthFemales { get; set; }
    }

    [Table("morb_det_base_case")]
    public partial class MorbDetBaseCase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [Column("MorbPerMalesComDis")]
        public float? MorbPerMalesComDis { get; set; }
        [Column("MorbPerFeMalesComDis")]
        public float? MorbPerFeMalesComDis { get; set; }
        [Column("MorbPerMalesNonComDis")]
        public float? MorbPerMalesNonComDis { get; set; }
        [Column("MorbPerFeMalesNonComDis")]
        public float? MorbPerFeMalesNonComDis { get; set; }
        [Column("MorbPerMalesInjuries")]
        public float? MorbPerMalesInjuries { get; set; }
        [Column("MorbPerFeMalesInjuries")]
        public float? MorbPerFeMalesInjuries { get; set; }
        [Column("MaligNeoPlasMales")]
        public float? MaligNeoPlasMales { get; set; }
        [Column("MaligNeoPlasFemales")]
        public float? MaligNeoPlasFemales { get; set; }
        [Column("CardioVascMales")]
        public float? CardioVascMales { get; set; }
        [Column("CardioVascFemales")]
        public float? CardioVascFemales { get; set; }
        [Column("DigestiveMales")]
        public float? DigestiveMales { get; set; }
        [Column("DigestiveFemales")]
        public float? DigestiveFemales { get; set; }
        [Column("RespiratoryMales")]
        public float? RespiratoryMales { get; set; }
        [Column("RespiratoryFemales")]
        public float? RespiratoryFemales { get; set; }
        [Column("OtherG2Males")]
        public float? OtherG2Males { get; set; }
        [Column("OtherG2Females")]
        public float? OtherG2Females { get; set; }
        [Column("RoadTrafficAccMales")]
        public float? RoadTrafficAccMales { get; set; }
        [Column("RoadTrafficAccFemales")]
        public float? RoadTrafficAccFemales { get; set; }
        [Column("OtherunintInjMales")]
        public float? OtherunintInjMales { get; set; }
        [Column("OtherunintInjFemales")]
        public float? OtherunintInjFemales { get; set; }
        [Column("IntenInjMales")]
        public float? IntenInjMales { get; set; }
        [Column("IntenInjFemales")]
        public float? IntenInjFemales { get; set; }
        [Column("DiabetesMales")]
        public float? DiabetesMales { get; set; }
        [Column("DiabetesFemales")]
        public float? DiabetesFemales { get; set; }
        [Column("AIDSMales")]
        public float? AIDSMales { get; set; }
        [Column("AIDSFemales")]
        public float? AIDSFemales { get; set; }
        [Column("ComDisNoAIDSMales")]
        public float? ComDisNoAIDSMales { get; set; }
        [Column("ComDisNoAIDSFemales")]
        public float? ComDisNoAIDSFemales { get; set; }
        [Column("DiarrheaMales")]
        public float? DiarrheaMales { get; set; }
        [Column("DiarrheaFemales")]
        public float? DiarrheaFemales { get; set; }
        [Column("MalariaMales")]
        public float? MalariaMales { get; set; }
        [Column("MalariaFemales")]
        public float? MalariaFemales { get; set; }
        [Column("RespInfMales")]
        public float? RespInfMales { get; set; }
        [Column("RespInfFemales")]
        public float? RespInfFemales { get; set; }
        [Column("MentalHealthMales")]
        public float? MentalHealthMales { get; set; }
        [Column("MentalHealthFemales")]
        public float? MentalHealthFemales { get; set; }
    }

    [Table("EdnGradsBaseCase")]
    public partial class EdnGradsBaseCase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [Column("PriGradMales")]
        public float? PriGradMales { get; set; }
        [Column("PriGradFemales")]
        public float? PriGradFemales { get; set; }
        [Column("SecGradMales")]
        public float? SecGradMales { get; set; }
        [Column("SecGradFemales")]
        public float? SecGradFemales { get; set; }
        [Column("TerGradMales")]
        public float? TerGradMales { get; set; }
        [Column("TerGradFemales")]
        public float? TerGradFemales { get; set; }
        // public TimeBucket Time { get; set; }
        // public RegionBucket Region { get; set; }
        // public CohortBucket Cohort { get; set; }
    }

    [Table("EdnYears25BaseCase")]
    public partial class EdnYears25BaseCase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [Column("EdnYears25Males")]
        public float? EdnYears25Males { get; set; }
        [Column("EdnYears25Females")]
        public float? EdnYears25Females { get; set; }
        [Column("EdnYears25Total")]
        public float? EdnYears25Total { get; set; }
        // public TimeBucket Time { get; set; }
        // public RegionBucket Region { get; set; }
        // public CohortBucket Cohort { get; set; }
    }

    [Table("EdnYears15BaseCase")]
    public partial class EdnYears15BaseCase
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short TimeId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [Column("EdnYears15Males")]
        public float? EdnYears15Males { get; set; }
        [Column("EdnYears15Females")]
        public float? EdnYears15Females { get; set; }
        [Column("EdnYears15Total")]
        public float? EdnYears15Total { get; set; }
        // public TimeBucket Time { get; set; }
        // public RegionBucket Region { get; set; }
        // public CohortBucket Cohort { get; set; }
    }

    [Table("mor_det_factors")]
    public partial class MorDetFactors
    {
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [Column("MortPerMalesComDis")]
        public float? MortPerMalesComDis { get; set; }
        [Column("MortPerFeMalesComDis")]
        public float? MortPerFeMalesComDis { get; set; }
        [Column("MaligNeoPlasMales")]
        public float? MaligNeoPlasMales { get; set; }
        [Column("MaligNeoPlasFemales")]
        public float? MaligNeoPlasFemales { get; set; }
        [Column("CardioVascMales")]
        public float? CardioVascMales { get; set; }
        [Column("CardioVascFemales")]
        public float? CardioVascFemales { get; set; }
        [Column("DigestiveMales")]
        public float? DigestiveMales { get; set; }
        [Column("DigestiveFemales")]
        public float? DigestiveFemales { get; set; }
        [Column("RespiratoryMales")]
        public float? RespiratoryMales { get; set; }
        [Column("RespiratoryFemales")]
        public float? RespiratoryFemales { get; set; }
        [Column("OtherG2Males")]
        public float? OtherG2Males { get; set; }
        [Column("OtherG2Females")]
        public float? OtherG2Females { get; set; }
        [Column("RoadTrafficAccMales")]
        public float? RoadTrafficAccMales { get; set; }
        [Column("RoadTrafficAccFemales")]
        public float? RoadTrafficAccFemales { get; set; }
        [Column("OtherunintInjMales")]
        public float? OtherunintInjMales { get; set; }
        [Column("OtherunintInjFemales")]
        public float? OtherunintInjFemales { get; set; }
        [Column("IntenInjMales")]
        public float? IntenInjMales { get; set; }
        [Column("IntenInjFemales")]
        public float? IntenInjFemales { get; set; }
        [Column("DiabetesMales")]
        public float? DiabetesMales { get; set; }
        [Column("DiabetesFemales")]
        public float? DiabetesFemales { get; set; }
        [Column("DiarrheaMales")]
        public float? DiarrheaMales { get; set; }
        [Column("DiarrheaFemales")]
        public float? DiarrheaFemales { get; set; }
        [Column("MalariaMales")]
        public float? MalariaMales { get; set; }
        [Column("MalariaFemales")]
        public float? MalariaFemales { get; set; }
        [Column("RespInfMales")]
        public float? RespInfMales { get; set; }
        [Column("RespInfFemales")]
        public float? RespInfFemales { get; set; }
        [Column("MentalHealthMales")]
        public float? MentalHealthMales { get; set; }
        [Column("MentalHealthFemales")]
        public float? MentalHealthFemales { get; set; }
    }

    [Table("mor_det_norm_factors")]
    public partial class MorDetNormFactors
    {
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short CohortId { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegionId { get; set; }
        [Column("Males")]
        public float? Males { get; set; }
        [Column("Females")]
        public float? Females { get; set; }
    }

    [Table("HLPROXIMATEDRIVERS")]
    public partial class HLPROXIMATEDRIVERS
    {
        [Column("VarName")]
        public string VarName { get; set; }
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Year { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Region { get; set; }
        [Column("Dim2")]
        public short? Dim2 { get; set; }
        [Column("Dim3")]
        public short? Dim3 { get; set; }
        [Column("DistalFormVal")]
        public float? DistalFormVal { get; set; }
    }

    [Table("StuntingByAge")]
    public partial class StuntingByAge
    {
        [ForeignKey("TimeBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Years { get; set; }
        [ForeignKey("CohortBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short NCohorts { get; set; }
        [ForeignKey("RegionBucket"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Region { get; set; }
        [Column("StuntingMales")]
        public float? StuntingMales { get; set; }
        [Column("StuntingFemales")]
        public float? StuntingFemales { get; set; }
    }

    [Table("dim_time")]
    public partial class TimeBucket: BasecaseDimension
    {}

    [Table("dim_region")]
    public partial class RegionBucket: BasecaseDimension
    {}

    [Table("dim_cohort")]
    public partial class CohortBucket: BasecaseDimension
    {}

    [Table("dim_gender")]
    public partial class GenderBucket: BasecaseDimension
    {}

    internal partial class BasecaseContext {
        internal virtual DbSet<PopBasecase> PopBasecase { get; set; }
        internal virtual DbSet<FerBasecase> FerBasecase { get; set; }
        internal virtual DbSet<MorBasecase> MorBasecase { get; set; }
        internal virtual DbSet<MorDetBaseCase> MorDetBaseCase { get; set; }
        internal virtual DbSet<MorbDetBaseCase> MorbDetBaseCase { get; set; }        
        internal virtual DbSet<MorDetFactors> MorDetFactors { get; set; }
        internal virtual DbSet<MorDetNormFactors> MorDetNormFactors { get; set; }
        internal virtual DbSet<StuntingByAge> StuntingByAge { get; set; }        
        internal virtual DbSet<EdnGradsBaseCase> EdnGradsBaseCase { get; set; }
        internal virtual DbSet<EdnYears25BaseCase> EdnYears25BaseCase { get; set; }
        internal virtual DbSet<EdnYears15BaseCase> EdnYears15BaseCase { get; set; }
        internal virtual DbSet<TimeBucket> TimeBuckets { get; set; }
        internal virtual DbSet<RegionBucket> RegionBuckets { get; set; }
        internal virtual DbSet<CohortBucket> CohortBuckets { get; set; }
        internal virtual DbSet<GenderBucket> GenderBuckets { get; set; }
        internal virtual DbSet<HLPROXIMATEDRIVERS> HLPROXIMATEDRIVERS { get; set; }        
    }
}
