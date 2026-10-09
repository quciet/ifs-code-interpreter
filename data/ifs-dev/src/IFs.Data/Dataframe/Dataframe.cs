using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IFs.Data
{
    // Abstract base class of dataframe
    public abstract class AbstractDataframe : IDataframe
    {

        public abstract IVariable Variable { get; }

        public abstract IReadOnlyList<IDimension> Dimensions { get; }

        public abstract object Handle { get; }

        public abstract int RowCount { get; }

        public abstract float? this[int index] { get; set; }

        public float? this[params short[] keys]
        {
            get
            {
                return this[this.Loc(keys)];
            }
            set
            {
                this[this.Loc(keys)] = value;
            }
        }

        public abstract int Loc(short[] keys);

        public abstract IDataframe Append(IDataframe source);

        public abstract IDataframe Append(params object[] values);

        public abstract IDataframe Append(params IEnumerable<object>[] source);

        public abstract IQueryBuilder Filter(IQueryFilter pred = null);

        public abstract byte[] ToBytes();

        public abstract IEnumerator<IDataframeRecord> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract void Dispose();
    }

    // dataframe implementation (public interface)
    public partial class Dataframe : AbstractDataframe
    {

        // Create a blank dataframe for the specified variable and context
        public static Dataframe Create(string variableName, IContext context, int? capacity = null)
        {
            IVariable variable = context.GetVariable(variableName);
            IReadOnlyList<IDimension> dimensions = (from d in variable.Dimensions select context.GetDimension(d)).ToList();
            return new Dataframe(variable, dimensions, capacity);
        }

        // Create a single-year dataframe for the specified variable and context, filled with specified initial value
        public static Dataframe Create(string variableName, IContext context, short year, float? value)
        {
            IVariable variable = context.GetVariable(variableName);
            IReadOnlyList<IDimension> dimensions = (from d in variable.Dimensions select context.GetDimension(d)).ToList();
            return Dataframe.Allocate(variable, dimensions, year, year, value);
        }

        // Create a year-range dataframe for the specified variable and context, filled with specified initial value
        public static Dataframe Create(string variableName, IContext context, short startYear, short endYear, float? value)
        {
            IVariable variable = context.GetVariable(variableName);
            IReadOnlyList<IDimension> dimensions = (from d in variable.Dimensions select context.GetDimension(d)).ToList();
            return Dataframe.Allocate(variable, dimensions, startYear, endYear, value);
        }

        public override IVariable Variable { get; }

        public override IReadOnlyList<IDimension> Dimensions { get; }

        public Dataframe(IVariable variable, IReadOnlyList<IDimension> dimensions, int? capacity = null) : this(variable, capacity)
        {
            this.Variable = variable;
            this.Dimensions = dimensions;
        }

        public override IDataframe Append(params object[] values)
        {
            return Append(new[] { values });
        }

        public override IQueryBuilder Filter(IQueryFilter pred = null)
        {
            if (pred == null) {
                return new DataframeQueryBuilder(this);
            }
            return new DataframeQueryBuilder(this).Filter(pred);
        }
    }

}
