using Meta.Net.Data.Enum;
using Meta.Net.Data.Objects;

namespace Meta.Net.Data.Interfaces
{
    public interface IDataScriptFactory
    {
		#region Operations (55) 

        string AddUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        string AlterAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        string AlterCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        string AlterForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        string AlterInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        string AlterScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        string AlterStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        string AlterTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        string AlterTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        string AlterUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        string AlterView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        string CreateAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        string CreateCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

        string CreateCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        string CreateDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint);

        string CreateForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        string CreateIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index);

        string CreateInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        string CreateObjectValue(DataContext sourceDataContext, DataContext targetDataContext, CommonDataType dataType, object value);

        string CreatePrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey);

        string CreateScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        string CreateSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema);

        string CreateStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        string CreateTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        string CreateTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        string CreateUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint);

        string CreateUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType);

        string CreateUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        string CreateUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn, bool firstColumn = false);

        string CreateView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        string DisableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        string DropAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        string DropCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

        string DropCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        string DropDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint);

        string DropForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        string DropIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index);

        string DropInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        string DropPrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey);

        string DropScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        string DropSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema);

        string DropStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        string DropTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        string DropTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        string DropUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint);

        string DropUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType);

        string DropUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        string DropUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        string DropView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        string EnableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        string EscapeString(DataContext sourceDataContext, DataContext targetDataContext, object value);

        string GetIndexOptions(DataContext sourceDataContext, DataContext targetDataContext, bool online, bool allowRowLocks, bool allowPageLocks,
            int fillFactor, bool isPadded, bool ignoreDupKey, bool ignoreFileGroups, string fileGroup);

        string InsertRow(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable, object[] values);

        string TruncateTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        string UseCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

		#endregion Operations 
    }
}
