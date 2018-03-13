using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_Nationals.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.MS_Nationals
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterNational)]
    public class MsNationAppService : DemoAppServiceBase, IMsNationAppService
    {
        private readonly IRepository<MS_Nation, string> _msNationepo;

        public MsNationAppService(IRepository<MS_Nation, string> msNationepo)
        {
            _msNationepo = msNationepo;
        }

        public ListResultDto<GetMSNationDropdownListDto> GetMSNationDropdown()
        {
            var result = (from x in _msNationepo.GetAll()
                          select new GetMSNationDropdownListDto
                          {
                              entityCode = x.entityCode,
                              nationID = x.nationID,
                              nationality = x.nationality,
                              ppatkNationCode = x.ppatkNationCode
                          }).ToList();
            return new ListResultDto<GetMSNationDropdownListDto>(result);
        }
    }
}
