using System;
using System.Linq;
using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class UserTable : SchemaBasedObject
    {
        public static readonly string DefaultDescription = "User-Table";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string FileStreamFileGroup { get; set; }
        [DataMember] public string LobFileGroup { get; set; }
        [DataMember] public bool HasTextNTextOrImageColumns { get; set; }
        [DataMember] public bool UsesAnsiNulls { get; set; }
        [DataMember] public int TextInRowLimit { get; set; }

        [DataMember] public DataObjectLookup<UserTable, CheckConstraint> CheckConstraints { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, ComputedColumn> ComputedColumns { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, DefaultConstraint> DefaultConstraints { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, PrimaryKey> PrimaryKeys { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, ForeignKey> ForeignKeys { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, IdentityColumn> IdentityColumns { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, Index> Indexes { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, UniqueConstraint> UniqueConstraints { get; private set; }
        [DataMember] public DataObjectLookup<UserTable, UserTableColumn> UserTableColumns { get; private set; }

        public UserTable()
        {
            FileStreamFileGroup = string.Empty;
            HasTextNTextOrImageColumns = false;
            LobFileGroup = string.Empty;
            TextInRowLimit = 0;
            UsesAnsiNulls = true;
            CheckConstraints = new DataObjectLookup<UserTable, CheckConstraint>(this);
            ComputedColumns = new DataObjectLookup<UserTable, ComputedColumn>(this);
            DefaultConstraints = new DataObjectLookup<UserTable, DefaultConstraint>(this);
            ForeignKeys = new DataObjectLookup<UserTable, ForeignKey>(this);
            IdentityColumns = new DataObjectLookup<UserTable, IdentityColumn>(this);
            Indexes = new DataObjectLookup<UserTable, Index>(this);
            PrimaryKeys = new DataObjectLookup<UserTable, PrimaryKey>(this);
            UniqueConstraints = new DataObjectLookup<UserTable, UniqueConstraint>(this);
            UserTableColumns = new DataObjectLookup<UserTable, UserTableColumn>(this);
        }

        public static void AddCheckConstraint(UserTable userTable, CheckConstraint checkConstraint)
        {
            if (checkConstraint.UserTable != null && !checkConstraint.UserTable.Equals(userTable))
                RemoveCheckConstraint(checkConstraint.UserTable, checkConstraint);

            userTable.CheckConstraints.Add(checkConstraint);
        }

        public static void AddComputedColumn(UserTable userTable, ComputedColumn computedColumn)
        {
            if (computedColumn.UserTable != null && !computedColumn.UserTable.Equals(userTable))
                RemoveComputedColumn(computedColumn.UserTable, computedColumn);

            userTable.ComputedColumns.Add(computedColumn);
        }

        public static void AddDefaultConstraint(UserTable userTable, DefaultConstraint defaultConstraint)
        {
            if (defaultConstraint.UserTable != null && !defaultConstraint.UserTable.Equals(userTable))
                RemoveDefaultConstraint(defaultConstraint.UserTable, defaultConstraint);

            userTable.DefaultConstraints.Add(defaultConstraint);
        }

        public static void AddForeignKey(UserTable userTable, ForeignKey foreignKey)
        {
            if (foreignKey.UserTable != null && !foreignKey.UserTable.Equals(userTable))
                RemoveForeignKey(foreignKey.UserTable, foreignKey);

            userTable.ForeignKeys.Add(foreignKey);
        }

        public static void AddIdentityColumn(UserTable userTable, IdentityColumn identityColumn)
        {
            if (identityColumn.UserTable != null && !identityColumn.UserTable.Equals(userTable))
                RemoveIdentityColumn(identityColumn.UserTable, identityColumn);

            userTable.IdentityColumns.Add(identityColumn);
        }

        public static void AddIndex(UserTable userTable, Index index)
        {
            if (index.UserTable != null && !index.UserTable.Equals(userTable))
                RemoveIndex(index.UserTable, index);

            userTable.Indexes.Add(index);
        }

        public static void AddPrimaryKey(UserTable userTable, PrimaryKey primaryKey)
        {
            if (primaryKey.UserTable != null && !primaryKey.UserTable.Equals(userTable))
                RemovePrimaryKey(primaryKey.UserTable, primaryKey);

            userTable.PrimaryKeys.Add(primaryKey);
        }

        public static void AddUniqueConstraint(UserTable userTable, UniqueConstraint uniqueConstraint)
        {
            if (uniqueConstraint.UserTable != null && !uniqueConstraint.UserTable.Equals(userTable))
                RemoveUniqueConstraint(uniqueConstraint.UserTable, uniqueConstraint);

            userTable.UniqueConstraints.Add(uniqueConstraint);
        }

        public static void AddUserTableColumn(UserTable userTable, UserTableColumn userTableColumn)
        {
            if (userTableColumn.UserTable != null && !userTableColumn.UserTable.Equals(userTable))
                RemoveUserTableColumn(userTableColumn.UserTable, userTableColumn);

            userTable.UserTableColumns.Add(userTableColumn);
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

            foreach (var foreignKey in userTable.ForeignKeys)
                ForeignKey.Clear(foreignKey);

            userTable.ForeignKeys.Clear();
            userTable.IdentityColumns.Clear();
            foreach (var index in userTable.Indexes)
                Index.Clear(index);
            userTable.Indexes.Clear();
            foreach (var primaryKey in userTable.PrimaryKeys)
                PrimaryKey.Clear(primaryKey);
            userTable.PrimaryKeys.Clear();
            foreach (var uniqueConstraint in userTable.UniqueConstraints)
                UniqueConstraint.Clear(uniqueConstraint);
            userTable.UniqueConstraints.Clear();
            userTable.UserTableColumns.Clear();
        }

        //public static bool CompareDefinitions(UserTable sourceUserTable, UserTable targetUserTable)
        //{
        //    if (!CompareObjectNames(sourceUserTable, targetUserTable))
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTable.FileStreamFileGroup, targetUserTable.FileStreamFileGroup) != 0)
        //        return false;

        //    if (sourceUserTable.HasTextNTextOrImageColumns != targetUserTable.HasTextNTextOrImageColumns)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserTable.LobFileGroup, targetUserTable.LobFileGroup) != 0)
        //        return false;

        //    if (sourceUserTable.TextInRowLimit != targetUserTable.TextInRowLimit)
        //        return false;

        //    return sourceUserTable.UsesAnsiNulls == targetUserTable.UsesAnsiNulls;
        //}

        //public static bool CompareObjectNames(UserTable sourceUserTable, UserTable targetUserTable, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceUserTable.ObjectName, targetUserTable.ObjectName) == 0;
        //}

        ///// <summary>
        ///// Removes all objects in the target UserTable from the source UserTable.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceUserTable">The source UserTable.</param>
        ///// <param name="targetUserTable">The target UserTable.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void ExceptWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTable sourceUserTable, UserTable targetUserTable,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
        //    matchingCheckConstraints.IntersectWith(targetUserTable.CheckConstraints.Keys);

        //    foreach (var checkConstraint in matchingCheckConstraints)
        //    {
        //        var sourceCheckConstraint = sourceUserTable.CheckConstraints[checkConstraint];
        //        if (sourceCheckConstraint == null)
        //            continue;

        //        var targetCheckConstraint = targetUserTable.CheckConstraints[checkConstraint];
        //        if (targetCheckConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (CheckConstraint.CompareDefinitions(sourceCheckConstraint, targetCheckConstraint))
        //                    RemoveCheckConstraint(sourceUserTable, checkConstraint);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (CheckConstraint.CompareObjectNames(sourceCheckConstraint, targetCheckConstraint))
        //                    RemoveCheckConstraint(sourceUserTable, checkConstraint);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
        //    matchingComputedColumns.IntersectWith(targetUserTable.ComputedColumns.Keys);

        //    foreach (var computedColumn in matchingComputedColumns)
        //    {
        //        var sourceComputedColumn = sourceUserTable.ComputedColumns[computedColumn];
        //        if (sourceComputedColumn == null)
        //            continue;

        //        var targetComputedColumn = targetUserTable.ComputedColumns[computedColumn];
        //        if (targetComputedColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (ComputedColumn.CompareDefinitions(sourceComputedColumn, targetComputedColumn))
        //                    RemoveComputedColumn(sourceUserTable, computedColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (ComputedColumn.CompareObjectNames(sourceComputedColumn, targetComputedColumn))
        //                    RemoveComputedColumn(sourceUserTable, computedColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
        //    matchingDefaultConstraints.IntersectWith(targetUserTable.DefaultConstraints.Keys);

        //    foreach (var defaultConstraint in matchingDefaultConstraints)
        //    {
        //        var sourceDefaultConstraint = sourceUserTable.DefaultConstraints[defaultConstraint];
        //        if (sourceDefaultConstraint == null)
        //            continue;

        //        var targetDefaultConstraint = targetUserTable.DefaultConstraints[defaultConstraint];
        //        if (targetDefaultConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (DefaultConstraint.CompareDefinitions(sourceDefaultConstraint, targetDefaultConstraint))
        //                    RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (DefaultConstraint.CompareObjectNames(sourceDefaultConstraint, targetDefaultConstraint))
        //                    RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
        //    matchingIdentityColumns.IntersectWith(targetUserTable.IdentityColumns.Keys);

        //    foreach (var identityColumn in matchingIdentityColumns)
        //    {
        //        var sourceIdentityColumn = sourceUserTable.IdentityColumns[identityColumn];
        //        if (sourceIdentityColumn == null)
        //            continue;

        //        var targetIdentityColumn = targetUserTable.IdentityColumns[identityColumn];
        //        if (targetIdentityColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (IdentityColumn.CompareDefinitions(sourceIdentityColumn, targetIdentityColumn))
        //                    RemoveIdentityColumn(sourceUserTable, identityColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (IdentityColumn.CompareObjectNames(sourceIdentityColumn, targetIdentityColumn))
        //                    RemoveIdentityColumn(sourceUserTable, identityColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
        //    matchingForeignKeys.IntersectWith(targetUserTable.ForeignKeys.Keys);

        //    foreach (var foreignKey in matchingForeignKeys)
        //    {
        //        var sourceForeignKey = sourceUserTable.ForeignKeys[foreignKey];
        //        if (sourceForeignKey == null)
        //            continue;

        //        var targetForeignKey = targetUserTable.ForeignKeys[foreignKey];
        //        if (targetForeignKey == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (ForeignKey.CompareDefinitions(sourceForeignKey, targetForeignKey))
        //                {
        //                    ForeignKey.ExceptWith(sourceForeignKey, targetForeignKey, dataComparisonType);
        //                    if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
        //                        RemoveForeignKey(sourceUserTable, foreignKey);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (ForeignKey.CompareObjectNames(sourceForeignKey, targetForeignKey))
        //                {
        //                    ForeignKey.ExceptWith(sourceForeignKey, targetForeignKey, dataComparisonType);
        //                    if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
        //                        RemoveForeignKey(sourceUserTable, foreignKey);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIndexes.UnionWith(sourceUserTable.Indexes.Keys);
        //    matchingIndexes.IntersectWith(targetUserTable.Indexes.Keys);

        //    foreach (var index in matchingIndexes)
        //    {
        //        var sourceIndex = sourceUserTable.Indexes[index];
        //        if (sourceIndex == null)
        //            continue;

        //        var targetIndex = targetUserTable.Indexes[index];
        //        if (targetIndex == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Index.CompareDefinitions(sourceIndex, targetIndex))
        //                {
        //                    Index.ExceptWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
        //                    if (Index.ObjectCount(sourceIndex) == 0)
        //                        RemoveIndex(sourceUserTable, index);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (Index.CompareObjectNames(sourceIndex, targetIndex))
        //                {
        //                    Index.ExceptWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
        //                    if (Index.ObjectCount(sourceIndex) == 0)
        //                        RemoveIndex(sourceUserTable, index);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingPrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingPrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
        //    matchingPrimaryKeys.IntersectWith(targetUserTable.PrimaryKeys.Keys);

        //    foreach (var primaryKey in matchingPrimaryKeys)
        //    {
        //        var sourcePrimaryKey = sourceUserTable.PrimaryKeys[primaryKey];
        //        if (sourcePrimaryKey == null)
        //            continue;

        //        var targetPrimaryKey = targetUserTable.PrimaryKeys[primaryKey];
        //        if (targetPrimaryKey == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (PrimaryKey.CompareDefinitions(sourcePrimaryKey, targetPrimaryKey))
        //                {
        //                    PrimaryKey.ExceptWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
        //                    if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
        //                        RemovePrimaryKey(sourceUserTable, primaryKey);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (PrimaryKey.CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
        //                {
        //                    PrimaryKey.ExceptWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
        //                    if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
        //                        RemovePrimaryKey(sourceUserTable, primaryKey);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
        //    matchingUniqueConstraints.IntersectWith(targetUserTable.UniqueConstraints.Keys);

        //    foreach (var uniqueConstraint in matchingUniqueConstraints)
        //    {
        //        var sourceUniqueConstraint = sourceUserTable.UniqueConstraints[uniqueConstraint];
        //        if (sourceUniqueConstraint == null)
        //            continue;

        //        var targetUniqueConstraint = targetUserTable.UniqueConstraints[uniqueConstraint];
        //        if (targetUniqueConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UniqueConstraint.CompareDefinitions(sourceUniqueConstraint, targetUniqueConstraint))
        //                {
        //                    UniqueConstraint.ExceptWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
        //                    if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
        //                        RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UniqueConstraint.CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
        //                {
        //                    UniqueConstraint.ExceptWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
        //                    if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
        //                        RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
        //    matchingUserTableColumns.IntersectWith(targetUserTable.UserTableColumns.Keys);

        //    foreach (var userTableColumn in matchingUserTableColumns)
        //    {
        //        var sourceUserTableColumn = sourceUserTable.UserTableColumns[userTableColumn];
        //        if (sourceUserTableColumn == null)
        //            continue;

        //        var targetUserTableColumn = targetUserTable.UserTableColumns[userTableColumn];
        //        if (targetUserTableColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UserTableColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUserTableColumn, targetUserTableColumn))
        //                    RemoveUserTableColumn(sourceUserTable, userTableColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UserTableColumn.CompareObjectNames(sourceUserTableColumn, targetUserTableColumn))
        //                    RemoveUserTableColumn(sourceUserTable, userTableColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTable alteredUserTable, UserTable addableUserTable, UserTable droppableUserTable,
        //    UserTable alterableUserTable, UserTable droppedUserTable, UserTable createdUserTable,
        //    UserTable sourceUserTable, UserTable targetUserTable, UserTable matchedUserTable,
        //    DataSyncActionsCollection dataSyncActions, DataDependencyBuilder dataDependencyBuilder, DataProperties dataProperties)
        //{
        //    // DataDependecyBuilder would only be null if the call did not
        //    // originate from GenerateAlterSchemas
        //    if (dataDependencyBuilder == null)
        //    {
        //        dataDependencyBuilder = new DataDependencyBuilder();

        //        if (droppedUserTable != null)
        //            dataDependencyBuilder.PreloadDroppedDependencies(droppedUserTable);

        //        if (createdUserTable != null)
        //            dataDependencyBuilder.PreloadCreatedDependencies(createdUserTable);
        //    }

        //    // Computed Column does not contain any Generate...Scripts Methods.
        //    // Identity Column does not contain any Generate...Scripts Methods.

        //    // Droppable UserTable
        //    foreach (var checkConstraint in
        //        droppableUserTable.CheckConstraints.Where(
        //            checkConstraint => dataDependencyBuilder.DroppedConstraints.Add(checkConstraint.Namespace)))
        //                CheckConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //    foreach (var defaultConstraint in
        //        droppableUserTable.DefaultConstraints.Where(
        //            defaultConstraint => dataDependencyBuilder.DroppedConstraints.Add(defaultConstraint.Namespace)))
        //                DefaultConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //    foreach (var foreignKey in
        //        droppableUserTable.ForeignKeys.Where(
        //            foreignKey => dataDependencyBuilder.DroppedForeignKeys.Add(foreignKey.Namespace)))
        //                ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //    foreach (var index in
        //        droppableUserTable.Indexes.Where(
        //            index => dataDependencyBuilder.DroppedConstraints.Add(index.Namespace)))
        //                Index.GenerateDropScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
        //    foreach (var primaryKey in
        //        droppableUserTable.PrimaryKeys.Where(
        //            primaryKey => dataDependencyBuilder.DroppedConstraints.Add(primaryKey.Namespace)))
        //                PrimaryKey.GenerateDropScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
        //    foreach (var uniqueConstraint in
        //        droppableUserTable.UniqueConstraints.Where(
        //            uniqueConstraint => dataDependencyBuilder.DroppedConstraints.Add(uniqueConstraint.Namespace)))
        //                UniqueConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        //    foreach (var userTableColumn in
        //        droppableUserTable.UserTableColumns.Where(
        //            userTableColumn => dataDependencyBuilder.DroppedConstraints.Add(userTableColumn.Namespace)))
        //                UserTableColumn.GenerateDropScripts(sourceDataContext, targetDataContext, userTableColumn, dataSyncActions, dataProperties);

        //    // Addable UserTable
        //    foreach (var checkConstraint in
        //        addableUserTable.CheckConstraints.Where(
        //            checkConstraint => dataDependencyBuilder.CreatedConstraints.Add(checkConstraint.Namespace)))
        //                CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //    foreach (var defaultConstraint in
        //        addableUserTable.DefaultConstraints.Where(
        //            defaultConstraint => dataDependencyBuilder.CreatedConstraints.Add(defaultConstraint.Namespace)))
        //                DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //    foreach (var foreignKey in
        //        addableUserTable.ForeignKeys.Where(
        //            foreignKey => dataDependencyBuilder.CreatedForeignKeys.Add(foreignKey.Namespace)))
        //                ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //    foreach (var index in
        //        addableUserTable.Indexes.Where(
        //            index => dataDependencyBuilder.CreatedConstraints.Add(index.Namespace)))
        //                Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties); 
        //    foreach (var primaryKey in
        //        addableUserTable.PrimaryKeys.Where(
        //            primaryKey => dataDependencyBuilder.CreatedConstraints.Add(primaryKey.Namespace)))
        //                PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);  
        //    foreach (var uniqueConstraint in
        //        addableUserTable.UniqueConstraints.Where(
        //            uniqueConstraint => dataDependencyBuilder.CreatedConstraints.Add(uniqueConstraint.Namespace)))
        //                UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);        
        //    foreach (var userTableColumn in
        //        addableUserTable.UserTableColumns.Where(
        //            userTableColumn => dataDependencyBuilder.CreatedConstraints.Add(userTableColumn.Namespace)))
        //                UserTableColumn.GenerateCreateScripts(sourceDataContext, targetDataContext, userTableColumn, dataSyncActions, dataProperties);             

        //    // Alterable UserTable
        //    foreach (var checkConstraint in alterableUserTable.CheckConstraints)
        //    {
        //        if (dataDependencyBuilder.DroppedConstraints.Add(checkConstraint.Namespace))
        //            CheckConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //        if (dataDependencyBuilder.CreatedConstraints.Add(checkConstraint.Namespace))
        //            CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //    }
        //    foreach (var defaultConstraint in alterableUserTable.DefaultConstraints)
        //    {
        //        if (dataDependencyBuilder.DroppedConstraints.Add(defaultConstraint.Namespace))
        //            DefaultConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //        if (dataDependencyBuilder.CreatedConstraints.Add(defaultConstraint.Namespace))
        //            DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //    }
        //    foreach (var foreignKey in alterableUserTable.ForeignKeys)
        //    {
        //        if (dataDependencyBuilder.DroppedForeignKeys.Add(foreignKey.Namespace))
        //            ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //        if (dataDependencyBuilder.CreatedForeignKeys.Add(foreignKey.Namespace))
        //            ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //    }
        //    foreach (var index in alterableUserTable.Indexes)
        //    {
        //        if (dataDependencyBuilder.DroppedConstraints.Add(index.Namespace))
        //            Index.GenerateDropScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
        //        if (dataDependencyBuilder.CreatedConstraints.Add(index.Namespace))
        //            Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
        //    }
        //    foreach (var primaryKey in alterableUserTable.PrimaryKeys)
        //    {
        //        if (dataDependencyBuilder.DroppedConstraints.Add(primaryKey.Namespace))
        //            PrimaryKey.GenerateDropScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
        //        if (dataDependencyBuilder.CreatedConstraints.Add(primaryKey.Namespace))
        //            PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
        //    }
        //    foreach (var uniqueConstraint in alterableUserTable.UniqueConstraints)
        //    {
        //        if (dataDependencyBuilder.DroppedConstraints.Add(uniqueConstraint.Namespace))
        //            UniqueConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        //        if (dataDependencyBuilder.CreatedConstraints.Add(uniqueConstraint.Namespace))
        //            UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        //    }
        //    foreach (var userTableColumn in alterableUserTable.UserTableColumns)
        //    {
        //        var sourceUserTableColumn = sourceUserTable.UserTableColumns[userTableColumn.Namespace];
        //        var targetUserTableColumn = targetUserTable.UserTableColumns[userTableColumn.Namespace];
                
        //        UserTableColumn.GenerateAlterScripts(sourceDataContext, targetDataContext, userTableColumn, sourceUserTableColumn, targetUserTableColumn, dataSyncActions, dataProperties);
        //    }
            
        //    // Matched UserTable
        //    if (matchedUserTable != null)
        //    {
        //        // For safety we are dropping and re-adding all constraints instead of checking
        //        // their inter-dependency for if the constraint exists on the same column as
        //        // a primary key, etc...
        //        foreach (var checkConstraint in matchedUserTable.CheckConstraints)
        //        {
        //            if (dataDependencyBuilder.DroppedConstraints.Add(checkConstraint.Namespace))
        //                CheckConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //            if (dataDependencyBuilder.CreatedConstraints.Add(checkConstraint.Namespace))
        //                CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //        }
        //        foreach (var defaultConstraint in matchedUserTable.DefaultConstraints)
        //        {
        //            if (dataDependencyBuilder.DroppedConstraints.Add(defaultConstraint.Namespace))
        //                DefaultConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //            if (dataDependencyBuilder.CreatedConstraints.Add(defaultConstraint.Namespace))
        //                DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //        }
        //        foreach (var foreignKey in matchedUserTable.ForeignKeys)
        //        {
        //            if (dataDependencyBuilder.DroppedForeignKeys.Add(foreignKey.Namespace))
        //                ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //            if (dataDependencyBuilder.CreatedForeignKeys.Add(foreignKey.Namespace))
        //                ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //        }
        //        foreach (var index in matchedUserTable.Indexes)
        //        {
        //            if (dataDependencyBuilder.DroppedConstraints.Add(index.Namespace))
        //                Index.GenerateDropScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
        //            if (dataDependencyBuilder.CreatedConstraints.Add(index.Namespace))
        //                Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
        //        }
        //        foreach (var primaryKey in matchedUserTable.PrimaryKeys)
        //        {
        //            if (dataDependencyBuilder.DroppedConstraints.Add(primaryKey.Namespace))
        //                PrimaryKey.GenerateDropScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
        //            if (dataDependencyBuilder.CreatedConstraints.Add(primaryKey.Namespace))
        //                PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
        //        }
        //        foreach (var uniqueConstraint in matchedUserTable.UniqueConstraints)
        //        {
        //            if (dataDependencyBuilder.DroppedConstraints.Add(uniqueConstraint.Namespace))
        //                UniqueConstraint.GenerateDropScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        //            if (dataDependencyBuilder.CreatedConstraints.Add(uniqueConstraint.Namespace))
        //                UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        //        }
        //    }

        //    // Ensure drops and creates of foreign keys happens to all tables that also reference this table.
        //    List<ForeignKey> targetReferencingForeignKeys;
        //    if (targetUserTable.Catalog.ReferencedUserTablePool.TryGetValue(alterableUserTable.Namespace, out targetReferencingForeignKeys))
        //    {
        //        foreach (var targetReferencingForeignKey in
        //            targetReferencingForeignKeys.Where(
        //                targetReferencingForeignKey => dataDependencyBuilder.DroppedForeignKeys.Add(targetReferencingForeignKey.Namespace)))
        //            ForeignKey.GenerateDropScripts(sourceDataContext, targetDataContext, targetReferencingForeignKey, dataSyncActions, dataProperties);
        //    }

        //    List<ForeignKey> sourceReferencingForeignKeys;
        //    if (!sourceUserTable.Catalog.ReferencedUserTablePool.TryGetValue(alterableUserTable.Namespace, out sourceReferencingForeignKeys))
        //        return;

        //    foreach (var sourceReferencingForeignKey in
        //        sourceReferencingForeignKeys.Where(
        //            sourceReferencingForeignKey => dataDependencyBuilder.CreatedForeignKeys.Add(sourceReferencingForeignKey.Namespace)))
        //    {
        //        var foreignKeyClone = ForeignKey.Clone(sourceReferencingForeignKey);
        //        foreignKeyClone.UserTable = targetUserTable;
        //        ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, sourceReferencingForeignKey, dataSyncActions, dataProperties);
        //    }
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTable userTable, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataProperties.IgnoredUserTables.Contains(userTable.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.CreateUserTable(sourceDataContext, targetDataContext, userTable);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
                
        //    // Computed Column does not contain any Generate...Scripts Methods.
        //    // Identity Column does not contain any Generate...Scripts Methods.
        //    foreach (var checkConstraint in userTable.CheckConstraints)
        //        CheckConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, checkConstraint, dataSyncActions, dataProperties);
        //    foreach (var defaultConstraint in userTable.DefaultConstraints)
        //        DefaultConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, defaultConstraint, dataSyncActions, dataProperties);
        //    foreach (var foreignKey in userTable.ForeignKeys)
        //        ForeignKey.GenerateCreateScripts(sourceDataContext, targetDataContext, foreignKey, dataSyncActions, dataProperties);
        //    foreach (var index in userTable.Indexes)
        //        Index.GenerateCreateScripts(sourceDataContext, targetDataContext, index, dataSyncActions, dataProperties);
        //    foreach (var primaryKey in userTable.PrimaryKeys)
        //        PrimaryKey.GenerateCreateScripts(sourceDataContext, targetDataContext, primaryKey, dataSyncActions, dataProperties);
        //    foreach (var uniqueConstraint in userTable.UniqueConstraints)
        //        UniqueConstraint.GenerateCreateScripts(sourceDataContext, targetDataContext, uniqueConstraint, dataSyncActions, dataProperties);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTable userTable, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (!dataProperties.TightSync)
        //        return;

        //    if (DataProperties.IgnoredUserTables.Contains(userTable.Namespace))
        //        return;

        //    var dataSyncAction = DataActionFactory.DropUserTable(sourceDataContext, targetDataContext, userTable);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public DataSet GetDataset(DataConnectionManager connectionManager)
        //{
        //    var command = connectionManager.DataConnectionInfo.CreateSelectAllCommand(this, connectionManager.DataConnection);
        //    var adapter = connectionManager.DataConnectionInfo.CreateDataAdapter();
        //    var dataset = new DataSet();
        //    adapter.SelectCommand = command;
        //    adapter.Fill(dataset);
        //    return dataset;
        //}

        ///// <summary>
        ///// Modifies the source UserTable to contain only objects that are
        ///// present in the source UserTable and in the target UserTable.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceUserTable">The source UserTable.</param>
        ///// <param name="targetUserTable">The target UserTable.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void IntersectWith(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTable sourceUserTable, UserTable targetUserTable,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
        //    matchingCheckConstraints.IntersectWith(targetUserTable.CheckConstraints.Keys);

        //    removableCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
        //    removableCheckConstraints.ExceptWith(matchingCheckConstraints);

        //    foreach (var checkConstraint in removableCheckConstraints)
        //        RemoveCheckConstraint(sourceUserTable, checkConstraint);

        //    foreach (var checkConstraint in matchingCheckConstraints)
        //    {
        //        var sourceCheckConstraint = sourceUserTable.CheckConstraints[checkConstraint];
        //        if (sourceCheckConstraint == null)
        //            continue;

        //        var targetCheckConstraint = targetUserTable.CheckConstraints[checkConstraint];
        //        if (targetCheckConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!CheckConstraint.CompareDefinitions(sourceCheckConstraint, targetCheckConstraint))
        //                    RemoveCheckConstraint(sourceUserTable, checkConstraint);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!CheckConstraint.CompareObjectNames(sourceCheckConstraint, targetCheckConstraint))
        //                    RemoveCheckConstraint(sourceUserTable, checkConstraint);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
        //    matchingComputedColumns.IntersectWith(targetUserTable.ComputedColumns.Keys);

        //    removableComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
        //    removableComputedColumns.ExceptWith(matchingComputedColumns);

        //    foreach (var computedColumn in removableComputedColumns)
        //        RemoveComputedColumn(sourceUserTable, computedColumn);

        //    foreach (var computedColumn in matchingComputedColumns)
        //    {
        //        var sourceComputedColumn = sourceUserTable.ComputedColumns[computedColumn];
        //        if (sourceComputedColumn == null)
        //            continue;

        //        var targetComputedColumn = targetUserTable.ComputedColumns[computedColumn];
        //        if (targetComputedColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!ComputedColumn.CompareDefinitions(sourceComputedColumn, targetComputedColumn))
        //                    RemoveComputedColumn(sourceUserTable, computedColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!ComputedColumn.CompareObjectNames(sourceComputedColumn, targetComputedColumn))
        //                    RemoveComputedColumn(sourceUserTable, computedColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
        //    matchingDefaultConstraints.IntersectWith(targetUserTable.DefaultConstraints.Keys);

        //    removableDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
        //    removableDefaultConstraints.ExceptWith(matchingDefaultConstraints);

        //    foreach (var defaultConstraint in removableDefaultConstraints)
        //        RemoveDefaultConstraint(sourceUserTable, defaultConstraint);

        //    foreach (var defaultConstraint in matchingDefaultConstraints)
        //    {
        //        var sourceDefaultConstraint = sourceUserTable.DefaultConstraints[defaultConstraint];
        //        if (sourceDefaultConstraint == null)
        //            continue;

        //        var targetDefaultConstraint = targetUserTable.DefaultConstraints[defaultConstraint];
        //        if (targetDefaultConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!DefaultConstraint.CompareDefinitions(sourceDefaultConstraint, targetDefaultConstraint))
        //                    RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!DefaultConstraint.CompareObjectNames(sourceDefaultConstraint, targetDefaultConstraint))
        //                    RemoveDefaultConstraint(sourceUserTable, defaultConstraint);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
        //    matchingIdentityColumns.IntersectWith(targetUserTable.IdentityColumns.Keys);

        //    removableIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
        //    removableIdentityColumns.ExceptWith(matchingIdentityColumns);

        //    foreach (var identityColumn in removableIdentityColumns)
        //        RemoveIdentityColumn(sourceUserTable, identityColumn);

        //    foreach (var identityColumn in matchingIdentityColumns)
        //    {
        //        var sourceIdentityColumn = sourceUserTable.IdentityColumns[identityColumn];
        //        if (sourceIdentityColumn == null)
        //            continue;

        //        var targetIdentityColumn = targetUserTable.IdentityColumns[identityColumn];
        //        if (targetIdentityColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!IdentityColumn.CompareDefinitions(sourceIdentityColumn, targetIdentityColumn))
        //                    RemoveIdentityColumn(sourceUserTable, identityColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!IdentityColumn.CompareObjectNames(sourceIdentityColumn, targetIdentityColumn))
        //                    RemoveIdentityColumn(sourceUserTable, identityColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
        //    matchingForeignKeys.IntersectWith(targetUserTable.ForeignKeys.Keys);

        //    removableForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
        //    removableForeignKeys.ExceptWith(matchingForeignKeys);

        //    foreach (var foreignKey in removableForeignKeys)
        //        RemoveForeignKey(sourceUserTable, foreignKey);

        //    foreach (var foreignKey in matchingForeignKeys)
        //    {
        //        var sourceForeignKey = sourceUserTable.ForeignKeys[foreignKey];
        //        if (sourceForeignKey == null)
        //            continue;

        //        var targetForeignKey = targetUserTable.ForeignKeys[foreignKey];
        //        if (targetForeignKey == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (ForeignKey.CompareDefinitions(sourceForeignKey, targetForeignKey))
        //                {
        //                    ForeignKey.IntersectWith(sourceForeignKey, targetForeignKey, dataComparisonType);
        //                    if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
        //                        RemoveForeignKey(sourceUserTable, foreignKey);
        //                }
        //                else
        //                {
        //                    RemoveForeignKey(sourceUserTable, foreignKey);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (ForeignKey.CompareObjectNames(sourceForeignKey, targetForeignKey))
        //                {
        //                    ForeignKey.IntersectWith(sourceForeignKey, targetForeignKey, dataComparisonType);
        //                    if (ForeignKey.ObjectCount(sourceForeignKey) == 0)
        //                        RemoveForeignKey(sourceUserTable, foreignKey);
        //                }
        //                else
        //                {
        //                    RemoveForeignKey(sourceUserTable, foreignKey);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIndexes.UnionWith(sourceUserTable.Indexes.Keys);
        //    matchingIndexes.IntersectWith(targetUserTable.Indexes.Keys);

        //    removableIndexes.UnionWith(sourceUserTable.Indexes.Keys);
        //    removableIndexes.ExceptWith(matchingIndexes);

        //    foreach (var index in removableIndexes)
        //        RemoveIndex(sourceUserTable, index);

        //    foreach (var index in matchingIndexes)
        //    {
        //        var sourceIndex = sourceUserTable.Indexes[index];
        //        if (sourceIndex == null)
        //            continue;

        //        var targetIndex = targetUserTable.Indexes[index];
        //        if (targetIndex == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Index.CompareDefinitions(sourceIndex, targetIndex))
        //                {
        //                    Index.IntersectWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
        //                    if (Index.ObjectCount(sourceIndex) == 0)
        //                        RemoveIndex(sourceUserTable, index);
        //                }
        //                else
        //                {
        //                    RemoveIndex(sourceUserTable, index);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (Index.CompareObjectNames(sourceIndex, targetIndex))
        //                {
        //                    Index.IntersectWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
        //                    if (Index.ObjectCount(sourceIndex) == 0)
        //                        RemoveIndex(sourceUserTable, index);
        //                }
        //                else
        //                {
        //                    RemoveIndex(sourceUserTable, index);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingPrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removablePrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingPrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
        //    matchingPrimaryKeys.IntersectWith(targetUserTable.PrimaryKeys.Keys);

        //    removablePrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
        //    removablePrimaryKeys.ExceptWith(matchingPrimaryKeys);

        //    foreach (var primaryKey in removablePrimaryKeys)
        //        RemovePrimaryKey(sourceUserTable, primaryKey);

        //    foreach (var primaryKey in matchingPrimaryKeys)
        //    {
        //        var sourcePrimaryKey = sourceUserTable.PrimaryKeys[primaryKey];
        //        if (sourcePrimaryKey == null)
        //            continue;

        //        var targetPrimaryKey = targetUserTable.PrimaryKeys[primaryKey];
        //        if (targetPrimaryKey == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (PrimaryKey.CompareDefinitions(sourcePrimaryKey, targetPrimaryKey))
        //                {
        //                    PrimaryKey.IntersectWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
        //                    if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
        //                        RemovePrimaryKey(sourceUserTable, primaryKey);
        //                }
        //                else
        //                {
        //                    RemovePrimaryKey(sourceUserTable, primaryKey);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (PrimaryKey.CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
        //                {
        //                    PrimaryKey.IntersectWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
        //                    if (PrimaryKey.ObjectCount(sourcePrimaryKey) == 0)
        //                        RemovePrimaryKey(sourceUserTable, primaryKey);
        //                }
        //                else
        //                {
        //                    RemovePrimaryKey(sourceUserTable, primaryKey);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
        //    matchingUniqueConstraints.IntersectWith(targetUserTable.UniqueConstraints.Keys);

        //    removableUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
        //    removableUniqueConstraints.ExceptWith(matchingUniqueConstraints);

        //    foreach (var uniqueConstraint in removableUniqueConstraints)
        //        RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);

        //    foreach (var uniqueConstraint in matchingUniqueConstraints)
        //    {
        //        var sourceUniqueConstraint = sourceUserTable.UniqueConstraints[uniqueConstraint];
        //        if (sourceUniqueConstraint == null)
        //            continue;

        //        var targetUniqueConstraint = targetUserTable.UniqueConstraints[uniqueConstraint];
        //        if (targetUniqueConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UniqueConstraint.CompareDefinitions(sourceUniqueConstraint, targetUniqueConstraint))
        //                {
        //                    UniqueConstraint.IntersectWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
        //                    if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
        //                        RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
        //                }
        //                else
        //                {
        //                    RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
        //                }
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UniqueConstraint.CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
        //                {
        //                    UniqueConstraint.IntersectWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
        //                    if (UniqueConstraint.ObjectCount(sourceUniqueConstraint) == 0)
        //                        RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
        //                }
        //                else
        //                {
        //                    RemoveUniqueConstraint(sourceUserTable, uniqueConstraint);
        //                }
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var removableUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
        //    matchingUserTableColumns.IntersectWith(targetUserTable.UserTableColumns.Keys);

        //    removableUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
        //    removableUserTableColumns.ExceptWith(matchingUserTableColumns);

        //    foreach (var userTableColumn in removableUserTableColumns)
        //        RemoveUserTableColumn(sourceUserTable, userTableColumn);

        //    foreach (var userTableColumn in matchingUserTableColumns)
        //    {
        //        var sourceUserTableColumn = sourceUserTable.UserTableColumns[userTableColumn];
        //        if (sourceUserTableColumn == null)
        //            continue;

        //        var targetUserTableColumn = targetUserTable.UserTableColumns[userTableColumn];
        //        if (targetUserTableColumn == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (!UserTableColumn.CompareDefinitions(sourceDataContext, targetDataContext, sourceUserTableColumn, targetUserTableColumn))
        //                    RemoveUserTableColumn(sourceUserTable, userTableColumn);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (!UserTableColumn.CompareObjectNames(sourceUserTableColumn, targetUserTableColumn))
        //                    RemoveUserTableColumn(sourceUserTable, userTableColumn);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }
        //}

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
                   userTable.ForeignKeys.Sum(
                       foreignKey => ForeignKey.ObjectCount(foreignKey)) +
                   userTable.Indexes.Sum(
                       index => Index.ObjectCount(index)) +
                   userTable.PrimaryKeys.Sum(
                       primaryKey => PrimaryKey.ObjectCount(primaryKey)) +
                   userTable.UniqueConstraints.Sum(
                       uniqueConstraint => UniqueConstraint.ObjectCount(uniqueConstraint));
        }

        public static void RemoveCheckConstraint(UserTable userTable, string objectNamespace)
        {
            userTable.CheckConstraints.Remove(objectNamespace);
        }

        public static void RemoveCheckConstraint(UserTable userTable, CheckConstraint checkConstraint)
        {
            userTable.CheckConstraints.Remove(checkConstraint.Namespace);
        }

        public static void RemoveComputedColumn(UserTable userTable, string objectNamespace)
        {

            userTable.ComputedColumns.Remove(objectNamespace);
        }

        public static void RemoveComputedColumn(UserTable userTable, ComputedColumn computedColumn)
        {
            userTable.ComputedColumns.Remove(computedColumn.Namespace);
        }

        public static void RemoveDefaultConstraint(UserTable userTable, string objectNamespace)
        {
            userTable.DefaultConstraints.Remove(objectNamespace);
        }

        public static void RemoveDefaultConstraint(UserTable userTable, DefaultConstraint defaultConstraint)
        {
            userTable.DefaultConstraints.Remove(defaultConstraint.Namespace);
        }

        public static void RemoveForeignKey(UserTable userTable, string objectNamespace)
        {
            userTable.ForeignKeys.Remove(objectNamespace);
        }

        public static void RemoveForeignKey(UserTable userTable, ForeignKey foreignKey)
        {
            userTable.ForeignKeys.Remove(foreignKey.Namespace);
        }

        public static void RemoveIdentityColumn(UserTable userTable, string objectNamespace)
        {
            userTable.IdentityColumns.Remove(objectNamespace);
        }

        public static void RemoveIdentityColumn(UserTable userTable, IdentityColumn identityColumn)
        {
            userTable.ForeignKeys.Remove(identityColumn.Namespace);
        }

        public static void RemoveIndex(UserTable userTable, string objectNamespace)
        {
            userTable.Indexes.Remove(objectNamespace);
        }

        public static void RemoveIndex(UserTable userTable, Index index)
        {
            userTable.ForeignKeys.Remove(index.Namespace);
        }

        public static void RemovePrimaryKey(UserTable userTable, string objectNamespace)
        {
            userTable.PrimaryKeys.Remove(objectNamespace);
        }

        public static void RemovePrimaryKey(UserTable userTable, PrimaryKey primaryKey)
        {
            userTable.PrimaryKeys.Remove(primaryKey.Namespace);
        }

        public static void RemoveUniqueConstraint(UserTable userTable, string objectNamespace)
        {
            userTable.UniqueConstraints.Remove(objectNamespace);
        }

        public static void RemoveUniqueConstraint(UserTable userTable, UniqueConstraint uniqueConstraint)
        {
            userTable.UniqueConstraints.Remove(uniqueConstraint.Namespace);
        }

        public static void RemoveUserTableColumn(UserTable userTable, string objectNamespace)
        {
            userTable.UserTableColumns.Remove(objectNamespace);
        }

        public static void RemoveUserTableColumn(UserTable userTable, UserTableColumn userTableColumn)
        {
            userTable.UserTableColumns.Remove(userTableColumn.Namespace);
        }

        public static void RenameCheckConstraint(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var checkConstraint = userTable.CheckConstraints[objectNamespace];
            if (checkConstraint == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.CheckConstraints.Rename(checkConstraint, newObjectName);
        }

        public static void RenameComputedColumn(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var computedColumn = userTable.ComputedColumns[objectNamespace];
            if (computedColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.ComputedColumns.Rename(computedColumn, newObjectName);
        }

        public static void RenameDefaultConstraint(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var defaultConstraint = userTable.DefaultConstraints[objectNamespace];
            if (defaultConstraint == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.DefaultConstraints.Rename(defaultConstraint, newObjectName);
        }

        public static void RenameForeignKey(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var foreignKey = userTable.ForeignKeys[objectNamespace];
            if (foreignKey == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.ForeignKeys.Rename(foreignKey, newObjectName);
        }

        public static void RenameIdentityColumn(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var identityColumn = userTable.IdentityColumns[objectNamespace];
            if (identityColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.IdentityColumns.Rename(identityColumn, newObjectName);
        }

        public static void RenameIndex(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var index = userTable.Indexes[objectNamespace];
            if (index == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.Indexes.Rename(index, newObjectName);
        }

        public static void RenamePrimaryKey(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var primaryKey = userTable.PrimaryKeys[objectNamespace];
            if (primaryKey == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.PrimaryKeys.Rename(primaryKey, newObjectName);
        }

        public static void RenameUniqueConstraint(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var uniqueConstraint = userTable.UniqueConstraints[objectNamespace];
            if (uniqueConstraint == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.UniqueConstraints.Rename(uniqueConstraint, newObjectName);
        }

        public static void RenameUserTableColumn(UserTable userTable, string objectNamespace, string newObjectName)
        {
            var userTableColumn = userTable.UserTableColumns[objectNamespace];
            if (userTableColumn == null)
                throw new Exception(string.Format("{0} could not be found in {1} {2} to rename to {3}",
                    objectNamespace, userTable.Description, userTable.Namespace, newObjectName));

            userTable.UserTableColumns.Rename(userTableColumn, newObjectName);
        }

        ///// <summary>
        ///// Modifies the source UserTable to contain all objects that are
        ///// present in both iteself and the target UserTable.
        ///// </summary>
        ///// <param name="sourceDataContext">The source DataContext.</param>
        ///// <param name="targetDataContext">The target DataContext.</param>
        ///// <param name="sourceUserTable">The source UserTable.</param>
        ///// <param name="targetUserTable">The target UserTable.</param>
        ///// <param name="dataComparisonType">
        ///// The completeness of comparisons between matching objects.
        ///// </param>
        //public static void UnionWith(UserTable sourceUserTable, UserTable targetUserTable,
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    DataComparisonType dataComparisonType = DataComparisonType.SchemaLevelNamespaces)
        //{
        //    var matchingCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableCheckConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingCheckConstraints.UnionWith(sourceUserTable.CheckConstraints.Keys);
        //    matchingCheckConstraints.IntersectWith(targetUserTable.CheckConstraints.Keys);

        //    addableCheckConstraints.UnionWith(targetUserTable.CheckConstraints.Keys);
        //    addableCheckConstraints.ExceptWith(matchingCheckConstraints);

        //    foreach (var checkConstraint in addableCheckConstraints)
        //    {
        //        var targetCheckConstraint = targetUserTable.CheckConstraints[checkConstraint];
        //        if (targetCheckConstraint == null)
        //            continue;

        //        AddCheckConstraint(sourceUserTable, CheckConstraint.Clone(targetCheckConstraint));
        //    }

        //    var matchingComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableComputedColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingComputedColumns.UnionWith(sourceUserTable.ComputedColumns.Keys);
        //    matchingComputedColumns.IntersectWith(targetUserTable.ComputedColumns.Keys);

        //    addableComputedColumns.UnionWith(targetUserTable.ComputedColumns.Keys);
        //    addableComputedColumns.ExceptWith(matchingComputedColumns);

        //    foreach (var computedColumn in addableComputedColumns)
        //    {
        //        var targetComputedColumn = targetUserTable.ComputedColumns[computedColumn];
        //        if (targetComputedColumn == null)
        //            continue;

        //        AddComputedColumn(sourceUserTable, ComputedColumn.Clone(targetComputedColumn));
        //    }

        //    var matchingDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableDefaultConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingDefaultConstraints.UnionWith(sourceUserTable.DefaultConstraints.Keys);
        //    matchingDefaultConstraints.IntersectWith(targetUserTable.DefaultConstraints.Keys);

        //    addableDefaultConstraints.UnionWith(targetUserTable.DefaultConstraints.Keys);
        //    addableDefaultConstraints.ExceptWith(matchingDefaultConstraints);

        //    foreach (var defaultConstraint in addableDefaultConstraints)
        //    {
        //        var targetDefaultConstraint = targetUserTable.DefaultConstraints[defaultConstraint];
        //        if (targetDefaultConstraint == null)
        //            continue;

        //        AddDefaultConstraint(sourceUserTable, DefaultConstraint.Clone(targetDefaultConstraint));
        //    }

        //    var matchingIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableIdentityColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIdentityColumns.UnionWith(sourceUserTable.IdentityColumns.Keys);
        //    matchingIdentityColumns.IntersectWith(targetUserTable.IdentityColumns.Keys);

        //    addableIdentityColumns.UnionWith(targetUserTable.IdentityColumns.Keys);
        //    addableIdentityColumns.ExceptWith(matchingIdentityColumns);

        //    foreach (var identityColumn in addableIdentityColumns)
        //    {
        //        var targetIdentityColumn = targetUserTable.IdentityColumns[identityColumn];
        //        if (targetIdentityColumn == null)
        //            continue;

        //        AddIdentityColumn(sourceUserTable, IdentityColumn.Clone(targetIdentityColumn));
        //    }

        //    var matchingForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingForeignKeys.UnionWith(sourceUserTable.ForeignKeys.Keys);
        //    matchingForeignKeys.IntersectWith(targetUserTable.ForeignKeys.Keys);

        //    addableForeignKeys.UnionWith(targetUserTable.ForeignKeys.Keys);
        //    addableForeignKeys.ExceptWith(matchingForeignKeys);

        //    foreach (var foreignKey in addableForeignKeys)
        //    {
        //        var targetForeignKey = targetUserTable.ForeignKeys[foreignKey];
        //        if (targetForeignKey == null)
        //            continue;

        //        AddForeignKey(sourceUserTable, ForeignKey.Clone(targetForeignKey));
        //    }

        //    foreach (var foreignKey in matchingForeignKeys)
        //    {
        //        var sourceForeignKey = sourceUserTable.ForeignKeys[foreignKey];
        //        if (sourceForeignKey == null)
        //            continue;

        //        var targetForeignKey = targetUserTable.ForeignKeys[foreignKey];
        //        if (targetForeignKey == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (ForeignKey.CompareDefinitions(sourceForeignKey, targetForeignKey))
        //                    ForeignKey.UnionWith(sourceDataContext, targetDataContext, sourceForeignKey, targetForeignKey, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (ForeignKey.CompareObjectNames(sourceForeignKey, targetForeignKey))
        //                    ForeignKey.UnionWith(sourceDataContext, targetDataContext, sourceForeignKey, targetForeignKey, dataComparisonType);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableIndexes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingIndexes.UnionWith(sourceUserTable.Indexes.Keys);
        //    matchingIndexes.IntersectWith(targetUserTable.Indexes.Keys);

        //    addableIndexes.UnionWith(targetUserTable.Indexes.Keys);
        //    addableIndexes.ExceptWith(matchingIndexes);

        //    foreach (var index in addableIndexes)
        //    {
        //        var targetIndex = targetUserTable.Indexes[index];
        //        if (targetIndex == null)
        //            continue;

        //        AddIndex(sourceUserTable, Index.Clone(targetIndex));
        //    }

        //    foreach (var index in matchingIndexes)
        //    {
        //        var sourceIndex = sourceUserTable.Indexes[index];
        //        if (sourceIndex == null)
        //            continue;

        //        var targetIndex = targetUserTable.Indexes[index];
        //        if (targetIndex == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (Index.CompareDefinitions(sourceIndex, targetIndex))
        //                    Index.UnionWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (Index.CompareObjectNames(sourceIndex, targetIndex))
        //                    Index.UnionWith(sourceDataContext, targetDataContext, sourceIndex, targetIndex, dataComparisonType);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingPrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addablePrimaryKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingPrimaryKeys.UnionWith(sourceUserTable.PrimaryKeys.Keys);
        //    matchingPrimaryKeys.IntersectWith(targetUserTable.PrimaryKeys.Keys);

        //    addablePrimaryKeys.UnionWith(targetUserTable.PrimaryKeys.Keys);
        //    addablePrimaryKeys.ExceptWith(matchingPrimaryKeys);

        //    foreach (var primaryKey in addablePrimaryKeys)
        //    {
        //        var targetPrimaryKey = targetUserTable.PrimaryKeys[primaryKey];
        //        if (targetPrimaryKey == null)
        //            continue;

        //        AddPrimaryKey(sourceUserTable, PrimaryKey.Clone(targetPrimaryKey));
        //    }

        //    foreach (var primaryKey in matchingPrimaryKeys)
        //    {
        //        var sourcePrimaryKey = sourceUserTable.PrimaryKeys[primaryKey];
        //        if (sourcePrimaryKey == null)
        //            continue;

        //        var targetPrimaryKey = targetUserTable.PrimaryKeys[primaryKey];
        //        if (targetPrimaryKey == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (PrimaryKey.CompareDefinitions(sourcePrimaryKey, targetPrimaryKey))
        //                    PrimaryKey.UnionWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (PrimaryKey.CompareObjectNames(sourcePrimaryKey, targetPrimaryKey))
        //                    PrimaryKey.UnionWith(sourceDataContext, targetDataContext, sourcePrimaryKey, targetPrimaryKey, dataComparisonType);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableUniqueConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUniqueConstraints.UnionWith(sourceUserTable.UniqueConstraints.Keys);
        //    matchingUniqueConstraints.IntersectWith(targetUserTable.UniqueConstraints.Keys);

        //    addableUniqueConstraints.UnionWith(targetUserTable.UniqueConstraints.Keys);
        //    addableUniqueConstraints.ExceptWith(matchingUniqueConstraints);

        //    foreach (var uniqueConstraint in addableUniqueConstraints)
        //    {
        //        var targetUniqueConstraint = targetUserTable.UniqueConstraints[uniqueConstraint];
        //        if (targetUniqueConstraint == null)
        //            continue;

        //        AddUniqueConstraint(sourceUserTable, UniqueConstraint.Clone(targetUniqueConstraint));
        //    }

        //    foreach (var uniqueConstraint in matchingUniqueConstraints)
        //    {
        //        var sourceUniqueConstraint = sourceUserTable.UniqueConstraints[uniqueConstraint];
        //        if (sourceUniqueConstraint == null)
        //            continue;

        //        var targetUniqueConstraint = targetUserTable.UniqueConstraints[uniqueConstraint];
        //        if (targetUniqueConstraint == null)
        //            continue;

        //        switch (dataComparisonType)
        //        {
        //            case DataComparisonType.Definitions:
        //                if (UniqueConstraint.CompareDefinitions(sourceUniqueConstraint, targetUniqueConstraint))
        //                    UniqueConstraint.UnionWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
        //                break;
        //            case DataComparisonType.Namespaces:
        //                if (UniqueConstraint.CompareObjectNames(sourceUniqueConstraint, targetUniqueConstraint))
        //                    UniqueConstraint.UnionWith(sourceDataContext, targetDataContext, sourceUniqueConstraint, targetUniqueConstraint, dataComparisonType);
        //                break;
        //            //case DataComparisonType.SchemaLevelNamespaces:
        //            //    // Do Nothing
        //            //    break;
        //        }
        //    }

        //    var matchingUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        //    var addableUserTableColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        //    matchingUserTableColumns.UnionWith(sourceUserTable.UserTableColumns.Keys);
        //    matchingUserTableColumns.IntersectWith(targetUserTable.UserTableColumns.Keys);

        //    addableUserTableColumns.UnionWith(targetUserTable.UserTableColumns.Keys);
        //    addableUserTableColumns.ExceptWith(matchingUserTableColumns);

        //    foreach (var userTableColumn in addableUserTableColumns)
        //    {
        //        var targetUserTableColumn = targetUserTable.UserTableColumns[userTableColumn];
        //        if (targetUserTableColumn == null)
        //            continue;

        //        var sourceUserTableColumn = UserTableColumn.Clone(targetUserTableColumn);
        //        DataTypes.ConvertDataType(targetDataContext, sourceDataContext, ref sourceUserTableColumn);
        //        AddUserTableColumn(sourceUserTable, sourceUserTableColumn);
        //    }
        //}
    }
}
