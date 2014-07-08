using System;

namespace Meta.Net.Data.Interfaces
{
    /// <summary>
    /// Interface for defining a common DataMetadata table row
    /// class that contains the following data type properties.
    /// </summary>
    public interface IDataColumnDescripterRow
    {
		#region Data Members (9) 

        string Collation { get; set; }

        string DataType { get; set; }

        bool HasDefault { get; set; }

        bool IsAssemblyType { get; set; }

        bool IsNullable { get; set; }

        bool IsUserDefined { get; set; }

        Int64 MaxLength { get; set; }

        int Precision { get; set; }

        int Scale { get; set; }

		#endregion Data Members 
    }
}
