using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Abstract
{
    public abstract class BaseModule : SchemaBasedObject, IModule
    {
        public string Definition { get; set; }
        public bool UsesAnsiNulls { get; set; }
        public bool UsesQuotedIdentifier { get; set; }

        protected static void Init(BaseModule baseDataModule, Schema schema, string objectName)
        {
            baseDataModule.Schema = schema;
            baseDataModule.ObjectName = GetDefaultObjectName(baseDataModule, objectName);
            baseDataModule.Definition = "";
            baseDataModule.UsesAnsiNulls = true;
            baseDataModule.UsesQuotedIdentifier = true;
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
