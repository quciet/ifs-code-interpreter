using System;
using System.Collections.Generic;
using System.Linq;

namespace IFs.Data
{

    // Abstract query filter for filtering a dataframe
    public abstract class QueryFilter : IQueryFilter
    {

        // Create a query filter on the specified dimension
        public static IQueryFilter Create(FilterType type, IDimension dimension, params short[] operands)
        {
            foreach (var op in operands) {
                if (!dimension.Buckets.ContainsKey(op)) {
                    throw new ArgumentException("Invalid operand for the specified dimension.");
                }
            }
            switch (type)
            {
                case FilterType.Equal:
                    if (operands.Length != 1) {
                        throw new ArgumentException("Must specify exactly one operand.");
                    }
                    return new EqualFilter(dimension, operands[0]);
                case FilterType.NotEqual:
                    if (operands.Length != 1) {
                        throw new ArgumentException("Must specify exactly one operand.");
                    }
                    return new NotEqualFilter(dimension, operands[0]);
                case FilterType.ElementOf:
                    {
                        // reduce to the simplest filter
                        HashSet<short> opSet = new HashSet<short>(operands);
                        if (opSet.Count == dimension.Buckets.Count) {
                            // `element-of` everything is equal to `null` (not imposing any constraint)
                            return null;
                        } else if (opSet.Count == 0) {
                            // `element-of` no value is equal to `none` (always returning empty set)
                            return new ElementOfFilter(dimension);
                        } else if (opSet.Count == 1) {
                            // `element-of` one single value is equal to `equal`
                            return new EqualFilter(dimension, opSet.First());
                        } else if (opSet.Count == dimension.Buckets.Count - 1) {
                            // `element-of` everything except one single value is equal to `not-equal`
                            opSet.SymmetricExceptWith(dimension.Buckets.Keys);
                            return new NotEqualFilter(dimension, opSet.First());
                        } else {
                            short lbound = opSet.Min();
                            short ubound = opSet.Max();
                            IEnumerable<short> range = (from b in dimension.Buckets
                                                        select b.Key).Where(x => lbound <= x && x <= ubound);
                            if (opSet.SetEquals(range))
                                // `element-of` a consecutive subset of the full set is equal to `between` [lbound, ubound]
                                return new BetweenFilter(dimension, lbound, ubound);
                            return new ElementOfFilter(dimension, opSet.ToArray());
                        }
                    }
                case FilterType.Between:
                    {
                        if (operands.Length != 2) {
                            throw new ArgumentException("Must specify exactly two operands.");
                        }
                        short lbound = operands[0];
                        short ubound = operands[1];
                        if (lbound > ubound) {
                            // `between` empty range [] is equal to `none` (always returning empty set)
                            return new ElementOfFilter(dimension);
                        } else if (lbound == ubound) {
                            // `between` singleton range [a, a] is equal to `equal`
                            return new EqualFilter(dimension, lbound);
                        } else {
                            HashSet<short> opSet = new HashSet<short>(dimension.Buckets.Keys);
                            if (lbound == opSet.Min() && ubound == opSet.Max()) {
                                // `between` full range [min, max] is equal to `null` (not imposing any constraint)
                                return null;
                            }
                            return new BetweenFilter(dimension, lbound, ubound);
                        }
                    }
                default:
                    throw new ArgumentException($"Invalid filter type {type}.");
            }
        }

        // conjunction (logical and) of filters
        private static IQueryFilter Conjunct(IQueryFilter u, IQueryFilter v)
        {
            if (u.Dimension.Id != v.Dimension.Id) {
                throw new ArgumentException("Incompatible filters.");
            }
            // combine everything into an element-of filter
            HashSet<short> fullOpSet = new HashSet<short>(u.Dimension.Buckets.Keys);
            foreach (IQueryFilter p in new[] { u, v }) {
                HashSet<short> predOpSet = new HashSet<short>();
                switch (p.Type) {
                    case FilterType.Equal:
                        predOpSet.UnionWith(p.Operands);
                        break;
                    case FilterType.NotEqual:
                        predOpSet.UnionWith(p.Dimension.Buckets.Keys);
                        predOpSet.ExceptWith(p.Operands);
                        break;
                    case FilterType.ElementOf:
                        predOpSet.UnionWith(p.Operands);
                        break;
                    case FilterType.Between:
                        {
                            short lbound = p.Operands.Min();
                            short ubound = p.Operands.Max();
                            IEnumerable<short> range = (from b in u.Dimension.Buckets
                                                        select b.Key).Where(x => lbound <= x && x <= ubound);
                            predOpSet.UnionWith(range);
                            break;
                        }
                    default:
                        throw new ArgumentException($"Invalid filter type {p.Type}.");
                }
                fullOpSet.IntersectWith(predOpSet);
            }
            return QueryFilter.Create(FilterType.ElementOf, u.Dimension, fullOpSet.ToArray());
        }

