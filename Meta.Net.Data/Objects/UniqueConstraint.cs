using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class UniqueConstraint : IDataObject, IDataUserTableBasedObject
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

        public Dictionary<string, UniqueConstraintColumn> UniqueConstraintColumns { get; private set; }

        public UserTable UserTable { get; set; }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public UniqueConstraint(SerializationInfo info, StreamingContext context)
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

            // Deserialize Unique-Constraint Columns
            var uniqueConstraintColumns = info.GetInt32("UniqueConstraintColumns");
            UniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < uniqueConstraintColumns; i++)
            {
                var uniqueConstraintColumn = (UniqueConstraintColumn)info.GetValue("UniqueConstraintColumn" + i, typeof(UniqueConstraintColumn));
                uniqueConstraintColumn.UniqueConstraint = this;
                UniqueConstraintColumns.Add(uniqueConstraintColumn.ObjectName, uniqueConstraintColumn);
            }
        }

        public UniqueConstraint(UserTable userTable, UniqueConstraintsRow uniqueConstraintsRow)
        {
            Init(this, userTable, uniqueConstraintsRow.ObjectName, uniqueConstraintsRow);
        }

        public UniqueConstraint(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public UniqueConstraint(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public UniqueConstraint()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (21) 

		#region Public Methods (20) 

        public static bool AddUniqueConstraintColumn(UniqueConstraint uniqueConstraint, UniqueConstraintColumn uniqueConstraintColumn)
        {
            if (uniqueConstraint.UniqueConstraintColumns.ContainsKey(uniqueConstraintColumn.ObjectName))
                return false;

            if (uniqueConstraintColumn.UniqueConstraint == null)
            {
                uniqueConstraintColumn.UniqueConstraint = uniqueConstraint;
                uniqueConstraint.UniqueConstraintColumns.Add(uniqueConstraintColumn.ObjectName, uniqueConstraintColumn);
                return true;
            }

            if (uniqueConstraintColumn.UniqueConstraint.Equals(uniqueConstraint))
            {
                uniqueConstraint.UniqueConstraintColumns.Add(uniqueConstraintColumn.ObjectName, uniqueConstraintColumn);
                return true;
            }

            return false;
        }

        public static bool AddUniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (uniqueConstraint.UniqueConstraintColumns.ContainsKey(objectName))
                return false;

            var uniqueConstraintColumn = new UniqueConstraintColumn(uniqueConstraint, objectName);
            uniqueConstraint.UniqueConstraintColumns.Add(objectName, uniqueConstraintColumn);

            return true;
        }

        /// <summary>
        /// Shallow Clear...
        /// </summary>
        /// <param name="uniqueConstraint">The unique contraint to shallow clear.</param>
        public static void Clear(UniqueConstraint uniqueConstraint)
        {
            uniqueConstraint.UniqueConstraintColumns.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="uniqueConstraint">The unique constraint to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static UniqueConstraint Clone(UniqueConstraint uniqueConstraint)
        {
            var uniqueConstraintClone = new UniqueConstraint(uniqueConstraint.ObjectName)
            {
                AllowPageLocks = uniqueConstraint.AllowPageLocks,
                AllowRowLocks = uniqueConstraint.AllowRowLocks,
                Description = uniqueConstraint.Description,
                FileGroup = uniqueConstraint.FileGroup,
                FillFactor = uniqueConstraint.FillFactor,
                IgnoreDupKey = uniqueConstraint.IgnoreDupKey,
                IndexType = uniqueConstraint.IndexType,
                IsClustered = uniqueConstraint.IsClustered,
                IsDisabled = uniqueConstraint.IsDisabled,
                IsPadded = uniqueConstraint.IsPadded
            };

            foreach (var uniqueConstraintColumn in uniqueConstraint.UniqueConstraintColumns.Values)
                AddUniqueConstraintColumn(uniqueConstraintClone, UniqueConstraintColumn.Clone(uniqueConstraintColumn));

            return uniqueConstraintClone;
        }

        public static bool CompareDefinitions(UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint)
        {
            if (!CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
                return false;

            if (sourceUniqueConstraint.AllowPageLocks != targetUniqueConstraint.AllowPageLocks)
                return false;

            if (sourceUniqueConstraint.AllowRowLocks != targetUniqueConstraint.AllowRowLocks)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceUniqueConstraint.FileGroup, targetUniqueConstraint.FileGroup) != 0)
                return false;

            if (sourceUniqueConstraint.FillFactor != targetUniqueConstraint.FillFactor)
                return false;

            if (sourceUniqueConstraint.IgnoreDupKey != targetUniqueConstraint.IgnoreDupKey)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceUniqueConstraint.IndexType, targetUniqueConstraint.IndexType) != 0)
                return false;

            if (sourceUniqueConstraint.IsClustered != targetUniqueConstraint.IsClustered)
                return false;

            if (sourceUniqueConstraint.IsDisabled != targetUniqueConstraint.IsDisabled)
                return false;

            return sourceUniqueConstraint.IsPadded == targetUniqueConstraint.IsPadded;
        }

        public static bool CompareObjectNames(UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target UniqueConstraint from the source UniqueConstraint.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUniqueConstraint">The source UniqueConstraint.</param>
        /// <param name="targetUniqueConstraint">The target UniqueConstraint.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
            matchingUniqueConstraintColumns.IntersectWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);

            foreach (var uniqueConstraintColumn in matchingUniqueConstraintColumns)
            {
                UniqueConstraintColumn sourceUniqueConstraintColumn;
                if (!sourceUniqueConstraint.UniqueConstraintColumns.TryGetValue(uniqueConstraintColumn, out sourceUniqueConstraintColumn))
                    continue;

                UniqueConstraintColumn targetUniqueConstraintColumn;
                if (!targetUniqueConstraint.UniqueConstraintColumns.TryGetValue(uniqueConstraintColumn, out targetUniqueConstraintColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UniqueConstraintColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
                            RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (UniqueConstraintColumn.CompareObjectNames(sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
                            RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static void Fill(UniqueConstraint uniqueConstraint, DataGenerics generics)
        {
            Clear(uniqueConstraint);
            var predicate = new StringPredicate(uniqueConstraint.Namespace + ".", StringComparison.OrdinalIgnoreCase);

            var uniqueConstraints = generics.UniqueConstraints.FindAll(predicate.StartsWith);
            foreach (var str in uniqueConstraints)
            {
                UniqueConstraintsRow uniqueConstraintsRow;
                if (!generics.UniqueConstraintRows.TryGetValue(str + ".", out uniqueConstraintsRow))
                    continue;

                var uniqueConstraintColumn = new UniqueConstraintColumn(uniqueConstraint, uniqueConstraintsRow);
                AddUniqueConstraintColumn(uniqueConstraint, uniqueConstraintColumn);
            }
        }

        public static UniqueConstraint FromJson(string json)
        {
            return JsonConvert.DeserializeObject<UniqueConstraint>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UniqueConstraint uniqueConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreateUniqueConstraint(sourceDataContext, targetDataContext, uniqueConstraint);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UniqueConstraint uniqueConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.DropUniqueConstraint(sourceDataContext, targetDataContext, uniqueConstraint);
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

            // Serialize Unique Constraint Columns
            info.AddValue("UniqueConstraintColumns", UniqueConstraintColumns.Count);

            var i = 0;
            foreach (var uniqueConstraintColumn in UniqueConstraintColumns.Values)
                info.AddValue("UniqueConstraintColumn" + i++, uniqueConstraintColumn);
        }

        /// <summary>
        /// Modifies the source UniqueConstraint to contain only objects that are
        /// present in the source UniqueConstraint and in the target UniqueConstraint.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUniqueConstraint">The source UniqueConstraint.</param>
        /// <param name="targetUniqueConstraint">The target UniqueConstraint.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
            matchingUniqueConstraintColumns.IntersectWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);

            removableUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
            removableUniqueConstraintColumns.ExceptWith(matchingUniqueConstraintColumns);

            foreach (var uniqueConstraintColumn in removableUniqueConstraintColumns)
                RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);

            foreach (var uniqueConstraintColumn in matchingUniqueConstraintColumns)
            {
                UniqueConstraintColumn sourceUniqueConstraintColumn;
                if (!sourceUniqueConstraint.UniqueConstraintColumns.TryGetValue(uniqueConstraintColumn, out sourceUniqueConstraintColumn))
                    continue;

                UniqueConstraintColumn targetUniqueConstraintColumn;
                if (!targetUniqueConstraint.UniqueConstraintColumns.TryGetValue(uniqueConstraintColumn, out targetUniqueConstraintColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!UniqueConstraintColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
                            RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!UniqueConstraintColumn.CompareObjectNames(sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
                            RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static long ObjectCount(UniqueConstraint uniqueConstraint)
        {
            return uniqueConstraint.UniqueConstraintColumns.Count;
        }

        public static bool RemoveUniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && uniqueConstraint.UniqueConstraintColumns.Remove(objectName);
        }

        public static bool RemoveUniqueConstraintColumn(UniqueConstraint uniqueConstraint, UniqueConstraintColumn uniqueConstraintColumn)
        {
            UniqueConstraintColumn uniqueConstraintColumnObject;
            if (!uniqueConstraint.UniqueConstraintColumns.TryGetValue(uniqueConstraintColumn.ObjectName, out uniqueConstraintColumnObject))
                return false;

            return uniqueConstraintColumn.Equals(uniqueConstraintColumnObject) &&
                   uniqueConstraint.UniqueConstraintColumns.Remove(uniqueConstraintColumn.ObjectName);
        }

        public bool RenameUniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (UniqueConstraintColumns.ContainsKey(newObjectName))
                return false;

            UniqueConstraintColumn uniqueConstraintColumn;
            if (!UniqueConstraintColumns.TryGetValue(objectName, out uniqueConstraintColumn))
                return false;

            UniqueConstraintColumns.Remove(objectName);
            uniqueConstraintColumn.UniqueConstraint = null;
            uniqueConstraintColumn.ObjectName = newObjectName;
            uniqueConstraintColumn.UniqueConstraint = this;
            UniqueConstraintColumns.Add(newObjectName, uniqueConstraintColumn);

            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="uniqueConstraint">The unique constraint to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static UniqueConstraint ShallowClone(UniqueConstraint uniqueConstraint)
        {
            return new UniqueConstraint(uniqueConstraint.ObjectName)
                {
                    AllowPageLocks = uniqueConstraint.AllowPageLocks,
                    AllowRowLocks = uniqueConstraint.AllowRowLocks,
                    Description = uniqueConstraint.Description,
                    FileGroup = uniqueConstraint.FileGroup,
                    FillFactor = uniqueConstraint.FillFactor,
                    IgnoreDupKey = uniqueConstraint.IgnoreDupKey,
                    IndexType = uniqueConstraint.IndexType,
                    IsClustered = uniqueConstraint.IsClustered,
                    IsDisabled = uniqueConstraint.IsDisabled,
                    IsPadded = uniqueConstraint.IsPadded
                };
        }

        public static string ToJson(UniqueConstraint uniqueConstraint, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(uniqueConstraint, formatting);
        }

        /// <summary>
        /// Modifies the source UniqueConstraint to contain all objects that are
        /// present in both iteself and the target UniqueConstraint.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUniqueConstraint">The source UniqueConstraint.</param>
        /// <param name="targetUniqueConstraint">The target UniqueConstraint.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
            matchingUniqueConstraintColumns.IntersectWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);

            addableUniqueConstraintColumns.UnionWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);
            addableUniqueConstraintColumns.ExceptWith(matchingUniqueConstraintColumns);

            foreach (var uniqueConstraintColumn in addableUniqueConstraintColumns)
            {
                UniqueConstraintColumn targetUniqueConstraintColumn;
                if (!targetUniqueConstraint.UniqueConstraintColumns.TryGetValue(uniqueConstraintColumn, out targetUniqueConstraintColumn))
                    continue;

                AddUniqueConstraintColumn(sourceUniqueConstraint, UniqueConstraintColumn.Clone(targetUniqueConstraintColumn));
            }
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(UniqueConstraint uniqueConstraint, UserTable userTable, string objectName, UniqueConstraintsRow uniqueConstraintsRow)
        {
            uniqueConstraint.UniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);
            uniqueConstraint.UserTable = userTable;
            uniqueConstraint._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            uniqueConstraint.Description = "Unique Constraint";

            if (uniqueConstraintsRow == null)
            {
                uniqueConstraint.AllowPageLocks = true;
                uniqueConstraint.AllowRowLocks = true;
                uniqueConstraint.FileGroup = "PRIMARY";
                uniqueConstraint.FillFactor = 0;
                uniqueConstraint.IgnoreDupKey = false;
                uniqueConstraint.IndexType = "";
                uniqueConstraint.IsClustered = false;
                uniqueConstraint.IsDisabled = false;
                uniqueConstraint.IsPadded = true;
                return;
            }

            uniqueConstraint.AllowPageLocks = uniqueConstraintsRow.AllowPageLocks;
            uniqueConstraint.AllowRowLocks = uniqueConstraintsRow.AllowRowLocks;
            uniqueConstraint.FileGroup = uniqueConstraintsRow.FileGroup;
            uniqueConstraint.FillFactor = uniqueConstraintsRow.FillFactor;
            uniqueConstraint.IgnoreDupKey = uniqueConstraintsRow.IgnoreDupKey;
            uniqueConstraint.IndexType = uniqueConstraintsRow.IndexType;
            uniqueConstraint.IsClustered = uniqueConstraintsRow.IsClustered;
            uniqueConstraint.IsDisabled = uniqueConstraintsRow.IsDisabled;
            uniqueConstraint.IsPadded = uniqueConstraintsRow.IsPadded;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
