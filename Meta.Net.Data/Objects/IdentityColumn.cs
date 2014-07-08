using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class IdentityColumn : IDataObject, IDataUserTableBasedObject
    {
		#region Properties (10) 

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

        public string Description { get; set; }

        public int IncrementValue { get; set; }

        public bool IsNotForReplication { get; set; }

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
                    if (UserTable.RenameIdentityColumn(UserTable, _objectName, value))
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

        public int SeedValue { get; set; }

        public string TypeDescription { get; private set; }

        public UserTable UserTable { get; set; }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public IdentityColumn(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UserTable = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            IncrementValue = info.GetInt32("IncrementValue");
            IsNotForReplication = info.GetBoolean("IsNotForReplication");
            SeedValue = info.GetInt32("SeedValue");
            TypeDescription = info.GetString("TypeDescription");
        }

        public IdentityColumn(UserTable userTable, IdentityColumnsRow identityColumnsRow)
		{
            Init(this, userTable, identityColumnsRow.ObjectName, identityColumnsRow);
		}

        public IdentityColumn(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public IdentityColumn(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public IdentityColumn()
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
        /// <param name="identityColumn">The identity column to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static IdentityColumn Clone(IdentityColumn identityColumn)
        {
            return new IdentityColumn(identityColumn.ObjectName)
                {
                    SeedValue = identityColumn.SeedValue,
                    IncrementValue = identityColumn.IncrementValue,
                    IsNotForReplication = identityColumn.IsNotForReplication
                };
        }

        public static bool CompareDefinitions(IdentityColumn sourceIdentityColumn, IdentityColumn targetIdentityColumn)
        {
            if (!CompareObjectNames(sourceIdentityColumn, targetIdentityColumn))
                return false;

            if (sourceIdentityColumn.IncrementValue != targetIdentityColumn.IncrementValue)
                return false;

            if (sourceIdentityColumn.IsNotForReplication != targetIdentityColumn.IsNotForReplication)
                return false;

            return sourceIdentityColumn.SeedValue == targetIdentityColumn.SeedValue;
        }

        public static bool CompareObjectNames(IdentityColumn sourceIdentityColumn, IdentityColumn targetIdentityColumn,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceIdentityColumn.ObjectName, targetIdentityColumn.ObjectName) == 0;
            }
        }

        public static IdentityColumn FromJson(string json)
        {
            return JsonConvert.DeserializeObject<IdentityColumn>(json);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("IncrementValue", IncrementValue);
            info.AddValue("IsNotForReplication", IsNotForReplication);
            info.AddValue("SeedValue", SeedValue);
            info.AddValue("TypeDescription", TypeDescription);
        }

        public static string ToJson(IdentityColumn identityColumn, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(identityColumn, formatting);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(IdentityColumn identityColumn, UserTable userTable, string objectName, IdentityColumnsRow identityColumnsRow)
        {
            identityColumn.UserTable = userTable;
            identityColumn._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            identityColumn.Description = "Identity Column";
            identityColumn.TypeDescription = "IDENTITY_COLUMN";
            
            if (identityColumnsRow == null)
            {
                identityColumn.SeedValue = 1;
                identityColumn.IncrementValue = 1;
                identityColumn.IsNotForReplication = false;
                return;
            }

            identityColumn.SeedValue = identityColumnsRow.SeedValue;
            identityColumn.IncrementValue = identityColumnsRow.IncrementValue;
            identityColumn.IsNotForReplication = identityColumnsRow.IsNotForReplication;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
