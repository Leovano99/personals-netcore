using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_MarStatuses.Dto;

namespace VDI.Demo.Personals.LK_MarStatuses
{
    public interface ILkMarStatusAppService : IApplicationService
    {
        ListResultDto<GetAllMarStatusListDto> GetAllLkMarStatusList();
    }
}
