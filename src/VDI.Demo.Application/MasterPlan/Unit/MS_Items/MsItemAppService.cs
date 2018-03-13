using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Items.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Items
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Item)]
    public class MsItemAppService : DemoAppServiceBase, IMsItemAppService
    {
        private readonly IRepository<LK_Item> _lkItemRepo;
        private readonly IRepository<MS_UnitItem> _msUnitItemRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;

        public MsItemAppService(
            IRepository<LK_Item> lkItemRepo,
            IRepository<MS_UnitItem> msUnitItemRepo,
            IRepository<MS_Unit> msUnitRepo)
        {
            _lkItemRepo = lkItemRepo;
            _msUnitItemRepo = msUnitItemRepo;
            _msUnitRepo = msUnitRepo;
        }

        public ListResultDto<GetAllMsItemListDto> GetAllMsItem()
        {
            var listResult = (from x in _lkItemRepo.GetAll()
                              orderby x.Id descending
                              select new GetAllMsItemListDto
                              {
                                  Id = x.Id,
                                  itemCode = x.itemCode,
                                  itemName = x.itemName,
                                  shortName = x.shortName
                              }).ToList();


            return new ListResultDto<GetAllMsItemListDto>(listResult);

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GenerateUnit)]
        public ListResultDto<GetAllMsItemListDto> GetMsItemDropdown()
        {
            var listResult = (from x in _lkItemRepo.GetAll()
                              select new GetAllMsItemListDto
                              {
                                  Id = x.Id,
                                  itemCode = x.itemCode,
                                  itemName = x.itemName,
                                  shortName = x.shortName
                              }).ToList();


            return new ListResultDto<GetAllMsItemListDto>(listResult);

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Item_Create)]
        public void CreateMsItem(CreateMsItemInput input)
        {
            Logger.Info("CreateMsItem() - Started.");

            Logger.DebugFormat("CreateMsItem() - Start checking exiting code, name, and shortName. Params sent:{0}" +
                "itemCode   ={1}{0}" +
                "itemName   ={2}{0}" +
                "shortName  ={3}"
                , Environment.NewLine, input.itemCode, input.itemName, input.shortName);
            var checkCodeName = (from x in _lkItemRepo.GetAll()
                                 where x.itemCode == input.itemCode || x.itemName == input.itemName || x.shortName == input.shortName
                                 select x).Any();
            Logger.DebugFormat("CreateMsItem() - End checking exiting code, name, and shortName. Result:{0}", checkCodeName);

            if (!checkCodeName)
            {
                var createMsItem = new LK_Item
                {
                    itemName = input.itemName,
                    itemCode = input.itemCode,
                    shortName = input.shortName,
                    sortNo = 0, //hardcode for not null field
                    isOption = false, //hardcode for not null field
                    optionSort = 0 //hardcode for not null field
                };


                try
                {
                    Logger.DebugFormat("CreateMsItem() - Start update Item. Parameters sent:{0}" +
                            "itemName    = {1}{0}" +
                            "itemCode    = {2}{0}" +
                            "shortName   = {3}{0}" +
                            "sortNo      = {4}{0}" +
                            "isOption    = {5}{0}" +
                            "optionSort  = {6}"
                            , Environment.NewLine, input.itemName, input.itemCode, input.shortName, 0, false, 0);
                    _lkItemRepo.Insert(createMsItem);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsItem() - End update Item.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                                            .SelectMany(x => x.ValidationErrors)
                                            .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.ErrorFormat("CreateMsItem() ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("CreateMsItem() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsItem() ERROR DbException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("CreateMsItem() ERROR. Result = {0}", "Item Code or Item Name or Short Name Already Exist!");
                throw new UserFriendlyException("Item Code or Item Name or Short Name Already Exist!");
            }
            Logger.Info("CreateMsItem() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Item_Edit)]
        public void UpdateMsItem(UpdateMsItemInput input)
        {
            Logger.Info("UpdateMsItem() - Started.");

            Logger.DebugFormat("UpdateMsItem() - Start checking exiting code, name, and shortName. Params sent:{0}" +
                "itemCode   ={1}{0}" +
                "itemName   ={2}{0}" +
                "shortName  ={3}{0}" +
                "itemID     ={4}"
                , Environment.NewLine, input.itemCode, input.itemName, input.shortName, input.Id);
            var checkCodeName = (from x in _lkItemRepo.GetAll()
                                 where x.Id != input.Id && (x.itemCode == input.itemCode || x.itemName == input.itemName || x.shortName == input.shortName)
                                 select x).Any();
            Logger.DebugFormat("UpdateMsItem() - End checking exiting code, name, and shortName. Result:{0}", checkCodeName);

            if (!checkCodeName)
            {
                var getMsItem = (from x in _lkItemRepo.GetAll()
                                 where x.Id == input.Id
                                 select x).FirstOrDefault();

                var updateMsItem = getMsItem.MapTo<LK_Item>();

                updateMsItem.itemName = input.itemName;
                updateMsItem.itemCode = input.itemCode;
                updateMsItem.shortName = input.shortName;


                try
                {
                    Logger.DebugFormat("UpdateMsItem() - Start updatae Item. Params sent:{0}" +
                    "itemCode   = {1}{0}" +
                    "itemName   = {2}{0}" +
                    "shortName  = {3}"
                    , Environment.NewLine, input.itemCode, input.itemName, input.shortName);
                    _lkItemRepo.Update(updateMsItem);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateMsItem() - End update item.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                                            .SelectMany(x => x.ValidationErrors)
                                            .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.ErrorFormat("UpdateMsItem() ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateMsItem() ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsItem() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("CreateOrUpdateMsSchemaRequirement() ERROR. Result = {0}", "Item Code or Item Name or Short Name Already Exist!");
                throw new UserFriendlyException("Item Code or Item Name or Short Name Already Exist!");
            }
            Logger.Info("UpdateMsItem() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Item_Delete)]
        public void DeleteMsItem(int Id)
        {
            Logger.Info("DeleteMsItem() - Finished.");

            Logger.DebugFormat("DeleteMsItem() - Start checking data. Params sent:{0}", Id);
            bool checkUnitItem = (from unitItem in _msUnitItemRepo.GetAll()
                                  where unitItem.itemID == Id
                                  select unitItem).Any();
            Logger.DebugFormat("DeleteMsItem() - End checking data. Result:{0}", checkUnitItem);
            if (!checkUnitItem)
            {

                try
                {
                    Logger.DebugFormat("DeleteMsItem() - Start delete item. Params sent:{0}", Id);
                    _lkItemRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("DeleteMsItem() - End checking data. Result:{0}", checkUnitItem);
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteMsItem() ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsItem() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsItem() ERROR. Result = {0}", "This Item is used!");
                throw new UserFriendlyException("This Item is used!");
            }
            Logger.Info("DeleteMsItem() - Finished.");
        }

        public ListResultDto<GetMsUnitItemDropdownListDto> GetMsUnitItemDropdown(List<int> unitID)
        {
            var getDataDropdown = (from A in _msUnitItemRepo.GetAll()
                                   join B in _lkItemRepo.GetAll() on A.itemID equals B.Id
                                   where unitID.Contains(A.unitID)
                                   select new GetMsUnitItemDropdownListDto
                                   {
                                       itemID = A.itemID,
                                       itemCode = B.itemCode,
                                       itemName = B.itemName
                                   }).Distinct().ToList();

            return new ListResultDto<GetMsUnitItemDropdownListDto>(getDataDropdown);
        }
    }
}
