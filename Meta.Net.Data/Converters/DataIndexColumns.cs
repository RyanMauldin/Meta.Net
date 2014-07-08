using System;
using Meta.Net.Data.Enum;
using Meta.Net.Data.Interfaces;

namespace Meta.Net.Data.Converters
{
    public class DataIndexColumns
    {
		#region Methods (8) 

		#region Public Methods (2) 

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceDataIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetDataIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        public static bool CompareDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IDataIndexColumn sourceDataIndexColumn, IDataIndexColumn targetDataIndexColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return CompareMySqlToMySqlDataIndexColumn(sourceDataContext, targetDataContext, sourceDataIndexColumn, targetDataIndexColumn);
                        case DataContextType.SqlServer:
                            return CompareMySqlToSqlServerDataIndexColumn(sourceDataContext, targetDataContext, sourceDataIndexColumn, targetDataIndexColumn);
                        default:
                            return false;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return CompareSqlServerToMySqlDataIndexColumn(sourceDataContext, targetDataContext, sourceDataIndexColumn, targetDataIndexColumn);
                        case DataContextType.SqlServer:
                            return CompareSqlServerToSqlServerDataIndexColumn(sourceDataContext, targetDataContext, sourceDataIndexColumn, targetDataIndexColumn);
                        default:
                            return false;
                    }
                default:
                    return false;
            }
        }

        /// <summary>
        /// Converts the IDataIndexColumn based object to appropriate values based on source and target DataContext types.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="dataIndexColumn">The IDataIndexColumn object to convert.</param>
        public static void ConvertDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, ref IDataIndexColumn dataIndexColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.SqlServer:
                            ConvertMySqlToSqlServerDataIndexColumn(sourceDataContext, targetDataContext, ref dataIndexColumn);
                            return;
                        default:
                            return;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            ConvertSqlServerToMySqlDataIndexColumn(sourceDataContext, targetDataContext, ref dataIndexColumn);
                            return;
                        default:
                            return;
                    }
                default:
                    return;
            }
        }

		#endregion Public Methods 
		#region Private Methods (6) 

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// MySql -> MySql specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceDataIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetDataIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareMySqlToMySqlDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IDataIndexColumn sourceDataIndexColumn, IDataIndexColumn targetDataIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareMySqlToMySqlDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareMySqlToMySqlDataIndexColumn", targetDataContext.ContextType));

            if (sourceDataIndexColumn.KeyOrdinal != targetDataIndexColumn.KeyOrdinal)
                return false;

            return sourceDataIndexColumn.PartitionOrdinal == targetDataIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// MySql -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceDataIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetDataIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareMySqlToSqlServerDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IDataIndexColumn sourceDataIndexColumn, IDataIndexColumn targetDataIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareMySqlToSqlServerDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareMySqlToSqlServerDataIndexColumn", targetDataContext.ContextType));

            if (sourceDataIndexColumn.KeyOrdinal != targetDataIndexColumn.KeyOrdinal)
                return false;

            return sourceDataIndexColumn.PartitionOrdinal == targetDataIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// SqlServer -> MySql specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceDataIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetDataIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareSqlServerToMySqlDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IDataIndexColumn sourceDataIndexColumn, IDataIndexColumn targetDataIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", targetDataContext.ContextType));

            if (sourceDataIndexColumn.KeyOrdinal != targetDataIndexColumn.KeyOrdinal)
                return false;

            return sourceDataIndexColumn.PartitionOrdinal == targetDataIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// SqlServer -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceDataIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetDataIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareSqlServerToSqlServerDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IDataIndexColumn sourceDataIndexColumn, IDataIndexColumn targetDataIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareSqlServerToSqlServerDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareSqlServerToSqlServerDataIndexColumn", targetDataContext.ContextType));

            if (sourceDataIndexColumn.IsDescendingKey != targetDataIndexColumn.IsDescendingKey)
                return false;

            if (sourceDataIndexColumn.IsIncludedColumn != targetDataIndexColumn.IsIncludedColumn)
                return false;

            if (sourceDataIndexColumn.KeyOrdinal != targetDataIndexColumn.KeyOrdinal)
                return false;

            return sourceDataIndexColumn.PartitionOrdinal == targetDataIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Converts the IDataIndexColumn based object to appropriate values based on source and target DataContext types.
        /// MySql -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="dataIndexColumn">The IDataIndexColumn object to convert.</param>
        private static void ConvertMySqlToSqlServerDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, ref IDataIndexColumn dataIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareMySqlToSqlServerDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareMySqlToSqlServerDataIndexColumn", targetDataContext.ContextType));

            return;
        }

        /// <summary>
        /// Converts the IDataIndexColumn based object to appropriate values based on source and target DataContext types.
        /// SqlServer -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="dataIndexColumn">The IDataIndexColumn object to convert.</param>
        private static void ConvertSqlServerToMySqlDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, ref IDataIndexColumn dataIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", targetDataContext.ContextType));

            dataIndexColumn.IsDescendingKey = false;
            dataIndexColumn.IsIncludedColumn = false;
        }

		#endregion Private Methods 

		#endregion Methods 
    }
}
