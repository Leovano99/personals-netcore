using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VDI.Demo.Payment.PaymentLK_OthersType.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using System.Linq;
using Abp.AutoMapper;
using Abp.Authorization.Users;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.Extensions;

namespace VDI.Demo.Payment.PaymentLK_OthersType
{
    public class PaymentLkOthersTypeAppService : DemoAppServiceBase, IPaymentLkOthersTypeAppService
    {
        private readonly IRepository<LK_OthersType> _lkOthersTypeRepo;
        private readonly IRepository<LK_PayFor> _lkPayForRepo;
        private readonly IRepository<UserRole, long> _userRoleRepo;
        private readonly IRepository<SYS_RolesOthersType> _sysRolesOthersTypeRepo;

        public PaymentLkOthersTypeAppService(
            IRepository<LK_OthersType> lkOthersTypeRepo,
            IRepository<LK_PayFor> lkPayForRepo,
            IRepository<UserRole, long> userRoleRepo,
            IRepository<SYS_RolesOthersType> sysRolesOthersTypeRepo
            )
        {
            _lkOthersTypeRepo = lkOthersTypeRepo;
            _lkPayForRepo = lkPayForRepo;
            _userRoleRepo = userRoleRepo;
            _sysRolesOthersTypeRepo = sysRolesOthersTypeRepo;
        }

