using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_PostCodes.Dto;
using VDI.Demo.Personals.MS_Provinces;
using VDI.Demo.Personals.MS_Provinces.Dto;
using VDI.Demo.PersonalsDB;
using System.Linq;
using Abp.Authorization;
using VDI.Demo.Authorization;

namespace VDI.Demo.Personals.MS_PostCodes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterPostCode)]
    public class MsPostCodeAppService : DemoAppServiceBase, IMsPostCodesAppService
    {
        private readonly IRepository<MS_PostCode, string> _msPostCodeRepo;


        public MsPostCodeAppService(IRepository<MS_PostCode, string> msPostCodeRepo)
        {
            _msPostCodeRepo = msPostCodeRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterPostCode_GetPostCodeByCity)]
        public List<GetMsPostCodeListDto> GetPostCodeByCity(string cityCode)
        {
            var result = (from x in _msPostCodeRepo.GetAll()
                          where x.cityCode == cityCode
                          select new GetMsPostCodeListDto
                          {
                              postCode = x.postCode
                          }).ToList();
            return result;
        }
    }
}
