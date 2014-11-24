Meta.Net
========

Meta.Net is a C#.NET Framework for pulling database metadata from Sql Server and MySql.

Look at Meta.Net.TestConsole project, Program.cs - GetCatalogs() method for current usage.

For now do not use anything from Meta.Net.Sync library. I broke the Sync library on purpose during a major re-write as I do not want anyone synchronizing databases with it yet.

Currently, I am working on serialization and deserialization to and from json first, before re-writing the synchronization libraries. Contact me if you are interested.


I noticed the following link:
http://technet.microsoft.com/en-us/library/aa224827(v=sql.80).aspx
I realize that I must now take an extra look for representing these differences, although as far as just pulling metadata it might, but I need to recheck. I believe I have a TODO comment to check for where IsUnique on Indexes that needs to remain instead of remove or I may need to re-write the metadata queries to include unique indexes as well as the constraints. I thought they were one in the same, however creating a unique constraint also creates the unique index, however keeps an instance in the unique constraint metadata. But you can create a Unique Index by itself without the constraint. This I messed up in the sync library thinking they were one in the same. So that is one of many reasons I broke the Meta.Net.Sync library. I will prob get around to the fixes over the next year unless i get some team effort or someone saying they need this library. I am not sure what the deal is in MySql side either, will have to check there as well. I prob need to support MariaDB as well since it may be used more now than MySql itself?

BTW... this is a side project, I don't have much time to devote to it. Anyone want to donate to continue that work, please do.
