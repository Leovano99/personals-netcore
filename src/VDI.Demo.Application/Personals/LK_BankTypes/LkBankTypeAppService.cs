using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.LK_BankTypes.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.LK_BankTypes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_LkBankType)]
    public class LkBankTypeAppService : DemoAppServiceBase, ILkBankTypeAppService
    {
        private readonly IRepository<LK_BankType, string> _lkBankTypeRepo;

        public LkBankTypeAppService(
            IRepository<LK_BankType, string> lkBankTypeRepo
        )
        {
            _lkBankTypeRepo = lkBankTypeRepo;
        }

        public ListResultDto<GetAllBankTypeListDto> GetAllLkBankTypeList()
        {
            var getAllData = (from A in _lkBankTypeRepo.GetAll()
                              select new GetAllBankTypeListDto
                              {
                                  bankType = A.bankType,
                                  bankTypeName = A.bankTypeName
                              }).ToList();

            return new ListResultDto<GetAllBankTypeListDto>(getAllData);
        }
    }
}
