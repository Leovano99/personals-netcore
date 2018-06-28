using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_Occupations.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.MS_Occupations
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterOccupation)]
    public class MsOccupationAppService : DemoAppServiceBase, IMsOccupationAppService
    {
        private readonly IRepository<MS_Occupation, string> _MsOccupationRepo;

        public MsOccupationAppService(IRepository<MS_Occupation, string> MsOccupationRepo)
        {
            _MsOccupationRepo = MsOccupationRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterOccupation_GetMsOccupationDropdown)]
        public ListResultDto<GetMsOccupationDropdownListDto> GetMsOccupationDropdown()
        {
            var result = (from x in _MsOccupationRepo.GetAll()
                          select new GetMsOccupationDropdownListDto
                          {
                              occID = x.occID,
                              occDesc = x.occDesc
                          }).ToList();

            return new ListResultDto<GetMsOccupationDropdownListDto>(result);
        }
    }
}
