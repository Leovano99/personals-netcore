using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.MultiTenancy;
using Abp.Zero.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.EntityFrameworkCore
{
    public class NewCommDbMigrator : AbpZeroDbMigrator<NewCommDbContext>
    {
        public NewCommDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IDbContextResolver dbContextResolver) :
            base(
                unitOfWorkManager,
                connectionStringResolver,
                dbContextResolver)
        {

        }
    }
}
