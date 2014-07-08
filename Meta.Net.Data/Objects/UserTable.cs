using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using Meta.Net.Data.Converters;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;
using Meta.Net.Data.Metadata;
using Newtonsoft.Json;

namespace Meta.Net.Data.Objects
{
    [Serializable]
    public class UserTable : IDataObject, IDataCatalogBasedObject
    {
		#region Properties (19) 

        public Catalog Catalog
        {
            get
            {
                var schema = Schema;
                return schema == null
                    ? null
                    : schema.Catalog;
            }
        }

        public Dictionary<string, CheckConstraint> CheckConstraints { get; private set; }

        public Dictionary<string, ComputedColumn> ComputedColumns { get; private set; }

        public Dictionary<string, DefaultConstraint> DefaultConstraints { get; private set; }

        public string Description { get; set; }

        public string FileStreamFileGroup { get; set; }

        public Dictionary<string, ForeignKey> ForeignKeys { get; private set; }

        public bool HasTextNTextOrImageColumns { get; set; }

        public Dictionary<string, IdentityColumn> IdentityColumns { get; private set; }

        public Dictionary<string, Index> Indexes { get; private set; }

        public string LobFileGroup { get; set; }

        public string Namespace
        {
            get
            {
                if (Schema == null)
                    return ObjectName;

                return Schema.ObjectName + "." + ObjectName;

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
                if (Schema != null)
                {
                    if (Schema.RenameUserTable(Schema, _objectName, value))
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

        public Dictionary<string, PrimaryKey> PrimaryKeys { get; private set; }

        public Schema Schema { get; set; }

        public int TextInRowLimit { get; set; }

        public Dictionary<string, UniqueConstraint> UniqueConstraints { get; private set; }

        public Dictionary<string, UserTableColumn> UserTableColumns { get; private set; }

        public bool UsesAnsiNulls { get; set; }

		#endregion Properties 

		#region Fields (1) 

        [NonSerialized]
        private string _objectName;

		#endregion Fields 

		#region Constructors (5) 

        public UserTable(SerializationInfo info, StreamingContext context)
        {
            // Set Null Members
            Schema = null;

            // Deserialize Members
            ObjectName = info.GetString("ObjectName");
            Description = info.GetString("Description");
            FileStreamFileGroup = info.GetString("FileStreamFileGroup");
            HasTextNTextOrImageColumns = info.GetBoolean("HasTextNTextOrImageColumns");
            LobFileGroup = info.GetString("LobFileGroup");
            TextInRowLimit = info.GetInt32("TextInRowLimit");
            UsesAnsiNulls = info.GetBoolean("UsesAnsiNulls");

            // Deserialize Check Constraints
            var checkConstraints = info.GetInt32("CheckConstraints");
            CheckConstraints = new Dictionary<string, CheckConstraint>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < checkConstraints; i++)
            {
                var checkConstraint = (CheckConstraint)info.GetValue("CheckConstraint" + i, typeof(CheckConstraint));
                checkConstraint.UserTable = this;
                CheckConstraints.Add(checkConstraint.ObjectName, checkConstraint);
            }

            // Deserialize Computed Columns
            var computedColumns = info.GetInt32("ComputedColumns");
            ComputedColumns = new Dictionary<string, ComputedColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < computedColumns; i++)
            {
                var computedColumn = (ComputedColumn)info.GetValue("ComputedColumn" + i, typeof(ComputedColumn));
                computedColumn.UserTable = this;
                ComputedColumns.Add(computedColumn.ObjectName, computedColumn);
            }

            // Deserialize Default Constraints
            var defaultConstraints = info.GetInt32("DefaultConstraints");
            DefaultConstraints = new Dictionary<string, DefaultConstraint>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < defaultConstraints; i++)
            {
                var defaultConstraint = (DefaultConstraint)info.GetValue("DefaultConstraint" + i, typeof(DefaultConstraint));
                defaultConstraint.UserTable = this;
                DefaultConstraints.Add(defaultConstraint.ObjectName, defaultConstraint);
            }

            // Deserialize Foreign Keys
            var foreignKeys = info.GetInt32("ForeignKeys");
            ForeignKeys = new Dictionary<string, ForeignKey>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < foreignKeys; i++)
            {
                var foreignKey = (ForeignKey)info.GetValue("ForeignKey" + i, typeof(ForeignKey));
                foreignKey.UserTable = this;
                ForeignKeys.Add(foreignKey.ObjectName, foreignKey);
            }

            // Deserialize Identity Columns
            var identityColumns = info.GetInt32("IdentityColumns");
            IdentityColumns = new Dictionary<string, IdentityColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < identityColumns; i++)
            {
                var identityColumn = (IdentityColumn)info.GetValue("IdentityColumn" + i, typeof(IdentityColumn));
                identityColumn.UserTable = this;
                IdentityColumns.Add(identityColumn.ObjectName, identityColumn);
            }

