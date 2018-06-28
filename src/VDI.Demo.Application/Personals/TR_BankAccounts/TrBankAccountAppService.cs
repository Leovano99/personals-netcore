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
using VDI.Demo.Personals.MS_Banks.Dto;
using Abp.Application.Services.Dto;

namespace VDI.Demo.Personals.TR_BankAccounts
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrBankAccount)]
    public class TrBankAccountAppService : DemoAppServiceBase, ITrBankAccountAppService
    {
        #region constructor
        private readonly IRepository<TR_BankAccount, string> _bankAccountRepo;
        private readonly IRepository<MS_BankPersonal, string> _msBankRepo;

        public TrBankAccountAppService(
            IRepository<TR_BankAccount, string> bankAccountRepo,
            IRepository<MS_BankPersonal, string> msBankRepo
        )
        {
            _bankAccountRepo = bankAccountRepo;
            _msBankRepo = msBankRepo;
        }
        #endregion
        
        /*
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
        */

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrBankAccount_Edit)]
        public void UpdateBankAccount(GetUpdateBankAccountInputDto input)
        {
            Logger.InfoFormat("UpdateBankAccount() - Started.");

            Logger.DebugFormat("UpdateBankAccount() - Start checking before update BankAccount. Parameters sent:{0}" +
                        "psCode = {1}{0}" +
                        "refID = {2}{0}" +
                        "bankCode = {3}{0}" +
                        "AccountName = {4}{0}" +
                        "AccountNo = {5}{0}" +
                        "isAutoDebit = {6}{0}" +
                        "isMain = {7}{0}"
                        , Environment.NewLine, input.psCode, input.refID, input.bankCode, input.AccName, input.AccNo, input.isAutoDebit, input.isMain);

            var checkBankNameNo = (from bankAccount in _bankAccountRepo.GetAll()
                                   where bankAccount.entityCode == "1"
                                   && bankAccount.psCode == input.psCode
                                   && bankAccount.BankCode == input.bankCode
                                   && (bankAccount.refID != input.refID &&
                                   (bankAccount.AccountNo == input.AccName && (bankAccount.AccountName == input.AccName || bankAccount.AccountNo == input.AccNo)))
                                   select bankAccount).Any();

            Logger.DebugFormat("UpdateBankAccount() - Ended checking before update BankAccount. Result BankAccount Exist = {0}", checkBankNameNo);

            if (!checkBankNameNo)
            {
                var getSetBankAccount = (from bankAccount in _bankAccountRepo.GetAll()
                                         where bankAccount.entityCode == "1"
                                         && bankAccount.psCode == input.psCode
                                         && bankAccount.refID == input.refID
                                         && bankAccount.BankCode == input.bankCode
                                         select bankAccount).FirstOrDefault();

                #region Bank Account data
                var update = getSetBankAccount.MapTo<TR_BankAccount>();
                update.AccountNo = input.AccNo;
                update.AccountName = input.AccName;
                update.isAutoDebit = input.isAutoDebit;
                update.isMain = input.isMain;
                update.BankBranchName = input.bankBranchName;
                #endregion

                try
                {
                    Logger.DebugFormat("UpdateBankAccount() - Start update BankAccount. Parameters sent:{0}" +
                        "AccountNo = {1}{0}" +
                        "AccountName = {2}{0}" +
                        "isAutoDebit = {3}{0}" +
                        "isMain = {4}{0}"
                        , Environment.NewLine, input.AccNo, input.AccName, input.isAutoDebit, input.isMain);

                    _bankAccountRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateBankAccount() - Ended update BankAccount.");
                }
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateBankAccount() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateBankAccount() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateBankAccount() - ERROR Result = {0}.", "The bank name or code is already exist in this bank type!");
                throw new UserFriendlyException("The bank name or code is already exist in this bank type!");
            }
            Logger.Info("UpdateBankAccount() - Finished.");
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

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrBankAccount_GetAllMsBankListByPsCode)]
        public ListResultDto<GetAllBankPersonalsListDto> GetAllMsBankListByPsCode(string psCode)
        {
            var getAllData = (from mb in _msBankRepo.GetAll()
                              join tb in _bankAccountRepo.GetAll() on mb.bankCode equals tb.BankCode
                              where tb.psCode==psCode
                              orderby mb.bankCode ascending
                              select new GetAllBankPersonalsListDto
                              {
                                  bankCode = mb.bankCode,
                                  bankName = mb.bankName
                              }).ToList();

            if (getAllData == null)
            {
                throw new UserFriendlyException("The Bank Account is not exist!");
            }

            return new ListResultDto<GetAllBankPersonalsListDto>(getAllData);
        }
    }
}
