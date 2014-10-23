using System;
using System.Collections.Generic;
using Meta.Net.Abstract;
using Meta.Net.Types;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class UniqueConstraint : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Unique Constraint";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string FileGroup { get; set; }
        public int FillFactor { get; set; }
        public bool IgnoreDupKey { get; set; }
        public string IndexType { get; set; }
        public bool IsClustered { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsPadded { get; set; }
        public bool AllowPageLocks { get; set; }
        public bool AllowRowLocks { get; set; }

        public DataObjectLookup<UniqueConstraint, UniqueConstraintColumn> UniqueConstraintColumns { get; private set; }

		private static void Init(UniqueConstraint uniqueConstraint, UserTable userTable, string objectName)
        {
            uniqueConstraint.UniqueConstraintColumns = new DataObjectLookup<UniqueConstraint, UniqueConstraintColumn>(uniqueConstraint);
            uniqueConstraint.UserTable = userTable;
            uniqueConstraint.ObjectName = GetDefaultObjectName(uniqueConstraint, objectName);
            uniqueConstraint.AllowPageLocks = true;
            uniqueConstraint.AllowRowLocks = true;
            uniqueConstraint.FileGroup = "PRIMARY";
            uniqueConstraint.FillFactor = 0;
            uniqueConstraint.IgnoreDupKey = false;
            uniqueConstraint.IndexType = "";
            uniqueConstraint.IsClustered = false;
            uniqueConstraint.IsDisabled = false;
            uniqueConstraint.IsPadded = true;
        }

        public UniqueConstraint(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName);
        }

        public UniqueConstraint()
        {
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

        //public static UniqueConstraint FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<UniqueConstraint>(json);
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

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="uniqueConstraint">The unique constraint to shallow clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static UniqueConstraint ShallowClone(UniqueConstraint uniqueConstraint)
        {
            return new UniqueConstraint
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
                var targetUniqueConstraintColumn = targetUniqueConstraint.UniqueConstraintColumns[uniqueConstraintColumn];
                if (targetUniqueConstraintColumn == null)
                    continue;

                AddUniqueConstraintColumn(sourceUniqueConstraint, UniqueConstraintColumn.Clone(targetUniqueConstraintColumn));
            }
        }

		//public UniqueConstraint(SerializationInfo info, StreamingContext context)
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

        //    // Deserialize Unique-Constraint Columns
        //    var uniqueConstraintColumns = info.GetInt32("UniqueConstraintColumns");
        //    UniqueConstraintColumns = new Dictionary<string, UniqueConstraintColumn>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < uniqueConstraintColumns; i++)
        //    {
        //        var uniqueConstraintColumn = (UniqueConstraintColumn)info.GetValue("UniqueConstraintColumn" + i, typeof(UniqueConstraintColumn));
        //        uniqueConstraintColumn.UniqueConstraint = this;
        //        UniqueConstraintColumns.Add(uniqueConstraintColumn.ObjectName, uniqueConstraintColumn);
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

        //    // Serialize Unique Constraint Columns
        //    info.AddValue("UniqueConstraintColumns", UniqueConstraintColumns.Count);

        //    var i = 0;
        //    foreach (var uniqueConstraintColumn in UniqueConstraintColumns.Values)
        //        info.AddValue("UniqueConstraintColumn" + i++, uniqueConstraintColumn);
        //}

        //public static string ToJson(UniqueConstraint uniqueConstraint, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(uniqueConstraint, formatting);
        //}
    }
}
