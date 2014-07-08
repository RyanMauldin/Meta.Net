namespace Meta.Net.Data.Interfaces
{
    public interface IDataModule : IDataSchemaBasedObject
    {
		#region Data Members (4) 

        string Definition { get; set; }

        bool UsesAnsiNulls { get; set; }

        bool UsesQuotedIdentifier { get; set; }

		#endregion Data Members 
    }
}