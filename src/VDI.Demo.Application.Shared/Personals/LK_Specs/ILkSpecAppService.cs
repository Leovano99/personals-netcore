using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_Specs.Dto;

namespace VDI.Demo.Personals.LK_Specs
{
    public interface ILkSpecAppService : IApplicationService
    {
        ListResultDto<GetAllSpecListDto> GetAllLkSpecList();
    }
}
