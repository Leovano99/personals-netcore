using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Project.MS_Banks.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.MasterPlan.Project.MS_Banks
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBank)]
    public class MsBankAppService : DemoAppServiceBase, IMsBankAppService
    {
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<LK_BankLevel> _lkBankLevelRepo;
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;

        public MsBankAppService(
            IRepository<MS_Bank> msBankRepo,
            IRepository<LK_BankLevel> lkBankLevelRepo,
            IRepository<MS_BankBranch> msBankBranchRepo,
            IRepository<MS_Account> msAccountRepo
        )
        {
            _msBankRepo = msBankRepo;
            _lkBankLevelRepo = lkBankLevelRepo;
            _msBankBranchRepo = msBankBranchRepo;
            _msAccountRepo = msAccountRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBank_Create)]
        public void CreateMsBank(CreateMsBankInput input)
        {
            Logger.Info("CreateMsBank() - Started.");
            Logger.DebugFormat("CreateMsBank() - Start checking before insert Bank. Parameters sent:{0}" +
                        "bankCode = {1}{0}" +
                        "bankName = {2}{0}"
                        , Environment.NewLine, input.bankCode, input.bankName);

            var checkBank = (from A in _msBankRepo.GetAll()
                             where A.bankCode == input.bankCode || A.bankName == input.bankName
                             select A).Any();

            Logger.DebugFormat("CreateMsBank() - Ended checking before insert Bank. Result = {0}", checkBank);

            if (!checkBank)
            {
                var createMsBank = new MS_Bank
                {
                    bankName = input.bankName,
                    bankCode = input.bankCode,
                    parentBankCode = "-",
                    divertToRO = false,
                    address = "-",
                    phone = "-",
                    fax = "-",
                    headName = "-",
                    deputyName1 = "-",
                    deputyName2 = "-",
                    att = "-",
                    isActive = input.isActive,
                    SWIFTCode = input.swiftCode,
                    bankTypeID = input.bankTypeID
                };

                try
                {
                    Logger.DebugFormat("CreateMsBank() - Start insert Bank. Parameters sent:{0}" +
                        "bankName = {1}{0}" +
                        "bankCode = {2}{0}" +
                        "parentBankCode = {3}{0}" +
                        "divertToRO = {4}{0}" +
                        "address = {5}{0}" +
                        "phone = {6}{0}" +
                        "fax = {7}{0}" +
                        "headName = {8}{0}" +
                        "deputyName1 = {9}{0}" +
                        "deputyName2 = {10}{0}" +
                        "att = {11}{0}" +
                        "isActive = {12}{0}" +
                        "SWIFTCode = {13{0}" +
                        "bankTypeID = {14}{0}"
                        , Environment.NewLine, input.bankName, input.bankCode, "-", false, "-", "-", "-"
                        , "-", "-", "-", "-", input.isActive, input.swiftCode, input.bankTypeID);

                    _msBankRepo.Insert(createMsBank);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("CreateMsBank() - Ended insert Bank.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsBank() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsBank() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                Logger.ErrorFormat("CreateMsBank() - ERROR Result = {0}.", "Bank Code or Bank Name Already Exist !");
                throw new UserFriendlyException("Bank Code or Bank Name Already Exist !");
            }
            Logger.Info("CreateMsBank() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBank_Delete)]
        public void DeleteMsBank(int Id)
        {
            Logger.Info("DeleteMsBank() - Started.");
            Logger.DebugFormat("DeleteMsBank() - Start checking before delete Bank. Parameters sent:{0}" +
                        "bankID = {1}{0}"
                        , Environment.NewLine, Id);

            var checkBankBranch = _msBankBranchRepo.GetAll().Where(x => x.bankID == Id).Any();
            var checkAccount = _msAccountRepo.GetAll().Where(x => x.bankID == Id).Any();

            Logger.DebugFormat("DeleteMsBank() - Ended checking before delete Bank. Result (checkBankBranch, checkAccount)= {0}, {1}", checkBankBranch, checkAccount);

            if (!checkBankBranch && !checkAccount)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsBank() - Start delete Bank. Parameters sent:{0}" +
                        "bankID = {1}{0}"
                        , Environment.NewLine, Id);

                    _msBankRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteMsBank() - Ended delete Bank.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsBank() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsBank() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsBank() - ERROR Result = {0}.", "This bank is used by another master!");
                throw new UserFriendlyException("This Bank is used by another master!");
            }
            Logger.Info("DeleteMsBank() - Finished.");
        }

        public ListResultDto<GetAllBankListDto> GetAllMsBank()
        {
            var getAllBank = (from A in _msBankRepo.GetAll()
                              join b in _lkBankLevelRepo.GetAll()
                              on A.bankTypeID equals b.Id
                              orderby A.Id descending
                              select new GetAllBankListDto
                              {
                                  bankID = A.Id,
                                  bankName = A.bankName,
                                  bankCode = A.bankCode,
                                  swiftCode = A.SWIFTCode,
                                  bankTypeID = b.Id,
                                  bankTypeName = b.bankLevelName,
                                  isActive = A.isActive
                              }).ToList();
            return new ListResultDto<GetAllBankListDto>(getAllBank);
        }

        public ListResultDto<GetBankListDto> GetAllMsBankList()
        {
            var getAllData = (from A in _msBankRepo.GetAll()
                              select new GetBankListDto
                              {
                                  bankCode = A.bankCode,
                                  bankName = A.bankName
                              }).ToList();

            return new ListResultDto<GetBankListDto>(getAllData);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount, AppPermissions.Pages_Tenant_MasterBankBranch)]
        public ListResultDto<GetBankDropDownListDto> GetMsBankDropDown()
        {
            var getBankDropDown = (from A in _msBankRepo.GetAll()
                                   orderby A.Id descending
                                   select new GetBankDropDownListDto
                                   {
                                       bankID = A.Id,
                                       bankName = A.bankName
                                   }).ToList();
            return new ListResultDto<GetBankDropDownListDto>(getBankDropDown);
        }

        public ListResultDto<GetTypeBankDropDownListDto> GetBankTypeDropdown()
        {
            var getBankType = (from x in _lkBankLevelRepo.GetAll()
                               where x.isBankType && !x.isBankBranchType
                               orderby x.Id descending
                               select new GetTypeBankDropDownListDto
                               {
                                   bankTypeID = x.Id,
                                   typeName = x.bankLevelName
                               }).ToList();
            return new ListResultDto<GetTypeBankDropDownListDto>(getBankType);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBank_Edit)]
        public JObject UpdateMsBank(GetAllBankListDto input)
        {
            Logger.Info("UpdateMsBank() - Started.");
            Logger.DebugFormat("UpdateMsBank() - Start checking before update Bank. Parameters sent:{0}" +
                        "bankID = {1}{0}" +
                        "bankCode = {2}{0}" +
                        "bankName = {3}{0}"
                        , Environment.NewLine, input.bankID, input.bankCode, input.bankName);

            JObject obj = new JObject();

            var checkBank = (from A in _msBankRepo.GetAll()
                             where A.Id != input.bankID && (A.bankCode == input.bankCode || A.bankName == input.bankName)
                             select A).Any();
            Logger.DebugFormat("UpdateMsBank() - Ended checking MS_Bank before update Bank. Result = {0}", checkBank);

            var checkMsAccount = (from A in _msAccountRepo.GetAll()
                                  where A.bankID == input.bankID
                                  select A).Any();
            Logger.DebugFormat("UpdateMsBank() - Ended checking MS_Account before update Bank. Result = {0}", checkMsAccount);

            var checkMsBankBranch = (from A in _msBankBranchRepo.GetAll()
                                     where A.bankID == input.bankID
                                     select A).Any();
            Logger.DebugFormat("UpdateMsBank() - Ended checking MS_BankBranch before update Bank. Result = {0}", checkMsAccount);

            if (!checkBank)
            {
                Logger.DebugFormat("UpdateMsBank() - Start get data before update Bank. Parameters sent:{0}" +
                            "bankID = {1}{0}"
                            , Environment.NewLine, input.bankID);

                var getMsbank = (from A in _msBankRepo.GetAll()
                                 where input.bankID == A.Id
                                 select A).FirstOrDefault();

                Logger.DebugFormat("UpdateMsBank() - Ended get data before update Bank.");

                var updateMsbank = getMsbank.MapTo<MS_Bank>();

                updateMsbank.SWIFTCode = input.swiftCode;
                updateMsbank.bankTypeID = input.bankTypeID;
                updateMsbank.isActive = input.isActive;

                if (!checkMsAccount && !checkMsBankBranch)
                {
                    updateMsbank.bankName = input.bankName;
                    updateMsbank.bankCode = input.bankCode;

                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change Bank Name & Code");
                }

                try
                {
                    Logger.DebugFormat("UpdateMsBank() - Start update Bank. Parameters sent:{0}" +
                        "bankName = {1}{0}" +
                        "bankCode = {2}{0}" +
                        "isActive = {3}{0}" +
                        "SWIFTCode = {4{0}" +
                        "bankTypeID = {5}{0}"
                        , Environment.NewLine, input.bankName, input.bankCode, input.isActive, input.swiftCode, input.bankTypeID);

                    _msBankRepo.Update(updateMsbank);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("UpdateMsBank() - Ended update Bank.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsBank() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsBank() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsBank() - ERROR Result = {0}.", "Bank Code or Bank Name Already Exist !");
                throw new UserFriendlyException("Bank Code or Bank Name Already Exist !");
            }
            Logger.Info("UpdateMsBank() - Finished.");
            return obj;
        }
    }
}
