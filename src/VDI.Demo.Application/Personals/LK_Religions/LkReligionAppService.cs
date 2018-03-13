using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_Religions.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_Religions
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkReligion)]
    public class LkReligionAppService : DemoAppServiceBase, ILkReligionAppService
    {
        private readonly IRepository<LK_Religion, string> _lkReligionRepo;

        public LkReligionAppService(
            IRepository<LK_Religion, string> lkReligionRepo
        )
        {
            _lkReligionRepo = lkReligionRepo;
        }

        public ListResultDto<GetAllReligionListDto> GetAllLkReligionList()
        {
            var getAllData = (from A in _lkReligionRepo.GetAll()
                              select new GetAllReligionListDto
                              {
                                  relCode = A.relCode,
                                  relName = A.relName
                              }).ToList();

            return new ListResultDto<GetAllReligionListDto>(getAllData);
        }
    }
}
