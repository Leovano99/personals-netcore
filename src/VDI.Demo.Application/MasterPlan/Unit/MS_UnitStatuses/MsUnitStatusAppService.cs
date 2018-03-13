using System;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_UnitStatuses.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitStatuses
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterUnitStatus)]
    public class MsUnitStatusAppService : DemoAppServiceBase, IMsUnitStatusAppService
    {
        private readonly IRepository<LK_UnitStatus> _lkUnitStatusRepo;

        public MsUnitStatusAppService(IRepository<LK_UnitStatus> lkUnitStatusRepo)
        {
            _lkUnitStatusRepo = lkUnitStatusRepo;
        }

        public ListResultDto<GetAllMsUnitStatusListDto> GetAllMsUnitStatus()
        {
            var listResult = (from x in _lkUnitStatusRepo.GetAll()
                              select new GetAllMsUnitStatusListDto
                              {
                                  Id = x.Id,
                                  unitStatusCode = x.unitStatusCode,
                                  unitStatusName = x.unitStatusName
                              }).ToList();

            return new ListResultDto<GetAllMsUnitStatusListDto>(listResult);
        }

        public ListResultDto<GetAllMsUnitStatusListDto> GetMsUnitStatusDropdown()
        {
            var listResult = (from x in _lkUnitStatusRepo.GetAll()
                              select new GetAllMsUnitStatusListDto
                              {
                                  Id = x.Id,
                                  unitStatusCode = x.unitStatusCode,
                                  unitStatusName = x.unitStatusName
                              }).ToList();

            return new ListResultDto<GetAllMsUnitStatusListDto>(listResult);
        }
    }
}
