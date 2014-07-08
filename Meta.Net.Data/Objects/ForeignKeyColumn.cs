using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class ForeignKeyColumn : IDataObject, IDataUserTableBasedObject
    {
		#region Properties (9) 

        public Catalog Catalog
        {
            get
            {
                var foreignKey = ForeignKey;
                if (foreignKey == null)
                    return null;

                var userTable = foreignKey.UserTable;
                if (userTable == null)
                    return null;

                var schema = userTable.Schema;
                return schema == null
                    ? null
                    : schema.Catalog;
            }
        }

        public string Description { get; set; }

        public ForeignKey ForeignKey { get; set; }

        public int KeyOrdinal { get; set; }

        public string Namespace
        {
            get
            {
                if (ForeignKey == null)
                    return ObjectName;

                return ForeignKey.Namespace + "." + ObjectName;
            }
        }

        public string ObjectName
        {
            get
            {
                return _objectName;
            }
            set
            {
                if (ForeignKey != null)
                {
                    if (ForeignKey.RenameForeignKeyColumn(ForeignKey, _objectName, value))
                        _objectName = value;
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                        return;

                    _objectName = value;
                }
            }
        }

        public string ReferencedColumnName { get; set; }

        public Schema Schema
        {
            get
            {
                var foreignKey = ForeignKey;
                if (foreignKey == null)
                    return null;

                var userTable = foreignKey.UserTable;
                return userTable == null
                    ? null
                    : userTable.Schema;
            }
        }

        public UserTable UserTable
        {
            get
            {
                var foreignKey = ForeignKey;
                return foreignKey == null
                    ? null
                    : foreignKey.UserTable;
            }
        }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public ForeignKeyColumn(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            ForeignKey = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            KeyOrdinal = info.GetInt32("KeyOrdinal");
            ReferencedColumnName = info.GetString("ReferencedColumnName");
        }

        public ForeignKeyColumn(ForeignKey foreignKey, ForeignKeysRow foreignKeysRow)
		{
            Init(this, foreignKey, foreignKeysRow.ColumnName, foreignKeysRow);
		}

        public ForeignKeyColumn(ForeignKey foreignKey, string objectName)
        {
            Init(this, foreignKey, objectName, null);
        }

        public ForeignKeyColumn(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public ForeignKeyColumn()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (7) 

		#region Public Methods (6) 

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="foreignKeyColumn">The foreign key column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static ForeignKeyColumn Clone(ForeignKeyColumn foreignKeyColumn)
        {
            return new ForeignKeyColumn(foreignKeyColumn.ObjectName)
                {
                    KeyOrdinal = foreignKeyColumn.KeyOrdinal,
                    ReferencedColumnName = foreignKeyColumn.ReferencedColumnName
                };
        }

        public static bool CompareDefinitions(ForeignKeyColumn sourceForeignKeyColumn, ForeignKeyColumn targetForeignKeyColumn)
        {
            if (!CompareObjectNames(sourceForeignKeyColumn, targetForeignKeyColumn))
                return false;

            if (sourceForeignKeyColumn.KeyOrdinal != targetForeignKeyColumn.KeyOrdinal)
                return false;

            return StringComparer.OrdinalIgnoreCase.Compare(
                sourceForeignKeyColumn.ReferencedColumnName, targetForeignKeyColumn.ReferencedColumnName) == 0;
        }

        public static bool CompareObjectNames(ForeignKeyColumn sourceForeignKeyColumn, ForeignKeyColumn targetForeignKeyColumn,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceForeignKeyColumn.ObjectName, targetForeignKeyColumn.ObjectName) == 0;
            }
        }

        public static ForeignKeyColumn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ForeignKeyColumn>(json);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("KeyOrdinal", KeyOrdinal);
            info.AddValue("ReferencedColumnName", ReferencedColumnName);
        }

        public static string ToJson(ForeignKeyColumn foreignKeyColumn, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(foreignKeyColumn, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(ForeignKeyColumn foreignKeyColumn, ForeignKey foreignKey, string objectName, ForeignKeysRow foreignKeysRow)
        {
            foreignKeyColumn.ForeignKey = foreignKey;
            foreignKeyColumn._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            foreignKeyColumn.Description = "Foreign Key Column";

            foreignKeyColumn.KeyOrdinal = foreignKeysRow == null
                ? 0
                : foreignKeysRow.KeyOrdinal;
            foreignKeyColumn.ReferencedColumnName = "";
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
