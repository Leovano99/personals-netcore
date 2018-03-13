using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Categories.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Categories
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCategory)]
    public class MsCategoryAppService : DemoAppServiceBase, IMsCategoryAppService
    {
        private readonly IRepository<MS_Category> _msCategoryRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;

        public MsCategoryAppService(
            IRepository<MS_Category> msCategoryRepo,
            IRepository<MS_Unit> msUnitRepo
        )
        {
            _msCategoryRepo = msCategoryRepo;
            _msUnitRepo = msUnitRepo;
        }


        public ListResultDto<GetAllCategoryListDto> GetAllMsCategory()
        {
            var getAllData = (from A in _msCategoryRepo.GetAll()
                              orderby A.CreationTime descending
                              select new GetAllCategoryListDto
                              {
                                  categoryID = A.Id,
                                  categoryName = A.categoryName,
                                  categoryCode = A.categoryCode
                              }).ToList();

            try
            {
                return new ListResultDto<GetAllCategoryListDto>(getAllData);
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

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCategory_Create)]
        public void CreateMsCategory(CreateCategoryInputDto input)
        {
            Logger.Info("CreateMsCategory() - Started.");
            Logger.DebugFormat("CreateMsCategory() - Start checking before insert Category. Parameters sent:{0}" +
                        "categoryCode = {1}{0}" +
                        "categoryName = {2}{0}"
                        , Environment.NewLine, input.categoryCode, input.categoryName);

            bool checkCategory = (from A in _msCategoryRepo.GetAll()
                                  where A.categoryCode == input.categoryCode ||
                                  A.categoryName == input.categoryName
                                  select A).Any();

            Logger.DebugFormat("CreateMsCategory() - Ended checking before insert Category. Result = {0}", checkCategory);

            if (!checkCategory)
            {
                var createMsCategory = new MS_Category
                {
                    categoryName = input.categoryName,
                    categoryCode = input.categoryCode,
                    projectField = "-", //hardcode for not null field
                    areaField = "-", //hardcode for not null field
                    categoryField = "-", //hardcode for not null field
                    clusterField = "-", //hardcode for not null field
                    productField = "-", //hardcode for not null field
                    detailField = "-", //hardcode for not null field
                    zoningField = "-", //hardcode for not null field
                    facingField = "-", //hardcode for not null field
                    roadField = "-", //hardcode for not null field
                    kavNoField = "-"
                };


                try
                {
                    Logger.DebugFormat("CreateMsCategory() - Start insert Category. Parameters sent:{0}" +
                        "categoryName = {1}{0}" +
                        "categoryCode = {2}{0}" +
                        "projectField = {3}{0}" +
                        "areaField = {4}{0}" +
                        "categoryField = {5}{0}" +
                        "clusterField = {6}{0}" +
                        "productField = {7}{0}" +
                        "detailField = {8}{0}" +
                        "zoningField = {9}{0}" +
                        "facingField = {10}{0}" +
                        "roadField = {11}{0}" +
                        "kavNoField = {12}{0}"
                        , Environment.NewLine, input.categoryName, input.categoryCode, "-", "-", "-", "-", "-"
                        , "-", "-", "-", "-", "-");

                    _msCategoryRepo.Insert(createMsCategory);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("CreateMsCategory() - Ended insert Category.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.ErrorFormat("CreateMsCategory() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsCategory() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsCategory() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("CreateMsCategory() - ERROR Exception.", "Category Code or Category Name Already Exist !");
                throw new UserFriendlyException("Category Code or Category Name Already Exist !");
            }
            Logger.Info("CreateMsCategory() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCategory_Edit)]
        public void UpdateMsCategory(GetAllCategoryListDto input)
        {
            Logger.Info("UpdateMsCategory() - Started.");
            Logger.DebugFormat("UpdateMsCategory() - Start checking before update Category. Parameters sent:{0}" +
                        "categoryID = {1}{0}" +
                        "categoryCode = {2}{0}" +
                        "categoryName = {3}{0}"
                        , Environment.NewLine, input.categoryID, input.categoryCode, input.categoryName);

            bool checkCategory = (from A in _msCategoryRepo.GetAll()
                                  where A.Id != input.categoryID && A.categoryCode == input.categoryCode &&
                                  (A.categoryName == input.categoryName)
                                  select A).Any();

            Logger.DebugFormat("UpdateMsCategory() - Ended checking before update Category. Result = {0}", checkCategory);

            if (!checkCategory)
            {
                Logger.DebugFormat("UpdateMsCategory() - Start get data before update Category. Parameters sent:{0}" +
                        "categoryID = {1}{0}"
                        , Environment.NewLine, input.categoryID);

                var getMsCategory = (from A in _msCategoryRepo.GetAll()
                                     where A.Id == input.categoryID
                                     select A).FirstOrDefault();

                Logger.DebugFormat("UpdateMsCategory() - Ended get data before update Category.");

                var updateMsCategory = getMsCategory.MapTo<MS_Category>();

                updateMsCategory.categoryName = input.categoryName;
                updateMsCategory.categoryCode = input.categoryCode;


                try
                {
                    Logger.DebugFormat("UpdateMsCategory() - Start update Category. Parameters sent:{0}" +
                        "categoryName = {1}{0}" +
                        "categoryCode = {2}{0}"
                        , Environment.NewLine, input.categoryName, input.categoryCode);

                    _msCategoryRepo.Update(updateMsCategory);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateMsCategory() - Ended update Category.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.ErrorFormat("UpdateMsCategory() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsCategory() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsCategory() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                Logger.ErrorFormat("UpdateMsCategory() - ERROR Result = {0}.", "Category Code or Category Name Already Exist !");
                throw new UserFriendlyException("Category Code or Category Name Already Exist !");
            }
            Logger.Info("UpdateMsCategory() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProduct, AppPermissions.Pages_Tenant_GenerateUnit)]
        public ListResultDto<GetCategoryDropdownListDto> GetMsCategoryDropdown()
        {
            var getData = (from A in _msCategoryRepo.GetAll()
                           orderby A.CreationTime descending
                           select new GetCategoryDropdownListDto
                           {
                               categoryID = A.Id,
                               categoryName = A.categoryName
                           }).ToList();



            try
            {
                return new ListResultDto<GetCategoryDropdownListDto>(getData);
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

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCategory_Delete)]
        public void DeleteMsCategory(int Id)
        {
            Logger.Info("DeleteMsCategory() - Started.");
            Logger.DebugFormat("DeleteMsCategory() - Start checking before delete Category. Parameters sent:{0}" +
                        "categoryID = {1}{0}"
                        , Environment.NewLine, Id);

            var checkUnit = (from A in _msUnitRepo.GetAll()
                             where A.categoryID == Id
                             select A.Id).Any();

            Logger.DebugFormat("DeleteMsCategory() - Ended checking before delete Category. Result = {0}", checkUnit);

            if (!checkUnit)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsCategory() - Start delete Category. Parameters sent:{0}" +
                        "categoryID = {1}{0}"
                        , Environment.NewLine, Id);

                    _msCategoryRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsCategory() - Ended delete Category.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsCategory() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsCategory() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsCategory() - ERROR Result = {0}.", "This category is used!");
                throw new UserFriendlyException("This category is used!");
            }
            Logger.Info("DeleteMsCategory() - Finished.");
        }

        public ListResultDto<GetCategoryDropdownListDto> GetMsCategoryDropdownByProjectCluster(int projectID, int clusterID)
        {
            var result = (from unit in _msUnitRepo.GetAll()
                          join cat in _msCategoryRepo.GetAll() on unit.categoryID equals cat.Id
                          where unit.projectID == projectID && unit.clusterID == clusterID
                          select new GetCategoryDropdownListDto
                          {
                              categoryID = cat.Id,
                              categoryName = cat.categoryName
                          })
                          .Distinct()
                          .ToList();

            return new ListResultDto<GetCategoryDropdownListDto>(result);
        }
    }
}
