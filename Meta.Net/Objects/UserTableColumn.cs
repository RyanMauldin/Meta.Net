using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class UserTableColumn : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "User-Table Column";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string Collation { get; set; }
        public int ColumnOrdinal { get; set; }
        public string DataType { get; set; }
        //public CommonDataType CommonDataType { get; set; }
        public bool HasDefault { get; set; }
        public bool HasXmlCollection { get; set; }
        public bool IsAnsiPadded { get; set; }
        public bool IsAssemblyType { get; set; }
        public bool IsComputed { get; set; }
        public bool IsFileStream { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsNullable { get; set; }
        public bool IsRowGuidColumn { get; set; }
        public bool IsUserDefined { get; set; }
        public bool IsXmlDocument { get; set; }
        public long MaxLength { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }
        public bool HasForeignKey { get; set; }

        private static void Init(UserTableColumn userTableColumn, UserTable userTable, string objectName)
        {
            userTableColumn.UserTable = userTable;
            userTableColumn.ObjectName = GetDefaultObjectName(userTableColumn, objectName);
            userTableColumn.Collation = DataProperties.DefaultCollation;
            userTableColumn.ColumnOrdinal = 0;
            userTableColumn.DataType = "int";
            //userTableColumn.CommonDataType = DataTypes.GetCommonDataType(userTableColumn.DataType);
            userTableColumn.HasDefault = false;
            userTableColumn.HasXmlCollection = false;
            userTableColumn.IsAnsiPadded = true;
            userTableColumn.IsAssemblyType = false;
            userTableColumn.IsComputed = false;
            userTableColumn.IsFileStream = false;
            userTableColumn.IsIdentity = false;
            userTableColumn.IsNullable = false;
            userTableColumn.IsRowGuidColumn = false;
            userTableColumn.IsUserDefined = false;
            userTableColumn.IsXmlDocument = false;
            userTableColumn.MaxLength = 0;
            userTableColumn.Precision = 0;
            userTableColumn.Scale = 0;
        }

        public UserTableColumn(UserTable userTable, string objectName)
        {
            Init(this, userTable, objectName);
        }

        public UserTableColumn()
        {
            
        }

        /// <summary>
        /// Deep Clone and Shallow Clone... Leaf Node.
        /// A clone of this class's instance specific metadata.
        /// </summary>
        /// <param name="userTableColumn">The user-table column to clone.</param>
        /// <returns>A clone of this class's instance specific metadata.</returns>
        public static UserTableColumn Clone(UserTableColumn userTableColumn)
        {
            return new UserTableColumn
            {
                ObjectName = userTableColumn.ObjectName,
                Collation = userTableColumn.Collation,
                ColumnOrdinal = userTableColumn.ColumnOrdinal,
                //CommonDataType = userTableColumn.CommonDataType,
                DataType = userTableColumn.DataType,
                HasDefault = userTableColumn.HasDefault,
                HasXmlCollection = userTableColumn.HasXmlCollection,
                IsAnsiPadded = userTableColumn.IsAnsiPadded,
                IsAssemblyType = userTableColumn.IsAssemblyType,
                IsComputed = userTableColumn.IsComputed,
                IsFileStream = userTableColumn.IsFileStream,
                IsIdentity = userTableColumn.IsIdentity,
                IsNullable = userTableColumn.IsNullable,
                IsRowGuidColumn = userTableColumn.IsRowGuidColumn,
                IsUserDefined = userTableColumn.IsUserDefined,
                IsXmlDocument = userTableColumn.IsXmlDocument,
                MaxLength = userTableColumn.MaxLength,
                Precision = userTableColumn.Precision,
                Scale = userTableColumn.Scale
            };
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

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("Collation", Collation);
        //    info.AddValue("ColumnOrdinal", ColumnOrdinal);
        //    info.AddValue("DataType", DataType);
        //    info.AddValue("HasDefault", HasDefault);
        //    info.AddValue("HasXmlCollection", HasXmlCollection);
        //    info.AddValue("IsAnsiPadded", IsAnsiPadded);
        //    info.AddValue("IsAssemblyType", IsAssemblyType);
        //    info.AddValue("IsComputed", IsComputed);
        //    info.AddValue("IsFileStream", IsFileStream);
        //    info.AddValue("IsIdentity", IsIdentity);
        //    info.AddValue("IsNullable", IsNullable);
        //    info.AddValue("IsRowGuidColumn", IsRowGuidColumn);
        //    info.AddValue("IsUserDefined", IsUserDefined);
        //    info.AddValue("IsXmlDocument", IsXmlDocument);
        //    info.AddValue("MaxLength", MaxLength);
        //    info.AddValue("Precision", Precision);
        //    info.AddValue("Scale", Scale);
        //}

        //public UserTableColumn(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UserTable = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    Collation = info.GetString("Collation");
        //    ColumnOrdinal = info.GetInt32("ColumnOrdinal");
        //    DataType = info.GetString("DataType");
        //    HasDefault = info.GetBoolean("HasDefault");
        //    HasXmlCollection = info.GetBoolean("HasXmlCollection");
        //    IsAnsiPadded = info.GetBoolean("IsAnsiPadded");
        //    IsAssemblyType = info.GetBoolean("IsAssemblyType");
        //    IsComputed = info.GetBoolean("IsComputed");
        //    IsFileStream = info.GetBoolean("IsFileStream");
        //    IsIdentity = info.GetBoolean("IsIdentity");
        //    IsNullable = info.GetBoolean("IsNullable");
        //    IsRowGuidColumn = info.GetBoolean("IsRowGuidColumn");
        //    IsUserDefined = info.GetBoolean("IsUserDefined");
        //    IsXmlDocument = info.GetBoolean("IsXmlDocument");
        //    MaxLength = info.GetInt32("MaxLength");
        //    Precision = info.GetInt32("Precision");
        //    Scale = info.GetInt32("Scale");
        //    CommonDataType = (CommonDataType)System.Enum.Parse(typeof(CommonDataType), info.GetString("CommonDataType"), true);
        //    HasForeignKey = info.GetBoolean("HasForeignKey");
        //}

        //public static UserTableColumn FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<UserTableColumn>(json);
        //}

        //public static string ToJson(UserTableColumn userTableColumn, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(userTableColumn, formatting);
        //}
    }
}
