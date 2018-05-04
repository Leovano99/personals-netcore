using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PersonalsDB;
using Abp.Authorization;
using VDI.Demo.Authorization;
using VDI.Demo.EntityFrameworkCore;
using System.Linq;
using VDI.Demo.Personals.MS_Cities.Dto;

namespace VDI.Demo.Personals.MS_Cities
{
    public class MsCityAppService : DemoAppServiceBase, IMsCityAppService
    {
        private readonly IRepository<MS_City, string> _msCityRepo;

        public MsCityAppService(IRepository<MS_City, string> msCityRepo)
        {
            _msCityRepo = msCityRepo;
        }

        public List<GetCityListDto> GetCityListByProvinceCode(string provinceCode)
        {
            List<GetCityListDto> cityList = new List<GetCityListDto>();

            var getCityList = (from x in _msCityRepo.GetAll()
                               where x.provinceCode == provinceCode
                               select new GetCityListDto
                               {
                                   cityCode = x.cityAbbreviation,
                                   cityName = x.cityName,
                                   cityAbbreviation = x.cityAbbreviation
                               }).ToList();

            cityList.AddRange(getCityList);
            return cityList;
        }
    }
}
