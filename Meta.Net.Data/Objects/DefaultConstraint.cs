using System;
using System.Runtime.Serialization;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class DefaultConstraint : IDataObject, IDataUserTableBasedObject
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

        public string ColumnName { get; set; }

        public string Definition { get; set; }

        public string Description { get; set; }

        public bool IsSystemNamed { get; set; }

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
                    if (UserTable.RenameDefaultConstraint(UserTable, _objectName, value))
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

        public DefaultConstraint(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UserTable = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            ColumnName = info.GetString("ColumnName");
            Definition = info.GetString("Definition");
            IsSystemNamed = info.GetBoolean("IsSystemNamed");
        }

        public DefaultConstraint(UserTable userTable, DefaultConstraintsRow defaultConstraintsRow)
        {
            Init(this, userTable, defaultConstraintsRow.ObjectName, defaultConstraintsRow);
        }

        public DefaultConstraint(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public DefaultConstraint(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public DefaultConstraint()
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
        /// <param name="defaultConstraint">The default constraint to clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static DefaultConstraint Clone(DefaultConstraint defaultConstraint)
        {
            return new DefaultConstraint(defaultConstraint.ObjectName)
                {
                    ColumnName = defaultConstraint.ColumnName,
                    Definition = defaultConstraint.Definition,
                    IsSystemNamed = defaultConstraint.IsSystemNamed
                };
        }

        public static bool CompareDefinitions(DefaultConstraint sourceDefaultConstraint, DefaultConstraint targetDefaultConstraint)
        {
            if (!CompareObjectNames(sourceDefaultConstraint, targetDefaultConstraint))
                return false;

            if (sourceDefaultConstraint.IsSystemNamed != targetDefaultConstraint.IsSystemNamed)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceDefaultConstraint.ColumnName, targetDefaultConstraint.ColumnName) != 0)
                return false;

            return DataProperties.DefinitionComparer.Compare(
                sourceDefaultConstraint.Definition, targetDefaultConstraint.Definition) == 0;
        }

        public static bool CompareObjectNames(DefaultConstraint sourceDefaultConstraint, DefaultConstraint targetDefaultConstraint,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
            }
        }

        public static DefaultConstraint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<DefaultConstraint>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            DefaultConstraint defaultConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreateDefaultConstraint(sourceDataContext, targetDataContext, defaultConstraint);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            DefaultConstraint defaultConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.DropDefaultConstraint(sourceDataContext, targetDataContext, defaultConstraint);
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
            info.AddValue("IsSystemNamed", IsSystemNamed);
        }

        public static string ToJson(DefaultConstraint defaultConstraint, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(defaultConstraint, formatting);
        }

        #endregion Public Methods
        #region Private Methods (1)

        private static void Init(DefaultConstraint defaultConstraint, UserTable userTable, string objectName, DefaultConstraintsRow defaultConstraintsRow)
        {
            defaultConstraint.UserTable = userTable;
            defaultConstraint._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            defaultConstraint.Description = "Default Constraint";

            if (defaultConstraintsRow == null)
            {
                defaultConstraint.ColumnName = "";
                defaultConstraint.Definition = "";
                defaultConstraint.IsSystemNamed = false;
                return;
            }

            defaultConstraint.ColumnName = defaultConstraintsRow.ColumnName;
            defaultConstraint.Definition = defaultConstraintsRow.Definition;
            defaultConstraint.IsSystemNamed = defaultConstraintsRow.IsSystemNamed;
        }

        #endregion Private Methods

        #endregion Methods
    }
}
