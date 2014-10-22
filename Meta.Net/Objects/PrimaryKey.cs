using System;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class PrimaryKey : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Primary Key";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string FileGroup { get; set; }
        public bool IgnoreDupKey { get; set; }
        public bool IsClustered { get; set; }
        public int FillFactor { get; set; }
        public bool IsPadded { get; set; }
        public bool IsDisabled { get; set; }
        public bool AllowRowLocks { get; set; }
        public bool AllowPageLocks { get; set; }
        public string IndexType { get; set; }
        
        public DataObjectLookup<PrimaryKey, PrimaryKeyColumn> PrimaryKeyColumns { get; private set; }

        private static void Init(PrimaryKey primaryKey, UserTable userTable, string objectName)
        {
            primaryKey.PrimaryKeyColumns = new DataObjectLookup<PrimaryKey, PrimaryKeyColumn>(primaryKey);
            primaryKey.UserTable = userTable;
            primaryKey.ObjectName = GetDefaultObjectName(primaryKey, objectName);
            primaryKey.AllowPageLocks = true;
            primaryKey.AllowRowLocks = true;
            primaryKey.FileGroup = "PRIMARY";
            primaryKey.FillFactor = 0;
            primaryKey.IgnoreDupKey = false;
            primaryKey.IndexType = string.Empty;
            primaryKey.IsClustered = false;
            primaryKey.IsDisabled = false;
            primaryKey.IsPadded = true;
        }

        public PrimaryKey(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName);
        }

        public PrimaryKey()
        {
            PrimaryKeyColumns = new DataObjectLookup<PrimaryKey, PrimaryKeyColumn>(this);
        }

        public static void AddPrimaryKeyColumn(PrimaryKey primaryKey, PrimaryKeyColumn primaryKeyColumn)
        {
            if (primaryKeyColumn.PrimaryKey != null && !primaryKeyColumn.PrimaryKey.Equals(primaryKey))
                RemovePrimaryKeyColumn(primaryKeyColumn.PrimaryKey, primaryKeyColumn);

            primaryKey.PrimaryKeyColumns.Add(primaryKeyColumn);
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
            var primaryKeyClone = new PrimaryKey
            {
                ObjectName = primaryKey.ObjectName,
                AllowPageLocks = primaryKey.AllowPageLocks,
                AllowRowLocks = primaryKey.AllowRowLocks,
                FileGroup = primaryKey.FileGroup,
                FillFactor = primaryKey.FillFactor,
                IgnoreDupKey = primaryKey.IgnoreDupKey,
                IndexType = primaryKey.IndexType,
                IsClustered = primaryKey.IsClustered,
                IsDisabled = primaryKey.IsDisabled,
                IsPadded = primaryKey.IsPadded
            };

            foreach (var primaryKeyColumn in primaryKey.PrimaryKeyColumns)
                AddPrimaryKeyColumn(primaryKeyClone, PrimaryKeyColumn.Clone(primaryKeyColumn));

            return primaryKeyClone;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="primaryKey">The primary key to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static PrimaryKey ShallowClone(PrimaryKey primaryKey)
        {
            return new PrimaryKey
            {
                ObjectName = primaryKey.ObjectName,
                AllowPageLocks = primaryKey.AllowPageLocks,
                AllowRowLocks = primaryKey.AllowRowLocks,
                FileGroup = primaryKey.FileGroup,
                FillFactor = primaryKey.FillFactor,
                IgnoreDupKey = primaryKey.IgnoreDupKey,
                IndexType = primaryKey.IndexType,
                IsClustered = primaryKey.IsClustered,
                IsDisabled = primaryKey.IsDisabled,
                IsPadded = primaryKey.IsPadded
            };
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

        public static long ObjectCount(PrimaryKey primaryKey)
        {
            return primaryKey.PrimaryKeyColumns.Count;
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

        //public PrimaryKey(SerializationInfo info, StreamingContext context)
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

        //    // Deserialize Primary Key Columns
        //    var primaryKeyColumns = info.GetInt32("PrimaryKeyColumns");
        //    PrimaryKeyColumns = new Dictionary<string, PrimaryKeyColumn>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < primaryKeyColumns; i++)
        //    {
        //        var primaryKeyColumn = (PrimaryKeyColumn)info.GetValue("PrimaryKeyColumn" + i, typeof(PrimaryKeyColumn));
        //        primaryKeyColumn.PrimaryKey = this;
        //        PrimaryKeyColumns.Add(primaryKeyColumn.ObjectName, primaryKeyColumn);
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

        //    // Serialize Primary Key Columns
        //    info.AddValue("PrimaryKeyColumns", PrimaryKeyColumns.Count);

        //    var i = 0;
        //    foreach (var primaryKeyColumn in PrimaryKeyColumns.Values)
        //        info.AddValue("PrimaryKeyColumn" + i++, primaryKeyColumn);
        //}

        //public static PrimaryKey FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<PrimaryKey>(json);
        //}

        //public static string ToJson(PrimaryKey primaryKey, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(primaryKey, formatting);
        //}
    }
}
