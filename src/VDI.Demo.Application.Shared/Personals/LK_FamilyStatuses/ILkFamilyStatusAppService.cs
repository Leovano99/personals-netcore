using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_FamilyStatuses.Dto;

namespace VDI.Demo.Personals.LK_FamilyStatuses
{
    public interface ILkFamilyStatusAppService : IApplicationService
    {
        ListResultDto<GetLkFamilyStatusDropdownListDto> GetLkFamilyStatusDropdown();
    }
}
