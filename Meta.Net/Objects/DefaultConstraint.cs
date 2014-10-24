using Meta.Net.Abstract;
using Meta.Net.Interfaces;

namespace Meta.Net.Objects
{
    //[Serializable]
    public class DefaultConstraint : UserTableBasedObject
    {
        public static readonly string DefaultDescription = "Default Constraint";

        public override string Description
        {
            get { return DefaultDescription; }
        }

        public string ColumnName { get; set; }
        public string Definition { get; set; }
        public bool IsSystemNamed { get; set; }

        public DefaultConstraint()
        {
            ColumnName = string.Empty;
            Definition = string.Empty;
            IsSystemNamed = false;
        }

        public override IMetaObject DeepClone()
        {
            return new DefaultConstraint
            {
                ObjectName = ObjectName == null ? null : string.Copy(ObjectName),
                ColumnName = ColumnName == null ? null : string.Copy(ColumnName),
                Definition = Definition == null ? null : string.Copy(Definition),
                IsSystemNamed = IsSystemNamed
            };
        }

        public override IMetaObject ShallowClone()
        {
            return new DefaultConstraint
            {
                ObjectName = ObjectName,
                ColumnName = ColumnName,
                Definition = Definition,
                IsSystemNamed = IsSystemNamed
            };
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

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Serialize Members
        //    info.AddValue("ObjectName", ObjectName);
        //    info.AddValue("Description", Description);
        //    info.AddValue("ColumnName", ColumnName);
        //    info.AddValue("Definition", Definition);
        //    info.AddValue("IsSystemNamed", IsSystemNamed);
        //}

        //public DefaultConstraint(SerializationInfo info, StreamingContext context)
        //{
        //    // Holding off on the serialzation in this manner because, this is
        //    // extremely complicated to do in this manner do to data object
        //    // associations, especially
        //    // Set Null Members
        //    UserTable = null;

        //    // Deserialize Members
        //    ObjectName = info.GetString("ObjectName");
        //    Description = info.GetString("Description");
        //    ColumnName = info.GetString("ColumnName");
        //    Definition = info.GetString("Definition");
        //    IsSystemNamed = info.GetBoolean("IsSystemNamed");
        //}

        //public static DefaultConstraint FromJson(string json)
        //{
        //    return JsonConvert.DeserializeObject<DefaultConstraint>(json);
        //}

        //public static string ToJson(DefaultConstraint defaultConstraint, Formatting formatting = Formatting.Indented)
        //{
        //    return JsonConvert.SerializeObject(defaultConstraint, formatting);
        //}
    }
}
