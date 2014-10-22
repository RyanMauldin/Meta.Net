using System.Data.Common;

namespace Meta.Net.Interfaces
{
    /// <summary>
    /// Interface for defining common database connection information properties.
    /// </summary>
    public interface IDataConnectionInfo
    {
		/// <summary>
        /// ConnectionString is generated from the class members.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Data Source of the connection.
        /// </summary>
        string DataSource { get; set; }

        /// <summary>
        /// Initial Catalog of the connection.
        /// </summary>
        string InitialCatalog { get; set; }

        /// <summary>
        /// Endpoint name, not used in ConnectionString property.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Password of the connection.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// User Id of the connection.
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        /// The data context of specific server:
        /// SqlServer, MySQL, DB2, etc...
        /// </summary>
        DataContext DataContext { get; set; }

        DbConnection CreateDbConnection();
        DbDataAdapter CreateDataAdapter();
    }
}