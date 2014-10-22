using System;
using System.Linq;
using System.Text;
using Meta.Net.MySql.MySqlModuleParser;
using Meta.Net.Objects;
using Meta.Net.Sync;
using Meta.Net.Sync.Converters;
using Meta.Net.Sync.Types;

namespace Meta.Net.MySql
{
    public class MySqlScriptFactory : DataScriptFactory
    {
		public override string AddUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(userTableColumn.UserTable.Namespace));
            builder.Append(" ADD ");
            builder.Append(CreateUserTableColumn(sourceDataContext, targetDataContext, userTableColumn, true).Trim());
            return builder.ToString();
        }

        public override string AlterAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(aggregateFunction.Definition);
        }

        public override string AlterCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            return null;
        }

        public override string AlterForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            if (!(DataProperties.ReplicateItemsMarkedNotForReplication || !foreignKey.IsNotForReplication))
                return string.Empty;

            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(foreignKey.UserTable));
            builder.Append(foreignKey.IsDisabled ? " NOCHECK CONSTRAINT `" : " CHECK CONSTRAINT `");
            builder.Append(foreignKey.ObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string AlterInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(inlineTableValuedFunction.Definition);
        }

        public override string AlterScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(scalarFunction.Definition);
        }

        public override string AlterStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(storedProcedure.Definition);
        }

        public override string AlterTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(tableValuedFunction.Definition);
        }

        public override string AlterTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            if (!(DataProperties.ReplicateItemsMarkedNotForReplication || !trigger.IsNotForReplication))
                return string.Empty;

            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(trigger.Definition);
        }

        public override string AlterUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(userTableColumn.UserTable));
            builder.Append(" ALTER COLUMN ");
            builder.Append(CreateUserTableColumn(sourceDataContext, targetDataContext, userTableColumn, true).Trim());
            return builder.ToString();
        }

        public override string AlterView(DataContext sourceDataContext, DataContext targetDataContext, View view)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : MySqlModuleManager.GetAlterDefinition(view.Definition);
        }

        public override string CreateAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : aggregateFunction.Definition;
        }

        public override string CreateCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog)
        {
            var builder = new StringBuilder();
            builder.Append("CREATE DATABASE ");
            builder.Append(CreateIdentifier(catalog.Namespace));
            return builder.ToString();
        }

        public override string CreateCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            //MySql does not support check constraints...
            return null;
        }

        public override string CreateDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(defaultConstraint.UserTable));
            builder.Append(" ADD CONSTRAINT `");
            builder.Append(defaultConstraint.ObjectName);
            builder.Append("`  DEFAULT ");
            builder.Append(defaultConstraint.Definition);
            builder.Append(" FOR `");
            builder.Append(defaultConstraint.ColumnName);
            builder.Append("`");
            return builder.ToString();
        }

        // ALTER TABLE [SiteEasy].[Test].[1] WITH CHECK
        // ADD CONSTRAINT [FK_1_id] FOREIGN KEY ([id], [id2])
        // REFERENCES [Test].[Test2] ([id], [id2]) ON DELETE CASCADE ON UPDATE NO ACTION
        public override string CreateForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            if (!(DataProperties.ReplicateItemsMarkedNotForReplication || !foreignKey.IsNotForReplication))
                return string.Empty;

            var builder = new StringBuilder();
            var builder2 = new StringBuilder();

            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(foreignKey.UserTable));

            var first = true;
            foreach (var foreignKeyColumn in foreignKey.ForeignKeyColumns)
            {
                if (first)
                {
                    builder.Append("ADD CONSTRAINT `");
                    builder.Append(foreignKey.ObjectName);
                    builder.Append("`  FOREIGN KEY (`");
                    builder.Append(foreignKeyColumn.ObjectName);
                    builder.Append("`");

                    builder2.Append("REFERENCES `");
                    builder2.Append(foreignKey.ReferencedUserTable.ObjectName);
                    builder2.Append("`  (`");
                    builder2.Append(foreignKeyColumn.ReferencedColumn.ObjectName);
                    builder2.Append("`");

                    first = false;
                    continue;
                }

                builder.Append(", `");
                builder.Append(foreignKeyColumn.ObjectName);
                builder.Append("`");

                builder2.Append(", `");
                builder2.Append(foreignKeyColumn.ReferencedColumn.ObjectName);
                builder2.Append("`");
            }

            builder.Append(") ");
            builder.Append(builder2);
            builder.Append(")");

            if (foreignKey.DeleteAction != 0)
            {
                builder.Append(" ON DELETE ");
                builder.Append(foreignKey.DeleteActionDescription);
            }

            if (foreignKey.UpdateAction != 0)
            {
                builder.Append(" ON UPDATE ");
                builder.Append(foreignKey.UpdateActionDescription);
            }

            return builder.ToString();
        }

        public override string CreateIdentifier(string obj)
        {
            var psuedoIdentifier = string.Format("`{0}`", string.Join("`.`", obj.Split(".".ToCharArray(), StringSplitOptions.None)));
            return psuedoIdentifier.StartsWith("`dbo`.") && psuedoIdentifier.Length > 6
                ? psuedoIdentifier.Substring(6)
                : psuedoIdentifier;
        }

        // CREATE UNIQUE NONCLUSTERED INDEX [IX_TestIndexUniqueIncludes]
        // ON [Test].[TestIndex2]([ID3]) INCLUDE ([ID2], [ID4])
        // WITH (ONLINE = ON, PAD_INDEX = ON, FILLFACTOR = 75, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY];
        public override string CreateIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index)
        {
            var builder = new StringBuilder();
            var builder2 = new StringBuilder();

            builder.Append("CREATE ");
            builder.Append(index.IsUnique ? "UNIQUE " : string.Empty);
            builder.Append(index.IsClustered ? "CLUSTERED " : string.Empty);
            builder.Append(" INDEX `");
            builder.Append(index.ObjectName);
            builder.Append("`  ON ");
            builder.Append(CreateIdentifier(index.UserTable));
            builder.Append(" (");

            var options = GetIndexOptions(
                sourceDataContext,
                targetDataContext,
                DataProperties.SqlServerOnlineOption,
                index.AllowRowLocks,
                index.AllowPageLocks,
                index.FillFactor,
                index.IsPadded,
                index.IgnoreDupKey,
                DataProperties.SqlServerIgnoreFileGroups,
                index.FileGroup);

            var first = true;
            var firstInclude = true;
            foreach (var indexColumn in index.IndexColumns)
            {

                // if key ordinal is >= 1025 it is an included column
                if (indexColumn.KeyOrdinal < 1025)
                {
                    // indexed column
                    if (first)
                    {
                        builder.Append("`");
                        builder.Append(indexColumn.ObjectName);
                        builder.Append(indexColumn.IsDescendingKey ? "`  DESC" : "`  ASC");

                        first = false;
                        continue;
                    }

                    builder.Append(", `");
                    builder.Append(indexColumn.ObjectName);
                    builder.Append(indexColumn.IsDescendingKey ? "`  DESC" : "`  ASC");
                }
                else
                {
                    // included column
                    if (firstInclude)
                    {
                        builder2.Append(") INCLUDE (`");
                        builder2.Append(indexColumn.ObjectName);
                        builder2.Append(indexColumn.IsDescendingKey ? "`  DESC" : "`  ASC");

                        firstInclude = false;
                        continue;
                    }

                    builder2.Append(", `");
                    builder2.Append(indexColumn.ObjectName);
                    builder2.Append(indexColumn.IsDescendingKey ? "`  DESC" : "`  ASC");
                }
            }

            if (!firstInclude)
                builder.Append(builder2);

            builder.Append(")");
            if (options.Length > 0)
                builder.Append(" ");
            builder.Append(options);
            return builder.ToString();
        }

        public override string CreateInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : inlineTableValuedFunction.Definition;
        }

        public override string CreateObjectValue(DataContext sourceDataContext, DataContext targetDataContext, CommonDataType dataType, object value)
        {
            if (value == null || value == DBNull.Value)
                return "NULL";

            switch (dataType)
            {
                case CommonDataType.Decimal:
                case CommonDataType.Double:
                case CommonDataType.Float:
                case CommonDataType.Integer8:
                case CommonDataType.Integer16:
                case CommonDataType.Integer24:
                case CommonDataType.Integer32:
                case CommonDataType.Integer64:
                case CommonDataType.Numeric:
                case CommonDataType.Real:
                    return value.ToString();
                default:
                    return string.Format("'{0}'", EscapeString(sourceDataContext, targetDataContext, value));
            }
        }

        // ALTER TABLE [dbo].[AdminSubPages] ADD CONSTRAINT [PK_AdminSubPages] PRIMARY KEY ([id] ASC)
        // WITH (ONLINE = ON, PAD_INDEX = ON, FILLFACTOR = 75, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY];
        public override string CreatePrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey)
        {
            return null; // TODO: WTF?
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(primaryKey.UserTable));
            builder.Append(" ADD CONSTRAINT `");
            builder.Append(primaryKey.ObjectName);
            builder.Append("`  PRIMARY KEY ");
            builder.Append(primaryKey.IsClustered ? "(" : "NONCLUSTERED (");


            var options = GetIndexOptions(
                sourceDataContext,
                targetDataContext,
                DataProperties.SqlServerOnlineOption,
                primaryKey.AllowRowLocks,
                primaryKey.AllowPageLocks,
                primaryKey.FillFactor,
                primaryKey.IsPadded,
                primaryKey.IgnoreDupKey,
                DataProperties.SqlServerIgnoreFileGroups,
                primaryKey.FileGroup);

            var first = true;
            foreach (var primaryKeyColumn in primaryKey.PrimaryKeyColumns)
            {
                if (first)
                {
                    builder.Append("`");
                    builder.Append(primaryKeyColumn.ObjectName);
                    builder.Append(primaryKeyColumn.IsDescendingKey ? "`  DESC" : "`  ASC");

                    first = false;
                    continue;
                }

                builder.Append(", `");
                builder.Append(primaryKeyColumn.ObjectName);
                builder.Append(primaryKeyColumn.IsDescendingKey ? "`  DESC" : "`  ASC");
            }

            builder.Append(")");
            if (options.Length > 0)
                builder.Append(" ");
            builder.Append(options);
            return builder.ToString();
        }

        public override string CreateScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : scalarFunction.Definition;
        }

        public override string CreateSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema)
        {
            return null;
        }

        public override string CreateStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : storedProcedure.Definition;
        }

        public override string CreateTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : tableValuedFunction.Definition;
        }

        public override string CreateTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            if (!(DataProperties.ReplicateItemsMarkedNotForReplication || !trigger.IsNotForReplication))
                return string.Empty;

            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : trigger.Definition;
        }

        // ALTER TABLE [dbo].[SampleConstraints] ADD CONSTRAINT [IX_ProductName]
        // UNIQUE NONCLUSTERED ([id] ASC, [id2] DESC)
        // WITH (ONLINE = ON, PAD_INDEX = ON, FILLFACTOR = 75, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY];
        public override string CreateUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(uniqueConstraint.UserTable));
            builder.Append(" ADD CONSTRAINT `");
            builder.Append(uniqueConstraint.ObjectName);
            builder.Append("`  UNIQUE ");
            builder.Append(uniqueConstraint.IsClustered ? "CLUSTERED (" : "NONCLUSTERED (");

            var options = GetIndexOptions(
                sourceDataContext,
                targetDataContext,
                DataProperties.SqlServerOnlineOption,
                uniqueConstraint.AllowRowLocks,
                uniqueConstraint.AllowPageLocks,
                uniqueConstraint.FillFactor,
                uniqueConstraint.IsPadded,
                uniqueConstraint.IgnoreDupKey,
                DataProperties.SqlServerIgnoreFileGroups,
                uniqueConstraint.FileGroup);

            var first = true;
            foreach (var uniqueConstraintColumn in uniqueConstraint.UniqueConstraintColumns)
            {
                if (first)
                {
                    builder.Append("`");
                    builder.Append(uniqueConstraintColumn.ObjectName);
                    builder.Append(uniqueConstraintColumn.IsDescendingKey ? "`  DESC" : "`  ASC");

                    first = false;
                    continue;
                }

                builder.Append(", `");
                builder.Append(uniqueConstraintColumn.ObjectName);
                builder.Append(uniqueConstraintColumn.IsDescendingKey ? "`  DESC" : "`  ASC");
            }

            builder.Append(")");
            if (options.Length > 0)
                builder.Append(" ");
            builder.Append(options);

            return builder.ToString();
        }

        public override string CreateUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType)
        {
            return null;
        }

        public override string CreateUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable)
        {
            var builder = new StringBuilder();
            builder.Append("CREATE TABLE ");
            builder.Append(CreateIdentifier(userTable));
            builder.AppendLine(" (");

            var first = true;
            foreach (var userTableColumn in userTable.UserTableColumns)
            {

                builder.AppendLine(CreateUserTableColumn(sourceDataContext, targetDataContext, userTableColumn, first));
                first = false;
            }
            var primaryKey = userTable.UserTableColumns.FirstOrDefault(p => p.IsIdentity);
            if (primaryKey != null)
            {
                builder.AppendFormat(", PRIMARY KEY (`{0}`)\r\n", primaryKey.ObjectName);
            }
            //foreach (var fk in userTable.UserTableColumns.Values.Where(p=>p.))
            //{
            //    builder.AppendFormat(", KEY `{0}` (`{0}`)\r\n", fk.ObjectName);
            //}
            builder.Append(") ENGINE = InnoDB DEFAULT CHARSET=latin1");
            return builder.ToString();
        }

        // creates user-table column
        public override string CreateUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn, bool firstColumn = false)
        {
            var builder = new StringBuilder();
            var definition = DataTypes.ConvertDataTypeDefinition(sourceDataContext, targetDataContext, userTableColumn);
            if (string.IsNullOrEmpty(definition))
                return null;

            builder.Append(firstColumn ? "    " : "  , ");
            builder.Append(definition);
            return builder.ToString();
        }

        public override string CreateView(DataContext sourceDataContext, DataContext targetDataContext, View view)
        {
            return sourceDataContext.ContextType != targetDataContext.ContextType
                ? null
                : view.Definition;
        }

        public override string DisableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            if (trigger.TriggerForSchema.Length == 0
                || trigger.TriggerForObjectName.Length == 0)
                return string.Empty;

            if (!(DataProperties.ReplicateItemsMarkedNotForReplication || !trigger.IsNotForReplication))
                return string.Empty;

            var builder = new StringBuilder();
            builder.Append("DISABLE TRIGGER ");
            builder.Append(CreateIdentifier(trigger));
            builder.Append(" ON `");
            builder.Append(trigger.TriggerForSchema);
            builder.Append("` .`");
            builder.Append(trigger.TriggerForObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string DropAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction)
        {
            if (sourceDataContext.ContextType != targetDataContext.ContextType)
                return null;

            var builder = new StringBuilder();
            builder.Append("DROP AGGREGATE ");
            builder.Append(CreateIdentifier(aggregateFunction));
            return builder.ToString();
        }

        public override string DropCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog)
        {
            var builder = new StringBuilder();
            builder.Append("DROP DATABASE ");
            builder.Append(CreateIdentifier(catalog.Namespace));
            return builder.ToString();
        }

        public override string DropCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            return null;
        }

        public override string DropDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(defaultConstraint.UserTable));
            builder.Append(" DROP CONSTRAINT `");
            builder.Append(defaultConstraint.ObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string DropForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(foreignKey.UserTable));
            builder.Append(" DROP FOREIGN KEY `");
            builder.Append(foreignKey.ObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string DropIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index)
        {
            var builder = new StringBuilder();
            builder.Append("DROP INDEX `");
            builder.Append(index.ObjectName);
            builder.Append("`  ON");
            builder.Append(CreateIdentifier(index.UserTable));
            return builder.ToString();
        }

        public override string DropInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction)
        {
            if (sourceDataContext.ContextType != targetDataContext.ContextType)
                return null;

            var builder = new StringBuilder();
            builder.Append("DROP FUNCTION ");
            builder.Append(CreateIdentifier(inlineTableValuedFunction));
            return builder.ToString();
        }

        public override string DropPrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(primaryKey.UserTable));
            builder.Append(" DROP PRIMARY KEY");
            return builder.ToString();
        }

        public override string DropScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction)
        {
            if (sourceDataContext.ContextType != targetDataContext.ContextType)
                return null;

            var builder = new StringBuilder();
            builder.Append("DROP FUNCTION ");
            builder.Append(CreateIdentifier(scalarFunction));
            return builder.ToString();
        }

        public override string DropSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema)
        {
            return null;
        }

        public override string DropStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure)
        {
            if (sourceDataContext.ContextType != targetDataContext.ContextType)
                return null;

            var builder = new StringBuilder();
            builder.Append("DROP PROCEDURE ");
            builder.Append(CreateIdentifier(storedProcedure));
            return builder.ToString();
        }

        public override string DropTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction)
        {
            if (sourceDataContext.ContextType != targetDataContext.ContextType)
                return null;

            var builder = new StringBuilder();
            builder.Append("DROP FUNCTION ");
            builder.Append(CreateIdentifier(tableValuedFunction));
            return builder.ToString();
        }

        public override string DropTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            var builder = new StringBuilder();
            builder.Append("DROP TRIGGER ");
            builder.Append(CreateIdentifier(trigger));
            return builder.ToString();
        }

        public override string DropUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(uniqueConstraint.UserTable));
            builder.Append(" DROP CONSTRAINT `");
            builder.Append(uniqueConstraint.ObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string DropUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType)
        {
            return null;
            // MySQL does not support user-defined data types.
            //var builder = new StringBuilder();
            //builder.Append("DROP TYPE ");
            //builder.Append(CreateIdentifier(userDefinedDataType));
            //return builder.ToString();
        }

        public override string DropUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable)
        {
            var builder = new StringBuilder();
            builder.Append("DROP TABLE ");
            builder.Append(CreateIdentifier(userTable));
            return builder.ToString();
        }

        public override string DropUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            var builder = new StringBuilder();
            builder.Append("ALTER TABLE ");
            builder.Append(CreateIdentifier(userTableColumn.UserTable));
            builder.Append(" DROP COLUMN `");
            builder.Append(userTableColumn.ObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string DropView(DataContext sourceDataContext, DataContext targetDataContext, View view)
        {
            if (sourceDataContext.ContextType != targetDataContext.ContextType)
                return null;

            var builder = new StringBuilder();
            builder.Append("DROP VIEW ");
            builder.Append(CreateIdentifier(view));
            return builder.ToString();
        }

        public override string EnableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            if (trigger.TriggerForSchema.Length == 0
                || trigger.TriggerForObjectName.Length == 0)
                return string.Empty;

            if (!(DataProperties.ReplicateItemsMarkedNotForReplication || !trigger.IsNotForReplication))
                return string.Empty;

            var builder = new StringBuilder();
            builder.Append("ENABLE TRIGGER ");
            builder.Append(CreateIdentifier(trigger));
            builder.Append(" ON `");
            builder.Append(trigger.TriggerForSchema);
            builder.Append("` .`");
            builder.Append(trigger.TriggerForObjectName);
            builder.Append("`");
            return builder.ToString();
        }

        public override string EscapeString(DataContext sourceDataContext, DataContext targetDataContext, object value)
        {
            return value.ToString().Replace("'", "\\'");
        }

        public override string GetIndexOptions(DataContext sourceDataContext, DataContext targetDataContext, bool online, bool allowRowLocks, bool allowPageLocks,
            int fillFactor, bool isPadded, bool ignoreDupKey, bool ignoreFileGroups, string fileGroup)
        {
            var builder = new StringBuilder();
            var builder2 = new StringBuilder();
            var i = 0;

            if (online)
            {
                builder.Append(i++ == 0 ? string.Empty : ", ");
                builder.Append("ONLINE = ON");
            }

            if (!allowRowLocks)
            {
                builder.Append(i++ == 0 ? string.Empty : ", ");
                builder.Append("ALLOW_ROW_LOCKS = OFF");
            }

            if (!allowPageLocks)
            {
                builder.Append(i++ == 0 ? string.Empty : ", ");
                builder.Append("ALLOW_PAGE_LOCKS = OFF");
            }

            if (fillFactor > 0 && isPadded)
            {
                builder.Append(i++ == 0 ? string.Empty : ", ");
                builder.Append("PAD_INDEX = ON, FILLFACTOR = ");
                builder.Append(fillFactor);
            }

            if (ignoreDupKey)
            {
                builder.Append(i++ == 0 ? string.Empty : ", ");
                builder.Append("IGNORE_DUP_KEY = ON");
            }

            if (ignoreFileGroups)
            {

                if (i > 0)
                {
                    builder2.Append(" WITH (");
                    builder2.Append(builder);
                    builder2.Append(")");
                    return builder2.ToString();
                }

                return string.Empty;
            }

            if (i > 0)
            {
                builder2.Append(" WITH (");
                builder2.Append(builder);
                builder2.Append(") ON `");
                builder2.Append(fileGroup);
                builder2.Append("`");
                return builder2.ToString();
            }


            builder2.Append(" ON `");
            builder2.Append(fileGroup);
            builder2.Append("`");
            return builder2.ToString();
        }

        public override string InsertRow(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable, object[] values)
        {
            throw new NotImplementedException();
            //var builder = new StringBuilder();
            //builder.AppendFormat("INSERT INTO {0} VALUES (", CreateIdentifier(userTable));
            //var i = 0;
            //foreach (var column in userTable.UserTableColumns)
            //{
            //    var isLast = column == userTable.UserTableColumns.Last();
            //    var value = CreateObjectValue(sourceDataContext, targetDataContext, column.CommonDataType, values[i]);
            //    builder.Append(value);
            //    if (!isLast) builder.Append(", ");
            //    i++;
            //}
            //builder.Append(")");
            //return builder.ToString();
        }

        public override string TruncateTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable)
        {
            return string.Format("TRUNCATE TABLE {0}", CreateIdentifier(userTable));
        }

        public override string UseCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog)
        {
            var builder = new StringBuilder();
            builder.Append("USE ");
            builder.Append(CreateIdentifier(catalog.Namespace));
            return builder.ToString();
        }
    }
}
