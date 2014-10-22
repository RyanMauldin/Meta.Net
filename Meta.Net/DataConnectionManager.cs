using System;
using System.Data;
using System.Data.Common;

namespace Meta.Net
{
    public class DataConnectionManager
    {
        #region Properties (5)

        public string CatalogName
        {
            get
            {

                if (DataConnection == null)
                    return DataConnectionInfo.InitialCatalog;

                switch (DataConnection.State)
                {
                    case ConnectionState.Broken:
                    case ConnectionState.Closed:
                    case ConnectionState.Connecting:
                        return DataConnectionInfo.InitialCatalog;
                    case ConnectionState.Executing:
                    case ConnectionState.Fetching:
                    case ConnectionState.Open:
                        return DataConnection.Database;
                }

                return DataConnectionInfo.InitialCatalog;
            }
        }

        public DataConnectionInfo DataConnectionInfo { get; private set; }

        public DbConnection DataConnection { get; set; }

        //public SqlServerConnectionInfo SqlServerConnectionInfo { get; private set; }

        public ConnectionState State
        {
            get
            {
                return DataConnection == null
                    ? ConnectionState.Closed
                    : DataConnection.State;
            }
        }

        #endregion Properties

        #region Constructors (1)

        /// <summary>
        /// Initializes the connection with our custom connectionInfo for whatever
        /// type of database we want to connect to. For now it is SqlServer only.
        /// </summary>
        /// <param name="dataConnectionInfo">IDataConnectionInfo compatible object</param>
        public DataConnectionManager(DataConnectionInfo dataConnectionInfo)
        {
            DataConnectionInfo = dataConnectionInfo;
            InitializeDataConnection();
        }

        #endregion Constructors

        #region Methods (10)

        #region Public Methods (6)

        public void ChangeCatalog(string catalogName)
        {

            ChangeSqlServerCatalog(catalogName);

        }

        public void ChangeDataConnectionInfo(DataConnectionInfo dataConnectionInfo)
        {
            Close();
            DataConnectionInfo = dataConnectionInfo;
            InitializeDataConnection();

        }

        public void Close()
        {

            CloseSqlServerConnection();

        }

        public bool IsBusy()
        {

            if (DataConnection == null)
                return false;
            switch (DataConnection.State)
            {
                case ConnectionState.Connecting:
                case ConnectionState.Executing:
                case ConnectionState.Fetching:
                    return true;
            }


            return false;
        }

        public bool IsOpen()
        {

            if (DataConnection == null)
                return false;
            if (DataConnection.State == ConnectionState.Open)
                return true;


            return false;
        }

        public void Open()
        {
            OpenSqlServerConnection();
        }

        #endregion Public Methods
        #region Private Methods (4)

        private void ChangeSqlServerCatalog(string catalogName)
        {
            OpenSqlServerConnection();

            switch (DataConnection.State)
            {
                case ConnectionState.Open:
                    if (!DataConnection.Database.Equals(catalogName, StringComparison.OrdinalIgnoreCase))
                        DataConnection.ChangeDatabase(catalogName);
                    break;
                case ConnectionState.Connecting:
                    throw new Exception(@"Cannot change the catalog while connecting to a datasource.");
                case ConnectionState.Executing:
                    throw new Exception(@"Cannot change the catalog while connection is executing commands.");
                case ConnectionState.Fetching:
                    throw new Exception(@"Cannot change the catalog while connection is fetching data.");
                //default:
                    // Exception thrown in OpenSqlServerConnection
                    //break;
            }
        }

        private void CloseSqlServerConnection()
        {
            if (DataConnection == null)
                return;

            switch (DataConnection.State)
            {
                case ConnectionState.Closed:
                    break;
                default:
                    DataConnection.Close();
                    break;
            }
        }

        private void InitializeDataConnection()
        {
            if (DataConnection != null)
                return;

            DataConnection = DataConnectionInfo.CreateDbConnection();
        }

        private void OpenSqlServerConnection()
        {
            InitializeDataConnection();

            switch (DataConnection.State)
            {
                case ConnectionState.Broken:
                    DataConnection.Close();
                    DataConnection.Open();
                    if (!DataConnectionInfo.IsAcceptableVersion(DataConnection))
                    {
                        DataConnection.Close();
                        DataConnectionInfo.ThrowVersionException();
                    }
                    break;
                case ConnectionState.Closed:
                    DataConnection.Open();
                    if (!DataConnectionInfo.IsAcceptableVersion(DataConnection))
                    {
                        DataConnection.Close();
                        DataConnectionInfo.ThrowVersionException();
                    }
                    break;
                //default:
                //    break;
            }
        }

        #endregion Private Methods

        #endregion Methods
    }
}
