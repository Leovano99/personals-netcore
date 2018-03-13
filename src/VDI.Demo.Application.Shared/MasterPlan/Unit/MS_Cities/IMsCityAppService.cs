using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Cities.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Cities
{
    public interface IMsCityAppService : IApplicationService
    {
        ListResultDto<GetMsCityListDto> GetAllDropdownMsCity(int countyID);
        void CreateMsCity(GetCreateMsCityInputDto input);
    }
}
