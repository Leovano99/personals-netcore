using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace VDI.Demo.Helper
{
    public static class DemoRepositoryExtension
    {
        public static void BulkInsert<TEntity, TPrimaryKey>(this DbContext context, IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var type = Type.GetType("VDI.Demo.EntityFrameworkCore.Helper.DemoRepositoryHelper, VDI.Demo.EntityFrameworkCore");

            var bulkInsertMethod = type.GetMethod("BulkInsert", BindingFlags.Static | BindingFlags.Public);

            var genericMethod = bulkInsertMethod.MakeGenericMethod(typeof(TEntity), typeof(TPrimaryKey));

            genericMethod.Invoke(null, new object[] { context, repository, entities });
        }

        public static void BulkInsertOrUpdate<TEntity, TPrimaryKey>(this DbContext context, IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var type = Type.GetType("VDI.Demo.EntityFrameworkCore.Helper.DemoRepositoryHelper, VDI.Demo.EntityFrameworkCore");

            var bulkInsertOrUpdateMethod = type.GetMethod("BulkInsertOrUpdate", BindingFlags.Static | BindingFlags.Public);

            var genericMethod = bulkInsertOrUpdateMethod.MakeGenericMethod(typeof(TEntity), typeof(TPrimaryKey));

            genericMethod.Invoke(null, new object[] { context, repository, entities });
        }
        public static void BulkUpdate<TEntity, TPrimaryKey>(this DbContext context, IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var type = Type.GetType("VDI.Demo.EntityFrameworkCore.Helper.DemoRepositoryHelper, VDI.Demo.EntityFrameworkCore");

            var bulkUpdateMethod = type.GetMethod("BulkUpdate", BindingFlags.Static | BindingFlags.Public);

            var genericMethod = bulkUpdateMethod.MakeGenericMethod(typeof(TEntity), typeof(TPrimaryKey));

            genericMethod.Invoke(null, new object[] { context, repository, entities });
        }

        public static void BulkDelete<TEntity, TPrimaryKey>(this DbContext context, IRepository<TEntity, TPrimaryKey> repository,
            IEnumerable<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var type = Type.GetType("VDI.Demo.EntityFrameworkCore.Helper.DemoRepositoryHelper, VDI.Demo.EntityFrameworkCore");

            var bulkDeleteMethod = type.GetMethod("BulkDelete", BindingFlags.Static | BindingFlags.Public);

            var genericMethod = bulkDeleteMethod.MakeGenericMethod(typeof(TEntity), typeof(TPrimaryKey));

            genericMethod.Invoke(null, new object[] { context, repository, entities });
        }
    }
}
