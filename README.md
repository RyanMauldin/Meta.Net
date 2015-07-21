Meta.Net
========

Meta.Net is a C#.NET Framework for pulling database metadata from Sql Server and MySql.

Look at Meta.Net.TestConsole project, Program.cs - GetCatalogs() method for current usage.

For now do not use anything from Meta.Net.Sync library. I broke the Sync library on purpose during a major re-write as I do not want anyone synchronizing databases with it yet.

I just finished a big refactor with serialization and deserialization decorating all Meta objects and base objects with DataContract and DataMember attributes for use with the DataContractSerializer. This helped me keep object references for use with Deep Cloning and WCF. Look at the extension methods provided in the SerializationExtensions class, which also has some Json.NET extension methods so I could rip them out of the Meta object classes. The adapters have been overhauled with TAP pattern (async/await) and can accept CancellationToken.

Contact me if you are interested in helping me finish this.

My next steps are to uncomment the HashSet like functionality (IntersectWith, ExceptWith, UnionWith) into the Meta objects like Server, Catalog, ForeignKey, etc. to be able to make change sets between two Servers/Catalogs again. This will allow me to then pull back in, scripting out the change sets into SQL scripts.


I noticed the following link:
http://technet.microsoft.com/en-us/library/aa224827(v=sql.80).aspx
I realize that I must now take an extra look for representing these differences, although as far as just pulling metadata it might, but I need to recheck. I believe I have a TODO comment to check for where IsUnique on Indexes that needs to remain instead of remove or I may need to re-write the metadata queries to include unique indexes as well as the constraints. I thought they were one in the same, however creating a unique constraint also creates the unique index, however keeps an instance in the unique constraint metadata. But you can create a Unique Index by itself without the constraint. This I messed up in the sync library thinking they were one in the same. So that is one of many reasons I broke the Meta.Net.Sync library. I will prob get around to the fixes over the next year unless i get some team effort or someone saying they need this library. I am not sure what the deal is in MySql side either, will have to check there as well. I prob need to support MariaDB as well since it may be used more now than MySql itself?

BTW... this is a side project, I don't have much time to devote to it. Anyone want to donate to continue that work, please do.
