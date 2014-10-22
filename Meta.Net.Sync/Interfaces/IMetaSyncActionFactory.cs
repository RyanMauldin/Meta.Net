using Meta.Net.Objects;

namespace Meta.Net.Sync.Interfaces
{
    public interface IMetaSyncActionFactory
    {
        DataSyncAction AddUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        DataSyncAction AlterAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        DataSyncAction AlterInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        DataSyncAction AlterScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        DataSyncAction AlterStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        DataSyncAction AlterTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        DataSyncAction AlterTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        DataSyncAction AlterUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        DataSyncAction AlterView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        DataSyncAction CreateAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        DataSyncAction CreateCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

        DataSyncAction CreateCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        DataSyncAction CreateDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint);

        DataSyncAction CreateForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        DataSyncAction CreateIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index);

        DataSyncAction CreateInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        DataSyncAction CreatePrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey);

        DataSyncAction CreateScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        DataSyncAction CreateSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema);

        DataSyncAction CreateStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        DataSyncAction CreateTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        DataSyncAction CreateTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        DataSyncAction CreateUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint);

        DataSyncAction CreateUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType);

        DataSyncAction CreateUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        DataSyncAction CreateView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        DataSyncAction DisableCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        DataSyncAction DisableForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        DataSyncAction DisableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        DataSyncAction DropAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        DataSyncAction DropCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

        DataSyncAction DropCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        DataSyncAction DropDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint);

        DataSyncAction DropForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        DataSyncAction DropIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index);

        DataSyncAction DropInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        DataSyncAction DropPrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey);

        DataSyncAction DropScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        DataSyncAction DropSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema);

        DataSyncAction DropStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        DataSyncAction DropTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        DataSyncAction DropTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        DataSyncAction DropUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint);

        DataSyncAction DropUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType);

        DataSyncAction DropUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        DataSyncAction DropUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        DataSyncAction DropView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        DataSyncAction EnableCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        DataSyncAction EnableForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        DataSyncAction EnableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        DataSyncAction InsertRow(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable, params object[] values);

        /// <summary>
        /// Sorts the SyncActionsCollection so that it can be executed by the database one
        /// query at a time. The speed is O(2n) where n is the number of SyncActions.
        /// </summary>
        DataSyncActionsCollection SortSyncActions(DataContext sourceDataContext, DataContext targetDataContext, DataSyncActionsCollection dataSyncActions);

        DataSyncAction TruncateTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        DataSyncAction UseCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);
    }
}
