using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_JobTItles.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.MS_JobTItles
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterJobTitle)]
    public class MsJobTitleAppService : DemoAppServiceBase, IMsJobTitleAppService
    {
        private readonly IRepository<MS_JobTitle, string> _msJobTitleRepo;

        public MsJobTitleAppService(IRepository<MS_JobTitle, string> msJobTitleRepo)
        {
            _msJobTitleRepo = msJobTitleRepo;
        }

        public ListResultDto<GetAllMsJobTitleDropdownList> GetAllMsJobTitleDropdown()
        {
            var result = (from x in _msJobTitleRepo.GetAll()
                          select new GetAllMsJobTitleDropdownList
                          {
                              jobTitleID = x.jobTitleID,
                              jobTitleName = x.jobTitleName
                          }).ToList();

            return new ListResultDto<GetAllMsJobTitleDropdownList>(result);
        }
    }
}
