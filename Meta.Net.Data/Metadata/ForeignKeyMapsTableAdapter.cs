using System.Data.Common;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Metadata
{
    public class ForeignKeyMapsTableAdapter
    {
		#region Properties (2) 

        public string CommandText { get; set; }

        public DbDataAdapter DataAdapter { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public ForeignKeyMapsTableAdapter(DbDataAdapter dataAdapter, IDataMetadataScriptProvider dataMetadataScriptProvider, string catalogName)
        {
            CommandText = dataMetadataScriptProvider.ForeignKeyMapsSql2(catalogName);
            var dataTableMapping = new DataTableMapping { SourceTable = "Table", DataSetTable = "ForeignKeyMaps" };
            dataTableMapping.ColumnMappings.Add("CatalogName", "CatalogName");
            dataTableMapping.ColumnMappings.Add("ColumnName", "ColumnName");
            dataTableMapping.ColumnMappings.Add("Description", "Description");
            dataTableMapping.ColumnMappings.Add("KeyOrdinal", "KeyOrdinal");
            dataTableMapping.ColumnMappings.Add("Namespace", "Namespace");
            dataTableMapping.ColumnMappings.Add("NamespaceGroup", "NamespaceGroup");
            dataTableMapping.ColumnMappings.Add("NamespaceInverse", "NamespaceInverse");
            dataTableMapping.ColumnMappings.Add("NamespaceInverseGroup", "NamespaceInverseGroup");
            dataTableMapping.ColumnMappings.Add("ObjectName", "ObjectName");
            dataTableMapping.ColumnMappings.Add("ReferencedCatalogName", "ReferencedCatalogName");
            dataTableMapping.ColumnMappings.Add("ReferencedColumnName", "ReferencedColumnName");
            dataTableMapping.ColumnMappings.Add("ReferencedObjectName", "ReferencedObjectName");
            dataTableMapping.ColumnMappings.Add("ReferencedSchemaName", "ReferencedSchemaName");
            dataTableMapping.ColumnMappings.Add("ReferencedTableName", "ReferencedTableName");
            dataTableMapping.ColumnMappings.Add("SchemaName", "SchemaName");
            dataTableMapping.ColumnMappings.Add("TableName", "TableName");

            DataAdapter = dataAdapter;
            DataAdapter.TableMappings.Add(dataTableMapping);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

        public int Fill(DataConnectionManager dataConnectionManager, ForeignKeyMapsDataTable foreignKeyMapsDataTable)
        {
            return Fill(dataConnectionManager.DataConnection, foreignKeyMapsDataTable);      
        }

        public int Fill(DbConnection dbConnection, ForeignKeyMapsDataTable foreignKeyMapsDataTable)
        {
            foreignKeyMapsDataTable.Clear();
            using (var dbCommmand = dbConnection.CreateCommand())
            {
                dbCommmand.CommandText = CommandText;
                DataAdapter.SelectCommand = dbCommmand;
                foreignKeyMapsDataTable.BeginLoadData();

                var result = DataAdapter.Fill(foreignKeyMapsDataTable);
                foreignKeyMapsDataTable.EndLoadData();
                DataAdapter.SelectCommand = null;
                return result;
            }
        }

        public ForeignKeyMapsDataTable GetData(DbConnection dbConnection)
        {
            var foreignKeyMapsDataTable = new ForeignKeyMapsDataTable();
            Fill(dbConnection, foreignKeyMapsDataTable);
            return foreignKeyMapsDataTable;
        }

		#endregion Public Methods 

		#endregion Methods 
    }
}
