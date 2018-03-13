using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_Countries.Dto;

namespace VDI.Demo.Personals.LK_Countries
{
    public interface ILkCountryAppService : IApplicationService
    {
        ListResultDto<GetAllCountryListDto> GetAllLkCountryList();
    }
}
