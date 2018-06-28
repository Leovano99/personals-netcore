using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_Bloods.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_Bloods
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkBlood)]
    public class LkBloodAppService : DemoAppServiceBase, ILkBloodAppService
    {
        private readonly IRepository<LK_Blood, string> _lkBloodRepo;

        public LkBloodAppService(
            IRepository<LK_Blood, string> lkBloodRepo
        )
        {
            _lkBloodRepo = lkBloodRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkBlood_GetAllLkBloodList)]
        public ListResultDto<GetAllBloodListDto> GetAllLkBloodList()
        {
            var getAllData = (from A in _lkBloodRepo.GetAll()
                              select new GetAllBloodListDto
                              {
                                  bloodCode = A.bloodCode,
                                  bloodName = A.bloodName
                              }).ToList();

            return new ListResultDto<GetAllBloodListDto>(getAllData);
        }
    }
}
