using System.Collections.Generic;
using System.Text;

namespace Meta.Net.SqlServer
{
    public class SqlServerMetadataScriptFactory : DataMetadataScriptFactory
    {
        public SqlServerMetadataScriptFactory()
        {
            CatalogToken = DefaultCatalogToken;
            OrderByObjectName = DefaultOrderByObjectName;
            CatalogsSql = DefaultCatalogsSql;
            SchemasSql = DefaultSchemasSql;
            CheckConstraintsSql = DefaultCheckConstraintsSql;
            ComputedColumnsSql = DefaultComputedColumnsSql;
            DefaultConstraintsSql = DefaultDefaultConstraintsSql;
            ForeignKeysSql = DefaultForeignKeysSql;
            IdentityColumnsSql = DefaultIdentityColumnsSql;
            IndexesSql = DefaultIndexesSql;
            ModulesSql = DefaultModulesSql;
            PrimaryKeysSql = DefaultPrimaryKeysSql;
            UniqueConstraintsSql = DefaultUniqueConstraintsSql;
            UserDefinedDataTypesSql = DefaultUserDefinedDataTypesSql;
            UserTableColumnsSql = DefaultUserTableColumnsSql;
            UserTablesSql = DefaultUserTablesSql;
        }

        private const string DefaultCatalogToken = "#Catalog#";
        private const string DefaultOrderByObjectName = "ORDER BY [ObjectName] ASC";

        public override string Catalogs(IList<string> catalogs)
        {
            var builder = new StringBuilder(CatalogsSql);
            builder.Replace(OrderByObjectName, " ");
            builder.Append("AND db.[name] COLLATE SQL_Latin1_General_CP1_CI_AS IN (");
            var count = catalogs == null
                ? 0
                : catalogs.Count;

            if (count > 0)
            {
                builder.AppendFormat("'{0}'", catalogs[0]);
                for (var i = 1; i < count; i++)
                    builder.AppendFormat("', {0}'", catalogs[i]);
            }
            builder.AppendLine(") ");
            builder.Append(OrderByObjectName);
            return builder.ToString();
        }

