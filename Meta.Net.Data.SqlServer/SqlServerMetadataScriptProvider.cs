using System;
using System.Collections.Generic;
using System.Text;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.SqlServer
{
    public class SqlServerMetadataScriptProvider : IDataMetadataScriptProvider
    {
		#region Properties (16) 

        public string CatalogsSql
        {
            get
            {
                return
                    @"
SELECT
    db.[name] [Namespace]
  , db.[name] [ObjectName]
  , 'Catalog' [Description]
  , db.[create_date] [CreateDate]
  , db.[compatibility_level] [CompatibilityLevel]
  , IsNull(db.[collation_name], '') [CollationName]
  , db.[user_access] [UserAccess]
  , db.[user_access_desc] [UserAccessDescription]
  , db.[is_read_only] [IsReadOnly]
  , db.[is_auto_close_on] [IsAutoCloseOn]
  , db.[is_auto_shrink_on] [IsAutoShrinkOn]
  , db.[state] [State]
  , db.[state_desc] [StateDescription]
  , db.[is_in_standby] [IsInStandby]
  , db.[is_cleanly_shutdown] [IsCleanlyShutdown]
  , db.[is_supplemental_logging_enabled] [IsSupplementalLoggingEnabled]
  , db.[recovery_model] [RecoveryModel]
  , db.[recovery_model_desc] [RecoveryModelDescription]
  , db.[page_verify_option] [PageVerifyOption]
  , db.[page_verify_option_desc] [PageVerifyOptionDescription]
  , db.[is_auto_create_stats_on] [IsAutoCreateStatsOn]
  , db.[is_auto_update_stats_on] [IsAutoUpdateStatsOn]
  , db.[is_auto_update_stats_async_on] [IsAutoUpdateStatsAsyncOn]
  , db.[is_ansi_null_default_on] [IsAnsiNullDefaultOn]
  , db.[is_ansi_nulls_on] [IsAnsiNullsOn]
  , db.[is_ansi_padding_on] [IsAnsiPaddingOn]
  , db.[is_ansi_warnings_on] [IsAnsiWarningsOn]
  , db.[is_arithabort_on] [IsArithabortOn]
  , db.[is_concat_null_yields_null_on] [IsConcatNullYieldsNullOn]
  , db.[is_numeric_roundabort_on] [IsNumericRoundabortOn]
  , db.[is_quoted_identifier_on] [IsQuotedIdentifierOn]
  , db.[is_recursive_triggers_on] [IsRecursiveTriggersOn]
  , db.[is_cursor_close_on_commit_on] [IsCursorCloseOnCommitOn]
  , db.[is_local_cursor_default] [IsLocalCursorDefault]
  , db.[is_fulltext_enabled] [IsFulltextEnabled]
  , db.[is_trustworthy_on] [IsTrustworthyOn]
  , db.[is_db_chaining_on] [IsDbChainingOn]
  , db.[is_parameterization_forced] [IsParameterizationForced]
  , db.[is_master_key_encrypted_by_server] [IsMasterKeyEncryptedByServer]
  , db.[is_date_correlation_on] [IsDateCorrelationOn]
FROM [sys].[databases] db
WHERE db.[source_database_id] IS NULL AND db.[owner_sid] <> 0x01
ORDER BY [ObjectName]
";
            }
        }

        /// <summary>
        /// Gets a copy of the internal HashSet or sets the value of the internal HashSet to contain the same elements
        /// being passed in and regenerates the querries to match the given catalog collection. If there are no updates
        /// to the internal collection the metadata querries will not be regenerated as they will not waste the
        /// time to regenerate them all. If you pass this a null reference or an empty collection it will cause
        /// the internal HashSet to clear() and the querry strings will all be set to null so that if they are used
        /// it will cause the application to fail and throw an exception.
        /// </summary>
        public HashSet<string> CatalogsToFill
        {
            get
            {
                var catalogsToFillCopy = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                catalogsToFillCopy.UnionWith(_catalogsToFill);
                return catalogsToFillCopy;
            }

            set
            {
                if (value == null)
                {
                    Clear();
                    return;
                }

                if (value.Count == 0)
                {
                    Clear();
                    return;
                }

                if (_catalogsToFill.SetEquals(value))
                    return;

                _catalogsToFill.Clear();
                _catalogsToFill.UnionWith(value);
                UpdateSqlMetadataScripts();
            }
        }

        public string CheckConstraintsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_checkConstraintsSql)
                    ? _checkConstraintsSql
                    : ShellLogicExecuter(() => CheckConstraintsShellSql, () => CheckConstraintsInnerSql, _catalogsToFill);
            }
        }

        public string ComputedColumnsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_computedColumnsSql)
                    ? _computedColumnsSql
                    : ShellLogicExecuter(() => ComputedColumnsShellSql, () => ComputedColumnsInnerSql, _catalogsToFill);
            }
        }

        public string DefaultConstraintsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_defaultConstraintsSql)
                    ? _defaultConstraintsSql
                    : ShellLogicExecuter(() => DefaultConstraintsShellSql, () => DefaultConstraintsInnerSql, _catalogsToFill);
            }
        }

        public string ForeignKeyMapsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_foreignKeyMapsSql)
                    ? _foreignKeyMapsSql
                    : ShellLogicExecuter(() => ForeignKeyMapsShellSql, () => ForeignKeyMapsInnerSql, _catalogsToFill);
            }
        }

        public string ForeignKeysSql
        {
            get
            {
                return !string.IsNullOrEmpty(_foreignKeysSql)
                    ? _foreignKeysSql
                    : ShellLogicExecuter(() => ForeignKeysShellSql, () => ForeignKeysInnerSql, _catalogsToFill);
            }
        }

        public string IdentityColumnsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_identityColumnsSql)
                    ? _identityColumnsSql
                    : ShellLogicExecuter(() => IdentityColumnsShellSql, () => IdentityColumnsInnerSql, _catalogsToFill);
            }
        }

        public string IndexesSql
        {
            get
            {
                return !string.IsNullOrEmpty(_indexesSql)
                    ? _indexesSql
                    : ShellLogicExecuter(() => IndexesShellSql, () => IndexesInnerSql, _catalogsToFill);
            }
        }

        public string ModulesSql
        {
            get
            {
                return !string.IsNullOrEmpty(_modulesSql)
                    ? _modulesSql
                    : ShellLogicExecuter(() => ModulesShellSql, () => ModulesInnerSql, _catalogsToFill);
            }
        }

        public string PrimaryKeysSql
        {
            get
            {
                return !string.IsNullOrEmpty(_primaryKeysSql)
                    ? _primaryKeysSql
                    : ShellLogicExecuter(() => PrimaryKeysShellSql, () => PrimaryKeysInnerSql, _catalogsToFill);
            }
        }

        public string SchemasSql
        {
            get
            {
                return !string.IsNullOrEmpty(_schemasSql)
                    ? _schemasSql
                    : ShellLogicExecuter(() => SchemasShellSql, () => SchemasInnerSql, _catalogsToFill);
            }
        }

        public string UniqueConstraintsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_uniqueConstraintsSql)
                    ? _uniqueConstraintsSql
                    : ShellLogicExecuter(() => UniqueConstraintsShellSql, () => UniqueConstraintsInnerSql, _catalogsToFill);
            }
        }

        public string UserDefinedDataTypesSql
        {
            get
            {
                return !string.IsNullOrEmpty(_userDefinedDataTypesSql)
                    ? _userDefinedDataTypesSql
                    : ShellLogicExecuter(() => UserDefinedDataTypesShellSql, () => UserDefinedDataTypesInnerSql, _catalogsToFill);
            }
        }

        public string UserTableColumnsSql
        {
            get
            {
                return !string.IsNullOrEmpty(_userTableColumnsSql)
                    ? _userTableColumnsSql
                    : ShellLogicExecuter(() => UserTableColumnsShellSql, () => UserTableColumnsInnerSql, _catalogsToFill);
            }
        }

        public string UserTablesSql
        {
            get
            {
                return !string.IsNullOrEmpty(_userTablesSql)
                    ? _userTablesSql
                    : ShellLogicExecuter(() => UserTablesShellSql, () => UserTablesInnerSql, _catalogsToFill);
            }
        }

		#endregion Properties 

		#region Fields (41) 

        private HashSet<string> _catalogsToFill;
        private string _checkConstraintsSql;
        private string _computedColumnsSql;
        private string _defaultConstraintsSql;
        private string _foreignKeyMapsSql;
        private string _foreignKeysSql;
        private string _identityColumnsSql;
        private string _indexesSql;
        private string _modulesSql;
        private string _primaryKeysSql;
        private string _schemasSql;
        private string _uniqueConstraintsSql;
        private string _userDefinedDataTypesSql;
        private string _userTableColumnsSql;
        private string _userTablesSql;

        private const string UnionAllInnerSql = @"
    UNION ALL