            // Deserialize Indexes
            var indexes = info.GetInt32("Indexes");
            Indexes = new Dictionary<string, Index>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < indexes; i++)
            {
                var index = (Index)info.GetValue("Index" + i, typeof(Index));
                index.UserTable = this;
                Indexes.Add(index.ObjectName, index);
            }

            // Deserialize Primary Keys
            var primaryKeys = info.GetInt32("PrimaryKeys");
            PrimaryKeys = new Dictionary<string, PrimaryKey>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < primaryKeys; i++)
            {
                var primaryKey = (PrimaryKey)info.GetValue("PrimaryKey" + i, typeof(PrimaryKey));
                primaryKey.UserTable = this;
                PrimaryKeys.Add(primaryKey.ObjectName, primaryKey);
            }

            // Deserialize Unique Constraints
            var uniqueConstraints = info.GetInt32("UniqueConstraints");
            UniqueConstraints = new Dictionary<string, UniqueConstraint>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < uniqueConstraints; i++)
            {
                var uniqueConstraint = (UniqueConstraint)info.GetValue("UniqueConstraint" + i, typeof(UniqueConstraint));
                uniqueConstraint.UserTable = this;
                UniqueConstraints.Add(uniqueConstraint.ObjectName, uniqueConstraint);
            }

            // Deserialize User-Table Columns
            var userTableColumns = info.GetInt32("UserTableColumns");
            UserTableColumns = new Dictionary<string, UserTableColumn>(StringComparer.OrdinalIgnoreCase);

            for (var i = 0; i < userTableColumns; i++)
            {
                var userTableColumn = (UserTableColumn)info.GetValue("UserTableColumn" + i, typeof(UserTableColumn));
                userTableColumn.UserTable = this;
                UserTableColumns.Add(userTableColumn.ObjectName, userTableColumn);
            }
        }

        public UserTable(Schema schema, UserTablesRow userTablesRow)
		{
            Init(this, schema, userTablesRow.ObjectName, userTablesRow);
		}

        public UserTable(Schema schema, string objectName)
        {
            Init(this, schema, objectName, null);
        }

        public UserTable(string objectName)
        {
            Init(this, null, objectName, null);
        }

        public UserTable()
        {
            Init(this, null, null, null);
        }

		#endregion Constructors 

		#region Methods (63) 

		#region Public Methods (62) 

        public static bool AddCheckConstraint(UserTable userTable, CheckConstraint checkConstraint)
        {
            if (userTable.CheckConstraints.ContainsKey(checkConstraint.ObjectName))
                return false;

            if (checkConstraint.UserTable == null)
            {
                checkConstraint.UserTable = userTable;
                userTable.CheckConstraints.Add(checkConstraint.ObjectName, checkConstraint);
                return true;
            }

            if (checkConstraint.UserTable.Equals(userTable))
            {
                userTable.CheckConstraints.Add(checkConstraint.ObjectName, checkConstraint);
                return true;
            }

            return false;
        }

        public static bool AddCheckConstraint(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.CheckConstraints.ContainsKey(objectName))
                return false;

            var checkConstraint = new CheckConstraint(userTable, objectName);
            userTable.CheckConstraints.Add(objectName, checkConstraint);
            
            return true;
        }

        public static bool AddComputedColumn(UserTable userTable, ComputedColumn computedColumn)
        {
            if (userTable.ComputedColumns.ContainsKey(computedColumn.ObjectName))
                return false;

            if (computedColumn.UserTable == null)
            {
                computedColumn.UserTable = userTable;
                userTable.ComputedColumns.Add(computedColumn.ObjectName, computedColumn);
                return true;
            }

            if (computedColumn.UserTable.Equals(userTable))
            {
                userTable.ComputedColumns.Add(computedColumn.ObjectName, computedColumn);
                return true;
            }

            return false;
        }

        public static bool AddComputedColumn(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.ComputedColumns.ContainsKey(objectName))
                return false;

            var computedColumn = new ComputedColumn(userTable, objectName);
            userTable.ComputedColumns.Add(objectName, computedColumn);
            
            return true;
        }

        public static bool AddDefaultConstraint(UserTable userTable, DefaultConstraint defaultConstraint)
        {
            if (userTable.DefaultConstraints.ContainsKey(defaultConstraint.ObjectName))
                return false;

            if (defaultConstraint.UserTable == null)
            {
                defaultConstraint.UserTable = userTable;
                userTable.DefaultConstraints.Add(defaultConstraint.ObjectName, defaultConstraint);
                return true;
            }

            if (defaultConstraint.UserTable.Equals(userTable))
            {
                userTable.DefaultConstraints.Add(defaultConstraint.ObjectName, defaultConstraint);
                return true;
            }

            return false;
        }

        public static bool AddDefaultConstraint(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.DefaultConstraints.ContainsKey(objectName))
                return false;

            var defaultConstraint = new DefaultConstraint(userTable, objectName);
            userTable.DefaultConstraints.Add(objectName, defaultConstraint);
            
            return true;
        }

        public static bool AddForeignKey(UserTable userTable, ForeignKey foreignKey)
        {
            if (userTable.ForeignKeys.ContainsKey(foreignKey.ObjectName))
                return false;

            if (foreignKey.UserTable == null)
            {
                foreignKey.UserTable = userTable;
                userTable.ForeignKeys.Add(foreignKey.ObjectName, foreignKey);
                if (userTable.Catalog != null)
                    ForeignKey.LinkForeignKey(foreignKey);
                return true;
            }

            if (foreignKey.UserTable.Equals(userTable))
            {
                userTable.ForeignKeys.Add(foreignKey.ObjectName, foreignKey);
                if (userTable.Catalog != null)
                    ForeignKey.LinkForeignKey(foreignKey);
                return true;
            }

            return false;
        }

        public static bool AddForeignKey(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.ForeignKeys.ContainsKey(objectName))
                return false;

            var foreignKey = new ForeignKey(userTable, objectName);
            userTable.ForeignKeys.Add(objectName, foreignKey);
            if (userTable.Catalog != null)
                ForeignKey.LinkForeignKey(foreignKey);

            return true;
        }

        public static bool AddIdentityColumn(UserTable userTable, IdentityColumn identityColumn)
        {
            if (userTable.IdentityColumns.ContainsKey(identityColumn.ObjectName))
                return false;

            if (identityColumn.UserTable == null)
            {
                identityColumn.UserTable = userTable;
                userTable.IdentityColumns.Add(identityColumn.ObjectName, identityColumn);
                return true;
            }

            if (identityColumn.UserTable.Equals(userTable))
            {
                userTable.IdentityColumns.Add(identityColumn.ObjectName, identityColumn);
                return true;
            }

            return false;
        }

        public static bool AddIdentityColumn(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.IdentityColumns.ContainsKey(objectName))
                return false;

            var identityColumn = new IdentityColumn(userTable, objectName);
            userTable.IdentityColumns.Add(objectName, identityColumn);
            
            return true;
        }

        public static bool AddIndex(UserTable userTable, Index index)
        {
            if (userTable.Indexes.ContainsKey(index.ObjectName))
                return false;

            if (index.UserTable == null)
            {
                index.UserTable = userTable;
                userTable.Indexes.Add(index.ObjectName, index);
                return true;
            }

            if (index.UserTable.Equals(userTable))
            {
                userTable.Indexes.Add(index.ObjectName, index);
                return true;
            }

            return false;
        }

        public static bool AddIndex(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.Indexes.ContainsKey(objectName))
                return false;

            var index = new Index(userTable, objectName);
            userTable.Indexes.Add(objectName, index);
            
            return true;
        }

        public static bool AddPrimaryKey(UserTable userTable, PrimaryKey primaryKey)
        {
            if (userTable.PrimaryKeys.ContainsKey(primaryKey.ObjectName))
                return false;

            if (primaryKey.UserTable == null)
            {
                primaryKey.UserTable = userTable;
                userTable.PrimaryKeys.Add(primaryKey.ObjectName, primaryKey);
                return true;
            }

            if (primaryKey.UserTable.Equals(userTable))
            {
                userTable.PrimaryKeys.Add(primaryKey.ObjectName, primaryKey);
                return true;
            }

            return false;
        }

        public static bool AddPrimaryKey(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.PrimaryKeys.ContainsKey(objectName))
                return false;

            var primaryKey = new PrimaryKey(userTable, objectName);
            userTable.PrimaryKeys.Add(objectName, primaryKey);
            
            return true;
        }

        public static bool AddUniqueConstraint(UserTable userTable, UniqueConstraint uniqueConstraint)
        {
            if (userTable.UniqueConstraints.ContainsKey(uniqueConstraint.ObjectName))
                return false;

            if (uniqueConstraint.UserTable == null)
            {
                uniqueConstraint.UserTable = userTable;
                userTable.UniqueConstraints.Add(uniqueConstraint.ObjectName, uniqueConstraint);
                return true;
            }

            if (uniqueConstraint.UserTable.Equals(userTable))
            {
                userTable.UniqueConstraints.Add(uniqueConstraint.ObjectName, uniqueConstraint);
                return true;
            }

            return false;
        }

        public static bool AddUniqueConstraint(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.UniqueConstraints.ContainsKey(objectName))
                return false;

            var uniqueConstraint = new UniqueConstraint(userTable, objectName);
            userTable.UniqueConstraints.Add(objectName, uniqueConstraint);
            
            return true;
        }

        public static bool AddUserTableColumn(UserTable userTable, UserTableColumn userTableColumn)
        {
            if (userTable.UserTableColumns.ContainsKey(userTableColumn.ObjectName))
                return false;

            if (userTableColumn.UserTable == null)
            {
                userTableColumn.UserTable = userTable;
                userTable.UserTableColumns.Add(userTableColumn.ObjectName, userTableColumn);
                return true;
            }

            if (userTableColumn.UserTable.Equals(userTable))
            {
                userTable.UserTableColumns.Add(userTableColumn.ObjectName, userTableColumn);
                return true;
            }

            return false;
        }

        public static bool AddUserTableColumn(UserTable userTable, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (userTable.UserTableColumns.ContainsKey(objectName))
                return false;

            var userTableColumn = new UserTableColumn(userTable, objectName);
            userTable.UserTableColumns.Add(objectName, userTableColumn);
            
            return true;
        }

        /// <summary>
        /// Deep Clear... disassociates all objects in each user-table based object
        /// first before clearing out the collections in hopes of better
        /// memory dissipation from garbage collection.
        /// </summary>
        /// <param name="userTable">The user-table to deep clear.</param>
        public static void Clear(UserTable userTable)
        {
            userTable.CheckConstraints.Clear();
            userTable.ComputedColumns.Clear();
            userTable.DefaultConstraints.Clear();

            foreach (var foreignKey in userTable.ForeignKeys.Values)
            {
                ForeignKey.UnlinkForeignKey(foreignKey);
                ForeignKey.Clear(foreignKey);
            }
            userTable.ForeignKeys.Clear();
            userTable.IdentityColumns.Clear();
            foreach (var index in userTable.Indexes.Values)
                Index.Clear(index);
            userTable.Indexes.Clear();
            foreach (var primaryKey in userTable.PrimaryKeys.Values)
                PrimaryKey.Clear(primaryKey);
            userTable.PrimaryKeys.Clear();
            foreach (var uniqueConstraint in userTable.UniqueConstraints.Values)
                UniqueConstraint.Clear(uniqueConstraint);
            userTable.UniqueConstraints.Clear();
            userTable.UserTableColumns.Clear();
        }

        /// <summary>
        /// Deep Clone...
        /// A clone of this class and clones of all assosiated metadata.
        /// </summary>
        /// <param name="userTable">The user-table to deep clone.</param>
        /// <returns>A clone of this class and clones of all assosiated metadata.</returns>
        public static UserTable Clone(UserTable userTable)
        {
            var userTableClone = new UserTable(userTable.ObjectName)
            {
                FileStreamFileGroup = userTable.FileStreamFileGroup,
                HasTextNTextOrImageColumns = userTable.HasTextNTextOrImageColumns,
                LobFileGroup = userTable.LobFileGroup,
                TextInRowLimit = userTable.TextInRowLimit,
                UsesAnsiNulls = userTable.UsesAnsiNulls
            };

            foreach (var checkConstraint in userTable.CheckConstraints.Values)
                AddCheckConstraint(userTableClone, CheckConstraint.Clone(checkConstraint));

            foreach (var computedColumn in userTable.ComputedColumns.Values)
                AddComputedColumn(userTableClone, ComputedColumn.Clone(computedColumn));

            foreach (var defaultConstraint in userTable.DefaultConstraints.Values)
                AddDefaultConstraint(userTableClone, DefaultConstraint.Clone(defaultConstraint));

            foreach (var foreignKey in userTable.ForeignKeys.Values)
                AddForeignKey(userTableClone, ForeignKey.Clone(foreignKey));

            foreach (var identityColumn in userTable.IdentityColumns.Values)
                AddIdentityColumn(userTableClone, IdentityColumn.Clone(identityColumn));

            foreach (var index in userTable.Indexes.Values)
                AddIndex(userTableClone, Index.Clone(index));

            foreach (var primaryKey in userTable.PrimaryKeys.Values)
                AddPrimaryKey(userTableClone, PrimaryKey.Clone(primaryKey));

            foreach (var uniqueConstraint in userTable.UniqueConstraints.Values)
                AddUniqueConstraint(userTableClone, UniqueConstraint.Clone(uniqueConstraint));

            foreach (var userTableColumn in userTable.UserTableColumns.Values)
                AddUserTableColumn(userTableClone, UserTableColumn.Clone(userTableColumn));

            return userTableClone;
        }

        public static bool CompareDefinitions(UserTable sourceUserTable, UserTable targetUserTable)
        {
            if (!CompareObjectNames(sourceUserTable, targetUserTable))
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTable.FileStreamFileGroup, targetUserTable.FileStreamFileGroup) != 0)
                return false;

            if (sourceUserTable.HasTextNTextOrImageColumns != targetUserTable.HasTextNTextOrImageColumns)
                return false;

            if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTable.LobFileGroup, targetUserTable.LobFileGroup) != 0)
                return false;

            if (sourceUserTable.TextInRowLimit != targetUserTable.TextInRowLimit)
                return false;

            return sourceUserTable.UsesAnsiNulls == targetUserTable.UsesAnsiNulls;
        }

        public static bool CompareObjectNames(UserTable sourceUserTable, UserTable targetUserTable,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            switch (stringComparison)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
                case StringComparison.InvariantCulture:
                    return StringComparer.InvariantCulture.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparer.InvariantCultureIgnoreCase.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
                default:
                    return StringComparer.OrdinalIgnoreCase.Compare(
                        sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
            }
        }

        /// <summary>
        /// Removes all objects in the target UserTable from the source UserTable.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUserTable">The source UserTable.</param>
        /// <param name="targetUserTable">The target UserTable.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void ExceptWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserTable sourceUserTable, UserTable targetUserTable,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
            matchingCheckConstraints.IntersectWith(targetUserTable.CheckConstraints.Keys);

            foreach (var checkConstraint in matchingCheckConstraints)
            {
                CheckConstraint sourceCheckConstraint;
                if (!sourceUserTable.CheckConstraints.TryGetValue(checkConstraint, out sourceCheckConstraint))
                    continue;

                CheckConstraint targetCheckConstraint;
                if (!targetUserTable.CheckConstraints.TryGetValue(checkConstraint, out targetCheckConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (CheckConstraint.CompareDefinitions(sourceCheckConstraint, targetCheckConstraint))
                            RemoveCheckConstraint(sourceUserTable, checkConstraint);
                        break;
                    case DataComparisonType.Namespaces:
                        if (CheckConstraint.CompareObjectNames(sourceCheckConstraint, targetCheckConstraint))
                            RemoveCheckConstraint(sourceUserTable, checkConstraint);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
            matchingComputedColumns.IntersectWith(targetUserTable.ComputedColumns.Keys);

            foreach (var computedColumn in matchingComputedColumns)
            {
                ComputedColumn sourceComputedColumn;
                if (!sourceUserTable.ComputedColumns.TryGetValue(computedColumn, out sourceComputedColumn))
                    continue;

                ComputedColumn targetComputedColumn;
                if (!targetUserTable.ComputedColumns.TryGetValue(computedColumn, out targetComputedColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (ComputedColumn.CompareDefinitions(sourceComputedColumn, targetComputedColumn))
                            RemoveComputedColumn(sourceUserTable, computedColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (ComputedColumn.CompareObjectNames(sourceComputedColumn, targetComputedColumn))
                            RemoveComputedColumn(sourceUserTable, computedColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
            matchingDefaultConstraints.IntersectWith(targetUserTable.DefaultConstraints.Keys);

            foreach (var defaultConstraint in matchingDefaultConstraints)
            {
                DefaultConstraint sourceDefaultConstraint;
                if (!sourceUserTable.DefaultConstraints.TryGetValue(defaultConstraint, out sourceDefaultConstraint))
                    continue;

                DefaultConstraint targetDefaultConstraint;
                if (!targetUserTable.DefaultConstraints.TryGetValue(defaultConstraint, out targetDefaultConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (DefaultConstraint.CompareDefinitions(sourceDefaultConstraint, targetDefaultConstraint))
                            RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
                        break;
                    case DataComparisonType.Namespaces:
                        if (DefaultConstraint.CompareObjectNames(sourceDefaultConstraint, targetDefaultConstraint))
                            RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
            matchingIdentityColumns.IntersectWith(targetUserTable.IdentityColumns.Keys);

            foreach (var identityColumn in matchingIdentityColumns)
            {
                IdentityColumn sourceIdentityColumn;
                if (!sourceUserTable.IdentityColumns.TryGetValue(identityColumn, out sourceIdentityColumn))
                    continue;

                IdentityColumn targetIdentityColumn;
                if (!targetUserTable.IdentityColumns.TryGetValue(identityColumn, out targetIdentityColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (IdentityColumn.CompareDefinitions(sourceIdentityColumn, targetIdentityColumn))
                            RemoveIdentityColumn(sourceUserTable, identityColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (IdentityColumn.CompareObjectNames(sourceIdentityColumn, targetIdentityColumn))
                            RemoveIdentityColumn(sourceUserTable, identityColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
            matchingForeignKeys.IntersectWith(targetUserTable.ForeignKeys.Keys);

            foreach (var foreignKey in matchingForeignKeys)
            {
                ForeignKey sourceForeignKey;
                if (!sourceUserTable.ForeignKeys.TryGetValue(foreignKey, out sourceForeignKey))
                    continue;

                ForeignKey targetForeignKey;
                if (!targetUserTable.ForeignKeys.TryGetValue(foreignKey, out targetForeignKey))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (ForeignKey.CompareDefinitions(sourceForeignKey, targetForeignKey))
                        {
                            ForeignKey.ExceptWith(sourceForeignKey, targetForeignKey, dataComparisonType);
                            if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
                                RemoveForeignKey(sourceUserTable, foreignKey);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (ForeignKey.CompareObjectNames(sourceForeignKey, targetForeignKey))
                        {
                            ForeignKey.ExceptWith(sourceForeignKey, targetForeignKey, dataComparisonType);
                            if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
                                RemoveForeignKey(sourceUserTable, foreignKey);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIndexes.UnionWith(sourceUserTable.Indexes.Keys);
            matchingIndexes.IntersectWith(targetUserTable.Indexes.Keys);

            foreach (var index in matchingIndexes)
            {
                Index sourceIndex;
                if (!sourceUserTable.Indexes.TryGetValue(index, out sourceIndex))
                    continue;

                Index targetIndex;
                if (!targetUserTable.Indexes.TryGetValue(index, out targetIndex))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Index.CompareDefinitions(sourceIndex, targetIndex))
                        {
                            Index.ExceptWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
                            if (Index.ObjectCount(sourceIndex) == 0)
                                RemoveIndex(sourceUserTable, index);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (Index.CompareObjectNames(sourceIndex, targetIndex))
                        {
                            Index.ExceptWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
                            if (Index.ObjectCount(sourceIndex) == 0)
                                RemoveIndex(sourceUserTable, index);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingPrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingPrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
            matchingPrimaryKeys.IntersectWith(targetUserTable.PrimaryKeys.Keys);

            foreach (var primaryKey in matchingPrimaryKeys)
            {
                PrimaryKey sourcePrimaryKey;
                if (!sourceUserTable.PrimaryKeys.TryGetValue(primaryKey, out sourcePrimaryKey))
                    continue;

                PrimaryKey targetPrimaryKey;
                if (!targetUserTable.PrimaryKeys.TryGetValue(primaryKey, out targetPrimaryKey))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (PrimaryKey.CompareDefinitions(sourcePrimaryKey, targetPrimaryKey))
                        {
                            PrimaryKey.ExceptWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
                            if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
                                RemovePrimaryKey(sourceUserTable, primaryKey);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (PrimaryKey.CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
                        {
                            PrimaryKey.ExceptWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
                            if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
                                RemovePrimaryKey(sourceUserTable, primaryKey);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
            matchingUniqueConstraints.IntersectWith(targetUserTable.UniqueConstraints.Keys);

            foreach (var uniqueConstraint in matchingUniqueConstraints)
            {
                UniqueConstraint sourceUniqueConstraint;
                if (!sourceUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out sourceUniqueConstraint))
                    continue;

                UniqueConstraint targetUniqueConstraint;
                if (!targetUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out targetUniqueConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UniqueConstraint.CompareDefinitions(sourceUniqueConstraint, targetUniqueConstraint))
                        {
                            UniqueConstraint.ExceptWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
                            if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
                                RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (UniqueConstraint.CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
                        {
                            UniqueConstraint.ExceptWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
                            if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
                                RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
            matchingUserTableColumns.IntersectWith(targetUserTable.UserTableColumns.Keys);

            foreach (var userTableColumn in matchingUserTableColumns)
            {
                UserTableColumn sourceUserTableColumn;
                if (!sourceUserTable.UserTableColumns.TryGetValue(userTableColumn, out sourceUserTableColumn))
                    continue;

                UserTableColumn targetUserTableColumn;
                if (!targetUserTable.UserTableColumns.TryGetValue(userTableColumn, out targetUserTableColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UserTableColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUserTableColumn, targetUserTableColumn))
                            RemoveUserTableColumn(sourceUserTable, userTableColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (UserTableColumn.CompareObjectNames(sourceUserTableColumn, targetUserTableColumn))
                            RemoveUserTableColumn(sourceUserTable, userTableColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        public static void Fill(UserTable userTable, DataGenerics generics)
        {
            Clear(userTable);
            var predicate = new StringPredicate(userTable.Namespace + ".", StringComparison.OrdinalIgnoreCase);

            var checkConstraints = generics.CheckConstraints.FindAll(predicate.StartsWith);
            foreach(var str in checkConstraints)
            {
                CheckConstraintsRow checkConstraintsRow;
                if (!generics.CheckConstraintRows.TryGetValue(str + ".", out checkConstraintsRow))
                    continue;

                var checkConstraint = new CheckConstraint(userTable, checkConstraintsRow);
                AddCheckConstraint(userTable, checkConstraint);
            }

            var computedColumns = generics.ComputedColumns.FindAll(predicate.StartsWith);
            foreach (var str in computedColumns)
            {
                ComputedColumnsRow computedColumnsRow;
                if (!generics.ComputedColumnRows.TryGetValue(str + ".", out computedColumnsRow))
                    continue;

                var computedColumn = new ComputedColumn(userTable, computedColumnsRow);
                AddComputedColumn(userTable, computedColumn);
            }

            var defaultConstraints = generics.DefaultConstraints.FindAll(predicate.StartsWith);
            foreach (var str in defaultConstraints)
            {
                DefaultConstraintsRow defaultConstraintsRow;
                if (!generics.DefaultConstraintRows.TryGetValue(str + ".", out defaultConstraintsRow))
                    continue;

                var defaultConstraint = new DefaultConstraint(userTable, defaultConstraintsRow);
                AddDefaultConstraint(userTable, defaultConstraint);
            }

            var foreignKeyGroups = generics.ForeignKeyGroups.FindAll(predicate.StartsWith);
            foreach (var str in foreignKeyGroups)
            {
                predicate.MatchQualifier = str + ".";
                var foreignKeyNamespace = generics.ForeignKeys.Find(predicate.StartsWith);
                if (string.IsNullOrEmpty(foreignKeyNamespace))
                    continue;
                
                ForeignKeysRow foreignKeysRow;
                if (!generics.ForeignKeyRows.TryGetValue(foreignKeyNamespace + ".", out foreignKeysRow)) continue;

                var foreignKey = new ForeignKey(userTable, foreignKeysRow);
                ForeignKey.Fill(foreignKey, generics);
                AddForeignKey(userTable, foreignKey);
            }

            predicate.MatchQualifier = userTable.Namespace + ".";
            var identityColumns = generics.IdentityColumns.FindAll(predicate.StartsWith);
            foreach (var str in identityColumns)
            {
                IdentityColumnsRow identityColumnsRow;
                if (!generics.IdentityColumnRows.TryGetValue(str + ".", out identityColumnsRow))
                    continue;

                var identityColumn = new IdentityColumn(userTable, identityColumnsRow);
                AddIdentityColumn(userTable, identityColumn);
            }

            var indexGroups = generics.IndexGroups.FindAll(predicate.StartsWith);
            foreach (var str in indexGroups)
            {
                predicate.MatchQualifier = str + ".";
                var indexNamespace = generics.Indexes.Find(predicate.StartsWith);
                if (string.IsNullOrEmpty(indexNamespace))
                    continue;

                IndexesRow indexesRow;
                if (!generics.IndexRows.TryGetValue(indexNamespace + ".", out indexesRow))
                    continue;

                var index = new Index(userTable, indexesRow);
                Index.Fill(index, generics);
                AddIndex(userTable, index);
            }

            predicate.MatchQualifier = userTable.Namespace + ".";
            var primaryKeyGroups = generics.PrimaryKeyGroups.FindAll(predicate.StartsWith);
            foreach (var str in primaryKeyGroups)
            {
                predicate.MatchQualifier = str + ".";
                var primaryKeyNamespace = generics.PrimaryKeys.Find(predicate.StartsWith);
                if (string.IsNullOrEmpty(primaryKeyNamespace))
                    continue;

                PrimaryKeysRow primaryKeysRow;
                if (!generics.PrimaryKeyRows.TryGetValue(primaryKeyNamespace + ".", out primaryKeysRow))
                    continue;

                var primaryKey = new PrimaryKey(userTable, primaryKeysRow);
                PrimaryKey.Fill(primaryKey, generics);
                AddPrimaryKey(userTable, primaryKey);
            }

            predicate.MatchQualifier = userTable.Namespace + ".";
            var uniqueConstraintGroups = generics.UniqueConstraintGroups.FindAll(predicate.StartsWith);
            foreach (var str in uniqueConstraintGroups)
            {
                predicate.MatchQualifier = str + ".";
                var uniqueConstraintNamespace = generics.UniqueConstraints.Find(predicate.StartsWith);
                if (string.IsNullOrEmpty(uniqueConstraintNamespace))
                    continue;

                UniqueConstraintsRow uniqueConstraintsRow;
                if (!generics.UniqueConstraintRows.TryGetValue(uniqueConstraintNamespace + ".", out uniqueConstraintsRow))
                    continue;

                var uniqueConstraint = new UniqueConstraint(userTable, uniqueConstraintsRow);
                
                UniqueConstraint.Fill(uniqueConstraint, generics);
                AddUniqueConstraint(userTable, uniqueConstraint);
            }

            predicate.MatchQualifier = userTable.Namespace + ".";
            var userTableColumns = generics.UserTableColumns.FindAll(predicate.StartsWith);
            foreach (var str in userTableColumns)
            {
                UserTableColumnsRow userTableColumnsRow;
                if (!generics.UserTableColumnRows.TryGetValue(str + ".", out userTableColumnsRow))
                    continue;
                
                var userTableColumn = new UserTableColumn(userTable, userTableColumnsRow);
                AddUserTableColumn(userTable, userTableColumn);
            }
        }

        public static UserTable FromJson(string json)
        {
            return JsonConvert.DeserializeObject<UserTable>(json);
        }

        public static void GenerateAlterScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserTable alteredUserTable, UserTable addableUserTable, UserTable droppableUserTable,
            UserTable alterableUserTable, UserTable droppedUserTable, UserTable createdUserTable,
            UserTable sourceUserTable, UserTable targetUserTable, UserTable matchedUserTable,
            DataSyncActionsCollection dataSyncActions, DataDependencyBuilder dataDependencyBuilder, DataProperties dataProperties)
        {
            // DataDependecyBuilder would only be null if the call did not
            // originate from GenerateAlterSchemas
            if (dataDependencyBuilder == null)
            {
                dataDependencyBuilder = new DataDependencyBuilder();

                if (droppedUserTable != null)
                    dataDependencyBuilder.PreloadDroppedDependencies(droppedUserTable);

                if (createdUserTable != null)
                    dataDependencyBuilder.PreloadCreatedDependencies(createdUserTable);
            }

            // Computed Column does not contain any Generate...Scripts Methods.
            // Identity Column does not contain any Generate...Scripts Methods.

            // Droppable UserTable
            foreach (var checkConstraint in
                droppableUserTable.CheckConstraints.Values.Where(
                    checkConstraint => dataDependencyBuilder.DroppedConstraints.Add(checkConstraint.Namespace)))
                        CheckConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
            foreach (var defaultConstraint in
                droppableUserTable.DefaultConstraints.Values.Where(
                    defaultConstraint => dataDependencyBuilder.DroppedConstraints.Add(defaultConstraint.Namespace)))
                        DefaultConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
            foreach (var foreignKey in
                droppableUserTable.ForeignKeys.Values.Where(
                    foreignKey => dataDependencyBuilder.DroppedForeignKeys.Add(foreignKey.Namespace)))
                        ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
            foreach (var index in
                droppableUserTable.Indexes.Values.Where(
                    index => dataDependencyBuilder.DroppedConstraints.Add(index.Namespace)))
                        Index.GenerateDropScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
            foreach (var primaryKey in
                droppableUserTable.PrimaryKeys.Values.Where(
                    primaryKey => dataDependencyBuilder.DroppedConstraints.Add(primaryKey.Namespace)))
                        PrimaryKey.GenerateDropScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
            foreach (var uniqueConstraint in
                droppableUserTable.UniqueConstraints.Values.Where(
                    uniqueConstraint => dataDependencyBuilder.DroppedConstraints.Add(uniqueConstraint.Namespace)))
                        UniqueConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
            foreach (var userTableColumn in
                droppableUserTable.UserTableColumns.Values.Where(
                    userTableColumn => dataDependencyBuilder.DroppedConstraints.Add(userTableColumn.Namespace)))
                        UserTableColumn.GenerateDropScripts(sourceDataContext, targetDataContext, userTableColumn, dataSyncActions, dataProperties);

            // Addable UserTable
            foreach (var checkConstraint in
                addableUserTable.CheckConstraints.Values.Where(
                    checkConstraint => dataDependencyBuilder.CreatedConstraints.Add(checkConstraint.Namespace)))
                        CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
            foreach (var defaultConstraint in
                addableUserTable.DefaultConstraints.Values.Where(
                    defaultConstraint => dataDependencyBuilder.CreatedConstraints.Add(defaultConstraint.Namespace)))
                        DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
            foreach (var foreignKey in
                addableUserTable.ForeignKeys.Values.Where(
                    foreignKey => dataDependencyBuilder.CreatedForeignKeys.Add(foreignKey.Namespace)))
                        ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
            foreach (var index in
                addableUserTable.Indexes.Values.Where(
                    index => dataDependencyBuilder.CreatedConstraints.Add(index.Namespace)))
                        Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties); 
            foreach (var primaryKey in
                addableUserTable.PrimaryKeys.Values.Where(
                    primaryKey => dataDependencyBuilder.CreatedConstraints.Add(primaryKey.Namespace)))
                        PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);  
            foreach (var uniqueConstraint in
                addableUserTable.UniqueConstraints.Values.Where(
                    uniqueConstraint => dataDependencyBuilder.CreatedConstraints.Add(uniqueConstraint.Namespace)))
                        UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);        
            foreach (var userTableColumn in
                addableUserTable.UserTableColumns.Values.Where(
                    userTableColumn => dataDependencyBuilder.CreatedConstraints.Add(userTableColumn.Namespace)))
                        UserTableColumn.GenerateCreateScripts(sourceDataContext, targetDataContext, userTableColumn, dataSyncActions, dataProperties);             

            // Alterable UserTable
            foreach (var checkConstraint in alterableUserTable.CheckConstraints.Values)
            {
                if (dataDependencyBuilder.DroppedConstraints.Add(checkConstraint.Namespace))
                    CheckConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
                if (dataDependencyBuilder.CreatedConstraints.Add(checkConstraint.Namespace))
                    CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
            }
            foreach (var defaultConstraint in alterableUserTable.DefaultConstraints.Values)
            {
                if (dataDependencyBuilder.DroppedConstraints.Add(defaultConstraint.Namespace))
                    DefaultConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
                if (dataDependencyBuilder.CreatedConstraints.Add(defaultConstraint.Namespace))
                    DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
            }
            foreach (var foreignKey in alterableUserTable.ForeignKeys.Values)
            {
                if (dataDependencyBuilder.DroppedForeignKeys.Add(foreignKey.Namespace))
                    ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
                if (dataDependencyBuilder.CreatedForeignKeys.Add(foreignKey.Namespace))
                    ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
            }
            foreach (var index in alterableUserTable.Indexes.Values)
            {
                if (dataDependencyBuilder.DroppedConstraints.Add(index.Namespace))
                    Index.GenerateDropScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
                if (dataDependencyBuilder.CreatedConstraints.Add(index.Namespace))
                    Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
            }
            foreach (var primaryKey in alterableUserTable.PrimaryKeys.Values)
            {
                if (dataDependencyBuilder.DroppedConstraints.Add(primaryKey.Namespace))
                    PrimaryKey.GenerateDropScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
                if (dataDependencyBuilder.CreatedConstraints.Add(primaryKey.Namespace))
                    PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
            }
            foreach (var uniqueConstraint in alterableUserTable.UniqueConstraints.Values)
            {
                if (dataDependencyBuilder.DroppedConstraints.Add(uniqueConstraint.Namespace))
                    UniqueConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
                if (dataDependencyBuilder.CreatedConstraints.Add(uniqueConstraint.Namespace))
                    UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
            }
            foreach (var userTableColumn in alterableUserTable.UserTableColumns.Values)
            {
                UserTableColumn sourceUserTableColumn;
                UserTableColumn targetUserTableColumn;

                sourceUserTable.UserTableColumns.TryGetValue(userTableColumn.ObjectName, out sourceUserTableColumn);
                targetUserTable.UserTableColumns.TryGetValue(userTableColumn.ObjectName, out targetUserTableColumn);
                
                UserTableColumn.GenerateAlterScripts(sourceDataContext, targetDataContext, userTableColumn, sourceUserTableColumn, targetUserTableColumn, dataSyncActions, dataProperties);
            }
            
            // Matched UserTable
            if (matchedUserTable != null)
            {
                // For safety we are dropping and re-adding all constraints instead of checking
                // their inter-dependency for if the constraint exists on the same column as
                // a primary key, etc...
                foreach (var checkConstraint in matchedUserTable.CheckConstraints.Values)
                {
                    if (dataDependencyBuilder.DroppedConstraints.Add(checkConstraint.Namespace))
                        CheckConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
                    if (dataDependencyBuilder.CreatedConstraints.Add(checkConstraint.Namespace))
                        CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
                }
                foreach (var defaultConstraint in matchedUserTable.DefaultConstraints.Values)
                {
                    if (dataDependencyBuilder.DroppedConstraints.Add(defaultConstraint.Namespace))
                        DefaultConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
                    if (dataDependencyBuilder.CreatedConstraints.Add(defaultConstraint.Namespace))
                        DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
                }
                foreach (var foreignKey in matchedUserTable.ForeignKeys.Values)
                {
                    if (dataDependencyBuilder.DroppedForeignKeys.Add(foreignKey.Namespace))
                        ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
                    if (dataDependencyBuilder.CreatedForeignKeys.Add(foreignKey.Namespace))
                        ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
                }
                foreach (var index in matchedUserTable.Indexes.Values)
                {
                    if (dataDependencyBuilder.DroppedConstraints.Add(index.Namespace))
                        Index.GenerateDropScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
                    if (dataDependencyBuilder.CreatedConstraints.Add(index.Namespace))
                        Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
                }
                foreach (var primaryKey in matchedUserTable.PrimaryKeys.Values)
                {
                    if (dataDependencyBuilder.DroppedConstraints.Add(primaryKey.Namespace))
                        PrimaryKey.GenerateDropScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
                    if (dataDependencyBuilder.CreatedConstraints.Add(primaryKey.Namespace))
                        PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
                }
                foreach (var uniqueConstraint in matchedUserTable.UniqueConstraints.Values)
                {
                    if (dataDependencyBuilder.DroppedConstraints.Add(uniqueConstraint.Namespace))
                        UniqueConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
                    if (dataDependencyBuilder.CreatedConstraints.Add(uniqueConstraint.Namespace))
                        UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
                }
            }

            // Ensure drops and creates of foreign keys happens to all tables that also reference this table.
            List<ForeignKey> targetReferencingForeignKeys;
            if (targetUserTable.Catalog.ReferencedUserTablePool.TryGetValue(alterableUserTable.Namespace, out targetReferencingForeignKeys))
            {
                foreach (var targetReferencingForeignKey in
                    targetReferencingForeignKeys.Where(
                        targetReferencingForeignKey => dataDependencyBuilder.DroppedForeignKeys.Add(targetReferencingForeignKey.Namespace)))
                    ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, targetReferencingForeignKey, dataSyncActions, dataProperties);
            }

            List<ForeignKey> sourceReferencingForeignKeys;
            if (!sourceUserTable.Catalog.ReferencedUserTablePool.TryGetValue(alterableUserTable.Namespace, out sourceReferencingForeignKeys))
                return;

            foreach (var sourceReferencingForeignKey in
                sourceReferencingForeignKeys.Where(
                    sourceReferencingForeignKey => dataDependencyBuilder.CreatedForeignKeys.Add(sourceReferencingForeignKey.Namespace)))
            {
                var foreignKeyClone = ForeignKey.Clone(sourceReferencingForeignKey);
                foreignKeyClone.UserTable = targetUserTable;
                ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, sourceReferencingForeignKey, dataSyncActions, dataProperties);
            }
        }

        public static void GenerateCreateScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserTable userTable, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (DataProperties.IgnoredUserTables.Contains(userTable.Namespace))
                return;

            var dataSyncAction = DataActionFactory.CreateUserTable(sourceDataContext, targetDataContext, userTable);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
                
            // Computed Column does not contain any Generate...Scripts Methods.
            // Identity Column does not contain any Generate...Scripts Methods.
            foreach (var checkConstraint in userTable.CheckConstraints.Values)
                CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
            foreach (var defaultConstraint in userTable.DefaultConstraints.Values)
                DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
            foreach (var foreignKey in userTable.ForeignKeys.Values)
                ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
            foreach (var index in userTable.Indexes.Values)
                Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
            foreach (var primaryKey in userTable.PrimaryKeys.Values)
                PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
            foreach (var uniqueConstraint in userTable.UniqueConstraints.Values)
                UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        }

        public static void GenerateDropScripts(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserTable userTable, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        {
            if (!dataProperties.TightSync)
                return;

            if (DataProperties.IgnoredUserTables.Contains(userTable.Namespace))
                return;

            var dataSyncAction = DataActionFactory.DropUserTable(sourceDataContext, targetDataContext, userTable);
            if (dataSyncAction != null)
                dataSyncActions.Add(dataSyncAction);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Serialize Members
            info.AddValue("ObjectName", ObjectName);
            info.AddValue("Description", Description);
            info.AddValue("FileStreamFileGroup", FileStreamFileGroup);
            info.AddValue("HasTextNTextOrImageColumns", HasTextNTextOrImageColumns);
            info.AddValue("LobFileGroup", LobFileGroup);
            info.AddValue("TextInRowLimit", TextInRowLimit);
            info.AddValue("UsesAnsiNulls", UsesAnsiNulls);

            // Serialize Check Constraints
            info.AddValue("CheckConstraints", CheckConstraints.Count);

            var i = 0;
            foreach (var checkConstraint in CheckConstraints.Values)
                info.AddValue("CheckConstraint" + i++, checkConstraint);

            // Serialize Computed Columns
            info.AddValue("ComputedColumns", ComputedColumns.Count);

            i = 0;
            foreach (var computedColumn in ComputedColumns.Values)
                info.AddValue("ComputedColumn" + i++, computedColumn);

            // Serialize Default Constraints
            info.AddValue("DefaultConstraints", DefaultConstraints.Count);

            i = 0;
            foreach (var defaultConstraint in DefaultConstraints.Values)
                info.AddValue("DefaultConstraint" + i++, defaultConstraint);

            // Serialize Foreign Keys
            info.AddValue("ForeignKeys", ForeignKeys.Count);

            i = 0;
            foreach (var foreignKey in ForeignKeys.Values)
                info.AddValue("ForeignKey" + i++, foreignKey);

            // Serialize Identity Columns
            info.AddValue("IdentityColumns", IdentityColumns.Count);

            i = 0;
            foreach (var identityColumn in IdentityColumns.Values)
                info.AddValue("IdentityColumn" + i++, identityColumn);

            // Serialize Indexes
            info.AddValue("Indexes", Indexes.Count);

            i = 0;
            foreach (var index in Indexes.Values)
                info.AddValue("Index" + i++, index);

            // Serialize Primary Keys
            info.AddValue("PrimaryKeys", PrimaryKeys.Count);

            i = 0;
            foreach (var primaryKey in PrimaryKeys.Values)
                info.AddValue("PrimaryKey" + i++, primaryKey);

            // Serialize Unique Constraints
            info.AddValue("UniqueConstraints", UniqueConstraints.Count);

            i = 0;
            foreach (var uniqueConstraint in UniqueConstraints.Values)
                info.AddValue("UniqueConstraint" + i++, uniqueConstraint);

            // Serialize User-Table Columns
            info.AddValue("UserTableColumns", UserTableColumns.Count);

            i = 0;
            foreach (var userTableColumn in UserTableColumns.Values)
                info.AddValue("UserTableColumn" + i++, userTableColumn);
        }

        /// <summary>
        /// Modifies the source UserTable to contain only objects that are
        /// present in the source UserTable and in the target UserTable.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUserTable">The source UserTable.</param>
        /// <param name="targetUserTable">The target UserTable.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void IntersectWith(
            DataContext sourceDataContext, DataContext targetDataContext,
            UserTable sourceUserTable, UserTable targetUserTable,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
            matchingCheckConstraints.IntersectWith(targetUserTable.CheckConstraints.Keys);

            removableCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
            removableCheckConstraints.ExceptWith(matchingCheckConstraints);

            foreach (var checkConstraint in removableCheckConstraints)
                RemoveCheckConstraint(sourceUserTable, checkConstraint);

            foreach (var checkConstraint in matchingCheckConstraints)
            {
                CheckConstraint sourceCheckConstraint;
                if (!sourceUserTable.CheckConstraints.TryGetValue(checkConstraint, out sourceCheckConstraint))
                    continue;

                CheckConstraint targetCheckConstraint;
                if (!targetUserTable.CheckConstraints.TryGetValue(checkConstraint, out targetCheckConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!CheckConstraint.CompareDefinitions(sourceCheckConstraint, targetCheckConstraint))
                            RemoveCheckConstraint(sourceUserTable, checkConstraint);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!CheckConstraint.CompareObjectNames(sourceCheckConstraint, targetCheckConstraint))
                            RemoveCheckConstraint(sourceUserTable, checkConstraint);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
            matchingComputedColumns.IntersectWith(targetUserTable.ComputedColumns.Keys);

            removableComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
            removableComputedColumns.ExceptWith(matchingComputedColumns);

            foreach (var computedColumn in removableComputedColumns)
                RemoveComputedColumn(sourceUserTable, computedColumn);

            foreach (var computedColumn in matchingComputedColumns)
            {
                ComputedColumn sourceComputedColumn;
                if (!sourceUserTable.ComputedColumns.TryGetValue(computedColumn, out sourceComputedColumn))
                    continue;

                ComputedColumn targetComputedColumn;
                if (!targetUserTable.ComputedColumns.TryGetValue(computedColumn, out targetComputedColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!ComputedColumn.CompareDefinitions(sourceComputedColumn, targetComputedColumn))
                            RemoveComputedColumn(sourceUserTable, computedColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!ComputedColumn.CompareObjectNames(sourceComputedColumn, targetComputedColumn))
                            RemoveComputedColumn(sourceUserTable, computedColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
            matchingDefaultConstraints.IntersectWith(targetUserTable.DefaultConstraints.Keys);

            removableDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
            removableDefaultConstraints.ExceptWith(matchingDefaultConstraints);

            foreach (var defaultConstraint in removableDefaultConstraints)
                RemoveDefaultConstraint(sourceUserTable, defaultConstraint);

            foreach (var defaultConstraint in matchingDefaultConstraints)
            {
                DefaultConstraint sourceDefaultConstraint;
                if (!sourceUserTable.DefaultConstraints.TryGetValue(defaultConstraint, out sourceDefaultConstraint))
                    continue;

                DefaultConstraint targetDefaultConstraint;
                if (!targetUserTable.DefaultConstraints.TryGetValue(defaultConstraint, out targetDefaultConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!DefaultConstraint.CompareDefinitions(sourceDefaultConstraint, targetDefaultConstraint))
                            RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!DefaultConstraint.CompareObjectNames(sourceDefaultConstraint, targetDefaultConstraint))
                            RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
            matchingIdentityColumns.IntersectWith(targetUserTable.IdentityColumns.Keys);

            removableIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
            removableIdentityColumns.ExceptWith(matchingIdentityColumns);

            foreach (var identityColumn in removableIdentityColumns)
                RemoveIdentityColumn(sourceUserTable, identityColumn);

            foreach (var identityColumn in matchingIdentityColumns)
            {
                IdentityColumn sourceIdentityColumn;
                if (!sourceUserTable.IdentityColumns.TryGetValue(identityColumn, out sourceIdentityColumn))
                    continue;

                IdentityColumn targetIdentityColumn;
                if (!targetUserTable.IdentityColumns.TryGetValue(identityColumn, out targetIdentityColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!IdentityColumn.CompareDefinitions(sourceIdentityColumn, targetIdentityColumn))
                            RemoveIdentityColumn(sourceUserTable, identityColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!IdentityColumn.CompareObjectNames(sourceIdentityColumn, targetIdentityColumn))
                            RemoveIdentityColumn(sourceUserTable, identityColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
            matchingForeignKeys.IntersectWith(targetUserTable.ForeignKeys.Keys);

            removableForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
            removableForeignKeys.ExceptWith(matchingForeignKeys);

            foreach (var foreignKey in removableForeignKeys)
                RemoveForeignKey(sourceUserTable, foreignKey);

            foreach (var foreignKey in matchingForeignKeys)
            {
                ForeignKey sourceForeignKey;
                if (!sourceUserTable.ForeignKeys.TryGetValue(foreignKey, out sourceForeignKey))
                    continue;

                ForeignKey targetForeignKey;
                if (!targetUserTable.ForeignKeys.TryGetValue(foreignKey, out targetForeignKey))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (ForeignKey.CompareDefinitions(sourceForeignKey, targetForeignKey))
                        {
                            ForeignKey.IntersectWith(sourceForeignKey, targetForeignKey, dataComparisonType);
                            if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
                                RemoveForeignKey(sourceUserTable, foreignKey);
                        }
                        else
                        {
                            RemoveForeignKey(sourceUserTable, foreignKey);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (ForeignKey.CompareObjectNames(sourceForeignKey, targetForeignKey))
                        {
                            ForeignKey.IntersectWith(sourceForeignKey, targetForeignKey, dataComparisonType);
                            if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
                                RemoveForeignKey(sourceUserTable, foreignKey);
                        }
                        else
                        {
                            RemoveForeignKey(sourceUserTable, foreignKey);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIndexes.UnionWith(sourceUserTable.Indexes.Keys);
            matchingIndexes.IntersectWith(targetUserTable.Indexes.Keys);

            removableIndexes.UnionWith(sourceUserTable.Indexes.Keys);
            removableIndexes.ExceptWith(matchingIndexes);

            foreach (var index in removableIndexes)
                RemoveIndex(sourceUserTable, index);

            foreach (var index in matchingIndexes)
            {
                Index sourceIndex;
                if (!sourceUserTable.Indexes.TryGetValue(index, out sourceIndex))
                    continue;

                Index targetIndex;
                if (!targetUserTable.Indexes.TryGetValue(index, out targetIndex))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Index.CompareDefinitions(sourceIndex, targetIndex))
                        {
                            Index.IntersectWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
                            if (Index.ObjectCount(sourceIndex) == 0)
                                RemoveIndex(sourceUserTable, index);
                        }
                        else
                        {
                            RemoveIndex(sourceUserTable, index);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (Index.CompareObjectNames(sourceIndex, targetIndex))
                        {
                            Index.IntersectWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
                            if (Index.ObjectCount(sourceIndex) == 0)
                                RemoveIndex(sourceUserTable, index);
                        }
                        else
                        {
                            RemoveIndex(sourceUserTable, index);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingPrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removablePrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingPrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
            matchingPrimaryKeys.IntersectWith(targetUserTable.PrimaryKeys.Keys);

            removablePrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
            removablePrimaryKeys.ExceptWith(matchingPrimaryKeys);

            foreach (var primaryKey in removablePrimaryKeys)
                RemovePrimaryKey(sourceUserTable, primaryKey);

            foreach (var primaryKey in matchingPrimaryKeys)
            {
                PrimaryKey sourcePrimaryKey;
                if (!sourceUserTable.PrimaryKeys.TryGetValue(primaryKey, out sourcePrimaryKey))
                    continue;

                PrimaryKey targetPrimaryKey;
                if (!targetUserTable.PrimaryKeys.TryGetValue(primaryKey, out targetPrimaryKey))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (PrimaryKey.CompareDefinitions(sourcePrimaryKey, targetPrimaryKey))
                        {
                            PrimaryKey.IntersectWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
                            if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
                                RemovePrimaryKey(sourceUserTable, primaryKey);
                        }
                        else
                        {
                            RemovePrimaryKey(sourceUserTable, primaryKey);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (PrimaryKey.CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
                        {
                            PrimaryKey.IntersectWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
                            if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
                                RemovePrimaryKey(sourceUserTable, primaryKey);
                        }
                        else
                        {
                            RemovePrimaryKey(sourceUserTable, primaryKey);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
            matchingUniqueConstraints.IntersectWith(targetUserTable.UniqueConstraints.Keys);

            removableUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
            removableUniqueConstraints.ExceptWith(matchingUniqueConstraints);

            foreach (var uniqueConstraint in removableUniqueConstraints)
                RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);

            foreach (var uniqueConstraint in matchingUniqueConstraints)
            {
                UniqueConstraint sourceUniqueConstraint;
                if (!sourceUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out sourceUniqueConstraint))
                    continue;

                UniqueConstraint targetUniqueConstraint;
                if (!targetUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out targetUniqueConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UniqueConstraint.CompareDefinitions(sourceUniqueConstraint, targetUniqueConstraint))
                        {
                            UniqueConstraint.IntersectWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
                            if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
                                RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
                        }
                        else
                        {
                            RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
                        }
                        break;
                    case DataComparisonType.Namespaces:
                        if (UniqueConstraint.CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
                        {
                            UniqueConstraint.IntersectWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
                            if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
                                RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
                        }
                        else
                        {
                            RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
                        }
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var removableUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
            matchingUserTableColumns.IntersectWith(targetUserTable.UserTableColumns.Keys);

            removableUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
            removableUserTableColumns.ExceptWith(matchingUserTableColumns);

            foreach (var userTableColumn in removableUserTableColumns)
                RemoveUserTableColumn(sourceUserTable, userTableColumn);

            foreach (var userTableColumn in matchingUserTableColumns)
            {
                UserTableColumn sourceUserTableColumn;
                if (!sourceUserTable.UserTableColumns.TryGetValue(userTableColumn, out sourceUserTableColumn))
                    continue;

                UserTableColumn targetUserTableColumn;
                if (!targetUserTable.UserTableColumns.TryGetValue(userTableColumn, out targetUserTableColumn))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (!UserTableColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUserTableColumn, targetUserTableColumn))
                            RemoveUserTableColumn(sourceUserTable, userTableColumn);
                        break;
                    case DataComparisonType.Namespaces:
                        if (!UserTableColumn.CompareObjectNames(sourceUserTableColumn, targetUserTableColumn))
                            RemoveUserTableColumn(sourceUserTable, userTableColumn);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }
        }

        /// <summary>
        /// This method is called automatically through a chain of calls after Catalog.Clone()
        /// method has been called and will simply return if the Catalog... userTable.Catalog...
        /// is equal to null. This method has been added to assist in populating
        /// Catalog.ForeignKeyPool and Catalog.ReferencedUserTablePool in case anything custom
        /// has to be done for any reason to those lists. UserTable.AddForeignKey automatically
        /// calls ForeignKey.LinkForeignKey and this method only needs to be used if a custom
        /// cloning method is created or after a serialization operation has completed and the
        /// calls should only be channeled through the chain of objects originating from the
        /// Catalog object as no action will take place unless the object you are passing in
        /// has a Catalog and if the foreign keys do not already exist in those lists.
        /// </summary>
        /// <param name="userTable">The user-table with foreign keys to link.</param>
        public static void LinkForeignKeys(UserTable userTable)
        {
            if (userTable.Catalog == null)
                return;

            foreach (var foreignKey in userTable.ForeignKeys.Values)
                ForeignKey.LinkForeignKey(foreignKey);
        }

        public static long ObjectCount(UserTable userTable, bool deepCount = false)
        {
            var count = (long)userTable.CheckConstraints.Count;
            count += userTable.ComputedColumns.Count;
            count += userTable.DefaultConstraints.Count;
            count += userTable.ForeignKeys.Count;
            count += userTable.IdentityColumns.Count;
            count += userTable.Indexes.Count;
            count += userTable.PrimaryKeys.Count;
            count += userTable.UserTableColumns.Count;
            count += userTable.UniqueConstraints.Count;

            if (!deepCount)
                return count;

            return count +
                   userTable.ForeignKeys.Values.Sum(
                       foreignKey => ForeignKey.ObjectCount(foreignKey)) +
                   userTable.Indexes.Values.Sum(
                       index => Index.ObjectCount(index)) +
                   userTable.PrimaryKeys.Values.Sum(
                       primaryKey => PrimaryKey.ObjectCount(primaryKey)) +
                   userTable.UniqueConstraints.Values.Sum(
                       uniqueConstraint => UniqueConstraint.ObjectCount(uniqueConstraint));
        }

        public static bool RemoveCheckConstraint(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.CheckConstraints.Remove(objectName);
        }

        public static bool RemoveCheckConstraint(UserTable userTable, CheckConstraint checkConstraint)
        {
            CheckConstraint checkConstraintObject;
            if (!userTable.CheckConstraints.TryGetValue(checkConstraint.ObjectName, out checkConstraintObject))
                return false;

            return checkConstraint.Equals(checkConstraintObject) &&
                   userTable.CheckConstraints.Remove(checkConstraint.ObjectName);
        }

        public static bool RemoveComputedColumn(UserTable userTable, string objectName)
        {

            return !string.IsNullOrEmpty(objectName)
                   && userTable.ComputedColumns.Remove(objectName);
        }

        public static bool RemoveComputedColumn(UserTable userTable, ComputedColumn computedColumn)
        {
            ComputedColumn computedColumnObject;
            if (!userTable.ComputedColumns.TryGetValue(computedColumn.ObjectName, out computedColumnObject))
                return false;

            return computedColumn.Equals(computedColumnObject)
                   && userTable.ComputedColumns.Remove(computedColumn.ObjectName);
        }

        public static bool RemoveDefaultConstraint(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.DefaultConstraints.Remove(objectName);
        }

        public static bool RemoveDefaultConstraint(UserTable userTable, DefaultConstraint defaultConstraint)
        {
            DefaultConstraint defaultConstraintObject;
            if (!userTable.DefaultConstraints.TryGetValue(defaultConstraint.ObjectName, out defaultConstraintObject))
                return false;

            return defaultConstraint.Equals(defaultConstraintObject) &&
                   userTable.DefaultConstraints.Remove(defaultConstraint.ObjectName);
        }

        public static bool RemoveForeignKey(UserTable userTable, string objectName)
        {
            ForeignKey foreignKey;
            if (!userTable.ForeignKeys.TryGetValue(objectName, out foreignKey))
                return false;

            if (userTable.Catalog != null)
                ForeignKey.UnlinkForeignKey(foreignKey);

            return !string.IsNullOrEmpty(objectName)
                && userTable.ForeignKeys.Remove(objectName);
        }

        public static bool RemoveForeignKey(UserTable userTable, ForeignKey foreignKey)
        {
            ForeignKey foreignKeyObject;
            if (!userTable.ForeignKeys.TryGetValue(foreignKey.ObjectName, out foreignKeyObject))
                return false;

            if (userTable.Catalog != null)
                ForeignKey.UnlinkForeignKey(foreignKey);

            return foreignKey.Equals(foreignKeyObject) &&
                   userTable.ForeignKeys.Remove(foreignKey.ObjectName);
        }

        public static bool RemoveIdentityColumn(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.IdentityColumns.Remove(objectName);
        }

        public static bool RemoveIdentityColumn(UserTable userTable, IdentityColumn identityColumn)
        {
            IdentityColumn identityColumnObject;
            if (!userTable.IdentityColumns.TryGetValue(identityColumn.ObjectName, out identityColumnObject))
                return false;

            return identityColumn.Equals(identityColumnObject) &&
                   userTable.ForeignKeys.Remove(identityColumn.ObjectName);
        }

        public static bool RemoveIndex(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.Indexes.Remove(objectName);
        }

        public static bool RemoveIndex(UserTable userTable, Index index)
        {
            Index indexObject;
            if (!userTable.Indexes.TryGetValue(index.ObjectName, out indexObject))
                return false;

            return index.Equals(indexObject) &&
                   userTable.ForeignKeys.Remove(index.ObjectName);
        }

        public static bool RemovePrimaryKey(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.PrimaryKeys.Remove(objectName);
        }

        public static bool RemovePrimaryKey(UserTable userTable, PrimaryKey primaryKey)
        {
            PrimaryKey primaryKeyObject;
            if (!userTable.PrimaryKeys.TryGetValue(primaryKey.ObjectName, out primaryKeyObject))
                return false;

            return primaryKey.Equals(primaryKeyObject) &&
                   userTable.PrimaryKeys.Remove(primaryKey.ObjectName);
        }

        public static bool RemoveUniqueConstraint(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.UniqueConstraints.Remove(objectName);
        }

        public static bool RemoveUniqueConstraint(UserTable userTable, UniqueConstraint uniqueConstraint)
        {
            UniqueConstraint uniqueConstraintObject;
            if (!userTable.UniqueConstraints.TryGetValue(uniqueConstraint.ObjectName, out uniqueConstraintObject))
                return false;

            return uniqueConstraint.Equals(uniqueConstraintObject) &&
                   userTable.UniqueConstraints.Remove(uniqueConstraint.ObjectName);
        }

        public static bool RemoveUserTableColumn(UserTable userTable, string objectName)
        {
            return !string.IsNullOrEmpty(objectName)
                && userTable.UserTableColumns.Remove(objectName);
        }

        public static bool RemoveUserTableColumn(UserTable userTable, UserTableColumn userTableColumn)
        {
            UserTableColumn userTableColumnObject;
            if (!userTable.UserTableColumns.TryGetValue(userTableColumn.ObjectName, out userTableColumnObject))
                return false;

            return userTableColumn.Equals(userTableColumnObject) &&
                   userTable.UserTableColumns.Remove(userTableColumn.ObjectName);
        }

        public static bool RenameCheckConstraint(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.CheckConstraints.ContainsKey(newObjectName))
                return false;
            
            CheckConstraint checkConstraint;
            if (!userTable.CheckConstraints.TryGetValue(objectName, out checkConstraint))
                return false;

            userTable.CheckConstraints.Remove(objectName);
            checkConstraint.UserTable = null;
            checkConstraint.ObjectName = newObjectName;
            checkConstraint.UserTable = userTable;
            userTable.CheckConstraints.Add(newObjectName, checkConstraint);
            
            return true;
        }

        public static bool RenameComputedColumn(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.ComputedColumns.ContainsKey(newObjectName))
                return false;
            
            ComputedColumn computedColumn;
            if (!userTable.ComputedColumns.TryGetValue(objectName, out computedColumn))
                return false;

            userTable.ComputedColumns.Remove(objectName);
            computedColumn.UserTable = null;
            computedColumn.ObjectName = newObjectName;
            computedColumn.UserTable = userTable;
            userTable.ComputedColumns.Add(newObjectName, computedColumn);
            
            return true;
        }

        public static bool RenameDefaultConstraint(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.DefaultConstraints.ContainsKey(newObjectName))
                return false;
            
            DefaultConstraint defaultConstraint;
            if (!userTable.DefaultConstraints.TryGetValue(objectName, out defaultConstraint))
                return false;

            userTable.DefaultConstraints.Remove(objectName);
            defaultConstraint.UserTable = null;
            defaultConstraint.ObjectName = newObjectName;
            defaultConstraint.UserTable = userTable;
            userTable.DefaultConstraints.Add(newObjectName, defaultConstraint);
            
            return true;
        }

        public static bool RenameForeignKey(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.ForeignKeys.ContainsKey(newObjectName))
                return false;
            
            ForeignKey foreignKey;
            if (!userTable.ForeignKeys.TryGetValue(objectName, out foreignKey))
                return false;

            userTable.ForeignKeys.Remove(objectName);
            foreignKey.UserTable = null;
            foreignKey.ObjectName = newObjectName;
            foreignKey.UserTable = userTable;
            userTable.ForeignKeys.Add(newObjectName, foreignKey);
            
            return true;
        }

        public static bool RenameIdentityColumn(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.IdentityColumns.ContainsKey(newObjectName))
                return false;
            
            IdentityColumn identityColumn;
            if (!userTable.IdentityColumns.TryGetValue(objectName, out identityColumn))
                return false;

            userTable.IdentityColumns.Remove(objectName);
            identityColumn.UserTable = null;
            identityColumn.ObjectName = newObjectName;
            identityColumn.UserTable = userTable;
            userTable.IdentityColumns.Add(newObjectName, identityColumn);
            
            return true;
        }

        public static bool RenameIndex(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.Indexes.ContainsKey(newObjectName))
                return false;
            
            Index index;
            if (!userTable.Indexes.TryGetValue(objectName, out index))
                return false;

            userTable.Indexes.Remove(objectName);
            index.UserTable = null;
            index.ObjectName = newObjectName;
            index.UserTable = userTable;
            userTable.Indexes.Add(newObjectName, index);
            
            return true;
        }

        public static bool RenamePrimaryKey(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.PrimaryKeys.ContainsKey(newObjectName))
                return false;
            
            PrimaryKey primaryKey;
            if (!userTable.PrimaryKeys.TryGetValue(objectName, out primaryKey))
                return false;

            userTable.PrimaryKeys.Remove(objectName);
            primaryKey.UserTable = null;
            primaryKey.ObjectName = newObjectName;
            primaryKey.UserTable = userTable;
            userTable.PrimaryKeys.Add(newObjectName, primaryKey);
            
            return true;
        }

        public static bool RenameUniqueConstraint(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.UniqueConstraints.ContainsKey(newObjectName))
                return false;
            
            UniqueConstraint uniqueConstraint;
            if (!userTable.UniqueConstraints.TryGetValue(objectName, out uniqueConstraint))
                return false;

            userTable.UniqueConstraints.Remove(objectName);
            uniqueConstraint.UserTable = null;
            uniqueConstraint.ObjectName = newObjectName;
            uniqueConstraint.UserTable = userTable;
            userTable.UniqueConstraints.Add(newObjectName, uniqueConstraint);
            
            return true;
        }

        public static bool RenameUserTableColumn(UserTable userTable, string objectName, string newObjectName)
        {
            if (string.IsNullOrEmpty(objectName))
                return false;

            if (string.IsNullOrEmpty(newObjectName))
                return false;

            if (userTable.UserTableColumns.ContainsKey(newObjectName))
                return false;
            
            UserTableColumn userTableColumn;
            if (!userTable.UserTableColumns.TryGetValue(objectName, out userTableColumn))
                return false;

            userTable.UserTableColumns.Remove(objectName);
            userTableColumn.UserTable = null;
            userTableColumn.ObjectName = newObjectName;
            userTableColumn.UserTable = userTable;
            userTable.UserTableColumns.Add(newObjectName, userTableColumn);
            
            return true;
        }

        /// <summary>
        /// Shallow Clone...
        /// A clone of this class's isntance specific metadata.
        /// </summary>
        /// <param name="userTable">The user-table to shallow clone.</param>
        /// <returns>A clone of this class's isntance specific metadata.</returns>
        public static UserTable ShallowClone(UserTable userTable)
        {
            return new UserTable(userTable.ObjectName)
                {
                    FileStreamFileGroup = userTable.FileStreamFileGroup,
                    HasTextNTextOrImageColumns = userTable.HasTextNTextOrImageColumns,
                    LobFileGroup = userTable.LobFileGroup,
                    TextInRowLimit = userTable.TextInRowLimit,
                    UsesAnsiNulls = userTable.UsesAnsiNulls
                };
        }

        public static string ToJson(UserTable userTable, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(userTable, formatting);
        }

        /// <summary>
        /// Modifies the source UserTable to contain all objects that are
        /// present in both iteself and the target UserTable.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceUserTable">The source UserTable.</param>
        /// <param name="targetUserTable">The target UserTable.</param>
        /// <param name="dataComparisonType">
        /// The completeness of comparisons between matching objects.
        /// </param>
        public static void UnionWith(UserTable sourceUserTable, UserTable targetUserTable,
            DataContext sourceDataContext, DataContext targetDataContext,
            DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        {
            var matchingCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
            matchingCheckConstraints.IntersectWith(targetUserTable.CheckConstraints.Keys);

            addableCheckConstraints.UnionWith(targetUserTable.CheckConstraints.Keys);
            addableCheckConstraints.ExceptWith(matchingCheckConstraints);

            foreach (var checkConstraint in addableCheckConstraints)
            {
                CheckConstraint targetCheckConstraint;
                if (!targetUserTable.CheckConstraints.TryGetValue(checkConstraint, out targetCheckConstraint))
                    continue;

                AddCheckConstraint(sourceUserTable, CheckConstraint.Clone(targetCheckConstraint));
            }

            var matchingComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
            matchingComputedColumns.IntersectWith(targetUserTable.ComputedColumns.Keys);

            addableComputedColumns.UnionWith(targetUserTable.ComputedColumns.Keys);
            addableComputedColumns.ExceptWith(matchingComputedColumns);

            foreach (var computedColumn in addableComputedColumns)
            {
                ComputedColumn targetComputedColumn;
                if (!targetUserTable.ComputedColumns.TryGetValue(computedColumn, out targetComputedColumn))
                    continue;

                AddComputedColumn(sourceUserTable, ComputedColumn.Clone(targetComputedColumn));
            }

            var matchingDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
            matchingDefaultConstraints.IntersectWith(targetUserTable.DefaultConstraints.Keys);

            addableDefaultConstraints.UnionWith(targetUserTable.DefaultConstraints.Keys);
            addableDefaultConstraints.ExceptWith(matchingDefaultConstraints);

            foreach (var defaultConstraint in addableDefaultConstraints)
            {
                DefaultConstraint targetDefaultConstraint;
                if (!targetUserTable.DefaultConstraints.TryGetValue(defaultConstraint, out targetDefaultConstraint))
                    continue;

                AddDefaultConstraint(sourceUserTable, DefaultConstraint.Clone(targetDefaultConstraint));
            }

            var matchingIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
            matchingIdentityColumns.IntersectWith(targetUserTable.IdentityColumns.Keys);

            addableIdentityColumns.UnionWith(targetUserTable.IdentityColumns.Keys);
            addableIdentityColumns.ExceptWith(matchingIdentityColumns);

            foreach (var identityColumn in addableIdentityColumns)
            {
                IdentityColumn targetIdentityColumn;
                if (!targetUserTable.IdentityColumns.TryGetValue(identityColumn, out targetIdentityColumn))
                    continue;

                AddIdentityColumn(sourceUserTable, IdentityColumn.Clone(targetIdentityColumn));
            }

            var matchingForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
            matchingForeignKeys.IntersectWith(targetUserTable.ForeignKeys.Keys);

            addableForeignKeys.UnionWith(targetUserTable.ForeignKeys.Keys);
            addableForeignKeys.ExceptWith(matchingForeignKeys);

            foreach (var foreignKey in addableForeignKeys)
            {
                ForeignKey targetForeignKey;
                if (!targetUserTable.ForeignKeys.TryGetValue(foreignKey, out targetForeignKey))
                    continue;

                AddForeignKey(sourceUserTable, ForeignKey.Clone(targetForeignKey));
            }

            foreach (var foreignKey in matchingForeignKeys)
            {
                ForeignKey sourceForeignKey;
                if (!sourceUserTable.ForeignKeys.TryGetValue(foreignKey, out sourceForeignKey))
                    continue;

                ForeignKey targetForeignKey;
                if (!targetUserTable.ForeignKeys.TryGetValue(foreignKey, out targetForeignKey))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (ForeignKey.CompareDefinitions(sourceForeignKey, targetForeignKey))
                            ForeignKey.UnionWith(sourceDataContext, targetDataContext, sourceForeignKey, targetForeignKey, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                        if (ForeignKey.CompareObjectNames(sourceForeignKey, targetForeignKey))
                            ForeignKey.UnionWith(sourceDataContext, targetDataContext, sourceForeignKey, targetForeignKey, dataComparisonType);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingIndexes.UnionWith(sourceUserTable.Indexes.Keys);
            matchingIndexes.IntersectWith(targetUserTable.Indexes.Keys);

            addableIndexes.UnionWith(targetUserTable.Indexes.Keys);
            addableIndexes.ExceptWith(matchingIndexes);

            foreach (var index in addableIndexes)
            {
                Index targetIndex;
                if (!targetUserTable.Indexes.TryGetValue(index, out targetIndex))
                    continue;

                AddIndex(sourceUserTable, Index.Clone(targetIndex));
            }

            foreach (var index in matchingIndexes)
            {
                Index sourceIndex;
                if (!sourceUserTable.Indexes.TryGetValue(index, out sourceIndex))
                    continue;

                Index targetIndex;
                if (!targetUserTable.Indexes.TryGetValue(index, out targetIndex))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (Index.CompareDefinitions(sourceIndex, targetIndex))
                            Index.UnionWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                        if (Index.CompareObjectNames(sourceIndex, targetIndex))
                            Index.UnionWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingPrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addablePrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingPrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
            matchingPrimaryKeys.IntersectWith(targetUserTable.PrimaryKeys.Keys);

            addablePrimaryKeys.UnionWith(targetUserTable.PrimaryKeys.Keys);
            addablePrimaryKeys.ExceptWith(matchingPrimaryKeys);

            foreach (var primaryKey in addablePrimaryKeys)
            {
                PrimaryKey targetPrimaryKey;
                if (!targetUserTable.PrimaryKeys.TryGetValue(primaryKey, out targetPrimaryKey))
                    continue;

                AddPrimaryKey(sourceUserTable, PrimaryKey.Clone(targetPrimaryKey));
            }

            foreach (var primaryKey in matchingPrimaryKeys)
            {
                PrimaryKey sourcePrimaryKey;
                if (!sourceUserTable.PrimaryKeys.TryGetValue(primaryKey, out sourcePrimaryKey))
                    continue;

                PrimaryKey targetPrimaryKey;
                if (!targetUserTable.PrimaryKeys.TryGetValue(primaryKey, out targetPrimaryKey))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (PrimaryKey.CompareDefinitions(sourcePrimaryKey, targetPrimaryKey))
                            PrimaryKey.UnionWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                        if (PrimaryKey.CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
                            PrimaryKey.UnionWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
            matchingUniqueConstraints.IntersectWith(targetUserTable.UniqueConstraints.Keys);

            addableUniqueConstraints.UnionWith(targetUserTable.UniqueConstraints.Keys);
            addableUniqueConstraints.ExceptWith(matchingUniqueConstraints);

            foreach (var uniqueConstraint in addableUniqueConstraints)
            {
                UniqueConstraint targetUniqueConstraint;
                if (!targetUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out targetUniqueConstraint))
                    continue;

                AddUniqueConstraint(sourceUserTable, UniqueConstraint.Clone(targetUniqueConstraint));
            }

            foreach (var uniqueConstraint in matchingUniqueConstraints)
            {
                UniqueConstraint sourceUniqueConstraint;
                if (!sourceUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out sourceUniqueConstraint))
                    continue;

                UniqueConstraint targetUniqueConstraint;
                if (!targetUserTable.UniqueConstraints.TryGetValue(uniqueConstraint, out targetUniqueConstraint))
                    continue;

                switch (dataComparisonType)
                {
                    case DataComparisonType.Definitions:
                        if (UniqueConstraint.CompareDefinitions(sourceUniqueConstraint, targetUniqueConstraint))
                            UniqueConstraint.UnionWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
                        break;
                    case DataComparisonType.Namespaces:
                        if (UniqueConstraint.CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
                            UniqueConstraint.UnionWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
                        break;
                    //case DataComparisonType.SchemaLevelNamespaces:
                    //    // Do Nothing
                    //    break;
                }
            }

            var matchingUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var addableUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            matchingUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
            matchingUserTableColumns.IntersectWith(targetUserTable.UserTableColumns.Keys);

            addableUserTableColumns.UnionWith(targetUserTable.UserTableColumns.Keys);
            addableUserTableColumns.ExceptWith(matchingUserTableColumns);

            foreach (var userTableColumn in addableUserTableColumns)
            {
                UserTableColumn targetUserTableColumn;
                if (!targetUserTable.UserTableColumns.TryGetValue(userTableColumn, out targetUserTableColumn))
                    continue;

                var sourceUserTableColumn = UserTableColumn.Clone(targetUserTableColumn);
                DataTypes.ConvertDataType(targetDataContext, sourceDataContext, ref sourceUserTableColumn);
                AddUserTableColumn(sourceUserTable, sourceUserTableColumn);
            }
        }

		#endregion Public Methods 
		#region Private Methods (1) 

        private static void Init(UserTable userTable, Schema schema, string objectName, UserTablesRow userTablesRow)
        {
            userTable.CheckConstraints = new Dictionary<string, CheckConstraint>(StringComparer.OrdinalIgnoreCase);
            userTable.ComputedColumns = new Dictionary<string, ComputedColumn>(StringComparer.OrdinalIgnoreCase);
            userTable.DefaultConstraints = new Dictionary<string, DefaultConstraint>(StringComparer.OrdinalIgnoreCase);
            userTable.ForeignKeys = new Dictionary<string, ForeignKey>(StringComparer.OrdinalIgnoreCase);
            userTable.IdentityColumns = new Dictionary<string, IdentityColumn>(StringComparer.OrdinalIgnoreCase);
            userTable.Indexes = new Dictionary<string, Index>(StringComparer.OrdinalIgnoreCase);
            userTable.PrimaryKeys = new Dictionary<string, PrimaryKey>(StringComparer.OrdinalIgnoreCase);
            userTable.UniqueConstraints = new Dictionary<string, UniqueConstraint>(StringComparer.OrdinalIgnoreCase);
            userTable.UserTableColumns = new Dictionary<string, UserTableColumn>(StringComparer.OrdinalIgnoreCase);

            userTable.Schema = schema;
            userTable._objectName = string.IsNullOrEmpty(objectName)
                ? "Default"
                : objectName;

            userTable.Description = "User-Table";
            
            if (userTablesRow == null)
            {
                userTable.FileStreamFileGroup = "";
                userTable.HasTextNTextOrImageColumns = false;
                userTable.LobFileGroup = "";
                userTable.TextInRowLimit = 0;
                userTable.UsesAnsiNulls = true;
                return;
            }

            userTable.FileStreamFileGroup = userTablesRow.FileStreamFileGroup;
            userTable.HasTextNTextOrImageColumns = userTablesRow.HasTextNTextOrImageColumns;
            userTable.LobFileGroup = userTablesRow.LobFileGroup;
            userTable.TextInRowLimit = userTablesRow.TextInRowLimit;
            userTable.UsesAnsiNulls = userTablesRow.UsesAnsiNulls;
        }

		#endregion Private Methods 

		#endregion Methods 

        public DataSet GetDataset(DataConnectionManager connectionManager)
        {
            var command = connectionManager.DataConnectionInfo.CreateSelectAllCommand(this, connectionManager.DataConnection);
            var adapter = connectionManager.DataConnectionInfo.CreateDataAdapter();
            var dataset = new DataSet();
            adapter.SelectCommand = command;
            adapter.Fill(dataset);
            return dataset;
        }
    }
}
