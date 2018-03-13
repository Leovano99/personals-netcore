using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_BankAccounts.Dto;

namespace VDI.Demo.Personals.TR_BankAccounts
{
    public interface ITrBankAccountAppService : IApplicationService
    {
        void UpdateBankAccount(GetUpdateBankAccountInputDto input);
        void DeleteBankAccount(DeleteBankAccountInputDto input);
    }
}
