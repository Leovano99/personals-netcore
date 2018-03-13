using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VDI.Demo.Payment.PaymentLK_PayFor.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using System.Linq;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Authorization.Users;

namespace VDI.Demo.Payment.PaymentLK_PayFor
{
    public class PaymentLkPayForAppService : DemoAppServiceBase, IPaymentLkPayForAppService
    {
        private readonly IRepository<LK_PayFor> _lkPayForRepo;
        private readonly IRepository<UserRole, long> _userRolesRepo;
        private readonly IRepository<SYS_RolesPayFor> _sysRolesPayForRepo;

        public PaymentLkPayForAppService(
            IRepository<LK_PayFor> lkPayForRepo,
            IRepository<UserRole, long> userRolesRepo,
            IRepository<SYS_RolesPayFor> sysRolesPayForRepo
            )
        {
            _lkPayForRepo = lkPayForRepo;
            _userRolesRepo = userRolesRepo;
            _sysRolesPayForRepo = sysRolesPayForRepo;
        }
        public void CreateOrUpdateLkPayFor(CreateOrUpdateLkPayForInputDto input)
        {
            Logger.InfoFormat("CreateOrUpdateLkPayFor() - Started.");
            if (input.Id == null)
            {
                Logger.InfoFormat("CreateLkPayFor() - Started.");

                Logger.DebugFormat("CreateLkPayFor(). Check duplicate payforcode & payforname. param sent: {0}"+
                    "payForCode     = {1}{0}" +
                    "payForName     = {2}{0}",
                    Environment.NewLine,input.payForCode, input.payForName);
                var checkDuplicateData = (from x in _lkPayForRepo.GetAll()
                                          where x.payForCode == input.payForCode || x.payForName == input.payForName
                                          select x).Any();
                Logger.DebugFormat("CreateLkPayFor(). Check duplicate payforcode & payforname. Result isDuplicate: {0}", checkDuplicateData);

                if (!checkDuplicateData)
                {
                    var dataCreateLkPayFor = new LK_PayFor
                    {
                        payForCode = input.payForCode,
                        payForName = input.payForName,
                        isIncome = input.isIncome,
                        isSched = input.isSched,
                        isInventory = input.isInventory,
                        isSDH = input.isSDH,
                        isActive = input.isActive
                    };

                    try
                    {
                        Logger.DebugFormat("CreateLkPayFor() - Start Insert LkPayFor. Parameters sent: {0} " +
                               "   payforcode = {1}{0}" +
                               "   payforname = {2}{0}" +
                               "   isSched = {3}{0}" +
                               "   isIncome = {4}{0}" +
                               "   isInventory = {5}{0}" +
                               "   isSDH = {6}{0}" +
                               "   isActive = {7}{0}"
                               , Environment.NewLine, input.payForCode, input.payForName, input.isSched, input.isIncome, input.isInventory,
                                input.isSDH, input.isActive);

                        _lkPayForRepo.Insert(dataCreateLkPayFor);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("CreateLkPayFor() - End Insert LkPayFor.");
                    }
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("CreateLkPayFor() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("CreateLkPayFor() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("CreateLkPayFor() - ERROR Exception. Result = {0}", "PayForCode Or PayForName is Already Exist!");
                    throw new UserFriendlyException("PayForCode Or PayForName is Already Exist!");
                }
                
                Logger.InfoFormat("CreateLkPayFor() - End.");
            }
            else
            {
                Logger.InfoFormat("UpdateLkPayFor() - Started.");
                Logger.DebugFormat("UpdateLkPayFor(). Check duplicate payforname. param sent: {0}" +
                    "payForName     = {1}{0}",
                    Environment.NewLine, input.payForName);
                var checkDuplicateData = (from A in _lkPayForRepo.GetAll()
                                 where A.Id != input.Id && A.payForName == input.payForName
                                 select A).Any();
                Logger.DebugFormat("UpdateLkPayFor(). Check duplicate payforname. Result isDuplicate: {0}", checkDuplicateData);

                if (!checkDuplicateData)
                {
                    Logger.DebugFormat("UpdateLkPayFor() - Start get data LKPayFor for update. Parameters sent: {0} " +
                    "   PayForID = {1}{0}"
                    , Environment.NewLine, input.Id);
                    var getPayFor = (from x in _lkPayForRepo.GetAll()
                                     where x.Id == input.Id
                                     select x).FirstOrDefault();
                    var updateLkPayFor = getPayFor.MapTo<LK_PayFor>();
                    Logger.DebugFormat("UpdateLkPayFor() - End  get data LkPayFor for update. ");

                    updateLkPayFor.payForName = input.payForName;
                    updateLkPayFor.isIncome = input.isIncome;
                    updateLkPayFor.isInventory = input.isInventory;
                    updateLkPayFor.isSched = input.isSched;
                    updateLkPayFor.isSDH = input.isSDH;
                    updateLkPayFor.isActive = input.isActive;

                    try
                    {
                        Logger.DebugFormat("UpdateLkPayFor() - Start update LkPayFor. Parameters sent: {0} " +
                           "PayForCode = {1}{0}" +
                           "PayForName = {2}{0}" +
                           "isIncome = {3}{0}" +
                           "isInventory = {4}{0}" +
                           "isSched = {5}{0}" +
                           "isSDH = {6}{0}" +
                           "isActive = {7}{0}",
                       Environment.NewLine, updateLkPayFor.payForCode, updateLkPayFor.payForName,
                       updateLkPayFor.isIncome, updateLkPayFor.isInventory, updateLkPayFor.isSched,
                       updateLkPayFor.isSDH, input.isActive);

                        _lkPayForRepo.Update(updateLkPayFor);
                        CurrentUnitOfWork.SaveChanges();
                        Logger.DebugFormat("UpdateLkPayFor() - End  getupdate LkPayFor");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.DebugFormat("UpdateLkPayFor() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.DebugFormat("UpdateLkPayFor() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                    Logger.InfoFormat("UpdateLkPayFor() - End.");
                }
                else
                {
                    Logger.DebugFormat("UpdateLkPayFor() - ERROR. Result = {0}", "PayForName Already Exist!");
                    throw new UserFriendlyException("PayForName Already Exist!");
                }
            }
            Logger.InfoFormat("CreateOrUpdateLkPayFor() - Finished.");
        }

        public List<CreateOrUpdateLkPayForInputDto> GetAllLkPayFor()
        {
            var getDataPayFor = (from x in _lkPayForRepo.GetAll()
                                  orderby x.Id descending
                                  select new CreateOrUpdateLkPayForInputDto
                                  {
                                      Id = x.Id,
                                      payForCode = x.payForCode,
                                      payForName = x.payForName,
                                      isSched = x.isSched,
                                      isSDH = x.isSDH,
                                      isIncome = x.isIncome,
                                      isInventory = x.isInventory,
                                      isActive = x.isActive
                                  }).ToList();

            return getDataPayFor;
        }

        public List<DropdownLkPayForDto> LkPayForDropdown()
        {
            var getDDLPayFor = (from x in _lkPayForRepo.GetAll()
                                 where x.isActive == true
                                 orderby x.payForCode ascending
                                 select new DropdownLkPayForDto
                                 {
                                     Id = x.Id,
                                     payForCode = x.payForCode,
                                     dropdownDesc = x.payForCode + " - " + x.payForName
                                 }).ToList();

            return getDDLPayFor;
        }

        [UnitOfWork(isTransactional: false)]
        public List<DropdownLkPayForDto> LkPayForDropdownCheckRole(int userID)
        {
            var getRoleId = (from A in _userRolesRepo.GetAll()
                             where A.UserId == userID
                             select A.RoleId).ToList();

            var getDDLPayForCheckRole = (from A in _sysRolesPayForRepo.GetAll()
                                         join B in _lkPayForRepo.GetAll() on A.payForID equals B.Id
                                         where B.isActive == true && getRoleId.Contains(A.rolesID)
                                         orderby B.payForCode
                                         select new DropdownLkPayForDto
                                         {
                                             Id = B.Id,
                                             payForCode = B.payForCode,
                                             dropdownDesc = B.payForCode + " - " + B.payForName
                                         }).ToList();

            return getDDLPayForCheckRole;
        }

        public List<DropdownLkPayForDto> LkPayForDropdownUnknown()
        {
            var getDDLPayFor = (from x in _lkPayForRepo.GetAll()
                                where x.isActive == true && x.payForName.Contains("Unknown")
                                orderby x.payForCode ascending
                                select new DropdownLkPayForDto
                                {
                                    Id = x.Id,
                                    payForCode = x.payForCode,
                                    dropdownDesc = x.payForCode + " - " + x.payForName
                                }).ToList();

            return getDDLPayFor;
        }
    }
}
