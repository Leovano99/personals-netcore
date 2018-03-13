using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_Bloods.Dto;

namespace VDI.Demo.Personals.LK_Bloods
{
    public interface ILkBloodAppService : IApplicationService
    {
        ListResultDto<GetAllBloodListDto> GetAllLkBloodList();
    }
}
