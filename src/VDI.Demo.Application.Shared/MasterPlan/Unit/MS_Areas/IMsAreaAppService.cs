using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Areas.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Areas
{
    public interface IMsAreaAppService : IApplicationService
    {
        ListResultDto<GetMsAreaListDto> GetAllMsArea();
        void CreateMsArea(CreateMsAreaInput input);
        void UpdateMsArea(UpdateMsAreaInput input);
        void DeleteMsArea(int Id);
    }
}
