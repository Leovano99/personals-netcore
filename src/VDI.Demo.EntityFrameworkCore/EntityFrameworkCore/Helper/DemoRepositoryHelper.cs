using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.EntityFrameworkCore.Helper
{
    public static class DemoRepositoryHelper
    {
        public static void BulkInsert<TEntity, TPrimaryKey>(DbContext context, IRepository<TEntity, TPrimaryKey> repository, IList<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkInsert(entities, new BulkConfig { PreserveInsertOrder = true });
                transaction.Commit();
            }

            //var options = new DbContextOptions<NewCommDbContext>();
            //var dbContext = new NewCommDbContext(options);
            //dbContext.BulkInsert(entities);
        }

        public static void BulkInsertOrUpdate<TEntity, TPrimaryKey>(DbContext context, IRepository<TEntity, TPrimaryKey> repository, IList<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            
            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkInsertOrUpdate(entities, new BulkConfig { PreserveInsertOrder = true });
                transaction.Commit();
            }

            //var options = new DbContextOptions<NewCommDbContext>();
            //var dbContext = new NewCommDbContext(options);
            //dbContext.BulkInsertOrUpdate(entities);
        }
        public static void BulkUpdate<TEntity, TPrimaryKey>(DbContext context, IRepository<TEntity, TPrimaryKey> repository, IList<TEntity> entities)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkUpdate(entities, new BulkConfig { PreserveInsertOrder = true });
                transaction.Commit();
            }
        }


        public static void BulkDelete<TEntity, TPrimaryKey>(DbContext context, IRepository<TEntity, TPrimaryKey> repository, IList<TEntity> entities, DbContextOptions dbContextOptions)
            where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                context.BulkDelete(entities, new BulkConfig { PreserveInsertOrder = true });
                transaction.Commit();
            }
        }
    }
}
