using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using VDI.Demo.Authorization;
using VDI.Demo.Payment.PaymentLK_Alloc.Dto;
using VDI.Demo.Payment.PaymentLK_Alloc;
using VDI.Demo.Payment.PaymentLK_Alloc.Dto;
using VDI.Demo.Pricing.MS_FinType.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.Payment.PaymentLK_Alloc
{
    public class PaymentLkAllocAppService : DemoAppServiceBase, IPaymentLkAllocAppService
    {
        private readonly IRepository<LK_Alloc> _lkAllocRepo;
        private readonly IRepository<LK_PayFor> _lkPayForRepo;

        public PaymentLkAllocAppService(
            IRepository<LK_Alloc> lkAllocRepo,
            IRepository<LK_PayFor> lkPayForRepo
            )
        {
            _lkAllocRepo = lkAllocRepo;
            _lkPayForRepo = lkPayForRepo;
        }

        public void CreateOrUpdateLkAlloc(CreateOrUpdateLkAllocInputDto input)
        {
            Logger.Info("CreateOrUpdateLkAlloc() - Started.");

            //update
            if (input.Id != null)
            {
                Logger.DebugFormat("CreateOrUpdateLkAlloc() - Start check existing data. Parameters sent: {0} " +
                    "Id           = {1}{0}" +
                    "allocDesc    = {2}{0}"
                    , Environment.NewLine, input.Id, input.allocDesc);

                var checkData = (from A in _lkAllocRepo.GetAll()
                                 where A.Id != input.Id && A.allocDesc == input.allocDesc
                                 select A).Any();

                Logger.DebugFormat("CreateOrUpdateLkAlloc() - End check existing data. Result: {0}", checkData);

                if (!checkData)
                {
                    Logger.DebugFormat("CreateOrUpdateLkAlloc() - Start get data Alloc for update. Parameters sent: {0} " +
                    "Id = {1}{0}"
                    , Environment.NewLine, input.Id);

                    var getDataAlloc = (from A in _lkAllocRepo.GetAll()
                                          where A.Id == input.Id
                                          select A).FirstOrDefault();

                    Logger.DebugFormat("CreateOrUpdateLkAlloc() - End get data Alloc for update");

                    var updateAlloc = getDataAlloc.MapTo<LK_Alloc>();

                    updateAlloc.allocDesc = input.allocDesc;
                    updateAlloc.isVAT = input.isVat;
                    updateAlloc.isActive = input.isActive;
                    updateAlloc.payForID = input.payForId;

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - Start update Alloc. Parameters sent: {0} " +
                           "allocDesc   = {1}{0}" +
                           "isVAT       = {2}{0}" +
                           "isActive    = {3}{0}" +
                           "payForID    = {4}{0}"
                           , Environment.NewLine, input.allocDesc, input.isVat, input.isActive, input.payForId);

                        _lkAllocRepo.Update(updateAlloc);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - End update Alloc.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateLkAlloc() - ERROR. Result = {0}", "AllocCode Or AllocDescription Already Exist!");
                    throw new UserFriendlyException("AllocCode Or AllocDescription Already Exist!");
                }

            }

            //insert
            else
            {
                Logger.DebugFormat("CreateOrUpdateLkAlloc() - Start check existing data. Parameters sent: {0} " +
                    "allocDesc    = {1}{0}" +
                    "allocCode    = {2}{0}"
                    , Environment.NewLine, input.allocDesc, input.allocCode);

                var checkData = (from A in _lkAllocRepo.GetAll()
                                 where A.allocDesc == input.allocDesc || A.allocCode == input.allocCode
                                 select A).Any();

                Logger.DebugFormat("CreateOrUpdateLkAlloc() - End check existing data. Result: {0}", checkData);

                if (!checkData)
                {
                    var dataCreateLkAlloc = new LK_Alloc
                    {
                        entityID = 1,
                        allocCode = input.allocCode,
                        allocDesc = input.allocDesc,
                        isVAT = input.isVat,
                        payForID = input.payForId,
                        isActive = input.isActive
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - Start Insert Alloc. Parameters sent: {0} " +
                            "entityID   = {1}{0}" +
                            "allocCode  = {2}{0}" +
                            "allocDesc  = {3}{0}" +
                            "isVAT      = {4}{0}" +
                            "payForID   = {5}{0}" +
                            "isActive   = {6}{0}"
                            , Environment.NewLine, 1, input.allocCode, input.allocDesc, input.isVat, input.payForId, input.isActive);

                        _lkAllocRepo.Insert(dataCreateLkAlloc);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - End Insert Alloc.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkAlloc() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateLkAlloc() - ERROR. Result = {0}", "AllocCode Or AllocDescription Already Exist!");
                    throw new UserFriendlyException("AllocCode Or AllocDescription Already Exist!");
                }
            }

            Logger.Info("CreateOrUpdateLkAlloc() - Finished.");
        }

        public List<GetLkAllocListDto> GetAllLkAlloc()
        {
            var getDataAlloc = (from a in _lkAllocRepo.GetAll()
                                join pt in _lkPayForRepo.GetAll() on a.payForID equals pt.Id
                                orderby a.Id descending
                                select new GetLkAllocListDto
                                {
                                    Id = a.Id,
                                    allocCode = a.allocCode,
                                    allocDesc = a.allocDesc,
                                    isVat = a.isVAT,
                                    isActive = a.isActive,
                                    payForId = pt.Id,
                                    payFor = pt.payForName
                                }).ToList();

            return getDataAlloc;
        }

        public List<GetLkAllocDropdownListDto> GetLkAllocDropdown()
        {
            var getDataAllocDd = (from a in _lkAllocRepo.GetAll()
                                where a.isActive == true
                                orderby a.allocCode
                                select new GetLkAllocDropdownListDto
                                {
                                    Id = a.Id,
                                    alloc = a.allocCode + " - " + a.allocDesc
                                }).ToList();

            return getDataAllocDd;
        }
    }
}
