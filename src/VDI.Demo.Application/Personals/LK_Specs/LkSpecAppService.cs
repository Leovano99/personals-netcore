using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_Specs.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_Specs
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkSpec)]
    public class LkSpecAppService : DemoAppServiceBase, ILkSpecAppService
    {
        private readonly IRepository<LK_Spec, string> _lkSpecRepo;

        public LkSpecAppService(
            IRepository<LK_Spec, string> lkSpecRepo
        )
        {
            _lkSpecRepo = lkSpecRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkSpec_GetAllLkSpecList)]
        public ListResultDto<GetAllSpecListDto> GetAllLkSpecList()
        {
            var getAllData = (from A in _lkSpecRepo.GetAll()
                              select new GetAllSpecListDto
                              {
                                  specCode = A.specCode,
                                  specName = A.specName
                              }).ToList();

            return new ListResultDto<GetAllSpecListDto>(getAllData);
        }
    }
}
