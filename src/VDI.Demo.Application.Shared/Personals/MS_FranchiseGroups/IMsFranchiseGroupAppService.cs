using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_FranchiseGroups.Dto;

namespace VDI.Demo.Personals.MS_FranchiseGroups
{
    public interface IMsFranchiseGroupAppService : IApplicationService
    {
        ListResultDto<GetFranchiseGroupDropdownListDto> GetFranchiseGroupDropdown();
    }
}