        public void CreateOrUpdateLkOthersType(CreateOrUpdateLkOthersTypeInputDto input)
        {
            Logger.InfoFormat("CreateOrUpdateLkOthersType() - Started.");
            if (input.Id == null)
            {
                Logger.InfoFormat("CreateLkOthersType() - Started.");

                Logger.DebugFormat("CreateLkOthersType(). Check duplicate othersTypeCode & othersTypeDesc. param sent: {0}" +
                    "othersTypeCode     = {1}{0}" +
                    "othersTypeDesc     = {2}{0}",
                Environment.NewLine, input.othersTypeCode, input.othersTypeDesc);

                var checkDuplicateData = (from x in _lkOthersTypeRepo.GetAll()
                                          where x.othersTypeCode == input.othersTypeCode || x.othersTypeDesc == input.othersTypeDesc
                                          select x).Any();

                Logger.DebugFormat("CreateLkOthersType(). Check duplicate othersTypeCode & othersTypeDesc. Result isDuplicate: {0}", checkDuplicateData);

                if (!checkDuplicateData)
                {
                    var dataCreateLkOthersType = new LK_OthersType
                    {
                        othersTypeCode = input.othersTypeCode,
                        othersTypeDesc = input.othersTypeDesc,
                        isActive = input.isActive,
                        isAdjSAD = input.isAdjSAD,
                        isOthers = input.isOthers,
                        isSDH = input.isSDH,
                        isOTP = input.isOTP,
                        isPayment = input.isPayment,
                        sortNum = input.sortNum
                    };

                    try
                    {
                        Logger.DebugFormat("CreateLkOthersType() - Start Insert LkOthersType. Parameters sent: {0} " +
                               "   othersTypeCode   = {1}{0}" +
                               "   othersTypeDesc   = {2}{0}" +
                               "   isActive         = {3}{0}" +
                               "   isAdjSAD         = {4}{0}" +
                               "   isOthers         = {5}{0}" +
                               "   isSDH            = {6}{0}" +
                               "   isOTP            = {7}{0}" +
                               "   isPayment        = {8}{0}" +
                               "   sortNum          = {9}{0}",
                        Environment.NewLine, input.othersTypeCode, input.othersTypeDesc, input.isActive, input.isAdjSAD, input.isOthers,
                                input.isSDH, input.isOTP, input.isPayment, input.sortNum);

                        _lkOthersTypeRepo.Insert(dataCreateLkOthersType);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("CreateLkOthersType() - End Insert LkOthersType.");
                    }
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateLkOthersType() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateLkOthersType() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("CreateLkOthersType() - ERROR Exception. Result = {0}", "othersTypeCode Or othersTypeDesc is Already Exist!");
                    throw new UserFriendlyException("othersTypeCode Or othersTypeDesc is Already Exist!");
                }

                Logger.InfoFormat("CreateLkOthersType() - End.");
            }
            else
            {
                Logger.InfoFormat("UpdateLkOthersType() - Started.");
                Logger.DebugFormat("UpdateLkOthersType(). Check duplicate othersTypeDesc. param sent: {0}" +
                    "payForName     = {1}{0}",
                    Environment.NewLine, input.othersTypeDesc);
                var checkDuplicateData = (from A in _lkOthersTypeRepo.GetAll()
                                          where A.Id != input.Id && A.othersTypeDesc == input.othersTypeDesc
                                          select A).Any();
                Logger.DebugFormat("UpdateLkOthersType(). Check duplicate othersTypeDesc. Result isDuplicate: {0}", checkDuplicateData);

                if (!checkDuplicateData)
                {
                    Logger.DebugFormat("UpdateLkOthersType() - Start get data OthersType for update. Parameters sent: {0} " +
                            "   OthersTypeID = {1}{0}"
                    , Environment.NewLine, input.Id);
                    var getOthersType = (from x in _lkOthersTypeRepo.GetAll()
                                     where x.Id == input.Id
                                     select x).FirstOrDefault();
                    var updateLkOthersType = getOthersType.MapTo<LK_OthersType>();
                    Logger.DebugFormat("UpdateLkOthersType() - End  get data OthersType for update. ");

                    updateLkOthersType.othersTypeDesc = input.othersTypeDesc;
                    updateLkOthersType.isAdjSAD = input.isAdjSAD;
                    updateLkOthersType.isOthers = input.isOthers;
                    updateLkOthersType.isOTP = input.isOTP;
                    updateLkOthersType.isSDH = input.isSDH;
                    updateLkOthersType.isPayment = input.isPayment;
                    updateLkOthersType.isActive = input.isActive;
                    updateLkOthersType.sortNum = input.sortNum;

                    try
                    {
                        Logger.DebugFormat("UpdateLkOthersType() - Start Update LkOthersType. Parameters sent: {0} " +
                               "   othersTypeCode   = {1}{0}" +
                               "   othersTypeDesc   = {2}{0}" +
                               "   isActive         = {3}{0}" +
                               "   isAdjSAD         = {4}{0}" +
                               "   isOthers         = {5}{0}" +
                               "   isSDH            = {6}{0}" +
                               "   isOTP            = {7}{0}" +
                               "   isPayment        = {8}{0}" +
                               "   sortNum          = {9}{0}"
                               , Environment.NewLine, input.othersTypeCode, input.othersTypeDesc, input.isActive, input.isAdjSAD, input.isOthers,
                                input.isSDH, input.isOTP, input.isPayment, input.sortNum);

                        _lkOthersTypeRepo.Update(updateLkOthersType);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("UpdateLkOthersType() - End  getupdate LkOthersType");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("UpdateLkOthersType() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("UpdateLkOthersType() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                    Logger.InfoFormat("UpdateLkOthersType() - End.");
                }
                else
                {
                    Logger.DebugFormat("UpdateLkOthersType() - ERROR. Result = {0}", "OthersTypeDesc Already Exist!");
                    throw new UserFriendlyException("OthersTypeDesc Already Exist!");
                }
            }
            Logger.InfoFormat("CreateOrUpdateLkPayFor() - Finished.");
        }

        public List<CreateOrUpdateLkOthersTypeInputDto> GetAllLkOthersType()
        {
            var getDataOthersType = (from x in _lkOthersTypeRepo.GetAll()
                                 orderby x.Id descending
                                 select new CreateOrUpdateLkOthersTypeInputDto
                                 {
                                     Id = x.Id,
                                     othersTypeCode = x.othersTypeCode,
                                     othersTypeDesc = x.othersTypeDesc,
                                     isAdjSAD = x.isAdjSAD,
                                     isSDH = x.isSDH,
                                     isOthers = x.isOthers,
                                     isOTP = x.isOTP,
                                     sortNum = x.sortNum,
                                     isPayment = x.isPayment,
                                     isActive = x.isActive
                                 }).ToList();

            return getDataOthersType;
        }

        [UnitOfWork(isTransactional: false)]
        public List<DropdownLkOthersTypeDto> GetLkOthersTypeDropdownCheckRole(int userID, int payForID)
        {
            var getRole = (from a in _userRoleRepo.GetAll()
                           where a.UserId == userID
                           select a.RoleId).ToList();

            var getPayForCode = (from a in _lkPayForRepo.GetAll()
                                 where a.Id == payForID
                                 select a.payForCode).FirstOrDefault();

            var getOthersTypeCheckRole = (from a in _sysRolesOthersTypeRepo.GetAll()
                                          join b in _lkOthersTypeRepo.GetAll() on a.othersTypeID equals b.Id
                                          where getRole.Contains(a.rolesID) && b.isActive == true
                                          select new
                                          {
                                              b.Id,
                                              othersType = b.othersTypeCode + " - " + b.othersTypeDesc,
                                              b.isAdjSAD,
                                              b.isOthers,
                                              b.isOTP,
                                              b.isPayment,
                                              b.isSDH
                                          })
                                      .WhereIf(getPayForCode.Contains("PMT"), item => item.isPayment == true)
                                      .WhereIf(getPayForCode.Contains("ADJ"), item => item.isAdjSAD == true)
                                      .WhereIf(getPayForCode.Contains("OTP"), item => item.isOTP == true)
                                      .WhereIf(getPayForCode.Contains("OTH"), item => item.isOthers == true)
                                      .Select(x => new DropdownLkOthersTypeDto
                                      {
                                          Id = x.Id,
                                          othersType = x.othersType
                                      })
                                      .ToList();

            return getOthersTypeCheckRole;
        }
    }
}
