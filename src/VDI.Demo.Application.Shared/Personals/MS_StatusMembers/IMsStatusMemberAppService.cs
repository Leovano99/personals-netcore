using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_StatusMembers.Dto;

namespace VDI.Demo.Personals.MS_StatusMembers
{
    public interface IMsStatusMemberAppService : IApplicationService
    {
        ListResultDto<GetAllMsStatusMemberDropdownList> GetAllMsStatusMemberDropdown(string scmCode);
    }
}
