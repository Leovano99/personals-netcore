using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_PostCodes.Dto;
using VDI.Demo.Personals.MS_Provinces;
using VDI.Demo.Personals.MS_Provinces.Dto;
using VDI.Demo.PersonalsDB;
using System.Linq;

namespace VDI.Demo.Personals.MS_PostCodes
{
    public class MsPostCodeAppService : DemoAppServiceBase, IMsPostCodesAppService
    {
        private readonly IRepository<MS_PostCode, string> _msPostCodeRepo;


        public MsPostCodeAppService(IRepository<MS_PostCode, string> msPostCodeRepo)
        {
            _msPostCodeRepo = msPostCodeRepo;
        }

        public List<GetMsPostCodeListDto> GetPostCodeByCity(string cityCode)
        {
            var result = (from x in _msPostCodeRepo.GetAll()
                          where x.cityCode == cityCode
                          select new GetMsPostCodeListDto
                          {
                              postCode = x.postCode
                          }).ToList();
            return result;
        }
    }
}
