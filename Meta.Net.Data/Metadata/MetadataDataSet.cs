using System.Data;
using System.Data.Common;

namespace Meta.Net.Data.Metadata
{
    public class MetadataDataSet: DataSet
    {
		#region Properties (30) 

        public CatalogsDataTable Catalogs { get; set; }

        public CatalogsTableAdapter CatalogsAdapter { get; set; }

        public CheckConstraintsDataTable CheckConstraints { get; set; }

        public CheckConstraintsTableAdapter CheckConstraintsAdapter { get; set; }

        public ComputedColumnsDataTable ComputedColumns { get; set; }

        public ComputedColumnsTableAdapter ComputedColumnsAdapter { get; set; }

        public DefaultConstraintsDataTable DefaultConstraints { get; set; }

        public DefaultConstraintsTableAdapter DefaultConstraintsAdapter { get; set; }

        public ForeignKeyMapsDataTable ForeignKeyMaps { get; set; }

        public ForeignKeyMapsTableAdapter ForeignKeyMapsAdapter { get; set; }

        public ForeignKeysDataTable ForeignKeys { get; set; }

        public ForeignKeysTableAdapter ForeignKeysAdapter { get; set; }

        public IdentityColumnsDataTable IdentityColumns { get; set; }

        public IdentityColumnsTableAdapter IdentityColumnsAdapter { get; set; }

        public IndexesDataTable Indexes { get; set; }

        public IndexesTableAdapter IndexesAdapter { get; set; }

        public ModulesDataTable Modules { get; set; }

        public ModulesTableAdapter ModulesAdapter { get; set; }

        public PrimaryKeysDataTable PrimaryKeys { get; set; }

        public PrimaryKeysTableAdapter PrimaryKeysAdapter { get; set; }

        public SchemasDataTable Schemas { get; set; }

        public SchemasTableAdapter SchemasAdapter { get; set; }

        public UniqueConstraintsDataTable UniqueConstraints { get; set; }

        public UniqueConstraintsTableAdapter UniqueConstraintsAdapter { get; set; }

        public UserDefinedDataTypesDataTable UserDefinedDataTypes { get; set; }

        public UserDefinedDataTypesTableAdapter UserDefinedDataTypesAdapter { get; set; }

        public UserTableColumnsDataTable UserTableColumns { get; set; }

        public UserTableColumnsTableAdapter UserTableColumnsAdapter { get; set; }

        public UserTablesDataTable UserTables { get; set; }

        public UserTablesTableAdapter UserTablesAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionInfo">Needed to created server specific data adapters (SqlServer, MySql, etc...)</param>
        /// <param name="catalogs">The catalogs that</param>
        //public MetadataDataSet(DataConnectionInfo connectionInfo, HashSet<string> catalogs)
        public MetadataDataSet(DataConnectionInfo connectionInfo, string catalogName)
        {
            DataSetName = "Metadata";
            Prefix = "";
            Namespace = "http://tempuri.org/Metadata";
            
            Catalogs = new CatalogsDataTable();
            CheckConstraints = new CheckConstraintsDataTable();
            ComputedColumns = new ComputedColumnsDataTable();
            DefaultConstraints = new DefaultConstraintsDataTable();
            ForeignKeyMaps = new ForeignKeyMapsDataTable();
            ForeignKeys = new ForeignKeysDataTable();
            IdentityColumns = new IdentityColumnsDataTable();
            Indexes = new IndexesDataTable();
            Modules = new ModulesDataTable();
            PrimaryKeys = new PrimaryKeysDataTable();
            Schemas = new SchemasDataTable();
            UniqueConstraints = new UniqueConstraintsDataTable();
            UserDefinedDataTypes = new UserDefinedDataTypesDataTable();
            UserTableColumns = new UserTableColumnsDataTable();
            UserTables = new UserTablesDataTable();

            base.Tables.Add(Catalogs);
            base.Tables.Add(CheckConstraints);
            base.Tables.Add(ComputedColumns);
            base.Tables.Add(DefaultConstraints);
            base.Tables.Add(ForeignKeyMaps);
            base.Tables.Add(ForeignKeys);
            base.Tables.Add(IdentityColumns);
            base.Tables.Add(Indexes);
            base.Tables.Add(Modules);
            base.Tables.Add(PrimaryKeys);
            base.Tables.Add(Schemas);
            base.Tables.Add(UniqueConstraints);
            base.Tables.Add(UserDefinedDataTypes);
            base.Tables.Add(UserTableColumns);
            base.Tables.Add(UserTables);

            base.EnforceConstraints = false;

            ChangeCatalog(connectionInfo, catalogName);


            //CatalogsAdapter = new CatalogsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider());
            //CheckConstraintsAdapter = new CheckConstraintsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //ComputedColumnsAdapter = new ComputedColumnsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //DefaultConstraintsAdapter = new DefaultConstraintsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //ForeignKeyMapsAdapter = new ForeignKeyMapsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //ForeignKeysAdapter = new ForeignKeysTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //IdentityColumnsAdapter = new IdentityColumnsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //IndexesAdapter = new IndexesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //ModulesAdapter = new ModulesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //PrimaryKeysAdapter = new PrimaryKeysTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //SchemasAdapter = new SchemasTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //UniqueConstraintsAdapter = new UniqueConstraintsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //UserDefinedDataTypesAdapter = new UserDefinedDataTypesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //UserTableColumnsAdapter = new UserTableColumnsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            //UserTablesAdapter = new UserTablesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (5) 

        //public void ChangeCatalog(DataConnectionInfo connectionInfo, DataProperties dataProperties)
        public void ChangeCatalog(DataConnectionInfo connectionInfo, string catalogName)
        {
            CatalogsAdapter = new CatalogsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider());
            CheckConstraintsAdapter = new CheckConstraintsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            ComputedColumnsAdapter = new ComputedColumnsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            DefaultConstraintsAdapter = new DefaultConstraintsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            ForeignKeyMapsAdapter = new ForeignKeyMapsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            ForeignKeysAdapter = new ForeignKeysTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            IdentityColumnsAdapter = new IdentityColumnsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            IndexesAdapter = new IndexesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            ModulesAdapter = new ModulesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            PrimaryKeysAdapter = new PrimaryKeysTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            SchemasAdapter = new SchemasTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            UniqueConstraintsAdapter = new UniqueConstraintsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            UserDefinedDataTypesAdapter = new UserDefinedDataTypesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            UserTableColumnsAdapter = new UserTableColumnsTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
            UserTablesAdapter = new UserTablesTableAdapter(connectionInfo.CreateDataAdapter(), connectionInfo.CreateMetadataScriptProvider(), catalogName);
        }

