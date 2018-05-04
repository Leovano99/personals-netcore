using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_City.Dto;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Personals.MS_City
{
    public class MsCityAppService : DemoAppServiceBase, IMsCityAppService
    {
        //private readonly IRepository<MS_City> _msCityRepo;

        //public MsCityAppService(IRepository<MS_City, string> msCityRepo)
        //{
        //    msCityRepo = _msCityRepo;
        //}


        //public List<GetCityListDto> GetCityListByProvinceCode(string provinceCode)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
