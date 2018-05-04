using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_City.Dto;

namespace VDI.Demo.Personals.MS_City
{
    public class MsCityAppService : DemoAppServiceBase, IMsCityAppService
    {
        private readonly IRepository<MS_City> _msCityRepo;

        public MsCityAppService(IRepository<MS_City> msCityRepo)
        {
            _msCityRepo = msCityRepo;
        }

        public List<GetCityListDto> GetCityListByProvinceCode(string provinceCode)
        {
            throw new NotImplementedException();
        }
    }
}