";

        private const string SchemasInnerSql = @"
    SELECT DISTINCT
        '#Catalog#.' + [sch].[Name] [Namespace]
      , '#Catalog#' [CatalogName]
      , [sch].[name] [ObjectName]
      , 'Schema' [Description]
    FROM [thesocialhut].[sys].[schemas] [sch]
        INNER JOIN [#Catalog#].[sys].[objects] [obj] ON [sch].[schema_id] = [obj].[schema_id]
    WHERE [obj].[is_ms_shipped] = 0
";

        private const string SchemasShellSql = @"
SELECT * FROM
(
#Inner#
) [Schemas]
ORDER BY [Schemas].[Namespace]
";

        private const string CheckConstraintsInnerSql = @"
SELECT
    '#Catalog#.' + [sch].[Name] + '.' + [tab].[Name] + '.' + [chk].[name] [Namespace]
  , '#Catalog#' [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , IsNull(col.[Name], '') [ColumnName]
  , chk.[name] [ObjectName]
  , 'Check Constraint' [Description]
  , chk.[definition] [Definition]
  , CASE WHEN chk.[parent_column_id] = 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsTableConstraint]
  , chk.[is_disabled] [IsDisabled]
  , chk.[is_not_for_replication] [IsNotForReplication]
  , chk.[is_not_trusted] [IsNotTrusted]
  , chk.[is_system_named] [IsSystemNamed]
FROM [sys].[check_constraints] chk
    INNER JOIN [sys].[objects] obj ON chk.[parent_object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON obj.[object_id] = tab.[object_id]
    LEFT JOIN [sys].[columns] col ON chk.[parent_object_id] = col.[object_id] AND chk.[parent_column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
";

        private const string CheckConstraintsShellSql = @"
SELECT * FROM
(
#Inner#
) [CheckConstraints]
ORDER BY [CheckConstraints].[Namespace]
";

        private const string ComputedColumnsInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'Computed Column' [Description]
  , cmp.[definition] [Definition]
  , cmp.[is_persisted] [IsPersisted]
  , cmp.[is_nullable] [IsNullable]
FROM [sys].[computed_columns] cmp
    INNER JOIN [sys].[objects] obj ON cmp.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON obj.[object_id] = tab.[object_id]
    INNER JOIN [sys].[columns] col ON cmp.[object_id] = col.[object_id] AND cmp.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";

        private const string ComputedColumnsShellSql = @"
SELECT * FROM
(
#Inner#
) [ComputedColumns]
ORDER BY [ComputedColumns].[Namespace]
";

        private const string DefaultConstraintsInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + dc.[name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , dc.[name] [ObjectName]
  , 'Default Constraint' [Description]
  , dc.[definition] [Definition]
  , dc.[is_system_named] [IsSystemNamed]
FROM [sys].[default_constraints] dc
    INNER JOIN [sys].[objects] obj ON dc.[parent_object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  obj.[object_id] = tab.[object_id]
    INNER JOIN [sys].[columns] col ON dc.[parent_object_id] = col.[object_id] AND dc.[parent_column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";

        private const string DefaultConstraintsShellSql = @"
SELECT * FROM
(
#Inner#
) [DefaultConstraints]
ORDER BY [DefaultConstraints].[Namespace]
";

        private const string ForeignKeyMapsInnerSql = @"
SELECT
    fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] + '.' + fk_c.[Name] + '.'
        + pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] + '.' + pk.[column_name] [Namespace]
  , fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] + '.'
        + pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] [NamespaceGroup]
  , pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] + '.' + pk.[column_name] + '.'
        + fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] + '.' + fk_c.[Name] [NamespaceInverse]
  , pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] + '.'
        + fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] [NamespaceInverseGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , fk_sch.[name] [SchemaName]
  , fk_tab.[name] [TableName]
  , fk_c.[name] [ColumnName]
  , fk.[name] [ObjectName]
  , 'Reference Mapping' [Description]
  ,  fk_col.[constraint_column_id] [KeyOrdinal]
  , DB_NAME(DB_ID()) [ReferencedCatalogName]
  , pk.[schema_name] [ReferencedSchemaName]
  , pk.[table_name] [ReferencedTableName]
  , pk.[column_name] [ReferencedColumnName]
  , pk.[index_name] [ReferencedObjectName]
FROM [sys].[foreign_keys] fk
    INNER JOIN [sys].[objects] fk_obj ON fk.[object_id] = fk_obj.[object_id]
    INNER JOIN [sys].[schemas] fk_sch ON fk_obj.[schema_id] = fk_sch.[schema_id]
    INNER JOIN [sys].[tables] fk_tab ON  fk.[parent_object_id] = fk_tab.[object_id]
    INNER JOIN [sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
    INNER JOIN [sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
    INNER JOIN
    (
        SELECT
            ind.[name] [index_name]
          , obj.[object_id]
          , sch.[name] [schema_name]
          , tab.[name] [table_name]
          , col.[name] [column_name]
          , col.[column_id] [column_id]
        FROM [sys].[indexes] ind
            INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
            INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
            INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
            INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
            INNER JOIN [sys].[columns] col ON icol.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
        WHERE obj.[is_ms_shipped] = 0
    ) pk ON fk_col.[referenced_object_id] = pk.[object_id] AND fk_col.[referenced_column_id] = pk.[column_id]
WHERE fk_obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string ForeignKeyMapsShellSql = @"
SELECT * FROM
(
#Inner#
) [ForeignKeyMaps]
ORDER BY [ForeignKeyMaps].[Namespace]
";

        private const string ForeignKeysInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + fk.[name] + '.' + fk_c.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + fk.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , fk_c.[name] [ColumnName]
  , fk.[name] [ObjectName]
  , 'Foreign Key' [Description]
  , fk_col.[constraint_column_id] [KeyOrdinal]
  , fk.[is_disabled] [IsDisabled]
  , fk.[is_not_for_replication] [IsNotForReplication]
  , fk.[is_not_trusted] [IsNotTrusted]
  , fk.[delete_referential_action] [DeleteAction]
  , fk.[delete_referential_action_desc] [DeleteActionDescription]
  , fk.[update_referential_action] [UpdateAction]
  , fk.[update_referential_action_desc] [UpdateActionDescription]
  , fk.[is_system_named] [IsSystemNamed]
FROM [sys].[foreign_keys] fk
    INNER JOIN [sys].[objects] obj ON fk.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  fk.[parent_object_id] = tab.[object_id]
    INNER JOIN [sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
    INNER JOIN [sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string ForeignKeysShellSql = @"
SELECT * FROM
(
#Inner#
) [ForeignKeys]
ORDER BY [ForeignKeys].[Namespace]
";

        private const string IdentityColumnsInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'Identity Column' [Description]
  , ic.[seed_value] [SeedValue]
  , ic.[increment_value] [IncrementValue]
  , ic.[is_not_for_replication] [IsNotForReplication]
FROM [sys].[identity_columns] ic
    INNER JOIN [sys].[objects] obj ON ic.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  obj.[object_id] = tab.[object_id]
    INNER JOIN [sys].[columns] col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";

        private const string IdentityColumnsShellSql = @"
SELECT * FROM
(
#Inner#
) [IdentityColumns]
ORDER BY [IdentityColumns].[Namespace]
";

        private const string IndexesInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] + '.' + col.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , ind.[name] [ObjectName]
  , 'Index' [Description]
  , flg.[name] [FileGroup]
  , CASE WHEN icol.[key_ordinal] = 0 THEN icol.[index_column_id] + 1024 ELSE icol.[key_ordinal] END [KeyOrdinal]
  , icol.[partition_ordinal] [PartitionOrdinal]
  , CASE WHEN ind.[type] = 1 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsClustered]
  , icol.[is_descending_key] [IsDescendingKey]
  , icol.[is_included_column] [IsIncludedColumn]
  , ind.[is_unique] [IsUnique]
  , ind.[ignore_dup_key] [IgnoreDupKey]
  , ind.[fill_factor] [FillFactor]
  , ind.[is_padded] [IsPadded]
  , ind.[is_disabled] [IsDisabled]
  , ind.[allow_row_locks] [AllowRowLocks]
  , ind.[allow_page_locks] [AllowPageLocks]
  , '' [IndexType]
FROM [sys].[indexes] ind
    INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 0 AND ind.[is_unique_constraint] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string IndexesShellSql = @"
SELECT * FROM
(
#Inner#
) [Indexes]
ORDER BY [Indexes].[Namespace]
";

        private const string ModulesInnerSql = @"
SELECT
    sch.[Name] + '.' + obj.[name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , obj.[name] [ObjectName]
  , 'Module' [Description]
  , obj.[type_desc] [TypeDescription]
  , mod.[definition] [Definition]
  , mod.[uses_ansi_nulls] [UsesAnsiNulls]
  , mod.[uses_quoted_identifier] [UsesQuotedIdentifier]
  , isNull(trig.[is_not_for_replication], 0) [IsNotForReplication]
  , isNull(trig.[is_disabled], 0) [IsDisabled]
  , isNull(trigps.[name], '') [TriggerForSchema]
  , isNull(trigp.[name], '') [TriggerForObjectName]
FROM [sys].[sql_modules] mod
    INNER JOIN [sys].[objects] obj ON mod.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    LEFT JOIN [sys].[triggers] trig ON obj.[object_id] = trig.[object_id]
    LEFT JOIN [sys].[objects] trigp ON trig.[parent_id] = trigp.[object_id]
    LEFT JOIN [sys].[schemas] trigps ON trigp.[schema_id] = trigps.[schema_id]
WHERE obj.[is_ms_shipped] = 0 AND isNull(trigp.[is_ms_shipped], 0) = 0
ORDER BY [CatalogName], [SchemaName], [ObjectName]
";

        private const string ModulesShellSql = @"
SELECT * FROM
(
#Inner#
) [Modules]
ORDER BY [Modules].[Namespace]
";

        private const string PrimaryKeysInnerSql = @"";

        private const string PrimaryKeysShellSql = @"
SELECT * FROM
(
#Inner#
) [PrimaryKeys]
ORDER BY [PrimaryKeys].[Namespace]
";

        private const string UniqueConstraintsInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] + '.' + col.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , ind.[name] [ObjectName]
  , 'Unique Constraint' [Description]
  , flg.[name] [FileGroup]
  , icol.[key_ordinal] [KeyOrdinal]
  , icol.[partition_ordinal] [PartitionOrdinal]
  , CASE WHEN ind.[type] = 1 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsClustered]
  , icol.[is_descending_key] [IsDescendingKey]
  , ind.[ignore_dup_key] [IgnoreDupKey]
  , ind.[fill_factor] [FillFactor]
  , ind.[is_padded] [IsPadded]
  , ind.[is_disabled] [IsDisabled]
  , ind.[allow_row_locks] [AllowRowLocks]
  , ind.[allow_page_locks] [AllowPageLocks]
  , '' [IndexType]
FROM [sys].[indexes] ind
    INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 0 AND ind.[is_unique_constraint] = 1
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string UniqueConstraintsShellSql = @"
SELECT * FROM
(
#Inner#
) [UniqueConstraints]
ORDER BY [UniqueConstraints].[Namespace]
";

        private const string UserDefinedDataTypesInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'User-Table Column' [Description]
  , col.[column_id] [ColumnOrdinal]
  , typ.[name] [DataType]
  , col.[max_length] [MaxLength]
  , col.[precision] [Precision]
  , col.[scale] [Scale]
  , IsNull(col.[collation_name], IsNull(db.[collation_name], '')) [Collation]
  , CASE WHEN col.[default_object_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasDefault]
  , CASE WHEN col.[xml_collection_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasXmlCollection]
  , typ.[is_user_defined] [IsUserDefined]
  , typ.[is_assembly_type] [IsAssemblyType]
  , col.[is_nullable] [IsNullable]
  , col.[is_ansi_padded] [IsAnsiPadded]
  , col.[is_rowguidcol] [IsRowGuidColumn]
  , col.[is_identity] [IsIdentity]
  , col.[is_computed] [IsComputed]
  , col.[is_filestream] [IsFileStream]
  , col.[is_xml_document] [IsXmlDocument]
FROM [sys].[columns] col
    INNER JOIN [sys].[objects] obj ON col.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  col.[object_id] = tab.[object_id]
    INNER JOIN [sys].[types] typ ON col.[system_type_id] = typ.[system_type_id] AND col.[user_type_id] = typ.[user_type_id]
    LEFT JOIN [sys].[databases] db ON db.[name] = DB_NAME(DB_ID()) AND typ.[name] IN ('char', 'varchar', 'nchar', 'nvarchar')
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ColumnOrdinal]
";

        private const string UserDefinedDataTypesShellSql = @"
SELECT * FROM
(
#Inner#
) [UserDefinedDataTypes]
ORDER BY [UserDefinedDataTypes].[Namespace]
";

        private const string UserTableColumnsInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'User-Table Column' [Description]
  , col.[column_id] [ColumnOrdinal]
  , typ.[name] [DataType]
  , col.[max_length] [MaxLength]
  , col.[precision] [Precision]
  , col.[scale] [Scale]
  , IsNull(col.[collation_name], IsNull(db.[collation_name], '')) [Collation]
  , CASE WHEN col.[default_object_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasDefault]
  , CASE WHEN col.[xml_collection_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasXmlCollection]
  , typ.[is_user_defined] [IsUserDefined]
  , typ.[is_assembly_type] [IsAssemblyType]
  , col.[is_nullable] [IsNullable]
  , col.[is_ansi_padded] [IsAnsiPadded]
  , col.[is_rowguidcol] [IsRowGuidColumn]
  , col.[is_identity] [IsIdentity]
  , col.[is_computed] [IsComputed]
  , col.[is_filestream] [IsFileStream]
  , col.[is_xml_document] [IsXmlDocument]
FROM [sys].[columns] col
    INNER JOIN [sys].[objects] obj ON col.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  col.[object_id] = tab.[object_id]
    INNER JOIN [sys].[types] typ ON col.[system_type_id] = typ.[system_type_id] AND col.[user_type_id] = typ.[user_type_id]
    LEFT JOIN [sys].[databases] db ON db.[name] = DB_NAME(DB_ID()) AND typ.[name] IN ('char', 'varchar', 'nchar', 'nvarchar')
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ColumnOrdinal]
";

        private const string UserTableColumnsShellSql = @"
