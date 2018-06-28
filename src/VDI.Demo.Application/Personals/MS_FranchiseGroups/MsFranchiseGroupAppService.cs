using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_FranchiseGroups.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.MS_FranchiseGroups
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterFranchiseGroup)]
    public class MsFranchiseGroupAppService : DemoAppServiceBase, IMsFranchiseGroupAppService
    {
        private readonly IRepository<MS_FranchiseGroup> _msFranchiseGroupRepo;

        public MsFranchiseGroupAppService(
            IRepository<MS_FranchiseGroup> msFranchiseGroupRepo
            )
        {
            _msFranchiseGroupRepo = msFranchiseGroupRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterFranchiseGroup_GetFranchiseGroupDropdown)]
        public ListResultDto<GetFranchiseGroupDropdownListDto> GetFranchiseGroupDropdown()
        {
            var getAllData = (from A in _msFranchiseGroupRepo.GetAll()
                              select new GetFranchiseGroupDropdownListDto
                              {
                                  franchiseGroupName = A.franchiseGroupName
                              }).ToList();

            return new ListResultDto<GetFranchiseGroupDropdownListDto>(getAllData);
        }
    }
}
