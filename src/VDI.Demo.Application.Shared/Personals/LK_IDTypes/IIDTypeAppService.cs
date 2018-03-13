using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_IDTypes.Dto;

namespace VDI.Demo.Personals.LK_IDTypes
{
    public interface IIDTypeAppService : IApplicationService
    {
        ListResultDto<GetAllIDTypeListDto> GetAllIDTypeList();
    }
}
