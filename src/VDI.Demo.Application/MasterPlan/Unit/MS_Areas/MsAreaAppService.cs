using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Areas.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Areas
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterArea)]
    public class MsAreaAppService : DemoAppServiceBase, IMsAreaAppService
    {
        private readonly IRepository<MS_Area> _msAreaRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_Territory> _msTerritoryRepo;
        private readonly IRepository<MS_County> _msCountyRepo;
        private readonly IRepository<MS_City> _msCityRepo;

        public MsAreaAppService(
            IRepository<MS_Area> msAreaRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_Territory> msTerritoryRepo,
            IRepository<MS_County> msCountyRepo,
            IRepository<MS_City> msCityRepo
            )
        {
            _msAreaRepo = msAreaRepo;
            _msUnitRepo = msUnitRepo;
            _msTerritoryRepo = msTerritoryRepo;
            _msCountyRepo = msCountyRepo;
            _msCityRepo = msCityRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterArea_Create)]
        public void CreateMsArea(CreateMsAreaInput input)
        {
            Logger.Info("CreateMsArea() Started.");

            Logger.DebugFormat("CreateMsArea() - Start checking existing code. Parameters sent:{0}" +
                            "areaCode      = {1}"
                            , Environment.NewLine, input.areaCode);

            var checkCode = (from area in _msAreaRepo.GetAll()
                             where area.areaCode == input.areaCode
                             select area.areaCode).Any();

            Logger.DebugFormat("CreateMsArea() - End checking existing code. Result:{0}", checkCode);

            if (!checkCode)
            {

                var data = new MS_Area
                {
                    entityID = 1,
                    areaCode = input.areaCode,
                    cityID = input.cityID,
                    regionName = input.regionName,
                };

                try
                {
                    Logger.DebugFormat("CreateMsArea() - Start insert Schema Area. Parameters sent:{0}" +
                            "entityID       = {1}{0}" +
                            "areaCode       = {2}{0}" +
                            "cityID  = {3}{0}" +
                            "regionName     = {4}{0}"
                            , Environment.NewLine, 1, input.areaCode, input.cityID, input.regionName);

                    _msAreaRepo.Insert(data);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("CreateMsArea() - Finish insert Schema Area.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.ErrorFormat("CreateMsArea() ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsArea() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsArea() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("CreateMsArea() ERROR. Result = {0}", "Area Code already exist!");
                throw new UserFriendlyException("Area Code already exist!");
            }
            Logger.Info("CreateMsArea() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterArea_Delete)]
        public void DeleteMsArea(int Id)
        {
            Logger.Info("DeleteMsArea() - Started.");

            Logger.DebugFormat("DeleteMsArea() - Start check unit with area Id: {0}", Id);
            var checkUnit = _msUnitRepo.GetAll().Where(x => x.areaID == Id).Any();
            Logger.DebugFormat("DeleteMsArea() - Finish check unit. Result: {0}", checkUnit);

            if (!checkUnit)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsArea() - Start delete area. Parameters sent: {0}", Id);
                    _msAreaRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("DeleteMsArea() - End delete area");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsArea() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsArea() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsArea() ERROR Exception. Result = {0}", "This Area is used in unit");
                throw new UserFriendlyException("This Area is used in unit");
            }

            Logger.Info("DeleteMsArea() - Finished.");

        }

        public ListResultDto<GetMsAreaListDto> GetAllMsArea()
        {
            var result = (from area in _msAreaRepo.GetAll()
                          join city in _msCityRepo.GetAll() on area.cityID equals city.Id
                          join county in _msCountyRepo.GetAll() on city.countyID equals county.Id
                          join territory in _msTerritoryRepo.GetAll() on county.territoryID equals territory.Id
                          orderby area.areaCode
                          select new GetMsAreaListDto
                          {
                              Id = area.Id,
                              areaCode = area.areaCode,
                              territoryID = territory.Id,
                              countyID = county.Id,
                              cityID = area.cityID,
                              territoryName = territory.territoryName,
                              countyName = county.countyName,
                              cityName = city.cityName,
                              regionName = area.regionName
                          }).ToList();

            return new ListResultDto<GetMsAreaListDto>(result);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterArea_Edit)]
        public void UpdateMsArea(UpdateMsAreaInput input)
        {
            Logger.Info("UpdateMsArea() Started.");

            Logger.DebugFormat("UpdateMsArea() - Start check unit with area Id: {0}", input.Id);
            var checkUnit = _msUnitRepo.GetAll().Where(x => x.areaID == input.Id).Any();
            Logger.DebugFormat("UpdateMsArea() - Finish check unit. Result: {0}", checkUnit);

            if (!checkUnit)
            {

                Logger.DebugFormat("UpdateMsArea() - Start checking existing code. Parameters sent:{0}" +
                            "areaCode       = {1}{0}" +
                            "areaID        = {2}{0}"
                            , Environment.NewLine, input.areaCode, input.Id);
            var checkCode = (from area in _msAreaRepo.GetAll()
                             where area.areaCode == input.areaCode &&
                             (area.Id != input.Id)
                             select area.areaCode).Any();
            Logger.DebugFormat("UpdateMsArea() - End checking existing code. Result:{0}", checkCode);

            if (!checkCode)
            {
                var getArea = (from area in _msAreaRepo.GetAll()
                               where area.Id == input.Id
                               select area).FirstOrDefault();

                var data = getArea.MapTo<MS_Area>();
                data.areaCode = input.areaCode;
                data.cityID = input.cityID;
                data.regionName = input.regionName;

                try
                {
                    Logger.DebugFormat("UpdateMsArea() - Start update Area. Parameters sent:{0}" +
                            "areaCode       = {1}{0}" +
                            "cityID  = {2}{0}" +
                            "regionName     = {3}"
                            , Environment.NewLine, data.areaCode, data.cityID, data.regionName);
                    _msAreaRepo.Update(data);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsArea() - End update area.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.ErrorFormat("UpdateMsArea() ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsArea() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsArea() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsArea() ERROR. Result = {0}", "Area Code already exist!");
                throw new UserFriendlyException("Area Code already exist!");
            }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsArea() ERROR. Result = {0}", "This Area is used in unit!");
                throw new UserFriendlyException("This Area is used in unit!");
            }
            Logger.Info("UpdateMsArea() Finished.");
        }
    }
}
