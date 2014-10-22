using System.Collections.Generic;
using System.Text;
using Meta.Net.Interfaces;

namespace Meta.Net
{
    public abstract class DataMetadataScriptFactory : IMetadataScriptFactory
    {
        public string CatalogToken { get; set; }
        public string OrderByObjectName { get; set; }
        public string CatalogsSql { get; set; }
        public string SchemasSql { get; set; }
        public string CheckConstraintsSql { get; set; }
        public string ComputedColumnsSql { get; set; }
        public string DefaultConstraintsSql { get; set; }
        public string ForeignKeysSql { get; set; }
        public string IdentityColumnsSql { get; set; }
        public string IndexesSql { get; set; }
        public string ModulesSql { get; set; }
        public string PrimaryKeysSql { get; set; }
        public string UniqueConstraintsSql { get; set; }
        public string UserDefinedDataTypesSql { get; set; }
        public string UserTableColumnsSql { get; set; }
        public string UserTablesSql { get; set; }

        public string Catalogs()
        {
            return CatalogsSql;
        }

        public virtual string Catalogs(IList<string> catalogs)
        {
            var builder = new StringBuilder(CatalogsSql);
            builder.Replace(OrderByObjectName, " ");
            builder.Append("AND sch.`SCHEMA_NAME` IN (");
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

        public virtual string Schemas(string catalog)
        {
            var builder = new StringBuilder(SchemasSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string CheckConstraints(string catalog)
        {
            var builder = new StringBuilder(CheckConstraintsSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string ComputedColumns(string catalog)
        {
            var builder = new StringBuilder(ComputedColumnsSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string DefaultConstraints(string catalog)
        {
            var builder = new StringBuilder(DefaultConstraintsSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string ForeignKeys(string catalog)
        {
            var builder = new StringBuilder(ForeignKeysSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string IdentityColumns(string catalog)
        {
            var builder = new StringBuilder(IdentityColumnsSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string Indexes(string catalog)
        {
            var builder = new StringBuilder(IndexesSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string Modules(string catalog)
        {
            var builder = new StringBuilder(ModulesSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string PrimaryKeys(string catalog)
        {
            var builder = new StringBuilder(PrimaryKeysSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string UniqueConstraints(string catalog)
        {
            var builder = new StringBuilder(UniqueConstraintsSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string UserDefinedDataTypes(string catalog)
        {
            var builder = new StringBuilder(UserDefinedDataTypesSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string UserTableColumns(string catalog)
        {
            var builder = new StringBuilder(UserTableColumnsSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }

        public virtual string UserTables(string catalog)
        {
            var builder = new StringBuilder(UserTablesSql);
            builder.Replace(CatalogToken, catalog);
            return builder.ToString();
        }
    }
}
