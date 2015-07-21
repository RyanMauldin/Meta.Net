using System;
using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class Index : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Index";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string FileGroup { get; set; }
        [DataMember] public int FillFactor { get; set; }
        [DataMember] public bool IgnoreDupKey { get; set; }
        [DataMember] public bool IsClustered { get; set; }
        [DataMember] public bool IsDisabled { get; set; }
        [DataMember] public bool IsPadded { get; set; }
        [DataMember] public bool IsUnique { get; set; }
        [DataMember] public bool AllowPageLocks { get; set; }
        [DataMember] public bool AllowRowLocks { get; set; }
        [DataMember] public string IndexType { get; set; }

        [DataMember] public DataObjectLookup<Index, IndexColumn> IndexColumns { get; private set; }

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
    }
}
