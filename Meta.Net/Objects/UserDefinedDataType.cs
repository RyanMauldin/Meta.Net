using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class UserDefinedDataType : SchemaBasedObject
    {
        public static readonly string DefaultDescription = "User-Defined Data Type";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string DataType { get; set; }
        [DataMember] public long MaxLength { get; set; }
        [DataMember] public int Precision { get; set; }
        [DataMember] public int Scale { get; set; }
        [DataMember] public string Collation { get; set; }
        [DataMember] public bool HasDefault { get; set; }
        [DataMember] public bool IsUserDefined { get; set; }
        [DataMember] public bool IsAssemblyType { get; set; }
        [DataMember] public bool IsNullable { get; set; }

        public UserDefinedDataType()
        {
            Collation = string.Empty;
            DataType = "int";
            HasDefault = false;
            IsAssemblyType = false;
            IsNullable = false;
            IsUserDefined = false;
            MaxLength = 0;
            Precision = 0;
            Scale = 0;
        }

        //public static bool CompareDefinitions(UserDefinedDataType sourceUserDefinedDataType, UserDefinedDataType targetUserDefinedDataType)
        //{
        //    if (!CompareObjectNames(sourceUserDefinedDataType, targetUserDefinedDataType))
        //        return false;

        //    if (sourceUserDefinedDataType.HasDefault != targetUserDefinedDataType.HasDefault)
        //        return false;

        //    if (sourceUserDefinedDataType.IsAssemblyType != targetUserDefinedDataType.IsAssemblyType)
        //        return false;

        //    if (sourceUserDefinedDataType.IsNullable != targetUserDefinedDataType.IsNullable)
        //        return false;

        //    if (sourceUserDefinedDataType.IsUserDefined != targetUserDefinedDataType.IsUserDefined)
        //        return false;

        //    if (sourceUserDefinedDataType.MaxLength != targetUserDefinedDataType.MaxLength)
        //        return false;

        //    if (sourceUserDefinedDataType.Precision != targetUserDefinedDataType.Precision)
        //        return false;

        //    if (sourceUserDefinedDataType.Scale != targetUserDefinedDataType.Scale)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(sourceUserDefinedDataType.Collation, targetUserDefinedDataType.Collation) != 0)
        //        return false;

        //    return StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceUserDefinedDataType.DataType, targetUserDefinedDataType.DataType) == 0;
        //}

        //public static bool CompareObjectNames(UserDefinedDataType sourceUserDefinedDataType, UserDefinedDataType targetUserDefinedDataType, StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceUserDefinedDataType.ObjectName, targetUserDefinedDataType.ObjectName) == 0;
        //}
        
        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserDefinedDataType userDefinedDataType, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreateUserDefinedDataType(sourceDataContext, targetDataContext, userDefinedDataType);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    UserDefinedDataType userDefinedDataType, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    if (!dataProperties.TightSync)
        //        return;

        //    var dataSyncAction = DataActionFactory.DropUserDefinedDataType(sourceDataContext, targetDataContext, userDefinedDataType);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
