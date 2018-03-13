using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_UnitStatuses.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitStatuses
{
    public interface IMsUnitStatusAppService : IApplicationService
    {
        ListResultDto<GetAllMsUnitStatusListDto> GetAllMsUnitStatus();
        ListResultDto<GetAllMsUnitStatusListDto> GetMsUnitStatusDropdown();
    }
}
