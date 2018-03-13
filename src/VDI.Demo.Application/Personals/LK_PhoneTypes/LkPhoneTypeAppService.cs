using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.LK_PhoneTypes.Dto;
using VDI.Demo.PersonalsDB;
using System.Linq;

namespace VDI.Demo.Personals.LK_PhoneTypes
{
    public class LkPhoneTypeAppService : DemoAppServiceBase, ILkPhoneTypeAppService
    {
        private readonly IRepository<LK_PhoneType, string> _lkPhoneTypeRepo;

        public LkPhoneTypeAppService(IRepository<LK_PhoneType, string> lkPhoneTypeRepo)
        {
            _lkPhoneTypeRepo = lkPhoneTypeRepo;
        }

        public List<GetLkPhoneTypeListDto> GetLkPhoneTypeDropdown()
        {
            var result = (from x in _lkPhoneTypeRepo.GetAll()
                          select new GetLkPhoneTypeListDto
                          {
                              phoneType = x.phoneType,
                              phoneTypeName = x.phoneTypeName
                          }).ToList();

            return result;
        }
    }
}