SELECT * FROM
(
#Inner#
) [UserTableColumns]
ORDER BY [UserTableColumns].[Namespace]
";

        private const string UserTablesInnerSql = @"
SELECT
    sch.[Name] + '.' + tab.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [ObjectName]
  , 'User-Table' [Description]
  , tab.[type_desc] [TypeDescription]
  , CASE WHEN fs_flg.[name] IS NOT NULL THEN fs_flg.[name] ELSE flg.[name] END [FileStreamFileGroup]
  , CASE WHEN lob_flg.[name] IS NOT NULL THEN lob_flg.[name] ELSE flg.[name] END [LobFileGroup]
  , CASE WHEN tab.[lob_data_space_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasTextNTextOrImageColumns]
  , tab.[uses_ansi_nulls] [UsesAnsiNulls]
  , tab.[text_in_row_limit] [TextInRowLimit]
FROM [sys].[tables] tab
    INNER JOIN [sys].[objects] obj ON tab.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[filegroups] flg ON flg.[is_default] = 1
    LEFT JOIN [sys].[filegroups] fs_flg ON tab.[filestream_data_space_id] = fs_flg.[data_space_id]
    LEFT JOIN [sys].[filegroups] lob_flg ON tab.[lob_data_space_id] = lob_flg.[data_space_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [ObjectName]
";

        private const string UserTablesShellSql = @"
SELECT * FROM
(
#Inner#
) [UserTables]
ORDER BY [UserTables].[Namespace]
";

		#endregion Fields 

		#region Constructors (3) 

        ///// <summary>
        ///// Fills CatalogsToFill with catalogs parameter and prefills
        ///// all Sql Strings with the correct querries based on the InitialCatalog.
        ///// </summary>
        ///// <param name="catalogs">The catalogs to base the querries from.</param>
        //public SqlServerMetadataScriptProvider(IEnumerable<string> catalogs)
        //{
        //    Init(null, catalogs);
        //}

        // Temp For Build
        public SqlServerMetadataScriptProvider()
        {
            
        }

        /// <summary>
        /// Fills CatalogsToFill with InitialCatalog from IDataConnectionInfo and prefills
        /// all Sql Strings with the correct querries based on the InitialCatalog.
        /// </summary>
        /// <param name="connectionInfo">The connectionInfo object to grab the InitialCatalog from.</param>
        public SqlServerMetadataScriptProvider(IDataConnectionInfo connectionInfo)
        {
            Init(connectionInfo, null);
        }

        ///// <summary>
        ///// Should never be called or implemented.
        ///// </summary>
        //protected SqlServerMetadataScriptProvider()
        //protected SqlServerMetadataScriptProvider()
        //{
        //    // Never implement!
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Should never be called or implemented.
        /// Temporarily only use if calling MetadataScriptProvider(#2) methods.
        /// </summary>
        /// <param name="catalogs">The catalogs to use.</param>
        public SqlServerMetadataScriptProvider(IEnumerable<string> catalogs)
        {
            _catalogsToFill = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Clear();
            // Never implement!
            //throw new NotImplementedException();
        }

		#endregion Constructors 

		#region Methods (4) 

		#region Public Methods (2) 

        /// <summary>
        /// Clears internal HashSet and sets all strings to null.
        /// </summary>
        public void Clear()
        {
            _catalogsToFill.Clear();
            _schemasSql = null;
            _checkConstraintsSql = null;
            _computedColumnsSql = null;
            _defaultConstraintsSql = null;
            _userTablesSql = null;
            _userTableColumnsSql = null;
            _userDefinedDataTypesSql = null;
            _uniqueConstraintsSql = null;
            _primaryKeysSql = null;
            _modulesSql = null;
            _indexesSql = null;
            _identityColumnsSql = null;
            _foreignKeysSql = null;
            _foreignKeyMapsSql = null;
            return;
        }

        /// <summary>
        /// Generates each SqlMetadataScript by calling ShellLogicExecuter for each involved function or simply
        /// grabbing pre-generated querries for metadata that does not exist in the case of MySql.
        /// </summary>
        public void UpdateSqlMetadataScripts()
        {
            _schemasSql = ShellLogicExecuter(() => SchemasShellSql, () => SchemasInnerSql, _catalogsToFill);
            _checkConstraintsSql = ShellLogicExecuter(() => CheckConstraintsShellSql, () => CheckConstraintsInnerSql, _catalogsToFill);
            _computedColumnsSql = ShellLogicExecuter(() => ComputedColumnsShellSql, () => ComputedColumnsInnerSql, _catalogsToFill);
            _defaultConstraintsSql = ShellLogicExecuter(() => DefaultConstraintsShellSql, () => DefaultConstraintsInnerSql, _catalogsToFill);
            _userTablesSql = ShellLogicExecuter(() => UserTablesShellSql, () => UserTablesInnerSql, _catalogsToFill);
            _userTableColumnsSql = ShellLogicExecuter(() => UserTableColumnsShellSql, () => UserTableColumnsInnerSql, _catalogsToFill);
            _userDefinedDataTypesSql = ShellLogicExecuter(() => UserDefinedDataTypesShellSql, () => UserDefinedDataTypesInnerSql, _catalogsToFill);
            _uniqueConstraintsSql = ShellLogicExecuter(() => UniqueConstraintsShellSql, () => UniqueConstraintsInnerSql, _catalogsToFill);
            _primaryKeysSql = ShellLogicExecuter(() => PrimaryKeysShellSql, () => PrimaryKeysInnerSql, _catalogsToFill);
            _modulesSql = ShellLogicExecuter(() => ModulesShellSql, () => ModulesInnerSql, _catalogsToFill);
            _indexesSql = ShellLogicExecuter(() => IndexesShellSql, () => IndexesInnerSql, _catalogsToFill);
            _identityColumnsSql = ShellLogicExecuter(() => IdentityColumnsShellSql, () => IdentityColumnsInnerSql, _catalogsToFill);
            _foreignKeysSql = ShellLogicExecuter(() => ForeignKeysShellSql, () => ForeignKeysInnerSql, _catalogsToFill);
            _foreignKeyMapsSql = ShellLogicExecuter(() => ForeignKeyMapsShellSql, () => ForeignKeyMapsInnerSql, _catalogsToFill);
        }

		#endregion Public Methods 
		#region Private Methods (2) 

        private void Init(IDataConnectionInfo connectionInfo, IEnumerable<string> catalogs)
        {
            _catalogsToFill = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (connectionInfo != null)
            {
                if (string.IsNullOrEmpty(connectionInfo.InitialCatalog))
                    throw new Exception("SqlServerMetadataScriptProvider was instantiated with a IDataConnectionInfo object with a null or empty InitialCatalog string.");

                _catalogsToFill.Add(connectionInfo.InitialCatalog);
                UpdateSqlMetadataScripts();
                return;
            }

            if (catalogs == null)
                return;

            _catalogsToFill.UnionWith(catalogs);
            UpdateSqlMetadataScripts();
        }

        /// <summary>
        /// Executes the functions passed into the method where the innerSqlFunction gets executed on every catalog string value in this instance's
        /// CatalogsToFill Hashset of type string where string is non case sensitive as StringComparer.OrdinalIgnoreCase is used to instantiate the
        /// Hashset. InnerShellFunctions become concatonated with UnionAllSql between eachother and then are sorted in the ShellFunction. Without
        /// this function's capabilities the code for this function would be in every Sql method and would be messy. Also, this enables us to write
        /// code to target each individual catalog in SQL Server all at once instead of pulling metadata a catalog at a time, therefor increasing
        /// the metadata fetch speeds when there may be hundreds of catalogs. Please notice that MySql's implementation will not need this type
        /// of functionality as we will simply need to pass in the catalogs as a array of comma seperated values like the following:
        /// WHERE sch.`SCHEMA_NAME` IN ('catalog1', 'catalog2')...
        /// This cannot be accomplished the same way for SQL Server and so we are stuck making a huge query to each catalogs's [catalog1].[sys].[objects]
        /// , etc. for every catalog in which the [sys] schema views for the SQL Server metadata only let the connection fetch info for that particular
        /// catalog. However in naming it this way we can still fetch this info from any catalog granted we have permissions to pull data from that
        /// catalog's view.
        /// </summary>
        /// <param name="shellSqlFunction">The ShellSql function to execute.</param>
        /// <param name="innerSqlFunction">The InnerSql function to execute inside of the ShellSql wrapper function for each catalog in catalogsToFill.</param>
        /// <param name="catalogsToFill">The local instances reference to _catalogsToFill.</param>
        /// <returns>The query string to be executed against the database to fetch metadata for that particular metadata type.</returns>
        private static string ShellLogicExecuter(Func<string> shellSqlFunction, Func<string> innerSqlFunction, ICollection<string> catalogsToFill)
        {
            const string errorBegin = "SqlServerMetadataScriptProvider.ShellLogicExecuter was passed ";
            if (shellSqlFunction == null || innerSqlFunction == null)
                throw new Exception(errorBegin + "a null value for one or both of the Func<string> parameters.");

            if (catalogsToFill == null)
                throw new Exception(errorBegin + "a null value for the catalogsToFill ICollection parameter.");

            var count = catalogsToFill.Count;
            
            if (count == 0)
                throw new Exception(errorBegin + "an empty collection for the catalogsToFill ICollection parameter.");

            // Creating quick copy of catalogsToFill to filter through to quickly release the use of references to
            // catalogsToFill in case the collection gets updated during the processing of this method.
            var catalogs = new string[count];
            catalogsToFill.CopyTo(catalogs, 0);

            var builder = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                if (string.IsNullOrEmpty(catalogs[i]))
                    throw new Exception("SqlServerMetadataScriptProvider.CatalogsToFill contained a null or empty string.");

                builder.Append(innerSqlFunction.Invoke().Replace("#Catalog#", catalogs[i]));

                // Do not place the UnionAllSql if there is only one innerSqlFunction or after the last innerSqlFunction.
                if (i + 1 < count)
                    builder.Append(UnionAllInnerSql);
            }

            return shellSqlFunction.Invoke().Replace("#Inner#", builder.ToString());
        }

		#endregion Private Methods 

		#endregion Methods 

        #region oldMethods

        public string SchemasSql2(string catalogName)
        {
            return @"
SELECT DISTINCT
    [sch].[Name] [Namespace]
    , DB_NAME(DB_ID()) [CatalogName]
    , [sch].[name] [ObjectName]
    , 'Schema' [Description]
FROM [sys].[schemas] [sch]
    INNER JOIN [sys].[objects] [obj] ON [sch].[schema_id] = [obj].[schema_id]
WHERE [obj].[is_ms_shipped] = 0
ORDER BY [CatalogName], [ObjectName]
";
        }

        public string CheckConstraintsSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + chk.[name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , IsNull(col.[Name], '') [ColumnName]
  , chk.[name] [ObjectName]
  , 'Check Constraint' [Description]
  , chk.[definition] [Definition]
  , CASE WHEN chk.[parent_column_id] = 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsTableConstraint]
  , chk.[is_disabled] [IsDisabled]
  , chk.[is_not_for_replication] [IsNotForReplication]
  , chk.[is_not_trusted] [IsNotTrusted]
  , chk.[is_system_named] [IsSystemNamed]
