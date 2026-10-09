using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using QuickGraph;
using QuickGraph.Algorithms.TopologicalSort;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace IFs.Data 
{

    public abstract partial class ForecastServiceBase {

        public IModelPlanner<IRebuildContext> GetPlanner(IRebuildContext context) {
            // initialize all models
            Debug.WriteLine($"initializing models for rebuild");
            Debug.Indent();
            foreach (var mod in this.Models) {
                Debug.WriteLine($"finding initializers in {mod.Key}");
                Debug.Indent();
                foreach (var info in mod.Value.Initializers.Where(c => (c.Stage & ExecutionStage.Rebuild) != 0)) {
                    Debug.WriteLine($"- {info.Method.Name}");
                    Debug.Indent();
                    info.Bind(context)();
                    Debug.Unindent();
                }
                Debug.Unindent();
            }
            Debug.Unindent();
            return new RebuildPlanner(this.Models, context);
        }

        public IModelPlanner<IForecastContext> GetPlanner(IForecastContext context) {
            // initialize all models
            Debug.WriteLine($"initializing models for forecast");
            Debug.Indent();
            foreach (var mod in this.Models) {
                Debug.WriteLine($"finding initializers in {mod.Key}");
                Debug.Indent();
                foreach (var info in mod.Value.Initializers.Where(c => (c.Stage & ExecutionStage.Forecast) != 0)) {
                    Debug.WriteLine($"- {info.Method.Name}");
                    Debug.Indent();
                    info.Bind(context)();
                    Debug.Unindent();
                }
                Debug.Unindent();
            }
            Debug.Unindent();
            return new ForecastPlanner(this.Models, context);
        }
    }

    // captures the dependencies among variable rebuilders, the source is the producer
    // of one or more series (country-based slices of variables), and the target is the
    // consumer (of some or all) of the series produced by the source.
    internal sealed class RebuilderDependency: IEdge<RebuilderInfo> {
        public RebuilderInfo Source { get; internal set; }
        public RebuilderInfo Target { get; internal set; }
        // the intersection of Source.ExportsAsset and Target.ImportsAsset
        public AssetInfo Asset { get; internal set; }
    }

    // captures the dependencies among variable forecasters, the source is the producer
    // of one or more dataframes (annual slice) of variables, and the target is the
    // consumer (of some or all) of the dataframes produced by the source.
    internal sealed class ForecasterDependency: IEdge<ForecasterInfo> {
        public ForecasterInfo Source { get; internal set; }
        public ForecasterInfo Target { get; internal set; }
        // the intersection of Source.ProducesVariables and Target.ConsumesVariables
        public VariableInfo Asset { get; internal set; }
        public ProducerScope Scope { get; set; }
    }

    // initialize models 
    internal sealed class RebuildPlanner: IModelPlanner<IRebuildContext> {

        // context for planning rebuild base execution
        public readonly IRebuildContext Context;

        // Interaction graph of variables during rebuild base. Vertices of the graph are model methods with the
        // `RebuildsVariable` attribute; directed edges X -> Y of the graph represents the dependecency between
        // two model methods X and Y, in that one more series (country-based slices of variables) produced by
        // X are consumed by Y, as the `ImportsAsset` attribute specifies either on the model methods, or on the
        // aggregators of the rebuilders. There may be parallel edges between two vertices, e.g., there can be
        // more than one variables produced by one source vertex X that are consumed by another target vertex Y.
        public readonly BidirectionalGraph<RebuilderInfo, RebuilderDependency> Graph;

        // index for looking up the rebuilder given the variable to produce
        public readonly IReadOnlyDictionary<VariableInfo, RebuilderInfo> VerticesIndex;

        public RebuildPlanner(IEnumerable<KeyValuePair<string, ModelInfo>> models, IRebuildContext context) {
            this.Context = context;
            // build dependency graph
            Debug.WriteLine($"computing rebuild graph");
            var variableProducers = new Dictionary<VariableInfo, RebuilderInfo>();
            var seriesExporters = new Dictionary<AssetInfo, RebuilderInfo>();
            var seriesImporters = new Dictionary<AssetInfo, ISet<RebuilderInfo>>();
            Debug.Indent();
            foreach (var m in models) {
                Debug.WriteLine($"finding dependencies in {m.Key}");
                Debug.Indent();
                foreach (var rebuilder in m.Value.Producers.Where(p => (p.Stage & ExecutionStage.Rebuild) != 0).Select(p => p as RebuilderInfo)) {
                    Debug.WriteLine($"- {rebuilder.Method.Name}");
                    Debug.Indent();
                    foreach (var s in rebuilder.ImportedAssets) {
                        if (!seriesImporters.ContainsKey(s)) {
                            seriesImporters.Add(s, new HashSet<RebuilderInfo>());
                        }
                        seriesImporters[s].Add(rebuilder);
                        Debug.WriteLine($"[IMP] {s.Name}");
                    }
                    foreach (var s in rebuilder.ExportedAssets.Distinct()) {
                        if (seriesExporters.ContainsKey(s)) {
                            throw new ArgumentException($"Series {s.Name} has more than one source.");
                        }
                        seriesExporters.Add(s, rebuilder);
                        Debug.WriteLine($"[EXP] {s.Name}");
                    }
                    foreach (var agg in rebuilder.Aggregators.Distinct()) {
                        foreach (var s in agg.ImportsAssets) {
                            if (!seriesImporters.ContainsKey(s)) {
                                seriesImporters.Add(s, new HashSet<RebuilderInfo>());
                            }
                            seriesImporters[s].Add(rebuilder);
                            Debug.WriteLine($"[IMP] {s.Name} (via {agg.AggregatorType.Name})");
                        }
                        foreach (var s in agg.ExportsAsset) {
                            if (seriesExporters.ContainsKey(s)) {
                                throw new ArgumentException($"Series {s.Name} has more than one source.");
                            }
                            seriesExporters.Add(s, rebuilder);
                            Debug.WriteLine($"[EXP] {s.Name} (via {agg.AggregatorType.Name})");
                        }
                    }
                    foreach (var v in rebuilder.ProducesVariables) {
                        variableProducers.Add(v, rebuilder);
                        Debug.WriteLine($"[OUT] {v.Name}");
                    }
                    Debug.Unindent();
                }
                Debug.Unindent();
            }
            // identify broken dependencies (consumers without corresponding producers)
            var broken = new HashSet<AssetInfo>(seriesImporters.Keys.Except(seriesExporters.Keys));
            if (broken.Count > 0) {
                Debug.WriteLine($"found broken dependency");
                Debug.Indent();
                foreach (var info in broken) {
                    foreach (var prod in seriesImporters[info]) {
                        Debug.WriteLine($"{info.Name}: imported by {prod.ModelType.Name}/{prod.Method.Name}");
                    }
                    throw new KeyNotFoundException($"Broken dependency: {info.Name}");
                }
                Debug.Unindent();
            }
            // generate dependency graph
            var edges = (
                from s1 in seriesExporters
                join s2 in (
                    from s in seriesImporters
                    from r in s.Value
                    select new KeyValuePair<AssetInfo, RebuilderInfo>(s.Key, r)
                ) on s1.Key equals s2.Key
                select new RebuilderDependency() {
                    Source = s1.Value,
                    Target = s2.Value,
                    Asset = s1.Key
                }
            );
            this.Graph = new BidirectionalGraph<RebuilderInfo, RebuilderDependency>(true);
            this.Graph.AddVertexRange(variableProducers.Values.Distinct());
            this.Graph.AddEdgeRange(edges);
            this.VerticesIndex = variableProducers;
            Debug.Unindent();
        }

        public IEnumerable<VariableProducer> Plan(short year) {
            Trace.Assert(year == this.Context.FirstYear);
            // register aggregators
            Debug.WriteLine($"registering aggregators");
            // register model-specified aggregators
            foreach (var rebuilder in this.Graph.Vertices) {
                foreach (var va in rebuilder.ProducesVariables.Zip(rebuilder.Aggregators, (v, agg) => new KeyValuePair<VariableInfo, AggregatorInfo>(v, agg))) {
                    // register the aggregator type to the variable
                    this.Context.PutAggregator(va.Key.Name, va.Value.AggregatorType);
                }
            }
            Debug.WriteLine($"planning for {year}");
            Debug.Indent();
            var algo = new TopologicalSortAlgorithm<RebuilderInfo, RebuilderDependency>(this.Graph);
            algo.Compute();
            foreach (var v in algo.SortedVertices) {
                Debug.WriteLine($"- {v.ModelType.Name}.{v.Method.Name}");
                Debug.Indent();
                foreach (var imp in this.Graph.InEdges(v)) {
                    Debug.WriteLine($"[IMP] {imp.Asset.Name} (from {imp.Source.ModelType}.{imp.Source.Method.Name})");
                }
                foreach (var exp in this.Graph.OutEdges(v)) {
                    Debug.WriteLine($"[EXP] {exp.Asset.Name} (to {exp.Target.ModelType}.{exp.Target.Method.Name})");
                }
                foreach (var ou in v.ProducesVariables) {
                    Debug.WriteLine($"[OUT] {ou.Name}");
                }
                Debug.Unindent();
                yield return this.Bind(v, this.Context);
            }
            Debug.Unindent();
        }

        // For all aggregator types discovered from the rebuilder (via the `RebuildsVariable` attribute), generate an action
        // that, when executed, assign matching series to the properties of respective aggregator instances where the series
        // are wired (via the `ImportsAsset` attribute). Such actions are invoked before the rebuilder itself is invoked.
        private IEnumerable<Action> BeforeRebuildJobs(RebuilderInfo rebuilder, IRebuildContext context) {
            foreach (var agg in rebuilder.Aggregators) {
                var aggInst = context.Components.GetService(agg.AggregatorType);
                foreach (var prop in agg.AggregatorType.GetProperties().Where(p => p.CanWrite)) {
                    var importCount = 0;
                    foreach (var attr in ForecastServiceBase.FindAttributes<ImportsAssetAttribute>(prop)) {
                        var setter = prop.GetSetMethod(true);
                        if (importCount > 0) {
                            throw new ArgumentException($"Property {agg.AggregatorType.Name}.{prop.Name} is wired to multiple assets.");
                        }
                        importCount += 1;
                        yield return () => {
                            var value = context.Fetch<object>(attr.AssetName);
                            if (!prop.PropertyType.IsAssignableFrom(value.GetType())) {
                                throw new ArgumentException($"Invalid type for asset {attr.AssetName}, expected {prop.PropertyType.Name}");
                            }
                            setter.Invoke(aggInst, new object [] { value });
                        };
                    }
                }
            }
        }

        private IEnumerable<Action> AfterRebuildJobs(RebuilderInfo rebuilder, IRebuildContext context) {
            // TODO
            yield break;
        }

        private VariableProducer Bind(RebuilderInfo rebuilder, IRebuildContext context) {
            var beforeJobs = this.BeforeRebuildJobs(rebuilder, context).ToArray();
            var afterJobs = this.AfterRebuildJobs(rebuilder, context).ToArray();
            var args = new object[] { context };
            if (typeof(IEnumerable<IDataframe>).IsAssignableFrom(rebuilder.Method.ReturnType)) {
                // bulk producer
                return () => {
                    foreach (var job in beforeJobs) {
                        job();
                    }
                    var ret = rebuilder.Method.Invoke(rebuilder.ModelType, args) as IEnumerable<IDataframe>;
                    foreach (var job in afterJobs) {
                        job();
                    }
                    return ret;
                };
            } else if (typeof(IDataframe).IsAssignableFrom(rebuilder.Method.ReturnType)) {
                // single producer
                return () => {
                    foreach (var job in beforeJobs) {
                        job();
                    }
                    var ret = Enumerable.Repeat<IDataframe>(
                        rebuilder.Method.Invoke(rebuilder.ModelType, args) as IDataframe, 1);
                    foreach (var job in afterJobs) {
                        job();
                    }
                    return ret;
                };
            } else {
                throw new ArgumentException($"Unexpected return type {rebuilder.Method.ReturnType} of producer {rebuilder.Method.Name}.");
            }
        }

    }

    internal sealed class ForecastPlanner: IModelPlanner<IForecastContext> {

        private static ProducerScope[] NON_OVERLAP_SCOPES = new ProducerScope[] {
            ProducerScope.FirstYearOnly,
            ProducerScope.SecondYearOnward
        };

        // context for planning run model execution
        public readonly IForecastContext Context;

        // interaction graph of variables during run models. Vertices of the graph are model methods with the
        // `ForecastsVariable` attribute; directed edges X -> Y of the graph represents the dependecency between
        // two model methods X and Y, in that one more variables (annual dataframes) produced by X are consumed by
        // Y, as expressed by the `ConsumesVariable` attributes on the forecaster methods. There may be parallel
        // edges between two vertices, e.g., there can be more than one variables produced by one source vertex X
        // that are consumed by another target vertex Y.
        public readonly IDictionary<ProducerScope, BidirectionalGraph<ForecasterInfo, ForecasterDependency>> Graph;

        // index for looking up the forecaster given the variable to produce
        public readonly IReadOnlyDictionary<VariableInfo, ForecasterInfo> VerticesIndex;

        public ForecastPlanner(IEnumerable<KeyValuePair<string, ModelInfo>> models, IForecastContext context) {
            this.Context = context;
            // build full-horizon dependency graph
            Debug.WriteLine($"computing forecast graph");
            var variableProducers = new Dictionary<VariableInfo, ForecasterInfo>();
            var variableConsumers = new Dictionary<VariableInfo, ISet<ForecasterInfo>>();
            Debug.Indent();
            foreach (var m in models) {
                Debug.WriteLine($"finding dependencies in {m.Key}");
                Debug.Indent();
                foreach (var forecaster in m.Value.Producers.Where(p => (p.Stage & ExecutionStage.Forecast) != 0).Select(p => p as ForecasterInfo)) {
                    Debug.WriteLine($"- {forecaster.Method.Name}");
                    Debug.Indent();
                    // split up the scope of consumer-side variable info into non-overlapping segments 
                    foreach (var s in NON_OVERLAP_SCOPES) {
                        foreach (var v in forecaster.ConsumesVariables.Where(v => (v.Scope & s) != 0)) {
                            var seg = new VariableInfo() {
                                Name = v.Name,
                                Scope = s,
                                Description = v.Description,
                            };
                            if (!variableConsumers.ContainsKey(seg)) {
                                variableConsumers.Add(seg, new HashSet<ForecasterInfo>());
                            }
                            variableConsumers[seg].Add(forecaster);
                            Debug.WriteLine($"[IN] {v.Name}/{s}");
                        }
                    }
                    // split up the scope of producer-side variable info into non-overlapping segments 
                    foreach (var s in NON_OVERLAP_SCOPES) {
                        foreach (var v in forecaster.ProducesVariables.Where(v => (v.Scope & s) != 0)) {
                            var seg = new VariableInfo() {
                                Name = v.Name,
                                Scope = s,
                                Description = v.Description,
                            };
                            if (variableProducers.ContainsKey(seg)) {
                                throw new ArgumentException($"Variable {seg.Name}/{s} has more than one source.");
                            }
                            variableProducers.Add(seg, forecaster);
                            Debug.WriteLine($"[OUT] {v.Name}/{s}");
                        }
                    }
                    Debug.Unindent();
                }
                Debug.Unindent();
            }
            // identify broken dependencies (consumers without corresponding producers)
            var broken = new HashSet<VariableInfo>(variableConsumers.Keys.Except(variableProducers.Keys));
            if (broken.Count > 0) {
                Debug.WriteLine($"found broken dependency");
                Debug.Indent();
                foreach (var info in broken) {
                    foreach (var prod in variableConsumers[info]) {
                        Debug.WriteLine($"{info.Name}/{info.Scope}: required by {prod.ModelType.Name}/{prod.Method.Name}");
                    }
                    throw new KeyNotFoundException($"Broken dependency: {info.Name}/{info.Scope}");
                }
                Debug.Unindent();
            }
            // generate dependency graphs for each non-overlapping scope
            var vertices = variableProducers.Values.Distinct();
            var edges = (
                from v1 in variableProducers join v2 in (
                    from v in variableConsumers
                    from f in v.Value
                    select new KeyValuePair<VariableInfo, ForecasterInfo>(v.Key, f)
                ) on v1.Key.Name equals v2.Key.Name
                where (v1.Key.Scope & v2.Key.Scope) != 0
                select new ForecasterDependency() {
                    Source = v1.Value,
                    Target = v2.Value,
                    Asset = v1.Key,
                    Scope = (v1.Key.Scope & v2.Key.Scope)
                }
            );
            this.Graph = new Dictionary<ProducerScope, BidirectionalGraph<ForecasterInfo, ForecasterDependency>>();
            foreach (var s in NON_OVERLAP_SCOPES) {
                var g = new BidirectionalGraph<ForecasterInfo, ForecasterDependency>(true);
                g.AddVertexRange(vertices.Where(v => v.ProducesVariables.Any(pv => (pv.Scope & s) != 0)));
                g.AddEdgeRange(edges.Where(e => (e.Scope & s) != 0));
                this.Graph.Add(s, g);
            }
            this.VerticesIndex = variableProducers;
            Debug.Unindent();
        }

        private ProducerScope GetNonOverlapScope(short year) {
            if (year == this.Context.FirstYear) {
                return ProducerScope.FirstYearOnly;
            } else if ((year > this.Context.FirstYear) && (year <= this.Context.LastYear)) {
                return ProducerScope.SecondYearOnward;
            }
            throw new ArgumentOutOfRangeException($"Year {year} exceeded time horizon [{this.Context.FirstYear}, {this.Context.LastYear}]");
        }

        public IEnumerable<VariableProducer> Plan(short year) {
            Trace.Assert(year >= this.Context.FirstYear && year <= this.Context.LastYear);
            Debug.WriteLine($"planning for {year}");
            Debug.Indent();
            // get year-specific dependency graph and compute call order
            var scope = this.GetNonOverlapScope(year);
            Debug.WriteLine($"scope: {scope}");
            var graph = this.Graph[scope];
            var algo = new TopologicalSortAlgorithm<ForecasterInfo, ForecasterDependency>(graph);
            algo.Compute();
            foreach (var v in algo.SortedVertices) {
                Debug.WriteLine($"- {v.ModelType.Name}.{v.Method.Name}");
                Debug.Indent();
                foreach (var inp in graph.InEdges(v).Where(dep => (dep.Scope & scope) != 0)) {
                    Debug.WriteLine($"[INP] {inp.Asset.Name}/{inp.Scope} (from {inp.Source.ModelType}.{inp.Source.Method.Name})");
                }
                foreach (var ou in v.ProducesVariables.Where(pv => (pv.Scope & scope) != 0)) {
                    Debug.WriteLine($"[OUT] {ou.Name}/{ou.Scope}");
                }
                Debug.Unindent();
                yield return this.Bind(v, year, this.Context);
            }
            Debug.Unindent();
        }

        private VariableProducer Bind(ForecasterInfo forecaster, short year, IForecastContext context) {
            var args = new object[] { year, context };
            if (typeof(IEnumerable<IDataframe>).IsAssignableFrom(forecaster.Method.ReturnType)) {
                // bulk producer
                return () => forecaster.Method.Invoke(forecaster.ModelType, args) as IEnumerable<IDataframe>;
            } else if (typeof(IDataframe).IsAssignableFrom(forecaster.Method.ReturnType)) {
                // singleton producer
                return () => Enumerable.Repeat<IDataframe>(forecaster.Method.Invoke(forecaster.ModelType, args) as IDataframe, 1);
            } else {
                throw new ArgumentException($"Unexpected return type {forecaster.Method.ReturnType} of producer {forecaster.Method.Name}.");
            }
        }

    }

}
