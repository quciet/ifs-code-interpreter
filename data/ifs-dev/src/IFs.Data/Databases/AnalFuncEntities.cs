using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data {

    [Table("AnalFunc")]
    internal partial class PartialRegression: IRegression
    {
        // primary key of the regression
        [Column("Function Name"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        [Column("XVariable"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string InputName { get; set; }
        [Column("YVariable")]
        public string OutputName { get; set; }
        // coefficients for formula-based regression
        [Column("b1")]
        public double? b1 { get; set; }
        [Column("b2")]
        public double? b2 { get; set; }
        [Column("b3")]
        public double? b3 { get; set; }
        [Column("b4")]
        public double? b4 { get; set; }
        [Column("b5")]
        public double? b5 { get; set; }
        [Column("b6")]
        public double? b6 { get; set; }
        [Column("b7")]
        public double? b7 { get; set; }
        [Column("b8")]
        public double? b8 { get; set; }
        [Column("b9")]
        public double? b9 { get; set; }
        [Column("a")]
        public double? a { get; set; }
        [Column("StandardError")] //jrs 2019/08/07
        public double? StdError { get; set; }
        [NotMapped]
        public IReadOnlyDictionary<string, double> Coefficients {
            get {
                return allCoefficients().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        [NotMapped]
        public IReadOnlyCollection<IPoint<double>> Points
            => Array.Empty<IPoint<double>>();
        private IEnumerable<KeyValuePair<string, double?>> allCoefficients() {
            yield return new KeyValuePair<string, double?>("b1", b1);
            yield return new KeyValuePair<string, double?>("b2", b2);
            yield return new KeyValuePair<string, double?>("b3", b3);
            yield return new KeyValuePair<string, double?>("b4", b4);
            yield return new KeyValuePair<string, double?>("b5", b5);
            yield return new KeyValuePair<string, double?>("b6", b6);
            yield return new KeyValuePair<string, double?>("b7", b7);
            yield return new KeyValuePair<string, double?>("b8", b8);
            yield return new KeyValuePair<string, double?>("b9", b9);
            yield return new KeyValuePair<string, double?>("a", a);
        }
        [NotMapped] //jrs 2019/07/23
        public short Seq => 0;
    }

    internal partial class AnalFuncContext {

        protected virtual DbSet<PartialRegression> RegressionData { get; set; }

    }

}
