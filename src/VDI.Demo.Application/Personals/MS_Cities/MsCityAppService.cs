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
using Abp.UI;

namespace VDI.Demo.Personals.MS_Cities
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterCities)]
    public class MsCityAppService : DemoAppServiceBase, IMsCityAppService
    {
        private readonly IRepository<MS_City, string> _msCityRepo;
        private readonly IRepository<MS_Province, string> _msProvinceRepo;

        public MsCityAppService(IRepository<MS_City, string> msCityRepo,
            IRepository<MS_Province, string> msProvinceRepo)
        {
            _msCityRepo = msCityRepo;
            _msProvinceRepo = msProvinceRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterCities_GetCityListByProvinceCode)]
        public List<GetCityListDto> GetCityListByProvinceCode(string countyCode)
        {
            //List<GetCityListDto> cityList = new List<GetCityListDto>();

            //if(provinceCode != null)
            //{
            //    cityList = (from x in _msCityRepo.GetAll()
            //                where x.provinceCode == provinceCode
            //                select new GetCityListDto
            //                {                              
            //                    cityCode = x.cityCode,
            //                    cityName = x.cityName,
            //                    cityAbbreviation = x.cityAbbreviation
            //                }).ToList();              
            //}
            //else
            //{
            //    cityList = (from x in _msCityRepo.GetAll()
            //                where x.country == country
            //                select new GetCityListDto
            //                {
            //                    cityCode = x.cityCode,
            //                    cityName = x.cityName,
            //                    cityAbbreviation = x.cityAbbreviation
            //                }).ToList();
            //}

            //return cityList;

            if(countyCode == null || countyCode == string.Empty)
            {
                throw new UserFriendlyException("Parameter is empty");
            }

            var getCity = (from a in _msCityRepo.GetAll()
                           where a.countyCode == countyCode
                           select new GetCityListDto
                           {
                               cityAbbreviation = a.cityAbbreviation,
                               cityCode = a.cityCode,
                               cityName = a.cityName
                           }).Distinct().ToList();

            return getCity;
        }
    }
}
