using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Territories.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Territories
{
    public interface IMsTerritoryAppService : IApplicationService
    {
        ListResultDto<GetMsTerritoryListDto> GetAllDropdownMsTerritory();
        void CreateMsTerritory(GetCreateMsTerritoryInputDto input);
    }
}
