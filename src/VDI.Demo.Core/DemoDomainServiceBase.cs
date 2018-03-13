using Abp.Domain.Services;

namespace VDI.Demo
{
    public abstract class DemoDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected DemoDomainServiceBase()
        {
            LocalizationSourceName = DemoConsts.LocalizationSourceName;
        }
    }
}
