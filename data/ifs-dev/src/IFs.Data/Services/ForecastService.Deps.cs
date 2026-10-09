using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;

namespace IFs.Data 
{

    public abstract partial class ForecastServiceBase {

        // find all attributes of the specified MemberInfo matching the specified type
        internal static IEnumerable<A> FindAttributes<A>(MemberInfo info) where A: System.Attribute {
            foreach (var obj in info.GetCustomAttributes(true)) {
                var attr = obj as A;
                if (attr != null) {
                    yield return attr;
                }
            }
        }

        internal static IEnumerable<InitializerInfo> FindModelInitializers(Type modelType, ExecutionStage stage) {
            foreach (var m in modelType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod)) {
                foreach (var attr in FindAttributes<IFsModelInitAttribute>(m)) {
                    if ((attr.Stage & stage) != 0) {
                        yield return new InitializerInfo() {
                            Stage = attr.Stage,
                            ModelType = modelType,
                            Method = m,
                        };
                    }
                }
            }
        }

        internal static IEnumerable<ProducerInfo> FindModelProducers(Type modelType, ExecutionStage stage) {
            foreach (var m in modelType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod)) {
                // analyze rebuild producers for dependencies
                if ((ExecutionStage.Rebuild & stage) != 0) {
                    var producers = FindAttributes<RebuildsVariableAttribute>(m).Select(attr => new Tuple<VariableInfo, AggregatorInfo>(
                        new VariableInfo() {
                            Name = attr.VariableName,
                            Scope = ProducerScope.FirstYearOnly,
                            Description = attr.VariableName,
                        },
                        // in case no custom aggregator is specified for the variable, use a dummy aggregator as placeholder,
                        // this will be replaced by the real default aggregator configured for the rebuild context.
                        AnalyzeAggregator(attr.AggregatorType ?? typeof(DummyAggregator))
                    )).ToArray();
                    var exports = FindAttributes<ExportsAssetAttribute>(m).Select(attr => new AssetInfo() {
                        Name = attr.AssetName,
                        Description = attr.Description,
                    }).ToArray();
                    var imports = FindAttributes<ImportsAssetAttribute>(m).Select(attr => new AssetInfo() {
                        Name = attr.AssetName,
                        Description = attr.Description,
                    }).ToArray();
                    if (producers.Length < 1) {
                        if (exports.Length > 0) {
                            throw new ArgumentException($"Non-producer method {modelType.FullName}.{m.Name} cannot export series.");
                        }
                        if (imports.Length > 1) {
                            throw new ArgumentException($"Non-producer method {modelType.FullName}.{m.Name} cannot import series.");
                        }
                        continue;
                    }
                    // a rebuilder may contain duplicated VariableInfo by mistake, such errors would be discovered when constructing
                    // the dependency graph where the mapping from VariableInfo to ProducerInfo would be collected into a dictionary.
                    yield return new RebuilderInfo() {
                        ModelType = modelType,
                        Method = m,
                        ProducesVariables = producers.Select(tup => tup.Item1).ToArray(),
                        Aggregators = producers.Select(tup => tup.Item2).ToArray(),
                        ExportedAssets = exports,
                        ImportedAssets = imports,
                    };
                }
                // analyze forecast producers for dependencies
                if ((ExecutionStage.Forecast & stage) != 0) {
                    // producers
                    var producers = FindAttributes<ForecastsVariableAttribute>(m).Select(attr => new VariableInfo() {
                        Name = attr.VariableName,
                        Scope = attr.Scope,
                        Description = attr.Description
                    }).ToArray();
                    // union of all scopes
                    var unionScope = producers.Select(p => p.Scope).Aggregate(ProducerScope.None, (a, b) => a | b);
                    // consumers
                    var consumers = FindAttributes<ConsumesVariableAttribute>(m).Select(attr => new VariableInfo() {
                        Name = attr.VariableName,
                        Scope = unionScope,
                        Description = attr.Description,
                    }).ToArray();
                    if (producers.Length < 1) {
                        if (consumers.Length > 0) {
                            throw new ArgumentException($"Non-producer method {modelType.FullName}.{m.Name} cannot consume variables.");
                        }
                        continue;
                    }
                    yield return new ForecasterInfo() {
                        ModelType = modelType,
                        Method = m,
                        // TODO: what if forecasters contains duplicated VariableInfo?
                        ProducesVariables = producers,
                        ConsumesVariables = consumers,
                    };
                }
            }
        }

        internal static AggregatorInfo AnalyzeAggregator(Type aggregatorType) {
            if (!typeof(IAggregator).IsAssignableFrom(aggregatorType)) {
                throw new ArgumentException($"{aggregatorType.FullName} is not an IAggregator.");
            }
            var exports = new List<ExportsAssetAttribute>();
            var imports = new List<ImportsAssetAttribute>();
            // analyze aggregator methods
            foreach (var method in aggregatorType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                exports.AddRange(FindAttributes<ExportsAssetAttribute>(method));
                imports.AddRange(FindAttributes<ImportsAssetAttribute>(method));
            }
            // analyze aggregator fields
            foreach (var field in aggregatorType.GetFields()) {
                imports.AddRange(FindAttributes<ImportsAssetAttribute>(field));
            }
            // analyze aggregator properties
            foreach (var prop in aggregatorType.GetProperties()) {
                imports.AddRange(FindAttributes<ImportsAssetAttribute>(prop));
            }
            return new AggregatorInfo() {
                AggregatorType = aggregatorType,
                ExportsAsset = exports.Select(attr => new AssetInfo() {
                    Name = attr.AssetName,
                    Description = attr.Description,
                }).DistinctBy(s => s.Name).ToArray(),
                ImportsAssets = imports.Select(attr => new AssetInfo() {
                    Name = attr.AssetName,
                    Description = attr.Description,
                }).DistinctBy(s => s.Name).ToArray(),
            };
        }

        internal static ModelInfo AnalyzeModel(Type modelType, IFsModelAttribute attr) {

            Debug.WriteLine($"finding model initializers");
            var initializers = new List<InitializerInfo>();
            Debug.Indent();
            initializers.AddRange(FindModelInitializers(modelType, ExecutionStage.Rebuild));
            initializers.AddRange(FindModelInitializers(modelType, ExecutionStage.Forecast));
            Debug.Unindent();
        
            Debug.WriteLine($"finding model dependencies");
            var producers = new List<ProducerInfo>();
            Debug.Indent();
            producers.AddRange(FindModelProducers(modelType, ExecutionStage.Rebuild));
            producers.AddRange(FindModelProducers(modelType, ExecutionStage.Forecast));
            Debug.Unindent();

            return new ModelInfo() {
                ModelType = modelType,
                Name = attr.Name,
                Description = attr.Description,
                Initializers = initializers.ToArray(),
                Producers = producers.ToArray(),
            };
        }

    }

    internal abstract class NamedInfo {
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            }
            if (!typeof(NamedInfo).IsAssignableFrom(obj.GetType())) {
                return false;
            }
            var rhs = obj as NamedInfo;
            return StringComparer.InvariantCulture.Equals(this.Name, rhs.Name);
        }

        public override int GetHashCode() {
            return StringComparer.InvariantCulture.GetHashCode(this.Name);
        }
    }

    // metadata collected from model (IFsModel) attributes
    internal sealed class ModelInfo: NamedInfo {
        public Type ModelType { get; set; }
        public override string Name { get; set; }
        public override string Description { get; set; }
        public InitializerInfo[] Initializers { get; set; }
        public ProducerInfo[] Producers { get; set; }
    }

    // series is the intermediate (pre-normalization) form of a dataframe
    internal sealed class AssetInfo: NamedInfo {
        public override string Name { get; set; }
        public override string Description { get; set; }
    }

    internal sealed class VariableInfo: NamedInfo {
        public override string Name { get; set; }
        public override string Description { get; set; }
        public ProducerScope Scope { get; set; }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            }
            if (!typeof(VariableInfo).IsAssignableFrom(obj.GetType())) {
                return false;
            }
            var rhs = obj as VariableInfo;
            return StringComparer.InvariantCulture.Equals(this.Name, rhs.Name) && this.Scope.Equals(rhs.Scope);
        }

        public override int GetHashCode() {
            int hash = 17;
            hash = hash * 31 + StringComparer.InvariantCulture.GetHashCode(this.Name);
            hash = hash * 31 + this.Scope.GetHashCode();
            return hash;
        }
    }

    internal sealed class AggregatorInfo {

        public Type AggregatorType { get; set; }

        public AssetInfo[] ExportsAsset { get; set; }

        public AssetInfo[] ImportsAssets { get; set; }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            }
            if (!typeof(AggregatorInfo).IsAssignableFrom(obj.GetType())) {
                return false;
            }
            var rhs = obj as AggregatorInfo;
            return this.AggregatorType.Equals(rhs.AggregatorType);
        }

        public override int GetHashCode() {
            return this.AggregatorType.GetHashCode();
        }
    }

    internal sealed class PreparatorInfo {
        public Type PreparatorType { get; set; }

        public override bool Equals(object obj) {
            if (this == obj) {
                return true;
            }
            if (!typeof(PreparatorInfo).IsAssignableFrom(obj.GetType())) {
                return false;
            }
            var rhs = obj as PreparatorInfo;
            return this.PreparatorType.Equals(rhs.PreparatorType);
        }

        public override int GetHashCode() {
            return this.PreparatorType.GetHashCode();
        }
    }

    internal abstract class ModelMethod {

        public abstract ExecutionStage Stage { get; set; }

        public Type ModelType { get; set; }

        public MethodInfo Method { get; set; }

        public override bool Equals(object other) {
            if (this == other) {
                return true;
            }
            if (!typeof(ModelMethod).IsAssignableFrom(other.GetType())) {
                return false;
            }
            var rhs = other as ModelMethod;
            return this.Method.Equals(rhs.Method);
        }

        public override int GetHashCode() {
            return this.Method.GetHashCode();
        }
    }

    // metadata collected from model initializer (IFsModelInit) attributes
    internal sealed class InitializerInfo: ModelMethod {

        public override ExecutionStage Stage { get; set; }

        public Action Bind<T>(T context) where T: IContext {
            var args = new List<object>();
            foreach (var p in this.Method.GetParameters()) {
                if (p.ParameterType.IsInstanceOfType(context)) {
                    args.Add(context);
                } else {
                    throw new ArgumentException($"Unexpected argument {p} to initializer {this.Method.Name}.");
                }
            }
            return () => this.Method.Invoke(this.ModelType, args.ToArray());
        }
    }

    internal abstract class ProducerInfo: ModelMethod {
        public override ExecutionStage Stage { get; set; }
        public VariableInfo[] ProducesVariables { get; set; }
    }

    // metadata collected from variable rebuilder (RebuildsVariable) attributes
    internal sealed class RebuilderInfo: ProducerInfo {
        public override ExecutionStage Stage => ExecutionStage.Rebuild;
        public AggregatorInfo[] Aggregators { get; set; }
        public AssetInfo[] ExportedAssets { get; set; }
        public AssetInfo[] ImportedAssets { get; set; }
    }

    // metadata collected from variable forecaster (ProducesVariable) attributes
    internal sealed class ForecasterInfo: ProducerInfo {
        public override ExecutionStage Stage => ExecutionStage.Forecast;
        public PreparatorInfo[] Preparators { get; set; }
        public VariableInfo[] ConsumesVariables { get; set; }
    }

    // NOTE: the DummyAggregator should never be inherited or instantiated. It is used as a place-holder during
    // model analysis for variables that haven't specified a custom aggregator in the `RebuildsVariable` attribute.
    // The actual aggregator to use during rebuild is the `DefaultAggregator` configured for the rebuild context.
    internal abstract class DummyAggregator: IAggregator
    {
        public abstract IDictionary<string, float?> Normalize(string seriesName, IRebuildFacade cntx);
        public abstract IEnumerable<KeyValuePair<int, float?>> Regionalize(string seriesName, IRebuildFacade cntx);
    }

}
