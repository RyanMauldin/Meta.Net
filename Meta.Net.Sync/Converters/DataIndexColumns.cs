using System;
using Meta.Net.Types;
using Meta.Net.Interfaces;

namespace Meta.Net.Sync.Converters
{
    public class DataIndexColumns
    {
		/// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        public static bool CompareDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IIndexColumn sourceIndexColumn, IIndexColumn targetIndexColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return CompareMySqlToMySqlDataIndexColumn(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn);
                        case DataContextType.SqlServer:
                            return CompareMySqlToSqlServerDataIndexColumn(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn);
                        default:
                            return false;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            return CompareSqlServerToMySqlDataIndexColumn(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn);
                        case DataContextType.SqlServer:
                            return CompareSqlServerToSqlServerDataIndexColumn(sourceDataContext, targetDataContext, sourceIndexColumn, targetIndexColumn);
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
        /// <param name="indexColumn">The IDataIndexColumn object to convert.</param>
        public static void ConvertDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, ref IIndexColumn indexColumn)
        {
            switch (sourceDataContext.ContextType)
            {
                case DataContextType.MySql:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.SqlServer:
                            ConvertMySqlToSqlServerDataIndexColumn(sourceDataContext, targetDataContext, ref indexColumn);
                            return;
                        default:
                            return;
                    }
                case DataContextType.SqlServer:
                    switch (targetDataContext.ContextType)
                    {
                        case DataContextType.MySql:
                            ConvertSqlServerToMySqlDataIndexColumn(sourceDataContext, targetDataContext, ref indexColumn);
                            return;
                        default:
                            return;
                    }
                default:
                    return;
            }
        }

		/// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// MySql -> MySql specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareMySqlToMySqlDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IIndexColumn sourceIndexColumn, IIndexColumn targetIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareMySqlToMySqlDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareMySqlToMySqlDataIndexColumn", targetDataContext.ContextType));

            if (sourceIndexColumn.KeyOrdinal != targetIndexColumn.KeyOrdinal)
                return false;

            return sourceIndexColumn.PartitionOrdinal == targetIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// MySql -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareMySqlToSqlServerDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IIndexColumn sourceIndexColumn, IIndexColumn targetIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareMySqlToSqlServerDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareMySqlToSqlServerDataIndexColumn", targetDataContext.ContextType));

            if (sourceIndexColumn.KeyOrdinal != targetIndexColumn.KeyOrdinal)
                return false;

            return sourceIndexColumn.PartitionOrdinal == targetIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// SqlServer -> MySql specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareSqlServerToMySqlDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IIndexColumn sourceIndexColumn, IIndexColumn targetIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", targetDataContext.ContextType));

            if (sourceIndexColumn.KeyOrdinal != targetIndexColumn.KeyOrdinal)
                return false;

            return sourceIndexColumn.PartitionOrdinal == targetIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Compares IDataIndexColumn based objects based on source and target DataContext types.
        /// SqlServer -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="sourceIndexColumn">The source IDataIndexColumn.</param>
        /// <param name="targetIndexColumn">The target IDataIndexColumn.</param>
        /// <returns>
        ///     true - the datasource differentiated index column properties matched.
        ///     false - the datasource differentiated index column properties did not match.
        /// </returns>
        private static bool CompareSqlServerToSqlServerDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, IIndexColumn sourceIndexColumn, IIndexColumn targetIndexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareSqlServerToSqlServerDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareSqlServerToSqlServerDataIndexColumn", targetDataContext.ContextType));

            if (sourceIndexColumn.IsDescendingKey != targetIndexColumn.IsDescendingKey)
                return false;

            if (sourceIndexColumn.IsIncludedColumn != targetIndexColumn.IsIncludedColumn)
                return false;

            if (sourceIndexColumn.KeyOrdinal != targetIndexColumn.KeyOrdinal)
                return false;

            return sourceIndexColumn.PartitionOrdinal == targetIndexColumn.PartitionOrdinal;
        }

        /// <summary>
        /// Converts the IDataIndexColumn based object to appropriate values based on source and target DataContext types.
        /// MySql -> SqlServer specific.
        /// </summary>
        /// <param name="sourceDataContext">The source DataContext.</param>
        /// <param name="targetDataContext">The target DataContext.</param>
        /// <param name="indexColumn">The IDataIndexColumn object to convert.</param>
        private static void ConvertMySqlToSqlServerDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, ref IIndexColumn indexColumn)
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
        /// <param name="indexColumn">The IDataIndexColumn object to convert.</param>
        private static void ConvertSqlServerToMySqlDataIndexColumn(DataContext sourceDataContext, DataContext targetDataContext, ref IIndexColumn indexColumn)
        {
            if (sourceDataContext.ContextType != DataContextType.SqlServer)
                throw new Exception(string.Format("Invalid sourceDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", sourceDataContext.ContextType));

            if (targetDataContext.ContextType != DataContextType.MySql)
                throw new Exception(string.Format("Invalid targetDataContext type of {0} for DataIndexColumns.CompareSqlServerToMySqlDataIndexColumn", targetDataContext.ContextType));

            indexColumn.IsDescendingKey = false;
            indexColumn.IsIncludedColumn = false;
        } 
    }
}
