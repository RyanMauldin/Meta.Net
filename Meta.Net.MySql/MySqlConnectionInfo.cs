using System;
using System.Data.Common;
using System.Linq;
using System.Xml.Serialization;
using Meta.Net.Interfaces;
using Meta.Net.Objects;
using MySql.Data.MySqlClient;

namespace Meta.Net.MySql
{
    public class MySqlConnectionInfo : DataConnectionInfo
    {
		/// <summary>
        /// The base version allowed for MySql. 5.0.0.0 = 2003.
        /// </summary>
        [XmlIgnore]
        public Version AcceptableVersion { get; private set; }

        /// <summary>
        /// Is set to IntegratedSecurity in the constructors when UserID and Password have
        /// a length of 0. This was left public to be adjusted as needed for the
        /// ConnectionString property.
        /// </summary>
        public MySqlAuthenticationType AuthenticationType { get; set; }

        /// <summary>
        /// ConnectionString is generated from the class members.
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                switch (AuthenticationType)
                {
                    case MySqlAuthenticationType.MySql:
                        return string.Format(
                              "Server={0};Database={1};Uid={2};Pwd={3};"
                            , DataSource
                            , InitialCatalog
                            , UserId
                            , Password
                        );
                }

                return base.ConnectionString;
            }
        }

        /// <summary>
        /// Version object for camparison between connections. This will remain null
        /// until the SqlConnection has been made. This gets populated when a sqlConnection
        /// object is passed into IsAcceptableVersion.
        /// </summary>
        [XmlIgnore]
        public Version Version { get; private set; }

		/// <summary>
        /// Constructor used when all members have been defined. Defaults AuthenticationType
        /// to IntegratedSecurity when UserID and Password have a length of 0.
        /// </summary>
        /// <param name="name">Endpoint name, not used in ConnectionString property.</param>
        /// <param name="dataSource">Data Source of the connection.</param>
        /// <param name="initialCatalog">Initial Catalog of the connection.</param>
        /// <param name="userId">User Id of the connection.</param>
        /// <param name="password">Password of the connection.</param>
        /// <param name="dataContext">The data context.</param>
        public MySqlConnectionInfo(string name, string dataSource, string initialCatalog, string userId, string password, DataContext dataContext = null)
            : base(name, dataSource, initialCatalog, userId, password, dataContext)
        {
            DataContext = dataContext ?? new MySqlContext();
            AcceptableVersion = new Version(5, 0, 0, 0);
            AuthenticationType = MySqlAuthenticationType.MySql;
        }

        /// <summary>
        /// Constructor used when UserID, or Password are not defined. Defaults AuthenticationType
        /// to IntegratedSecurity.
        /// </summary>
        /// <param name="name">Endpoint name, not used in ConnectionString property.</param>
        /// <param name="dataSource">Data Source of the connection.</param>
        /// <param name="initialCatalog">Initial Catalog of the connection.</param>
        /// <param name="dataContext">The data context.</param>
        public MySqlConnectionInfo(string name, string dataSource, string initialCatalog, DataContext dataContext)
            : base(name, dataSource, initialCatalog, dataContext)
        {
            DataContext = dataContext ?? new MySqlContext();
            AcceptableVersion = new Version(5, 0, 0, 0);
            AuthenticationType = MySqlAuthenticationType.MySql;
        }

        /// <summary>
        /// Constructor that accepts any ConnectionInfo object that implements the
        /// IDataConnectionInfo interface and initializes its members to the
        /// IDatatConnectionInfo object member definitions. Defaults AuthenticationType
        /// to IntegratedSecurity when UserID and Password have a length of 0.
        /// </summary>
        /// <param name="connectionInfo">Connection Info object that implements IDataConnectionInfo interface.</param>
        public MySqlConnectionInfo(IDataConnectionInfo connectionInfo)
            : base(connectionInfo)
        {
            DataContext = connectionInfo.DataContext ?? new MySqlContext();
            AcceptableVersion = new Version(5, 0, 0, 0);

            AuthenticationType = MySqlAuthenticationType.MySql;
        }

        public MySqlConnectionInfo()
        {
            DataContext = new MySqlContext();
            AcceptableVersion = new Version(5, 0, 0, 0);
        }

		public override DataContext CreateContext()
        {
            return new MySqlContext();
        }

        public override DbDataAdapter CreateDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        public override DbConnection CreateDbConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public override IMetadataScriptFactory CreateMetadataScriptFactory()
        {
            return new MySqlMetadataScriptFactory();
        }

        public override DbCommand CreateSelectAllCommand(UserTable userTable, DbConnection dbConnection, bool selectAll = true, int skip = 0, int limit = 0)
        {
            return new MySqlCommand(string.Format("SELECT * FROM {0}", userTable.Namespace), dbConnection as MySqlConnection);
        }

        /// <summary>
        /// Returns if this connection is valid to get metadata information from the
        /// source or target database and if it can run certain optimized query information
        /// against that database.
        /// </summary>
        /// <param name="dbConnection">DbConnection object.</param>
        /// <returns>
        /// True - version is valid
        /// False - version is not valid
        /// </returns>
        public override bool IsAcceptableVersion(DbConnection dbConnection)
        {
            if (dbConnection == null || dbConnection.State != System.Data.ConnectionState.Open)
                return false;

            if (dbConnection.ServerVersion == null)
                return false;

            var parts = dbConnection.ServerVersion.Split("-".ToCharArray(), int.MaxValue, StringSplitOptions.RemoveEmptyEntries);
            //Version = new Version(dbConnection.ServerVersion.Replace("-community-log",""));
            if (parts[0].Any(character => !char.IsNumber(character) && !char.IsPunctuation(character)))
                parts[0] = "0.0.0.0";
            Version = new Version(parts[0]);
            return Version >= AcceptableVersion;
        }

        /// <summary>
        /// Returns if the version passed in meets accaptable standards with
        /// this objects version.
        /// </summary>
        /// <param name="version">System.Version object to verify.</param>
        /// <returns>
        /// True - version is valid
        /// False - version is not valid
        /// </returns>
        public bool IsCompatibleVersion(Version version)
        {
            return version != null && Version != null && version >= AcceptableVersion;
        }

        public override void ThrowVersionException()
        {
            string message;

            if (Version == null)
            {
                message = string.Format(
                      "Version [0.0.0.0] is an invalid version of SqlServer. "
                    + "Connection attempt may have failed or may not have been attempted. "
                    + "Accaptable versions are greater than or equal to [{0}]."
                    , AcceptableVersion.ToString(4)
                );
            }
            else
            {
                message = string.Format(
                      "Version [{0}] is an invalid version of SqlServer for this application. "
                    + "Connection will not be attempted. "
                    + "Accaptable versions are greater than or equal to [{1}]."
                    , Version.ToString(4)
                    , AcceptableVersion.ToString(4)
                );
            }

            throw new Exception(message);
        }
    }
}
