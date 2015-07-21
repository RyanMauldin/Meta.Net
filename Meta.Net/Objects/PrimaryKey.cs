using System;
using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class PrimaryKey : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Primary Key";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string FileGroup { get; set; }
        [DataMember] public bool IgnoreDupKey { get; set; }
        [DataMember] public bool IsClustered { get; set; }
        [DataMember] public int FillFactor { get; set; }
        [DataMember] public bool IsPadded { get; set; }
        [DataMember] public bool IsDisabled { get; set; }
        [DataMember] public bool AllowRowLocks { get; set; }
        [DataMember] public bool AllowPageLocks { get; set; }
        [DataMember] public string IndexType { get; set; }
        
        [DataMember] public DataObjectLookup<PrimaryKey, PrimaryKeyColumn> PrimaryKeyColumns { get; private set; }

        public PrimaryKey()
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
            PrimaryKeyColumns = new DataObjectLookup<PrimaryKey, PrimaryKeyColumn>(this);
        }

        public static void Clear(PrimaryKey primaryKey)
        {
            primaryKey.PrimaryKeyColumns.Clear();
        }

        public static void AddPrimaryKeyColumn(PrimaryKey primaryKey, PrimaryKeyColumn primaryKeyColumn)
        {
            if (primaryKeyColumn.PrimaryKey != null && !primaryKeyColumn.PrimaryKey.Equals(primaryKey))
                RemovePrimaryKeyColumn(primaryKeyColumn.PrimaryKey, primaryKeyColumn);

            primaryKey.PrimaryKeyColumns.Add(primaryKeyColumn);
        }

        public static void RemovePrimaryKeyColumn(PrimaryKey primaryKey, string objectNamespace)
        {
            primaryKey.PrimaryKeyColumns.Remove(objectNamespace);
        }

        public static void RemovePrimaryKeyColumn(PrimaryKey primaryKey, PrimaryKeyColumn primaryKeyColumn)
        {
            primaryKey.PrimaryKeyColumns.Remove(primaryKeyColumn.Namespace);
        }

        public static void RenamePrimaryKeyColumn(PrimaryKey primaryKey, string objectNamespace, string newObjectName)
        {
            var primaryKeyColumn = primaryKey.PrimaryKeyColumns[objectNamespace];
            if (primaryKeyColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, primaryKey.Description, primaryKey.Namespace, newObjectName));

            primaryKey.PrimaryKeyColumns.Rename(primaryKeyColumn, newObjectName);
        }

        public static long ObjectCount(PrimaryKey primaryKey)
        {
            return primaryKey.PrimaryKeyColumns.Count;
        }

        //public static bool CompareDefinitions(PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey)
        //{
        //    if (!CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
        //        return false;

        //    if (sourcePrimaryKey.AllowPageLocks != targetPrimaryKey.AllowPageLocks)
        //        return false;

        //    if (sourcePrimaryKey.AllowRowLocks != targetPrimaryKey.AllowRowLocks)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourcePrimaryKey.FileGroup, targetPrimaryKey.FileGroup) != 0)
        //        return false;

        //    if (sourcePrimaryKey.FillFactor != targetPrimaryKey.FillFactor)
        //        return false;

        //    if (sourcePrimaryKey.IgnoreDupKey != targetPrimaryKey.IgnoreDupKey)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourcePrimaryKey.IndexType, targetPrimaryKey.IndexType) != 0)
        //        return false;

        //    if (sourcePrimaryKey.IsClustered != targetPrimaryKey.IsClustered)
        //        return false;

        //    if (sourcePrimaryKey.IsDisabled != targetPrimaryKey.IsDisabled)
        //        return false;

        //    return sourcePrimaryKey.IsPadded == targetPrimaryKey.IsPadded;
        //}

        //public static bool CompareObjectNames(PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
        //    StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourcePrimaryKey.ObjectName, targetPrimaryKey.ObjectName) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target PrimaryKey from the source PrimaryKey.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourcePrimaryKey">The source PrimaryKey.</param>
        ///// <param name="targetPrimaryKey">The target PrimaryKey.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingPrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingPrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
        //    matchingPrimaryKeyColumns.IntersectWith(targetPrimaryKey.PrimaryKeyColumns.Keys);

        //    foreach (var primaryKeyColumn in matchingPrimaryKeyColumns)
        //    {
        //        var sourcePrimaryKeyColumn = sourcePrimaryKey.PrimaryKeyColumns[primaryKeyColumn];
        //        if (sourcePrimaryKeyColumn == null)
        //            continue;

        //        var targetPrimaryKeyColumn = targetPrimaryKey.PrimaryKeyColumns[primaryKeyColumn];
        //        if (targetPrimaryKeyColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (PrimaryKeyColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
        //                    RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (PrimaryKeyColumn.CompareObjectNames(sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
        //                    RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    PrimaryKey primaryKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreatePrimaryKey(sourceDataContext, targetDataContext, primaryKey);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    PrimaryKey primaryKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.DropPrimaryKey(sourceDataContext, targetDataContext, primaryKey);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        ///// <summary>
        ///// Modifies the source PrimaryKey to contain only objects that are
        ///// present in the source PrimaryKey and in the target PrimaryKey.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourcePrimaryKey">The source PrimaryKey.</param>
        ///// <param name="targetPrimaryKey">The target PrimaryKey.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingPrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removablePrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingPrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
        //    matchingPrimaryKeyColumns.IntersectWith(targetPrimaryKey.PrimaryKeyColumns.Keys);

        //    removablePrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
        //    removablePrimaryKeyColumns.ExceptWith(matchingPrimaryKeyColumns);

        //    foreach (var primaryKeyColumn in removablePrimaryKeyColumns)
        //        RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);

        //    foreach (var primaryKeyColumn in matchingPrimaryKeyColumns)
        //    {
        //        var sourcePrimaryKeyColumn = sourcePrimaryKey.PrimaryKeyColumns[primaryKeyColumn];
        //        if (sourcePrimaryKeyColumn == null)
        //            continue;

        //        var targetPrimaryKeyColumn = targetPrimaryKey.PrimaryKeyColumns[primaryKeyColumn];
        //        if (targetPrimaryKeyColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!PrimaryKeyColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
        //                    RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!PrimaryKeyColumn.CompareObjectNames(sourcePrimaryKeyColumn, targetPrimaryKeyColumn))
        //                    RemovePrimaryKeyColumn(sourcePrimaryKey, primaryKeyColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Modifies the source PrimaryKey to contain all objects that are
        ///// present in both iteself and the target PrimaryKey.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourcePrimaryKey">The source PrimaryKey.</param>
        ///// <param name="targetPrimaryKey">The target PrimaryKey.</param>
        ///// <param name="dataComparisonType">The completeness of comparisons between matching objects.</param>
        ///// <returns></returns>
        //public static void UnionWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    PrimaryKey sourcePrimaryKey, PrimaryKey targetPrimaryKey,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingPrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addablePrimaryKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingPrimaryKeyColumns.UnionWith(sourcePrimaryKey.PrimaryKeyColumns.Keys);
        //    matchingPrimaryKeyColumns.IntersectWith(targetPrimaryKey.PrimaryKeyColumns.Keys);

        //    addablePrimaryKeyColumns.UnionWith(targetPrimaryKey.PrimaryKeyColumns.Keys);
        //    addablePrimaryKeyColumns.ExceptWith(matchingPrimaryKeyColumns);

        //    foreach (var primaryKeyColumn in addablePrimaryKeyColumns)
        //    {
        //        var targetPrimaryKeyColumn = targetPrimaryKey.PrimaryKeyColumns[primaryKeyColumn];
        //        if (targetPrimaryKeyColumn == null)
        //            continue;

        //        AddPrimaryKeyColumn(sourcePrimaryKey, PrimaryKeyColumn.Clone(targetPrimaryKeyColumn));
        //    }
        //}
    }
}
