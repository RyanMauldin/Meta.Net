using System.Collections.Generic;
using System.Text;

namespace Meta.Net.MySql
{
    public class MySqlMetadataScriptFactory : DataMetadataScriptFactory
    {
        public MySqlMetadataScriptFactory()
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
        private const string DefaultOrderByObjectName = "ORDER BY `ObjectName`";

        public override string Catalogs(IList<string> catalogs)
        {
            var builder = new StringBuilder(CatalogsSql);
            builder.Replace(OrderByObjectName, " ");
            builder.Append("AND sch.`SCHEMA_NAME` COLLATE utf8_unicode_ci IN (");
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
    sch.`SCHEMA_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'Catalog' COLLATE utf8_unicode_ci `Description`
  , NOW() `CreateDate`
  , '100' `CompatibilityLevel`
  , sch.`DEFAULT_COLLATION_NAME` COLLATE utf8_unicode_ci `CollationName`
  , '0' `UserAccess`
  , 'MULTI_USER' COLLATE utf8_unicode_ci `UserAccessDescription`
  , 'false' `IsReadOnly`
  , 'false' `IsAutoCloseOn`
  , 'false' `IsAutoShrinkOn`
  , '0' `State`
  , 'ONLINE' COLLATE utf8_unicode_ci `StateDescription`
  , 'false' `IsInStandby`
  , 'true' `IsCleanlyShutdown`
  , 'false' `IsSupplementalLoggingEnabled`
  , '3' `RecoveryModel`
  , 'SIMPLE' COLLATE utf8_unicode_ci `RecoveryModelDescription`
  , '2' `PageVerifyOption`
  , 'CHECKSUM' COLLATE utf8_unicode_ci `PageVerifyOptionDescription`
  , 'true' `IsAutoCreateStatsOn`
  , 'true' `IsAutoUpdateStatsOn`
  , 'false' `IsAutoUpdateStatsAsyncOn`
  , 'false' `IsAnsiNullDefaultOn`
  , 'false' `IsAnsiNullsOn`
  , 'false' `IsAnsiPaddingOn`
  , 'false' `IsAnsiWarningsOn`
  , 'false' `IsArithabortOn`
  , 'false' `IsConcatNullYieldsNullOn`
  , 'false' `IsNumericRoundabortOn`
  , 'false' `IsQuotedIdentifierOn`
  , 'false' `IsRecursiveTriggersOn`
  , 'false' `IsCursorCloseOnCommitOn`
  , 'false' `IsLocalCursorDefault`
  , 'false' `IsFulltextEnabled`
  , 'false' `IsTrustworthyOn`
  , 'false' `IsDbChainingOn`
  , 'false' `IsParameterizationForced`
  , 'false' `IsMasterKeyEncryptedByServer`
  , 'false' `IsDateCorrelationOn`
FROM `INFORMATION_SCHEMA`.`SCHEMATA` sch
WHERE sch.`SCHEMA_NAME` NOT IN ('mysql', 'INFORMATION_SCHEMA', 'phpmyadmin','performance_schema')
ORDER BY `ObjectName`
";

        private const string DefaultSchemasSql = @"
SELECT
    '#Catalog#' COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `ObjectName`
  , 'Schema' COLLATE utf8_unicode_ci `Description`
";

        // TODO: Check Constraints are not supported in MySql, use Triggers for SqlServer to MySql conversion.
        // I believe these are supported in MariaDB though... may want to add support for MariaDB.
        private const string DefaultCheckConstraintsSql = @"
SELECT
    '#Catalog#' COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , '' COLLATE utf8_unicode_ci `TableName`
  , '' COLLATE utf8_unicode_ci `ColumnName`
  , '' COLLATE utf8_unicode_ci `ObjectName`
  , '' COLLATE utf8_unicode_ci `Description`
  , '' COLLATE utf8_unicode_ci `Definition`
  , 'true' `IsTableConstraint`
  , 'false' `IsDisabled`
  , 'false' `IsNotForReplication`
  , 'false' `IsNotTrusted`
  , 'false' `IsSystemNamed`
FROM `INFORMATION_SCHEMA`.`SCHEMATA` sch
WHERE sch.`SCHEMA_NAME` IS NULL
";

        // TODO: Virutal columns are MariaDB specific... may want to add support for MariaDB
        // http://en.wikipedia.org/wiki/Virtual_column
        private const string DefaultComputedColumnsSql = @"
SELECT
    '#Catalog#' COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , '' COLLATE utf8_unicode_ci `TableName`
  , '' COLLATE utf8_unicode_ci `ObjectName`
  , '' COLLATE utf8_unicode_ci `Description`
  , '' COLLATE utf8_unicode_ci `Definition`
  , 'false' `IsPersisted`
  , 'false' `IsNullable`
FROM `INFORMATION_SCHEMA`.`SCHEMATA` sch
WHERE sch.`SCHEMA_NAME` IS NULL
";

        private const string DefaultDefaultConstraintsSql = @"
SELECT
    '#Catalog#' COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , col.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , col.`COLUMN_NAME` COLLATE utf8_unicode_ci `ColumnName`
  , CONCAT('DF_', col.`TABLE_NAME` , '_', col.`COLUMN_NAME`) COLLATE utf8_unicode_ci `ObjectName`
  , 'Default Constraint' COLLATE utf8_unicode_ci `Description`
  , col.`COLUMN_DEFAULT` COLLATE utf8_unicode_ci `Definition`
  , 'false' `IsSystemNamed`
FROM `INFORMATION_SCHEMA`.`COLUMNS` col
WHERE col.`COLUMN_DEFAULT` IS NOT NULL AND col.`TABLE_SCHEMA` = '#Catalog#'
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ObjectName`
";

        private const string DefaultForeignKeysSql = @"
SELECT
    fk_kcu.`CONSTRAINT_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , fk_kcu.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , fk_kcu.`COLUMN_NAME` COLLATE utf8_unicode_ci `ColumnName`
  , fk_tc.`CONSTRAINT_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'Reference Mapping' COLLATE utf8_unicode_ci `Description`
  , fk_kcu.`ORDINAL_POSITION` `KeyOrdinal`
  , 'Foreign Key' COLLATE utf8_unicode_ci `Description`
  , 'false' `IsDisabled`
  , 'false' `IsNotForReplication`
  , 'false' `IsNotTrusted`
  , CASE WHEN rc.`DELETE_RULE` = 'NO ACTION' THEN '0' ELSE
        CASE WHEN rc.`DELETE_RULE` = 'CASCADE' THEN '1' ELSE
            CASE WHEN rc.`DELETE_RULE` = 'SET NULL' THEN '2' ELSE
                CASE WHEN rc.`DELETE_RULE` = 'SET DEFAULT' THEN '3' ELSE
                    CASE WHEN `DELETE_RULE` = 'RESTRICT' THEN '4' ELSE
    '0' END END END END END `DeleteAction`
  , rc.`DELETE_RULE` COLLATE utf8_unicode_ci `DeleteActionDescription`
  , CASE WHEN rc.`UPDATE_RULE` = 'NO ACTION' THEN '0' ELSE
        CASE WHEN rc.`UPDATE_RULE` = 'CASCADE' THEN '1' ELSE
            CASE WHEN rc.`UPDATE_RULE` = 'SET NULL' THEN '2' ELSE
                CASE WHEN rc.`UPDATE_RULE` = 'SET DEFAULT' THEN '3' ELSE
                    CASE WHEN rc.`UPDATE_RULE` = 'RESTRICT' THEN '4' ELSE
    '0' END END END END END `UpdateAction`
  , rc.`UPDATE_RULE` COLLATE utf8_unicode_ci `UpdateActionDescription`
  , 'false' `IsSystemNamed`
  , fk_kcu.`REFERENCED_TABLE_SCHEMA` COLLATE utf8_unicode_ci `ReferencedCatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `ReferencedSchemaName`
  , fk_kcu.`REFERENCED_TABLE_NAME` COLLATE utf8_unicode_ci `ReferencedTableName`
  , fk_kcu.`REFERENCED_COLUMN_NAME` COLLATE utf8_unicode_ci `ReferencedColumnName`
  , pk.`CONSTRAINT_NAME` COLLATE utf8_unicode_ci `ReferencedObjectName`
FROM `INFORMATION_SCHEMA`.`REFERENTIAL_CONSTRAINTS` rc
    INNER JOIN `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` fk_tc
        ON rc.`CONSTRAINT_SCHEMA` = fk_tc.`CONSTRAINT_SCHEMA` AND rc.`TABLE_NAME` = fk_tc.`TABLE_NAME`
            AND rc.`CONSTRAINT_NAME` = fk_tc.`CONSTRAINT_NAME`
    INNER JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` fk_kcu
        ON fk_tc.`CONSTRAINT_SCHEMA` = fk_kcu.`CONSTRAINT_SCHEMA` AND fk_tc.`TABLE_NAME` = fk_kcu.`TABLE_NAME`
            AND fk_tc.`CONSTRAINT_NAME` = fk_kcu.`CONSTRAINT_NAME`
    INNER JOIN 
    (
        SELECT pk_tc.`TABLE_SCHEMA`, pk_tc.`TABLE_NAME`, pk_tc.`CONSTRAINT_NAME`
            , pk_tc.`CONSTRAINT_TYPE`, pk_kcu.`COLUMN_NAME`, pk_kcu.`ORDINAL_POSITION`
        FROM `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` pk_tc
			      INNER JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` pk_kcu
                ON pk_tc.`CONSTRAINT_SCHEMA` = pk_kcu.`CONSTRAINT_SCHEMA` AND pk_tc.`TABLE_NAME` = pk_kcu.`TABLE_NAME`
                    AND pk_tc.`CONSTRAINT_NAME` = pk_kcu.`CONSTRAINT_NAME`
        WHERE pk_tc.`CONSTRAINT_TYPE` = 'PRIMARY KEY'
            AND pk_tc.`CONSTRAINT_SCHEMA` = '#Catalog#'
    ) pk ON fk_kcu.`REFERENCED_TABLE_SCHEMA` = pk.`TABLE_SCHEMA` AND fk_kcu.`REFERENCED_TABLE_NAME` = pk.`TABLE_NAME`
            AND fk_kcu.`REFERENCED_COLUMN_NAME` = pk.`COLUMN_NAME`
WHERE rc.`CONSTRAINT_SCHEMA` = '#Catalog#'
    AND fk_tc.`CONSTRAINT_TYPE` = 'FOREIGN KEY'
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ObjectName`, `KeyOrdinal`
";

        private const string DefaultIdentityColumnsSql = @"
SELECT
    col.`TABLE_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , col.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , col.`COLUMN_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'Identity Column' COLLATE utf8_unicode_ci `Description`
  , '1' `SeedValue`
  , '1' `IncrementValue`
  , 'false' `IsNotForReplication`
FROM `INFORMATION_SCHEMA`.`COLUMNS` col
WHERE col.`EXTRA` = 'auto_increment' AND col.`TABLE_SCHEMA` = '#Catalog#'
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ObjectName`
";
        private const string DefaultIndexesSql = @"
SELECT
    i_stat.`TABLE_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , i_stat.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , i_stat.`COLUMN_NAME` COLLATE utf8_unicode_ci `ColumnName`
  , i_stat.`INDEX_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'Index' COLLATE utf8_unicode_ci `Description`
  , 'PRIMARY' COLLATE utf8_unicode_ci `FileGroup`
  , i_stat.`SEQ_IN_INDEX` `KeyOrdinal`
  , 1 `PartitionOrdinal`
  , 'false' `IsClustered`
  , 'false' `IsDescendingKey`
  , 'false' `IgnoreDupKey`
  , 'false' `IsIncludedColumn`
  , 'false' `IsUnique`
  , '0' `FillFactor`
  , 'false' `IsPadded`
  , 'false' `IsDisabled`
  , 'false' `AllowRowLocks`
  , 'false' `AllowPageLocks`
  , i_stat.`INDEX_TYPE` COLLATE utf8_unicode_ci `IndexType`
FROM `INFORMATION_SCHEMA`.`STATISTICS` i_stat
WHERE i_stat.`TABLE_SCHEMA` = '#Catalog#' AND i_stat.`NON_UNIQUE` = 1
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ObjectName`, `KeyOrdinal`
";

        private const string DefaultModulesSql = @"
SELECT
    mods.`db` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , mods.`name` COLLATE utf8_unicode_ci `ObjectName`
  , 'Module' COLLATE utf8_unicode_ci `Description`
  , mods.`type` COLLATE utf8_unicode_ci `TypeDescription`
  , CAST(CASE WHEN mods.`type` = 'FUNCTION' OR mods.`type` = 'PROCEDURE' THEN
        CONCAT('CREATE DEFINER = '
            , CASE WHEN mods.`definer` IS NULL OR CHAR_LENGTH(mods.`definer`) < 2 THEN
                  CONCAT('\'', SUBSTRING_INDEX(CURRENT_USER, '@', 1), '\'@\'', SUBSTRING_INDEX(CURRENT_USER, '@', -1), '\' ')
              ELSE
                  CONCAT('\'', SUBSTRING_INDEX(mods.`definer`, '@', 1), '\'@\'', SUBSTRING_INDEX(mods.`definer`, '@', -1), '\' ')
              END
            , mods.`type`, ' ', mods.`name`, ' (', mods.`param_list`, ')', CHAR(10)
            , CASE WHEN `type` = 'FUNCTION' THEN
                  CONCAT('RETURNS ', mods.`returns`, ' ', SUBSTRING_INDEX(mods.`db_collation`, '_', 1), CHAR(10))
              ELSE '' END
            , CASE WHEN mods.`comment` IS NOT NULL AND CHAR_LENGTH(mods.`comment`) > 0 THEN
                  CONCAT(' COMMENT \'', mods.`comment`, '\'', CHAR(10))
              ELSE '' END
            , 'LANGUAGE ', mods.`language`, CHAR(10)
            , CASE WHEN mods.`is_deterministic` = 'YES' THEN
                  CONCAT('DETERMINISTIC', CHAR(10))
              ELSE '' END
            , CASE WHEN mods.`sql_data_access` = 'CONTAINS_SQL' THEN
                  CONCAT('CONTAINS SQL', CHAR(10))
              ELSE '' END
            , 'SQL SECURITY ', mods.`security_type`, CHAR(10)
            , mods.`body`)
    ELSE
        CASE WHEN `type` = 'TRIGGER' THEN
            CONCAT('CREATE DEFINER = '
            , CASE WHEN mods.`definer` IS NULL OR CHAR_LENGTH(mods.`definer`) < 2 THEN
                  CONCAT('\'', SUBSTRING_INDEX(CURRENT_USER, '@', 1), '\'@\'', SUBSTRING_INDEX(CURRENT_USER, '@', -1), '\' ')
              ELSE
                  CONCAT('\'', SUBSTRING_INDEX(mods.`definer`, '@', 1), '\'@\'', SUBSTRING_INDEX(mods.`definer`, '@', -1), '\' ')
              END
            , mods.`type`, ' ', mods.`name`, CHAR(10)
            , mods.`param_list`, ' ', mods.`specific_name`, ' ON ', mods.`TriggerForObjectName`, CHAR(10)
            , 'FOR EACH ROW', CHAR(10)
            , mods.`body`)
        ELSE 
            CASE WHEN `type` = 'VIEW' THEN
                CONCAT('CREATE', CHAR(10)
                    , CASE WHEN mods.`is_deterministic` = 'YES' THEN CONCAT('OR REPLACE', CHAR(10)) ELSE '' END
                    , 'ALGORITHM = UNDEFINED', CHAR(10)
                    , 'DEFINER = '
                    , CASE WHEN mods.`definer` IS NULL OR CHAR_LENGTH(mods.`definer`) < 2 THEN
                          CONCAT('\'', SUBSTRING_INDEX(CURRENT_USER, '@', 1), '\'@\'', SUBSTRING_INDEX(CURRENT_USER, '@', -1), '\' ')
                      ELSE
                          CONCAT('\'', SUBSTRING_INDEX(mods.`definer`, '@', 1), '\'@\'', SUBSTRING_INDEX(mods.`definer`, '@', -1), '\' ')
                      END
                    , CHAR(10), 'SQL SECURITY ', mods.`security_type`, CHAR(10)
                    , mods.`type`, ' ', mods.`name`, ' (', mods.`param_list`, ')', CHAR(10)
                    , 'AS ', mods.`body`
                    , CASE WHEN mods.`sql_data_access` = 'NONE' THEN
                        ''
                      ELSE
                        CONCAT(CHAR(10), 'WITH ', mods.`sql_data_access`, ' CHECK OPTION') 
                      END)
            ELSE '' END END END AS CHAR(65535)) COLLATE utf8_unicode_ci `Definition`
  , 'false' `UsesAnsiNulls`
  , 'false' `UsesQuotedIdentifier`
  , 'false' `IsNotForReplication`
  , 'false' `IsDisabled`
  , mods.`TriggerForSchema` COLLATE utf8_unicode_ci `TriggerForSchema`
  , mods.`TriggerForObjectName` COLLATE utf8_unicode_ci `TriggerForObjectName`
FROM
(
    (
        SELECT proc.`db`, proc.`name`, proc.`type`, proc.`specific_name`, proc.`definer`, proc.`param_list`
            , proc.`db_collation`, proc.`comment`, proc.`language`, proc.`is_deterministic`, proc.`sql_data_access`
            , proc.`security_type`, proc.`returns`, proc.`body`, '' `TriggerForSchema`, '' `TriggerForObjectName`
        FROM `mysql`.`proc` proc
        WHERE proc.`db` = '#Catalog#'
    )
    UNION ALL
    (
        SELECT trig.`TRIGGER_SCHEMA` `db`, trig.`TRIGGER_NAME` `name`, 'TRIGGER' `type`, trig.`EVENT_MANIPULATION` `specific_name`
            , trig.`DEFINER` `definer`, trig.`ACTION_TIMING` `param_list`, trig.`DATABASE_COLLATION` `db_collation`
            , trig.`SQL_MODE` `comment`, 'SQL' `language`, 'NO' `is_deterministic`
            , trig.`ACTION_REFERENCE_OLD_ROW` `sql_data_access`, 'DEFINER' `security_type`, trig.`ACTION_REFERENCE_NEW_ROW` `returns`
            , trig.`ACTION_STATEMENT` `body`, trig.`EVENT_OBJECT_SCHEMA` `TriggerForSchema`, trig.`EVENT_OBJECT_TABLE` `TriggerForObjectName`
        FROM `INFORMATION_SCHEMA`.`TRIGGERS` trig
        WHERE trig.`TRIGGER_SCHEMA` = '#Catalog#'
    )
    UNION ALL
    (
        SELECT views.`TABLE_SCHEMA` `db`, views.`TABLE_NAME` `name`, 'VIEW' `type`, views.`IS_UPDATABLE` `specific_name`
            , views.`DEFINER` `definer`,  view_cols.`param_list`, '' `db_collation`
            , '' `comment`, 'SQL' `language`, views.`IS_UPDATABLE` `is_deterministic`
            , views.`CHECK_OPTION` `sql_data_access`, views.`SECURITY_TYPE` `security_type`, '' `returns`
            , views.`VIEW_DEFINITION` `body`, '' `TriggerForSchema`, '' `TriggerForObjectName`
        FROM `INFORMATION_SCHEMA`.`VIEWS` views
            INNER JOIN `INFORMATION_SCHEMA`.`TABLES` views_tab ON views.`TABLE_SCHEMA` = views_tab.`TABLE_SCHEMA`
                AND views.`TABLE_NAME` = views_tab.`TABLE_NAME`
            INNER JOIN
            (
                SELECT
                    col.`TABLE_SCHEMA` `TABLE_SCHEMA`
                  , col.`TABLE_NAME` `TABLE_NAME`
                  , GROUP_CONCAT(
                        CONCAT('`', col.`COLUMN_NAME`, '`')
                        ORDER BY col.`ORDINAL_POSITION` ASC
                        SEPARATOR ', ') `param_list`
                FROM `INFORMATION_SCHEMA`.`TABLES` tab
                    INNER JOIN `INFORMATION_SCHEMA`.`COLUMNS` col ON tab.`TABLE_SCHEMA` = col.`TABLE_SCHEMA`
                        AND tab.`TABLE_NAME` = col.`TABLE_NAME`
                WHERE tab.`TABLE_TYPE` = 'VIEW' AND tab.`TABLE_SCHEMA` = '#Catalog#'
                GROUP BY col.`TABLE_SCHEMA`, col.`TABLE_NAME`
                ORDER BY col.`TABLE_SCHEMA`, col.`TABLE_NAME`, col.`ORDINAL_POSITION`
            ) view_cols ON views_tab.`TABLE_SCHEMA` = view_cols.`TABLE_SCHEMA`
                AND views_tab.`TABLE_NAME` = view_cols.`TABLE_NAME`
        WHERE views.`TABLE_SCHEMA` = '#Catalog#'
    )
) mods 
WHERE mods.`db` = '#Catalog#'
ORDER BY `CatalogName`, `SchemaName`, `ObjectName`
";

        private const string DefaultPrimaryKeysSql = @"
SELECT
    pk_kcu.`TABLE_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , pk_kcu.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , pk_kcu.`COLUMN_NAME` COLLATE utf8_unicode_ci `ColumnName`
  , pk_kcu.`CONSTRAINT_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'Primary Key' COLLATE utf8_unicode_ci `Description`
  , 'PRIMARY' COLLATE utf8_unicode_ci `FileGroup`
  , pk_kcu.`ORDINAL_POSITION` `KeyOrdinal`
  , 1 `PartitionOrdinal`
  , 'false' `IsDescendingKey`
  , 'false' `IgnoreDupKey`
  , 'true' `IsClustered`
  , '0' `FillFactor`
  , 'false' `IsPadded`
  , 'false' `IsDisabled`
  , 'false' `AllowRowLocks`
  , 'false' `AllowPageLocks`
  , i_stat.`INDEX_TYPE` COLLATE utf8_unicode_ci `IndexType`
FROM `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` pk_tc
    INNER JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` pk_kcu
        ON pk_tc.`CONSTRAINT_SCHEMA` = pk_kcu.`CONSTRAINT_SCHEMA` AND pk_tc.`TABLE_NAME` = pk_kcu.`TABLE_NAME`
            AND pk_tc.`CONSTRAINT_NAME` = pk_kcu.`CONSTRAINT_NAME`
    INNER JOIN `INFORMATION_SCHEMA`.`STATISTICS` i_stat
        ON pk_kcu.`CONSTRAINT_SCHEMA` = i_stat.`TABLE_SCHEMA`
            AND pk_kcu.`TABLE_NAME` = i_stat.`TABLE_NAME`
            AND pk_kcu.`CONSTRAINT_NAME` = i_stat.`INDEX_NAME`
            AND pk_kcu.`COLUMN_NAME` = i_stat.`COLUMN_NAME`
WHERE pk_tc.`CONSTRAINT_TYPE` = 'PRIMARY KEY' AND pk_tc.`CONSTRAINT_SCHEMA` = '#Catalog#'
    AND i_stat.`NON_UNIQUE` = 0
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ObjectName`, `KeyOrdinal`
";

        private const string DefaultUniqueConstraintsSql = @"
SELECT
    uc_kcu.`TABLE_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , uc_kcu.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , uc_kcu.`COLUMN_NAME` COLLATE utf8_unicode_ci `ColumnName`
  , uc_kcu.`CONSTRAINT_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'Unique Constraint' COLLATE utf8_unicode_ci `Description`
  , 'PRIMARY' COLLATE utf8_unicode_ci `FileGroup`
  , uc_kcu.`ORDINAL_POSITION` `KeyOrdinal`
  , 1 `PartitionOrdinal`
  , CASE WHEN pk_cols.`PRIMARY_KEY_ORDINAL_SUM` IS NOT NULL THEN
        'false'
    ELSE
        CASE WHEN SUBSTRING_INDEX(uc_cols.`UNIQUE_CONSTRAINT_NAME_ORDER`, ', ', 1) = uc_kcu.`CONSTRAINT_NAME` THEN
            'true'
        ELSE
            'false'
        END
    END `IsClustered`
  , 'false' `IsDescendingKey`
  , 'false' `IgnoreDupKey`
  , '0' `FillFactor`
  , 'false' `IsPadded`
  , 'false' `IsDisabled`
  , 'false' `AllowRowLocks`
  , 'false' `AllowPageLocks`
  , i_stat.`INDEX_TYPE` COLLATE utf8_unicode_ci `IndexType`
FROM `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` uc_tc
    INNER JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` uc_kcu
        ON uc_tc.`CONSTRAINT_SCHEMA` = uc_kcu.`CONSTRAINT_SCHEMA`
            AND uc_tc.`TABLE_NAME` = uc_kcu.`TABLE_NAME`
            AND uc_tc.`CONSTRAINT_NAME` = uc_kcu.`CONSTRAINT_NAME`
    INNER JOIN `INFORMATION_SCHEMA`.`STATISTICS` i_stat
        ON uc_kcu.`CONSTRAINT_SCHEMA` = i_stat.`TABLE_SCHEMA`
            AND uc_kcu.`TABLE_NAME` = i_stat.`TABLE_NAME`
            AND uc_kcu.`CONSTRAINT_NAME` = i_stat.`INDEX_NAME`
            AND uc_kcu.`COLUMN_NAME` = i_stat.`COLUMN_NAME`
    LEFT JOIN
    (
        SELECT
            pk_kcu.`TABLE_SCHEMA` `TABLE_SCHEMA`
          , pk_kcu.`TABLE_NAME` `TABLE_NAME`
          , SUM(pk_kcu.`ORDINAL_POSITION`) `PRIMARY_KEY_ORDINAL_SUM`
        FROM `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` pk_tc
            LEFT JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` pk_kcu
                ON pk_tc.`CONSTRAINT_SCHEMA` = pk_kcu.`CONSTRAINT_SCHEMA`
                    AND pk_tc.`TABLE_NAME` = pk_kcu.`TABLE_NAME`
                    AND pk_tc.`CONSTRAINT_NAME` = pk_kcu.`CONSTRAINT_NAME`
        WHERE pk_tc.`CONSTRAINT_TYPE` = 'PRIMARY KEY' AND pk_tc.`TABLE_SCHEMA` = '#Catalog#'
        GROUP BY pk_kcu.`TABLE_SCHEMA`, pk_kcu.`TABLE_NAME`
    ) pk_cols ON uc_kcu.`TABLE_SCHEMA` = pk_cols.`TABLE_SCHEMA`
        AND uc_kcu.`TABLE_NAME` = pk_cols.`TABLE_NAME`
     LEFT JOIN
    (
        SELECT
            uc_kcu2.`TABLE_SCHEMA` `TABLE_SCHEMA`
          , uc_kcu2.`TABLE_NAME` `TABLE_NAME`
          , SUM(uc_kcu2.`ORDINAL_POSITION`) `UNIQUE_CONSTRAINT_ORDINAL_SUM`
          , GROUP_CONCAT(uc_kcu2.`CONSTRAINT_NAME`
                SEPARATOR ', ') `UNIQUE_CONSTRAINT_NAME_ORDER`
        FROM `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` uc_tc2
            LEFT JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` uc_kcu2
                ON uc_tc2.`CONSTRAINT_SCHEMA` = uc_kcu2.`CONSTRAINT_SCHEMA`
                    AND uc_tc2.`TABLE_NAME` = uc_kcu2.`TABLE_NAME`
                    AND uc_tc2.`CONSTRAINT_NAME` = uc_kcu2.`CONSTRAINT_NAME`
        WHERE uc_tc2.`CONSTRAINT_TYPE` = 'UNIQUE' AND uc_tc2.`TABLE_SCHEMA` = '#Catalog#'
        GROUP BY uc_kcu2.`TABLE_SCHEMA`, uc_kcu2.`TABLE_NAME`
    ) uc_cols ON uc_kcu.`TABLE_SCHEMA` = uc_cols.`TABLE_SCHEMA`
        AND uc_kcu.`TABLE_NAME` = uc_cols.`TABLE_NAME`
WHERE uc_tc.`CONSTRAINT_TYPE` = 'UNIQUE' AND uc_tc.`CONSTRAINT_SCHEMA` = '#Catalog#'
    AND i_stat.`NON_UNIQUE` = 0
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ObjectName`, `KeyOrdinal`
";

        // TODO: Implement for MySql
        private const string DefaultUserDefinedDataTypesSql = @"
SELECT
    '' COLLATE utf8_unicode_ci `CatalogName`
  , '' COLLATE utf8_unicode_ci `SchemaName`
  , '' COLLATE utf8_unicode_ci `ObjectName`
  , '' COLLATE utf8_unicode_ci `Description`
  , '' COLLATE utf8_unicode_ci `DataType`
  , 0 `MaxLength`
  , 0 `Precision`
  , 0 `Scale`
  , '' COLLATE utf8_unicode_ci `Collation`
  , 'false' `HasDefault`
  , 'false' `IsUserDefined`
  , 'false' `IsAssemblyType`
  , 'false' `IsNullable`
FROM `INFORMATION_SCHEMA`.`SCHEMATA` sch
WHERE sch.`SCHEMA_NAME` IS NULL
";

        private const string DefaultUserTableColumnsSql = @"
SELECT DISTINCT
    col.`TABLE_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , col.`TABLE_NAME` COLLATE utf8_unicode_ci `TableName`
  , col.`COLUMN_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'User-Table Column' COLLATE utf8_unicode_ci `Description`
  , col.`ORDINAL_POSITION` `ColumnOrdinal`
  , CASE WHEN col.`DATA_TYPE` IN ('set', 'enum') THEN col.`COLUMN_TYPE` ELSE col.`DATA_TYPE` END COLLATE utf8_unicode_ci `DataType`
  , CASE WHEN col.`CHARACTER_MAXIMUM_LENGTH` IS NULL THEN 0 ELSE col.`CHARACTER_MAXIMUM_LENGTH` END  `MaxLength`
  , CASE WHEN col.`NUMERIC_PRECISION` IS NULL THEN 0 ELSE col.`NUMERIC_PRECISION` END `Precision`
  , CASE WHEN col.`NUMERIC_SCALE` IS NULL THEN 0 ELSE col.`NUMERIC_SCALE` END `Scale`
  , CASE WHEN col.`COLLATION_NAME` IS NULL THEN '' ELSE col.`COLLATION_NAME` END COLLATE utf8_unicode_ci `Collation`
  , CASE WHEN col.`COLUMN_DEFAULT` IS NULL THEN 'false' ELSE 'true' END `HasDefault`
  , CASE WHEN `fk_maps`.`ObjectName` IS NULL THEN 'false' ELSE 'true' END `HasForeignKey`
  , 'false' `HasXmlCollection`
  , 'false' `IsUserDefined`
  , 'false' `IsAssemblyType`
  , CASE WHEN col.`IS_NULLABLE` = 'YES' THEN 'true' ELSE 'false' END `IsNullable`
  , CASE WHEN col.`DATA_TYPE` IN ('char', 'binary') THEN 'true' ELSE 'false' END `IsAnsiPadded`
  , 'false' `IsRowGuidColumn`
  , CASE WHEN col.`EXTRA` = 'auto_increment' THEN 'true' ELSE 'false' END  `IsIdentity`
  , 'false' `IsComputed`
  , 'false' `IsFileStream`
  , 'false' `IsXmlDocument`
FROM `INFORMATION_SCHEMA`.`COLUMNS` col
    LEFT JOIN
    (
        SELECT
            fk_kcu.`CONSTRAINT_SCHEMA` `CatalogName`
          , 'dbo' `SchemaName`
          , fk_kcu.`TABLE_NAME` `TableName`
          , fk_kcu.`COLUMN_NAME` `ColumnName`
          , fk_tc.`CONSTRAINT_NAME` `ObjectName`
          , 'Reference Mapping' `Description`
          , fk_kcu.`ORDINAL_POSITION` `KeyOrdinal`
          , fk_kcu.`REFERENCED_TABLE_SCHEMA` `ReferencedCatalogName`
          , 'dbo' `ReferencedSchemaName`
          , fk_kcu.`REFERENCED_TABLE_NAME` `ReferencedTableName`
          , fk_kcu.`REFERENCED_COLUMN_NAME` `ReferencedColumnName`
          , pk.`CONSTRAINT_NAME` `ReferencedObjectName`
        FROM `INFORMATION_SCHEMA`.`REFERENTIAL_CONSTRAINTS` rc
            INNER JOIN `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` fk_tc
                ON rc.`CONSTRAINT_SCHEMA` = fk_tc.`CONSTRAINT_SCHEMA` AND rc.`TABLE_NAME` = fk_tc.`TABLE_NAME`
                    AND rc.`CONSTRAINT_NAME` = fk_tc.`CONSTRAINT_NAME`
            INNER JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` fk_kcu
                ON fk_tc.`CONSTRAINT_SCHEMA` = fk_kcu.`CONSTRAINT_SCHEMA` AND fk_tc.`TABLE_NAME` = fk_kcu.`TABLE_NAME`
                    AND fk_tc.`CONSTRAINT_NAME` = fk_kcu.`CONSTRAINT_NAME`
            INNER JOIN 
            (
                SELECT pk_tc.`TABLE_SCHEMA`, pk_tc.`TABLE_NAME`, pk_tc.`CONSTRAINT_NAME`
                    , pk_tc.`CONSTRAINT_TYPE`, pk_kcu.`COLUMN_NAME`, pk_kcu.`ORDINAL_POSITION`
                FROM `INFORMATION_SCHEMA`.`TABLE_CONSTRAINTS` pk_tc
                          INNER JOIN `INFORMATION_SCHEMA`.`KEY_COLUMN_USAGE` pk_kcu
                        ON pk_tc.`CONSTRAINT_SCHEMA` = pk_kcu.`CONSTRAINT_SCHEMA` AND pk_tc.`TABLE_NAME` = pk_kcu.`TABLE_NAME`
                            AND pk_tc.`CONSTRAINT_NAME` = pk_kcu.`CONSTRAINT_NAME`
                WHERE pk_tc.`CONSTRAINT_TYPE` = 'PRIMARY KEY'
                    AND pk_tc.`CONSTRAINT_SCHEMA` = '#Catalog#'
            ) pk ON fk_kcu.`REFERENCED_TABLE_SCHEMA` = pk.`TABLE_SCHEMA` AND fk_kcu.`REFERENCED_TABLE_NAME` = pk.`TABLE_NAME`
                    AND fk_kcu.`REFERENCED_COLUMN_NAME` = pk.`COLUMN_NAME`
        WHERE rc.`CONSTRAINT_SCHEMA` = '#Catalog#'
            AND fk_tc.`CONSTRAINT_TYPE` = 'FOREIGN KEY'
    ) `fk_maps` ON `col`.`TABLE_SCHEMA` = '#Catalog#' AND `col`.`TABLE_NAME` = `fk_maps`.`ReferencedTableName`
            AND `col`.`COLUMN_NAME` = `fk_maps`.`ReferencedColumnName`
WHERE col.`TABLE_SCHEMA` = '#Catalog#'
ORDER BY `CatalogName`, `SchemaName`, `TableName`, `ColumnOrdinal`
";

        private const string DefaultUserTablesSql = @"
SELECT
    tab.`TABLE_SCHEMA` COLLATE utf8_unicode_ci `CatalogName`
  , 'dbo' COLLATE utf8_unicode_ci `SchemaName`
  , tab.`TABLE_NAME` COLLATE utf8_unicode_ci `ObjectName`
  , 'User-Table' COLLATE utf8_unicode_ci `Description`
  , 'USER_TABLE' COLLATE utf8_unicode_ci `TypeDescription`
  , 'PRIMARY' COLLATE utf8_unicode_ci `FileStreamFileGroup`
  , 'PRIMARY' COLLATE utf8_unicode_ci `LobFileGroup`
  , 'false' `HasTextNTextOrImageColumns`
  , 'false' `UsesAnsiNulls`
  , '0' `TextInRowLimit`
FROM `INFORMATION_SCHEMA`.`TABLES` tab
WHERE tab.`TABLE_TYPE` = 'BASE TABLE' AND tab.`TABLE_SCHEMA` = '#Catalog#'
ORDER BY `CatalogName`, `SchemaName`, `ObjectName`
";
    }
}
