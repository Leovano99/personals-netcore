using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.MasterPlan.Unit.MS_UnitCodes.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using Newtonsoft.Json.Linq;
using Abp.AutoMapper;
using System.Data;
using Abp.Authorization;
using VDI.Demo.Authorization;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitCodes
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterUnitCode)]
    public class MsUnitCodeAppService : DemoAppServiceBase, IMsUnitCodeAppService
    {
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;

        public MsUnitCodeAppService(
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<MS_Unit> msUnitRepo
            )
        {
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterUnitCode_Create)]
        public void CreateMsUnitCode(List<CreateOrUpdateMsUnitCodeInputDto> input)
        {
            Logger.Info("CreateMsUnitCode() Started.");

            foreach (var item in input)
            {
                Logger.DebugFormat("CreateMsUnitCode() - Start checking existing Unit Code. Parameters sent:{0}" +
                            "unitCode     = {1}{0}" +
                            "unitName     = {2}"
                            , Environment.NewLine, item.unitCode, item.unitName);

                var checkCode = (from x in _msUnitCodeRepo.GetAll()
                                 where x.unitCode == item.unitCode && x.projectID == item.projectID
                                 select x).Any();

                Logger.DebugFormat("CreateMsUnitCode() - End checking existing Unit Code. Result:{0}", checkCode);

                if (!checkCode)
                {
                    var data = new MS_UnitCode
                    {
                        entityID = 1,
                        unitCode = item.unitCode,
                        unitName = item.unitName,
                        projectID = item.projectID
                    };
                    Logger.DebugFormat("CreateMsUnitCode() - Start insert Unit Code. Parameters sent:{0}" +
                    "	entityID	= {1}{0}" +
                    "	unitCode	= {2}{0}" +
                    "	unitName	= {3}{0}" +
                    "   projectID   = {4][0]"
                    , Environment.NewLine, 1, item.unitCode, item.unitName, item.projectID);
                    try
                    {
                        _msUnitCodeRepo.Insert(data);
                    }
                    catch (DataException exDb)
                    {
                        Logger.ErrorFormat("CreateMsUnitCode() ERROR DbException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateMsUnitCode() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }

                    Logger.DebugFormat("CreateMsUnitCode() - End insert Detail.");
                }
                else
                {
                    Logger.ErrorFormat("CreateMsUnitCode() ERROR. Result = {0}", "Unit Code Already Exist !");
                    throw new UserFriendlyException("Unit Code Already Exist !");
                }
            }

            Logger.Info("CreateMsUnitCode() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterUnitCode_Delete)]
        public void DeleteMsUnitCode(int Id)
        {
            Logger.Info("DeleteMsUnitCode() Started.");

            Logger.DebugFormat("DeleteMsUnitCode() - Start checking Unit Code with Id: {0}.", Id);

            bool checkUnit = (from unit in _msUnitRepo.GetAll()
                              where unit.unitCodeID == Id
                              select unit.unitCodeID).Any();

            Logger.DebugFormat("DeleteMsUnitCode() - End checking Unit Code. Result: {0}.", checkUnit);

            if (!checkUnit)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsUnitCode() - Start delete Unit Code. Parameters sent: {0}", Id);
                    _msUnitCodeRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsUnitCode() - End delete Unit Code");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteMsUnitCode() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsUnitCode() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsUnitCode() ERROR Exception. Result = {0}", "This Unit Code is used by another master!");
                throw new UserFriendlyException("This Unit Code is used by another master!");
            }
            Logger.Info("DeleteMsUnitCode() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterUnitCode)]
        public ListResultDto<GetAllMsUnitCodeListDto> GetAllMsUnitCode(int projectID)
        {
            var result = (from x in _msUnitCodeRepo.GetAll()
                          where x.projectID == projectID
                          select new GetAllMsUnitCodeListDto
                          {
                              Id = x.Id,
                              unitCode = x.unitCode,
                              unitName = x.unitName
                          })
                          .ToList();

            return new ListResultDto<GetAllMsUnitCodeListDto>(result);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterUnitCode_Edit)]
        public JObject UpdateMsUnitCode(CreateOrUpdateMsUnitCodeInputDto input)
        {
            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMsUnitCode() - Start checking existing unit code. Parameters sent:{0}" +
                "unitCode     = {1}{0}" +
                "unitName     = {2}"
                , Environment.NewLine, input.unitCode, input.unitName);

            var checkCode = (from x in _msUnitCodeRepo.GetAll()
                             where x.Id != input.Id && x.unitCode == input.unitCode && x.projectID == input.projectID 
                             select x).Any();

            Logger.DebugFormat("UpdateMsUnitCode() - End checking existing unit code. Result: {0}", checkCode);

            var getMsUnitCode = (from x in _msUnitCodeRepo.GetAll()
                                 where x.Id == input.Id
                                 select x).FirstOrDefault();
            if (!checkCode)
            {
                var updateMsUnitCode = getMsUnitCode.MapTo<MS_UnitCode>();

                var checkUnit = (from unit in _msUnitRepo.GetAll()
                                 where unit.unitCodeID == input.Id
                                 select unit).Any();

                if (!checkUnit)
                {
                    updateMsUnitCode.unitCode = input.unitCode;
                    updateMsUnitCode.unitName = input.unitName;
                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change Unit Code & Name");
                }

                Logger.DebugFormat("UpdateMsUnitCode() - Start update Unit Code. Parameters sent:{0}" +
                    "	entityID	= {1}{0}" +
                    "	unitCode	= {2}{0}" +
                    "	unitName	= {3}{0}"
                , Environment.NewLine, 1, input.unitCode, input.unitName);

                try
                {
                    _msUnitCodeRepo.Update(updateMsUnitCode);
                }
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateMsUnitCode() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsUnitCode() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }

                Logger.DebugFormat("UpdateMsUnitCode() - End update Unit Code");
            }
            else
            {
                Logger.ErrorFormat("UpdateMsUnitCode() Unit Code already exist. Result = {0}", checkCode);
                throw new UserFriendlyException("Unit Code already exist!");
            }
            return obj;
        }
    }
}
