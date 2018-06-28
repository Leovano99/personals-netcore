using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_AddrTypes.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_AddrTypes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkAddrType)]
    public class LkAddrTypeAppService : DemoAppServiceBase, ILkAddrTypeAppService
    {
        private readonly IRepository<LK_AddrType, string> _lkAddrTypeRepo;

        public LkAddrTypeAppService(IRepository<LK_AddrType, string> lkAddrTypeRepo)
        {
            _lkAddrTypeRepo = lkAddrTypeRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkAddrType_GetLkAddrTypeDropdown)]
        public ListResultDto<GetLkAddrTypeDropdownListDto> GetLkAddrTypeDropdown()
        {
            var result = (from x in _lkAddrTypeRepo.GetAll()
                          select new GetLkAddrTypeDropdownListDto
                          {
                              addrType = x.addrType,
                              addrTypeName = x.addrTypeName
                          }).ToList();

            return new ListResultDto<GetLkAddrTypeDropdownListDto>(result);
        }
    }
}
