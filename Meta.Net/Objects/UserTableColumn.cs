using System.Runtime.Serialization;
using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    [DataContract]
    public class UserTableColumn : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "User-Table Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string Collation { get; set; }
        [DataMember] public int ColumnOrdinal { get; set; }
        [DataMember] public string DataType { get; set; }
        //public CommonDataType CommonDataType { get; set; }
        [DataMember] public bool HasDefault { get; set; }
        [DataMember] public bool HasXmlCollection { get; set; }
        [DataMember] public bool IsAnsiPadded { get; set; }
        [DataMember] public bool IsAssemblyType { get; set; }
        [DataMember] public bool IsComputed { get; set; }
        [DataMember] public bool IsFileStream { get; set; }
        [DataMember] public bool IsIdentity { get; set; }
        [DataMember] public bool IsNullable { get; set; }
        [DataMember] public bool IsRowGuidColumn { get; set; }
        [DataMember] public bool IsUserDefined { get; set; }
        [DataMember] public bool IsXmlDocument { get; set; }
        [DataMember] public long MaxLength { get; set; }
        [DataMember] public int Precision { get; set; }
        [DataMember] public int Scale { get; set; }
        [DataMember] public bool HasForeignKey { get; set; }

        public UserTableColumn()
        {
            Collation = DataProperties.DefaultCollation;
            ColumnOrdinal = 0;
            DataType = "int";
            HasDefault = false;
            HasXmlCollection = false;
            IsAnsiPadded = true;
            IsAssemblyType = false;
            IsComputed = false;
            IsFileStream = false;
            IsIdentity = false;
            IsNullable = false;
            IsRowGuidColumn = false;
            IsUserDefined = false;
            IsXmlDocument = false;
            MaxLength = 0;
            Precision = 0;
            Scale = 0;
        }

        //public static bool CompareDefinitions(DataContext sourceDataContext, DataContext targetDataContext, UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn)
        //{
        //    if (!CompareObjectNames(sourceUserTableColumn, targetUserTableColumn))
        //        return false;

        //    //if (sourceUserTableColumn.ColumnOrdinal != targetUserTableColumn.ColumnOrdinal)
        //    //    return false;

        //    if (sourceUserTableColumn.HasDefault != targetUserTableColumn.HasDefault)
        //        return false;

        //    //if (sourceUserTableColumn.HasXmlCollection != targetUserTableColumn.HasXmlCollection)
        //    //    return false;

        //    //if (sourceUserTableColumn.IsAnsiPadded != targetUserTableColumn.IsAnsiPadded)
        //    //    return false;

        //    //if (sourceUserTableColumn.IsAssemblyType != targetUserTableColumn.IsAssemblyType)
        //    //    return false;

        //    //if (sourceUserTableColumn.IsFileStream != targetUserTableColumn.IsFileStream)
        //    //    return false;

        //    //if (sourceUserTableColumn.IsXmlDocument != targetUserTableColumn.IsXmlDocument)
        //    //    return false;

        //    return DataTypes.CompareDataType(sourceDataContext, targetDataContext, sourceUserTableColumn, targetUserTableColumn);
        //}

        //public static bool CompareObjectNames(UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceUserTableColumn.ObjectName, targetUserTableColumn.ObjectName) == 0;
        //}

        //public static void GenerateAlterScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTableColumn userTableColumn, UserTableColumn sourceUserTableColumn, UserTableColumn targetUserTableColumn,
        //    DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (DataTypes.IsCompatibleAlter(sourceDataContext, targetDataContext, sourceUserTableColumn, targetUserTableColumn))
        //    {
        //        var dataSyncAction = DataActionFactory.AlterUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
        //        if (dataSyncAction != null)
        //            dataSyncActions.Add(dataSyncAction);
        //        return;
        //    }

        //    var dropDataSyncAction = DataActionFactory.DropUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
        //    if (dropDataSyncAction != null)
        //        dataSyncActions.Add(dropDataSyncAction);

        //    // TODO: Add logic to call IDataSyncActionFactory based objects to move data
        //    // TODO: here for different data sync strategies if they are not truncates

        //    var addDataSyncAction = DataActionFactory.AddUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
        //    if (addDataSyncAction != null)
        //        dataSyncActions.Add(addDataSyncAction);
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTableColumn userTableColumn, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{

        //    var dataSyncAction = DataActionFactory.AddUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserTableColumn userTableColumn, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{

        //    var dataSyncAction = DataActionFactory.DropUserTableColumn(sourceDataContext, targetDataContext, userTableColumn);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
