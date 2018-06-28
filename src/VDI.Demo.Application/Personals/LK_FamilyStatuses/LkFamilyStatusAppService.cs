using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_FamilyStatuses.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_FamilyStatuses
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkFamilyStatus)]
    public class LkFamilyStatusAppService : DemoAppServiceBase, ILkFamilyStatusAppService
    {
        private readonly IRepository<LK_FamilyStatus, string> _lkFamilyStatusRepo;

        public LkFamilyStatusAppService(IRepository<LK_FamilyStatus, string> lkFamilyStatusRepo)
        {
            _lkFamilyStatusRepo = lkFamilyStatusRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkFamilyStatus_GetLkFamilyStatusDropdown)]
        public ListResultDto<GetLkFamilyStatusDropdownListDto> GetLkFamilyStatusDropdown()
        {
            var result = (from x in _lkFamilyStatusRepo.GetAll()
                          select new GetLkFamilyStatusDropdownListDto
                          {
                              famStatus = x.famStatus,
                              famStatusName = x.famStatusName
                          }).ToList();

            return new ListResultDto<GetLkFamilyStatusDropdownListDto>(result);
        }
    }
}
