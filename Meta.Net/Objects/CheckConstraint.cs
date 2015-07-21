using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class CheckConstraint : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Check Constraint";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string ColumnName { get; set; }
        [DataMember] public string Definition { get; set; }
        [DataMember] public bool IsDisabled { get; set; }
        [DataMember] public bool IsNotForReplication { get; set; }
        [DataMember] public bool IsNotTrusted { get; set; }
        [DataMember] public bool IsSystemNamed { get; set; }
        [DataMember] public bool IsTableConstraint { get; set; }

        public CheckConstraint()
        {
            ColumnName = string.Empty;
            Definition = string.Empty;
            IsDisabled = false;
            IsNotForReplication = false;
            IsNotTrusted = false;
            IsSystemNamed = false;
            IsTableConstraint = true;
        }

        //public static bool CompareDefinitions(CheckConstraint sourceCheckConstraint, CheckConstraint targetCheckConstraint)
        //{
        //    if (!CompareObjectNames(sourceCheckConstraint, targetCheckConstraint))
        //        return false;

        //    if (sourceCheckConstraint.IsDisabled != targetCheckConstraint.IsDisabled)
        //        return false;

        //    if (sourceCheckConstraint.IsNotForReplication != targetCheckConstraint.IsNotForReplication)
        //        return false;

        //    if (sourceCheckConstraint.IsNotTrusted != targetCheckConstraint.IsNotTrusted)
        //        return false;

        //    if (sourceCheckConstraint.IsSystemNamed != targetCheckConstraint.IsSystemNamed)
        //        return false;

        //    if (sourceCheckConstraint.IsTableConstraint != targetCheckConstraint.IsTableConstraint)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceCheckConstraint.ColumnName, targetCheckConstraint.ColumnName) != 0)
        //        return false;

        //    return DataProperties.DefinitionComparer.Compare(
        //        sourceCheckConstraint.Definition, targetCheckConstraint.Definition) == 0;
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    CheckConstraint checkConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreateCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    CheckConstraint checkConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.DropCheckConstraint(sourceDataContext, targetDataContext, checkConstraint);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
