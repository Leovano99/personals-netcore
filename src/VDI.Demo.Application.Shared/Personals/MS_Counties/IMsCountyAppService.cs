using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Counties.Dto;

namespace VDI.Demo.Personals.MS_Counties
{
    public interface IMsCountyAppService : IApplicationService
    {
        List<GetListMsCountyResultDto> GetListMsCounty(string country);
    }
}
