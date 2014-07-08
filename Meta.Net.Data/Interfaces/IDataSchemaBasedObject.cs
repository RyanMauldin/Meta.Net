using Meta.Net.Data.Objects;

namespace Meta.Net.Data.Interfaces
{
    public interface IDataSchemaBasedObject : IDataCatalogBasedObject
    {
		#region Data Members (1) 

        Schema Schema { get; }

		#endregion Data Members 
    }
}