        // disjunction (logical or) of filters
        private static IQueryFilter Disjunct(IQueryFilter u, IQueryFilter v)
        {
            if (u.Dimension.Id != v.Dimension.Id) {
                throw new ArgumentException("Incompatible filters.");
            }
            // combine everything into an element-of filter
            HashSet<short> fullOpSet = new HashSet<short>();
            foreach (IQueryFilter p in new[] { u, v }) {
                HashSet<short> predOpSet = new HashSet<short>();
                switch (p.Type) {
                    case FilterType.Equal:
                        predOpSet.UnionWith(p.Operands);
                        break;
                    case FilterType.NotEqual:
                        predOpSet.UnionWith(p.Dimension.Buckets.Keys);
                        predOpSet.ExceptWith(p.Operands);
                        break;
                    case FilterType.ElementOf:
                        predOpSet.UnionWith(p.Operands);
                        break;
                    case FilterType.Between:
                        {
                            short lbound = p.Operands.Min();
                            short ubound = p.Operands.Max();
                            IEnumerable<short> range = (from b in u.Dimension.Buckets
                                                        select b.Key).Where(x => lbound <= x && x <= ubound);
                            predOpSet.UnionWith(range);
                            break;
                        }
                    default:
                        throw new ArgumentException($"Invalid filter type {p.Type}.");
                }
                fullOpSet.UnionWith(predOpSet);
            }
            return QueryFilter.Create(FilterType.ElementOf, u.Dimension, fullOpSet.ToArray());
        }

        public FilterType Type { get; }

        public IDimension Dimension { get; }

        public abstract IEnumerable<short> Operands { get; }

        public abstract Func<short, bool> Matcher { get; }

        protected QueryFilter(FilterType type, IDimension dimension)
        {
            this.Type = type;
            this.Dimension = dimension;
        }

        public virtual IQueryFilter Conjunct(IQueryFilter other)
        {
            return QueryFilter.Conjunct(this, other);
        }

        public virtual IQueryFilter Disjunct(IQueryFilter other)
        {
            return QueryFilter.Disjunct(this, other);
        }
    }

    // Equal Filter (=)
    public class EqualFilter : QueryFilter
    {
        public override IEnumerable<short> Operands
        {
            get
            {
                yield return this._operand;
            }
        }

        public override Func<short, bool> Matcher
        {
            get
            {
                return (v) => v == this._operand;
            }
        }

        private readonly short _operand;

        public EqualFilter(IDimension dimension, short operand) : base(FilterType.Equal, dimension)
        {
            this._operand = operand;
        }
    }

    // Not-Equal filter (<>)
    public class NotEqualFilter : QueryFilter
    {
        public override IEnumerable<short> Operands
        {
            get
            {
                yield return this._operand;
            }
        }

        public override Func<short, bool> Matcher
        {
            get
            {
                return (v) => v != this._operand;
            }
        }

        private readonly short _operand;

        public NotEqualFilter(IDimension dimension, short operand) : base(FilterType.NotEqual, dimension)
        {
            this._operand = operand;
        }
    }

    // Element-Of filter
    public class ElementOfFilter : QueryFilter
    {
        public override IEnumerable<short> Operands
            => this._operands;

        public override Func<short, bool> Matcher
        {
            get
            {
                return (v) => this._operands.Contains(v);
            }
        }

        private readonly HashSet<short> _operands;

        public ElementOfFilter(IDimension dimension, params short[] operands) : base(FilterType.ElementOf, dimension)
        {
            this._operands = new HashSet<short>(operands);
        }
    }

    // Between (inclusive range [a, b]) filter
    public class BetweenFilter : QueryFilter
    {
        public override IEnumerable<short> Operands
        {
            get
            {
                yield return this._lbound;
                yield return this._ubound;
            }
        }

        public override Func<short, bool> Matcher
        {
            get
            {
                return (v) => (v >= this._lbound) && (v <= this._ubound);
            }
        }

        private readonly short _lbound;

        private readonly short _ubound;

        public BetweenFilter(IDimension dimension, short lbound, short ubound) : base(FilterType.ElementOf, dimension)
        {
            this._lbound = lbound;
            this._ubound = ubound;
        }
    }

}
