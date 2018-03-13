using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Counties.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Counties
{
    public interface IMsCountyAppService : IApplicationService
    {
        ListResultDto<GetMsCountyListDto> GetAllDropdownMsCounty(int territoryID);
        void CreateMsCounty(GetCreateMsCountyInputDto input);
    }
}
