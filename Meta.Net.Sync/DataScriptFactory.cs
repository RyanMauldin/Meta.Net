using Meta.Net.Sync.Interfaces;
using Meta.Net.Sync.Types;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Sync
{
    public abstract class DataScriptFactory : IMetaScriptFactory
    {
		public abstract string AddUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        public abstract string AlterAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        public abstract string AlterCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        public abstract string AlterForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        public abstract string AlterInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        public abstract string AlterScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        public abstract string AlterStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        public abstract string AlterTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        public abstract string AlterTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        public abstract string AlterUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        public abstract string AlterView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        public abstract string CreateAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        public abstract string CreateCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

        public abstract string CreateCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        public abstract string CreateDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint);

        public abstract string CreateForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        public string CreateIdentifier(IMetaObject obj)
        {
            return CreateIdentifier(obj.Namespace);
        }

        public abstract string CreateIdentifier(string obj);

        public abstract string CreateIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index);

        public abstract string CreateInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        public abstract string CreateObjectValue(DataContext sourceDataContext, DataContext targetDataContext, CommonDataType dataType, object value);

        public abstract string CreatePrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey);

        public abstract string CreateScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        public abstract string CreateSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema);

        public abstract string CreateStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        public abstract string CreateTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        public abstract string CreateTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        public abstract string CreateUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint);

        public abstract string CreateUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType);

        public abstract string CreateUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        public abstract string CreateUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn, bool firstColumn = false);

        public abstract string CreateView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        public abstract string DisableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        public abstract string DropAggregateFunction(DataContext sourceDataContext, DataContext targetDataContext, AggregateFunction aggregateFunction);

        public abstract string DropCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);

        public abstract string DropCheckConstraint(DataContext sourceDataContext, DataContext targetDataContext, CheckConstraint checkConstraint);

        public abstract string DropDefaultConstraint(DataContext sourceDataContext, DataContext targetDataContext, DefaultConstraint defaultConstraint);

        public abstract string DropForeignKey(DataContext sourceDataContext, DataContext targetDataContext, ForeignKey foreignKey);

        public abstract string DropIndex(DataContext sourceDataContext, DataContext targetDataContext, Index index);

        public abstract string DropInlineTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, InlineTableValuedFunction inlineTableValuedFunction);

        public abstract string DropPrimaryKey(DataContext sourceDataContext, DataContext targetDataContext, PrimaryKey primaryKey);

        public abstract string DropScalarFunction(DataContext sourceDataContext, DataContext targetDataContext, ScalarFunction scalarFunction);

        public abstract string DropSchema(DataContext sourceDataContext, DataContext targetDataContext, Schema schema);

        public abstract string DropStoredProcedure(DataContext sourceDataContext, DataContext targetDataContext, StoredProcedure storedProcedure);

        public abstract string DropTableValuedFunction(DataContext sourceDataContext, DataContext targetDataContext, TableValuedFunction tableValuedFunction);

        public abstract string DropTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        public abstract string DropUniqueConstraint(DataContext sourceDataContext, DataContext targetDataContext, UniqueConstraint uniqueConstraint);

        public abstract string DropUserDefinedDataType(DataContext sourceDataContext, DataContext targetDataContext, UserDefinedDataType userDefinedDataType);

        public abstract string DropUserTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        public abstract string DropUserTableColumn(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn userTableColumn);

        public abstract string DropView(DataContext sourceDataContext, DataContext targetDataContext, View view);

        public abstract string EnableTrigger(DataContext sourceDataContext, DataContext targetDataContext, Trigger trigger);

        public abstract string EscapeString(DataContext sourceDataContext, DataContext targetDataContext, object value);

        public abstract string GetIndexOptions(DataContext sourceDataContext, DataContext targetDataContext, bool online, bool allowRowLocks, bool allowPageLocks,
            int fillFactor, bool isPadded, bool ignoreDupKey, bool ignoreFileGroups, string fileGroup);

        public abstract string InsertRow(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable, object[] values);

        public abstract string TruncateTable(DataContext sourceDataContext, DataContext targetDataContext, UserTable userTable);

        public abstract string UseCatalog(DataContext sourceDataContext, DataContext targetDataContext, Catalog catalog);
    }
}
