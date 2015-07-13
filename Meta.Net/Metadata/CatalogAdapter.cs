using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Meta.Net.Interfaces;
using Meta.Net.Metadata.Factories;
using Meta.Net.Objects;

namespace Meta.Net.Metadata
{
    public static class CatalogAdapter
    {
        /// <summary>
        /// Populates the catalogs in the server object from an IDataReader. This method is used by
        /// GetXXX() and BuildXXX() methods. They are marked public, in case they are needed for custom work.
        /// Use the GetXXX() and BuildXXX() methods for default metadata access.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="reader">The DbDataReader to read in metadata.</param>
        public static void Read(
            Server server,
            IDataReader reader)
        {
            var factory = new CatalogFactory(reader);

            while (reader.Read())
                factory.CreateCatalog(server, reader);
        }

        /// <summary>
        /// Populates the catalogs in the server object from a DBDataReader. This method is used by
        /// GetXXX() and BuildXXX() methods. They are marked public, in case they are needed for custom work.
        /// Use the GetXXX() and BuildXXX() methods for default metadata access. Note that IDataReader did
        /// not expose the ReadAsync method.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="reader">The DbDataReader to read in metadata.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        public static async Task ReadAsync(
            Server server,
            DbDataReader reader,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var factory = new CatalogFactory(reader);

            while (await reader.ReadAsync(cancellationToken))
            {
                cancellationToken.ThrowIfCancellationRequested();
                factory.CreateCatalog(server, reader);
            }
        }

        /// <summary>
        /// Fills only the catalogs in the server object.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The metadata script factory determined by the database type.</param>
        public static void Get(
            Server server,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Catalogs();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(server, reader);

                    reader.Close();
                }
            }
        }
        
        /// <summary>
        /// Fills only the catalogs in the server object.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The metadata script factory determined by the database type.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        public static async Task GetAsync(
            Server server, DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Catalogs();
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    await ReadAsync(server, reader, cancellationToken);

                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Fills only specific catalogs in the server object.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="catalogs">The catalogs to filter.</param>
        public static void GetSpecific(
            Server server,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            IList<string> catalogs)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Catalogs(catalogs);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    Read(server, reader);

                    reader.Close();
                }
            }
        }
        
        /// <summary>
        /// Fills only specific catalogs in the server object.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="catalogs">The catalogs to filter.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        public static async Task GetSpecificAsync(
            Server server, DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            IList<string> catalogs,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = metadataScriptFactory.Catalogs(catalogs);
                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        return;
                    }

                    await ReadAsync(server, reader, cancellationToken);

                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Fills all catalogs in the server object as well as building out the schemas
        /// and all objects in the database connection.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        public static void Build(
            Server server,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory)
        {
            Get(server, connection, metadataScriptFactory);
            foreach (var catalog in server.Catalogs)
                SchemaAdapter.Build(catalog, connection, metadataScriptFactory);
        }
        
        /// <summary>
        /// Fills all catalogs in the server object as well as building out the schemas
        /// and all objects in the database connection.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        public static async Task BuildAsync(
            Server server, DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await GetAsync(server, connection, metadataScriptFactory, cancellationToken);

            foreach (var catalog in server.Catalogs)
                await SchemaAdapter.BuildAsync(catalog, connection, metadataScriptFactory, cancellationToken);
        }

        /// <summary>
        /// Fills only specific catalogs in the server object as well as building out the schemas
        /// and all objects in the database connection.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="catalogs">The catalogs to filter.</param>
        public static void BuildSpecific(
            Server server,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            IList<string> catalogs)
        {
            GetSpecific(server, connection, metadataScriptFactory, catalogs);
            foreach (var catalog in server.Catalogs)
                SchemaAdapter.Build(catalog, connection, metadataScriptFactory);
        }
        
        /// <summary>
        /// Fills only specific catalogs in the server object as well as building out the schemas
        /// and all objects in the database connection under the given catalogs.
        /// </summary>
        /// <param name="server">The server to fill catalogs only.</param>
        /// <param name="connection">The open database connection to use.</param>
        /// <param name="metadataScriptFactory">The script factory determined by the database type.</param>
        /// <param name="catalogs">The catalogs to filter.</param>
        /// <param name="cancellationToken">The optional cancellation token.</param>
        public static async Task BuildSpecificAsync(
            Server server,
            DbConnection connection,
            IMetadataScriptFactory metadataScriptFactory,
            IList<string> catalogs,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            await GetSpecificAsync(server, connection, metadataScriptFactory, catalogs, cancellationToken);

            foreach (var catalog in server.Catalogs)
                await SchemaAdapter.BuildAsync(catalog, connection, metadataScriptFactory, cancellationToken);
        }
    }
}
