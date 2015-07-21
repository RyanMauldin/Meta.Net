using System.Runtime.Serialization;
using Meta.Net.Abstract;

namespace Meta.Net.Objects
{
    [DataContract]
    public class DefaultConstraint : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Default Constraint";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        [DataMember] public string ColumnName { get; set; }
        [DataMember] public string Definition { get; set; }
        [DataMember] public bool IsSystemNamed { get; set; }

        public DefaultConstraint()
        {
            ColumnName = string.Empty;
            Definition = string.Empty;
            IsSystemNamed = false;
        }

        //public static bool CompareDefinitions(DefaultConstraint sourceDefaultConstraint, DefaultConstraint targetDefaultConstraint)
        //{
        //    if (!CompareObjectNames(sourceDefaultConstraint, targetDefaultConstraint))
        //        return false;

        //    if (sourceDefaultConstraint.IsSystemNamed != targetDefaultConstraint.IsSystemNamed)
        //        return false;

        //    if (StringComparer.OrdinalIgnoreCase.Compare(
        //        sourceDefaultConstraint.ColumnName, targetDefaultConstraint.ColumnName) != 0)
        //        return false;

        //    return DataProperties.DefinitionComparer.Compare(
        //        sourceDefaultConstraint.Definition, targetDefaultConstraint.Definition) == 0;
        //}

        //public static bool CompareObjectNames(DefaultConstraint sourceDefaultConstraint, DefaultConstraint targetDefaultConstraint,
        //    StringComparer stringComparer = null)
        //{
        //    if (stringComparer == null)
        //        stringComparer = StringComparer.OrdinalIgnoreCase;

        //    return stringComparer.Compare(sourceDefaultConstraint.ObjectName, targetDefaultConstraint.ObjectName) == 0;
        //}

        //public static void GenerateCreateScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    DefaultConstraint defaultConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.CreateDefaultConstraint(sourceDataContext, targetDataContext, defaultConstraint);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}

        //public static void GenerateDropScripts(
        //    DataContext sourceDataContext, DataContext targetDataContext,
        //    DefaultConstraint defaultConstraint, DataSyncActionsCollection dataSyncActions, DataProperties dataProperties)
        //{
        //    var dataSyncAction = DataActionFactory.DropDefaultConstraint(sourceDataContext, targetDataContext, defaultConstraint);
        //    if (dataSyncAction != null)
        //        dataSyncActions.Add(dataSyncAction);
        //}
    }
}
