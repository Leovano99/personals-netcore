using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_Religions.Dto;

namespace VDI.Demo.Personals.LK_Religions
{
    public interface ILkReligionAppService : IApplicationService
    {
        ListResultDto<GetAllReligionListDto> GetAllLkReligionList();
    }
}
