using System.Threading.Tasks;
using Abp.Application.Services;
using VDI.Demo.Configuration.Dto;

namespace VDI.Demo.Configuration
{
    public interface IUiCustomizationSettingsAppService : IApplicationService
    {
        Task<UiCustomizationSettingsEditDto> GetUiManagementSettings();

        Task UpdateUiManagementSettings(UiCustomizationSettingsEditDto settings);

        Task UpdateDefaultUiManagementSettings(UiCustomizationSettingsEditDto settings);

        Task UseSystemDefaultSettings();
    }
}
