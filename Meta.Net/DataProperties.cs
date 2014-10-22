using System;
using System.Collections.Generic;

namespace Meta.Net
{
    /// <summary>
    /// Class for defining UI properties and gives a common object to pass down to
    /// SyncManager and SyncComparer objects.
    /// </summary>
    public class DataProperties
    {
		#region Properties (14) 

        /// <summary>
        /// Leave empty if you are comparing two seperate servers and you wish to simply
        /// compare every catalog and schema for replication. Use the following to add
        /// catalogs to compare that have a different name either on the same connection
        /// string or different connection strings. However, when using this method only
        /// the catalogs in question will be compared.
        /// Example: SiteEasy, SiteEasy2
        /// </summary>
        public Dictionary<string, string> CatalogsToCompare { get; set; }

        /// <summary>
        /// The default value is false.
        /// When set to true the DataSyncManager will synchronize the database objects and data. When
        /// set to false the DataSyncManager will only synchronize the database objects.
        /// </summary>
        public bool DataSync { get; set; }

        /// <summary>
        /// must include Catalog, Schema, and User-Table for user-table namespace
        /// </summary>
        public HashSet<string> TablesForSync { get; set; }

        /// <summary>
        /// Default Collation: SQL_Latin1_General_CP1_CI_AS
        /// </summary>
        public static string DefaultCollation { get; set; }

        /// <summary>
        /// Defaults to StringComparer.Ordinal for comparing module definitions.
        /// This application does not parse the entire module definition
        /// and warn for correctness if you make a mistake at this time.
        /// It only compares for spelling exactness by default and assumes
        /// you are pulling the module from an existing database, and that
        /// it is correct and already works... because it already exists
        /// in a database. If you write a custom one and it doesn't work
        /// my bad, hopefully I make enough money from doing this in my
        /// spare time to write a version two. Hey, at least it is fast :)
        /// </summary>
        public static StringComparer DefinitionComparer { get; set; }

        /// <summary>
        /// Defaults to true.
        /// If ReplicateItemsMarkedNotForReplication is set to true:
        /// This property is never looked at.
        /// 
        /// If ReplicateItemsMarkedNotForReplication is set to false:
        /// If this property is set to true sql will be generated to remove the objects
        /// from the target database to ensure it is only a replication of the source database.
        /// If this property is set to false the objects marked as NOT FOR REPLICATION will
        /// be skipped... however this could result in errors during synchronization and the
        /// transaction will be rolled back.
        /// </summary>
        public static bool DropItemsInTargetOnNotForReplication { get; set; }

        /// <summary>
        /// Add modules to ignore to this hashset in proper namespace format.
        /// Uses: StringComparer.OrdinalIgnoreCase for comparisons.
        /// Default:
        /// [dbo].[sysdiagrams]
        /// </summary>
        public static HashSet<string> IgnoredModules { get; set; }

        /// <summary>
        /// Add user-tables to ignore to this hashset in proper namespace format.
        /// Uses: StringComparer.OrdinalIgnoreCase for comparisons.
        /// Default:
        /// [dbo].[sp_upgraddiagrams]
        ///	[dbo].[sp_helpdiagrams]
        ///	[dbo].[sp_helpdiagramdefinition]
        ///	[dbo].[sp_creatediagram]
        ///	[dbo].[sp_renamediagram]
        ///	[dbo].[sp_alterdiagram]
        ///	[dbo].[sp_dropdiagram]
        /// </summary>
        public static HashSet<string> IgnoredUserTables { get; set; }

        /// <summary>
        /// The following catalogs can never be dropped in SQL Server.
        /// Default:
        /// [master]
        /// [model]
        /// [msdb]
        /// [tempdb]
        /// </summary>
        public static HashSet<string> NonRemovableCatalogs { get; private set; }

        /// <summary>
        /// The following schemas can never be dropped in SQL Server.
        /// Default:
        /// [dbo]
        /// [guest]
        /// [INFORMATION_SCHEMA]
        /// [sys]
        /// [db_owner]
        /// [db_accessadmin]
        /// [db_securityadmin]
        /// [db_ddladmin]
        /// [db_backupoperator]
        /// [db_datareader]
        /// [db_datawriter]
        /// [db_denydatareader]
        /// [db_denydatawriter]
        /// </summary>
        public static HashSet<string> NonRemovableSchemas { get; private set; }

        /// <summary>
        /// Defaults to false.
        /// Foreign keys, check constraints, identity columns, and triggers can be created with the
        /// NOT FOR REPLICATION option so that they are not copied to the target database. Setting
        /// this property to true means that we will copy all database objects even if they are
        /// marked as not replicatable, for testing, production etc... If you want to simply
        /// replicate a database and not include items marked as NOT FOR REPLICATION, set
        /// this property to false.
        /// </summary>
        public static bool ReplicateItemsMarkedNotForReplication { get; set; }

        /// <summary>
        /// Default for ignoring SQL Server file groups is true.
        /// Every database has a primary filegroup. This filegroup contains the primary data file and
        /// any secondary files that are not put into other filegroups. User-defined filegroups can
        /// be created to group data files together for administrative, data allocation, and placement
        /// purposes. For example, three files, Data1.ndf, Data2.ndf, and Data3.ndf, can be created
        /// on three disk drives, respectively, and assigned to the filegroup fgroup1. A table can
        /// then be created specifically on the filegroup fgroup1. Queries for data from the table
        /// will be spread across the three disks; this will improve performance. The same performance
        /// improvement can be accomplished by using a single file created on a RAID (redundant array
        /// of independent disks) stripe set. However, files and filegroups let you easily add new
        /// files to new disks.
        /// More info available at: http://msdn.microsoft.com/en-us/library/ms189563.aspx
        /// </summary>
        public static bool SqlServerIgnoreFileGroups { get; set; }

