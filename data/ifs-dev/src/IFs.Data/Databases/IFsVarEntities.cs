using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data
{

    [Table("AggRules")]
    internal partial class AggRules
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("AggName")]
        public string AggName { get; set; }
        [Column("ForecastVar")]
        public string ForecastVar { get; set; }
        [Column("Dim2")]
        public string Dim2 { get; set; }
        [Column("Dim3")]
        public string Dim3 { get; set; }
        [Column("HisEmpVar")]
        public string HisEmpVar { get; set; }
    }

    [Table("IFSVAR")]
    internal partial class VariableBind: IVariable
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("NAME")]
        public string Name { get; set; }
        [Column("DISPLAY NAME")]
        public string DisplayName { get; set; }
        [Column("TYPE")]
        public int Type { get; set; }
        [Column("DIMENSION1")]
        public int Dimension1 { get; set; }
        [Column("DIMENSION2")]
        public int Dimension2 { get; set; }
        [Column("DIMENSION3")]
        public int Dimension3 { get; set; } 
        [Column("MINIMUM")]
        public double? Min { get; set; }
        [Column("MAXIMUM")]
        public double? Max { get; set; }
        [Column("CommonTarget")]
        public bool? CommonTarget { get; set; }        //for parameters using group, the target is set to the same number for all members of the group, instead of keeping the same proportions
        [Column("Currency")]
        public string Currency { get; set; }
        [Column("Aggregation")]
        public string Aggregation { get; set; }
        //jrs 2025/05/23
        [Column("BilateralComp")]
        public int Bilat { get; set; } //jrs 2025/10/31 added ? to the type for older versions


        public IReadOnlyList<int> Dimensions {
            get {
                var dims = new List<int>();
                // all variables have the special time dimension
                dims.Add(DataExtensions.DIM_TIME);
                // other dimensions are from the relevant IFSVAR record
                if (Dimension1 > 0) {
                    dims.Add(Dimension1);
                }
                if (Dimension2 > 0) {
                    dims.Add(Dimension2);
                }
                if (Dimension3 > 0) {
                    dims.Add(Dimension3);
                }
                return dims;
            }
        }
    }

    internal partial class IFsVarContext
    {
        protected virtual DbSet<VariableBind> IFsVar { get; set; }
        internal virtual DbSet<AggRules> AggRules { get; set; }
    }

}