FROM [sys].[check_constraints] chk
    INNER JOIN [sys].[objects] obj ON chk.[parent_object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON obj.[object_id] = tab.[object_id]
    LEFT JOIN [sys].[columns] col ON chk.[parent_object_id] = col.[object_id] AND chk.[parent_column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";
        }
        public string ComputedColumnsSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'Computed Column' [Description]
  , cmp.[definition] [Definition]
  , cmp.[is_persisted] [IsPersisted]
  , cmp.[is_nullable] [IsNullable]
FROM [sys].[computed_columns] cmp
    INNER JOIN [sys].[objects] obj ON cmp.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON obj.[object_id] = tab.[object_id]
    INNER JOIN [sys].[columns] col ON cmp.[object_id] = col.[object_id] AND cmp.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";
        }
        public string DefaultConstraintsSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + dc.[name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , dc.[name] [ObjectName]
  , 'Default Constraint' [Description]
  , dc.[definition] [Definition]
  , dc.[is_system_named] [IsSystemNamed]
FROM [sys].[default_constraints] dc
    INNER JOIN [sys].[objects] obj ON dc.[parent_object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  obj.[object_id] = tab.[object_id]
    INNER JOIN [sys].[columns] col ON dc.[parent_object_id] = col.[object_id] AND dc.[parent_column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";
        }
        public string ForeignKeyMapsSql2(string catalogName)
        {
            return @"
SELECT
    fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] + '.' + fk_c.[Name] + '.'
        + pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] + '.' + pk.[column_name] [Namespace]
  , fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] + '.'
        + pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] [NamespaceGroup]
  , pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] + '.' + pk.[column_name] + '.'
        + fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] + '.' + fk_c.[Name] [NamespaceInverse]
  , pk.[schema_name] + '.' + pk.[table_name] + '.' + pk.[index_name] + '.'
        + fk_sch.[Name] + '.' + fk_tab.[Name] + '.' + fk.[name] [NamespaceInverseGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , fk_sch.[name] [SchemaName]
  , fk_tab.[name] [TableName]
  , fk_c.[name] [ColumnName]
  , fk.[name] [ObjectName]
  , 'Reference Mapping' [Description]
  ,  fk_col.[constraint_column_id] [KeyOrdinal]
  , DB_NAME(DB_ID()) [ReferencedCatalogName]
  , pk.[schema_name] [ReferencedSchemaName]
  , pk.[table_name] [ReferencedTableName]
  , pk.[column_name] [ReferencedColumnName]
  , pk.[index_name] [ReferencedObjectName]
FROM [sys].[foreign_keys] fk
    INNER JOIN [sys].[objects] fk_obj ON fk.[object_id] = fk_obj.[object_id]
    INNER JOIN [sys].[schemas] fk_sch ON fk_obj.[schema_id] = fk_sch.[schema_id]
    INNER JOIN [sys].[tables] fk_tab ON  fk.[parent_object_id] = fk_tab.[object_id]
    INNER JOIN [sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
    INNER JOIN [sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
    INNER JOIN
    (
        SELECT
            ind.[name] [index_name]
          , obj.[object_id]
          , sch.[name] [schema_name]
          , tab.[name] [table_name]
          , col.[name] [column_name]
          , col.[column_id] [column_id]
        FROM [sys].[indexes] ind
            INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
            INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
            INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
            INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
            INNER JOIN [sys].[columns] col ON icol.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
        WHERE obj.[is_ms_shipped] = 0
    ) pk ON fk_col.[referenced_object_id] = pk.[object_id] AND fk_col.[referenced_column_id] = pk.[column_id]
WHERE fk_obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";
        }
        public string ForeignKeysSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + fk.[name] + '.' + fk_c.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + fk.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , fk_c.[name] [ColumnName]
  , fk.[name] [ObjectName]
  , 'Foreign Key' [Description]
  , fk_col.[constraint_column_id] [KeyOrdinal]
  , fk.[is_disabled] [IsDisabled]
  , fk.[is_not_for_replication] [IsNotForReplication]
  , fk.[is_not_trusted] [IsNotTrusted]
  , fk.[delete_referential_action] [DeleteAction]
  , fk.[delete_referential_action_desc] [DeleteActionDescription]
  , fk.[update_referential_action] [UpdateAction]
  , fk.[update_referential_action_desc] [UpdateActionDescription]
  , fk.[is_system_named] [IsSystemNamed]
FROM [sys].[foreign_keys] fk
    INNER JOIN [sys].[objects] obj ON fk.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  fk.[parent_object_id] = tab.[object_id]
    INNER JOIN [sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
    INNER JOIN [sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";
        }
        public string IdentityColumnsSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'Identity Column' [Description]
  , ic.[seed_value] [SeedValue]
  , ic.[increment_value] [IncrementValue]
  , ic.[is_not_for_replication] [IsNotForReplication]
FROM [sys].[identity_columns] ic
    INNER JOIN [sys].[objects] obj ON ic.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  obj.[object_id] = tab.[object_id]
    INNER JOIN [sys].[columns] col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";
        }
        public string IndexesSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] + '.' + col.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , ind.[name] [ObjectName]
  , 'Index' [Description]
  , flg.[name] [FileGroup]
  , CASE WHEN icol.[key_ordinal] = 0 THEN icol.[index_column_id] + 1024 ELSE icol.[key_ordinal] END [KeyOrdinal]
  , icol.[partition_ordinal] [PartitionOrdinal]
  , CASE WHEN ind.[type] = 1 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsClustered]
  , icol.[is_descending_key] [IsDescendingKey]
  , icol.[is_included_column] [IsIncludedColumn]
  , ind.[is_unique] [IsUnique]
  , ind.[ignore_dup_key] [IgnoreDupKey]
  , ind.[fill_factor] [FillFactor]
  , ind.[is_padded] [IsPadded]
  , ind.[is_disabled] [IsDisabled]
  , ind.[allow_row_locks] [AllowRowLocks]
  , ind.[allow_page_locks] [AllowPageLocks]
  , '' [IndexType]
