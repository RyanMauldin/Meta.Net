using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public class UserDefinedDataTypesAdapter
    {
        public static void Read(Catalog catalog, IDataReader reader)
        {
            var schemaNameOrdinal = reader.GetOrdinal("SchemaName");
            var objectNameOrdinal = reader.GetOrdinal("ObjectName");
            var dataTypeOrdinal = reader.GetOrdinal("DataType");
            var maxLengthOrdinal = reader.GetOrdinal("MaxLength");
            var precisionOrdinal = reader.GetOrdinal("Precision");
            var scaleOrdinal = reader.GetOrdinal("Scale");
            var collationOrdinal = reader.GetOrdinal("Collation");
            var hasDefaultOrdinal = reader.GetOrdinal("HasDefault");
            var isUserDefinedOrdinal = reader.GetOrdinal("IsUserDefined");
            var isAssemblyTypeOrdinal = reader.GetOrdinal("IsAssemblyType");
            var isNullableOrdinal = reader.GetOrdinal("IsNullable");

            while (reader.Read())
            {
                var schemaName = Convert.ToString(reader[schemaNameOrdinal]);
                var objectName = Convert.ToString(reader[objectNameOrdinal]);
                var dataType = Convert.ToString(reader[dataTypeOrdinal]);
                var maxLength = Convert.ToInt32(reader[maxLengthOrdinal]);
                var precision = Convert.ToInt32(reader[precisionOrdinal]);
                var scale = Convert.ToInt32(reader[scaleOrdinal]);
                var collation = Convert.ToString(reader[collationOrdinal]);
                var hasDefault = Convert.ToBoolean(reader[hasDefaultOrdinal]);
                var isUserDefined = Convert.ToBoolean(reader[isUserDefinedOrdinal]);
                var isAssemblyType = Convert.ToBoolean(reader[isAssemblyTypeOrdinal]);
                var isNullable = Convert.ToBoolean(reader[isNullableOrdinal]);

                var schema = catalog.Schemas[schemaName];
                if (schema == null)
                    continue;

                var userDefinedDataType = new UserDefinedDataType
                {
                    Schema = schema,
                    ObjectName = objectName,
                    DataType = dataType,
                    MaxLength = maxLength,
                    Precision = precision,
                    Scale = scale,
                    Collation = collation,
                    HasDefault = hasDefault,
                    IsUserDefined = isUserDefined,
                    IsAssemblyType = isAssemblyType,
                    IsNullable = isNullable
                };

                schema.UserDefinedDataTypes.Add(userDefinedDataType);
            }
        }

        public static void Get(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserDefinedDataTypes(catalog.ObjectName);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(catalog, reader);

                    reader.Close();
                }
            }
        }
        
        public static async Task GetAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserDefinedDataTypes(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(catalog, reader);

                    reader.Close();
                }
            }
        }
        
        public static async Task GetAsync(Catalog catalog, DbConnection connection, IMetadataScriptFactory metadataScriptFactory, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.UserDefinedDataTypes(catalog.ObjectName);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(catalog, reader);

                    reader.Close();
                }
            }
        }
    }
}
