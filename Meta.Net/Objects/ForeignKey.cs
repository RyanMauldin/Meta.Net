using System;
using System.Linq;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class ForeignKey : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Foreign Key";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public DataObjectLookup<ForeignKey, ForeignKeyColumn> ForeignKeyColumns { get; private set; }

        public UserTable ReferencedUserTable
        {
            get
            {
                if (ForeignKeyColumns.Count == 0)
                    return null;

                var foreignKeyColumn = ForeignKeyColumns.FirstOrDefault();
                return foreignKeyColumn == null
                    ? null
                    : foreignKeyColumn.UserTable;
            }
        }

        public bool IsDisabled { get; set; }
        public bool IsNotForReplication { get; set; }
        public bool IsNotTrusted { get; set; }
        public bool IsSystemNamed { get; set; }
        public int DeleteAction { get; set; }
        public string DeleteActionDescription { get; set; }
        public int UpdateAction { get; set; }
        public string UpdateActionDescription { get; set; }

        public ForeignKey()
        {
            DeleteActionDescription = "NO_ACTION";
            UpdateActionDescription = "NO_ACTION";
            ForeignKeyColumns = new DataObjectLookup<ForeignKey, ForeignKeyColumn>(this);
        }

        public override IMetaObject DeepClone()
        {
            var foreignKey = new ForeignKey
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName),
                IsDisabled = IsDisabled,
                IsNotForReplication = IsNotForReplication,
                IsNotTrusted = IsNotTrusted,
                IsSystemNamed = IsSystemNamed,
                DeleteAction = DeleteAction,
                DeleteActionDescription = DeleteActionDescription == null ? null : string.Copy(DeleteActionDescription),
                UpdateAction = UpdateAction,
                UpdateActionDescription = UpdateActionDescription == null ? null : string.Copy(UpdateActionDescription)
            };

            foreignKey.ForeignKeyColumns.DeepClone(foreignKey);

            return foreignKey;
        }

        public override IMetaObject ShallowClone()
        {
            return new ForeignKey
            {
                ObjectName = ObjectName,
                IsDisabled = IsDisabled,
                IsNotForReplication = IsNotForReplication,
                IsNotTrusted = IsNotTrusted,
                IsSystemNamed = IsSystemNamed,
                DeleteAction = DeleteAction,
                DeleteActionDescription = DeleteActionDescription,
                UpdateAction = UpdateAction,
                UpdateActionDescription = UpdateActionDescription
            };
        }

		/// <summary>
        /// Shallow Clear...
        /// </summary>
        /// <param name="foreignKey">The foreignKey to shallow clear.</param>
        public static void Clear(ForeignKey foreignKey)
        {
            foreignKey.ForeignKeyColumns.Clear();
        }

        public static void AddForeignKeyColumn(ForeignKey foreignKey, ForeignKeyColumn foreignKeyColumn)
        {
            if (foreignKeyColumn.ForeignKey != null && foreignKeyColumn.ForeignKey.Equals(foreignKey))
                RemoveForeignKeyColumn(foreignKeyColumn.ForeignKey, foreignKeyColumn);

            if (foreignKey.ForeignKeyColumns.Count > 0)
            {
                var firstForeignKeyColumn = foreignKey.ForeignKeyColumns.First();
                if (!firstForeignKeyColumn.ReferencedUserTable.Equals(foreignKeyColumn.ReferencedUserTable))
                throw new Exception(string.Format("{0} {1} does not reference the same {2} as {3} {4}.",
                    foreignKeyColumn.Description, foreignKeyColumn.Namespace, foreignKeyColumn.ReferencedUserTable.Description,
                    firstForeignKeyColumn.Description, firstForeignKeyColumn.Namespace));
            }
            
            foreignKey.ForeignKeyColumns.Add(foreignKeyColumn);
        }

        public static void RemoveForeignKeyColumn(ForeignKey foreignKey, string objectNamesapce)
        {
            foreignKey.ForeignKeyColumns.Remove(objectNamesapce);
        }

        public static void RemoveForeignKeyColumn(ForeignKey foreignKey, ForeignKeyColumn foreignKeyColumn)
        {
            foreignKey.ForeignKeyColumns.Remove(foreignKeyColumn.Namespace);
        }

        public static void RenameForeignKeyColumn(ForeignKey foreignKey, string objectNamespace, string newObjectName)
        {
            var foreignKeyColumn = foreignKey.ForeignKeyColumns[objectNamespace];
            if (foreignKeyColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, foreignKey.Description, foreignKey.Namespace, newObjectName));

            foreignKey.ForeignKeyColumns.Rename(foreignKeyColumn, newObjectName);
        }

        public static long ObjectCount(ForeignKey foreignKey)
        {
            return foreignKey.ForeignKeyColumns.Count;
        }

        //public static bool CompareDefinitions(ForeignKey sourceForeignKey, ForeignKey targetForeignKey)
        //{
        //    if (!CompareObjectNames(sourceForeignKey, targetForeignKey))
        //        return false;

        //    if (sourceForeignKey.IsDisabled != targetForeignKey.IsDisabled)
        //        return false;

        //    if (sourceForeignKey.IsNotForReplication != targetForeignKey.IsNotForReplication)
        //        return false;

        //    if (sourceForeignKey.IsNotTrusted != targetForeignKey.IsNotTrusted)
        //        return false;

        //    if (sourceForeignKey.IsSystemNamed != targetForeignKey.IsSystemNamed)
        //        return false;

        //    if (sourceForeignKey.DeleteAction != targetForeignKey.DeleteAction)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceForeignKey.DeleteActionDescription, targetForeignKey.DeleteActionDescription) != 0)
        //        return false;

        //    if (sourceForeignKey.UpdateAction != targetForeignKey.UpdateAction)
        //        return false;

        //    return StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceForeignKey.UpdateActionDescription, targetForeignKey.UpdateActionDescription) == 0;
        //}

        //public static bool CompareObjectNames(ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
        //    StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target ForeignKey from the source ForeignKey.
        ///// </summary>
        ///// <param name="sourceForeignKey">The source ForeignKey.</param>
        ///// <param name="targetForeignKey">The target ForeignKey.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
        //    matchingForeignKeyColumns.IntersectWith(targetForeignKey.ForeignKeyColumns.Keys);

        //    foreach (var foreignKeyColumn in matchingForeignKeyColumns)
        //    {
        //        var sourceForeignKeyColumn = sourceForeignKey.ForeignKeyColumns[foreignKeyColumn];
        //        if (sourceForeignKeyColumn == null)
        //            continue;

        //        var targetForeignKeyColumn = targetForeignKey.ForeignKeyColumns[foreignKeyColumn];
        //        if (targetForeignKeyColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (ForeignKeyColumn.CompareDefinitions(sourceForeignKeyColumn, targetForeignKeyColumn))
        //                    RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (ForeignKeyColumn.CompareObjectNames(sourceForeignKeyColumn, targetForeignKeyColumn))
        //                    RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    ForeignKey foreignKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreateForeignKey(sourceDataContext, targetDataContext, foreignKey);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    ForeignKey foreignKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.DropForeignKey(sourceDataContext, targetDataContext, foreignKey);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        ///// <summary>
        ///// Modifies this ForeignKey to contain only objects that are
        ///// present in this ForeignKey and in the specified ForeignKey.
        ///// </summary>
        ///// <param name="sourceForeignKey">The source ForeignKey.</param>
        ///// <param name="targetForeignKey">The target ForeignKey.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
        //    matchingForeignKeyColumns.IntersectWith(targetForeignKey.ForeignKeyColumns.Keys);

        //    removableForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
        //    removableForeignKeyColumns.ExceptWith(matchingForeignKeyColumns);

        //    foreach (var foreignKeyColumn in removableForeignKeyColumns)
        //        RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);

        //    foreach (var foreignKeyColumn in matchingForeignKeyColumns)
        //    {
        //        var sourceForeignKeyColumn = sourceForeignKey.ForeignKeyColumns[foreignKeyColumn];
        //        if (sourceForeignKeyColumn == null)
        //            continue;

        //        var targetForeignKeyColumn = targetForeignKey.ForeignKeyColumns[foreignKeyColumn];
        //        if (targetForeignKeyColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!ForeignKeyColumn.CompareDefinitions(sourceForeignKeyColumn, targetForeignKeyColumn))
        //                    RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!ForeignKeyColumn.CompareObjectNames(sourceForeignKeyColumn, targetForeignKeyColumn))
        //                    RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        ///// <summary>
        ///// Modifies this ForeignKey to contain all objects that are
        ///// present in both iteself and the specified ForeignKey.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceForeignKey">The source ForeignKey.</param>
        ///// <param name="targetForeignKey">The target ForeignKey.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
        //    matchingForeignKeyColumns.IntersectWith(targetForeignKey.ForeignKeyColumns.Keys);

        //    addableForeignKeyColumns.UnionWith(targetForeignKey.ForeignKeyColumns.Keys);
        //    addableForeignKeyColumns.ExceptWith(matchingForeignKeyColumns);

        //    foreach (var foreignKeyColumn in addableForeignKeyColumns)
        //    {
        //        var targetForeignKeyColumn = targetForeignKey.ForeignKeyColumns[foreignKeyColumn];
        //        if (targetForeignKeyColumn == null)
        //            continue;

        //        AddForeignKeyColumn(sourceForeignKey, ForeignKeyColumn.Clone(targetForeignKeyColumn));
        //    }
        //}

        //public ForeignKey(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UserTable = null;
        //    ReferencedUserTable = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    DeleteAction = info.GetInt32("DeleteAction");
        //    DeleteActionDescription = info.GetString("DeleteActionDescription");
        //    IsDisabled = info.GetBoolean("IsDisabled");
        //    IsNotForReplication = info.GetBoolean("IsNotForReplication");
        //    IsNotTrusted = info.GetBoolean("IsNotTrusted");
        //    IsSystemNamed = info.GetBoolean("IsSystemNamed");
        //    //ReferencedObjectName = info.GetString("ReferencedObjectName");
        //    //ReferencedSchemaName = info.GetString("ReferencedSchemaName");
        //    //ReferencedTableName = info.GetString("ReferencedTableName");
        //    UpdateAction = info.GetInt32("UpdateAction");
        //    UpdateActionDescription = info.GetString("UpdateActionDescription");

        //    // Deserialize Foreign Key Columns
        //    var foreignKeyColumns = info.GetInt32("ForeignKeyColumns");
        //    ForeignKeyColumns = new Dictionary<string, ForeignKeyColumn>(StringComparer.OrdinalIgnoreCase);

        //    for (var i = 0; i < foreignKeyColumns; i++)
        //    {
        //        var foreignKeyColumn = (ForeignKeyColumn)info.GetValue("ForeignKeyColumn" + i, typeof(ForeignKeyColumn));
        //        foreignKeyColumn.ForeignKey = this;
        //        ForeignKeyColumns.Add(foreignKeyColumn.ObjectName, foreignKeyColumn);
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
        //    info.AddValue("DeleteAction", DeleteAction);
        //    info.AddValue("DeleteActionDescription", DeleteActionDescription);
        //    info.AddValue("IsDisabled", IsDisabled);
        //    info.AddValue("IsNotForReplication", IsNotForReplication);
        //    info.AddValue("IsNotTrusted", IsNotTrusted);
        //    info.AddValue("IsSystemNamed", IsSystemNamed);
        //    //info.AddValue("ReferencedObjectName", ReferencedObjectName);
        //    //info.AddValue("ReferencedSchemaName", ReferencedSchemaName);
        //    //info.AddValue("ReferencedTableName", ReferencedTableName);
        //    //TODO: Serialization work
        //    info.AddValue("UpdateAction", UpdateAction);
        //    info.AddValue("UpdateActionDescription", UpdateActionDescription);
        //    info.AddValue("ForeignKeyColumns", ForeignKeyColumns);
        //}

        //public static ForeignKey FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<ForeignKey>(json);
        //}

        //public static string ToJson(ForeignKey foreignKey, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(foreignKey, formatting);
        //}
    }
}
