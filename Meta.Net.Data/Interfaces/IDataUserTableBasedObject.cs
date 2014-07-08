using Meta.Net.Data.Objects;

namespace Meta.Net.Data.Interfaces
{
    public interface IDataUserTableBasedObject : IDataSchemaBasedObject
    {
		#region Data Members (1) 

        UserTable UserTable { get; }

		#endregion Data Members 
    }
}