FROM [sys].[indexes] ind
    INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 0 AND ind.[is_unique_constraint] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";
        }
        public string ModulesSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + obj.[name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , obj.[name] [ObjectName]
  , 'Module' [Description]
  , obj.[type_desc] [TypeDescription]
  , mod.[definition] [Definition]
  , mod.[uses_ansi_nulls] [UsesAnsiNulls]
  , mod.[uses_quoted_identifier] [UsesQuotedIdentifier]
  , isNull(trig.[is_not_for_replication], 0) [IsNotForReplication]
  , isNull(trig.[is_disabled], 0) [IsDisabled]
  , isNull(trigps.[name], '') [TriggerForSchema]
  , isNull(trigp.[name], '') [TriggerForObjectName]
FROM [sys].[sql_modules] mod
    INNER JOIN [sys].[objects] obj ON mod.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    LEFT JOIN [sys].[triggers] trig ON obj.[object_id] = trig.[object_id]
    LEFT JOIN [sys].[objects] trigp ON trig.[parent_id] = trigp.[object_id]
    LEFT JOIN [sys].[schemas] trigps ON trigp.[schema_id] = trigps.[schema_id]
WHERE obj.[is_ms_shipped] = 0 AND isNull(trigp.[is_ms_shipped], 0) = 0
ORDER BY [CatalogName], [SchemaName], [ObjectName]
";
        }
        public string PrimaryKeysSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] + '.' + col.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , ind.[name] [ObjectName]
  , 'Primary Key' [Description]
  , flg.[name] [FileGroup]
  , icol.[key_ordinal] [KeyOrdinal]
  , icol.[partition_ordinal] [PartitionOrdinal]
  , icol.[is_descending_key] [IsDescendingKey]
  , ind.[ignore_dup_key] [IgnoreDupKey]
  , CASE WHEN ind.[type] = 1 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsClustered]
  , ind.[fill_factor] [FillFactor]
  , ind.[is_padded] [IsPadded]
  , ind.[is_disabled] [IsDisabled]
  , ind.[allow_row_locks] [AllowRowLocks]
  , ind.[allow_page_locks] [AllowPageLocks]
  , '' [IndexType]
