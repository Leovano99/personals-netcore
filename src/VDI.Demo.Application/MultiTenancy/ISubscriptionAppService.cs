using System.Threading.Tasks;
using Abp.Application.Services;

namespace VDI.Demo.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task UpgradeTenantToEquivalentEdition(int upgradeEditionId);
    }
}
