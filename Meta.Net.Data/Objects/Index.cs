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
    public class Index : IDataObject, IDataUserTableBasedObject
    {
		#region Properties (17) 

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

        public Dictionary<string, IndexColumn> IndexColumns { get; private set; }

        public string IndexType { get; set; }

        public bool IsClustered { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsPadded { get; set; }

        public bool IsUnique { get; set; }

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
                    if (UserTable.RenameIndex(UserTable, _objectName, value))
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

        public Index(SerializationInfo info, StreamingContext context)
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
            IsUnique = info.GetBoolean("IsUnique");

            // Deserialize Unique-Constraint Columns
            var indexColumns = info.GetInt32("IndexColumns");
            IndexColumns = new Dictionary<string, IndexColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < indexColumns; i++)
            {
                var indexColumn = (IndexColumn)info.GetValue("IndexColumn" + i, typeof(IndexColumn));
                indexColumn.Index = this;
                IndexColumns.Add(indexColumn.ObjectName, indexColumn);
            }
        }

        public Index(UserTable userTable, IndexesRow indexesRow)
        {
            Init(this, userTable, indexesRow.ObjectName, indexesRow);
        }

        public Index(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public Index(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public Index()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (21) 

		#region Public Methods (20) 

        public static bool AddIndexColumn(Index index, IndexColumn indexColumn)
        {
            if (index.IndexColumns.ContainsKey(indexColumn.ObjectName))
                return false;

            if (indexColumn.Index == null)
            {
                indexColumn.Index = index;
                index.IndexColumns.Add(indexColumn.ObjectName, indexColumn);
                return true;
            }

            if (indexColumn.Index.Equals(index))
            {
                index.IndexColumns.Add(indexColumn.ObjectName, indexColumn);
                return true;
            }

            return false;
        }

        public static bool AddIndexColumn(Index index, string objectName)
        {
            if (string.IsNullOrEmpty(objectName)) return false;
            if (index.IndexColumns.ContainsKey(objectName)) return false;

            var uniqueConstraintColumn = new IndexColumn(index, objectName);
            index.IndexColumns.Add(objectName, uniqueConstraintColumn);

            return true;
        }

        /// <summary>
        /// Shallow Clear...
        /// </summary>
        /// <param name="index">The index to shallow clear.</param>
        public static void Clear(Index index)
        {
            index.IndexColumns.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="index">The index to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static Index Clone(Index index)
        {
            var indexClone = new Index(index.ObjectName)
            {
                AllowPageLocks = index.AllowPageLocks,
                AllowRowLocks = index.AllowRowLocks,
                Description = index.Description,
                FileGroup = index.FileGroup,
                FillFactor = index.FillFactor,
                IgnoreDupKey = index.IgnoreDupKey,
                IndexType = index.IndexType,
                IsClustered = index.IsClustered,
                IsDisabled = index.IsDisabled,
                IsPadded = index.IsPadded,
                IsUnique = index.IsUnique
            };

            foreach (var indexColumn in index.IndexColumns.Values)
                AddIndexColumn(indexClone, IndexColumn.Clone(indexColumn));

            return indexClone;
        }

        public static bool CompareDefinitions(Index sourceIndex, Index targetIndex)
        {
            if (!CompareObjectNames(sourceIndex, targetIndex))
                return false;

            if (sourceIndex.AllowPageLocks != targetIndex.AllowPageLocks)
                return false;

            if (sourceIndex.AllowRowLocks != targetIndex.AllowRowLocks)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceIndex.FileGroup, targetIndex.FileGroup) != 0)
                return false;

            if (sourceIndex.FillFactor != targetIndex.FillFactor)
                return false;

            if (sourceIndex.IgnoreDupKey != targetIndex.IgnoreDupKey)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceIndex.IndexType, targetIndex.IndexType) != 0)
                return false;

            if (sourceIndex.IsClustered != targetIndex.IsClustered)
                return false;

            if (sourceIndex.IsDisabled != targetIndex.IsDisabled)
                return false;

            if (sourceIndex.IsPadded != targetIndex.IsPadded)
                return false;

            return sourceIndex.IsUnique == targetIndex.IsUnique;
        }

        public static bool CompareObjectNames(Index sourceIndex, Index targetIndex,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target Index from the source Index.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndex">The source Index.</param>
        /// <param name="targetIndex">The target Index.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            Index sourceIndex, Index targetIndex,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
            matchingIndexColumns.IntersectWith(targetIndex.IndexColumns.Keys);

            foreach (var indexColumn in matchingIndexColumns)
            {
                IndexColumn sourceIndexColumn;
                if (!sourceIndex.IndexColumns.TryGetValue(indexColumn, out sourceIndexColumn))
                    continue;

                IndexColumn targetIndexColumn;
                if (!targetIndex.IndexColumns.TryGetValue(indexColumn, out targetIndexColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (IndexColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn))
                            RemoveIndexColumn(sourceIndex, indexColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (IndexColumn.CompareObjectNames(sourceIndexColumn, targetIndexColumn))
                            RemoveIndexColumn(sourceIndex, indexColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static void Fill(Index index, DataGenerics generics)
        {
            Clear(index);
            var predicate = new StringPredicate(index.Namespace + ".", StringComparison.OrdinalIgnoreCase);

            var indexes = generics.Indexes.FindAll(predicate.StartsWith);
            foreach (var str in indexes)
            {
                IndexesRow indexesRow;
                if (!generics.IndexRows.TryGetValue(str + ".", out indexesRow))
                    continue;

                var indexColumn = new IndexColumn(index, indexesRow);
                AddIndexColumn(index, indexColumn);
            }
        }

        public static Index FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Index>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            Index index, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreateIndex(sourceDataContext, targetDataContext, index);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            Index index, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.DropIndex(sourceDataContext, targetDataContext, index);
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
            info.AddValue("IsUnique", IsUnique);

            // Serialize Index Columns
            info.AddValue("IndexColumns", IndexColumns.Count);

            var i = 0;
            foreach (var indexColumn in IndexColumns.Values)
                info.AddValue("IndexColumn" + i++, indexColumn);
        }

        /// <summary>
        /// Modifies this Index to contain only objects that are
        /// present in this Index and in the specified Index.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndex">The source Index.</param>
        /// <param name="targetIndex">The target Index.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            Index sourceIndex, Index targetIndex,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
            matchingIndexColumns.IntersectWith(targetIndex.IndexColumns.Keys);

            removableIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
            removableIndexColumns.ExceptWith(matchingIndexColumns);

            foreach (var indexColumn in removableIndexColumns)
                RemoveIndexColumn(sourceIndex, indexColumn);

            foreach (var indexColumn in matchingIndexColumns)
            {
                IndexColumn sourceIndexColumn;
                if (!sourceIndex.IndexColumns.TryGetValue(indexColumn, out sourceIndexColumn))
                    continue;

                IndexColumn targetIndexColumn;
                if (!targetIndex.IndexColumns.TryGetValue(indexColumn, out targetIndexColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!IndexColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn))
                            RemoveIndexColumn(sourceIndex, indexColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!IndexColumn.CompareObjectNames(sourceIndexColumn, targetIndexColumn))
                            RemoveIndexColumn(sourceIndex, indexColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static long ObjectCount(Index index)
        {
            return index.IndexColumns.Count;
        }

        public static bool RemoveIndexColumn(Index index, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && index.IndexColumns.Remove(objectName);
        }

        public static bool RemoveIndexColumn(Index index, IndexColumn indexColumn)
        {
            IndexColumn indexColumnObject;
            if (!index.IndexColumns.TryGetValue(indexColumn.ObjectName, out indexColumnObject))
                return false;

            return indexColumn.Equals(indexColumnObject) &&
                   index.IndexColumns.Remove(indexColumn.ObjectName);
        }

        public static bool RenameIndexColumn(Index index, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (index.IndexColumns.ContainsKey(newObjectName))
                return false;

            IndexColumn indexColumn;
            if (!index.IndexColumns.TryGetValue(objectName, out indexColumn))
                return false;

            index.IndexColumns.Remove(objectName);
            indexColumn.Index = null;
            indexColumn.ObjectName = newObjectName;
            indexColumn.Index = index;
            index.IndexColumns.Add(newObjectName, indexColumn);

            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="index">The index to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static Index ShallowClone(Index index)
        {
            return new Index(index.ObjectName)
                {
                    AllowPageLocks = index.AllowPageLocks,
                    AllowRowLocks = index.AllowRowLocks,
                    Description = index.Description,
                    FileGroup = index.FileGroup,
                    FillFactor = index.FillFactor,
                    IgnoreDupKey = index.IgnoreDupKey,
                    IndexType = index.IndexType,
                    IsClustered = index.IsClustered,
                    IsDisabled = index.IsDisabled,
                    IsPadded = index.IsPadded,
                    IsUnique = index.IsUnique
                };
        }

        public static string ToJson(Index index, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(index, formatting);
        }

        /// <summary>
        /// Modifies this Index to contain all objects that are
        /// present in both iteself and the specified Index.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndex">The source Index.</param>
        /// <param name="targetIndex">The target Index.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            Index sourceIndex, Index targetIndex,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
            matchingIndexColumns.IntersectWith(targetIndex.IndexColumns.Keys);

            addableIndexColumns.UnionWith(targetIndex.IndexColumns.Keys);
            addableIndexColumns.ExceptWith(matchingIndexColumns);

            foreach (var indexColumn in addableIndexColumns)
            {
                IndexColumn targetIndexColumn;
                if (!targetIndex.IndexColumns.TryGetValue(indexColumn, out targetIndexColumn))
                    continue;

                AddIndexColumn(sourceIndex, IndexColumn.Clone(targetIndexColumn));
            }
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(Index index, UserTable userTable, string objectName, IndexesRow indexesRow)
        {
            index.IndexColumns = new Dictionary<string, IndexColumn>(StringComparer.OrdinalIgnoreCase);
            index.UserTable = userTable;
            index._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            index.Description = "Index";
            
            if (indexesRow == null)
            {
                index.AllowPageLocks = true;
                index.AllowRowLocks = true;
                index.FileGroup = "PRIMARY";
                index.FillFactor = 0;
                index.IgnoreDupKey = false;
                index.IndexType = "";
                index.IsClustered = false;
                index.IsDisabled = false;
                index.IsPadded = true;
                index.IsUnique = false;
                return;
            }

            index.AllowPageLocks = indexesRow.AllowPageLocks;
            index.AllowRowLocks = indexesRow.AllowRowLocks;
            index.FileGroup = indexesRow.FileGroup;
            index.FillFactor = indexesRow.FillFactor;
            index.IgnoreDupKey = indexesRow.IgnoreDupKey;
            index.IndexType = indexesRow.IndexType;
            index.IsClustered = indexesRow.IsClustered;
            index.IsDisabled = indexesRow.IsDisabled;
            index.IsPadded = indexesRow.IsPadded;
            index.IsUnique = indexesRow.IsUnique;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
