using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.LK_Facings.Dto;

namespace VDI.Demo.MasterPlan.Unit.LK_Facings
{
    public interface ILkFacingAppService : IApplicationService
    {
        ListResultDto<GetAllMsFacingList> GetAllMsFacing();
    }
}
