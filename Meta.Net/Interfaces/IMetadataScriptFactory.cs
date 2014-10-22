using System.Collections.Generic;

namespace Meta.Net.Interfaces
{
    public interface IMetadataScriptFactory
    {
        string CatalogToken { get; }
        string OrderByObjectName { get; }
        string CatalogsSql { get; }
        string SchemasSql { get; }
        string CheckConstraintsSql { get; }
        string ComputedColumnsSql { get; }
        string DefaultConstraintsSql { get; }
        string ForeignKeysSql { get; }
        string IdentityColumnsSql { get; }
        string IndexesSql { get; }
        string ModulesSql { get; }
        string PrimaryKeysSql { get; }
        string UniqueConstraintsSql { get; }
        string UserDefinedDataTypesSql { get; }
        string UserTableColumnsSql { get; }
        string UserTablesSql { get; }

        string Catalogs();
        string Catalogs(IList<string> catalogs);
        string Schemas(string catalog);
        string CheckConstraints(string catalog);
        string ComputedColumns(string catalog);
        string DefaultConstraints(string catalog);
        string ForeignKeys(string catalog);
        string IdentityColumns(string catalog);
        string Indexes(string catalog);
        string Modules(string catalog);
        string PrimaryKeys(string catalog);
        string UniqueConstraints(string catalog);
        string UserDefinedDataTypes(string catalog);
        string UserTableColumns(string catalog);
        string UserTables(string catalog);
    }
}
