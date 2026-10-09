using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IFs.Data {

    internal struct Point: IPoint<double>
    {
        public double X { get; internal set; }
        public double Y { get; internal set; }
    }

    [Table("Tablfunc")]
    internal partial class UnaryRegression: IRegression
    {
        // primary key of the regression
        [Column("Function Name"), Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        [Column("TFuncPoints")]
        public int PointsCount { get; set; }
        [Column("XVariable")]
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
        [Column("StdDev")] //jrs 2019/08/07
        public double? StdError { get; set; }
        // point coordinates for (piecewise) interpolated regression
        [Column("X1")]
        public double? X1 { get; set; }
        [Column("X2")]
        public double? X2 { get; set; }
        [Column("X3")]
        public double? X3 { get; set; }
        [Column("X4")]
        public double? X4 { get; set; }
        [Column("X5")]
        public double? X5 { get; set; }
        [Column("X6")]
        public double? X6 { get; set; }
        [Column("X7")]
        public double? X7 { get; set; }
        [Column("X8")]
        public double? X8 { get; set; }
        [Column("X9")]
        public double? X9 { get; set; }
        [Column("X10")]
        public double? X10 { get; set; }
        [Column("Y1")]
        public double? Y1 { get; set; }
        [Column("Y2")]
        public double? Y2 { get; set; }
        [Column("Y3")]
        public double? Y3 { get; set; }
        [Column("Y4")]
        public double? Y4 { get; set; }
        [Column("Y5")]
        public double? Y5 { get; set; }
        [Column("Y6")]
        public double? Y6 { get; set; }
        [Column("Y7")]
        public double? Y7 { get; set; }
        [Column("Y8")]
        public double? Y8 { get; set; }
        [Column("Y9")]
        public double? Y9 { get; set; }
        [Column("Y10")]
        public double? Y10 { get; set; }
        [NotMapped] //jrs 2019/07/23
        public short Seq => 0;
        [NotMapped]
        public IReadOnlyDictionary<string, double> Coefficients {
            get {
                return allCoefficients().Where(kv => kv.Value.HasValue)
                    .ToDictionary(kv => kv.Key, kv => kv.Value.Value);
            }
        }
        [NotMapped]
        public IReadOnlyCollection<IPoint<double>> Points {
            get {
                return new HashSet<IPoint<double>>(this.allPoints());
            }
        }
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
        private IEnumerable<IPoint<double>> allPoints() {
            if (PointsCount > 0) {
                yield return new Point { X = X1.Value, Y = Y1.Value };
            }
            if (PointsCount > 1) {
                yield return new Point { X = X2.Value, Y = Y2.Value };
            }
            if (PointsCount > 2) {
                yield return new Point { X = X3.Value, Y = Y3.Value };
            }
            if (PointsCount > 3) {
                yield return new Point { X = X4.Value, Y = Y4.Value };
            }
            if (PointsCount > 4) {
                yield return new Point { X = X5.Value, Y = Y5.Value };
            }
            if (PointsCount > 5) {
                yield return new Point { X = X6.Value, Y = Y6.Value };
            }
            if (PointsCount > 6) {
                yield return new Point { X = X7.Value, Y = Y7.Value };
            }
            if (PointsCount > 7) {
                yield return new Point { X = X8.Value, Y = Y8.Value };
            }
            if (PointsCount > 8) {
                yield return new Point { X = X9.Value, Y = Y9.Value };
            }
            if (PointsCount > 9) {
                yield return new Point { X = X10.Value, Y = Y10.Value };
            }
        }
    }

    internal partial class TablFuncContext {

        protected virtual DbSet<UnaryRegression> RegressionData { get; set; }

    }

}
