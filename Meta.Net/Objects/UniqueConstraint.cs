using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;
using Meta.Net.Types;

namespace Meta.Net.Objects
{
    [DataContract]
    public class UniqueConstraint : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Unique Constraint";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string FileGroup { get; set; }
        [DataMember] public int FillFactor { get; set; }
        [DataMember] public bool IgnoreDupKey { get; set; }
        [DataMember] public string IndexType { get; set; }
        [DataMember] public bool IsClustered { get; set; }
        [DataMember] public bool IsDisabled { get; set; }
        [DataMember] public bool IsPadded { get; set; }
        [DataMember] public bool AllowPageLocks { get; set; }
        [DataMember] public bool AllowRowLocks { get; set; }

        [DataMember] public DataObjectLookup<UniqueConstraint, UniqueConstraintColumn> UniqueConstraintColumns { get; private set; }

		public UniqueConstraint()
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
            UniqueConstraintColumns = new DataObjectLookup<UniqueConstraint, UniqueConstraintColumn>(this);
        }

		public static void AddUniqueConstraintColumn(UniqueConstraint uniqueConstraint, UniqueConstraintColumn uniqueConstraintColumn)
        {
            if (uniqueConstraintColumn.UniqueConstraint != null && !uniqueConstraintColumn.UniqueConstraint.Equals(uniqueConstraint))
                RemoveUniqueConstraintColumn(uniqueConstraintColumn.UniqueConstraint, uniqueConstraintColumn);

            uniqueConstraint.UniqueConstraintColumns.Add(uniqueConstraintColumn);
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
            var uniqueConstraintClone = new UniqueConstraint
            {
                ObjectName = uniqueConstraint.ObjectName,
                AllowPageLocks = uniqueConstraint.AllowPageLocks,
                AllowRowLocks = uniqueConstraint.AllowRowLocks,
                FileGroup = uniqueConstraint.FileGroup,
                FillFactor = uniqueConstraint.FillFactor,
                IgnoreDupKey = uniqueConstraint.IgnoreDupKey,
                IndexType = uniqueConstraint.IndexType,
                IsClustered = uniqueConstraint.IsClustered,
                IsDisabled = uniqueConstraint.IsDisabled,
                IsPadded = uniqueConstraint.IsPadded
            };

            foreach (var uniqueConstraintColumn in uniqueConstraint.UniqueConstraintColumns)
                AddUniqueConstraintColumn(uniqueConstraintClone, UniqueConstraintColumn.Clone(uniqueConstraintColumn));

            return uniqueConstraintClone;
        }

        //public static bool CompareDefinitions(UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint)
        //{
        //    if (!CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
        //        return false;

        //    if (sourceUniqueConstraint.AllowPageLocks != targetUniqueConstraint.AllowPageLocks)
        //        return false;

        //    if (sourceUniqueConstraint.AllowRowLocks != targetUniqueConstraint.AllowRowLocks)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceUniqueConstraint.FileGroup, targetUniqueConstraint.FileGroup) != 0)
        //        return false;

        //    if (sourceUniqueConstraint.FillFactor != targetUniqueConstraint.FillFactor)
        //        return false;

        //    if (sourceUniqueConstraint.IgnoreDupKey != targetUniqueConstraint.IgnoreDupKey)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceUniqueConstraint.IndexType, targetUniqueConstraint.IndexType) != 0)
        //        return false;

        //    if (sourceUniqueConstraint.IsClustered != targetUniqueConstraint.IsClustered)
        //        return false;

        //    if (sourceUniqueConstraint.IsDisabled != targetUniqueConstraint.IsDisabled)
        //        return false;

        //    return sourceUniqueConstraint.IsPadded == targetUniqueConstraint.IsPadded;
        //}

        //public static bool CompareObjectNames(UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceUniqueConstraint.ObjectName, targetUniqueConstraint.ObjectName) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target UniqueConstraint from the source UniqueConstraint.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceUniqueConstraint">The source UniqueConstraint.</param>
        ///// <param name="targetUniqueConstraint">The target UniqueConstraint.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
        //    matchingUniqueConstraintColumns.IntersectWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);

        //    foreach (var uniqueConstraintColumn in matchingUniqueConstraintColumns)
        //    {
        //        var sourceUniqueConstraintColumn = sourceUniqueConstraint.UniqueConstraintColumns[uniqueConstraintColumn];
        //        if (sourceUniqueConstraintColumn == null)
        //            continue;

        //        var targetUniqueConstraintColumn = targetUniqueConstraint.UniqueConstraintColumns[uniqueConstraintColumn];
        //        if (targetUniqueConstraintColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UniqueConstraintColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
        //                    RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UniqueConstraintColumn.CompareObjectNames(sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
        //                    RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UniqueConstraint uniqueConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreateUniqueConstraint(sourceDataContext, targetDataContext, uniqueConstraint);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UniqueConstraint uniqueConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.DropUniqueConstraint(sourceDataContext, targetDataContext, uniqueConstraint);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        ///// <summary>
        ///// Modifies the source UniqueConstraint to contain only objects that are
        ///// present in the source UniqueConstraint and in the target UniqueConstraint.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceUniqueConstraint">The source UniqueConstraint.</param>
        ///// <param name="targetUniqueConstraint">The target UniqueConstraint.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
        //    matchingUniqueConstraintColumns.IntersectWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);

        //    removableUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
        //    removableUniqueConstraintColumns.ExceptWith(matchingUniqueConstraintColumns);

        //    foreach (var uniqueConstraintColumn in removableUniqueConstraintColumns)
        //        RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);

        //    foreach (var uniqueConstraintColumn in matchingUniqueConstraintColumns)
        //    {
        //        var sourceUniqueConstraintColumn = sourceUniqueConstraint.UniqueConstraintColumns[uniqueConstraintColumn];
        //        if (sourceUniqueConstraintColumn == null)
        //            continue;

        //        var targetUniqueConstraintColumn = targetUniqueConstraint.UniqueConstraintColumns[uniqueConstraintColumn];
        //        if (targetUniqueConstraintColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!UniqueConstraintColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
        //                    RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!UniqueConstraintColumn.CompareObjectNames(sourceUniqueConstraintColumn, targetUniqueConstraintColumn))
        //                    RemoveUniqueConstraintColumn(sourceUniqueConstraint, uniqueConstraintColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        public static long ObjectCount(UniqueConstraint uniqueConstraint)
        {
            return uniqueConstraint.UniqueConstraintColumns.Count;
        }

        public static void RemoveUniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectNamespace)
        {
            uniqueConstraint.UniqueConstraintColumns.Remove(objectNamespace);
        }

        public static void RemoveUniqueConstraintColumn(UniqueConstraint uniqueConstraint, UniqueConstraintColumn uniqueConstraintColumn)
        {
            uniqueConstraint.UniqueConstraintColumns.Remove(uniqueConstraintColumn.Namespace);
        }

        public static void RenameUniqueConstraintColumn(UniqueConstraint uniqueConstraint, string objectNamespace, string newObjectName)
        {
            var uniqueConstraintColumn = uniqueConstraint.UniqueConstraintColumns[objectNamespace];
            if (uniqueConstraintColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, uniqueConstraint.Description, uniqueConstraint.Namespace, newObjectName));

            uniqueConstraint.UniqueConstraintColumns.Rename(uniqueConstraintColumn, newObjectName);
        }

        ///// <summary>
        ///// Modifies the source UniqueConstraint to contain all objects that are
        ///// present in both iteself and the target UniqueConstraint.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceUniqueConstraint">The source UniqueConstraint.</param>
        ///// <param name="targetUniqueConstraint">The target UniqueConstraint.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UniqueConstraint sourceUniqueConstraint, UniqueConstraint targetUniqueConstraint,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableUniqueConstraintColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUniqueConstraintColumns.UnionWith(sourceUniqueConstraint.UniqueConstraintColumns.Keys);
        //    matchingUniqueConstraintColumns.IntersectWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);

        //    addableUniqueConstraintColumns.UnionWith(targetUniqueConstraint.UniqueConstraintColumns.Keys);
        //    addableUniqueConstraintColumns.ExceptWith(matchingUniqueConstraintColumns);

        //    foreach (var uniqueConstraintColumn in addableUniqueConstraintColumns)
        //    {
        //        var targetUniqueConstraintColumn = targetUniqueConstraint.UniqueConstraintColumns[uniqueConstraintColumn];
        //        if (targetUniqueConstraintColumn == null)
        //            continue;

        //        AddUniqueConstraintColumn(sourceUniqueConstraint, UniqueConstraintColumn.Clone(targetUniqueConstraintColumn));
        //    }
        //}
    }
}
