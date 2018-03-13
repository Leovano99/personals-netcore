using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_BankAccounts.Dto;
using VDI.Demo.PersonalsDB;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_BankAccounts
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrBankAccount)]
    public class TrBankAccountAppService : DemoAppServiceBase, ITrBankAccountAppService
    {
        #region constructor
        private readonly IRepository<TR_BankAccount, string> _bankAccountRepo;

        public TrBankAccountAppService(
            IRepository<TR_BankAccount, string> bankAccountRepo
        )
        {
            _bankAccountRepo = bankAccountRepo;
        }
        #endregion

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrBankAccount_Edit)]
        public void UpdateBankAccount(GetUpdateBankAccountInputDto input)
        {
            var getSetBankAccount = (from bankAccount in _bankAccountRepo.GetAll()
                                     where bankAccount.entityCode == "1"
                                     && bankAccount.psCode == input.psCode
                                     && bankAccount.refID == input.refID
                                     && bankAccount.BankCode == input.bankCode
                                     select bankAccount).FirstOrDefault();
            if (getSetBankAccount != null)
            {
                #region Bank Account data
                var update = getSetBankAccount.MapTo<TR_BankAccount>();
                update.AccountNo = input.AccNo;
                update.AccountName = input.AccName;
                #endregion

                try
                {
                    _bankAccountRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                throw new UserFriendlyException("The Bank Account is not exist!");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrBankAccount_Delete)]
        public void DeleteBankAccount(DeleteBankAccountInputDto input)
        {
            var getSetBankAccount = (from bankAccount in _bankAccountRepo.GetAll()
                                     where bankAccount.entityCode == "1"
                                     && bankAccount.psCode == input.psCode
                                     && bankAccount.refID == input.refID
                                     && bankAccount.BankCode == input.bankCode
                                     select bankAccount).FirstOrDefault();
            if (getSetBankAccount != null)
            {
                try
                {
                    _bankAccountRepo.Delete(getSetBankAccount);
                    CurrentUnitOfWork.SaveChanges();
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                throw new UserFriendlyException("The Bank Account is not exist!");
            }
        }
    }
}
