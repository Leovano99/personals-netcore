using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_KeyPeoples.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_KeyPeoples
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkKeyPeople)]
    public class LkKeyPeopleAppService : DemoAppServiceBase, ILkKeyPeopleAppService
    {
        private readonly IRepository<LK_KeyPeople> _lkKeyPeopleRepo;

        public LkKeyPeopleAppService(IRepository<LK_KeyPeople> lkKeyPeopleRepo)
        {
            _lkKeyPeopleRepo = lkKeyPeopleRepo;
        }

        public ListResultDto<GetAllLkKeyPeopleDropdwonListDto> GetAllLkKeyPeopleDropdwon()
        {
            var result = (from x in _lkKeyPeopleRepo.GetAll()
                          select new GetAllLkKeyPeopleDropdwonListDto
                          {
                              Id = x.Id,
                              keyPeopleDesc = x.keyPeopleDesc
                          }).ToList();

            return new ListResultDto<GetAllLkKeyPeopleDropdwonListDto>(result);
        }
    }
}
