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
        private readonly IRepository<MS_Province, string> _msProvinceRepo;

        public MsCityAppService(IRepository<MS_City, string> msCityRepo)
        {
            _msCityRepo = msCityRepo;
        }

        public List<GetCityListDto> GetCityListByProvinceCode(string provinceName, string country)
        {
            List<GetCityListDto> cityList = new List<GetCityListDto>();
          
            if(provinceName != null)
            {
                cityList = (from x in _msProvinceRepo.GetAll()
                            where x.provinceName == provinceName
                            join y in _msCityRepo.GetAll() on x.provinceCode equals y.provinceCode
                            select new GetCityListDto
                            {                              
                                cityCode = y.cityCode,
                                cityName = y.cityName,
                                cityAbbreviation = y.cityAbbreviation
                            }).ToList();              
            }
            else
            {
                cityList = (from x in _msCityRepo.GetAll()
                            where x.country == country
                            select new GetCityListDto
                            {
                                cityCode = x.cityCode,
                                cityName = x.cityName,
                                cityAbbreviation = x.cityAbbreviation
                            }).ToList();
            }
            return cityList;
        }
    }
}
