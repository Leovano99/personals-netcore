using System.Threading.Tasks;
using Abp.Application.Services;
using VDI.Demo.Configuration.Tenants.Dto;

namespace VDI.Demo.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
