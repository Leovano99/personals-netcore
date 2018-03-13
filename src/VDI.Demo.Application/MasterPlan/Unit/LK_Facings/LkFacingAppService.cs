using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.LK_Facings.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.LK_Facings
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFacing)]
    public class LkFacingAppService : DemoAppServiceBase, ILkFacingAppService
    {
        private readonly IRepository<LK_Facing> _lkFacingRepo;

        public LkFacingAppService(IRepository<LK_Facing> lkFacingRepo)
        {
            _lkFacingRepo = lkFacingRepo;
        }

        public ListResultDto<GetAllMsFacingList> GetAllMsFacing()
        {
            var result = (from facing in _lkFacingRepo.GetAll()
                          select new GetAllMsFacingList
                          {
                              Id = facing.Id,
                              facingCode = facing.facingCode,
                              facingName = facing.facingName
                          }).ToList();

            return new ListResultDto<GetAllMsFacingList>(result);
        }
    }
}
