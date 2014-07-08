using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Meta.Net.Data;
using Meta.Net.Data.Objects;
using Meta.Net.Data.SqlServer;

namespace Meta.Net.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Meta.Net ... Servicing all your database needs since 1868!");
            Console.WriteLine();

            var sourceContext = new SqlServerContext();
            var targetContext = new SqlServerContext();
            var sourceDataConnectionInfo = new SqlServerConnectionInfo("Source", "CCILTIT07", "Lifeboat", sourceContext);
            var targetDataConnectionInfo = new SqlServerConnectionInfo("Target", "CCILTIT07", "LifeboatTest3", targetContext);
            var dataProperties = new DataProperties
            {
                CatalogsToCompare = new Dictionary<string, string>
                {
                    { "Lifeboat", "LifeboatTest3" }
                },
                DataSync = false,
                TightSync = true
            };

            var sqlServerSyncManager = new SqlServerSyncManager(sourceDataConnectionInfo, targetDataConnectionInfo, dataProperties);

            var totalStart = DateTime.Now;
            var start = DateTime.Now;

            Console.WriteLine("Fetching Source Database: {}" + sourceDataConnectionInfo.ConnectionString);

            sqlServerSyncManager.FetchSourceDataModelFromServer();

            var finish = DateTime.Now;

            Console.WriteLine("Operation took {0} to complete.", finish.Subtract(start).ToString());

            start = DateTime.Now;

            Console.WriteLine("Fetching Target Database: {}" + targetDataConnectionInfo.ConnectionString);

            try
            {
                sqlServerSyncManager.FetchTargetDataModelFromServer();
            }
            catch (Exception)
            {
                // Database does not exist... create one from scratch.
                sqlServerSyncManager = new SqlServerSyncManager(sqlServerSyncManager.SourceServer, new Server("Target", targetContext), dataProperties);
            }
            

            finish = DateTime.Now;

            Console.WriteLine("Operation took {0} to complete.", finish.Subtract(start).ToString());

            start = DateTime.Now;

            Console.WriteLine("Starting database comparisons....");

            Console.WriteLine("-----------------------------------------------------");

            var sourceServer = Server.Clone(sqlServerSyncManager.SourceServer, sourceContext);
            var targetServer = Server.Clone(sqlServerSyncManager.TargetServer, targetContext);

            //var sourceServer = sqlServerSyncManager.SourceServer;
            //var targetServer = sqlServerSyncManager.TargetServer;

            foreach (var catalog in sourceServer.Catalogs.Where(catalog => catalog.Key != "Lifeboat").ToList())
                sourceServer.Catalogs.Remove(catalog.Key);
            foreach (var catalog in targetServer.Catalogs.Where(catalog => catalog.Key != "LifeboatTest3").ToList())
                targetServer.Catalogs.Remove(catalog.Key);



            var sourceCatalogCount = 0;
            var sourceSchemaCount = 0;
            var sourceUserTableCount = 0;
            var sourceIdentityCount = 0;
            var sourceStoredProcedureCount = 0;
            var sourceIndexCount = 0;
            var sourcePrimaryKeyCount = 0;
            var sourceUniqueConstraintCount = 0;
            var sourceUserTableColumnCount = 0;
            var sourceForeignKeyCount = 0;
            var sourceCheckConstraintCount = 0;
            var sourceComputedColumnCount = 0;
            var sourceDefaultConstraintCount = 0;
            var sourceAggregateFunctionCount = 0;
            var sourceInlineTableValuedFunctionCount = 0;
            var sourceScalarFunctionCount = 0;
            var sourceTableValueFunctionCount = 0;
            var sourceUserDefinedFunctionCount = 0;
            var sourceViewCount = 0;

            foreach (var catalog in sourceServer.Catalogs.Values)
            {
                sourceCatalogCount += 1;

                foreach (var schema in catalog.Schemas.Values)
                {
                    sourceSchemaCount += 1;

                    foreach (var userTable in schema.UserTables.Values)
                    {
                        sourceUserTableCount += 1;

                        sourceIdentityCount += userTable.IdentityColumns.Values.Count;
                        sourceIndexCount += userTable.Indexes.Values.Count;
                        sourcePrimaryKeyCount += userTable.PrimaryKeys.Values.Count;
                        sourceUniqueConstraintCount += userTable.UniqueConstraints.Values.Count;
                        sourceUserTableColumnCount += userTable.UserTableColumns.Values.Count;
                        sourceForeignKeyCount += userTable.ForeignKeys.Values.Count;
                        sourceCheckConstraintCount += userTable.CheckConstraints.Values.Count;
                        sourceComputedColumnCount += userTable.ComputedColumns.Values.Count;
                        sourceDefaultConstraintCount += userTable.DefaultConstraints.Values.Count;
                    }

                    sourceStoredProcedureCount += schema.StoredProcedures.Values.Count;
                    sourceAggregateFunctionCount += schema.AggregateFunctions.Values.Count;
                    sourceInlineTableValuedFunctionCount += schema.InlineTableValuedFunctions.Values.Count;
                    sourceScalarFunctionCount += schema.ScalarFunctions.Values.Count;
                    sourceTableValueFunctionCount += schema.TableValuedFunctions.Values.Count;
                    sourceUserDefinedFunctionCount += schema.UserDefinedDataTypes.Values.Count;
                    sourceViewCount += schema.Views.Values.Count;
                }
            }

            var targetCatalogCount = 0;
            var targetSchemaCount = 0;
            var targetUserTableCount = 0;
            var targetIdentityCount = 0;
            var targetStoredProcedureCount = 0;
            var targetIndexCount = 0;
            var targetPrimaryKeyCount = 0;
            var targetUniqueConstraintCount = 0;
            var targetUserTableColumnCount = 0;
            var targetForeignKeyCount = 0;
            var targetCheckConstraintCount = 0;
            var targetComputedColumnCount = 0;
            var targetDefaultConstraintCount = 0;
            var targetAggregateFunctionCount = 0;
            var targetInlineTableValuedFunctionCount = 0;
            var targetScalarFunctionCount = 0;
            var targetTableValueFunctionCount = 0;
            var targetUserDefinedFunctionCount = 0;
            var targetViewCount = 0;

            foreach (var catalog in targetServer.Catalogs.Values)
            {
                targetCatalogCount += 1;

                foreach (var schema in catalog.Schemas.Values)
                {
                    targetSchemaCount += 1;

                    foreach (var userTable in schema.UserTables.Values)
                    {
                        targetUserTableCount += 1;

                        targetIdentityCount += userTable.IdentityColumns.Values.Count;
                        targetIndexCount += userTable.Indexes.Values.Count;
                        targetPrimaryKeyCount += userTable.PrimaryKeys.Values.Count;
                        targetUniqueConstraintCount += userTable.UniqueConstraints.Values.Count;
                        targetUserTableColumnCount += userTable.UserTableColumns.Values.Count;
                        targetForeignKeyCount += userTable.ForeignKeys.Values.Count;
                        targetCheckConstraintCount += userTable.CheckConstraints.Values.Count;
                        targetComputedColumnCount += userTable.ComputedColumns.Values.Count;
                        targetDefaultConstraintCount += userTable.DefaultConstraints.Values.Count;
                    }

                    targetStoredProcedureCount += schema.StoredProcedures.Values.Count;
                    targetAggregateFunctionCount += schema.AggregateFunctions.Values.Count;
                    targetInlineTableValuedFunctionCount += schema.InlineTableValuedFunctions.Values.Count;
                    targetScalarFunctionCount += schema.ScalarFunctions.Values.Count;
                    targetTableValueFunctionCount += schema.TableValuedFunctions.Values.Count;
                    targetUserDefinedFunctionCount += schema.UserDefinedDataTypes.Values.Count;
                    targetViewCount += schema.Views.Values.Count;
                }
            }

            Console.WriteLine("Source Catalogs: {0}, Target Catalogs: {1}", sourceCatalogCount, targetCatalogCount);
            Console.WriteLine("Source Schemas: {0}, Target Schemas: {1}", sourceSchemaCount, targetSchemaCount);
            Console.WriteLine("Source User-Tables: {0}, Target User-Tables: {1}", sourceUserTableCount, targetUserTableCount);
            Console.WriteLine("Source User-Table Columns: {0}, Target User-Table Columns: {1}", sourceUserTableColumnCount, targetUserTableColumnCount);
            Console.WriteLine("Source Identity Columns: {0}, Target Identity Columns: {1}", sourceIdentityCount, targetIdentityCount);
            Console.WriteLine("Source Primary Keys: {0}, Target Primary Keys: {1}", sourcePrimaryKeyCount, targetPrimaryKeyCount);
            Console.WriteLine("Source Foreign Keys: {0}, Target Foreign Keys: {1}", sourceForeignKeyCount, targetForeignKeyCount);
            Console.WriteLine("Source Unique Constraints: {0}, Target Unique Constraints: {1}", sourceUniqueConstraintCount, targetUniqueConstraintCount);
            Console.WriteLine("Source Indexes: {0}, Target Indexes: {1}", sourceIndexCount, targetIndexCount);
            Console.WriteLine("Source Check Constraints: {0}, Target Check Constraints: {1}", sourceCheckConstraintCount, targetCheckConstraintCount);
            Console.WriteLine("Source Computed Columns: {0}, Target Computed Columns: {1}", sourceComputedColumnCount, targetComputedColumnCount);
            Console.WriteLine("Source Default Constraints: {0}, Target Default Constraints: {1}", sourceDefaultConstraintCount, targetDefaultConstraintCount);
            Console.WriteLine("Source Stored Procedures: {0}, Target Stored Procedures: {1}", sourceStoredProcedureCount, targetStoredProcedureCount);
            Console.WriteLine("Source Aggregate Functions: {0}, Target Aggregate Functions: {1}", sourceAggregateFunctionCount, targetAggregateFunctionCount);
            Console.WriteLine("Source Inline Table-Valued Functions: {0}, Target Inline Table-Valued Functions: {1}", sourceInlineTableValuedFunctionCount, targetInlineTableValuedFunctionCount);
            Console.WriteLine("Source Scalar Functions: {0}, Target Scalar Functions: {1}", sourceScalarFunctionCount, targetScalarFunctionCount);
            Console.WriteLine("Source Table-Valued Functions: {0}, Target Table-Valued Functions: {1}", sourceTableValueFunctionCount, targetTableValueFunctionCount);
            Console.WriteLine("Source User-Defined Functions: {0}, Target User-Defined Functions: {1}", sourceUserDefinedFunctionCount, targetUserDefinedFunctionCount);
            Console.WriteLine("Source Views: {0}, Target Views: {1}", sourceViewCount, targetViewCount);

            Console.WriteLine("-----------------------------------------------------");


            if (sqlServerSyncManager.CompareForSync())
            {
                finish = DateTime.Now;
                Console.WriteLine("The databases are in sync.");
                Console.WriteLine("Comparison took {0} to complete.", finish.Subtract(start).ToString());
                Console.WriteLine("Total runtime took {0} to complete.", finish.Subtract(totalStart).ToString());
            }
            else
            {
                finish = DateTime.Now;
                Console.WriteLine("The databases are not in sync.");
                Console.WriteLine("Comparison took {0} to complete.", finish.Subtract(start).ToString());
                Console.WriteLine("Total runtime took {0} to complete.", finish.Subtract(totalStart).ToString());
                
                var scripts = sqlServerSyncManager.Script();
                sqlServerSyncManager.Sync();

                var dateStamp = finish.Year + finish.Month.ToString("D2") + finish.Day.ToString("D2") + "_" + finish.Hour.ToString("D2") + "_" + finish.Minute.ToString("D2") + "_" + finish.Second.ToString("D2");
                var path = string.Format("C:\\Data\\Sync\\LifeboatSync_{0}.sql", dateStamp);

                Console.WriteLine("Created Sql file: {0}", path);

                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                using(var streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var script in scripts)
                    {
                        streamWriter.Write(script);
                        streamWriter.Write(";\r\nGO\r\n\r\n");
                    }

                    streamWriter.Flush();
                    streamWriter.Close();
                    fileStream.Close();
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey(true);
        }
    }
}
