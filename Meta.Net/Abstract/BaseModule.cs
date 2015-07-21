using System.Runtime.Serialization;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    [DataContract]
    [KnownType(typeof(AggregateFunction))]
    [KnownType(typeof(InlineTableValuedFunction))]
    [KnownType(typeof(ScalarFunction))]
    [KnownType(typeof(StoredProcedure))]
    [KnownType(typeof(TableValuedFunction))]
    [KnownType(typeof(Trigger))]
    [KnownType(typeof(View))]
    public abstract class BaseModule : SchemaBasedObject, IModule
    {
        [DataMember] public string Definition { get; set; }
        [DataMember] public bool UsesAnsiNulls { get; set; }
        [DataMember] public bool UsesQuotedIdentifier { get; set; }

        protected BaseModule()
        {
            Definition = string.Empty;
            UsesAnsiNulls = true;
            UsesQuotedIdentifier = true;
        }

        public static bool CompareDefinitions(IModule sourceModule, IModule targetModule)
        {
            if (!CompareObjectNames(sourceModule, targetModule))
                return false;

            if (sourceModule.UsesAnsiNulls != targetModule.UsesAnsiNulls)
                return false;

            if (sourceModule.UsesQuotedIdentifier != targetModule.UsesQuotedIdentifier)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceModule.Definition, targetModule.Definition) == 0;
        }
    }
}
