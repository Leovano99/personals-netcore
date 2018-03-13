using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using Newtonsoft.Json.Linq;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Clusters.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Clusters
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCluster)]
    public class MsClusterAppService : DemoAppServiceBase, IMsClusterAppService
    {
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;

        public MsClusterAppService(
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Unit> msUnitRepo
        )
        {
            _msProjectRepo = msProjectRepo;
            _msClusterRepo = msClusterRepo;
            _msUnitRepo = msUnitRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCluster_Create)]
        public void CreateMsCluster(List<CreateOrUpdateMsClusterInputDto> input)
        {
            Logger.Info("CreateMsCluster() Started.");

            foreach (var item in input)
            {
                Logger.DebugFormat("CreateMsCluster() - Start checking existing cluster code. Parameters sent:{0}" +
                                "clusterCode     = {1}{0}" +
                                "clusterName     = {2}"
                                , Environment.NewLine, item.clusterCode, item.clusterName);

                var checkCode = (from x in _msClusterRepo.GetAll()
                                 where x.clusterCode == item.clusterCode
                                 select x).Any();

                Logger.DebugFormat("CreateMsCluster() - End checking existing cluster code. Result:{0}", checkCode);

                if (!checkCode)
                {
                    var data = new MS_Cluster
                    {
                        entityID = 1,
                        clusterCode = item.clusterCode,
                        clusterName = item.clusterName,
                        projectID = item.projectID,
                        gracePeriod = item.gracePeriod,
                        handOverPeriod = item.gracePeriod,
                        sortNo = 0, //hardcode di mockup belum ada ui
                        dueDateRemarks = "-",
                        penaltyRate = item.penaltyRate,
                        startPenaltyDay = item.startPenaltyDay
                    };
                    Logger.DebugFormat("CreateMsCluster() - Start insert cluster. Parameters sent:{0}" +
                    "	entityID	    = {1}{0}" +
                    "	clusterCode	    = {2}{0}" +
                    "	clusterName	    = {3}{0}" +
                    "	projectID	    = {4}{0}" +
                    "	gracePeriod	    = {5}{0}" +
                    "	handOverPeriod	= {6}{0}" +
                    "	sortNo      	= {7}{0}" +
                    "	penaltyRate 	= {8}{0}" +
                    "	startPenaltyDay = {9}{0}"
                    , Environment.NewLine, 1, item.clusterCode, item.clusterName, item.projectID, item.gracePeriod, item.handOverPeriod, item.sortNo, item.penaltyRate,
                    item.startPenaltyDay);
                    try
                    {
                        _msClusterRepo.Insert(data);
                    }
                    catch (DataException exDb)
                    {
                        Logger.ErrorFormat("CreateMsCluster() ERROR DbException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateMsCluster() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }

                    Logger.DebugFormat("CreateMsCluster() - End insert cluster.");
                }
                else
                {
                    Logger.ErrorFormat("CreateMsCluster() ERROR. Result = {0}", "Cluster Code Already Exist !");
                    throw new UserFriendlyException("Cluster Code Already Exist !");
                }
            }
            Logger.Info("CreateMsCluster() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCluster_Delete)]
        public void DeleteMsCluster(int Id)
        {
            Logger.Info("DeleteMsCluster() Started.");

            Logger.DebugFormat("DeleteMsCluster() - Start checking Cluster Code with Id: {0}.", Id);

            bool checkUnit = (from unit in _msUnitRepo.GetAll()
                              where unit.clusterID == Id
                              select unit.clusterID).Any();

            Logger.DebugFormat("DeleteMsCluster() - End checking Cluster Code. Result: {0}.", checkUnit);

            if (!checkUnit)
            {
                try
                {
                    Logger.DebugFormat("DeleteMsCluster() - Start delete Cluster. Parameters sent: {0}", Id);
                    _msClusterRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsCluster() - End delete Cluster");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteMsCluster() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsCluster() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("DeleteMsCluster() ERROR Exception. Result = {0}", "This Cluster is used by another master!");
                throw new UserFriendlyException("This Cluster is used by another master!");
            }
            Logger.Info("DeleteMsCluster() Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCluster)]
        public ListResultDto<GetAllMsClusterListDto> GetAllMsCluster(int projectID)
        {
            var result = (from x in _msClusterRepo.GetAll()
                          join a in _msProjectRepo.GetAll() on x.projectID equals a.Id
                          where x.projectID == projectID
                          select new GetAllMsClusterListDto
                          {
                              Id = x.Id,
                              clusterCode = x.clusterCode,
                              clusterName = x.clusterName,
                              projectName = a.projectName,
                              gracePeriod = x.gracePeriod,
                              handOverPeriod = x.handOverPeriod,
                              dueDateDevelopment = x.dueDateDevelopment,
                              dueDateRemarks = x.dueDateRemarks,
                              sortNo = x.sortNo,
                              penaltyRate = x.penaltyRate,
                              startPenaltyDay = x.startPenaltyDay
                          })
                          .ToList();

            return new ListResultDto<GetAllMsClusterListDto>(result);
        }

        public ListResultDto<GetClusterDropdownListDto> GetMsClusterByProjectDropdown(int projectID)
        {
            var getData = (from A in _msUnitRepo.GetAll()
                           join B in _msClusterRepo.GetAll() on A.clusterID equals B.Id
                           where A.projectID == projectID
                           orderby B.Id descending
                           select new GetClusterDropdownListDto
                           {
                               clusterID = B.Id,
                               clusterCode = B.clusterCode,
                               clusterName = B.clusterName
                           }).Distinct().ToList();

            return new ListResultDto<GetClusterDropdownListDto>(getData);
        }

        public ListResultDto<GetClusterDropdownListDto> GetMsClusterDropdownPerProject(int projectID)
        {
            var getData = (from A in _msClusterRepo.GetAll()
                           where A.projectID == projectID
                           orderby A.Id descending
                           select new GetClusterDropdownListDto
                           {
                               clusterID = A.Id,
                               clusterCode = A.clusterCode,
                               clusterName = A.clusterName
                           }).ToList();

            return new ListResultDto<GetClusterDropdownListDto>(getData);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCluster_Edit)]
        public JObject UpdateMsCluster(CreateOrUpdateMsClusterInputDto input)
        {
            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMsCluster() - Start checking existing cluster. Parameters sent:{0}" +
                "clusterCode     = {1}{0}" +
                "clusterName     = {2}"
                , Environment.NewLine, input.clusterCode, input.clusterName);

            var checkCode = (from x in _msClusterRepo.GetAll()
                             where x.Id != input.Id && x.clusterCode == input.clusterCode
                             select x).Any();

            Logger.DebugFormat("UpdateMsCluster() - End checking existing cluster. Result: {0}", checkCode);

            var getMsCluster = (from x in _msClusterRepo.GetAll()
                                 where x.Id == input.Id
                                 select x).FirstOrDefault();
            if (!checkCode)
            {
                var updateMsCluster = getMsCluster.MapTo<MS_Cluster>();

                var checkUnit = (from unit in _msUnitRepo.GetAll()
                                 where unit.clusterID == input.Id
                                 select unit).Any();

                if (!checkUnit)
                {
                    updateMsCluster.clusterCode = input.clusterCode;
                    updateMsCluster.clusterName = input.clusterName;
                    updateMsCluster.gracePeriod = input.gracePeriod;
                    updateMsCluster.handOverPeriod = input.handOverPeriod;
                    updateMsCluster.projectID = input.projectID;
                    updateMsCluster.penaltyRate = input.penaltyRate;
                    updateMsCluster.startPenaltyDay = input.startPenaltyDay;
                    updateMsCluster.sortNo = input.sortNo;
                    obj.Add("message", "Edit Successfully");
                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change cluster Code & Name");
                }
                Logger.DebugFormat("UpdateMsCluster() - Start update cluster. Parameters sent:{0}" +
                "	entityID	    = {1}{0}" +
                "	clusterCode	    = {2}{0}" +
                "	clusterName	    = {3}{0}" +
                "	projectID	    = {4}{0}" +
                "	gracePeriod	    = {5}{0}" +
                "	handOverPeriod	= {6}{0}" +
                "	sortNo      	= {7}{0}" +
                "	penaltyRate 	= {8}{0}" +
                "	startPenaltyDay = {9}{0}"
                , Environment.NewLine, 1, input.clusterCode, input.clusterName, input.projectID, input.gracePeriod, input.handOverPeriod, input.sortNo, input.penaltyRate,
                input.startPenaltyDay);

                try
                {
                    _msClusterRepo.Update(updateMsCluster);
                }
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateMsCluster() ERROR DbException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsCluster() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }

                Logger.DebugFormat("UpdateMsCluster() - End update cluster");
            }
            else
            {
                Logger.ErrorFormat("UpdateMsCluster() Cluster Code already exist. Result = {0}", checkCode);
                throw new UserFriendlyException("Cluster Code already exist!");
            }
            return obj;
        }
    }
}