        private const string DefaultCatalogsSql = @"
SELECT
    db.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Catalog' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , db.[create_date] [CreateDate]
  , db.[compatibility_level] [CompatibilityLevel]
  , IsNull(db.[collation_name], '') COLLATE SQL_Latin1_General_CP1_CI_AS [CollationName]
  , db.[user_access] [UserAccess]
  , db.[user_access_desc]  COLLATE SQL_Latin1_General_CP1_CI_AS [UserAccessDescription]
  , db.[is_read_only] [IsReadOnly]
  , db.[is_auto_close_on] [IsAutoCloseOn]
  , db.[is_auto_shrink_on] [IsAutoShrinkOn]
  , db.[state] [State]
  , db.[state_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [StateDescription]
  , db.[is_in_standby] [IsInStandby]
  , db.[is_cleanly_shutdown] [IsCleanlyShutdown]
  , db.[is_supplemental_logging_enabled] [IsSupplementalLoggingEnabled]
  , db.[recovery_model] [RecoveryModel]
  , db.[recovery_model_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [RecoveryModelDescription]
  , db.[page_verify_option] [PageVerifyOption]
  , db.[page_verify_option_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [PageVerifyOptionDescription]
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
FROM [master].[sys].[databases] db
WHERE db.[source_database_id] IS NULL AND db.[owner_sid] <> 0x01
ORDER BY [ObjectName] ASC
";

        private const string DefaultSchemasSql = @"
SELECT DISTINCT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , [sch].[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Schema' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
FROM [#Catalog#].[sys].[schemas] [sch]
    INNER JOIN [#Catalog#].[sys].[objects] [obj] ON [sch].[schema_id] = [obj].[schema_id]
WHERE [obj].[is_ms_shipped] = 0
ORDER BY [ObjectName] ASC
";

        private const string DefaultCheckConstraintsSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , IsNull(col.[Name], '') COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
  , chk.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Check Constraint' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , chk.[definition] COLLATE SQL_Latin1_General_CP1_CI_AS [Definition]
  , CASE WHEN chk.[parent_column_id] = 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [IsTableConstraint]
  , chk.[is_disabled] [IsDisabled]
  , chk.[is_not_for_replication] [IsNotForReplication]
  , chk.[is_not_trusted] [IsNotTrusted]
  , chk.[is_system_named] [IsSystemNamed]
FROM [#Catalog#].[sys].[check_constraints] chk
    INNER JOIN [#Catalog#].[sys].[objects] obj ON chk.[parent_object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON obj.[object_id] = tab.[object_id]
    LEFT JOIN [#Catalog#].[sys].[columns] col ON chk.[parent_object_id] = col.[object_id] AND chk.[parent_column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";
        private const string DefaultComputedColumnsSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Computed Column' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , cmp.[definition] COLLATE SQL_Latin1_General_CP1_CI_AS [Definition]
  , cmp.[is_persisted] [IsPersisted]
  , cmp.[is_nullable] [IsNullable]
FROM [#Catalog#].[sys].[computed_columns] cmp
    INNER JOIN [#Catalog#].[sys].[objects] obj ON cmp.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON obj.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[columns] col ON cmp.[object_id] = col.[object_id] AND cmp.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";

        private const string DefaultDefaultConstraintsSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
  , dc.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Default Constraint' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , dc.[definition] COLLATE SQL_Latin1_General_CP1_CI_AS [Definition]
  , dc.[is_system_named] [IsSystemNamed]
FROM [#Catalog#].[sys].[default_constraints] dc
    INNER JOIN [#Catalog#].[sys].[objects] obj ON dc.[parent_object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON  obj.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[columns] col ON dc.[parent_object_id] = col.[object_id] AND dc.[parent_column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";

        private const string DefaultForeignKeysSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , fk_sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , fk_tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , fk_c.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
  , fk.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Foreign Key' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , fk_col.[constraint_column_id] [KeyOrdinal]
  , fk.[is_disabled] [IsDisabled]
  , fk.[is_not_for_replication] [IsNotForReplication]
  , fk.[is_not_trusted] [IsNotTrusted]
  , fk.[delete_referential_action] [DeleteAction]
  , fk.[delete_referential_action_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [DeleteActionDescription]
  , fk.[update_referential_action] [UpdateAction]
  , fk.[update_referential_action_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [UpdateActionDescription]
  , fk.[is_system_named] [IsSystemNamed]
  , '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedCatalogName]
  , pk.[schema_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedSchemaName]
  , pk.[table_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedTableName]
  , pk.[column_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedColumnName]
  , pk.[index_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedObjectName]
FROM [#Catalog#].[sys].[foreign_keys] fk
    INNER JOIN [#Catalog#].[sys].[objects] fk_obj ON fk.[object_id] = fk_obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] fk_sch ON fk_obj.[schema_id] = fk_sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] fk_tab ON  fk.[parent_object_id] = fk_tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
    INNER JOIN [#Catalog#].[sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
    INNER JOIN
    (
        SELECT
            ind.[name] [index_name]
          , obj.[object_id]
          , sch.[name] [schema_name]
          , tab.[name] [table_name]
          , col.[name] [column_name]
          , col.[column_id] [column_id]
        FROM [#Catalog#].[sys].[indexes] ind
            INNER JOIN [#Catalog#].[sys].[objects] obj ON ind.[object_id] = obj.[object_id]
            INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
            INNER JOIN [#Catalog#].[sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
            INNER JOIN [#Catalog#].[sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
            INNER JOIN [#Catalog#].[sys].[columns] col ON icol.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
        WHERE obj.[is_ms_shipped] = 0
    ) pk ON fk_col.[referenced_object_id] = pk.[object_id] AND fk_col.[referenced_column_id] = pk.[column_id]
WHERE fk_obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string DefaultIdentityColumnsSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Identity Column' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , ic.[seed_value] [SeedValue]
  , ic.[increment_value] [IncrementValue]
  , ic.[is_not_for_replication] [IsNotForReplication]
FROM [#Catalog#].[sys].[identity_columns] ic
    INNER JOIN [#Catalog#].[sys].[objects] obj ON ic.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON  obj.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[columns] col ON ic.[object_id] = col.[object_id] AND ic.[column_id] = col.[column_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName]
";

        private const string DefaultIndexesSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
  , ind.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Index' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , flg.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [FileGroup]
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
  , '' COLLATE SQL_Latin1_General_CP1_CI_AS [IndexType]
FROM [#Catalog#].[sys].[indexes] ind
    INNER JOIN [#Catalog#].[sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [#Catalog#].[sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [#Catalog#].[sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 0 AND ind.[is_unique_constraint] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string DefaultModulesSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , obj.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Module' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , obj.[type_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [TypeDescription]
  , mod.[definition] COLLATE SQL_Latin1_General_CP1_CI_AS [Definition]
  , mod.[uses_ansi_nulls] [UsesAnsiNulls]
  , mod.[uses_quoted_identifier] [UsesQuotedIdentifier]
  , isNull(trig.[is_not_for_replication], 0) [IsNotForReplication]
  , isNull(trig.[is_disabled], 0) [IsDisabled]
  , isNull(trigps.[name], '') COLLATE SQL_Latin1_General_CP1_CI_AS [TriggerForSchema]
  , isNull(trigp.[name], '') COLLATE SQL_Latin1_General_CP1_CI_AS [TriggerForObjectName]
FROM [#Catalog#].[sys].[sql_modules] mod
    INNER JOIN [#Catalog#].[sys].[objects] obj ON mod.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    LEFT JOIN [#Catalog#].[sys].[triggers] trig ON obj.[object_id] = trig.[object_id]
    LEFT JOIN [#Catalog#].[sys].[objects] trigp ON trig.[parent_id] = trigp.[object_id]
    LEFT JOIN [#Catalog#].[sys].[schemas] trigps ON trigp.[schema_id] = trigps.[schema_id]
WHERE obj.[is_ms_shipped] = 0 AND isNull(trigp.[is_ms_shipped], 0) = 0
ORDER BY [CatalogName], [SchemaName], [ObjectName]
";

        private const string DefaultPrimaryKeysSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
  , ind.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Primary Key' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , flg.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [FileGroup]
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
  , '' COLLATE SQL_Latin1_General_CP1_CI_AS [IndexType]
FROM [#Catalog#].[sys].[indexes] ind
    INNER JOIN [#Catalog#].[sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [#Catalog#].[sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [#Catalog#].[sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 1
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string DefaultUniqueConstraintsSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
  , ind.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'Unique Constraint' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , flg.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [FileGroup]
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
  , '' COLLATE SQL_Latin1_General_CP1_CI_AS [IndexType]
FROM [#Catalog#].[sys].[indexes] ind
    INNER JOIN [#Catalog#].[sys].[objects] obj ON ind.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
    INNER JOIN [#Catalog#].[sys].[columns] col ON ind.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
    INNER JOIN [#Catalog#].[sys].[filegroups] flg ON ind.data_space_id = flg.data_space_id
WHERE obj.[is_ms_shipped] = 0 AND ind.[is_primary_key] = 0 AND ind.[is_unique_constraint] = 1
ORDER BY [CatalogName], [SchemaName], [TableName], [ObjectName], [KeyOrdinal]
";

        private const string DefaultUserDefinedDataTypesSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , typ.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'User-Defined Data Type' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , base_typ.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [DataType]
  , typ.[max_length] [MaxLength]
  , typ.[precision] [Precision]
  , typ.[scale] [Scale]
  , isnull(typ.[collation_name], '') COLLATE SQL_Latin1_General_CP1_CI_AS [Collation]
  , CASE WHEN typ.[default_object_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasDefault]
  , typ.[is_user_defined] [IsUserDefined]
  , typ.[is_assembly_type] [IsAssemblyType]
  , typ.[is_nullable] [IsNullable]
FROM [#Catalog#].[sys].[types] typ
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON typ.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[types] base_typ ON typ.[system_type_id] = base_typ.[system_type_id]
        AND base_typ.[is_user_defined] = 0
WHERE typ.[is_user_defined] = 1
ORDER BY [SchemaName], [ObjectName]
";

        private const string DefaultUserTableColumnsSql = @"
SELECT DISTINCT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'User-Table Column' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , col.[column_id] [ColumnOrdinal]
  , typ.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [DataType]
  , col.[max_length] [MaxLength]
  , col.[precision] [Precision]
  , col.[scale] [Scale]
  , IsNull(col.[collation_name], IsNull(db.[collation_name], '')) COLLATE SQL_Latin1_General_CP1_CI_AS [Collation]
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
FROM [#Catalog#].[sys].[columns] col
    INNER JOIN [#Catalog#].[sys].[objects] obj ON col.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[tables] tab ON  col.[object_id] = tab.[object_id]
    INNER JOIN [#Catalog#].[sys].[types] typ ON col.[system_type_id] = typ.[system_type_id] AND col.[user_type_id] = typ.[user_type_id]
    LEFT JOIN [#Catalog#].[sys].[databases] db ON db.[name] = DB_NAME(DB_ID()) AND typ.[name] IN ('char', 'varchar', 'nchar', 'nvarchar')
    LEFT JOIN (
		SELECT
			fk_sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
		  , fk_tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [TableName]
		  , fk_c.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ColumnName]
		  , fk.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
		  , 'Reference Mapping' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
		  ,  fk_col.[constraint_column_id] [KeyOrdinal]
		  , DB_NAME(DB_ID()) COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedCatalogName]
		  , pk.[schema_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedSchemaName]
		  , pk.[table_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedTableName]
		  , pk.[column_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedColumnName]
		  , pk.[index_name] COLLATE SQL_Latin1_General_CP1_CI_AS [ReferencedObjectName]
		FROM [#Catalog#].[sys].[foreign_keys] fk
			INNER JOIN [#Catalog#].[sys].[objects] fk_obj ON fk.[object_id] = fk_obj.[object_id]
			INNER JOIN [#Catalog#].[sys].[schemas] fk_sch ON fk_obj.[schema_id] = fk_sch.[schema_id]
			INNER JOIN [#Catalog#].[sys].[tables] fk_tab ON  fk.[parent_object_id] = fk_tab.[object_id]
			INNER JOIN [#Catalog#].[sys].[foreign_key_columns] fk_col ON fk.[object_id] = fk_col.[constraint_object_id]
			INNER JOIN [#Catalog#].[sys].[columns] fk_c ON fk_col.[parent_object_id] = fk_c.[object_id] AND fk_col.[parent_column_id] = fk_c.[column_id]
			INNER JOIN
			(
				SELECT
					ind.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [index_name]
				  , obj.[object_id]
				  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [schema_name]
				  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [table_name]
				  , col.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [column_name]
				  , col.[column_id] [column_id]
				FROM [#Catalog#].[sys].[indexes] ind
					INNER JOIN [#Catalog#].[sys].[objects] obj ON ind.[object_id] = obj.[object_id]
					INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
					INNER JOIN [#Catalog#].[sys].[tables] tab ON  ind.[object_id] = tab.[object_id]
					INNER JOIN [#Catalog#].[sys].[index_columns] icol ON ind.[object_id] = icol.[object_id] AND ind.[index_id] = icol.[index_id]
					INNER JOIN [#Catalog#].[sys].[columns] col ON icol.[object_id] = col.[object_id] AND icol.[column_id] = col.[column_id]
				WHERE obj.[is_ms_shipped] = 0
			) pk ON fk_col.[referenced_object_id] = pk.[object_id] AND fk_col.[referenced_column_id] = pk.[column_id]
		WHERE fk_obj.[is_ms_shipped] = 0
    ) [fk_maps] ON sch.[name] = [fk_maps].[ReferencedSchemaName] AND tab.[name] = [fk_maps].[ReferencedTableName]
		AND [fk_maps].[ReferencedColumnName] = col.[name]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [TableName], [ColumnOrdinal]
";

        private const string DefaultUserTablesSql = @"
SELECT
    '#Catalog#' COLLATE SQL_Latin1_General_CP1_CI_AS [CatalogName]
  , sch.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [SchemaName]
  , tab.[name] COLLATE SQL_Latin1_General_CP1_CI_AS [ObjectName]
  , 'User-Table' COLLATE SQL_Latin1_General_CP1_CI_AS [Description]
  , tab.[type_desc] COLLATE SQL_Latin1_General_CP1_CI_AS [TypeDescription]
  , CASE WHEN fs_flg.[name] IS NOT NULL THEN fs_flg.[name] ELSE flg.[name] END COLLATE SQL_Latin1_General_CP1_CI_AS [FileStreamFileGroup]
  , CASE WHEN lob_flg.[name] IS NOT NULL THEN lob_flg.[name] ELSE flg.[name] END COLLATE SQL_Latin1_General_CP1_CI_AS [LobFileGroup]
  , CASE WHEN tab.[lob_data_space_id] > 0 THEN Cast(1 AS BIT) ELSE Cast(0 AS BIT) END [HasTextNTextOrImageColumns]
  , tab.[uses_ansi_nulls] [UsesAnsiNulls]
  , tab.[text_in_row_limit] [TextInRowLimit]
FROM [#Catalog#].[sys].[tables] tab
    INNER JOIN [#Catalog#].[sys].[objects] obj ON tab.[object_id] = obj.[object_id]
    INNER JOIN [#Catalog#].[sys].[schemas] sch ON obj.[schema_id] = sch.[schema_id]
    INNER JOIN [#Catalog#].[sys].[filegroups] flg ON flg.[is_default] = 1
    LEFT JOIN [#Catalog#].[sys].[filegroups] fs_flg ON tab.[filestream_data_space_id] = fs_flg.[data_space_id]
    LEFT JOIN [#Catalog#].[sys].[filegroups] lob_flg ON tab.[lob_data_space_id] = lob_flg.[data_space_id]
WHERE obj.[is_ms_shipped] = 0
ORDER BY [CatalogName], [SchemaName], [ObjectName]
";
    }
}
