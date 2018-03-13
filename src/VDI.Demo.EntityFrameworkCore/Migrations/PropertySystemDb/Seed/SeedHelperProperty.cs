using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using VDI.Demo.EntityFrameworkCore;

namespace VDI.Demo.Migrations.PropertySystemDb.Seed
{
    public static class SeedHelperProperty
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<PropertySystemDbContext>(iocResolver, SeedPropertySystemDb);
        }

        public static void SeedPropertySystemDb(PropertySystemDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            //Host seed
            new InitialDbBuilder(context).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
