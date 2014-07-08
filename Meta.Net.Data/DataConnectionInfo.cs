using System.Data.Common;
using System.Xml.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Objects;

namespace Meta.Net.Data
{
    /// <summary>
    /// Class for defining common database connection information properties. This object can
    /// be passed to implementation specific connection information classes such as
    /// SQLServerConnectionInfo, etc.
    /// </summary>
    public abstract class DataConnectionInfo: IDataConnectionInfo
    {
		#region Properties (7) 

        /// <summary>
        /// ConnectionString is generated from the class members.
        /// </summary>
        public virtual string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(UserId) && string.IsNullOrEmpty(Password))
                {
                    return string.Format(
                      "Data Source={0};Initial Catalog={1};Integrated Security=true;Persist Security Info=no;Encrypt=yes;TrustServerCertificate=yes"
                    , DataSource
                    , InitialCatalog
                  
                );
                }
                return string.Format(
                      "Data Source={0};Initial Catalog={1};User Id={2};Password={3};Persist Security Info=no;Encrypt=yes;TrustServerCertificate=yes"
                    , DataSource
                    , InitialCatalog
                    , UserId
                    , Password
                );
            }
        }

        private DataContext _dataContext;

        /// <summary>
        /// The type of Data Server if known:
        /// SqlServer, MySQL, DB2, Unknown
        /// </summary>
        [XmlIgnore]
        public DataContext DataContext
        {
            get
            {
                return _dataContext
                    ?? (_dataContext = CreateContext());
            }
            set { _dataContext = value; }
        }

        /// <summary>
        /// Data Source of the connection.
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// Initial Catalog of the connection.
        /// </summary>
        public string InitialCatalog { get; set; }

        /// <summary>
        /// Endpoint name, not used in ConnectionString property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Password of the connection.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User Id of the connection.
        /// </summary>
        public string UserId { get; set; }

		#endregion Properties 

        #region Constructors

        protected DataConnectionInfo()
        {
            
        }

        /// <summary>
        /// Constructor used when all members have been defined.
        /// </summary>
        /// <param name="name">Endpoint name, not used in ConnectionString property.</param>
        /// <param name="dataSource">Data Source of the connection.</param>
        /// <param name="initialCatalog">Initial Catalog of the connection.</param>
        /// <param name="userId">User Id of the connection.</param>
        /// <param name="password">Password of the connection.</param>
        /// <param name="dataContext">The data context.</param>
        protected DataConnectionInfo(string name, string dataSource, string initialCatalog, string userId, string password, DataContext dataContext)
        {
            Name = name;
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            UserId = userId;
            Password = password;
            DataContext = dataContext;
        }

        /// <summary>
        /// Constructor used when UserID, or Password are not defined.
        /// </summary>
        /// <param name="name">Endpoint name, not used in ConnectionString property.</param>
        /// <param name="dataSource">Data Source of the connection.</param>
        /// <param name="initialCatalog">Initial Catalog of the connection.</param>
        /// <param name="dataContext">The data context.</param>
        protected DataConnectionInfo(string name, string dataSource, string initialCatalog, DataContext dataContext)
        {
            Name = name;
            DataSource = dataSource;
            InitialCatalog = initialCatalog;
            UserId = "";
            Password = "";
            DataContext = dataContext;
        }

        /// <summary>
        /// Constructor that accepts any ConnectionInfo object that implements the
        /// IDataConnectionInfo interface and initializes its members to the
        /// IDatatConnectionInfo object member definitions.
        /// </summary>
        /// <param name="connectionInfo">Connection Info object that implements IDataConnectionInfo interface.</param>
        protected DataConnectionInfo(IDataConnectionInfo connectionInfo)
        {
            Name = connectionInfo.Name;
            UserId = connectionInfo.UserId;
            Password = connectionInfo.Password;
            DataSource = connectionInfo.DataSource;
            InitialCatalog = connectionInfo.InitialCatalog;
            DataContext = connectionInfo.DataContext;
        }

		#endregion Constructors 

        public abstract DataContext CreateContext();
        public abstract IDataMetadataScriptProvider CreateMetadataScriptProvider();
        public abstract DbConnection CreateDbConnection();
        public abstract DbDataAdapter CreateDataAdapter();
        public abstract bool IsAcceptableVersion(DbConnection dbConnection);
        public abstract void ThrowVersionException();
        public abstract DbCommand CreateSelectAllCommand(UserTable userTable, DbConnection connection, bool selectAll = true, int skip = 0, int limit = 0);
    }
}