        public void Fill(DbConnection dbConnection)
        {
            CheckConstraintsAdapter.Fill(dbConnection, CheckConstraints);
            ComputedColumnsAdapter.Fill(dbConnection, ComputedColumns);
            DefaultConstraintsAdapter.Fill(dbConnection, DefaultConstraints);
            ForeignKeyMapsAdapter.Fill(dbConnection, ForeignKeyMaps);
            ForeignKeysAdapter.Fill(dbConnection, ForeignKeys);
            IdentityColumnsAdapter.Fill(dbConnection, IdentityColumns);
            IndexesAdapter.Fill(dbConnection, Indexes);
            ModulesAdapter.Fill(dbConnection, Modules);
            PrimaryKeysAdapter.Fill(dbConnection, PrimaryKeys);
            SchemasAdapter.Fill(dbConnection, Schemas);
            UniqueConstraintsAdapter.Fill(dbConnection, UniqueConstraints);
            UserDefinedDataTypesAdapter.Fill(dbConnection, UserDefinedDataTypes);
            UserTableColumnsAdapter.Fill(dbConnection, UserTableColumns);
            UserTablesAdapter.Fill(dbConnection, UserTables);   
        }

        public void Fill(DataConnectionManager dataConnectionManager)
        {
            CheckConstraintsAdapter.Fill(dataConnectionManager, CheckConstraints);
            ComputedColumnsAdapter.Fill(dataConnectionManager, ComputedColumns);
            DefaultConstraintsAdapter.Fill(dataConnectionManager, DefaultConstraints);
            ForeignKeyMapsAdapter.Fill(dataConnectionManager, ForeignKeyMaps);
            ForeignKeysAdapter.Fill(dataConnectionManager, ForeignKeys);
            IdentityColumnsAdapter.Fill(dataConnectionManager, IdentityColumns);
            IndexesAdapter.Fill(dataConnectionManager, Indexes);
            ModulesAdapter.Fill(dataConnectionManager, Modules);
            PrimaryKeysAdapter.Fill(dataConnectionManager, PrimaryKeys);
            SchemasAdapter.Fill(dataConnectionManager, Schemas);
            UniqueConstraintsAdapter.Fill(dataConnectionManager, UniqueConstraints);
            UserDefinedDataTypesAdapter.Fill(dataConnectionManager, UserDefinedDataTypes);
            UserTableColumnsAdapter.Fill(dataConnectionManager, UserTableColumns);
            UserTablesAdapter.Fill(dataConnectionManager, UserTables);  
        }

        public void FillCatalogs(DbConnection dbConnection)
        {
            CatalogsAdapter.Fill(dbConnection, Catalogs);
        }

        public void FillCatalogs(DataConnectionManager dataConnectionManager)
        {
            CatalogsAdapter.Fill(dataConnectionManager, Catalogs);
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
