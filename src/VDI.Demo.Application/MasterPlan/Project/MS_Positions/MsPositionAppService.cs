using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Project.MS_Positions.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.MasterPlan.Project.MS_Positions
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPosition)]
    public class MsPositionAppService : DemoAppServiceBase, IMsPositionAppService
    {
        private readonly IRepository<MS_Officer> _msOfficerRepo;
        private readonly IRepository<MS_Position> _msPositionRepo;
        private readonly IRepository<MS_Department> _msDepartmentRepo;

        public MsPositionAppService
        (
            IRepository<MS_Officer> msOfficerRepo,
            IRepository<MS_Position> msPositionRepo,
            IRepository<MS_Department> msDepartmentRepo
        )
        {
            _msOfficerRepo = msOfficerRepo;
            _msPositionRepo = msPositionRepo;
            _msDepartmentRepo = msDepartmentRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPosition_Create)]
        public void CreateMsPosition(MsPositionInput input)
        {
            Logger.Info("CreateMsPosition() - Started.");

            Logger.DebugFormat("CreateMsPosition() - Start checking existing code and name. Params sent:{0}" +
                "departmentID   = {1}{0}" +
                "positionCode   = {2}{0}" +
                "positionName   = {3}"
                , Environment.NewLine, input.departmentID, input.positionCode, input.positionName);
            var checkPositionCode = (from x in _msPositionRepo.GetAll()
                                     where x.departmentID == input.departmentID && (x.positionCode == input.positionCode || x.positionName == input.positionName)
                                     select x).Any();
            Logger.DebugFormat("CreateMsPosition() - End checking existing code and name. Result: {0}", checkPositionCode);

            if (!checkPositionCode)
            {
                var data = new MS_Position
                {
                    positionName = input.positionName,
                    positionCode = input.positionCode,
                    departmentID = input.departmentID,
                    isActive = input.isActive
                };

                try
                {
                    Logger.DebugFormat("CreateMsPosition() - Start insert position. Params sent:{0}" +
                    "positionName = {1}{0}" +
                    "positionCode = {2}{0}" +
                    "departmentID = {3}{0}" +
                    "departmentID = {4}"
                    , Environment.NewLine, input.positionName, input.positionCode, input.departmentID, input.isActive);
                    _msPositionRepo.Insert(data);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsPosition() - End insert position.");

                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsPosition() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsPosition() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("CreateMsPosition() ERROR. Result = {0}", "Position Code or Position Name Already Exist in This Department!");
                throw new UserFriendlyException("Position Code or Position Name Already Exist in This Department!");
            }
            Logger.Info("CreateMsPosition() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPosition_Delete)]
        public void DeleteMsPosition(int Id)
        {
            Logger.Info("DeleteMsPosition() Started.");

            Logger.DebugFormat("DeleteMsPosition() - Start checking data officer with positionID: {0}", Id);
            bool checkOfficer = _msOfficerRepo.GetAll().Where(x => x.positionID == Id).Any();
            Logger.DebugFormat("DeleteMsPosition() - End checking data officer. Result: {0}", checkOfficer);

            if (!checkOfficer)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsPosition() - Start delete position. Params sent: {0}", Id);
                    _msPositionRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("DeleteMsPosition() - End delete position");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsPosition() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsPosition() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsPosition() ERROR. Result = {0}", "This Position is used by another master!");
                throw new UserFriendlyException("This Position is used by another master!");
            }
            Logger.Info("DeleteMsPosition() Finished.");
        }

        public ListResultDto<GetAllMsPositionListDto> GetAllMsPosition()
        {
            var listMsPosition = (from x in _msPositionRepo.GetAll()
                                  join y in _msDepartmentRepo.GetAll()
                                  on x.departmentID equals y.Id
                                  orderby x.Id descending
                                  select new GetAllMsPositionListDto
                                  {
                                      Id = x.Id,
                                      positionName = x.positionName,
                                      positionCode = x.positionCode,
                                      departmentID = x.departmentID,
                                      departmentName = y.departmentName,
                                      isActive = x.isActive
                                  }).ToList();

            return new ListResultDto<GetAllMsPositionListDto>(listMsPosition);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterOfficer_Create, AppPermissions.Pages_Tenant_MasterOfficer_Edit)]
        public ListResultDto<GetMsPositionByDepartmentListDto> GetMsPositionByDepartment(int departmentID)
        {
            var listMsPosition = (from x in _msPositionRepo.GetAll()
                                  where x.departmentID == departmentID
                                  where x.isActive
                                  orderby x.positionName ascending
                                  select new GetMsPositionByDepartmentListDto
                                  {
                                      Id = x.Id,
                                      positionName = x.positionName
                                  }).ToList();

            return new ListResultDto<GetMsPositionByDepartmentListDto>(listMsPosition);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPosition_Edit)]
        public JObject UpdateMsPosition(MsPositionInput input)
        {
            Logger.Info("UpdateMsPosition() Started.");

            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMsPosition() - Start checking exiting code and name. Params sent:{0}" +
                "departmentID   = {1}{0}" +
                "postionId      = {2}{0}" +
                "positionCode   = {3}{0}" +
                "positionName   = {4}"
                , Environment.NewLine, input.departmentID, input.Id, input.positionCode, input.positionName);
            var checkPositionCode = (from A in _msPositionRepo.GetAll()
                                     where A.departmentID == input.departmentID && A.Id != input.Id && (A.positionCode == input.positionCode || A.positionName == input.positionName)
                                     select A).Any();
            Logger.DebugFormat("UpdateMsPosition() - End checking exiting code and name. Result: {0}", checkPositionCode);

            Logger.DebugFormat("UpdateMsPosition() - Start checking MS_Officer.");
            var checkOfficer = (from A in _msOfficerRepo.GetAll()
                                where A.positionID == input.Id
                                select A).Any();
            Logger.DebugFormat("UpdateMsPosition() - End checking MS_Officer. Result: {0}", checkOfficer);

            if (!checkPositionCode)
            {
                var getMsPosition = (from a in _msPositionRepo.GetAll()
                                     where a.Id == input.Id
                                     select a).FirstOrDefault();

                var updateMsPosition = getMsPosition.MapTo<MS_Position>();

                updateMsPosition.departmentID = input.departmentID;
                updateMsPosition.isActive = input.isActive;

                if (!checkOfficer)
                {
                    updateMsPosition.positionName = input.positionName;
                    updateMsPosition.positionCode = input.positionCode;

                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change Position Name & Code");
                }

                try
                {
                    Logger.DebugFormat("UpdateMsPosition() - Start update position. Params sent:{0}" +
                    "positionName   = {1}{0}" +
                    "positionCode   = {2}{0}" +
                    "departmentID   = {3}{0}" +
                    "isActive       = {4}"
                    , Environment.NewLine, input.positionName, input.positionCode, input.departmentID, input.isActive);
                    _msPositionRepo.Update(updateMsPosition);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("UpdateMsPosition() - End update position.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsPosition() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsPosition() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsPosition() ERROR. Result = {0}", "Position Code or Position Name Already Exist !");
                throw new UserFriendlyException("Position Code or Position Name Already Exist !");
            }

            Logger.Info("UpdateMsPosition() Finished.");
            return obj;
        }
    }
}
