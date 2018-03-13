using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.MS_Banks.Dto;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.MS_Banks
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_MasterBank)]
    public class MsBankPersonalsAppService : DemoAppServiceBase, IMsBankPersonalsAppService
    {
        private readonly IRepository<MS_BankPersonal, string> _msBankRepo;

        public MsBankPersonalsAppService(
            IRepository<MS_BankPersonal, string> msBankRepo
        )
        {
            _msBankRepo = msBankRepo;
        }

        public ListResultDto<GetAllBankPersonalsListDto> GetAllMsBankList()
        {
            var getAllData = (from A in _msBankRepo.GetAll()
                              select new GetAllBankPersonalsListDto
                              {
                                  bankCode = A.bankCode,
                                  bankName = A.bankName
                              }).ToList();

            return new ListResultDto<GetAllBankPersonalsListDto>(getAllData);
        }
    }
}
