using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Configuration;
using VDI.Demo.Migrations.PropertySystemDb.Seed;
using VDI.Demo.Migrations.Seed;
using Abp.Configuration.Startup;
using Abp.EntityFrameworkCore.Uow;
using Abp.Dependency;

namespace VDI.Demo.EntityFrameworkCore
{
    [DependsOn(
       typeof(AbpZeroCoreEntityFrameworkCoreModule),
       typeof(DemoCoreModule),
       typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule)
       )]

    public class AccountingDbEntityFrameworkCoreModule : AbpModule
    {
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {

            Configuration.ReplaceService<IEfCoreTransactionStrategy, DbContextEfCoreTransactionStrategy>(DependencyLifeStyle.Transient);
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<AccountingDbContext>(options =>
                {

                    AccountingDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    //if (options.ExistingConnection != null)
                    //{
                    //    NewCommDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    //}
                    //else
                    //{

                    //}
                });


            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(DemoEntityFrameworkCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            //var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();
            //if (!SkipDbSeed && DatabaseCheckHelper.Exist(configurationAccessor.Configuration["ConnectionStrings:AccountingDbContext"]))
            //{
            //    SeedHelperProperty.SeedHostDb(IocManager);
            //}
        }
    }
}
