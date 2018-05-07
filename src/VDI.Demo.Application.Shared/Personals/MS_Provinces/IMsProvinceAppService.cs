using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Provinces.Dto;

namespace VDI.Demo.Personals.MS_Provinces
{
    public interface IMsProvinceAppService : IApplicationService
    {
        List<GetMsProvinceListDto> GetMsProvinceDropdown(string country);
    }
}
