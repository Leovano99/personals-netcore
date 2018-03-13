using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.MasterPlan.Project.MS_Accounts.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using Abp.AutoMapper;
using VDI.Demo.Authorization;
using Abp.Authorization;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.MasterPlan.Project.MS_Accounts
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount)]
    public class MsAccountAppService : DemoAppServiceBase, IMsAccountAppService
    {
        private readonly IRepository<MS_Account> _msAccountRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;

        public MsAccountAppService(
            IRepository<MS_Account> msAccountRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_BankBranch> msBankBranchRepo,
            IRepository<MS_Company> msCompanyRepo,
            IRepository<MS_Project> msProjectRepo
        )
        {
            _msAccountRepo = msAccountRepo;
            _msBankRepo = msBankRepo;
            _msBankBranchRepo = msBankBranchRepo;
            _msCompanyRepo = msCompanyRepo;
            _msProjectRepo = msProjectRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount_Create)]
        public void CreateMsAccount(CreateMsAccountInput input)
        {
            Logger.Info("CreateMsAccount() - Started.");
            Logger.DebugFormat("CreateMsAccount() - Start checking before insert Account. Parameters sent:{0}" +
                       "accNo = {1}{0}" +
                       "accName = {2}{0}"
                       , Environment.NewLine, input.accNo, input.accName);

            var checkAccount = (from A in _msAccountRepo.GetAll()
                                where A.entityID == 1 && A.accNo == input.accNo
                                select A).Any();

            Logger.DebugFormat("CreateMsAccount() - Ended checking before insert Account. Result = {0}", checkAccount);

            Logger.DebugFormat("CreateMsAccount() - Start get data devCode before insert Account. Parameters sent:{0}" +
                        "companyID = {1}{0}"
                        , Environment.NewLine, input.companyID);

            var getDevCode = (from x in _msCompanyRepo.GetAll()
                              where x.Id == input.companyID
                              select x.coCode).FirstOrDefault();

            Logger.DebugFormat("CreateMsAccount() - Ended get data devCode before insert Account.");

            if (!checkAccount)
            {
                var data = new MS_Account
                {
                    entityID = 1,
                    accCode = input.accCode,
                    accName = input.accName,
                    accNo = input.accNo,
                    projectID = input.projectID,
                    coID = input.companyID,
                    bankID = input.bankID,
                    bankBranchID = input.bankBranchID,
                    isActive = input.isActive,
                    devCode = getDevCode
                };

                try
                {
                    Logger.DebugFormat("CreateMsAccount() - Start insert Account. Parameters sent:{0}" +
                        "entityID = {1}{0}" +
                        "accCode = {2}{0}" +
                        "accName = {3}{0}" +
                        "accNo = {4}{0}" +
                        "projectID = {5}{0}" +
                        "coID = {6}{0}" +
                        "bankID = {7}{0}" +
                        "bankBranchID = {8}{0}" +
                        "isActive = {9}{0}" +
                        "devCode = {10}{0}"
                        , Environment.NewLine, 1, input.accCode, input.accName, input.accNo
                        , input.projectID, input.companyID, input.bankID, input.bankBranchID, input.isActive, getDevCode);

                    _msAccountRepo.Insert(data);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("CreateMsAccount() - Ended insert Account.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsAccount() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsAccount() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("CreateMsAccount() - ERROR Result = {0}.", "Account No Already Exist !");
                throw new UserFriendlyException("Account No Already Exist !");
            }
            Logger.Info("CreateMsAccount() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount_Delete)]
        public void DeleteMsAccount(int Id)
        {
            try
            {
                Logger.DebugFormat("DeleteMsAccount() - Start delete Account. Parameters sent:{0}" +
                        "accID = {1}{0}"
                        , Environment.NewLine, Id);

                _msAccountRepo.Delete(Id);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("DeleteMsAccount() - Ended delete Account.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteMsAccount() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteMsAccount() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        public ListResultDto<GetAllAccountListDto> GetAllMsAccount()
        {
            var getAllMsAccount = (from account in _msAccountRepo.GetAll()
                                   join company in _msCompanyRepo.GetAll() on account.coID equals company.Id
                                   join bankBranch in _msBankBranchRepo.GetAll() on account.bankBranchID equals bankBranch.Id
                                   join bank in _msBankRepo.GetAll() on bankBranch.bankID equals bank.Id
                                   join project in _msProjectRepo.GetAll() on account.projectID equals project.Id
                                   orderby account.Id descending
                                   select new GetAllAccountListDto
                                   {
                                       accountID = account.Id,
                                       projectName = project.projectName,
                                       accName = account.accName,
                                       bankID = bank.Id,
                                       bankName = bank.bankName,
                                       accNo = account.accNo,
                                       companyID = company.Id,
                                       companyName = company.coName,
                                       bankBranchID = bankBranch.Id,
                                       bankBranchName = bankBranch.bankBranchName,
                                       isActive = account.isActive,
                                       accCode = account.accCode,
                                       projectID = account.projectID
                                   }).ToList();

            return new ListResultDto<GetAllAccountListDto>(getAllMsAccount);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount_Edit)]
        public JObject UpdateMsAccount(CreateMsAccountInput input)
        {
            JObject obj = new JObject();
            Logger.Info("UpdateMsAccount() - Started.");
            Logger.DebugFormat("UpdateMsAccount() - Start checking before update Account. Parameters sent:{0}" +
                        "accNo = {1}{0}" +
                        "accName = {2}{0}" +
                        "accID = {3}{0}"
                        , Environment.NewLine, input.accNo, input.accName, input.ID);

            var checkAccount = (from A in _msAccountRepo.GetAll()
                                where A.Id != input.ID && (A.accNo == input.accNo || A.accName == input.accName || A.accCode == input.accCode)
                                select A).Any();

            Logger.DebugFormat("UpdateMsAccount() - Ended checking before update Account. Result = {0}", checkAccount);

            var getAccount = (from A in _msAccountRepo.GetAll()
                              where input.ID == A.Id
                              select A).FirstOrDefault();

            Logger.DebugFormat("UpdateMsAccount() - Ended get data before update Account.");

            var updateMsAccount = getAccount.MapTo<MS_Account>();

            if (!checkAccount)
            {
                Logger.DebugFormat("UpdateMsAccount() - Start get data before update Account. Parameters sent:{0}" +
                        "accID = {1}{0}"
                        , Environment.NewLine, input.ID);

                updateMsAccount.entityID = input.entityID;
                updateMsAccount.accNo = input.accNo;
                updateMsAccount.accName = input.accName;
                updateMsAccount.accCode = input.accCode;
                updateMsAccount.isActive = input.isActive;
                updateMsAccount.projectID = input.projectID;
                updateMsAccount.bankID = input.bankID;
                updateMsAccount.bankBranchID = input.bankBranchID;
                updateMsAccount.coID = input.companyID;
                
                obj.Add("message", "Edit Successfully");

                try
                {
                    Logger.DebugFormat("UpdateMsAccount() - Start update Account. Parameters sent:{0}" +
                        "entityID = {1}{0}" +
                        "accCode = {2}{0}" +
                        "accName = {3}{0}" +
                        "accNo = {4}{0}" +
                        "projectID = {5}{0}" +
                        "coID = {6}{0}" +
                        "bankID = {7}{0}" +
                        "bankBranchID = {8}{0}" +
                        "isActive = {9}{0}"
                        , Environment.NewLine, input.entityID, input.accCode, input.accName, input.accNo
                        , input.projectID, input.companyID, input.bankID, input.bankBranchID, input.isActive);

                    _msAccountRepo.Update(updateMsAccount);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateMsAccount() - Ended update Account.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsAccount() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsAccount() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsAccount() - ERROR Result = {0}.", "Account with Acc No or Name or Code Already Exist !");
                throw new UserFriendlyException("Account with Acc No or Name or Code Already Exist !");
            }
            Logger.Info("UpdateMsAccount() - Finished.");

            return obj;
        }
    }
}
