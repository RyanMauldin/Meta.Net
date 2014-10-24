using Meta.Net.Interfaces;

namespace Meta.Net.Abstract
{
    public abstract class BaseModule : SchemaBasedObject, IModule
    {
        public string Definition { get; set; }
        public bool UsesAnsiNulls { get; set; }
        public bool UsesQuotedIdentifier { get; set; }

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
