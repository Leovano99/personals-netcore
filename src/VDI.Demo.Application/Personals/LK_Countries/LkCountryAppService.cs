using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_Countries.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_Countries
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkCountry)]
    public class LkCountryAppService : DemoAppServiceBase, ILkCountryAppService
    {
        private readonly IRepository<LK_Country, string> _lkCountryRepo;

        public LkCountryAppService(
            IRepository<LK_Country, string> lkCountryRepo
        )
        {
            _lkCountryRepo = lkCountryRepo;
        }

        public ListResultDto<GetAllCountryListDto> GetAllLkCountryList()
        {
            var getAllData = (from A in _lkCountryRepo.GetAll()
                              select new GetAllCountryListDto
                              {
                                  country = A.country,
                                  urut = A.urut,
                                  ppatkCountryCode = A.ppatkCountryCode
                              }).ToList();

            return new ListResultDto<GetAllCountryListDto>(getAllData);
        }
    }


}
