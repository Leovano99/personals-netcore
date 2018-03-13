using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Zonings.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Zonings
{
    public interface IMsZoningAppService : IApplicationService
    {
        ListResultDto<GetAllMsZoningListDto> GetAllMsZoning();
        void CreateMsZoning(CreateMsZoningInputDto input);
        void UpdateMsZoning(UpdateMsZoningInputDto input);
        void DeleteMsZoning(int Id);
    }
}
