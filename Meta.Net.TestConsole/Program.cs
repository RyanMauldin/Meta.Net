using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Meta.Net.Metadata;
using Meta.Net.Objects;
using Meta.Net.SqlServer;

namespace Meta.Net.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestStringBuilders();

            Console.WriteLine(GetCatalogs());
            Console.WriteLine("Press any key to quit.");
            Console.ReadKey(true);

            //#region Original

            //Console.WriteLine("Welcome to Meta.Net ... Servicing all your database needs since 1868!");
            //Console.WriteLine();

            //var sourceContext = new SqlServerContext();
            //var targetContext = new SqlServerContext();
            //var sourceDataConnectionInfo = new SqlServerConnectionInfo("Source", "CCILTIT54", "Lifeboat", sourceContext);
            //var targetDataConnectionInfo = new SqlServerConnectionInfo("Target", "CCILTIT54", "LifeboatTest3", targetContext);
            //var dataProperties = new DataProperties
            //{
            //    CatalogsToCompare = new Dictionary<string, string>
            //    {
            //        { "Lifeboat", "LifeboatTest3" }
            //    },
            //    DataSync = false,
            //    TightSync = true
            //};

            //var sqlServerSyncManager = new SqlServerSyncManager(sourceDataConnectionInfo, targetDataConnectionInfo, dataProperties);

            //var totalStart = DateTime.Now;
            //var start = DateTime.Now;

            //Console.WriteLine("Fetching Source Database: {}" + sourceDataConnectionInfo.ConnectionString);

            //sqlServerSyncManager.FetchSourceDataModelFromServer();

            //var finish = DateTime.Now;

            //Console.WriteLine("Operation took {0} to complete.", finish.Subtract(start).ToString());

            //start = DateTime.Now;

            //Console.WriteLine("Fetching Target Database: {}" + targetDataConnectionInfo.ConnectionString);

            //try
            //{
            //    sqlServerSyncManager.FetchTargetDataModelFromServer();
            //}
            //catch (Exception)
            //{
            //    // Database does not exist... create one from scratch.
            //    sqlServerSyncManager = new SqlServerSyncManager(sqlServerSyncManager.SourceServer, new Server("Target", targetContext), dataProperties);
            //}
            

            //finish = DateTime.Now;

            //Console.WriteLine("Operation took {0} to complete.", finish.Subtract(start).ToString());

            //start = DateTime.Now;

            //Console.WriteLine("Starting database comparisons....");

            //Console.WriteLine("-----------------------------------------------------");

            //var sourceServer = Server.Clone(sqlServerSyncManager.SourceServer, sourceContext);
            //var targetServer = Server.Clone(sqlServerSyncManager.TargetServer, targetContext);

            //foreach (var catalog in sourceServer.Catalogs.Where(p => StringComparer.OrdinalIgnoreCase.Compare(p.Namespace, "Lifeboat") != 0).ToList())
            //    sourceServer.Catalogs.Remove(catalog.Namespace);
            //foreach (var catalog in targetServer.Catalogs.Where(p => StringComparer.OrdinalIgnoreCase.Compare(p.Namespace, "Lifeboat3") != 0).ToList())
            //    targetServer.Catalogs.Remove(catalog.Namespace);

            //var sourceCatalogCount = 0;
            //var sourceSchemaCount = 0;
            //var sourceUserTableCount = 0;
            //var sourceIdentityCount = 0;
            //var sourceStoredProcedureCount = 0;
            //var sourceIndexCount = 0;
            //var sourcePrimaryKeyCount = 0;
            //var sourceUniqueConstraintCount = 0;
            //var sourceUserTableColumnCount = 0;
            //var sourceForeignKeyCount = 0;
            //var sourceCheckConstraintCount = 0;
            //var sourceComputedColumnCount = 0;
            //var sourceDefaultConstraintCount = 0;
            //var sourceAggregateFunctionCount = 0;
            //var sourceInlineTableValuedFunctionCount = 0;
            //var sourceScalarFunctionCount = 0;
            //var sourceTableValueFunctionCount = 0;
            //var sourceUserDefinedFunctionCount = 0;
            //var sourceViewCount = 0;

            //foreach (var catalog in sourceServer.Catalogs)
            //{
            //    sourceCatalogCount += 1;

            //    foreach (var schema in catalog.Schemas)
            //    {
            //        sourceSchemaCount += 1;

            //        foreach (var userTable in schema.UserTables)
            //        {
            //            sourceUserTableCount += 1;

            //            sourceIdentityCount += userTable.IdentityColumns.Count;
            //            sourceIndexCount += userTable.Indexes.Count;
            //            sourcePrimaryKeyCount += userTable.PrimaryKeys.Count;
            //            sourceUniqueConstraintCount += userTable.UniqueConstraints.Count;
            //            sourceUserTableColumnCount += userTable.UserTableColumns.Count;
            //            sourceForeignKeyCount += userTable.ForeignKeys.Count;
            //            sourceCheckConstraintCount += userTable.CheckConstraints.Count;
            //            sourceComputedColumnCount += userTable.ComputedColumns.Count;
            //            sourceDefaultConstraintCount += userTable.DefaultConstraints.Count;
            //        }

            //        sourceStoredProcedureCount += schema.StoredProcedures.Count;
            //        sourceAggregateFunctionCount += schema.AggregateFunctions.Count;
            //        sourceInlineTableValuedFunctionCount += schema.InlineTableValuedFunctions.Count;
            //        sourceScalarFunctionCount += schema.ScalarFunctions.Count;
            //        sourceTableValueFunctionCount += schema.TableValuedFunctions.Count;
            //        sourceUserDefinedFunctionCount += schema.UserDefinedDataTypes.Count;
            //        sourceViewCount += schema.Views.Count;
            //    }
            //}

            //var targetCatalogCount = 0;
            //var targetSchemaCount = 0;
            //var targetUserTableCount = 0;
            //var targetIdentityCount = 0;
            //var targetStoredProcedureCount = 0;
            //var targetIndexCount = 0;
            //var targetPrimaryKeyCount = 0;
            //var targetUniqueConstraintCount = 0;
            //var targetUserTableColumnCount = 0;
            //var targetForeignKeyCount = 0;
            //var targetCheckConstraintCount = 0;
            //var targetComputedColumnCount = 0;
            //var targetDefaultConstraintCount = 0;
            //var targetAggregateFunctionCount = 0;
            //var targetInlineTableValuedFunctionCount = 0;
            //var targetScalarFunctionCount = 0;
            //var targetTableValueFunctionCount = 0;
            //var targetUserDefinedFunctionCount = 0;
            //var targetViewCount = 0;

            //foreach (var catalog in targetServer.Catalogs)
            //{
            //    targetCatalogCount += 1;

            //    foreach (var schema in catalog.Schemas)
            //    {
            //        targetSchemaCount += 1;

            //        foreach (var userTable in schema.UserTables)
            //        {
            //            targetUserTableCount += 1;

            //            targetIdentityCount += userTable.IdentityColumns.Count;
            //            targetIndexCount += userTable.Indexes.Count;
            //            targetPrimaryKeyCount += userTable.PrimaryKeys.Count;
            //            targetUniqueConstraintCount += userTable.UniqueConstraints.Count;
            //            targetUserTableColumnCount += userTable.UserTableColumns.Count;
            //            targetForeignKeyCount += userTable.ForeignKeys.Count;
            //            targetCheckConstraintCount += userTable.CheckConstraints.Count;
            //            targetComputedColumnCount += userTable.ComputedColumns.Count;
            //            targetDefaultConstraintCount += userTable.DefaultConstraints.Count;
            //        }

            //        targetStoredProcedureCount += schema.StoredProcedures.Count;
            //        targetAggregateFunctionCount += schema.AggregateFunctions.Count;
            //        targetInlineTableValuedFunctionCount += schema.InlineTableValuedFunctions.Count;
            //        targetScalarFunctionCount += schema.ScalarFunctions.Count;
            //        targetTableValueFunctionCount += schema.TableValuedFunctions.Count;
            //        targetUserDefinedFunctionCount += schema.UserDefinedDataTypes.Count;
            //        targetViewCount += schema.Views.Count;
            //    }
            //}

            //Console.WriteLine("Source Catalogs: {0}, Target Catalogs: {1}", sourceCatalogCount, targetCatalogCount);
            //Console.WriteLine("Source Schemas: {0}, Target Schemas: {1}", sourceSchemaCount, targetSchemaCount);
            //Console.WriteLine("Source User-Tables: {0}, Target User-Tables: {1}", sourceUserTableCount, targetUserTableCount);
            //Console.WriteLine("Source User-Table Columns: {0}, Target User-Table Columns: {1}", sourceUserTableColumnCount, targetUserTableColumnCount);
            //Console.WriteLine("Source Identity Columns: {0}, Target Identity Columns: {1}", sourceIdentityCount, targetIdentityCount);
            //Console.WriteLine("Source Primary Keys: {0}, Target Primary Keys: {1}", sourcePrimaryKeyCount, targetPrimaryKeyCount);
            //Console.WriteLine("Source Foreign Keys: {0}, Target Foreign Keys: {1}", sourceForeignKeyCount, targetForeignKeyCount);
            //Console.WriteLine("Source Unique Constraints: {0}, Target Unique Constraints: {1}", sourceUniqueConstraintCount, targetUniqueConstraintCount);
            //Console.WriteLine("Source Indexes: {0}, Target Indexes: {1}", sourceIndexCount, targetIndexCount);
            //Console.WriteLine("Source Check Constraints: {0}, Target Check Constraints: {1}", sourceCheckConstraintCount, targetCheckConstraintCount);
            //Console.WriteLine("Source Computed Columns: {0}, Target Computed Columns: {1}", sourceComputedColumnCount, targetComputedColumnCount);
            //Console.WriteLine("Source Default Constraints: {0}, Target Default Constraints: {1}", sourceDefaultConstraintCount, targetDefaultConstraintCount);
            //Console.WriteLine("Source Stored Procedures: {0}, Target Stored Procedures: {1}", sourceStoredProcedureCount, targetStoredProcedureCount);
            //Console.WriteLine("Source Aggregate Functions: {0}, Target Aggregate Functions: {1}", sourceAggregateFunctionCount, targetAggregateFunctionCount);
            //Console.WriteLine("Source Inline Table-Valued Functions: {0}, Target Inline Table-Valued Functions: {1}", sourceInlineTableValuedFunctionCount, targetInlineTableValuedFunctionCount);
            //Console.WriteLine("Source Scalar Functions: {0}, Target Scalar Functions: {1}", sourceScalarFunctionCount, targetScalarFunctionCount);
            //Console.WriteLine("Source Table-Valued Functions: {0}, Target Table-Valued Functions: {1}", sourceTableValueFunctionCount, targetTableValueFunctionCount);
            //Console.WriteLine("Source User-Defined Functions: {0}, Target User-Defined Functions: {1}", sourceUserDefinedFunctionCount, targetUserDefinedFunctionCount);
            //Console.WriteLine("Source Views: {0}, Target Views: {1}", sourceViewCount, targetViewCount);

            //Console.WriteLine("-----------------------------------------------------");


            //if (sqlServerSyncManager.CompareForSync())
            //{
            //    finish = DateTime.Now;
            //    Console.WriteLine("The databases are in sync.");
            //    Console.WriteLine("Comparison took {0} to complete.", finish.Subtract(start).ToString());
            //    Console.WriteLine("Total runtime took {0} to complete.", finish.Subtract(totalStart).ToString());
            //}
            //else
            //{
            //    finish = DateTime.Now;
            //    Console.WriteLine("The databases are not in sync.");
            //    Console.WriteLine("Comparison took {0} to complete.", finish.Subtract(start).ToString());
            //    Console.WriteLine("Total runtime took {0} to complete.", finish.Subtract(totalStart).ToString());
                
            //    var scripts = sqlServerSyncManager.Script();
            //    sqlServerSyncManager.Sync();

            //    var dateStamp = finish.Year + finish.Month.ToString("D2") + finish.Day.ToString("D2") + "_" + finish.Hour.ToString("D2") + "_" + finish.Minute.ToString("D2") + "_" + finish.Second.ToString("D2");
            //    var path = string.Format("C:\\Data\\Sync\\LifeboatSync_{0}.sql", dateStamp);

            //    Console.WriteLine("Created Sql file: {0}", path);

            //    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            //    using(var streamWriter = new StreamWriter(fileStream))
            //    {
            //        foreach (var script in scripts)
            //        {
            //            streamWriter.Write(script);
            //            streamWriter.Write(";\r\nGO\r\n\r\n");
            //        }

            //        streamWriter.Flush();
            //        streamWriter.Close();
            //        fileStream.Close();
            //    }
            //}

            //Console.WriteLine();
            //Console.WriteLine("Press any key to quit.");
            //Console.ReadKey(true);

            //#endregion Original
        }

        public static string GetCatalogs()
        {
            var builder = new StringBuilder();
            builder.AppendLine("\nCatalogs:\n");

            //try
            //{
                var start = DateTime.Now;
                TimeSpan timeSpan;
                const string connectionString = "Data Source=localhost;Initial Catalog=master;Integrated Security=SSPI;Persist Security Info=no;Encrypt=yes;TrustServerCertificate=yes";
                using (DbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var startTime = DateTime.Now;
                    var metadataScriptFactory = new SqlServerMetadataScriptFactory();
                    var server = new Server()
                    {
                        ObjectName = "localhost",
                        DataContext = new SqlServerContext()
                    };
                    //CatalogsAdapter.Build(server, connection, metadataScriptFactory);
                    CatalogsAdapter.BuildSpecific(server, connection, metadataScriptFactory, new []{ "Lifeboat" });
                    var finishTime = DateTime.Now;
                    timeSpan = finishTime.Subtract(startTime);
                    connection.Close();

                    foreach (var catalog in server.Catalogs)
                    {
                        //if (catalog.ObjectName != "Lifeboat")
                        //    continue;

                        builder.AppendFormat("[ObjectName]: {0}", catalog.ObjectName);
                        builder.AppendFormat(", [IsAnsiPaddingOn]: {0}\n", catalog.IsAnsiPaddingOn);

                        foreach (var schema in catalog.Schemas)
                        {
                            builder.AppendFormat("    [Schema]: {0}\n", schema.ObjectName);
                            foreach (var userTable in schema.UserTables)
                            {
                                builder.AppendFormat("        [UserTable]: {0}\n", userTable.ObjectName);
                                foreach (var foreignKey in userTable.ForeignKeys)
                                {
                                    builder.AppendFormat("            [ForeignKey]: {0}\n", foreignKey.ObjectName);
                                    foreach (var foreignKeyColumn in foreignKey.ForeignKeyColumns)
                                    {
                                        builder.AppendFormat("                [ForeignKeyColumn]: {0}\n", foreignKeyColumn.ObjectName);
                                    }
                                }
                            }
                        }
                            
                    }
                }

                var finish = DateTime.Now;
                var span = finish.Subtract(start);
                builder.AppendFormat("\n\nTotal connection time: {0}", span.TotalMilliseconds);
                builder.AppendFormat("\n\nTotal read time: {0}\n\n", timeSpan.TotalMilliseconds);
            //}
            //catch (Exception e)
            //{
            //    builder.AppendLine("Exception: ").AppendLine(e.Message);
            //    if (e.InnerException != null)
            //        builder.AppendLine().Append("Inner Exception: ").AppendLine(e.InnerException.Message);
            //}

            builder.AppendLine();
            return builder.ToString();
        }

        public static void TestStringBuilders()
        {
            var cycles = 10000000;
            var testString1 = "SchemaName";
            var testString2 = "CatalogName";
            var testString3 = "ObjectName";
            Console.WriteLine("<---  String Builder Test - Short Strings: ({0}) cycles--->", cycles);
            Console.WriteLine(StringBuilderTest(cycles, testString1, testString2, testString3));
            testString1 = "SchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaNameSchemaName";
            testString2 = "CatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogNameCatalogName";
            testString3 = "ObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectNameObjectName";
            Console.WriteLine("<---  String Builder Test 2 - Long Strings: ({0}) cycles --->", cycles);
            Console.WriteLine(StringBuilderTest(cycles, testString1, testString2, testString3));
        }
        
        public static string StringBuilderTest(int cycles, string testString1, string testString2, string testString3)
        {
            if (cycles <= 0)
                cycles = 1;

            if (string.IsNullOrEmpty(testString1))
                testString1 = string.Empty;
            if (string.IsNullOrEmpty(testString2))
                testString2 = string.Empty;
            if (string.IsNullOrEmpty(testString3))
                testString3 = string.Empty;

            var start1 = DateTime.UtcNow;
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder(testString1.Length + testString2.Length + testString3.Length + 2);
                builder.AppendFormat("{0}.{1}.{2}", testString1, testString2, testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish1 = DateTime.UtcNow;

            var start2 = DateTime.UtcNow;
            // This is the fastest method by far (same as below but with method chaining) ~ Sometimes its faster, sometimes its not!!!
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder(testString1.Length + testString2.Length + testString3.Length + 2);
                builder.Append(testString1).Append(".").Append(testString2).Append(".").Append(testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish2 = DateTime.UtcNow;

            var start3 = DateTime.UtcNow;
            // This is the fastest method by far (same as above but seperated statements) ~ Sometimes its faster, sometimes its not!!!
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder(testString1.Length + testString2.Length + testString3.Length + 2);
                builder.Append(testString1);
                builder.Append(".");
                builder.Append(testString2);
                builder.Append(".");
                builder.Append(testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish3 = DateTime.UtcNow;

            var start4 = DateTime.UtcNow;
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder();
                builder.AppendFormat("{0}.{1}.{2}", testString1, testString2, testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish4 = DateTime.UtcNow;

            var start5 = DateTime.UtcNow;
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder();
                builder.Append(testString1).Append(".").Append(testString2).Append(".").Append(testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish5 = DateTime.UtcNow;

            var start6 = DateTime.UtcNow;
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder(testString1.Length + testString2.Length + testString3.Length + 2);
                builder.Append(string.Format("{0}.{1}.{2}", testString1, testString2, testString3));
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish6 = DateTime.UtcNow;

            var start7 = DateTime.UtcNow;
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder(testString1.Length + testString2.Length + testString3.Length + 2);
                builder.Append(testString1 + "." + testString2 + "." + testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish7 = DateTime.UtcNow;

            var start8 = DateTime.UtcNow;
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder();
                builder.Append(string.Format("{0}.{1}.{2}", testString1, testString2, testString3));
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish8 = DateTime.UtcNow;

            var start9 = DateTime.UtcNow;
            // This method has memory issues when called many times
            for (var i = 0; i < cycles; i++)
            {
                var builder = new StringBuilder();
                builder.Append(testString1 + "." + testString2 + "." + testString3);
                var result = builder.ToString();
                builder = new StringBuilder(result);
                builder.Clear();
            }
            var finish9 = DateTime.UtcNow;

            var resultBuilder = new StringBuilder(100);
            resultBuilder.Append("Time 1 - Known Length, AppendFormat           : ").AppendLine(Convert.ToString((finish1 - start1).TotalSeconds));
            resultBuilder.Append("Time 2 - Known Length, Chained Statements     : ").AppendLine(Convert.ToString((finish2 - start2).TotalSeconds));
            resultBuilder.Append("Time 3 - Known Length, Seperated Statements   : ").AppendLine(Convert.ToString((finish3 - start3).TotalSeconds));
            resultBuilder.Append("Time 4 - Unknown Length, AppendFormat         : ").AppendLine(Convert.ToString((finish4 - start4).TotalSeconds));
            resultBuilder.Append("Time 5 - Unknown Length, Chained Statements   : ").AppendLine(Convert.ToString((finish5 - start5).TotalSeconds));
            resultBuilder.Append("Time 6 - Known Length, string.Format, Append  : ").AppendLine(Convert.ToString((finish6 - start6).TotalSeconds));
            resultBuilder.Append("Time 7 - Known Length, concat +, Append       : ").AppendLine(Convert.ToString((finish7 - start7).TotalSeconds));
            resultBuilder.Append("Time 8 - Unknown Length, string.Format, Append: ").AppendLine(Convert.ToString((finish8 - start8).TotalSeconds));
            resultBuilder.Append("Time 9 - Unknown Length, concat +, Append     : ").AppendLine(Convert.ToString((finish9 - start9).TotalSeconds));
            resultBuilder.AppendLine();
            return resultBuilder.ToString();
        }
    }
}
