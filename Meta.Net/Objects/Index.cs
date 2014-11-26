using System;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class Index : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Index";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string FileGroup { get; set; }
        public int FillFactor { get; set; }
        public bool IgnoreDupKey { get; set; }
        public bool IsClustered { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsPadded { get; set; }
        public bool IsUnique { get; set; }
        public bool AllowPageLocks { get; set; }
        public bool AllowRowLocks { get; set; }
        public string IndexType { get; set; }

        public DataObjectLookup<Index, IndexColumn> IndexColumns { get; private set; }

        public Index()
        {
            AllowPageLocks = true;
            AllowRowLocks = true;
            FileGroup = "PRIMARY";
            FillFactor = 0;
            IgnoreDupKey = false;
            IndexType = string.Empty;
            IsClustered = false;
            IsDisabled = false;
            IsPadded = true;
            IsUnique = false;
            IndexColumns = new DataObjectLookup<Index, IndexColumn>(this);
        }

        public override IMetaObject DeepClone()
        {
            var index = new Index
            {
                ObjectName = ObjectName,
                AllowPageLocks = AllowPageLocks,
                AllowRowLocks = AllowRowLocks,
                FileGroup = FileGroup,
                FillFactor = FillFactor,
                IgnoreDupKey = IgnoreDupKey,
                IndexType = IndexType,
                IsClustered = IsClustered,
                IsDisabled = IsDisabled,
                IsPadded = IsPadded,
                IsUnique = IsUnique
            };

            index.IndexColumns.DeepClone(index);

            return index;
        }

        public override IMetaObject ShallowClone()
        {
            return new Index
            {
                ObjectName = ObjectName,
                AllowPageLocks = AllowPageLocks,
                AllowRowLocks = AllowRowLocks,
                FileGroup = FileGroup,
                FillFactor = FillFactor,
                IgnoreDupKey = IgnoreDupKey,
                IndexType = IndexType,
                IsClustered = IsClustered,
                IsDisabled = IsDisabled,
                IsPadded = IsPadded,
                IsUnique = IsUnique
            };
        }

        public static void Clear(Index index)
        {
            index.IndexColumns.Clear();
        }

		public static void AddIndexColumn(Index index, IndexColumn indexColumn)
		{
		    if (indexColumn.Index != null && !indexColumn.Index.Equals(index))
		        RemoveIndexColumn(indexColumn.Index, indexColumn);

            index.IndexColumns.Add(indexColumn);
        }

        public static void RemoveIndexColumn(Index index, string objectNamespace)
        {
            index.IndexColumns.Remove(objectNamespace);
        }

        public static void RemoveIndexColumn(Index index, IndexColumn indexColumn)
        {
            index.IndexColumns.Remove(indexColumn.Namespace);
        }

        public static void RenameIndexColumn(Index index, string objectNamespace, string newObjectName)
        {
            var indexColumn = index.IndexColumns[objectNamespace];
            if (indexColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, index.Description, index.Namespace, newObjectName));

            index.IndexColumns.Rename(indexColumn, newObjectName);
        }

        public static long ObjectCount(Index index)
        {
            return index.IndexColumns.Count;
        }

        //public static bool CompareDefinitions(Index sourceIndex, Index targetIndex)
        //{
        //    if (!CompareObjectNames(sourceIndex, targetIndex))
        //        return false;

        //    if (sourceIndex.AllowPageLocks != targetIndex.AllowPageLocks)
        //        return false;

        //    if (sourceIndex.AllowRowLocks != targetIndex.AllowRowLocks)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceIndex.FileGroup, targetIndex.FileGroup) != 0)
        //        return false;

        //    if (sourceIndex.FillFactor != targetIndex.FillFactor)
        //        return false;

        //    if (sourceIndex.IgnoreDupKey != targetIndex.IgnoreDupKey)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceIndex.IndexType, targetIndex.IndexType) != 0)
        //        return false;

        //    if (sourceIndex.IsClustered != targetIndex.IsClustered)
        //        return false;

        //    if (sourceIndex.IsDisabled != targetIndex.IsDisabled)
        //        return false;

        //    if (sourceIndex.IsPadded != targetIndex.IsPadded)
        //        return false;

        //    return sourceIndex.IsUnique == targetIndex.IsUnique;
        //}

        //public static bool CompareObjectNames(Index sourceIndex, Index targetIndex, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceIndex.ObjectName, targetIndex.ObjectName) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target Index from the source Index.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceIndex">The source Index.</param>
        ///// <param name="targetIndex">The target Index.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Index sourceIndex, Index targetIndex,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
        //    matchingIndexColumns.IntersectWith(targetIndex.IndexColumns.Keys);

        //    foreach (var indexColumn in matchingIndexColumns)
        //    {
        //        var sourceIndexColumn = sourceIndex.IndexColumns[indexColumn];
        //        if (sourceIndexColumn == null)
        //            continue;

        //        var targetIndexColumn = targetIndex.IndexColumns[indexColumn];
        //        if (targetIndexColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (IndexColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn))
        //                    RemoveIndexColumn(sourceIndex, indexColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (IndexColumn.CompareObjectNames(sourceIndexColumn, targetIndexColumn))
        //                    RemoveIndexColumn(sourceIndex, indexColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Index index, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreateIndex(sourceDataContext, targetDataContext, index);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Index index, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.DropIndex(sourceDataContext, targetDataContext, index);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        ///// <summary>
        ///// Modifies this Index to contain only objects that are
        ///// present in this Index and in the specified Index.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceIndex">The source Index.</param>
        ///// <param name="targetIndex">The target Index.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Index sourceIndex, Index targetIndex,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
        //    matchingIndexColumns.IntersectWith(targetIndex.IndexColumns.Keys);

        //    removableIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
        //    removableIndexColumns.ExceptWith(matchingIndexColumns);

        //    foreach (var indexColumn in removableIndexColumns)
        //        RemoveIndexColumn(sourceIndex, indexColumn);

        //    foreach (var indexColumn in matchingIndexColumns)
        //    {
        //        var sourceIndexColumn = sourceIndex.IndexColumns[indexColumn];
        //        if (sourceIndexColumn == null)
        //            continue;

        //        var targetIndexColumn = targetIndex.IndexColumns[indexColumn];
        //        if (targetIndexColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!IndexColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn))
        //                    RemoveIndexColumn(sourceIndex, indexColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!IndexColumn.CompareObjectNames(sourceIndexColumn, targetIndexColumn))
        //                    RemoveIndexColumn(sourceIndex, indexColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Modifies this Index to contain all objects that are
        ///// present in both iteself and the specified Index.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceIndex">The source Index.</param>
        ///// <param name="targetIndex">The target Index.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    Index sourceIndex, Index targetIndex,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableIndexColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIndexColumns.UnionWith(sourceIndex.IndexColumns.Keys);
        //    matchingIndexColumns.IntersectWith(targetIndex.IndexColumns.Keys);

        //    addableIndexColumns.UnionWith(targetIndex.IndexColumns.Keys);
        //    addableIndexColumns.ExceptWith(matchingIndexColumns);

        //    foreach (var indexColumn in addableIndexColumns)
        //    {
        //        var targetIndexColumn = targetIndex.IndexColumns[indexColumn];
        //        if (targetIndexColumn == null)
        //            continue;

        //        AddIndexColumn(sourceIndex, IndexColumn.Clone(targetIndexColumn));
        //    }
        //}

        //public Index(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UserTable = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    AllowPageLocks = info.GetBoolean("AllowPageLocks");
        //    AllowRowLocks = info.GetBoolean("AllowRowLocks");
        //    FileGroup = info.GetString("FileGroup");
        //    FillFactor = info.GetInt32("FillFactor");
        //    IgnoreDupKey = info.GetBoolean("IgnoreDupKey");
        //    IndexType = info.GetString("IndexType");
        //    IsClustered = info.GetBoolean("IsClustered");
        //    IsDisabled = info.GetBoolean("IsDisabled");
        //    IsPadded = info.GetBoolean("IsPadded");
        //    IsUnique = info.GetBoolean("IsUnique");

        //    // Deserialize Unique-Constraint Columns
        //    var indexColumns = info.GetInt32("IndexColumns");
        //    IndexColumns = new Dictionary<string, IndexColumn>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < indexColumns; i++)
        //    {
        //        var indexColumn = (IndexColumn)info.GetValue("IndexColumn" + i, typeof(IndexColumn));
        //        indexColumn.Index = this;
        //        IndexColumns.Add(indexColumn.ObjectName, indexColumn);
        //    }
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("AllowPageLocks", AllowPageLocks);
        //    info.AddValue("AllowRowLocks", AllowRowLocks);
        //    info.AddValue("FileGroup", FileGroup);
        //    info.AddValue("FillFactor", FillFactor);
        //    info.AddValue("IgnoreDupKey", IgnoreDupKey);
        //    info.AddValue("IndexType", IndexType);
        //    info.AddValue("IsClustered", IsClustered);
        //    info.AddValue("IsDisabled", IsDisabled);
        //    info.AddValue("IsPadded", IsPadded);
        //    info.AddValue("IsUnique", IsUnique);

        //    // Serialize Index Columns
        //    info.AddValue("IndexColumns", IndexColumns.Count);

        //    var i = 0;
        //    foreach (var indexColumn in IndexColumns.Values)
        //        info.AddValue("IndexColumn" + i++, indexColumn);
        //}

        //public static Index FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<Index>(json);
        //}

        //public static string ToJson(Index index, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(index, formatting);
        //}
    }
}
