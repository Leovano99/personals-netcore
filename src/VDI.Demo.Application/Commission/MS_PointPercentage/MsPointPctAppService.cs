using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_PointPercentage.Dto;
using VDI.Demo.NewCommDB;
using Abp.AutoMapper;
using Abp.Application.Services.Dto;

namespace VDI.Demo.Commission.MS_PointPercentage
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPointPercent)]
    public class MsPointPctAppService : DemoAppServiceBase, IMsPointPctAppService
    {
        private readonly IRepository<MS_PointPct> _msPointPctRepo;
        private readonly IRepository<MS_Schema> _msSchemaRepo;
        private readonly IRepository<MS_StatusMember> _msStatusMemberRepo;
        private readonly IRepository<LK_PointType> _lkPointTypeRepo;

        public MsPointPctAppService(
            IRepository<MS_PointPct> msPointPctRepo,
            IRepository<MS_Schema> msSchemaRepo,
            IRepository<MS_StatusMember> msStatusMemberRepo,
            IRepository<LK_PointType> lkPointTypeRepo
        )
        {
            _msPointPctRepo = msPointPctRepo;
            _msSchemaRepo = msSchemaRepo;
            _msStatusMemberRepo = msStatusMemberRepo;
            _lkPointTypeRepo = lkPointTypeRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPointPercent_Create)]
        public void CreateMsPointPct(List<InputPointPctDto> input)
        {
            Logger.Info("CreateMsPointPct() - Started.");

            foreach (var item in input)
            {
                var createPointPct = new MS_PointPct
                {
                    schemaID = item.schemaID,
                    statusMemberID = item.statusMemberID,
                    pointTypeID = item.pointTypeID,
                    pointPct = item.pointPct,
                    pointKonvert = item.pointKonvert,
                    asUplineNo = item.asUplineNo,
                    isActive = item.isActive,
                    isComplete = true
                };

                try
                {
                    Logger.DebugFormat("CreateMsPointPct() - Start insert PointPct. Params sent:{0}" +
                    "schemaID       = {1}{0}" +
                    "statusMemberID = {2}{0}" +
                    "pointTypeID    = {3}{0}" +
                    "pointPct       = {4}{0}" +
                    "pointKonvert   = {5}{0}" +
                    "asUplineNo     = {6}{0}" +
                    "isActive       = {7}{0}" +
                    "isComplete     = {8}"
                    , Environment.NewLine, item.schemaID, item.statusMemberID, item.pointTypeID, item.pointPct, item.pointKonvert, item.asUplineNo, item.isActive, true);
                    _msPointPctRepo.Insert(createPointPct);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsPointPct() - End insert PointPct.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsDepartment() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsDepartment() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("CreateMsPointPct() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPointPercent_Delete)]
        public void DeleteMsPointPct(int Id)
        {
            Logger.Info("DeleteMsPointPct() - Started.");

            var getPointPct = (from pointPct in _msPointPctRepo.GetAll()
                               where Id == pointPct.Id
                               select pointPct).FirstOrDefault();

            var updatePointPct = getPointPct.MapTo<MS_PointPct>();

            updatePointPct.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsPointPct() - Start delete PointPct with ID: {0}", Id);
                _msPointPctRepo.Update(updatePointPct);
                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("DeleteMsPointPct() - End delete PointPct.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteMsPointPct() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteMsPointPct() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("DeleteMsPointPct() - Finished.");
        }


        public ListResultDto<GetAllPointPctListDto> GetMsPointPctBySchemaId(int schemaID)
        {
            var listResult = (from x in _msPointPctRepo.GetAll()
                              join schema in _msSchemaRepo.GetAll() on x.schemaID equals schema.Id
                              join statusMember in _msStatusMemberRepo.GetAll() on x.statusMemberID equals statusMember.Id
                              join lkPointType in _lkPointTypeRepo.GetAll() on x.pointTypeID equals lkPointType.Id
                              where x.schemaID == schemaID && x.isComplete == true
                              orderby x.Id descending
                              select new GetAllPointPctListDto
                              {
                                  Id = x.Id,
                                  statusName = statusMember.statusName,
                                  statusCode = statusMember.statusCode,
                                  statusMemberID = statusMember.Id,
                                  pointTypeCode = lkPointType.pointTypeCode,
                                  pointTypeName = lkPointType.pointTypeName,
                                  pointTypeID = lkPointType.Id,
                                  uplineNo = x.asUplineNo,
                                  pointPct = x.pointPct,
                                  pointKonvert = x.pointKonvert,
                                  schemaID = x.schemaID,
                                  isActive = x.isActive
                              }).ToList();

            return new ListResultDto<GetAllPointPctListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterPointPercent_Edit)]
        public void UpdateMsPointPct(InputPointPctDto input)
        {
            Logger.Info("UpdateMsPointPct() - Started.");

            var getPointPct = (from pointPct in _msPointPctRepo.GetAll()
                               where input.pointPctID == pointPct.Id
                               select pointPct).FirstOrDefault();

            var updatePointPct = getPointPct.MapTo<MS_PointPct>();

            updatePointPct.statusMemberID = input.statusMemberID;
            updatePointPct.pointTypeID = input.pointTypeID;
            updatePointPct.pointPct = input.pointPct;
            updatePointPct.pointKonvert = input.pointKonvert;
            updatePointPct.asUplineNo = input.asUplineNo;
            updatePointPct.isActive = input.isActive;

            try
            {
                Logger.DebugFormat("UpdateMsPointPct() - Start update PointPct. Params sent:{0}" +
                    "statusMemberID = {1}{0}" +
                    "pointTypeID    = {2}{0}" +
                    "pointPct       = {3}{0}" +
                    "pointKonvert   = {4}{0}" +
                    "asUplineNo     = {5}{0}" +
                    "isActive       = {6}"
                    , Environment.NewLine, input.statusMemberID, input.pointTypeID, input.pointPct, input.pointKonvert, input.asUplineNo, input.isActive);
                _msPointPctRepo.Update(updatePointPct);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                Logger.DebugFormat("UpdateMsPointPct() - End update PointPct.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("UpdateMsPointPct() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdateMsPointPct() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("UpdateMsPointPct() - Finished.");
        }
    }
}
