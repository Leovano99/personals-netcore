using Abp.Dependency;
using Abp.EntityFrameworkCore.Configuration;
using Abp.EntityFrameworkCore.Uow;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Configuration;
using VDI.Demo.Migrations.PersonalsNewDb.Seed;
using Abp.Configuration.Startup;

namespace VDI.Demo.EntityFrameworkCore
{
    [DependsOn(
       typeof(AbpZeroCoreEntityFrameworkCoreModule),
       typeof(DemoCoreModule),
       typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule)
       )]

    public class PersonalsNewDbEntityFrameworkCoreModule : AbpModule
    {
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            Configuration.ReplaceService<IEfCoreTransactionStrategy, DbContextEfCoreTransactionStrategy>(DependencyLifeStyle.Transient);
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<PersonalsNewDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        PersonalsNewDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        PersonalsNewDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(PersonalsNewDbEntityFrameworkCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            var configurationAccessor = IocManager.Resolve<IAppConfigurationAccessor>();
            if (!SkipDbSeed && DatabaseCheckHelper.Exist(configurationAccessor.Configuration["ConnectionStrings:PersonalsNewDbContext"]))
            {
                SeedHelperPersonal.SeedHostDb(IocManager);
            }
        }
    }
}
