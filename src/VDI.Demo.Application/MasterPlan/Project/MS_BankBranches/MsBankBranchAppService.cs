using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.MasterPlan.Project.MS_BankBranches.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using Abp.Authorization;
using VDI.Demo.Authorization;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.MasterPlan.Project.MS_BankBranches
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBankBranch)]
    public class MsBankBranchAppService : DemoAppServiceBase, IMsBankBranchAppService
    {
        private readonly IRepository<MS_BankBranch> _msBankBranchRepo;
        private readonly IRepository<MS_Bank> _msBankRepo;
        private readonly IRepository<LK_BankLevel> _lkBankLevelRepo;
        private readonly IRepository<MS_Account> _msAccountRepo;

        public MsBankBranchAppService(
            IRepository<MS_BankBranch> msBankBranchRepo,
            IRepository<MS_Bank> msBankRepo,
            IRepository<MS_Account> msAccountRepo,
            IRepository<LK_BankLevel> lkBankLevelRepo)
        {
            _msBankBranchRepo = msBankBranchRepo;
            _msBankRepo = msBankRepo;
            _lkBankLevelRepo = lkBankLevelRepo;
            _msAccountRepo = msAccountRepo;
        }
        
        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBankBranch_Delete)]
        public void DeleteMsBankBranch(int Id)
        {
            Logger.InfoFormat("DeleteMsBankBranch() - Started.");

            Logger.DebugFormat("DeleteMsBankBranch() - Start checking existing bankBranchID. Parameters sent: {0} " +
                "bankBranchID = {1}{0}", Environment.NewLine, Id);
            var checkAccount = _msAccountRepo.GetAll().Where(x => x.bankBranchID == Id).Any();
            Logger.DebugFormat("DeleteMsBankBranch() - End checking existing bankBranchID. Result = {0}", checkAccount);

            if (!checkAccount)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsBankBranch() - Start delete bankBranch. Parameters sent: {0} " +
                        "bankBranchID = {1}{0}", Environment.NewLine, Id);
                    _msBankBranchRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("DeleteMsBankBranch() - End delete bankBranch");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("DeleteMsBankBranch() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("DeleteMsBankBranch() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("DeleteMsBankBranch() - ERROR. Result = {0}", "This Bank Branch is used by an Account !");
                throw new UserFriendlyException("This Bank Branch is used by an Account !");
            }
            Logger.InfoFormat("DeleteMsBankBranch() - Finished.");
        }
        
        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount)]
        public ListResultDto<GetBankBranchListDto> GetMsBankBranch()
        {
            var listResult = (from bankBranch in _msBankBranchRepo.GetAll()
                              join bank in _msBankRepo.GetAll() on bankBranch.bankID equals bank.Id
                              join bankLevel in _lkBankLevelRepo.GetAll() on bankBranch.bankBranchTypeID equals bankLevel.Id
                              orderby bankBranch.CreationTime descending
                              select new GetBankBranchListDto
                              {
                                  Id = bankBranch.Id,
                                  bankBranchCode = bankBranch.bankBranchCode,
                                  bankBranchName = bankBranch.bankBranchName,
                                  bankID = bank.Id,
                                  bankName = bank.bankName,
                                  bankBranchType = bankLevel.bankLevelName,
                                  bankBranchTypeID = bankLevel.Id,
                                  PICName = bankBranch.PICName,
                                  PICPosition = bankBranch.PICPosition,
                                  phone = bankBranch.phone,
                                  email = bankBranch.email,
                                  isActive = bankBranch.isActive
                              }).ToList();

            return new ListResultDto<GetBankBranchListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterAccount)]
        public ListResultDto<GetBankBranchListDto> GetMsBankBranchByBank(int bankId)
        {
            var listResult = (from x in _msBankBranchRepo.GetAll()
                              where x.bankID == bankId
                              select new GetBankBranchListDto
                              {
                                  Id = x.Id,
                                  bankBranchName = x.bankBranchName
                              }).ToList();

            return new ListResultDto<GetBankBranchListDto>(listResult);
        }

        public GetBankBranchListDto GetDetailMsBankBranch(int Id)
        {
            var listResult = (from x in _msBankBranchRepo.GetAll()
                              where x.Id == Id
                              select new GetBankBranchListDto
                              {
                                  Id = x.Id,
                                  bankBranchName = x.bankBranchName,
                                  bankBranchCode = x.bankBranchCode,
                                  PICName = x.PICName,
                                  PICPosition = x.PICPosition,
                                  phone = x.phone,
                                  email = x.email,
                                  isActive = x.isActive,
                                  bankID = x.bankID
                              }).FirstOrDefault();
            return listResult;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBankBranch_Create)]
        public void CreateMsBankBranch(CreateMsBankBranchInputDto input)
        {
            Logger.InfoFormat("CreateMsBankBranch() - Started.");

            Logger.DebugFormat("CreateMsBankBranch() - Start checking existing bankBranchCode, bankBranchName. Parameters sent: {0} " +
                "bankBranchCode = {1}{0}" +
                "bankBranchName = {2}{0}"
                , Environment.NewLine, input.bankBranchCode, input.bankBranchName);

            var checkCode = (from x in _msBankBranchRepo.GetAll()
                             where x.bankBranchCode == input.bankBranchCode
                             select x).Any();
            Logger.DebugFormat("CreateMsBankBranch() - End checking existing bankBranchCode, Result = {0}", checkCode);

            if (!checkCode)
            {
                var data = new MS_BankBranch
                {
                    entityID = 1, //hardcode
                    bankBranchCode = input.bankBranchCode,
                    bankBranchName = input.bankBranchName,
                    PICName = input.PICName,
                    PICPosition = input.PICPosition,
                    phone = input.phone,
                    email = input.email,
                    isActive = input.isActive,
                    bankID = input.bankID,
                    bankBranchTypeID = input.bankBranchTypeID,
                    bankRekNo = "-",
                    projectCode = "-"
                };

                try
                {
                    Logger.DebugFormat("CreateMsBankBranch() - Start Insert msBankBranch. Parameters sent: {0} " +
                        "entityID = {12}{0}" +
                        "bankBranchCode = {1}{0}" +
                        "bankBranchName = {2}{0}" +
                        "PICName = {3}{0}" +
                        "PICPosition = {4}{0}" +
                        "phone = {5}{0}" +
                        "email = {6}{0}" +
                        "isActive = {7}{0}" +
                        "bankID = {8}{0}" +
                        "bankBranchTypeID = {9}{0}" +
                        "bankRekNo = {10}{0}" +
                        "projectCode = {11}{0}"
                        , Environment.NewLine, input.bankBranchCode, input.bankBranchName, input.PICName, input.PICPosition, input.phone
                        , input.email, input.isActive, input.bankID, input.bankBranchTypeID, "-", "-", 1);
                    _msBankBranchRepo.Insert(data);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsBankBranch() - End Insert msBankBranch.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("CreateMsBankBranch() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsBankBranch() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("CreateMsBankBranch() - ERROR. Result = {0}", "Branch Code Already Exist !");
                throw new UserFriendlyException("Branch Code Already Exist !");
            }
            Logger.InfoFormat("CreateMsBankBranch() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterBankBranch_Edit)]
        public JObject UpdateMsBankBranch(UpdateMsBankBranchInputDto input)
        {
            JObject obj = new JObject();
            Logger.InfoFormat("UpdateMsBankBranch() - Started.");

            Logger.DebugFormat("UpdateMsBankBranch() - Start checking existing checking existing bankBranchCode, bankBranchName. Parameters sent: {0} " +
                "zoningCode = {1}{0}" +
                "zoningName = {2}{0}" +
                "zoningName = {3}{0}"
                , Environment.NewLine, input.Id, input.bankBranchCode, input.bankBranchName);

            var checkCode = (from x in _msBankBranchRepo.GetAll()
                             where x.Id != input.Id && x.bankBranchCode == input.bankBranchCode
                             select x).Any();

            Logger.DebugFormat("UpdateMsBankBranch() - End checking existing bankBranchCode. Result = {0}", checkCode);

            if (!checkCode)
            {
                Logger.DebugFormat("UpdateMsBankBranch() - Start get BankBranch for update. Parameters sent: {0} " +
                    "bankBranchId = {1}{0}", Environment.NewLine, input.Id);
                var getBankBranch = (from x in _msBankBranchRepo.GetAll()
                                     where x.Id == input.Id
                                     select x).FirstOrDefault();
                Logger.DebugFormat("UpdateMsBankBranch() - End get BankBranch for update. Result = {0} ", getBankBranch);

                Logger.DebugFormat("UpdateMsBankBranch() - Start Check MS_Account for update. BankBranchId = {0} ", input.Id);
                var checkAccount = _msAccountRepo.GetAll().Where(x => x.bankBranchID == input.Id).Any();
                Logger.DebugFormat("UpdateMsBankBranch() - End Check MS_Account for update. Result = {0} ", checkAccount);

                var updateBankBranch = getBankBranch.MapTo<MS_BankBranch>();

                updateBankBranch.PICName = input.PICName;
                updateBankBranch.PICPosition = input.PICPosition;
                updateBankBranch.phone = input.phone;
                updateBankBranch.email = input.email;
                updateBankBranch.isActive = input.isActive;
                updateBankBranch.bankID = input.bankID;
                updateBankBranch.bankBranchTypeID = input.bankBranchTypeID;

                if (!checkAccount)
                {
                    updateBankBranch.bankBranchCode = input.bankBranchCode;
                    updateBankBranch.bankBranchName = input.bankBranchName;

                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change bankBranch Name & Code");
                }

                try
                {
                    Logger.DebugFormat("UpdateMsBankBranch() - Start Update msBankBranch. Parameters sent: {0} " +
                        "bankBranchCode = {1}{0}" +
                        "bankBranchName = {2}{0}" +
                        "PICName = {3}{0}" +
                        "PICPosition = {4}{0}" +
                        "phone = {5}{0}" +
                        "email = {6}{0}" +
                        "isActive = {7}{0}" +
                        "bankID = {8}{0}" +
                        "bankBranchTypeID = {9}{0}"
                        , Environment.NewLine, input.bankBranchCode, input.bankBranchName, input.PICName, input.PICPosition, input.phone
                        , input.email, input.isActive, input.bankID, input.bankBranchTypeID);
                    _msBankBranchRepo.Update(updateBankBranch);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsBankBranch() - End Update msBankBranch.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("UpdateMsBankBranch() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsBankBranch() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsBankBranch() - ERROR. Result = {0}", "Branch Code Already Exist !");
                throw new UserFriendlyException("Branch Code Already Exist !");
            }
            Logger.InfoFormat("UpdateMsBankBranch() - Finished.");
            return obj;
        }

        public ListResultDto<GetBankBranchTypeListDto> GetBankBranchTypeDropdown()
        {
            var getBankBranchType = (from x in _lkBankLevelRepo.GetAll()
                                     where !x.isBankType && x.isBankBranchType
                                     orderby x.Id descending
                                     select new GetBankBranchTypeListDto
                                     {
                                         bankBranchTypeID = x.Id,
                                         typeName = x.bankLevelName
                                     }).ToList();

            return new ListResultDto<GetBankBranchTypeListDto>(getBankBranchType);
        }
    }
}
