using System.Runtime.Serialization;

namespace Meta.Net.Data.Interfaces
{
    public interface IDataObject : ISerializable
    {
		#region Data Members (2) 
        
        string Description { get; set; }

        string ObjectName { get; set; }

		#endregion Data Members 
    }
}
