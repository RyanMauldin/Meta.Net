using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class CheckConstraint : IDataObject, IDataUserTableBasedObject
    {
        #region Properties (13)

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

        public string ColumnName { get; set; }

        public string Definition { get; set; }

        public string Description { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsNotForReplication { get; set; }

        public bool IsNotTrusted { get; set; }

        public bool IsSystemNamed { get; set; }

        public bool IsTableConstraint { get; set; }

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
                    if (UserTable.RenameCheckConstraint(UserTable, _objectName, value))
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

        public CheckConstraint(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UserTable = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            ColumnName = info.GetString("ColumnName");
            Definition = info.GetString("Definition");
            IsDisabled = info.GetBoolean("IsDisabled");
            IsNotForReplication = info.GetBoolean("IsNotForReplication");
            IsNotTrusted = info.GetBoolean("IsNotTrusted");
            IsSystemNamed = info.GetBoolean("IsSystemNamed");
            IsTableConstraint = info.GetBoolean("IsTableConstraint");
        }

        public CheckConstraint(UserTable userTable, CheckConstraintsRow checkConstraintsRow)
        {
            Init(this, userTable, checkConstraintsRow.ObjectName, checkConstraintsRow);
        }

        public CheckConstraint(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public CheckConstraint(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public CheckConstraint()
        {
            Init(this, null, null, null);
        }

        #endregion Constructors

        #region Methods (9)

        #region Public Methods (8)

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="checkConstraint">The check constraint to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static CheckConstraint Clone(CheckConstraint checkConstraint)
        {
            return new CheckConstraint(checkConstraint.ObjectName)
                {
                    ColumnName = checkConstraint.ColumnName,
                    Definition = checkConstraint.Definition,
                    IsDisabled = checkConstraint.IsDisabled,
                    IsNotForReplication = checkConstraint.IsNotForReplication,
                    IsNotTrusted = checkConstraint.IsNotTrusted,
                    IsSystemNamed = checkConstraint.IsSystemNamed,
                    IsTableConstraint = checkConstraint.IsTableConstraint
                };
        }

        public static bool CompareDefinitions(CheckConstraint sourceCheckConstraint, CheckConstraint targetCheckConstraint)
        {
            if (!CompareObjectNames(sourceCheckConstraint, targetCheckConstraint))
                return false;

            if (sourceCheckConstraint.IsDisabled != targetCheckConstraint.IsDisabled)
                return false;

            if (sourceCheckConstraint.IsNotForReplication != targetCheckConstraint.IsNotForReplication)
                return false;

            if (sourceCheckConstraint.IsNotTrusted != targetCheckConstraint.IsNotTrusted)
                return false;

            if (sourceCheckConstraint.IsSystemNamed != targetCheckConstraint.IsSystemNamed)
                return false;

            if (sourceCheckConstraint.IsTableConstraint != targetCheckConstraint.IsTableConstraint)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceCheckConstraint.ColumnName, targetCheckConstraint.ColumnName) != 0)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceCheckConstraint.Definition, targetCheckConstraint.Definition) == 0;
        }

        public static bool CompareObjectNames(CheckConstraint sourceCheckConstraint, CheckConstraint targetCheckConstraint,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceCheckConstraint.ObjectName, targetCheckConstraint.ObjectName) == 0;
            }
        }

        public static CheckConstraint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CheckConstraint>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            CheckConstraint checkConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreateCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            CheckConstraint checkConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.DropCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("ColumnName", ColumnName);
            info.AddValue("Definition", Definition);
            info.AddValue("IsDisabled", IsDisabled);
            info.AddValue("IsNotForReplication", IsNotForReplication);
            info.AddValue("IsNotTrusted", IsNotTrusted);
            info.AddValue("IsSystemNamed", IsSystemNamed);
            info.AddValue("IsTableConstraint", IsTableConstraint);
        }

        public static string ToJson(CheckConstraint checkConstraint, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(checkConstraint, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(CheckConstraint checkConstraint, UserTable userTable, string objectName, CheckConstraintsRow checkConstraintsRow)
        {
            checkConstraint.UserTable = userTable;
            checkConstraint._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            checkConstraint.Description = "Check Constraint";

            if (checkConstraintsRow == null)
            {
                checkConstraint.ColumnName = "";
                checkConstraint.Definition = "";
                checkConstraint.IsDisabled = false;
                checkConstraint.IsNotForReplication = false;
                checkConstraint.IsNotTrusted = false;
                checkConstraint.IsSystemNamed = false;
                checkConstraint.IsTableConstraint = true;
                return;
            }

            checkConstraint.ColumnName = checkConstraintsRow.ColumnName;
            checkConstraint.Definition = checkConstraintsRow.Definition;
            checkConstraint.IsDisabled = checkConstraintsRow.IsDisabled;
            checkConstraint.IsNotForReplication = checkConstraintsRow.IsNotForReplication;
            checkConstraint.IsNotTrusted = checkConstraintsRow.IsNotTrusted;
            checkConstraint.IsSystemNamed = checkConstraintsRow.IsSystemNamed;
            checkConstraint.IsTableConstraint = checkConstraintsRow.IsTableConstraint;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
