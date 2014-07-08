using System;
using Meta.Net.Data.Metadata;
using Meta.Net.Generics;
using System.Collections.Generic;

namespace Meta.Net.Data
{
    /// <summary>
    /// Class of generic lists and dictionaries used to store all metadata for a 
    /// single catalog. All lists and dictionaries use a namespace format for
    /// unique entries. For example, the TableColumns List is a List of strings
    /// with a namespace format as follows:
    /// [SchemaName].[TableName].[ColumnName]... [dbo].[Customers].[FirstName]
    /// </summary>
    public class DataGenerics
    {
		#region Properties (38) 

        /// <summary>
        /// Dictionary containing key: "[CatalogName]"
        /// ... mapped as: Metadata.DatabasesRow.Namespace
        /// ... mapped to: Metadata.DatabasesRow
        /// </summary>
        public Dictionary<string, CatalogsRow> CatalogRows { get; private set; }

        /// <summary>
        /// List containing string: "[CatalogName]"
        /// ... mapped as: Metadata.DatabasesRow.Namespace
        /// </summary>
        public HistoryList<string> Catalogs { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.CheckConstraintsRow.Namespace
        /// ... mapped to: Metadata.CheckConstraintsRow
        /// </summary>
        public Dictionary<string, CheckConstraintsRow> CheckConstraintRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.CheckConstraints.Namespace
        /// </summary>
        public HistoryList<string> CheckConstraints { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ColumnName]"
        /// ... mapped as: Metadata.ComputedColumns.Namespace
        /// ... mapped to: Metadata.ComputedColumns
        /// </summary>
        public Dictionary<string, ComputedColumnsRow> ComputedColumnRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ColumnName]"
        /// ... mapped as: Metadata.ComputedColumns.Namespace
        /// </summary>
        public HistoryList<string> ComputedColumns { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.DefaultConstraintsRow.Namespace
        /// ... mapped to: Metadata.DefaultConstraintsRow
        /// </summary>
        public Dictionary<string, DefaultConstraintsRow> DefaultConstraintRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.DefaultConstraintsRow.Namespace
        /// </summary>
        public HistoryList<string> DefaultConstraints { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.ForeignKeysRow.NamespaceGroup
        /// </summary>
        public HistoryList<string> ForeignKeyGroups { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.ForeignKeyMapsRow.NamespaceGroup
        /// </summary>
        public HistoryList<string> ForeignKeyMapGroups { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.ForeignKeyMapsRow.Namespace
        /// ... mapped to: Metadata.ForeignKeyMaps
        /// </summary>
        public Dictionary<string, ForeignKeyMapsRow> ForeignKeyMapRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.ForeignKeyMapsRow.Namespace
        /// </summary>
        public HistoryList<string> ForeignKeyMaps { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.ForeignKeysRow.Namespace
        /// ... mapped to: Metadata.ForeignKeysRow
        /// </summary>
        public Dictionary<string, ForeignKeysRow> ForeignKeyRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.ForeignKeysRow.Namespace
        /// </summary>
        public HistoryList<string> ForeignKeys { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ColumnName]"
        /// ... mapped as: Metadata.IdentityColumnsRow.Namespace
        /// ... mapped to: Metadata.IdentityColumnsRow
        /// </summary>
        public Dictionary<string, IdentityColumnsRow> IdentityColumnRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ColumnName]"
        /// ... mapped as: Metadata.IdentityColumnsRow.Namespace
        /// </summary>
        public HistoryList<string> IdentityColumns { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.IndexesRow.Namespace
        /// </summary>
        public HistoryList<string> Indexes { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.IndexesRow.NamespaceGroup
        /// </summary>
        public HistoryList<string> IndexGroups { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.IndexesRow.Namespace
        /// ... mapped to: Metadata.IndexesRow
        /// </summary>
        public Dictionary<string, IndexesRow> IndexRows { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[ObjectName]"
        /// ... mapped as: Metadata.ModulesRow.Namespace
        /// ... mapped to: Metadata.ModulesRow
        /// </summary>
        public Dictionary<string, ModulesRow> ModuleRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[ObjectName]"
        /// ... mapped as: Metadata.ModulesRow.Namespace
        /// </summary>
        public HistoryList<string> Modules { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.PrimaryKeysRow.NamespaceGroup
        /// </summary>
        public HistoryList<string> PrimaryKeyGroups { get; private set; }

        /// <summary>
        /// List containing string: "[ReferencedSchemaName].[ReferencedTableName].[ReferencedObjectName]"
        /// ... mapped as: Metadata.ForeignKeyMapsRow.NamespaceInverseGroup
        /// </summary>
        public HistoryList<string> PrimaryKeyMapGroups { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[ReferencedSchemaName].[ReferencedTableName].[ReferencedObjectName].[ReferencedColumnName]"
        /// ... mapped as: Metadata.ForeignKeyMapsRow.NamespaceInverse
        /// ... mapped to: Metadata.ForeignKeyMapsRow
        /// </summary>
        public Dictionary<string, ForeignKeyMapsRow> PrimaryKeyMapRows { get; private set; }

        /// <summary>
        /// List containing string: "[ReferencedSchemaName].[ReferencedTableName].[ReferencedObjectName].[ReferencedColumnName]"
        /// ... mapped as: Metadata.ForeignKeyMapsRow.NamespaceInverse
        /// </summary>
        public HistoryList<string> PrimaryKeyMaps { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.PrimaryKeysRow.Namespace
        /// ... mapped to: Metadata.PrimaryKeysRow
        /// </summary>
        public Dictionary<string, PrimaryKeysRow> PrimaryKeyRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.PrimaryKeysRow.Namespace
        /// </summary>
        public HistoryList<string> PrimaryKeys { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName]"
        /// ... mapped as: Metadata.SchemasRow.Namespace
        /// ... mapped to: Metadata.SchemasRow
        /// </summary>
        public Dictionary<string, SchemasRow> SchemaRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName]"
        /// ... mapped as: Metadata.SchemasRow.Namespace
        /// </summary>
        public HistoryList<string> Schemas { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName]"
        /// ... mapped as: Metadata.UniqueConstraintsRow.NamespaceGroup
        /// </summary>
        public HistoryList<string> UniqueConstraintGroups { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.UniqueConstraintsRow.Namespace
        /// ... mapped to: Metadata.UniqueConstraintsRow
        /// </summary>
        public Dictionary<string, UniqueConstraintsRow> UniqueConstraintRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ObjectName].[ColumnName]"
        /// ... mapped as: Metadata.UniqueConstraintsRow.Namespace
        /// </summary>
        public HistoryList<string> UniqueConstraints { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[ObjectName]"
        /// ... mapped as: Metadata.UserDefinedDataTypesRow.Namespace
        /// ... mapped to: Metadata.UserDefinedDataTypesRow
        /// </summary>
        public Dictionary<string, UserDefinedDataTypesRow> UserDefinedDataTypeRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[ObjectName]"
        /// ... mapped as: Metadata.UserDefinedDataTypesRow.Namespace
        /// </summary>
        public HistoryList<string> UserDefinedDataTypes { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName].[ColumnName]"
        /// ... mapped as: Metadata.UserTableColumnsRow.Namespace
        /// ... mapped to: Metadata.UserTableColumnsRow
        /// </summary>
        public Dictionary<string, UserTableColumnsRow> UserTableColumnRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName].[ColumnName]"
        /// ... mapped as: Metadata.UserTableColumnsRow.Namespace
        /// </summary>
        public HistoryList<string> UserTableColumns { get; private set; }

        /// <summary>
        /// Dictionary containing key: "[SchemaName].[TableName]"
        /// ... mapped as: Metadata.UserTablesRow.Namespace
        /// ... mapped to: Metadata.UserTablesRow
        /// </summary>
        public Dictionary<string, UserTablesRow> UserTableRows { get; private set; }

        /// <summary>
        /// List containing string: "[SchemaName].[TableName]"
        /// ... mapped as: Metadata.UserTablesRow.Namespace
        /// </summary>
        public HistoryList<string> UserTables { get; private set; }

		#endregion Properties 

		#region Constructors (1) 

        /// <summary>
        /// Instantiates all generic collections.
        /// </summary>
        public DataGenerics()
        {
            Catalogs = new HistoryList<string>();
            CatalogRows = new Dictionary<string, CatalogsRow>(StringComparer.OrdinalIgnoreCase);
            Schemas = new HistoryList<string>();
            SchemaRows = new Dictionary<string, SchemasRow>(StringComparer.OrdinalIgnoreCase);
            UserTables = new HistoryList<string>();
            UserTableRows = new Dictionary<string, UserTablesRow>(StringComparer.OrdinalIgnoreCase);
            UserTableColumns = new HistoryList<string>();
            UserTableColumnRows = new Dictionary<string, UserTableColumnsRow>(StringComparer.OrdinalIgnoreCase);
            IdentityColumns = new HistoryList<string>();
            IdentityColumnRows = new Dictionary<string, IdentityColumnsRow>(StringComparer.OrdinalIgnoreCase);
            DefaultConstraints = new HistoryList<string>();
            DefaultConstraintRows = new Dictionary<string, DefaultConstraintsRow>(StringComparer.OrdinalIgnoreCase);
            CheckConstraints = new HistoryList<string>();
            CheckConstraintRows = new Dictionary<string, CheckConstraintsRow>(StringComparer.OrdinalIgnoreCase);
            ComputedColumns = new HistoryList<string>();
            ComputedColumnRows = new Dictionary<string, ComputedColumnsRow>(StringComparer.OrdinalIgnoreCase);
            PrimaryKeys = new HistoryList<string>();
            PrimaryKeyGroups = new HistoryList<string>();
            PrimaryKeyRows = new Dictionary<string, PrimaryKeysRow>(StringComparer.OrdinalIgnoreCase);
            UniqueConstraints = new HistoryList<string>();
            UniqueConstraintGroups = new HistoryList<string>();
            UniqueConstraintRows = new Dictionary<string, UniqueConstraintsRow>(StringComparer.OrdinalIgnoreCase);
            Indexes = new HistoryList<string>();
            IndexGroups = new HistoryList<string>();
            IndexRows = new Dictionary<string, IndexesRow>(StringComparer.OrdinalIgnoreCase);
            ForeignKeys = new HistoryList<string>();
            ForeignKeyGroups = new HistoryList<string>();
            ForeignKeyRows = new Dictionary<string, ForeignKeysRow>(StringComparer.OrdinalIgnoreCase);
            ForeignKeyMaps = new HistoryList<string>();
            ForeignKeyMapGroups = new HistoryList<string>();
            ForeignKeyMapRows = new Dictionary<string, ForeignKeyMapsRow>(StringComparer.OrdinalIgnoreCase);
            PrimaryKeyMaps = new HistoryList<string>();
            PrimaryKeyMapGroups = new HistoryList<string>();
            PrimaryKeyMapRows = new Dictionary<string, ForeignKeyMapsRow>(StringComparer.OrdinalIgnoreCase);
            Modules = new HistoryList<string>();
            ModuleRows = new Dictionary<string, ModulesRow>(StringComparer.OrdinalIgnoreCase);
            UserDefinedDataTypes = new HistoryList<string>();
            UserDefinedDataTypeRows = new Dictionary<string, UserDefinedDataTypesRow>(StringComparer.OrdinalIgnoreCase);
        }

		#endregion Constructors 

		#region Methods (3) 

		#region Public Methods (3) 

		/// <summary>
		/// Calls the Clear() method on all generic collections.
		/// </summary>
		public void Clear()
		{
            Catalogs.Clear();
            CatalogRows.Clear();
            Schemas.Clear();
            SchemaRows.Clear();
            UserTables.Clear();
            UserTableRows.Clear();
            UserTableColumns.Clear();
            UserTableColumnRows.Clear();
            IdentityColumns.Clear();
            IdentityColumnRows.Clear();
            DefaultConstraints.Clear();
            DefaultConstraintRows.Clear();
            CheckConstraints.Clear();
            CheckConstraintRows.Clear();
            ComputedColumns.Clear();
            ComputedColumnRows.Clear();
            PrimaryKeys.Clear();
            PrimaryKeyMapGroups.Clear();
            PrimaryKeyRows.Clear();
            UniqueConstraints.Clear();
            UniqueConstraintGroups.Clear();
            UniqueConstraintRows.Clear();
            Indexes.Clear();
            IndexGroups.Clear();
            IndexRows.Clear();
            ForeignKeys.Clear();
            ForeignKeyGroups.Clear();
            ForeignKeyRows.Clear();
            ForeignKeyMaps.Clear();
            ForeignKeyMapGroups.Clear();
            ForeignKeyMapRows.Clear();
            PrimaryKeyMaps.Clear();
            PrimaryKeyMapGroups.Clear();
            PrimaryKeyMapRows.Clear();
            Modules.Clear();
            ModuleRows.Clear();
            UserDefinedDataTypes.Clear();
            UserDefinedDataTypeRows.Clear();
		}

        /// <summary>
        /// Clears all generic collections and then fills the collections
        /// with namespaces as keys. The dictionary entries contain strongly
        /// typed MetadataDataSet rows mapped by the namespace keys. AdapterManager
        /// contains only informational properties needed to fill the generic collections
        /// properly and should have its connection closed. The MetadataDataSet should already
        /// be filled before passing it into this method.
        /// </summary>
        public void Fill(MetadataDataSet metadataDataSet)
        {
            Schemas.Clear();
            SchemaRows.Clear();
            UserTables.Clear();
            UserTableRows.Clear();
            UserTableColumns.Clear();
            UserTableColumnRows.Clear();
            IdentityColumns.Clear();
            IdentityColumnRows.Clear();
            DefaultConstraints.Clear();
            DefaultConstraintRows.Clear();
            CheckConstraints.Clear();
            CheckConstraintRows.Clear();
            ComputedColumns.Clear();
            ComputedColumnRows.Clear();
            PrimaryKeys.Clear();
            PrimaryKeyMapGroups.Clear();
            PrimaryKeyRows.Clear();
            UniqueConstraints.Clear();
            UniqueConstraintGroups.Clear();
            UniqueConstraintRows.Clear();
            Indexes.Clear();
            IndexGroups.Clear();
            IndexRows.Clear();
            ForeignKeys.Clear();
            ForeignKeyGroups.Clear();
            ForeignKeyRows.Clear();
            ForeignKeyMaps.Clear();
            ForeignKeyMapGroups.Clear();
            ForeignKeyMapRows.Clear();
            PrimaryKeyMaps.Clear();
            PrimaryKeyMapGroups.Clear();
            PrimaryKeyMapRows.Clear();
            Modules.Clear();
            ModuleRows.Clear();
            UserDefinedDataTypes.Clear();
            UserDefinedDataTypeRows.Clear();

            for (var i = 0; i < metadataDataSet.Schemas.Count; i++)
            {
                Schemas.Add(metadataDataSet.Schemas[i].Namespace);
                SchemaRows.Add(metadataDataSet.Schemas[i].Namespace + ".", metadataDataSet.Schemas[i]);
            }

            for (var i = 0; i < metadataDataSet.UserTables.Count; i++)
            {
                if (DataProperties.IgnoredUserTables.Contains(metadataDataSet.UserTables[i].Namespace))
                    continue;

                UserTables.Add(metadataDataSet.UserTables[i].Namespace);
                UserTableRows.Add(metadataDataSet.UserTables[i].Namespace + ".", metadataDataSet.UserTables[i]);
            }


            for (var i = 0; i < metadataDataSet.UserTableColumns.Count; i++)
            {
                var tableNamespace = metadataDataSet.UserTableColumns[i].SchemaName + "." + metadataDataSet.UserTableColumns[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                UserTableColumns.Add(metadataDataSet.UserTableColumns[i].Namespace);
                UserTableColumnRows.Add(metadataDataSet.UserTableColumns[i].Namespace + ".", metadataDataSet.UserTableColumns[i]);
            }

            for (var i = 0; i < metadataDataSet.IdentityColumns.Count; i++)
            {
                var tableNamespace = metadataDataSet.IdentityColumns[i].SchemaName + "." + metadataDataSet.IdentityColumns[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                IdentityColumns.Add(metadataDataSet.IdentityColumns[i].Namespace);
                IdentityColumnRows.Add(metadataDataSet.IdentityColumns[i].Namespace + ".", metadataDataSet.IdentityColumns[i]);
            }

            for (var i = 0; i < metadataDataSet.DefaultConstraints.Count; i++)
            {
                var tableNamespace = metadataDataSet.DefaultConstraints[i].SchemaName + "." + metadataDataSet.DefaultConstraints[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                DefaultConstraints.Add(metadataDataSet.DefaultConstraints[i].Namespace);
                DefaultConstraintRows.Add(metadataDataSet.DefaultConstraints[i].Namespace + ".", metadataDataSet.DefaultConstraints[i]);
            }

            for (var i = 0; i < metadataDataSet.CheckConstraints.Count; i++)
            {
                var tableNamespace = metadataDataSet.CheckConstraints[i].SchemaName + "." + metadataDataSet.CheckConstraints[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                CheckConstraints.Add(metadataDataSet.CheckConstraints[i].Namespace);
                CheckConstraintRows.Add(metadataDataSet.CheckConstraints[i].Namespace + ".", metadataDataSet.CheckConstraints[i]);
            }

            for (var i = 0; i < metadataDataSet.ComputedColumns.Count; i++)
            {
                var tableNamespace = metadataDataSet.ComputedColumns[i].SchemaName + "." + metadataDataSet.ComputedColumns[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                ComputedColumns.Add(metadataDataSet.ComputedColumns[i].Namespace);
                ComputedColumnRows.Add(metadataDataSet.ComputedColumns[i].Namespace + ".", metadataDataSet.ComputedColumns[i]);
            }

            var currentGroup = "";
            for (var i = 0; i < metadataDataSet.PrimaryKeys.Count; i++)
            {
                var tableNamespace = metadataDataSet.PrimaryKeys[i].SchemaName + "." + metadataDataSet.PrimaryKeys[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                PrimaryKeys.Add(metadataDataSet.PrimaryKeys[i].Namespace);
                PrimaryKeyRows.Add(metadataDataSet.PrimaryKeys[i].Namespace + ".", metadataDataSet.PrimaryKeys[i]);
                if (metadataDataSet.PrimaryKeys[i].NamespaceGroup == currentGroup)
                    continue;

                PrimaryKeyGroups.Add(metadataDataSet.PrimaryKeys[i].NamespaceGroup);
                currentGroup = metadataDataSet.PrimaryKeys[i].NamespaceGroup;
            }

            currentGroup = "";
            for (var i = 0; i < metadataDataSet.UniqueConstraints.Count; i++)
            {
                var tableNamespace = metadataDataSet.UniqueConstraints[i].SchemaName + "." + metadataDataSet.UniqueConstraints[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                UniqueConstraints.Add(metadataDataSet.UniqueConstraints[i].Namespace);
                UniqueConstraintRows.Add(metadataDataSet.UniqueConstraints[i].Namespace + ".", metadataDataSet.UniqueConstraints[i]);
                if (metadataDataSet.UniqueConstraints[i].NamespaceGroup == currentGroup)
                    continue;

                UniqueConstraintGroups.Add(metadataDataSet.UniqueConstraints[i].NamespaceGroup);
                currentGroup = metadataDataSet.UniqueConstraints[i].NamespaceGroup;
            }

            currentGroup = "";
            for (var i = 0; i < metadataDataSet.Indexes.Count; i++)
            {
                var tableNamespace = metadataDataSet.Indexes[i].SchemaName + "." + metadataDataSet.Indexes[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                Indexes.Add(metadataDataSet.Indexes[i].Namespace);
                IndexRows.Add(metadataDataSet.Indexes[i].Namespace + ".", metadataDataSet.Indexes[i]);
                if (metadataDataSet.Indexes[i].NamespaceGroup == currentGroup)
                    continue;

                IndexGroups.Add(metadataDataSet.Indexes[i].NamespaceGroup);
                currentGroup = metadataDataSet.Indexes[i].NamespaceGroup;
            }

            currentGroup = "";
            for (var i = 0; i < metadataDataSet.ForeignKeys.Count; i++)
            {
                var tableNamespace = metadataDataSet.ForeignKeys[i].SchemaName + "." + metadataDataSet.ForeignKeys[i].TableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace))
                    continue;

                ForeignKeys.Add(metadataDataSet.ForeignKeys[i].Namespace);
                ForeignKeyRows.Add(metadataDataSet.ForeignKeys[i].Namespace + ".", metadataDataSet.ForeignKeys[i]);
                if (metadataDataSet.ForeignKeys[i].NamespaceGroup == currentGroup)
                    continue;

                ForeignKeyGroups.Add(metadataDataSet.ForeignKeys[i].NamespaceGroup);
                currentGroup = metadataDataSet.ForeignKeys[i].NamespaceGroup;
            }

            currentGroup = "";
            var currentInverseGroup = "";
            for (var i = 0; i < metadataDataSet.ForeignKeyMaps.Count; i++)
            {
                var tableNamespace = metadataDataSet.ForeignKeyMaps[i].SchemaName + "." + metadataDataSet.ForeignKeyMaps[i].TableName;
                var tableNamespaceInverse = metadataDataSet.ForeignKeyMaps[i].ReferencedSchemaName + "." + metadataDataSet.ForeignKeyMaps[i].ReferencedTableName;
                if (DataProperties.IgnoredUserTables.Contains(tableNamespace) && DataProperties.IgnoredUserTables.Contains(tableNamespaceInverse))
                    continue;

                ForeignKeyMaps.Add(metadataDataSet.ForeignKeyMaps[i].Namespace);
                ForeignKeyMapRows.Add(metadataDataSet.ForeignKeyMaps[i].Namespace + ".", metadataDataSet.ForeignKeyMaps[i]);
                PrimaryKeyMaps.Add(metadataDataSet.ForeignKeyMaps[i].NamespaceInverse);
                PrimaryKeyMapRows.Add(metadataDataSet.ForeignKeyMaps[i].NamespaceInverse + ".", metadataDataSet.ForeignKeyMaps[i]);
                if (metadataDataSet.ForeignKeyMaps[i].NamespaceGroup != currentGroup)
                {
                    ForeignKeyMapGroups.Add(metadataDataSet.ForeignKeyMaps[i].NamespaceGroup);
                    currentGroup = metadataDataSet.ForeignKeyMaps[i].NamespaceGroup;
                }

                if (metadataDataSet.ForeignKeyMaps[i].NamespaceInverseGroup == currentInverseGroup)
                    continue;

                PrimaryKeyMapGroups.Add(metadataDataSet.ForeignKeyMaps[i].NamespaceInverseGroup);
                currentInverseGroup = metadataDataSet.ForeignKeyMaps[i].NamespaceInverseGroup;
            }

            for (var i = 0; i < metadataDataSet.Modules.Count; i++)
            {
                if (DataProperties.IgnoredModules.Contains(metadataDataSet.Modules[i].Namespace))
                    continue;

                Modules.Add(metadataDataSet.Modules[i].Namespace);
                ModuleRows.Add(metadataDataSet.Modules[i].Namespace + ".", metadataDataSet.Modules[i]);
            }

            for (var i = 0; i < metadataDataSet.UserDefinedDataTypes.Count; i++)
            {
                UserDefinedDataTypes.Add(metadataDataSet.UserDefinedDataTypes[i].Namespace);
                UserDefinedDataTypeRows.Add(metadataDataSet.UserDefinedDataTypes[i].Namespace + ".", metadataDataSet.UserDefinedDataTypes[i]);
            }
        }

		public void FillCatalogs(MetadataDataSet metadataDataSet)
		{
		    Catalogs.Clear();
		    CatalogRows.Clear();

            for (var i = 0; i < metadataDataSet.Catalogs.Count; i++)
            {
                Catalogs.Add(metadataDataSet.Catalogs[i].Namespace);
                CatalogRows.Add(metadataDataSet.Catalogs[i].Namespace + ".", metadataDataSet.Catalogs[i]);
            }
		}

		#endregion Public Methods 

		#endregion Methods 
    }
}