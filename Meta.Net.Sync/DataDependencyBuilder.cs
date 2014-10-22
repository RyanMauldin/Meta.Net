using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Net.Objects;

namespace Meta.Net.Sync
{
    public class DataDependencyBuilder
    {
		public HashSet<string> CreatedConstraints { get; set; }
        public HashSet<string> CreatedForeignKeys { get; set; }
        public HashSet<string> CreatedUserTables { get; set; }
        public HashSet<string> DroppedConstraints { get; set; }
        public HashSet<string> DroppedForeignKeys { get; set; }
        public HashSet<string> DroppedUserTables { get; set; }

        public DataDependencyBuilder()
        {
            DroppedUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            CreatedUserTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DroppedForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            CreatedForeignKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DroppedConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            CreatedConstraints = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
        
        public long ObjectCount()
        {
            var count = (long)DroppedUserTables.Count;
            count += CreatedUserTables.Count;
            count += DroppedForeignKeys.Count;
            count += CreatedForeignKeys.Count;

            return count;
        }

        public void PreloadCreatedDependencies(Catalog createdCatalog)
        {
            if (Catalog.ObjectCount(createdCatalog) == 0)
                return;

            CreatedUserTables.UnionWith(
                createdCatalog.Schemas.Select(
                    schema => schema.UserTables)
                .SelectMany(
                    userTables => userTables.Select(
                        userTable => userTable.Namespace)));

            CreatedForeignKeys.UnionWith(createdCatalog.ForeignKeyPool.Keys);
        }

        public void PreloadCreatedDependencies(Schema createdSchema)
        {
            if (Schema.ObjectCount(createdSchema) == 0)
                return;

            CreatedUserTables.UnionWith(
                createdSchema.UserTables.Select(
                    userTable => userTable.Namespace));

            if (createdSchema.Catalog != null)
            {
                CreatedForeignKeys.UnionWith(createdSchema.Catalog.ForeignKeyPool.Keys);
                return;
            }

            CreatedForeignKeys.UnionWith(
                createdSchema.UserTables.Select(
                    userTable => userTable.ForeignKeys)
                .SelectMany(
                    foreignKeys => foreignKeys.Select(
                        foreignKey => foreignKey.Namespace)));
        }

        public void PreloadCreatedDependencies(UserTable createdUserTable)
        {
            CreatedUserTables.Add(createdUserTable.Namespace);

            if (createdUserTable.Catalog != null)
            {
                CreatedForeignKeys.UnionWith(createdUserTable.Catalog.ForeignKeyPool.Keys);
                return;
            }

            if (UserTable.ObjectCount(createdUserTable) == 0)
                return;

            CreatedForeignKeys.UnionWith(
                createdUserTable.ForeignKeys.Select(
                    foreignKey => foreignKey.Namespace));
        }

        public void PreloadDroppedDependencies(Catalog droppedCatalog)
        {
            if (Catalog.ObjectCount(droppedCatalog) == 0)
                return;

            DroppedUserTables.UnionWith(
                droppedCatalog.Schemas.Select(
                    schema => schema.UserTables)
                .SelectMany(
                    userTables => userTables.Select(
                        userTable => userTable.Namespace)));

            DroppedForeignKeys.UnionWith(droppedCatalog.ForeignKeyPool.Keys);
        }

        public void PreloadDroppedDependencies(Schema droppedSchema)
        {
            if (Schema.ObjectCount(droppedSchema) == 0)
                return;

            DroppedUserTables.UnionWith(
                droppedSchema.UserTables.Select(
                    userTable => userTable.Namespace));

            if (droppedSchema.Catalog != null)
            {
                DroppedForeignKeys.UnionWith(droppedSchema.Catalog.ForeignKeyPool.Keys);
                return;
            }

            DroppedForeignKeys.UnionWith(
                droppedSchema.UserTables.Select(
                    userTable => userTable.ForeignKeys)
                .SelectMany(
                    foreignKeys => foreignKeys.Select(
                        foreignKey => foreignKey.Namespace)));
        }

        public void PreloadDroppedDependencies(UserTable droppedUserTable)
        {
            DroppedUserTables.Add(droppedUserTable.Namespace);

            if (droppedUserTable.Catalog != null)
            {
                DroppedForeignKeys.UnionWith(droppedUserTable.Catalog.ForeignKeyPool.Keys);
                return;
            }

            if (UserTable.ObjectCount(droppedUserTable) == 0)
                return;

            DroppedForeignKeys.UnionWith(
                droppedUserTable.ForeignKeys.Select(
                    foreignKey => foreignKey.Namespace));
        } 
    }
}