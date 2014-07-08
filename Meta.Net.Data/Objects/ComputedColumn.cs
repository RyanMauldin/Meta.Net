using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class ComputedColumn : IDataObject, IDataUserTableBasedObject
    {
		#region Properties (9) 

        public Catalog Catalog
        {
            get
            {
                var userTable = UserTable;
                if (userTable == null)
                    return null;

                var schema = userTable.Schema;
                return schema == null
                    ? null
                    : schema.Catalog;
            }
        }

        public string Definition { get; set; }

        public string Description { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPersisted { get; set; }

        public string Namespace
        {
            get
            {
                if (UserTable == null)
                    return ObjectName;

                return UserTable.Namespace + "." + ObjectName;
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
                if (UserTable != null)
                {
                    if (UserTable.RenameComputedColumn(UserTable, _objectName, value))
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

        public Schema Schema
        {
            get
            {
                var userTable = UserTable;
                return userTable == null
                    ? null
                    : userTable.Schema;
            }
        }

        public UserTable UserTable { get; set; }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public ComputedColumn(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UserTable = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            Definition = info.GetString("Definition");
            IsNullable = info.GetBoolean("IsNullable");
            IsPersisted = info.GetBoolean("IsPersisted");
        }

        public ComputedColumn(UserTable userTable, ComputedColumnsRow computedColumnsRow)
		{
            Init(this, userTable, computedColumnsRow.ObjectName, computedColumnsRow);
		}

        public ComputedColumn(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public ComputedColumn(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public ComputedColumn()
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
        /// <param name="computedColumn">The computed column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static ComputedColumn Clone(ComputedColumn computedColumn)
        {
            return new ComputedColumn(computedColumn.ObjectName)
                {
                    Definition = computedColumn.Definition,
                    IsPersisted = computedColumn.IsPersisted,
                    IsNullable = computedColumn.IsNullable
                };
        }

        public static bool CompareDefinitions(ComputedColumn sourceComputedColumn, ComputedColumn targetComputedColumn)
        {
            if (!CompareObjectNames(sourceComputedColumn, targetComputedColumn))
                return false;

            if (sourceComputedColumn.IsPersisted != targetComputedColumn.IsPersisted)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceComputedColumn.Definition, targetComputedColumn.Definition) == 0;
        }

        public static bool CompareObjectNames(ComputedColumn sourceComputedColumn, ComputedColumn targetComputedColumn,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceComputedColumn.ObjectName, targetComputedColumn.ObjectName) == 0;
            }
        }

        public static ComputedColumn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ComputedColumn>(json);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("Definition", Definition);
            info.AddValue("IsNullable", IsNullable);
            info.AddValue("IsPersisted", IsPersisted);
        }

        public static string ToJson(ComputedColumn computedColumn, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(computedColumn, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(ComputedColumn computedColumn, UserTable userTable, string objectName, ComputedColumnsRow computedColumnsRow)
        {
            computedColumn.UserTable = userTable;
            computedColumn._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            computedColumn.Description = "Computed Column";

            if (computedColumnsRow == null)
            {
                computedColumn.Definition = "";
                computedColumn.IsPersisted = false;
                computedColumn.IsNullable = true;
                return;
            }

            computedColumn.Definition = computedColumnsRow.Definition;
            computedColumn.IsPersisted = computedColumnsRow.IsPersisted;
            computedColumn.IsNullable = computedColumnsRow.IsNullable;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
