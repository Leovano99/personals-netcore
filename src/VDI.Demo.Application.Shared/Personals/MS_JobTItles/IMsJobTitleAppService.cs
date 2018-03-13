using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_JobTItles.Dto;

namespace VDI.Demo.Personals.MS_JobTItles
{
    public interface IMsJobTitleAppService : IApplicationService
    {
        ListResultDto<GetAllMsJobTitleDropdownList> GetAllMsJobTitleDropdown();
    }
}
