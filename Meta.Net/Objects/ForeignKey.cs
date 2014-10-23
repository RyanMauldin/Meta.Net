using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Net.Abstract;
using Meta.Net.Types;
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

        private static void Init(ForeignKey foreignKey, UserTable userTable, string objectName)
        {
            foreignKey.ForeignKeyColumns = new DataObjectLookup<ForeignKey, ForeignKeyColumn>(foreignKey);
            foreignKey.UserTable = userTable;
            foreignKey.ObjectName = GetDefaultObjectName(foreignKey, objectName);
            foreignKey.DeleteActionDescription = "NO_ACTION";
            foreignKey.UpdateActionDescription = "NO_ACTION";
        }

        public ForeignKey(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName);
        }

        public ForeignKey()
        {
            ForeignKeyColumns = new DataObjectLookup<ForeignKey, ForeignKeyColumn>(this);
        }

		public static long ObjectCount(ForeignKey foreignKey)
        {
            return foreignKey.ForeignKeyColumns.Count;
        }

        /// <summary>
        /// Shallow Clear...
        /// </summary>
        /// <param name="foreignKey">The foreignKey to shallow clear.</param>
        public static void Clear(ForeignKey foreignKey)
        {
            foreignKey.ForeignKeyColumns.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="foreignKey">The foreign key to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static ForeignKey Clone(ForeignKey foreignKey)
        {
            var foreignKeyClone = new ForeignKey
            {
                ObjectName = foreignKey.ObjectName,    
                IsDisabled = foreignKey.IsDisabled,
                IsNotForReplication = foreignKey.IsNotForReplication,
                IsNotTrusted = foreignKey.IsNotTrusted,
                IsSystemNamed = foreignKey.IsSystemNamed,
                DeleteAction = foreignKey.DeleteAction,
                DeleteActionDescription = foreignKey.DeleteActionDescription,
                UpdateAction = foreignKey.UpdateAction,
                UpdateActionDescription = foreignKey.UpdateActionDescription
            };

            foreach (var foreignKeyColumn in foreignKey.ForeignKeyColumns)
                AddForeignKeyColumn(foreignKeyClone, ForeignKeyColumn.Clone(foreignKeyColumn));

            return foreignKeyClone;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="parentMetaObject">The newly cloned parent meta object.</param>
        /// <param name="foreignKey">The foreign key to shallow clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static ForeignKey ShallowClone(IMetaObject parentMetaObject, ForeignKey foreignKey)
        {
            return new ForeignKey
            {
                ParentMetaObject = parentMetaObject,
                ObjectName = foreignKey.ObjectName,
                IsDisabled = foreignKey.IsDisabled,
                IsNotForReplication = foreignKey.IsNotForReplication,
                IsNotTrusted = foreignKey.IsNotTrusted,
                IsSystemNamed = foreignKey.IsSystemNamed,
                DeleteAction = foreignKey.DeleteAction,
                DeleteActionDescription = foreignKey.DeleteActionDescription,
                UpdateAction = foreignKey.UpdateAction,
                UpdateActionDescription = foreignKey.UpdateActionDescription
            };
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
