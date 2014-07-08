using Meta.Net.Data.Objects;

namespace Meta.Net.Data.Interfaces
{

    public interface IDataCatalogBasedObject
    {
		#region Data Members (1) 
        string Namespace { get; }
        Catalog Catalog { get; }

		#endregion Data Members 
    }
}