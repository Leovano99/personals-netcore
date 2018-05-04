using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using VDI.Demo.Personals.MS_City.Dto;

namespace VDI.Demo.Personals.MS_City
{
    public interface IMsCityAppService : IApplicationService
    {
        List<GetCityListDto> GetCityListByProvinceCode(string provinceCode);
    }
}
