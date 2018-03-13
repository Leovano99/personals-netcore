using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_MarStatuses.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_MarStatuses
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkMarStatus)]
    public class LkMarStatusAppService : DemoAppServiceBase, ILkMarStatusAppService
    {
        private readonly IRepository<LK_MarStatus, string> _lkMarStatusRepo;

        public LkMarStatusAppService(
            IRepository<LK_MarStatus, string> lkMarStatusRepo
        )
        {
            _lkMarStatusRepo = lkMarStatusRepo;
        }

        public ListResultDto<GetAllMarStatusListDto> GetAllLkMarStatusList()
        {
            var getAllData = (from A in _lkMarStatusRepo.GetAll()
                              select new GetAllMarStatusListDto
                              {
                                  marStatus = A.marStatus,
                                  marStatusName = A.marStatusName
                              }).ToList();

            return new ListResultDto<GetAllMarStatusListDto>(getAllData);
        }
    }
}
