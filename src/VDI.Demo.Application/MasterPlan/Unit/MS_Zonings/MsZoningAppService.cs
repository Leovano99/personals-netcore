using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Zonings.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Zonings
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterZoning)]
    public class MsZoningAppService : DemoAppServiceBase, IMsZoningAppService
    {
        private readonly IRepository<MS_Zoning> _msZoningRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;

        public MsZoningAppService
        (
            IRepository<MS_Zoning> msZoningRepo,
            IRepository<MS_Unit> msUnitRepo
        )
        {
            _msZoningRepo = msZoningRepo;
            _msUnitRepo = msUnitRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterZoning_Create)]
        public void CreateMsZoning(CreateMsZoningInputDto input)
        {
            Logger.InfoFormat("CreateMsZoning() - Started.");

            Logger.DebugFormat("CreateMsZoning() - Start checking existing zoningCode, zoningName. Parameters sent: {0} " +
                "zoningCode = {1}{0}" +
                "zoningName = {2}{0}"
                , Environment.NewLine, input.zoningCode, input.zoningName);
            bool checkZoning = (from zoning in _msZoningRepo.GetAll()
                                where zoning.zoningCode == input.zoningCode ||
                                      zoning.zoningName == input.zoningName
                                select zoning).Any();
            Logger.DebugFormat("CreateMsZoning() - End checking existing zoningCode, zoningName. Result = {0}", checkZoning);

            if (!checkZoning)
            {
                var createMsZoning = new MS_Zoning
                {
                    entityID = 1,
                    zoningCode = input.zoningCode,
                    zoningName = input.zoningName
                };

                try
                {
                    Logger.DebugFormat("CreateMsZoning() - Start insert msZoning. Parameters sent: {0} " +
                        "entityID = {1}{0}" +
                        "zoningCode = {2}{0}" +
                        "zoningName = {3}{0}", Environment.NewLine, 1, input.zoningCode, input.zoningName);
                    _msZoningRepo.Insert(createMsZoning);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsZoning() - End insert msZoning.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.DebugFormat("CreateMsZoning() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsZoning() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("CreateMsZoning() - ERROR. Result = {0}", "Zoning Code or Zoning Name Already Exist !");
                throw new UserFriendlyException("Zoning Code or Zoning Name Already Exist !");
            }
            Logger.InfoFormat("CreateMsZoning() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterZoning_Delete)]
        public void DeleteMsZoning(int Id)
        {
            Logger.InfoFormat("DeleteMsZoning() - Started.");

            Logger.DebugFormat("DeleteMsZoning() - Start checking existing zoningID. Parameters sent: {0} " +
                "zoningID = {1}{0}", Environment.NewLine, Id);
            var checkUnit = (from x in _msUnitRepo.GetAll()
                             where x.zoningID == Id
                             select x.zoningID).Any();
            Logger.DebugFormat("DeleteMsZoning() - End checking existing entityCode, entityName. Result = {0}", checkUnit);
            if (!checkUnit)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsZoning() - Start delete zoning. Parameters sent: {0} " +
                        "zoningID = {1}{0}", Environment.NewLine, Id);
                    _msZoningRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsZoning() - End delete zoning");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.DebugFormat("DeleteMsZoning() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("DeleteMsZoning() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("DeleteMsZoning() - ERROR. Result = {0}", "This zoning is used!");
                throw new UserFriendlyException("This zoning is used!");
            }
            Logger.InfoFormat("DeleteMsZoning() - Finished.");
        }

        public ListResultDto<GetAllMsZoningListDto> GetAllMsZoning()
        {
            var getZoning = (from zoning in _msZoningRepo.GetAll()
                             orderby zoning.Id descending
                             select new GetAllMsZoningListDto
                             {
                                 zoningID = zoning.Id,
                                 zoningCode = zoning.zoningCode,
                                 zoningName = zoning.zoningName
                             }).ToList();

            return new ListResultDto<GetAllMsZoningListDto>(getZoning);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterZoning_Edit)]
        public void UpdateMsZoning(UpdateMsZoningInputDto input)
        {
            Logger.InfoFormat("UpdateMsZoning() - Started.");

            Logger.DebugFormat("UpdateMsZoning() - Start checking existing zoningCode, zoningName. Parameters sent: {0} " +
                "zoningCode = {1}{0}" +
                "zoningName = {2}{0}"
                , Environment.NewLine, input.zoningCode, input.zoningName);
            bool checkZoning = (from zoning in _msZoningRepo.GetAll()
                                where (zoning.zoningCode == input.zoningCode ||
                                zoning.zoningName == input.zoningName)
                                && zoning.Id != input.zoningID
                                select zoning).Any();
            Logger.DebugFormat("UpdateMsZoning() - End checking existing zoningCode, zoningName. Result = {0}", checkZoning);

            if (!checkZoning)
            {
                Logger.DebugFormat("UpdateMsZoning() - Start get zoning for update. Parameters sent: {0} " +
                    "zoningId = {1}{0}", Environment.NewLine, input.zoningID);
                var getMsZoning = (from A in _msZoningRepo.GetAll()
                                   where A.Id == input.zoningID
                                   select A).FirstOrDefault();
                Logger.DebugFormat("UpdateMsZoning() - End get zoning for update. Result = {0} ", getMsZoning);

                var updateMsZoning = getMsZoning.MapTo<MS_Zoning>();

                updateMsZoning.zoningName = input.zoningName;
                updateMsZoning.zoningCode = input.zoningCode;

                try
                {
                    Logger.DebugFormat("UpdateMsZoning() - Start update MsZoning. Parameters sent: {0} " +
                        "zoningCode = {1}{0}" +
                        "zoningName = {2}{0}"
                        , Environment.NewLine, input.zoningCode, input.zoningName);
                    _msZoningRepo.Update(updateMsZoning);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateMsZoning() - End update MsZoning.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.DebugFormat("UpdateMsZoning() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsZoning() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsZoning() - ERROR. Result = {0}", "Zoning Code or Zoning Name Already Exist !");
                throw new UserFriendlyException("Zoning Code or Zoning Name Already Exist !");
            }
            Logger.InfoFormat("UpdateMsZoning() - Finished.");
        }
    }
}
