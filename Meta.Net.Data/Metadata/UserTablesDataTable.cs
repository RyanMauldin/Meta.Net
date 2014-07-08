using System;
using System.Data;

namespace Meta.Net.Data.Metadata
{
    public class UserTablesDataTable: DataTable
    {
		#region Properties (12) 

        public DataColumn CatalogNameColumn { get; set; }

        public int Count
        {
            get
            {
                return Rows.Count;
            }
        }

        public DataColumn DescriptionColumn { get; set; }

        public DataColumn FileStreamFileGroupColumn { get; set; }

        public DataColumn HasTextNTextOrImageColumnsColumn { get; set; }

        public DataColumn LobFileGroupColumn { get; set; }

        public DataColumn NamespaceColumn { get; set; }

        public DataColumn ObjectNameColumn { get; set; }

        public DataColumn SchemaNameColumn { get; set; }

        public DataColumn TextInRowLimitColumn { get; set; }

        public UserTablesRow this[int index]
        {
            get
            {
                return ((UserTablesRow)(Rows[index]));
            }
        }

        public DataColumn UsesAnsiNullsColumn { get; set; }

		#endregion Properties 

		#region Constructors (1) 

        public UserTablesDataTable()
        {
            TableName = "UserTables";

            CatalogNameColumn = new DataColumn("CatalogName", typeof(string), null, MappingType.Element);
            DescriptionColumn = new DataColumn("Description", typeof(string), null, MappingType.Element);
            FileStreamFileGroupColumn = new DataColumn("FileStreamFileGroup", typeof(string), null, MappingType.Element);
            HasTextNTextOrImageColumnsColumn = new DataColumn("HasTextNTextOrImageColumns", typeof(bool), null, MappingType.Element);
            LobFileGroupColumn = new DataColumn("LobFileGroup", typeof(string), null, MappingType.Element);
            NamespaceColumn = new DataColumn("Namespace", typeof(string), null, MappingType.Element);
            ObjectNameColumn = new DataColumn("ObjectName", typeof(string), null, MappingType.Element);
            SchemaNameColumn = new DataColumn("SchemaName", typeof(string), null, MappingType.Element);
            TextInRowLimitColumn = new DataColumn("TextInRowLimit", typeof(int), null, MappingType.Element);
            UsesAnsiNullsColumn = new DataColumn("UsesAnsiNulls", typeof(bool), null, MappingType.Element);
            
            base.Columns.Add(CatalogNameColumn);
            base.Columns.Add(DescriptionColumn);
            base.Columns.Add(FileStreamFileGroupColumn);
            base.Columns.Add(HasTextNTextOrImageColumnsColumn);
            base.Columns.Add(LobFileGroupColumn);
            base.Columns.Add(NamespaceColumn);
            base.Columns.Add(ObjectNameColumn);
            base.Columns.Add(SchemaNameColumn);
            base.Columns.Add(TextInRowLimitColumn);
            base.Columns.Add(UsesAnsiNullsColumn);
        }

		#endregion Constructors 

		#region Methods (5) 

		#region Public Methods (3) 

        public void AddUserTablesRow(UserTablesRow row)
        {
            Rows.Add(row);
        }

        public UserTablesRow NewUserTablesRow()
        {
            return ((UserTablesRow)(NewRow()));
        }

        public void RemoveUserTablesRow(UserTablesRow row)
        {
            Rows.Remove(row);
        }

		#endregion Public Methods 
		#region Protected Methods (2) 

        protected override Type GetRowType()
        {
            return typeof(UserTablesRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new UserTablesRow(builder);
        }

		#endregion Protected Methods 

		#endregion Methods 
    }
}