        /// <summary>
        /// The default value is false. Not really sure whether this will matter when we are performing
        /// transactions around index creation.
        /// You can create, rebuild, or drop indexes online. The ONLINE option allows concurrent user
        /// access to the underlying table or clustered index data and any associated nonclustered indexes
        /// during these index operations. For example, while a clustered index is being rebuilt by one
        /// user, that user and others can continue to update and query the underlying data. When you
        /// perform DDL operations offline, such as building or rebuilding a clustered index; these
        /// operations hold exclusive locks on the underlying data and associated indexes. This prevents
        /// modifications and queries to the underlying data until the index operation is complete.
        /// More info available at: http://msdn.microsoft.com/en-us/library/ms191261(SQL.90).aspx
        /// and: http://msdn.microsoft.com/en-us/library/ms177442.aspx
        /// and: http://technet.microsoft.com/en-us/library/ms188388.aspx
        /// </summary>
        public static bool SqlServerOnlineOption { get; set; }

        /// <summary>
        /// The default value is false.
        /// When you create or rebuild an index, by setting the SORT_IN_TEMPDB option to ON you can direct
        /// the SQL Server Database Engine to use tempdb to store the intermediate sort results that are
        /// used to build the index. Although this option increases the amount of temporary disk space
        /// that is used to create an index, the option could reduce the time that is required to create
        /// or rebuild an index when tempdb is on a set of disks different from that of the user database.
        /// Tempdb must contain enough disk space for the option to work or it will rollback the index and
        /// waste alot of time.
        /// More info available at: http://msdn.microsoft.com/en-us/library/ms188281.aspx
        /// and: http://technet.microsoft.com/en-us/library/ms188388.aspx
        /// </summary>
        public static bool SqlServerSortInTempDbOption { get; set; }

        /// <summary>
        /// The default value is false.
        /// Determines whether or not we drop target database objects that do not
        /// exist in the source database. Schemas, Tables, Modules, Permissions, etc.
        /// </summary>
        public bool TightSync { get; set; }

		#endregion Properties 

		#region Constructors (2) 

        /// <summary>
        /// Default static constructor for data properties.
        /// 
        /// Defaults:
        /// DefinitionComparer - StringComparer.Ordinal
        /// ReplicateItemsMarkedNotForReplication - false,
        /// DropItemsInTargetOnNotForReplication - true,
        /// SqlServerIgnoreFileGroups - true,
        /// SqlServerOnlineOption - false,
        /// SqlServerSortInTempDbOption - false.
        /// </summary>
     	static DataProperties()
		{
            NonRemovableCatalogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            NonRemovableSchemas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            IgnoredModules = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            IgnoredUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            NonRemovableCatalogs.Add("[master]");
            NonRemovableCatalogs.Add("[model]");
            NonRemovableCatalogs.Add("[msdb]");
            NonRemovableCatalogs.Add("[tempdb]");

            NonRemovableSchemas.Add("[dbo]");
            NonRemovableSchemas.Add("[guest]");
            NonRemovableSchemas.Add("[INFORMATION_SCHEMA]");
            NonRemovableSchemas.Add("[sys]");
            NonRemovableSchemas.Add("[db_owner]");
            NonRemovableSchemas.Add("[db_accessadmin]");
            NonRemovableSchemas.Add("[db_securityadmin]");
            NonRemovableSchemas.Add("[db_ddladmin]");
            NonRemovableSchemas.Add("[db_backupoperator]");
            NonRemovableSchemas.Add("[db_datareader]");
            NonRemovableSchemas.Add("[db_datawriter]");
            NonRemovableSchemas.Add("[db_denydatareader]");
            NonRemovableSchemas.Add("[db_denydatawriter]");

            IgnoredUserTables.Add("[dbo].[sysdiagrams]");

            IgnoredModules.Add("[dbo].[sp_upgraddiagrams]");
            IgnoredModules.Add("[dbo].[sp_helpdiagrams]");
            IgnoredModules.Add("[dbo].[sp_helpdiagramdefinition]");
            IgnoredModules.Add("[dbo].[sp_creatediagram]");
            IgnoredModules.Add("[dbo].[sp_renamediagram]");
            IgnoredModules.Add("[dbo].[sp_alterdiagram]");
            IgnoredModules.Add("[dbo].[sp_dropdiagram]");

            DefaultCollation = "SQL_Latin1_General_CP1_CI_AS";

		    DefinitionComparer = StringComparer.Ordinal;
            ReplicateItemsMarkedNotForReplication = false;
            DropItemsInTargetOnNotForReplication = true;
            SqlServerIgnoreFileGroups = true;
            SqlServerOnlineOption = false;
            SqlServerSortInTempDbOption = false;
		}

        /// <summary>
        /// Default constructor for data properties.
        /// 
        /// Defaults:
        /// TightSync - false,
        /// DataSync - false.
        /// </summary>
        public DataProperties()
        {
            
            TightSync = true;
            DataSync = false;
            CatalogsToCompare = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            TablesForSync = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

		#endregion Constructors 
    }
}
