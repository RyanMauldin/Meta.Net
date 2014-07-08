using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Meta.Net;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class PrimaryKey : IDataObject, IDataUserTableBasedObject
    {
		#region Properties (16) 

        public bool AllowPageLocks { get; set; }

        public bool AllowRowLocks { get; set; }

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

        public string FileGroup { get; set; }

        public int FillFactor { get; set; }

        public bool IgnoreDupKey { get; set; }

        public string IndexType { get; set; }

        public bool IsClustered { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsPadded { get; set; }

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
                    if (UserTable.RenameUniqueConstraint(UserTable, _objectName, value))
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

        public Dictionary<string, PrimaryKeyColumn> PrimaryKeyColumns { get; private set; }

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

        public PrimaryKey(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UserTable = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            AllowPageLocks = info.GetBoolean("AllowPageLocks");
            AllowRowLocks = info.GetBoolean("AllowRowLocks");
            FileGroup = info.GetString("FileGroup");
            FillFactor = info.GetInt32("FillFactor");
            IgnoreDupKey = info.GetBoolean("IgnoreDupKey");
            IndexType = info.GetString("IndexType");
            IsClustered = info.GetBoolean("IsClustered");
            IsDisabled = info.GetBoolean("IsDisabled");
            IsPadded = info.GetBoolean("IsPadded");

            // Deserialize Primary Key Columns
            var primaryKeyColumns = info.GetInt32("PrimaryKeyColumns");
            PrimaryKeyColumns = new Dictionary<string, PrimaryKeyColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < primaryKeyColumns; i++)
            {
                var primaryKeyColumn = (PrimaryKeyColumn)info.GetValue("PrimaryKeyColumn" + i, typeof(PrimaryKeyColumn));
                primaryKeyColumn.PrimaryKey = this;
                PrimaryKeyColumns.Add(primaryKeyColumn.ObjectName, primaryKeyColumn);
            }
        }

        public PrimaryKey(UserTable userTable, PrimaryKeysRow primaryKeysRow)
        {
            Init(this, userTable, primaryKeysRow.ObjectName, primaryKeysRow);
        }

        public PrimaryKey(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public PrimaryKey(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public PrimaryKey()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (21) 

		#region Public Methods (20) 

        public static bool AddPrimaryKeyColumn(PrimaryKey primaryKey, PrimaryKeyColumn primaryKeyColumn)
        {
            if (primaryKey.PrimaryKeyColumns.ContainsKey(primaryKeyColumn.ObjectName))
                return false;

            if (primaryKeyColumn.PrimaryKey == null)
            {
                primaryKeyColumn.PrimaryKey = primaryKey;
                primaryKey.PrimaryKeyColumns.Add(primaryKeyColumn.ObjectName, primaryKeyColumn);
                return true;
            }

            if (primaryKeyColumn.PrimaryKey.Equals(primaryKey))
            {
                primaryKey.PrimaryKeyColumns.Add(primaryKeyColumn.ObjectName, primaryKeyColumn);
                return true;
            }

            return false;
        }

        public static bool AddPrimaryKeyColumn(PrimaryKey primaryKey, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (primaryKey.PrimaryKeyColumns.ContainsKey(objectName))
                return false;

            var primaryKeyColumn = new PrimaryKeyColumn(primaryKey, objectName);
            primaryKey.PrimaryKeyColumns.Add(objectName, primaryKeyColumn);

            return true;
        }

        /// <summary>
        /// Shallow Clear...
        /// </summary>
        /// <param name="primaryKey">The primary key to shallow clear.</param>
        public static void Clear(PrimaryKey primaryKey)
        {
            primaryKey.PrimaryKeyColumns.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="primaryKey">The primary key to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static PrimaryKey Clone(PrimaryKey primaryKey)
        {
            var primaryKeyClone = new PrimaryKey(primaryKey.ObjectName)
            {
                AllowPageLocks = primaryKey.AllowPageLocks,
                AllowRowLocks = primaryKey.AllowRowLocks,
                Description = primaryKey.Description,
                FileGroup = primaryKey.FileGroup,
                FillFactor = primaryKey.FillFactor,
                IgnoreDupKey = primaryKey.IgnoreDupKey,
                IndexType = primaryKey.IndexType,
                IsClustered = primaryKey.IsClustered,
                IsDisabled = primaryKey.IsDisabled,
                IsPadded = primaryKey.IsPadded
            };

            foreach (var primaryKeyColumn in primaryKey.PrimaryKeyColumns.Values)
                AddPrimaryKeyColumn(primaryKeyClone, PrimaryKeyColumn.Clone(primaryKeyColumn));

            return primaryKeyClone;
        }

        public static bool CompareDefinitions(PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey)
        {
            if (!CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
                return false;

            if (sourcePrimaryKey.AllowPageLocks != targetPrimaryKey.AllowPageLocks)
                return false;

            if (sourcePrimaryKey.AllowRowLocks != targetPrimaryKey.AllowRowLocks)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourcePrimaryKey.FileGroup, targetPrimaryKey.FileGroup) != 0)
                return false;

            if (sourcePrimaryKey.FillFactor != targetPrimaryKey.FillFactor)
                return false;

            if (sourcePrimaryKey.IgnoreDupKey != targetPrimaryKey.IgnoreDupKey)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourcePrimaryKey.IndexType, targetPrimaryKey.IndexType) != 0)
                return false;

            if (sourcePrimaryKey.IsClustered != targetPrimaryKey.IsClustered)
                return false;

            if (sourcePrimaryKey.IsDisabled != targetPrimaryKey.IsDisabled)
                return false;

            return sourcePrimaryKey.IsPadded == targetPrimaryKey.IsPadded;
        }

        public static bool CompareObjectNames(PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target PrimaryKey from the source PrimaryKey.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourcePrimaryKey">The source PrimaryKey.</param>
        /// <param name="targetPrimaryKey">The target PrimaryKey.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingPrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingPrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
            matchingPrimaryKeyColumns.IntersectWith(targetPrimaryKey.PrimaryKeyColumns.Keys);

            foreach (var primaryKeyColumn in matchingPrimaryKeyColumns)
            {
                PrimaryKeyColumn sourcePrimaryKeyColumn;
                if (!sourcePrimaryKey.PrimaryKeyColumns.TryGetValue(primaryKeyColumn, out sourcePrimaryKeyColumn))
                    continue;

                PrimaryKeyColumn targetPrimaryKeyColumn;
                if (!targetPrimaryKey.PrimaryKeyColumns.TryGetValue(primaryKeyColumn, out targetPrimaryKeyColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (PrimaryKeyColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
                            RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (PrimaryKeyColumn.CompareObjectNames(sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
                            RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static void Fill(PrimaryKey primaryKey, DataGenerics generics)
        {
            Clear(primaryKey);
            var predicate = new StringPredicate(primaryKey.Namespace + ".", StringComparison.OrdinalIgnoreCase);

            var primaryKeys = generics.PrimaryKeys.FindAll(predicate.StartsWith);
            foreach (var str in primaryKeys)
            {
                PrimaryKeysRow primaryKeysRow;
                if (!generics.PrimaryKeyRows.TryGetValue(str + ".", out primaryKeysRow))
                    continue;

                var primaryKeyColumn = new PrimaryKeyColumn(primaryKey, primaryKeysRow);
                AddPrimaryKeyColumn(primaryKey, primaryKeyColumn);
            }
        }

        public static PrimaryKey FromJson(string json)
        {
            return JsonConvert.DeserializeObject<PrimaryKey>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            PrimaryKey primaryKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreatePrimaryKey(sourceDataContext, targetDataContext, primaryKey);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            PrimaryKey primaryKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.DropPrimaryKey(sourceDataContext, targetDataContext, primaryKey);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("AllowPageLocks", AllowPageLocks);
            info.AddValue("AllowRowLocks", AllowRowLocks);
            info.AddValue("FileGroup", FileGroup);
            info.AddValue("FillFactor", FillFactor);
            info.AddValue("IgnoreDupKey", IgnoreDupKey);
            info.AddValue("IndexType", IndexType);
            info.AddValue("IsClustered", IsClustered);
            info.AddValue("IsDisabled", IsDisabled);
            info.AddValue("IsPadded", IsPadded);

            // Serialize Primary Key Columns
            info.AddValue("PrimaryKeyColumns", PrimaryKeyColumns.Count);

            var i = 0;
            foreach (var primaryKeyColumn in PrimaryKeyColumns.Values)
                info.AddValue("PrimaryKeyColumn" + i++, primaryKeyColumn);
        }

        /// <summary>
        /// Modifies the source PrimaryKey to contain only objects that are
        /// present in the source PrimaryKey and in the target PrimaryKey.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourcePrimaryKey">The source PrimaryKey.</param>
        /// <param name="targetPrimaryKey">The target PrimaryKey.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingPrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removablePrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingPrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
            matchingPrimaryKeyColumns.IntersectWith(targetPrimaryKey.PrimaryKeyColumns.Keys);

            removablePrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
            removablePrimaryKeyColumns.ExceptWith(matchingPrimaryKeyColumns);

            foreach (var primaryKeyColumn in removablePrimaryKeyColumns)
                RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);

            foreach (var primaryKeyColumn in matchingPrimaryKeyColumns)
            {
                PrimaryKeyColumn sourcePrimaryKeyColumn;
                if (!sourcePrimaryKey.PrimaryKeyColumns.TryGetValue(primaryKeyColumn, out sourcePrimaryKeyColumn))
                    continue;

                PrimaryKeyColumn targetPrimaryKeyColumn;
                if (!targetPrimaryKey.PrimaryKeyColumns.TryGetValue(primaryKeyColumn, out targetPrimaryKeyColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!PrimaryKeyColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
                            RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!PrimaryKeyColumn.CompareObjectNames(sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
                            RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static long ObjectCount(PrimaryKey primaryKey)
        {
            return primaryKey.PrimaryKeyColumns.Count;
        }

        public static bool RemovePrimaryKeyColumn(PrimaryKey primaryKey, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && primaryKey.PrimaryKeyColumns.Remove(objectName);
        }

        public static bool RemovePrimaryKeyColumn(PrimaryKey primaryKey, PrimaryKeyColumn primaryKeyColumn)
        {
            PrimaryKeyColumn primaryKeyColumnObject;
            if (!primaryKey.PrimaryKeyColumns.TryGetValue(primaryKeyColumn.ObjectName, out primaryKeyColumnObject))
                return false;

            return primaryKeyColumn.Equals(primaryKeyColumnObject) &&
                   primaryKey.PrimaryKeyColumns.Remove(primaryKeyColumn.ObjectName);
        }

        public static bool RenamePrimaryKeyColumn(PrimaryKey primaryKey, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (primaryKey.PrimaryKeyColumns.ContainsKey(newObjectName))
                return false;

            PrimaryKeyColumn primaryKeyColumn;
            if (!primaryKey.PrimaryKeyColumns.TryGetValue(objectName, out primaryKeyColumn))
                return false;

            primaryKey.PrimaryKeyColumns.Remove(objectName);
            primaryKeyColumn.PrimaryKey = null;
            primaryKeyColumn.ObjectName = newObjectName;
            primaryKeyColumn.PrimaryKey = primaryKey;
            primaryKey.PrimaryKeyColumns.Add(newObjectName, primaryKeyColumn);

            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="primaryKey">The primary key to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static PrimaryKey ShallowClone(PrimaryKey primaryKey)
        {
            return new PrimaryKey(primaryKey.ObjectName)
                {
                    AllowPageLocks = primaryKey.AllowPageLocks,
                    AllowRowLocks = primaryKey.AllowRowLocks,
                    Description = primaryKey.Description,
                    FileGroup = primaryKey.FileGroup,
                    FillFactor = primaryKey.FillFactor,
                    IgnoreDupKey = primaryKey.IgnoreDupKey,
                    IndexType = primaryKey.IndexType,
                    IsClustered = primaryKey.IsClustered,
                    IsDisabled = primaryKey.IsDisabled,
                    IsPadded = primaryKey.IsPadded
                };
        }

        public static string ToJson(PrimaryKey primaryKey, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(primaryKey, formatting);
        }

        /// <summary>
        /// Modifies the source PrimaryKey to contain all objects that are
        /// present in both iteself and the target PrimaryKey.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourcePrimaryKey">The source PrimaryKey.</param>
        /// <param name="targetPrimaryKey">The target PrimaryKey.</param>
        /// <param name="dataComparisonType">The completeness of comparisons between matching objects.</param>
        /// <returns></returns>
        public static void UnionWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingPrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addablePrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingPrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
            matchingPrimaryKeyColumns.IntersectWith(targetPrimaryKey.PrimaryKeyColumns.Keys);

            addablePrimaryKeyColumns.UnionWith(targetPrimaryKey.PrimaryKeyColumns.Keys);
            addablePrimaryKeyColumns.ExceptWith(matchingPrimaryKeyColumns);

            foreach (var primaryKeyColumn in addablePrimaryKeyColumns)
            {
                PrimaryKeyColumn targetPrimaryKeyColumn;
                if (!targetPrimaryKey.PrimaryKeyColumns.TryGetValue(primaryKeyColumn, out targetPrimaryKeyColumn))
                    continue;

                AddPrimaryKeyColumn(sourcePrimaryKey, PrimaryKeyColumn.Clone(targetPrimaryKeyColumn));
            }
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(PrimaryKey primaryKey, UserTable userTable, string objectName, PrimaryKeysRow primaryKeysRow)
        {
            primaryKey.PrimaryKeyColumns = new Dictionary<string, PrimaryKeyColumn>(StringComparer.OrdinalIgnoreCase);
            primaryKey.UserTable = userTable;
            primaryKey._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            primaryKey.Description = "Primary Key";

            if (primaryKeysRow == null)
            {
                primaryKey.AllowPageLocks = true;
                primaryKey.AllowRowLocks = true;
                primaryKey.FileGroup = "PRIMARY";
                primaryKey.FillFactor = 0;
                primaryKey.IgnoreDupKey = false;
                primaryKey.IndexType = "";
                primaryKey.IsClustered = false;
                primaryKey.IsDisabled = false;
                primaryKey.IsPadded = true;
                return;
            }

            primaryKey.AllowPageLocks = primaryKeysRow.AllowPageLocks;
            primaryKey.AllowRowLocks = primaryKeysRow.AllowRowLocks;
            primaryKey.FileGroup = primaryKeysRow.FileGroup;
            primaryKey.FillFactor = primaryKeysRow.FillFactor;
            primaryKey.IndexType = primaryKeysRow.IndexType;
            primaryKey.IgnoreDupKey = primaryKeysRow.IgnoreDupKey;
            primaryKey.IsClustered = primaryKeysRow.IsClustered;
            primaryKey.IsDisabled = primaryKeysRow.IsDisabled;
            primaryKey.IsPadded = primaryKeysRow.IsPadded;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
