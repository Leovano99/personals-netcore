using System.Threading.Tasks;
using Abp.Application.Services;
using VDI.Demo.Configuration.Host.Dto;

namespace VDI.Demo.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
