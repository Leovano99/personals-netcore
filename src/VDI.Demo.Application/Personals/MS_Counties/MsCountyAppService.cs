using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.MS_Counties.Dto;
using VDI.Demo.PersonalsDB;
using System.Linq;
using Abp.UI;

namespace VDI.Demo.Personals.MS_Counties
{
    public class MsCountyAppService : DemoAppServiceBase, IMsCountyAppService
    {
        private readonly IRepository<MS_County, string> _msCountyRepo;

        public MsCountyAppService(
            IRepository<MS_County, string> msCountyRepo
            )
        {
            _msCountyRepo = msCountyRepo;
        }

        public List<GetListMsCountyResultDto> GetListMsCounty(string country)
        {
            if(country == null || country == string.Empty)
            {
                throw new UserFriendlyException("Parameter is empty");
            }

            var getCounty = (from a in _msCountyRepo.GetAll()
                             where a.country == country
                             select new GetListMsCountyResultDto
                             {
                                 countyCode = a.countyCode,
                                 countyName = a.countyDesc
                             }).Distinct().ToList();

            return getCounty;
        }
    }
}