FROM [sys].[indexes] ind
    INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 1
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";
        }
        public string UniqueConstraintsSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] + '.' + col.[Name] [Namespace]
  , sch.[Name] + '.' + tab.[Name] + '.' + ind.[name] [NamespaceGroup]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ColumnName]
  , ind.[name] [ObjectName]
  , 'Unique Constraint' [Description]
  , flg.[name] [FileGroup]
  , icol.[key_ordinal] [KeyOrdinal]
  , icol.[partition_ordinal] [PartitionOrdinal]
  , CASE WHEN ind.[type] = 1 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsClustered]
  , icol.[is_descending_key] [IsDescendingKey]
  , ind.[ignore_dup_key] [IgnoreDupKey]
  , ind.[fill_factor] [FillFactor]
  , ind.[is_padded] [IsPadded]
  , ind.[is_disabled] [IsDisabled]
  , ind.[allow_row_locks] [AllowRowLocks]
  , ind.[allow_page_locks] [AllowPageLocks]
  , '' [IndexType]
FROM [sys].[indexes] ind
    INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 0 AND ind.[is_unique_constraint] = 1
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";
        }
        public string UserDefinedDataTypesSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + typ.[name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , typ.[name] [ObjectName]
  , 'User-Defined Data Type' [Description]
  , base_typ.[name] [DataType]
  , typ.[max_length] [MaxLength]
  , typ.[precision] [Precision]
  , typ.[scale] [Scale]
  , isnull(typ.[collation_name], '') [Collation]
  , CASE WHEN typ.[default_object_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasDefault]
  , typ.[is_user_defined] [IsUserDefined]
  , typ.[is_assembly_type] [IsAssemblyType]
  , typ.[is_nullable] [IsNullable]
FROM [sys].[types] typ
    INNER JOIN [sys].[schemas] sch ON typ.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[types] base_typ ON typ.[system_type_id] = base_typ.[system_type_id]
        AND base_typ.[is_user_defined] = 0
WHERE typ.[is_user_defined] = 1
ORDER BY [SchemaName], [ObjectName]
";
        }
        public string UserTableColumnsSql2(string catalogName)
        {
            return @"
SELECT DISTINCT
    sch.[Name] + '.' + tab.[Name] + '.' + col.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [TableName]
  , col.[name] [ObjectName]
  , 'User-Table Column' [Description]
  , col.[column_id] [ColumnOrdinal]
  , typ.[name] [DataType]
  , col.[max_length] [MaxLength]
  , col.[precision] [Precision]
  , col.[scale] [Scale]
  , IsNull(col.[collation_name], IsNull(db.[collation_name], '')) [Collation]
  , CASE WHEN col.[default_object_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasDefault]
  , CASE WHEN IsNull([fk_maps].[ObjectName], '') = '' THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END [HasForeignKey]
  , CASE WHEN col.[xml_collection_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasXmlCollection]
  , typ.[is_user_defined] [IsUserDefined]
  , typ.[is_assembly_type] [IsAssemblyType]
  , col.[is_nullable] [IsNullable]
  , col.[is_ansi_padded] [IsAnsiPadded]
  , col.[is_rowguidcol] [IsRowGuidColumn]
  , col.[is_identity] [IsIdentity]
  , col.[is_computed] [IsComputed]
  , col.[is_filestream] [IsFileStream]
  , col.[is_xml_document] [IsXmlDocument]
FROM [sys].[columns] col
    INNER JOIN [sys].[objects] obj ON col.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[tables] tab ON  col.[object_id] = tab.[object_id]
    INNER JOIN [sys].[types] typ ON col.[system_type_id] = typ.[system_type_id] AND col.[user_type_id] = typ.[user_type_id]
    LEFT JOIN [sys].[databases] db ON db.[name] = DB_NAME(DB_ID()) AND typ.[name] IN ('char', 'varchar', 'nchar', 'nvarchar')
    LEFT JOIN (
		SELECT
			fk_sch.[name] [SchemaName]
		  , fk_tab.[name] [TableName]
		  , fk_c.[name] [ColumnName]
		  , fk.[name] [ObjectName]
		  , 'Reference Mapping' [Description]
		  ,  fk_col.[constraint_column_id] [KeyOrdinal]
		  , DB_NAME(DB_ID()) [ReferencedCatalogName]
		  , pk.[schema_name] [ReferencedSchemaName]
		  , pk.[table_name] [ReferencedTableName]
		  , pk.[column_name] [ReferencedColumnName]
		  , pk.[index_name] [ReferencedObjectName]
		FROM [sys].[foreign_keys] fk
			INNER JOIN [sys].[objects] fk_obj ON fk.[object_id] = fk_obj.[object_id]
			INNER JOIN [sys].[schemas] fk_sch ON fk_obj.[schema_id] = fk_sch.[schema_id]
			INNER JOIN [sys].[tables] fk_tab ON  fk.[parent_object_id] = fk_tab.[object_id]
			INNER JOIN [sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
			INNER JOIN [sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
			INNER JOIN
			(
				SELECT
					ind.[name] [index_name]
				  , obj.[object_id]
				  , sch.[name] [schema_name]
				  , tab.[name] [table_name]
				  , col.[name] [column_name]
				  , col.[column_id] [column_id]
				FROM [sys].[indexes] ind
					INNER JOIN [sys].[objects] obj ON ind.[object_id] = obj.[object_id]
					INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
					INNER JOIN [sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
					INNER JOIN [sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
					INNER JOIN [sys].[columns] col ON icol.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
				WHERE obj.[is_ms_shipped] = 0
			) pk ON fk_col.[referenced_object_id] = pk.[object_id] AND fk_col.[referenced_column_id] = pk.[column_id]
		WHERE fk_obj.[is_ms_shipped] = 0
    ) [fk_maps] ON sch.[name] = [fk_maps].[ReferencedSchemaName] AND tab.[name] = [fk_maps].[ReferencedTableName]
		AND [fk_maps].[ReferencedColumnName] = col.[name]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ColumnOrdinal]
";
        }
        public string UserTablesSql2(string catalogName)
        {
            return @"
SELECT
    sch.[Name] + '.' + tab.[Name] [Namespace]
  , DB_NAME(DB_ID()) [CatalogName]
  , sch.[name] [SchemaName]
  , tab.[name] [ObjectName]
  , 'User-Table' [Description]
  , tab.[type_desc] [TypeDescription]
  , CASE WHEN fs_flg.[name] IS NOT NULL THEN fs_flg.[name] ELSE flg.[name] END [FileStreamFileGroup]
  , CASE WHEN lob_flg.[name] IS NOT NULL THEN lob_flg.[name] ELSE flg.[name] END [LobFileGroup]
  , CASE WHEN tab.[lob_data_space_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasTextNTextOrImageColumns]
  , tab.[uses_ansi_nulls] [UsesAnsiNulls]
  , tab.[text_in_row_limit] [TextInRowLimit]
FROM [sys].[tables] tab
    INNER JOIN [sys].[objects] obj ON tab.[object_id] = obj.[object_id]
    INNER JOIN [sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [sys].[filegroups] flg ON flg.[is_default] = 1
    LEFT JOIN [sys].[filegroups] fs_flg ON tab.[filestream_data_space_id] = fs_flg.[data_space_id]
    LEFT JOIN [sys].[filegroups] lob_flg ON tab.[lob_data_space_id] = lob_flg.[data_space_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [ObjectName]
";
        }

        #endregion oldMethods
    }
}
