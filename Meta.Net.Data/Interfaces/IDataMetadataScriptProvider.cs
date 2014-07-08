using System.Collections.Generic;

namespace Meta.Net.Data.Interfaces
{
    public interface IDataMetadataScriptProvider
    {
        HashSet<string> CatalogsToFill { get; set; }
        string SchemasSql { get; }
        string CatalogsSql { get; }
        string CheckConstraintsSql { get; }
        string ComputedColumnsSql { get; }
        string DefaultConstraintsSql { get; }
        string UserTablesSql { get; }
        string UserTableColumnsSql { get; }
        string UserDefinedDataTypesSql { get; }
        string UniqueConstraintsSql { get; }
        string PrimaryKeysSql { get; }
        string ModulesSql { get; }
        string IndexesSql { get; }
        string IdentityColumnsSql { get; }
        string ForeignKeysSql { get; }
        string ForeignKeyMapsSql { get; }

        string SchemasSql2(string catalogName);
        string CheckConstraintsSql2(string catalogName);
        string ComputedColumnsSql2(string catalogName);
        string DefaultConstraintsSql2(string catalogName);
        string UserTablesSql2(string catalogName);
        string UserTableColumnsSql2(string catalogName);
        string UserDefinedDataTypesSql2(string catalogName);
        string UniqueConstraintsSql2(string catalogName);
        string PrimaryKeysSql2(string catalogName);
        string ModulesSql2(string catalogName);
        string IndexesSql2(string catalogName);
        string IdentityColumnsSql2(string catalogName);
        string ForeignKeysSql2(string catalogName);
        string ForeignKeyMapsSql2(string catalogName);
    }
}
