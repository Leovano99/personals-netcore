using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Occupations.Dto;

namespace VDI.Demo.Personals.MS_Occupations
{
    public interface IMsOccupationAppService : IApplicationService
    {
        ListResultDto<GetMsOccupationDropdownListDto> GetMsOccupationDropdown();
    }
}
