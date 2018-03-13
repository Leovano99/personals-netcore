using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VDI.Demo.Authorization;
using VDI.Demo.Payment.PaymentLK_PayType;
using VDI.Demo.Payment.PaymentLK_PayType.Dto;
using VDI.Demo.Pricing.MS_FinType.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.Payment.PaymentLK_PayType
{
    public class PaymentLkPayTypeAppService : DemoAppServiceBase, IPaymentLkPayTypeAppService
    {
        private readonly IRepository<LK_PayType> _lkPayTypeRepo;
        private readonly IRepository<UserRole, long> _userRoleRepo;
        private readonly IRepository<SYS_RolesPayType> _sysRolesPayTypeRepo;

        public PaymentLkPayTypeAppService(
            IRepository<LK_PayType> lkPayTypeRepo,
            IRepository<UserRole, long> userRoleRepo,
            IRepository<SYS_RolesPayType> sysRolesPayTypeRepo
            )
        {
            _lkPayTypeRepo = lkPayTypeRepo;
            _userRoleRepo = userRoleRepo;
            _sysRolesPayTypeRepo = sysRolesPayTypeRepo;
        }

        public void CreateOrUpdateLkPayType(CreateOrUpdateLkPayTypeInputDto input)
        {
            Logger.Info("CreateOrUpdateLkPayType() - Started.");

            //update
            if (input.Id != null)
            {
                Logger.DebugFormat("CreateOrUpdateLkPayType() - Start check existing data. Parameters sent: {0} " +
                    "Id             = {1}{0}" +
                    "payTypeDesc    = {2}{0}"
                    , Environment.NewLine, input.Id, input.payTypeDesc);

                var checkData = (from A in _lkPayTypeRepo.GetAll()
                                 where A.Id != input.Id && A.payTypeDesc == input.payTypeDesc
                                 select A).Any();

                Logger.DebugFormat("CreateOrUpdateLkPayType() - End check existing data. Result: {0}", checkData);

                if (!checkData)
                {
                    Logger.DebugFormat("CreateOrUpdateLkPayType() - Start get data Pay Type for update. Parameters sent: {0} " +
                    "Id = {1}{0}"
                    , Environment.NewLine, input.Id);

                    var getDataPayType = (from A in _lkPayTypeRepo.GetAll()
                                          where A.Id == input.Id
                                          select A).FirstOrDefault();

                    Logger.DebugFormat("CreateOrUpdateLkPayType() - End get data Pay Type for update");

                    var updatepayType = getDataPayType.MapTo<LK_PayType>();

                    updatepayType.payTypeDesc = input.payTypeDesc;
                    updatepayType.isBooking = input.isBooking;
                    updatepayType.isIncome = input.isIncome;
                    updatepayType.isInventory = input.isInventory;
                    updatepayType.isActive = input.isActive;

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - Start update Pay For. Parameters sent: {0} " +
                           "payTypeDesc     = {1}{0}" +
                           "isBooking       = {2}{0}" +
                           "isIncome        = {3}{0}" +
                           "isInventory     = {4}{0}" +
                           "isActive        = {5}{0}"
                           , Environment.NewLine, input.payTypeDesc, input.isBooking, input.isIncome, input.isInventory, input.isActive);

                        _lkPayTypeRepo.Update(updatepayType);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - End update Pay For.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateLkPayType() - ERROR. Result = {0}", "PayTypeCode Or PayTypeDescription Already Exist!");
                    throw new UserFriendlyException("PayTypeCode Or PayTypeDescription Already Exist!");
                }

            }

            //insert
            else
            {
                Logger.DebugFormat("CreateOrUpdateLkPayType() - Start check existing data. Parameters sent: {0} " +
                    "payTypeDesc    = {1}{0}" +
                    "payTypeCode    = {2}{0}"
                    , Environment.NewLine, input.payTypeDesc, input.payTypeCode);

                var checkData = (from A in _lkPayTypeRepo.GetAll()
                                 where A.payTypeDesc == input.payTypeDesc || A.payTypeCode == input.payTypeCode
                                 select A).Any();

                Logger.DebugFormat("CreateOrUpdateLkPayType() - End check existing data. Result: {0}", checkData);

                if (!checkData)
                {
                    var dataCreateLkPayType = new LK_PayType
                    {
                        entityID = 1,
                        payTypeCode = input.payTypeCode,
                        payTypeDesc = input.payTypeDesc,
                        isBooking = input.isBooking,
                        isIncome = input.isIncome,
                        isInventory = input.isInventory,
                        isActive = input.isActive
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - Start Insert PayType. Parameters sent: {0} " +
                            "entityID       = {1}{0}" +
                            "payTypeCode    = {2}{0}" +
                            "payTypeDesc    = {3}{0}" +
                            "isBooking      = {4}{0}" +
                            "isIncome       = {5}{0}" +
                            "isInventory    = {6}{0}" +
                            "isActive       = {7}{0}"
                            , Environment.NewLine, 1, input.payTypeCode, input.payTypeDesc, input.isBooking, input.isIncome, input.isInventory, input.isActive);

                        _lkPayTypeRepo.Insert(dataCreateLkPayType);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - End Insert PayType.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPayType() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateLkPayType() - ERROR. Result = {0}", "PayTypeCode Or PayTypeDescription Already Exist!");
                    throw new UserFriendlyException("PayTypeCode Or PayTypeDescription Already Exist!");
                }
            }

            Logger.Info("CreateOrUpdateLkPayType() - Finished.");
        }

        public List<CreateOrUpdateLkPayTypeInputDto> GetAllLkPayType()
        {
            var getDataPayType = (from pt in _lkPayTypeRepo.GetAll()
                                  orderby pt.Id descending
                                  select new CreateOrUpdateLkPayTypeInputDto
                                  {
                                      Id = pt.Id,
                                      payTypeCode = pt.payTypeCode,
                                      payTypeDesc = pt.payTypeDesc,
                                      isBooking = pt.isBooking,
                                      isIncome = pt.isIncome,
                                      isInventory = pt.isInventory,
                                      isActive = pt.isActive
                                  }).ToList();

            return getDataPayType;
        }

        public List<DropdownPayTypeDto> GetLkPayTypeDropdown()
        {
            var getDataPayTypeDd = (from pt in _lkPayTypeRepo.GetAll()
                                    where pt.isActive == true && pt.isInventory == false
                                    orderby pt.payTypeCode
                                    select new DropdownPayTypeDto
                                    {
                                        Id = pt.Id,
                                        payType = pt.payTypeCode + " - " + pt.payTypeDesc
                                    }).ToList();

            return getDataPayTypeDd;
        }

        [UnitOfWork(isTransactional:false)]
        public List<DropdownPayTypeDto> GetLkPayTypeDropdownCheckRole(int userID)
        {
            var getRole = (from a in _userRoleRepo.GetAll()
                           where a.UserId == userID
                           select a.RoleId).ToList();

            var getPayTypeCheckRole = (from a in _sysRolesPayTypeRepo.GetAll()
                                      join b in _lkPayTypeRepo.GetAll() on a.payTypeID equals b.Id
                                      where getRole.Contains(a.rolesID) && b.isInventory == false && b.isActive == true
                                      orderby b.payTypeCode
                                      select new DropdownPayTypeDto
                                      {
                                          Id = b.Id,
                                          payType = b.payTypeCode + " - " + b.payTypeDesc
                                      }).ToList();

            return getPayTypeCheckRole;
        }
    }
}
