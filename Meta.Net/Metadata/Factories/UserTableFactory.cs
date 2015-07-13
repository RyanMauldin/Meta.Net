using System;
using System.Collections.Generic;
using System.Data;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class UserTableFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int FileStreamFileGroupOrdinal { get; set; }
        private int LobFileGroupOrdinal { get; set; }
        private int HasTextNTextOrImageColumnsOrdinal { get; set; }
        private int UsesAnsiNullsOrdinal { get; set; }
        private int TextInRowLimitOrdinal { get; set; }

        public UserTableFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            FileStreamFileGroupOrdinal = reader.GetOrdinal("FileStreamFileGroup");
            LobFileGroupOrdinal = reader.GetOrdinal("LobFileGroup");
            HasTextNTextOrImageColumnsOrdinal = reader.GetOrdinal("HasTextNTextOrImageColumns");
            UsesAnsiNullsOrdinal = reader.GetOrdinal("UsesAnsiNulls");
            TextInRowLimitOrdinal = reader.GetOrdinal("TextInRowLimit");
        }

        public void CreateUserTable(
            Catalog catalog,
            Dictionary<string, UserTable> userTables,
            IDataRecord reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);

            var schema = catalog.Schemas[schemaName];
            if (schema == null)
                return;

            var userTable = new UserTable
            {
                Schema = schema,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                FileStreamFileGroup = Convert.ToString(reader[FileStreamFileGroupOrdinal]),
                LobFileGroup = Convert.ToString(reader[LobFileGroupOrdinal]),
                HasTextNTextOrImageColumns = Convert.ToBoolean(reader[HasTextNTextOrImageColumnsOrdinal]),
                UsesAnsiNulls = Convert.ToBoolean(reader[UsesAnsiNullsOrdinal]),
                TextInRowLimit = Convert.ToInt32(reader[TextInRowLimitOrdinal])
            };

            schema.UserTables.Add(userTable);
            userTables.Add(userTable.Namespace, userTable);
        }
    }
}
