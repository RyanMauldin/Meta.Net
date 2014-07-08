using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Meta.Net;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class ForeignKey : IDataObject, IDataUserTableBasedObject
    {
		#region Properties (18) 

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

        public int DeleteAction { get; set; }

        public string DeleteActionDescription { get; set; }

        public string Description { get; set; }

        public Dictionary<string, ForeignKeyColumn> ForeignKeyColumns { get; private set; }

        public bool IsDisabled { get; set; }

        public bool IsNotForReplication { get; set; }

        public bool IsNotTrusted { get; set; }

        public bool IsSystemNamed { get; set; }

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
                    if (UserTable.RenameForeignKey(UserTable, _objectName, value))
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

        public string ReferencedObjectName { get; set; }

        public string ReferencedSchemaName { get; set; }

        public string ReferencedTableName { get; set; }

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

        public int UpdateAction { get; set; }

        public string UpdateActionDescription { get; set; }

        public UserTable UserTable { get; set; }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public ForeignKey(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            UserTable = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            DeleteAction = info.GetInt32("DeleteAction");
            DeleteActionDescription = info.GetString("DeleteActionDescription");
            IsDisabled = info.GetBoolean("IsDisabled");
            IsNotForReplication = info.GetBoolean("IsNotForReplication");
            IsNotTrusted = info.GetBoolean("IsNotTrusted");
            IsSystemNamed = info.GetBoolean("IsSystemNamed");
            ReferencedObjectName = info.GetString("ReferencedObjectName");
            ReferencedSchemaName = info.GetString("ReferencedSchemaName");
            ReferencedTableName = info.GetString("ReferencedTableName");
            UpdateAction = info.GetInt32("UpdateAction");
            UpdateActionDescription = info.GetString("UpdateActionDescription");

            // Deserialize Foreign Key Columns
            var foreignKeyColumns = info.GetInt32("ForeignKeyColumns");
            ForeignKeyColumns = new Dictionary<string, ForeignKeyColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < foreignKeyColumns; i++)
            {
                var foreignKeyColumn = (ForeignKeyColumn)info.GetValue("ForeignKeyColumn" + i, typeof(ForeignKeyColumn));
                foreignKeyColumn.ForeignKey = this;
                ForeignKeyColumns.Add(foreignKeyColumn.ObjectName, foreignKeyColumn);
            }
        }

        public ForeignKey(UserTable userTable, ForeignKeysRow foreignKeysRow)
        {
            Init(this, userTable, foreignKeysRow.ObjectName, foreignKeysRow);
        }

        public ForeignKey(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName, null);
        }

        public ForeignKey(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public ForeignKey()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (23) 

		#region Public Methods (22) 

        public static bool AddForeignKeyColumn(ForeignKey foreignKey, ForeignKeyColumn foreignKeyColumn)
        {
            if (foreignKey.ForeignKeyColumns.ContainsKey(foreignKeyColumn.ObjectName))
                return false;

            if (foreignKeyColumn.ForeignKey == null)
            {
                foreignKeyColumn.ForeignKey = foreignKey;
                foreignKey.ForeignKeyColumns.Add(foreignKeyColumn.ObjectName, foreignKeyColumn);
                return true;
            }

            if (foreignKeyColumn.ForeignKey.Equals(foreignKey))
            {
                foreignKey.ForeignKeyColumns.Add(foreignKeyColumn.ObjectName, foreignKeyColumn);
                return true;
            }

            return false;
        }

        public static bool AddForeignKeyColumn(ForeignKey foreignKey, string objectName)
        {    
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (foreignKey.ForeignKeyColumns.ContainsKey(objectName))
                return false;

            var foreignKeyColumn = new ForeignKeyColumn(foreignKey, objectName);
            foreignKey.ForeignKeyColumns.Add(objectName, foreignKeyColumn);
            
            return true;
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
            var foreignKeyClone = new ForeignKey(foreignKey.ObjectName)
            {
                DeleteAction = foreignKey.DeleteAction,
                DeleteActionDescription = foreignKey.DeleteActionDescription,
                IsDisabled = foreignKey.IsDisabled,
                IsNotForReplication = foreignKey.IsNotForReplication,
                IsNotTrusted = foreignKey.IsNotTrusted,
                IsSystemNamed = foreignKey.IsSystemNamed,
                ReferencedObjectName = foreignKey.ReferencedObjectName,
                ReferencedSchemaName = foreignKey.ReferencedSchemaName,
                ReferencedTableName = foreignKey.ReferencedTableName,
                UpdateAction = foreignKey.UpdateAction,
                UpdateActionDescription = foreignKey.UpdateActionDescription
            };

            foreach (var foreignKeyColumn in foreignKey.ForeignKeyColumns.Values)
                AddForeignKeyColumn(foreignKeyClone, ForeignKeyColumn.Clone(foreignKeyColumn));

            return foreignKeyClone;
        }

        public static bool CompareDefinitions(ForeignKey sourceForeignKey, ForeignKey targetForeignKey)
        {
            if (!CompareObjectNames(sourceForeignKey, targetForeignKey))
                return false;

            if (sourceForeignKey.DeleteAction != targetForeignKey.DeleteAction)
                return false;

            if (sourceForeignKey.IsDisabled != targetForeignKey.IsDisabled)
                return false;

            if (sourceForeignKey.IsNotForReplication != targetForeignKey.IsNotForReplication)
                return false;

            if (sourceForeignKey.IsNotTrusted != targetForeignKey.IsNotTrusted)
                return false;

            if (sourceForeignKey.IsSystemNamed != targetForeignKey.IsSystemNamed)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceForeignKey.ReferencedObjectName, targetForeignKey.ReferencedObjectName) != 0)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceForeignKey.ReferencedSchemaName, targetForeignKey.ReferencedSchemaName) != 0)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(
                sourceForeignKey.ReferencedTableName, targetForeignKey.ReferencedTableName) != 0)
                return false;

            return sourceForeignKey.UpdateAction == targetForeignKey.UpdateAction;
        }

        public static bool CompareObjectNames(ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceForeignKey.ObjectName, targetForeignKey.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target ForeignKey from the source ForeignKey.
        /// </summary>
        /// <param name="sourceForeignKey">The source ForeignKey.</param>
        /// <param name="targetForeignKey">The target ForeignKey.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
            matchingForeignKeyColumns.IntersectWith(targetForeignKey.ForeignKeyColumns.Keys);

            foreach (var foreignKeyColumn in matchingForeignKeyColumns)
            {
                ForeignKeyColumn sourceForeignKeyColumn;
                if (!sourceForeignKey.ForeignKeyColumns.TryGetValue(foreignKeyColumn, out sourceForeignKeyColumn))
                    continue;

                ForeignKeyColumn targetForeignKeyColumn;
                if (!targetForeignKey.ForeignKeyColumns.TryGetValue(foreignKeyColumn, out targetForeignKeyColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (ForeignKeyColumn.CompareDefinitions(sourceForeignKeyColumn, targetForeignKeyColumn))
                            RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (ForeignKeyColumn.CompareObjectNames(sourceForeignKeyColumn, targetForeignKeyColumn))
                            RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static void Fill(ForeignKey foreignKey, DataGenerics generics)
		{
            Clear(foreignKey);
            var predicate = new StringPredicate(foreignKey.Namespace + ".", StringComparison.OrdinalIgnoreCase);

            var foreignKeys = generics.ForeignKeys.FindAll(predicate.StartsWith);
            foreach (var str in foreignKeys)
            {
                ForeignKeysRow foreignKeysRow;
                if (!generics.ForeignKeyRows.TryGetValue(str + ".", out foreignKeysRow))
                    continue;

                var foreignKeyColumn = new ForeignKeyColumn(foreignKey, foreignKeysRow);
                AddForeignKeyColumn(foreignKey, foreignKeyColumn);
            }

		    var foreignKeyMaps = generics.ForeignKeyMaps.FindAll(predicate.StartsWith);
		    var firstIteration = true;
		    foreach (var str in foreignKeyMaps)
		    {
		        ForeignKeyMapsRow foreignKeyMapsRow;
                if (!generics.ForeignKeyMapRows.TryGetValue(str + ".", out foreignKeyMapsRow))
                    continue;

		        ForeignKeyColumn foreignKeyColumn;
                if (!foreignKey.ForeignKeyColumns.TryGetValue(foreignKeyMapsRow.ColumnName, out foreignKeyColumn))
                    continue;

                if (firstIteration)
                {
                    firstIteration = false;
                    foreignKey.ReferencedObjectName = foreignKeyMapsRow.ReferencedObjectName;
                    foreignKey.ReferencedSchemaName = foreignKeyMapsRow.ReferencedSchemaName;
                    foreignKey.ReferencedTableName = foreignKeyMapsRow.ReferencedTableName;
                }

		        foreignKeyColumn.ReferencedColumnName = foreignKeyMapsRow.ReferencedColumnName;
		    }
		}

        public static ForeignKey FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ForeignKey>(json);
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            ForeignKey foreignKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.CreateForeignKey(sourceDataContext, targetDataContext, foreignKey);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            ForeignKey foreignKey, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            var dataSyncAction = DataActionFactory.DropForeignKey(sourceDataContext, targetDataContext, foreignKey);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("DeleteAction", DeleteAction);
            info.AddValue("DeleteActionDescription", DeleteActionDescription);
            info.AddValue("IsDisabled", IsDisabled);
            info.AddValue("IsNotForReplication", IsNotForReplication);
            info.AddValue("IsNotTrusted", IsNotTrusted);
            info.AddValue("IsSystemNamed", IsSystemNamed);
            info.AddValue("ReferencedObjectName", ReferencedObjectName);
            info.AddValue("ReferencedSchemaName", ReferencedSchemaName);
            info.AddValue("ReferencedTableName", ReferencedTableName);
            info.AddValue("UpdateAction", UpdateAction);
            info.AddValue("UpdateActionDescription", UpdateActionDescription);
            info.AddValue("ForeignKeyColumns", ForeignKeyColumns);
        }

        /// <summary>
        /// Modifies this ForeignKey to contain only objects that are
        /// present in this ForeignKey and in the specified ForeignKey.
        /// </summary>
        /// <param name="sourceForeignKey">The source ForeignKey.</param>
        /// <param name="targetForeignKey">The target ForeignKey.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
            matchingForeignKeyColumns.IntersectWith(targetForeignKey.ForeignKeyColumns.Keys);

            removableForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
            removableForeignKeyColumns.ExceptWith(matchingForeignKeyColumns);

            foreach (var foreignKeyColumn in removableForeignKeyColumns)
                RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);

            foreach (var foreignKeyColumn in matchingForeignKeyColumns)
            {
                ForeignKeyColumn sourceForeignKeyColumn;
                if (!sourceForeignKey.ForeignKeyColumns.TryGetValue(foreignKeyColumn, out sourceForeignKeyColumn))
                    continue;

                ForeignKeyColumn targetForeignKeyColumn;
                if (!targetForeignKey.ForeignKeyColumns.TryGetValue(foreignKeyColumn, out targetForeignKeyColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!ForeignKeyColumn.CompareDefinitions(sourceForeignKeyColumn, targetForeignKeyColumn))
                            RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!ForeignKeyColumn.CompareObjectNames(sourceForeignKeyColumn, targetForeignKeyColumn))
                            RemoveForeignKeyColumn(sourceForeignKey, foreignKeyColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static void LinkForeignKey(ForeignKey foreignKey)
        {
            if (foreignKey.Catalog == null)
                return;

            if (!foreignKey.Catalog.ForeignKeyPool.ContainsKey(foreignKey.Namespace) &&
                !foreignKey.Catalog.ForeignKeyPool.ContainsValue(foreignKey))
                foreignKey.Catalog.ForeignKeyPool.Add(foreignKey.Namespace, foreignKey);

            var referencedUserTableNamespace = foreignKey.ReferencedSchemaName + "." + foreignKey.ReferencedTableName;

            List<ForeignKey> referencingForeignKeys;
            if (!foreignKey.Catalog.ReferencedUserTablePool.TryGetValue(referencedUserTableNamespace, out referencingForeignKeys))
            {
                referencingForeignKeys = new List<ForeignKey> { foreignKey };
                foreignKey.Catalog.ReferencedUserTablePool.Add(referencedUserTableNamespace, referencingForeignKeys);
                return;
            }
            
            if (!referencingForeignKeys.Contains(foreignKey))
                referencingForeignKeys.Add(foreignKey);
        }

        public static long ObjectCount(ForeignKey foreignKey)
        {
            return foreignKey.ForeignKeyColumns.Count;
        }

        public static bool RemoveForeignKeyColumn(ForeignKey foreignKey, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && foreignKey.ForeignKeyColumns.Remove(objectName);
        }

        public static bool RemoveForeignKeyColumn(ForeignKey foreignKey, ForeignKeyColumn foreignKeyColumn)
        {
            ForeignKeyColumn foreignKeyColumnObject;
            if (!foreignKey.ForeignKeyColumns.TryGetValue(foreignKeyColumn.ObjectName, out foreignKeyColumnObject)) return false;

            return foreignKeyColumn.Equals(foreignKeyColumnObject)
                && foreignKey.ForeignKeyColumns.Remove(foreignKeyColumn.ObjectName);
        }

        public static bool RenameForeignKeyColumn(ForeignKey foreignKey, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (foreignKey.ForeignKeyColumns.ContainsKey(newObjectName))
                return false;
            
            ForeignKeyColumn foreignKeyColumn;
            if (!foreignKey.ForeignKeyColumns.TryGetValue(objectName, out foreignKeyColumn))
                return false;

            foreignKey.ForeignKeyColumns.Remove(objectName);
            foreignKeyColumn.ForeignKey = null;
            foreignKeyColumn.ObjectName = newObjectName;
            foreignKeyColumn.ForeignKey = foreignKey;
            foreignKey.ForeignKeyColumns.Add(newObjectName, foreignKeyColumn);
            
            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="foreignKey">The foreign key to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static ForeignKey ShallowClone(ForeignKey foreignKey)
        {
            return new ForeignKey(foreignKey.ObjectName)
                {
                    DeleteAction = foreignKey.DeleteAction,
                    DeleteActionDescription = foreignKey.DeleteActionDescription,
                    IsDisabled = foreignKey.IsDisabled,
                    IsNotForReplication = foreignKey.IsNotForReplication,
                    IsNotTrusted = foreignKey.IsNotTrusted,
                    IsSystemNamed = foreignKey.IsSystemNamed,
                    ReferencedObjectName = foreignKey.ReferencedObjectName,
                    ReferencedSchemaName = foreignKey.ReferencedSchemaName,
                    ReferencedTableName = foreignKey.ReferencedTableName,
                    UpdateAction = foreignKey.UpdateAction,
                    UpdateActionDescription = foreignKey.UpdateActionDescription
                };
        }

        public static string ToJson(ForeignKey foreignKey, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(foreignKey, formatting);
        }

        /// <summary>
        /// Modifies this ForeignKey to contain all objects that are
        /// present in both iteself and the specified ForeignKey.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceForeignKey">The source ForeignKey.</param>
        /// <param name="targetForeignKey">The target ForeignKey.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            ForeignKey sourceForeignKey, ForeignKey targetForeignKey,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableForeignKeyColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingForeignKeyColumns.UnionWith(sourceForeignKey.ForeignKeyColumns.Keys);
            matchingForeignKeyColumns.IntersectWith(targetForeignKey.ForeignKeyColumns.Keys);

            addableForeignKeyColumns.UnionWith(targetForeignKey.ForeignKeyColumns.Keys);
            addableForeignKeyColumns.ExceptWith(matchingForeignKeyColumns);

            foreach (var foreignKeyColumn in addableForeignKeyColumns)
            {
                ForeignKeyColumn targetForeignKeyColumn;
                if (!targetForeignKey.ForeignKeyColumns.TryGetValue(foreignKeyColumn, out targetForeignKeyColumn))
                    continue;

                AddForeignKeyColumn(sourceForeignKey, ForeignKeyColumn.Clone(targetForeignKeyColumn));
            }
        }

        public static void UnlinkForeignKey(ForeignKey foreignKey)
        {
            if (foreignKey.Catalog == null)
                return;

            if (!foreignKey.Catalog.ForeignKeyPool.ContainsValue(foreignKey))
                return;

            foreignKey.Catalog.ForeignKeyPool.Remove(foreignKey.Namespace);

            var referencedUserTableNamespace = foreignKey.ReferencedSchemaName + "." + foreignKey.ReferencedTableName;

            List<ForeignKey> referencingForeignKeys;
            if (!foreignKey.Catalog.ReferencedUserTablePool.TryGetValue(referencedUserTableNamespace, out referencingForeignKeys))
                return;

            referencingForeignKeys.Remove(foreignKey);
            if (referencingForeignKeys.Count == 0)
                foreignKey.Catalog.ReferencedUserTablePool.Remove(referencedUserTableNamespace);
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(ForeignKey foreignKey, UserTable userTable, string objectName, ForeignKeysRow foreignKeysRow)
        {
            foreignKey.ForeignKeyColumns = new Dictionary<string, ForeignKeyColumn>(StringComparer.OrdinalIgnoreCase);
            foreignKey.UserTable = userTable;
            foreignKey._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;
            foreignKey.Description = "Foreign Key";
            
            if (foreignKeysRow == null)
            {
                foreignKey.DeleteAction = 0;
                foreignKey.DeleteActionDescription = "NO_ACTION";
                foreignKey.IsDisabled = false;
                foreignKey.IsNotForReplication = false;
                foreignKey.IsNotTrusted = false;
                foreignKey.IsSystemNamed = false;
                foreignKey.UpdateAction = 0;
                foreignKey.UpdateActionDescription = "NO_ACTION";
            }
            else
            {
                foreignKey.DeleteAction = foreignKeysRow.DeleteAction;
                foreignKey.DeleteActionDescription = foreignKeysRow.DeleteActionDescription;
                foreignKey.IsDisabled = foreignKeysRow.IsDisabled;
                foreignKey.IsNotForReplication = foreignKeysRow.IsNotForReplication;
                foreignKey.IsNotTrusted = foreignKeysRow.IsNotTrusted;
                foreignKey.IsSystemNamed = foreignKeysRow.IsSystemNamed;
                foreignKey.UpdateAction = foreignKeysRow.UpdateAction;
                foreignKey.UpdateActionDescription = foreignKeysRow.UpdateActionDescription;
            }

            foreignKey.ReferencedObjectName = "";
            foreignKey.ReferencedSchemaName = "";
            foreignKey.ReferencedTableName = "";
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
