Meta.Net
========

Meta.Net is a C#.NET Framework for pulling database metadata from Sql Server and MySql. For now do not use anything from Meta.Net.Sync library. I broke this on purpose during a major re-write as I do not want anyone synchronizing databases with it yet.

Look at Meta.Net.TestConsole project, Program.cs - GetCatalogs() method for current usage.

I am working on serialization and deserialization to and from json first, before re-writing the synchronization libraries. Contact me if you are interested.
