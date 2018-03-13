using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.Pricing.MS_Discounts.Dto;
using VDI.Demo.PropertySystemDB.Pricing;
using Abp.UI;
using System.Data.Common;
using System.Data;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using Abp.Extensions;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.Pricing.MS_Discounts
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDiscount)]
    public class MsDiscountAppService : DemoAppServiceBase, IMsDiscountAppService
    {
        private readonly IRepository<MS_Discount> _msDiscountRepo;
        private readonly IRepository<MS_TermAddDisc> _msTermAddDiscRepo;

        public MsDiscountAppService(
            IRepository<MS_Discount> msDiscountRepo,
            IRepository<MS_TermAddDisc> msTermAddDiscRepo
            )
        {
            _msDiscountRepo = msDiscountRepo;
            _msTermAddDiscRepo = msTermAddDiscRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDiscount_Create)]
        public int CreateMsDiscount(CreateMsDiscountInput input)
        {
            int discountId;
            Logger.Info("CreateMsDiscount() Started.");
            var checkDiscount = (from discount in _msDiscountRepo.GetAll()
                                 where discount.discountCode == input.discountCode ||
                                 discount.discountName == input.discountName
                                 select discount).Any();

            if (!checkDiscount)
            {
                var createMsDiscount = new MS_Discount
                {
                    discountCode = input.discountCode,
                    discountName = input.discountName,
                    isActive = input.isActive
                };

                //Input validation
                try
                {
                    //Below check used because there are still no max and min length definition in model
                    if (input.discountCode == null || input.discountName == null ||
                        input.discountCode.Length > 5 || input.discountName.Length > 100 || input.discountName.Length == 0
                        || input.discountCode.Length == 0)
                    {
                        throw new UserFriendlyException("Validation error");
                    }
                    discountId = _msDiscountRepo.InsertAndGetId(createMsDiscount);
                }
                catch (DbException ex)
                {
                    throw new UserFriendlyException("DB Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                throw new UserFriendlyException("Discount Code or Discount Name Already Exist!");
            }
            Logger.Info("CreateMsDiscount() Finished.");
            return discountId;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDiscount_Delete)]
        public void DeleteMsDiscount(int Id)
        {
            Logger.Info("DeleteMsDiscount() - Start.");

            Logger.DebugFormat("DeleteMsDiscount() - Start checking existing data disc. Params sent Id: {0}", Id);
            var checkTermAddDisc = (from x in _msTermAddDiscRepo.GetAll()
                                    where x.discountID == Id
                                    select x.discountID).Any();
            Logger.DebugFormat("DeleteMsDiscount() - End checking existing data disc. Result: {0}", checkTermAddDisc);

            if (!checkTermAddDisc)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsDiscount() - Start delete discount. Params sent Id: {0}", Id);
                    _msDiscountRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("DeleteMsDiscount() - End delete discount.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsDiscount() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsDiscount() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsDiscount() ERROR. Result = {0}", "This discount is used in transaction!");
                throw new UserFriendlyException("This discount is used in transaction!");
            }
            Logger.Info("DeleteMsDiscount() - Finished.");
        }

        public ListResultDto<GetAllMsDiscountListDto> GetAllMsDiscount()
        {
            var discount = (from x in _msDiscountRepo.GetAll()
                            select new GetAllMsDiscountListDto
                            {
                                discountID = x.Id,
                                discountCode = x.discountCode,
                                discountName = x.discountName,
                                isActive = x.isActive
                            }).ToList();

            return new ListResultDto<GetAllMsDiscountListDto>(discount);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDiscount_Edit)]
        public JObject UpdateMsDiscount(CreateMsDiscountInput input)
        {
            Logger.Info("UpdateMsDiscount() - Started.");

            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMsDiscount() - Start checking existing code and name. Params sent:{0}" +
                "discountCode   ={1}{0}" +
                "discountName   ={2}"
                , Environment.NewLine, input.discountCode, input.discountName);
            var checkDiscount = (from A in _msDiscountRepo.GetAll()
                                 where A.Id != input.discountID && (A.discountCode == input.discountCode || A.discountName == input.discountName)
                                 select A).Any();
            Logger.DebugFormat("UpdateMsDiscount() - End checking existing code and name. Result: {0}", checkDiscount);

            if (!checkDiscount)
            {
                Logger.DebugFormat("UpdateMsDiscount() - Start checking MS_TermAddDisc.");

                var checkUsedDiscount = (from A in _msTermAddDiscRepo.GetAll()
                                         where A.discountID == input.discountID
                                         select A).Any();

                Logger.DebugFormat("UpdateMsDiscount() - End checking MS_TermAddDisc. Result: {0}", checkUsedDiscount);

                var getMsDiscount = (from A in _msDiscountRepo.GetAll()
                                     where input.discountID == A.Id
                                     select A).FirstOrDefault();

                var updateMsDiscount = getMsDiscount.MapTo<MS_Discount>();

                updateMsDiscount.isActive = input.isActive;

                if (!checkUsedDiscount)
                {
                    updateMsDiscount.discountCode = input.discountCode;
                    updateMsDiscount.discountName = input.discountName;
                    obj.Add("message", "Edit Successfully");
                } else
                {
                    obj.Add("message", "Edit Successfully, but can't change Discount Code & Name");
                }

                try
                {
                    Logger.DebugFormat("UpdateMsDiscount() - Start update discount. Parameters sent:{0}" +
                            "discountName   = {1}{0}" +
                            "discountCode   = {2}{0}" +
                            "isActive       = {3}"
                            , Environment.NewLine, input.discountName, input.discountCode, input.isActive);

                    _msDiscountRepo.Update(updateMsDiscount);

                    Logger.DebugFormat("UpdateMsDiscount() - End update discount.");
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsDiscount() ERROR DbException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsDiscount() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsDiscount() ERROR DbException. Result = {0}", "Discount Code or Discount Name Already Exist !");
                throw new UserFriendlyException("Discount Code or Discount Name Already Exist !");
            }
            Logger.Info("UpdateMsDiscount() - Finished.");
            return obj;
        }
    }
}
