using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{

    [Table("ifs_var")]
    internal partial class VariableRun: IVariable
    {
        // primary key of the variable
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        // human-readable name of the variable
        public string DisplayName { get; set; }
        // variable type from IFsVar.db
        [Required]
        public int Type { get; set; }
        //jrs 2025/05/23
        public int Bilat { get; set; }
        //jrs 2019/08/18
        public double? Min { get; set; }
        public double? Max { get; set; }

        //2019/09/30
        public bool? CommonTarget { get; set; }
        public string Currency { get; set; }
        public string Aggregation { get; set; }

        // many-to-many relation to DimensionRun via VariableDimension
        // [ForeignKey("VariableName")]
        public virtual ISet<VariableDimension> Dimensions { get; set; }
        // IVariable dimension IDs
        [NotMapped]
        IReadOnlyList<int> IVariable.Dimensions {
            get => this.Dimensions.OrderBy(vd => vd.Seq).Select(d => d.DimensionId).ToList();
        }
        // one-to-one relation to VariableBlob
        public virtual VariableBlob Blob { get; set; }
    }

    [Table("ifs_var_blob")]
    internal partial class VariableBlob
    {
        // global identifier of the variable
        [Key, ForeignKey("VariableRun"), DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string VariableName { get; set; }
        // dataframe of the variable
        public byte[] Data { get; set; }
        // one-to-zero_or_one relation to VariableRun 
        public virtual VariableRun Variable { get; set; }
    }

    [Table("ifs_dim")]
    internal partial class DimensionRun: IDimension
    {
        // primary key of the dimension
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        // dimension name (mostly null)
        public string Name { get; set; }
        // many-to-many relation to VariableRun
        public virtual ISet<VariableDimension> Variables { get; set; }
        // one-to-many relation to DimensionBucket via VariableDimension
        public virtual ISet<DimensionBucket> Buckets { get; set; }
        // IDimension bucket list
        [NotMapped]
        IReadOnlyDictionary<short, string> IDimension.Buckets {
            get => this.Buckets.ToDictionary(b => b.Seq, b => b.Name);
        }
    }

    [Table("ifs_var_dim")]
    internal partial class VariableDimension
    {
        // global identifier of the variable
        [Column(Order=0), ForeignKey("VariableRun"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string VariableName { get; set; }
        // local identifier (ordinal number) of the dimension w.r.t. the variable
        [Column(Order=1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Seq { get; set; }
        // global identifier of the dimension
        [ForeignKey("DimensionRun")]
        public int DimensionId { get; set; }
        public virtual VariableRun Variable { get; set; }
        public virtual DimensionRun Dimension { get; set; }
    }

    [Table("ifs_dim_bucket")]
    internal partial class DimensionBucket
    {
        // global identifier of the dimension
        [Column(Order=0), ForeignKey("DimensionRun"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DimensionId { get; set; }
        // local identifier (ordinal number) of the bucket w.r.t. the dimension
        [Column(Order=1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Seq { get; set; }
        // string name / value of each dimension bucket from IFs.grp
        [Required]
        public string Name { get; set; }
        public virtual DimensionRun Dimension { get; set; }
    }

    [Table("ifs_reg")]
    internal partial class RegressionRun: IRegression {
        // name of the regression (not necessarily distinct)
        [Column(Order=0), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        // sequence number of the regressions having the same name
        [Column(Order=1), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Seq { get; set; }
        // textual description of the input variable (not necessarily the variable name)
        [Column("InputName")]
        public string InputName { get; set; }
        // textual description of the output variable (not necessarily the variable name)
        [Column("OutputName")]
        public string OutputName{ get; set; }
        //jrs 2019/08/07
        [Column("StdError")]
        public double? StdError { get; set; }
        // one-to-many relation to RegressionCoefficients
        [ForeignKey("RegressionName,RegressionSeq")]
        public virtual ISet<RegressionCoefficient> Coefficients { get; set; }
        // one-to-many relation to RegressionPoint
        [ForeignKey("RegressionName,RegressionSeq")]
        public virtual ISet<RegressionPoint> Points { get; set; }
        [NotMapped]
        IReadOnlyDictionary<string, double> IRegression.Coefficients
            => this.Coefficients.ToDictionary(c => c.Name, c => c.Value);
        [NotMapped]
        IReadOnlyCollection<IPoint<double>> IRegression.Points
            => new HashSet<IPoint<double>>(this.Points.Select(p => p as IPoint<double>));
    }

    //jrs 2019/07/17 added : IRegressionCoefficients
    [Table("ifs_reg_coeff")]
    internal partial class RegressionCoefficient: IRegressionCoefficients    {
        // global identifier of the regression
        [Column(Order=0), ForeignKey("RegressionRun"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RegressionName { get; set; }
        [Column(Order=1), ForeignKey("RegressionRun"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegressionSeq { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        [Required]
        public double Value { get; set; }
    }

    [Table("ifs_reg_point")]
    internal partial class RegressionPoint: IPoint<double> {
        // global identifier of the regression
        [Column(Order=0), ForeignKey("RegressionRun"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string RegressionName { get; set; }
        [Column(Order=1), ForeignKey("RegressionRun"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short RegressionSeq { get; set; }
        [Column(Order=2), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Seq { get; set; }
        [Required]
        public double X { get; set; }
        [Required]
        public double Y { get; set; }
    }

    internal partial class StorageContext
    {
        protected virtual DbSet<VariableRun> VariableData { get; set; }
        protected virtual DbSet<VariableBlob> VariableBlob { get; set; }
        protected virtual DbSet<DimensionRun> DimensionData { get; set; }
        protected virtual DbSet<DimensionBucket> DimensionBucket { get; set; }
        protected virtual DbSet<VariableDimension> VariableDimension { get; set; }
        protected virtual DbSet<RegressionRun> RegressionData { get; set; }
        protected virtual DbSet<RegressionCoefficient> RegressionCoefficients { get; set; }
        protected virtual DbSet<RegressionPoint> RegressionPoints { get; set; }
    }

}
