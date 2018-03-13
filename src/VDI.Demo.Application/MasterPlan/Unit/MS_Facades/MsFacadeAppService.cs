using System;
using System.Linq;
using System.Data;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Facades.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace VDI.Demo.MasterPlan.Unit.MS_Facades
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFacade)]
    public class MsFacadeAppService : DemoAppServiceBase, IMsFacadeAppService
    {
        private readonly IRepository<MS_Facade> _msFacadeRepo;

        public MsFacadeAppService(IRepository<MS_Facade> msFacadeRepo)
        {
            _msFacadeRepo = msFacadeRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFacade_Create)]
        public void CreateMsFacade(CreateMsFacadeInput input)
        {
            Logger.InfoFormat("CreateMsFacade() - Started.");

            Logger.DebugFormat("CreateMsFacade() - Start checking existing facadeCode. Parameters sent: {0} " +
                "facadeCode = {1}{0}", Environment.NewLine, input.facadeCode);
            var checkCode = (from facade in _msFacadeRepo.GetAll()
                             where facade.facadeCode == input.facadeCode
                             select facade).Any();
            Logger.DebugFormat("CreateMsFacade() - End checking existing facadeCode. Result = {0}", checkCode);

            if (!checkCode)
            {
                var data = new MS_Facade
                {
                    entityID = 1,
                    facadeCode = input.facadeCode,
                    facadeName = input.facadeName
                };
                try
                {
                    Logger.DebugFormat("CreateMsFacade() - Start delete Facade. Parameters sent: {0} " +
                        "entityID = {1}{0}" +
                        "facadeCode = {2}{0}" +
                        "facadeName = {3}{0}", Environment.NewLine, 1, input.facadeCode, input.facadeName);
                    _msFacadeRepo.Insert(data);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsFacade() - End delete Facade");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.DebugFormat("CreateMsFacade() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.DebugFormat("CreateMsFacade() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("CreateMsFacade() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("CreateMsFacade() - ERROR. Result = {0}", "Facade Code already exist!");
                throw new UserFriendlyException("Facade Code already exist!");
            }
            Logger.InfoFormat("CreateMsFacade() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFacade_Delete)]
        public void DeleteMsFacade(int Id)
        {
            Logger.InfoFormat("DeleteMsFacade() - Started.");
            try
            {
                Logger.DebugFormat("DeleteMsFacade() - Start Delete msFacade. Parameters sent: {0} " +
                    "facadeID = {1}{0}", Environment.NewLine, Id);
                _msFacadeRepo.Delete(Id);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                Logger.DebugFormat("DeleteMsFacade() - End Delete msFacade.");
            }
            /*catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);
                string fullErrorMessage = string.Join("; ", errorMessages);
                string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                Logger.DebugFormat("DeleteMsFacade() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                throw new UserFriendlyException(exceptionMessage);
            }*/
            catch (DataException ex)
            {
                Logger.DebugFormat("DeleteMsFacade() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("DeleteMsFacade() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.InfoFormat("DeleteMsFacade() - Finished.");
        }

        public ListResultDto<GetMsFacadeListDto> GetAllMsFacade()
        {
            var result = (from facade in _msFacadeRepo.GetAll()
                          select new GetMsFacadeListDto
                          {
                              Id = facade.Id,
                              facadeCode = facade.facadeCode,
                              facadeName = facade.facadeName
                          }).ToList();

            return new ListResultDto<GetMsFacadeListDto>(result);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterFacade_Edit)]
        public void UpdateMsFacade(UpdateMsFacadeInput input)
        {
            Logger.InfoFormat("UpdateMsFacade() - Started.");

            Logger.DebugFormat("UpdateMsFacade() - Start checking existing facadeCode. Parameters sent: {0} " +
                "facadeCode = {1}{0}" +
                "Id = {2}{0}", Environment.NewLine, input.facadeCode, input.Id);
            var checkCode = (from facade in _msFacadeRepo.GetAll()
                             where facade.facadeCode == input.facadeCode &&
                             (facade.Id != input.Id)
                             select facade).Any();
            Logger.DebugFormat("UpdateMsFacade() - End checking existing facadeCode. Result = {0}", checkCode);

            if (!checkCode)
            {
                Logger.DebugFormat("UpdateMsFacade() - Start get data face for update. Parameters sent: {0} " +
                    "facadeID = {1}{0}", Environment.NewLine, input.Id);
                var getFacade = (from facade in _msFacadeRepo.GetAll()
                                 where facade.Id == input.Id
                                 select facade).FirstOrDefault();
                Logger.DebugFormat("UpdateMsFacade() - End get data face for update. Result = {0}", getFacade);

                var data = getFacade.MapTo<MS_Facade>();

                data.entityID = 1;
                data.facadeCode = input.facadeCode;
                data.facadeName = input.facadeName;
                try
                {
                    Logger.DebugFormat("UpdateMsFacade() - Start Update msFacade. Parameters sent: {0} " +
                       "entityID = {1}{0}" +
                       "facadeCode = {2}{0}" +
                       "facadeName = {3}{0}"
                       , Environment.NewLine, 1, input.facadeCode, input.facadeName);
                    _msFacadeRepo.Update(data);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsFacade() - End Update msFacade.");
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    Logger.DebugFormat("UpdateMsFacade() - ERROR DbEntityValidationException. Result = {0}", exceptionMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                catch (DataException ex)
                {
                    Logger.DebugFormat("UpdateMsFacade() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.DebugFormat("UpdateMsFacade() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.DebugFormat("UpdateMsFacade() - ERROR. Result = {0}", "Facade Code already exist!");
                throw new UserFriendlyException("Facade Code already exist!");
            }
            Logger.InfoFormat("UpdateMsFacade() - Finished.");
        }
    }
}
