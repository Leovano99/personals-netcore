using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Nationals.Dto;

namespace VDI.Demo.Personals.MS_Nationals
{
    public interface IMsNationAppService : IApplicationService
    {
        ListResultDto<GetMSNationDropdownListDto> GetMSNationDropdown();
    }
}
