using System;
using System.Linq;
using System.Text;
using Meta.Net.Sync.Interfaces;
using Meta.Net.Objects;
using Meta.Net.Sync.Types;

namespace Meta.Net.Sync
{
    public static class DataActionFactory
    {
		public static IMetaScriptFactory ContextScriptFactory { get; set; } 

		private const string EndAltered = " will be altered.";
        private const string EndCreated = " will be created.";
        private const string EndDisabled = " will be disabled.";
        private const string EndDropped = " will be dropped.";
        private const string EndEnabled = " will be enabled.";
        private const string EndTruncate = " will be truncated.";
        private const string EndUsed = " will be used.";
        private const string Space = " ";
        private const string StartThe = "The ";
        
        public static DataSyncAction AddUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userTableColumn.Description)
                .Append(Space)
                .Append(userTableColumn.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.AddUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userTableColumn
                , userTableColumn.Namespace
                , builder.ToString()
                , DataSyncOperationType.AddUserTableColumn
                , script
            );
        }

        public static DataSyncAction AlterAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(aggregateFunction.Description)
                .Append(Space)
                .Append(aggregateFunction.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  aggregateFunction
                , aggregateFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterAggregateFunction
                , script
            );
        }

        public static DataSyncAction AlterInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(inlineTableValuedFunction.Description)
                .Append(Space)
                .Append(inlineTableValuedFunction.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  inlineTableValuedFunction
                , inlineTableValuedFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterInlineTableValuedFunction
                , script
            );
        }

        public static DataSyncAction AlterScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(scalarFunction.Description)
                .Append(Space)
                .Append(scalarFunction.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  scalarFunction
                , scalarFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterScalarFunction
                , script
            );
        }

        public static DataSyncAction AlterStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(storedProcedure.Description)
                .Append(Space).Append(storedProcedure.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  storedProcedure
                , storedProcedure.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterStoredProcedure
                , script
            );
        }

        public static DataSyncAction AlterTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(tableValuedFunction.Description)
                .Append(Space)
                .Append(tableValuedFunction.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  tableValuedFunction
                , tableValuedFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterTableValuedFunction
                , script
            );
        }

        public static DataSyncAction AlterTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(trigger.Description)
                .Append(Space)
                .Append(trigger.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterTrigger(sourceDataContext, targetDataContext, trigger);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  trigger
                , trigger.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterTrigger
                , script
            );
        }

        public static DataSyncAction AlterUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userTableColumn.Description)
                .Append(Space)
                .Append(userTableColumn.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userTableColumn
                , userTableColumn.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterUserTableColumn
                , script
            );
        }

        public static DataSyncAction AlterView(DataContext sourceDataContext, DataContext targetDataContext, View view)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(view.Description)
                .Append(Space)
                .Append(view.Namespace)
                .Append(EndAltered);
            Func<string> script = () => ContextScriptFactory.AlterView(sourceDataContext, targetDataContext, view);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  view
                , view.Namespace
                , builder.ToString()
                , DataSyncOperationType.AlterView
                , script
            );
        }

        public static DataSyncAction CreateAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(aggregateFunction.Description)
                .Append(Space)
                .Append(aggregateFunction.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  aggregateFunction
                , aggregateFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateAggregateFunction
                , script
            );
        }

        public static DataSyncAction CreateCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(catalog.Description)
                .Append(Space)
                .Append(catalog.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateCatalog(sourceDataContext, targetDataContext, catalog);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  catalog
                , catalog.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateCatalog
                , script
            );
        }

        public static DataSyncAction CreateCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(checkConstraint.Description)
                .Append(Space)
                .Append(checkConstraint.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  checkConstraint
                , checkConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateCheckConstraint
                , script
            );
        }

        public static DataSyncAction CreateDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(defaultConstraint.Description)
                .Append(Space)
                .Append(defaultConstraint.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateDefaultConstraint(sourceDataContext, targetDataContext, defaultConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  defaultConstraint
                , defaultConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateDefaultConstraint
                , script
            );
        }

        public static DataSyncAction CreateForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(foreignKey.Description)
                .Append(Space)
                .Append(foreignKey.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateForeignKey(sourceDataContext, targetDataContext, foreignKey);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  foreignKey
                , foreignKey.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateForeignKey
                , script
            );
        }

        public static DataSyncAction CreateIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(index.Description)
                .Append(Space)
                .Append(index.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateIndex(sourceDataContext, targetDataContext, index);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  index
                , index.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateIndex
                , script
            );
        }

        public static DataSyncAction CreateInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(inlineTableValuedFunction.Description)
                .Append(Space)
                .Append(inlineTableValuedFunction.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  inlineTableValuedFunction
                , inlineTableValuedFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateInlineTableValuedFunction
                , script
            );
        }

        public static DataSyncAction CreatePrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(primaryKey.Description)
                .Append(Space)
                .Append(primaryKey.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreatePrimaryKey(sourceDataContext, targetDataContext, primaryKey);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  primaryKey
                , primaryKey.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreatePrimaryKey
                , script
            );
        }

        public static DataSyncAction CreateScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(scalarFunction.Description)
                .Append(Space)
                .Append(scalarFunction.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  scalarFunction
                , scalarFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateScalarFunction
                , script
            );
        }

        public static DataSyncAction CreateSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(schema.Description)
                .Append(Space)
                .Append(schema.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateSchema(sourceDataContext, targetDataContext, schema);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  schema
                , schema.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateSchema
                , script
            );
        }

        public static DataSyncAction CreateStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(storedProcedure.Description)
                .Append(Space)
                .Append(storedProcedure.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  storedProcedure
                , storedProcedure.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateStoredProcedure
                , script
            );
        }

        public static DataSyncAction CreateTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(tableValuedFunction.Description)
                .Append(Space)
                .Append(tableValuedFunction.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  tableValuedFunction
                , tableValuedFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateTableValuedFunction
                , script
            );
        }

        public static DataSyncAction CreateTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(trigger.Description)
                .Append(Space)
                .Append(trigger.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateTrigger(sourceDataContext, targetDataContext, trigger);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  trigger
                , trigger.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateTrigger
                , script
            );
        }

        public static DataSyncAction CreateUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(uniqueConstraint.Description)
                .Append(Space)
                .Append(uniqueConstraint.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateUniqueConstraint(sourceDataContext, targetDataContext, uniqueConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  uniqueConstraint
                , uniqueConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateUniqueConstraint
                , script
            );
        }

        public static DataSyncAction CreateUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userDefinedDataType.Description)
                .Append(Space)
                .Append(userDefinedDataType.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateUserDefinedDataType(sourceDataContext, targetDataContext, userDefinedDataType);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userDefinedDataType
                , userDefinedDataType.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateUserDefinedDataType
                , script
            );
        }

        public static DataSyncAction CreateUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userTable.Description)
                .Append(Space)
                .Append(userTable.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateUserTable(sourceDataContext, targetDataContext, userTable);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userTable
                , userTable.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateUserTable
                , script
            );
        }

        public static DataSyncAction CreateView(DataContext sourceDataContext, DataContext targetDataContext, View view)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(view.Description)
                .Append(Space)
                .Append(view.Namespace)
                .Append(EndCreated);
            Func<string> script = () => ContextScriptFactory.CreateView(sourceDataContext, targetDataContext, view);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  view
                , view.Namespace
                , builder.ToString()
                , DataSyncOperationType.CreateView
                , script
            );
        }

        public static DataSyncAction DisableCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(checkConstraint.Description)
                .Append(Space)
                .Append(checkConstraint.Namespace)
                .Append(EndDisabled);
            Func<string> script = () => ContextScriptFactory.AlterCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  checkConstraint
                , checkConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.DisableCheckConstraint
                , script
            );
        }

        public static DataSyncAction DisableForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(foreignKey.Description)
                .Append(Space)
                .Append(foreignKey.Namespace)
                .Append(EndDisabled);
            Func<string> script = () => ContextScriptFactory.AlterForeignKey(sourceDataContext, targetDataContext, foreignKey);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  foreignKey
                , foreignKey.Namespace
                , builder.ToString()
                , DataSyncOperationType.DisableForeignKey
                , script
            );
        }

        public static DataSyncAction DisableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(trigger.Description)
                .Append(Space)
                .Append(trigger.Namespace)
                .Append(EndDisabled);
            Func<string> script = () => ContextScriptFactory.DisableTrigger(sourceDataContext, targetDataContext, trigger);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  trigger
                , trigger.Namespace
                , builder.ToString()
                , DataSyncOperationType.DisableTrigger
                , script
            );
        }

        public static DataSyncAction DropAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(aggregateFunction.Description)
                .Append(Space)
                .Append(aggregateFunction.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropAggregateFunction(sourceDataContext, targetDataContext, aggregateFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  aggregateFunction
                , aggregateFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropAggregateFunction
                , script
            );
        }

        public static DataSyncAction DropCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(catalog.Description)
                .Append(Space)
                .Append(catalog.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropCatalog(sourceDataContext, targetDataContext, catalog);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  catalog
                , catalog.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropCatalog
                , script
            );
        }

        public static DataSyncAction DropCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(checkConstraint.Description)
                .Append(Space)
                .Append(checkConstraint.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  checkConstraint
                , checkConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropCheckConstraint
                , script
            );
        }

        public static DataSyncAction DropDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(defaultConstraint.Description)
                .Append(Space)
                .Append(defaultConstraint.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropDefaultConstraint(sourceDataContext, targetDataContext, defaultConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  defaultConstraint
                , defaultConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropDefaultConstraint
                , script
            );
        }

        public static DataSyncAction DropForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(foreignKey.Description)
                .Append(Space)
                .Append(foreignKey.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropForeignKey(sourceDataContext, targetDataContext, foreignKey);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  foreignKey
                , foreignKey.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropForeignKey
                , script
            );
        }

        public static DataSyncAction DropIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(index.Description)
                .Append(Space)
                .Append(index.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropIndex(sourceDataContext, targetDataContext, index);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  index
                , index.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropIndex
                , script
            );
        }

        public static DataSyncAction DropInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(inlineTableValuedFunction.Description)
                .Append(Space)
                .Append(inlineTableValuedFunction.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropInlineTableValuedFunction(sourceDataContext, targetDataContext, inlineTableValuedFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  inlineTableValuedFunction
                , inlineTableValuedFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropInlineTableValuedFunction
                , script
            );
        }

        public static DataSyncAction DropPrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(primaryKey.Description)
                .Append(Space)
                .Append(primaryKey.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropPrimaryKey(sourceDataContext, targetDataContext, primaryKey);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  primaryKey
                , primaryKey.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropPrimaryKey
                , script
            );
        }

        public static DataSyncAction DropScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(scalarFunction.Description)
                .Append(Space)
                .Append(scalarFunction.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropScalarFunction(sourceDataContext, targetDataContext, scalarFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  scalarFunction
                , scalarFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropScalarFunction
                , script
            );
        }

        public static DataSyncAction DropSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(schema.Description)
                .Append(Space)
                .Append(schema.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropSchema(sourceDataContext, targetDataContext, schema);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  schema
                , schema.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropSchema
                , script
            );
        }

        public static DataSyncAction DropStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(storedProcedure.Description)
                .Append(Space)
                .Append(storedProcedure.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropStoredProcedure(sourceDataContext, targetDataContext, storedProcedure);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  storedProcedure
                , storedProcedure.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropStoredProcedure
                , script
            );
        }

        public static DataSyncAction DropTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(tableValuedFunction.Description)
                .Append(Space)
                .Append(tableValuedFunction.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropTableValuedFunction(sourceDataContext, targetDataContext, tableValuedFunction);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  tableValuedFunction
                , tableValuedFunction.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropTableValuedFunction
                , script
            );
        }

        public static DataSyncAction DropTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(trigger.Description)
                .Append(Space)
                .Append(trigger.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropTrigger(sourceDataContext, targetDataContext, trigger);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  trigger
                , trigger.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropTrigger
                , script
            );
        }

        public static DataSyncAction DropUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(uniqueConstraint.Description)
                .Append(Space)
                .Append(uniqueConstraint.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropUniqueConstraint(sourceDataContext, targetDataContext, uniqueConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  uniqueConstraint
                , uniqueConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropUniqueConstraint
                , script
            );
        }

        public static DataSyncAction DropUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userDefinedDataType.Description)
                .Append(Space)
                .Append(userDefinedDataType.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropUserDefinedDataType(sourceDataContext, targetDataContext, userDefinedDataType);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userDefinedDataType
                , userDefinedDataType.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropUserDefinedDataType
                , script
            );
        }

        public static DataSyncAction DropUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userTable.Description)
                .Append(Space)
                .Append(userTable.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropUserTable(sourceDataContext, targetDataContext, userTable);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userTable
                , userTable.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropUserTable
                , script
            );
        }

        public static DataSyncAction DropUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(userTableColumn.Description)
                .Append(Space)
                .Append(userTableColumn.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  userTableColumn
                , userTableColumn.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropUserTableColumn
                , script
            );
        }

        public static DataSyncAction DropView(DataContext sourceDataContext, DataContext targetDataContext, View view)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(view.Description)
                .Append(Space)
                .Append(view.Namespace)
                .Append(EndDropped);
            Func<string> script = () => ContextScriptFactory.DropView(sourceDataContext, targetDataContext, view);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  view
                , view.Namespace
                , builder.ToString()
                , DataSyncOperationType.DropView
                , script
            );
        }

        public static DataSyncAction EnableCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(checkConstraint.Description)
                .Append(Space)
                .Append(checkConstraint.Namespace)
                .Append(EndEnabled);
            Func<string> script = () => ContextScriptFactory.AlterCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  checkConstraint
                , checkConstraint.Namespace
                , builder.ToString()
                , DataSyncOperationType.EnableCheckContraint
                , script
            );
        }

        public static DataSyncAction EnableForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(foreignKey.Description)
                .Append(Space)
                .Append(foreignKey.Namespace)
                .Append(EndEnabled);
            Func<string> script = () => ContextScriptFactory.AlterForeignKey(sourceDataContext, targetDataContext, foreignKey);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  foreignKey
                , foreignKey.Namespace
                , builder.ToString()
                , DataSyncOperationType.EnableForeignKey
                , script
            );
        }

        public static DataSyncAction EnableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(trigger.Description)
                .Append(Space)
                .Append(trigger.Namespace)
                .Append(EndEnabled);
            Func<string> script = () => ContextScriptFactory.EnableTrigger(sourceDataContext, targetDataContext, trigger);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  trigger
                , trigger.Namespace
                , builder.ToString()
                , DataSyncOperationType.EnableTrigger
                , script
            );
        }

        public static DataSyncAction InsertRow(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable, params object[] values)
        {
            Func<string> action = () => ContextScriptFactory.InsertRow(sourceDataContext, targetDataContext, userTable, values);
            return new DataSyncAction(
                  userTable
                , userTable.Namespace
                , string.Format("Inserts values into the {0} table", userTable.Namespace)
                , DataSyncOperationType.InsertRow
                , action
            );
        }

        /// <summary>
        /// Sorts the SyncActionsCollection so that it can be executed by the database one
        /// query at a time. The speed is O(2n) where n is the number of SyncActions.
        /// </summary>
        public static DataSyncActionsCollection SortSyncActions(DataContext sourceDataContext, DataContext targetDataContext, DataSyncActionsCollection dataSyncActions)
        {
            var actionsCollection = new DataSyncActionsCollection();
            actionsCollection.AddRange(dataSyncActions.OrderBy(p => (int)p.Type));
            return actionsCollection;
        }

        public static DataSyncAction TruncateTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable table)
        {
            return new DataSyncAction(
                  table
                , table.Namespace
                , string.Format("{0}{1}", table.Namespace, EndTruncate)
                , DataSyncOperationType.TruncateTable
                , () => ContextScriptFactory.TruncateTable(sourceDataContext, targetDataContext, table)
            );
        }

        public static DataSyncAction UseCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog)
        {
            var builder = new StringBuilder();
            builder.Append(StartThe)
                .Append(catalog.Description)
                .Append(Space)
                .Append(catalog.Namespace)
                .Append(EndUsed);
            Func<string> script = () => ContextScriptFactory.UseCatalog(sourceDataContext, targetDataContext, catalog);
            if (string.IsNullOrEmpty(script.Invoke()))
                return null;
            return new DataSyncAction(
                  catalog
                , catalog.Namespace
                , builder.ToString()
                , DataSyncOperationType.UseCatalog
                , script
            );
        }
    }
}
