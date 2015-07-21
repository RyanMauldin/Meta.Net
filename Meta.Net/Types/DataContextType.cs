using System.Runtime.Serialization;

namespace Meta.Net.Types
{
    [DataContract]
    public enum DataContextType
    {
        MySql,
        SqlServer
    }
}
