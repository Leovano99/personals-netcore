using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Products.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Products
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProduct)]
    public class MsProductAppService : DemoAppServiceBase, IMsProductAppService
    {
        private readonly IRepository<MS_Product> _msProductRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;

        public MsProductAppService
        (
            IRepository<MS_Product> msProductRepo,
            IRepository<MS_Unit> msUnitRepo
        )
        {
            _msProductRepo = msProductRepo;
            _msUnitRepo = msUnitRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GenerateUnit)]
        public ListResultDto<GetAllProductListDto> GetAllMsProduct()
        {
            var getProduct = (from product in _msProductRepo.GetAll()
                              orderby product.CreationTime descending
                              select new GetAllProductListDto
                              {
                                  Id = product.Id,
                                  productCode = product.productCode,
                                  productName = product.productName
                              }).ToList();
            return new ListResultDto<GetAllProductListDto>(getProduct);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProduct_Create)]
        public void CreateMsProduct(CreateMsProductDto input)
        {
            Logger.InfoFormat("CreateMsProduct() Started.");

            Logger.DebugFormat("CreateMsProduct() - Start checking existing productCode. Parameters sent: {0} " +
                "   productCode = {1}{0}"
                , Environment.NewLine, input.productCode);
            bool checkProductCode = (from product in _msProductRepo.GetAll()
                                     where product.productCode == input.productCode
                                     select product).Any();
            Logger.DebugFormat("CreateMsProduct() - End checking existing productCode. Result = {0}", checkProductCode);

            if (!checkProductCode)
            {
                var createMsProduct = new MS_Product
                {
                    entityID = 1,
                    productCode = input.productCode,
                    productName = input.productName,
                    sortNo = 1 //hardcode for not null field
                };

                try
                {
                    Logger.DebugFormat("CreateMsProduct() - Start update Product. Parameters sent: {0} " +
                "   entityID = {1}{0}" +
                "   productCode = {2}{0}" +
                "   productName = {3}{0}" +
                "   sortNo = {4}{0}"
                , Environment.NewLine, 1, input.productCode, input.productName, 1);
                    _msProductRepo.Insert(createMsProduct);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsProduct() - End update Product.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.DebugFormat("CreateMsProduct() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.DebugFormat("CreateMsProduct() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsProduct() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("CreateMsProduct() - ERROR. Result = {0}", "Product Code Already Exist In This Category!");
                throw new UserFriendlyException("Product Code Already Exist In This Category!");
            }

            Logger.InfoFormat("CreateMsProduct() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProduct_Delete)]
        public void DeleteMsProduct(int Id)
        {
            Logger.InfoFormat("DeleteMsProduct() - Started.");

            Logger.DebugFormat("DeleteMsProduct() - Start checking existing productID. Parameters sent: {0} " +
        "   productID = {1}{0}", Environment.NewLine, Id);
            var checkUnit = (from x in _msUnitRepo.GetAll()
                             where x.productID == Id
                             select x.productID).Any();
            Logger.DebugFormat("DeleteMsProduct() - End checking existing productID.");

            if (!checkUnit)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsProduct() - Start delete Product. Parameters sent: {0} " +
                "   productID = {1}{0}", Environment.NewLine, Id);
                    _msProductRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("DeleteMsProduct() - End delete Product.");
                }
                catch (DataException ex)
                {
                    Logger.DebugFormat("DeleteMsProduct() - ERROR DataException. Result = {0}", ex.Message);

                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("DeleteMsProduct() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("DeleteMsProduct() - ERROR. Result = {0}", "This Product is used by another master!");
                throw new UserFriendlyException("This Product is used by another master!");
            }

            Logger.InfoFormat("DeleteMsProduct() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProduct_Edit)]
        public void UpdateMsProduct(UpdateMsProductDto input)
        {
            Logger.InfoFormat("UpdateMsProduct() - Started.");

            Logger.DebugFormat("UpdateMsProduct() - Start checking existing productCode. Parameters sent: {0} " +
                "   productCode = {1}{0}"
                , Environment.NewLine, true, input.productCode);
            bool checkCodeName = (from x in _msProductRepo.GetAll()
                                  where x.productCode == input.productCode && x.Id != input.Id
                                  select x).Any();
            Logger.DebugFormat("UpdateMsProduct() - End checking existing productCode. Result = {0}", checkCodeName);

            if (!checkCodeName)
            {
                Logger.DebugFormat("UpdateMsProduct() - Start get data Product for update. Parameters sent: {0} " +
                        "   productID = {1}{0}"
                        , Environment.NewLine, input.Id);
                var getMsProduct = (from x in _msProductRepo.GetAll()
                                    where x.Id == input.Id
                                    select x).FirstOrDefault();

                var updateMsProduct = getMsProduct.MapTo<MS_Product>();
                Logger.DebugFormat("UpdateMsProduct() - End get data Product  for update. Result = {0}", updateMsProduct);


                updateMsProduct.productCode = input.productCode;
                updateMsProduct.productName = input.productName;

                try
                {
                    Logger.DebugFormat("UpdateMsProduct() - Start update Product. Parameters sent: {0} " +
                "   productCode = {1}{0}" +
                "   productName = {2}{0}"
                , Environment.NewLine, input.productCode, input.productName);
                    _msProductRepo.Update(updateMsProduct);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsProduct() - End update Product.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.DebugFormat("UpdateMsProduct() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.DebugFormat("UpdateMsProduct() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsProduct() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsProduct() - ERROR. Result = {0}", "Product Code Already Exist In This Category!");
                throw new UserFriendlyException("Product Code Already Exist In This Category!");
            }

            Logger.InfoFormat("UpdateMsProduct() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject, AppPermissions.Pages_Tenant_GenerateUnit)]
        public ListResultDto<GetProductDropdownListDto> GetMsProductDropdown()
        {
            var dropdown = (from x in _msProductRepo.GetAll()
                            orderby x.CreationTime descending
                            select new GetProductDropdownListDto
                            {
                                productID = x.Id,
                                productName = x.productName
                            }).ToList();

            return new ListResultDto<GetProductDropdownListDto>(dropdown);
        }

        public ListResultDto<GetProductDropdownListDto> GetMsProductDropdownByProjectClusterCategory(GetMsProductDropdownByProjectClusterCategoryInputDto input)
        {
            var result = (from unit in _msUnitRepo.GetAll()
                          join prod in _msProductRepo.GetAll() on unit.productID equals prod.Id
                          where unit.projectID == input.projectID && unit.clusterID == input.clusterID && unit.categoryID == input.categoryID
                          select new GetProductDropdownListDto
                          {
                              productID = prod.Id,
                              productName = prod.productName
                          })
                          .Distinct()
                          .ToList();

            return new ListResultDto<GetProductDropdownListDto>(result);
        }

    }
}
