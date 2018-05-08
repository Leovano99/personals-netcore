using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using VDI.Demo.Personals.MS_Cities.Dto;

namespace VDI.Demo.Personals.MS_Cities
{
    public interface IMsCityAppService : IApplicationService
    {
        List<GetCityListDto> GetCityListByProvinceCode(string provinceName, string country);
    }
}
