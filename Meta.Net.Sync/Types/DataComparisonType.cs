namespace Meta.Net.Sync.Types
{
    /// <summary>
    /// Whether or not to check for namespaces, namespaces and object
    /// definitions, or to only check namespaces on schema level data
    /// objects such as the existence of modules or user-tables.
    /// </summary>
    public enum DataComparisonType
    {
        Namespaces = 0,
        Definitions = 1,
        SchemaLevelNamespaces = 2
    }
}
