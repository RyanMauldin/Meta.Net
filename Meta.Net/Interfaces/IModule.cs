namespace Meta.Net.Interfaces
{
    public interface IModule : ISchema
    {
        string Definition { get; set; }
        bool UsesAnsiNulls { get; set; }
        bool UsesQuotedIdentifier { get; set; }
    }
}