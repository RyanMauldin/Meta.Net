using System;
using System.Data;
using Meta.Net.Objects;

namespace Meta.Net.Metadata.Factories
{
    internal class UserDefinedDataTypeFactory
    {
        private int SchemaNameOrdinal { get; set; }
        private int ObjectNameOrdinal { get; set; }
        private int DataTypeOrdinal { get; set; }
        private int MaxLengthOrdinal { get; set; }
        private int PrecisionOrdinal { get; set; }
        private int ScaleOrdinal { get; set; }
        private int CollationOrdinal { get; set; }
        private int HasDefaultOrdinal { get; set; }
        private int IsUserDefinedOrdinal { get; set; }
        private int IsAssemblyTypeOrdinal { get; set; }
        private int IsNullableOrdinal { get; set; }

        public UserDefinedDataTypeFactory(IDataRecord reader)
        {
            SchemaNameOrdinal = reader.GetOrdinal("SchemaName");
            ObjectNameOrdinal = reader.GetOrdinal("ObjectName");
            DataTypeOrdinal = reader.GetOrdinal("DataType");
            MaxLengthOrdinal = reader.GetOrdinal("MaxLength");
            PrecisionOrdinal = reader.GetOrdinal("Precision");
            ScaleOrdinal = reader.GetOrdinal("Scale");
            CollationOrdinal = reader.GetOrdinal("Collation");
            HasDefaultOrdinal = reader.GetOrdinal("HasDefault");
            IsUserDefinedOrdinal = reader.GetOrdinal("IsUserDefined");
            IsAssemblyTypeOrdinal = reader.GetOrdinal("IsAssemblyType");
            IsNullableOrdinal = reader.GetOrdinal("IsNullable");
        }

        public void CreateUserDefinedDataType(
            Catalog catalog,
            IDataRecord reader)
        {
            var schemaName = Convert.ToString(reader[SchemaNameOrdinal]);

            var schema = catalog.Schemas[schemaName];
            if (schema == null)
                return;

            var userDefinedDataType = new UserDefinedDataType
            {
                Schema = schema,
                ObjectName = Convert.ToString(reader[ObjectNameOrdinal]),
                DataType = Convert.ToString(reader[DataTypeOrdinal]),
                MaxLength = Convert.ToInt32(reader[MaxLengthOrdinal]),
                Precision = Convert.ToInt32(reader[PrecisionOrdinal]),
                Scale = Convert.ToInt32(reader[ScaleOrdinal]),
                Collation = Convert.ToString(reader[CollationOrdinal]),
                HasDefault = Convert.ToBoolean(reader[HasDefaultOrdinal]),
                IsUserDefined = Convert.ToBoolean(reader[IsUserDefinedOrdinal]),
                IsAssemblyType = Convert.ToBoolean(reader[IsAssemblyTypeOrdinal]),
                IsNullable = Convert.ToBoolean(reader[IsNullableOrdinal])
            };

            schema.UserDefinedDataTypes.Add(userDefinedDataType);
        }
    }
}
