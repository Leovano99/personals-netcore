using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Provinces.Dto;
using VDI.Demo.PersonalsDB;
using Abp.Authorization;
using VDI.Demo.Authorization;
using VDI.Demo.EntityFrameworkCore;
using System.Linq;

namespace VDI.Demo.Personals.MS_Provinces
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterProvinces)]
    public class MsProvinceAppService : DemoAppServiceBase, IMsProvinceAppService
    {
        private readonly IRepository<MS_Province, string> _msProvinceRepo;
   

        public MsProvinceAppService(IRepository<MS_Province, string> msProvinceRepo)
        {
            _msProvinceRepo = msProvinceRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterProvinces_GetMsProvinceDropdown)]
        public List<GetMsProvinceListDto> GetMsProvinceDropdown(string country)
        {
            var result = (from x in _msProvinceRepo.GetAll()
                          where x.country == country
                          select new GetMsProvinceListDto
                          {
                              provinceCode = x.provinceCode,
                              provinceName = x.provinceName
                          }).ToList();
            return result;
        }
    }
}
