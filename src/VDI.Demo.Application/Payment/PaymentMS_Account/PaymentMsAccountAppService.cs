using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VDI.Demo.Payment.PaymentMS_Account;
using VDI.Demo.Payment.PaymentMS_Account.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.Payment.PaymentMs_Account
{
    public class PaymentMsAccountAppService : DemoAppServiceBase, IPaymentMsAccountAppService
    {
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_AccountEmail> _msAccountEmailRepo;

        public PaymentMsAccountAppService(
            IRepository<MS_Account> msAccountRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Company> msCompanyRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_BankBranch> msBankBranchRepo,
            IRepository<MS_AccountEmail> msAccountEmailRepo
            )
        {
            _msAccountRepo = msAccountRepo;
            _msProjectRepo = msProjectRepo;
            _msCompanyRepo = msCompanyRepo;
            _msBankRepo = msBankRepo;
            _msBankBranchRepo = msBankBranchRepo;
            _msAccountEmailRepo = msAccountEmailRepo;
        }

        public List<GetMsAccountListDto> GetAllMsAccount()
        {
            var getDataMsAccount = (from a in _msAccountRepo.GetAll()
                                    join p in _msProjectRepo.GetAll() on a.projectID equals p.Id
                                    join c in _msCompanyRepo.GetAll() on a.coID equals c.Id
                                    join b in _msBankRepo.GetAll() on a.bankID equals b.Id
                                    orderby a.accCode
                                    select new GetMsAccountListDto
                                    {
                                        accID = a.Id,
                                        accName = a.accName,
                                        accNo = a.accNo,
                                        projectName = p.projectName,
                                        companyName = c.coName,
                                        bankName = b.bankName,
                                        isActive = a.isActive
                                    }).ToList();

            return getDataMsAccount;
        }

        public List<DropdownMsAccountDto> GetDropdownMsAccountByCompanyCode(string coCode)
        {
            var getDDLMsAccount = (from x in _msAccountRepo.GetAll()
                                   where x.isActive == true && x.devCode == coCode
                                   orderby x.accCode ascending
                                   select new DropdownMsAccountDto
                                   {
                                       Id = x.Id,
                                       accCode = x.accCode + " - " + x.accName
                                   }).Distinct().ToList();

            return getDDLMsAccount;
        }

        public GetDetailMsAccountListDto GetMsAccountDetail(int Id)
        {
            var getDataMsAccount = (from a in _msAccountRepo.GetAll()
                                    join p in _msProjectRepo.GetAll() on a.projectID equals p.Id
                                    join c in _msCompanyRepo.GetAll() on a.coID equals c.Id
                                    join b in _msBankRepo.GetAll() on a.bankID equals b.Id
                                    join bb in _msBankBranchRepo.GetAll() on a.bankBranchID equals bb.Id
                                    join e in _msAccountEmailRepo.GetAll() on a.Id equals e.accountID into l1
                                    from e in l1.DefaultIfEmpty()
                                    where a.Id == Id
                                    select new GetDetailMsAccountListDto
                                    {
                                        accID = a.Id,
                                        accName = a.accName,
                                        accNo = a.accNo,
                                        accCode = a.accCode,
                                        projectID = a.projectID,
                                        projectName = p.projectName,
                                        coID = a.coID,
                                        companyName = c.coName,
                                        bankID = a.bankID,
                                        bankName = b.bankName,
                                        bankBranchID = a.bankBranchID,
                                        bankBranchName = bb.bankBranchName,
                                        emailCc = e == null ? null : e.emailCc,
                                        emailTo = e == null ? null : e.emailTo,
                                        accountEmailID = e == null ? 0 : e.Id,
                                        natureAccountBank = a.NATURE_ACCOUNT_BANK,
                                        natureAccountDep = a.NATURE_ACCOUNT_DEP,
                                        org = a.ORG_ID,
                                        province = a.PROVINCE_ID,
                                        isActive = a.isActive
                                    }).FirstOrDefault();

            return getDataMsAccount;
        }

        public List<DropdownMsAccountDto> MsAccountDropdown()
        {
            var getDDLMsAccount = (from x in _msAccountRepo.GetAll()
                                   where x.isActive == true
                                   orderby x.accCode ascending
                                   select new DropdownMsAccountDto
                                   {
                                       Id = x.Id,
                                       accCode = x.accCode + " - " + x.accName
                                   }).Distinct().ToList();

            return getDDLMsAccount;
        }

        public void UpdateMsAccount(UpdateMsAccountInputDto input)
        {
            Logger.Info("UpdateMsAccountPaymentPayment() - Started.");

            Logger.DebugFormat("UpdateMsAccountPayment() - Start get data before update Account. Parameters sent:{0}" +
                    "accID = {1}{0}"
                    , Environment.NewLine, input.accID);

            var getAccount = (from A in _msAccountRepo.GetAll()
                              where input.accID == A.Id
                              select A).FirstOrDefault();

            Logger.DebugFormat("UpdateMsAccountPayment() - Ended get data before update Account.");

            var UpdateMsAccountPayment = getAccount.MapTo<MS_Account>();

            UpdateMsAccountPayment.NATURE_ACCOUNT_BANK = input.natureAccountBank;
            UpdateMsAccountPayment.NATURE_ACCOUNT_DEP = input.natureAccountDep;
            UpdateMsAccountPayment.ORG_ID = input.org;
            UpdateMsAccountPayment.PROVINCE_ID = input.province;
            UpdateMsAccountPayment.isActive = input.isActive;

            try
            {
                Logger.DebugFormat("UpdateMsAccountPayment() - Start update Account in MS Account. Parameters sent:{0}" +
                    "natureAccountBank  = {1}{0}" +
                    "natureAccountDep   = {2}{0}" +
                    "org                = {3}{0}" +
                    "province           = {4}{0}" +
                    "isActive           = {5}{0}"
                    , Environment.NewLine, input.natureAccountBank, input.natureAccountDep, input.org, input.province, input.isActive);

                _msAccountRepo.Update(UpdateMsAccountPayment);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("UpdateMsAccountPayment() - End update Account in MS Account.");

                if (input.emailTo != null)
                {
                    var dataAccountEmail = new CreateUpdateMsAccountEmailInputDto
                    {
                        accID = input.accID,
                        emailTo = input.emailTo,
                        emailCc = input.emailCc
                    };

                    Logger.DebugFormat("UpdateMsAccountPayment() - Start update Account in MS Account Email. Parameters sent:{0}" +
                        "accID      = {1}{0}" +
                        "emailTo    = {2}{0}" +
                        "emailCc    = {3}{0}"
                        , Environment.NewLine, input.accID, input.emailTo, input.emailCc);

                    CreateOrUpdateAccountEmail(dataAccountEmail);

                    Logger.DebugFormat("UpdateMsAccountPayment() - End update Account in MS Account Email.");
                }
                
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("UpdateMsAccountPayment() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdateMsAccountPayment() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("UpdateMsAccountPayment() - Finished.");
        }

        private void CreateOrUpdateAccountEmail(CreateUpdateMsAccountEmailInputDto input)
        {
            Logger.Info("CreateOrUpdateAccountEmail() - Started.");

            Logger.DebugFormat("UpdateMsAccountPayment() - Start check data in MS Account Email. Parameters sent:{0}" +
                    "accID      = {1}{0}"
                    , Environment.NewLine, input.accID);

            var checkData = (from E in _msAccountEmailRepo.GetAll()
                             where E.accountID == input.accID
                             select E);

            Logger.DebugFormat("UpdateMsAccountPayment() - End check data in MS Account Email.");

            //update
            if (checkData.Any())
            {
                var dataToUpdate = checkData.FirstOrDefault().MapTo<MS_AccountEmail>();

                dataToUpdate.emailCc = input.emailCc;
                dataToUpdate.emailTo = input.emailTo;

                try
                {
                    Logger.DebugFormat("UpdateMsAccountPayment() - Start update data in MS Account Email. Parameters sent:{0}" +
                    "emailCc  = {1}{0}" +
                    "emailTo  = {2}{0}"
                    , Environment.NewLine, input.emailCc, input.emailTo);

                    _msAccountEmailRepo.Update(dataToUpdate);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateMsAccountPayment() - End update data in MS Account Email.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateOrUpdateAccountEmail() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateOrUpdateAccountEmail() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            //insert
            else
            {
                var dataToInsert = new MS_AccountEmail
                {
                    accountID = input.accID,
                    emailTo = input.emailTo,
                    emailCc = input.emailCc
                };
                try
                {
                    Logger.DebugFormat("UpdateMsAccountPayment() - Start insert data in MS Account Email. Parameters sent:{0}" +
                    "accountID= {1}{0}" +
                    "emailCc  = {2}{0}" +
                    "emailTo  = {3}{0}"
                    , Environment.NewLine, input.accID, input.emailCc, input.emailTo);

                    _msAccountEmailRepo.Insert(dataToInsert);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateMsAccountPayment() - End insert data in MS Account Email.");
                }
                catch (DataException exDb)
                {
                    Logger.DebugFormat("CreateOrUpdateAccountEmail() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateOrUpdateAccountEmail() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }

            Logger.Info("CreateOrUpdateAccountEmail() - Finished.");

        }
    }
}
