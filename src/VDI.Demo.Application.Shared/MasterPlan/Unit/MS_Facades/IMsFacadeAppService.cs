using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Facades.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Facades
{
    public interface IMsFacadeAppService : IApplicationService
    {
        ListResultDto<GetMsFacadeListDto> GetAllMsFacade();
        void CreateMsFacade(CreateMsFacadeInput input);
        void UpdateMsFacade(UpdateMsFacadeInput input);
        void DeleteMsFacade(int Id);
    }
}
