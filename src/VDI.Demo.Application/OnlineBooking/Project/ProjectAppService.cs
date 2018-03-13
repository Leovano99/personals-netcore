using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Domain.Uow;
using VDI.Demo;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.OnlineBooking.Project.Dto;
using Abp.UI;
using VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo;
using System.Net.Mail;
using System.Net;
using VDI.Demo.Authorization;

namespace VDI.Demo.OnlineBooking.Project
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project)]
    public class ProjectAppService : DemoAppServiceBase, IProjectAppService
    {
        private readonly IRepository<MS_Project> _msProject;
        private readonly IRepository<MS_Unit> _msUnit;
        private readonly IRepository<MS_Category> _msCategory;
        private readonly IRepository<MS_Area> _msArea;
        private readonly IRepository<MS_UnitItem> _msUnitItem;
        private readonly IRepository<MS_UnitItemPrice> _msUnitItemPrice;
        private readonly IRepository<MS_Cluster> _msCluster;
        private readonly IRepository<MS_Detail> _msDetail;
        private readonly IRepository<MS_ProjectInfo> _msProjectInfo;
        private readonly IRepository<MS_ProjectKeyFeaturesCollection> _msProjectKeyFeaturesCollection;
        private readonly IRepository<MS_ProjectLocation> _msProjectLocation;
        private readonly IRepository<TR_ProjectImageGallery> _trProjectImageGallery;
        private readonly IRepository<TR_ProjectKeyFeatures> _trProjectKeyFeatures;
        private readonly IRepository<MS_UnitRoom> _msUnitRoomRepo;
        private readonly IRepository<MS_PromoOnlineBooking> _msPromoOnlineBooking;


        public ProjectAppService(
            IRepository<MS_Project> msProject,
            IRepository<MS_Category> msCategory,
            IRepository<MS_Unit> msUnit,
            IRepository<MS_Area> msArea,
            IRepository<MS_UnitItem> msUnitItem,
            IRepository<MS_UnitItemPrice> msUnitItemPrice,
            IRepository<MS_Cluster> msCluster,
            IRepository<MS_Detail> msDetail,
            IRepository<MS_ProjectInfo> msProjectInfo,
            IRepository<MS_ProjectKeyFeaturesCollection> msProjectKeyFeaturesCollection,
            IRepository<MS_ProjectLocation> msProjectLocation,
            IRepository<TR_ProjectImageGallery> trProjectImageGallery,
            IRepository<TR_ProjectKeyFeatures> trProjectKeyFeatures,
            IRepository<MS_UnitRoom> msUnitRoomRepo,
            IRepository<MS_PromoOnlineBooking> msPromoOnlineBooking
        )
        {
            _msProject = msProject;
            _msCategory = msCategory;
            _msUnit = msUnit;
            _msArea = msArea;
            _msUnitItem = msUnitItem;
            _msUnitItemPrice = msUnitItemPrice;
            _msCluster = msCluster;
            _msDetail = msDetail;
            _msProjectInfo = msProjectInfo;
            _msProjectKeyFeaturesCollection = msProjectKeyFeaturesCollection;
            _msProjectLocation = msProjectLocation;
            _trProjectImageGallery = trProjectImageGallery;
            _trProjectKeyFeatures = trProjectKeyFeatures;
            _msUnitRoomRepo = msUnitRoomRepo;
            _msPromoOnlineBooking = msPromoOnlineBooking;
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListPromotion)]
        public ListResultDto<GetListPromotionResultDto> GetListPromotion()
        {
            var getPromo = (from promo in _msPromoOnlineBooking.GetAll()
                            where promo.isActive == true
                            select new GetListPromotionResultDto
                            {
                                isActive = promo.isActive,
                                promoAlt = promo.promoAlt,
                                promoDataType = promo.promoDataType,
                                promoFile = promo.promoFile,
                                projectID = promo.projectID,
                                targetURL = promo.targetURL
                            }).ToList();

            return new ListResultDto<GetListPromotionResultDto>(getPromo);
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetDetailListProject)]
        public DetailProjectResultDto GetDetailListProject(int projectId)
        {
            var detailList = (from A in _msProject.GetAll()
                              where A.Id == projectId
                              select new DetailProjectResultDto
                              {
                                  projectID = A.Id,
                                  projectCode = A.projectCode,
                                  projectName = A.projectName
                              }).FirstOrDefault();
            if (detailList == null)
            {
                var error = new DetailProjectResultDto
                {
                    message = "data not found"
                };
                return error;
                //throw new UserFriendlyException("data not found");
            }
            var getcluster = (from x in _msUnit.GetAll()
                              join y in _msCluster.GetAll()
                              on x.clusterID equals y.Id
                              //REPAIR THIS!!!
                              where x.projectID == projectId /*&& x.clusterID == 1 //"FHP"*/
                              select new ClusterResultDto
                              {
                                  clusterId = x.clusterID,
                                  clusterCode = y.clusterCode,
                                  clusterName = y.clusterName
                              }).Distinct().ToList();

            //List<ClusterResultDto> getcluster;

            ////REPAIR THIS!!!
            //if (projectId == 3) //"MVI"
            //{
            //    getcluster = (from x in _msUnit.GetAll()
            //                  join y in _msCluster.GetAll()
            //                  on x.clusterID equals y.Id
            //                  //REPAIR THIS!!!
            //                  where x.projectID == projectId && x.clusterID == 1 //"FHP"
            //                  select new ClusterResultDto
            //                  {
            //                      clusterId = x.clusterID,
            //                      clusterName = y.clusterName
            //                  }).Distinct().ToList();
            //}
            ////REPAIR THIS!!!
            //else if (projectId == 1) //"HLV"
            //{
            //    getcluster = (from x in _msUnit.GetAll()
            //                  join y in _msCluster.GetAll()
            //                  on x.clusterID equals y.Id
            //                  //REPAIR THIS!!!
            //                  where x.projectID == projectId && new[] { 1, 2, 3 }.Contains(x.clusterID) //"2HV1", "HV2", "LTHV"
            //                  select new ClusterResultDto
            //                  {
            //                      clusterId = x.clusterID,
            //                      clusterName = y.clusterName
            //                  }).Distinct().ToList();

            //}
            ////REPAIR THIS!!!
            //else if (projectId == 2) //"MOR"
            //{
            //    getcluster = (from x in _msUnit.GetAll()
            //                  join y in _msCluster.GetAll()
            //                  on x.clusterID equals y.Id
            //                  //REPAIR THIS!!!
            //                  where x.projectID == projectId && new[] { 1, 2 }.Contains(x.clusterID) //"2AM", "2PR"
            //                  select new ClusterResultDto
            //                  {
            //                      clusterId = x.clusterID,
            //                      clusterName = y.clusterName
            //                  }).Distinct().ToList();

            //}

            //else if (projectId == 3) //"LKS"
            //{
            //    getcluster = (from x in _msUnit.GetAll()
            //                  join y in _msCluster.GetAll()
            //                  on x.clusterID equals y.Id
            //                  //REPAIR THIS!!!
            //                  where x.projectID == projectId && x.clusterID == 1 //"RLH"
            //                  select new ClusterResultDto
            //                  {
            //                      clusterId = x.clusterID,
            //                      clusterName = y.clusterName
            //                  }).Distinct().ToList();
            //}
            //else
            //{
            //    getcluster = (from x in _msUnit.GetAll()
            //                  join y in _msCluster.GetAll()
            //                  on x.clusterID equals y.Id
            //                  //REPAIR THIS!!!
            //                  where x.projectID == projectId && new[] { 1, 2, 3, 4, 5, 6, 7 }.Contains(x.clusterID) //"ADM", "DHG", "DLF", "HAR", "HBL", "LDN", "RDM"
            //                  select new ClusterResultDto
            //                  {
            //                      clusterId = x.clusterID,
            //                      clusterName = y.clusterName
            //                  }).Distinct().ToList();
            //}

            detailList = new DetailProjectResultDto
            {
                projectID = detailList.projectID,
                projectCode = detailList.projectCode,
                projectName = detailList.projectName,
                cluster = getcluster
            };

            return detailList;
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProject)]
        public ListResultDto<ListProjectResultDto> GetListProject()
        {
            var allList = (from A in _msProject.GetAll()
                           join b in _msProjectInfo.GetAll() on A.Id equals b.projectID into project
                           from b in project.DefaultIfEmpty()
                           select new ListProjectResultDto
                           {
                               projectID = A.Id,
                               projectCode = A.projectCode,
                               projectName = A.projectName,
                               image = b.projectImageLogo
                           }).ToList();

            if (allList == null)
            {
                var error = new List<ListProjectResultDto>();

                var message = new ListProjectResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                //throw new UserFriendlyException("data not found");
                return new ListResultDto<ListProjectResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListProjectResultDto>(allList);
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectByName)]
        public ListResultDto<ListProjectResultDto> GetListProjectByName(string projectName)
        {
            var detailList = (from A in _msProject.GetAll()
                              where A.projectName.Contains(projectName)
                              select new ListProjectResultDto
                              {
                                  projectID = A.Id,
                                  projectName = A.projectName,
                                  projectCode = A.projectCode
                              }).ToList();
            if (detailList == null)
            {
                var error = new List<ListProjectResultDto>();

                var message = new ListProjectResultDto
                {
                    message = "data not found"
                };

                error.Add(message);
                //throw new UserFriendlyException("data not found");
                return new ListResultDto<ListProjectResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListProjectResultDto>(detailList);
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectImageGallery)]
        public ListResultDto<ListProjectImageGalleryResultDto> GetListProjectImageGallery(int projectId)
        {
            var getAll = (from x in _trProjectImageGallery.GetAll()
                          join y in _msProjectInfo.GetAll()
                          on x.projectInfoID equals y.Id
                          where y.projectID == projectId
                          select new ListProjectImageGalleryResultDto
                          {
                              imageAlt = x.imageAlt,
                              imageStatus = x.imageStatus,
                              imageURL = x.imageURL,
                              ProjectImageGalleryID = x.Id,
                              projectInfoID = x.projectInfoID
                          }).ToList();
            if (getAll == null)
            {
                var error = new List<ListProjectImageGalleryResultDto>();

                var message = new ListProjectImageGalleryResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                return new ListResultDto<ListProjectImageGalleryResultDto>(error);

            }
            else
            {
                return new ListResultDto<ListProjectImageGalleryResultDto>(getAll);
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectInfo)]
        public ListResultDto<ListProjectInfoResultDto> GetListProjectInfo(int projectId)
        {
            var getAll = (from x in _msProjectInfo.GetAll()
                          where x.projectID == projectId
                          select new ListProjectInfoResultDto
                          {
                              projectDeveloper = x.projectDeveloper,
                              projectID = x.projectID,
                              projectInfoID = x.Id,
                              projectMarketingOffice = x.projectMarketingOffice,
                              projectMarketingPhone = x.projectMarketingPhone,
                              projectStatus = x.projectStatus,
                              projectWebsite = x.projectWebsite,
                              sitePlansImageUrl = x.sitePlansImageUrl,
                              sitePlansLegend = x.sitePlansLegend,
                              projectDesc = x.projectDesc
                          }).ToList();
            if (getAll == null)
            {
                var error = new List<ListProjectInfoResultDto>();

                var message = new ListProjectInfoResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                return new ListResultDto<ListProjectInfoResultDto>(error);

            }
            else
            {
                return new ListResultDto<ListProjectInfoResultDto>(getAll);
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectKeyFeatures)]
        public ListResultDto<ListProjectKeyFeaturesResultDto> GetListProjectKeyFeatures(int projectId)
        {
            var getAll = (from x in _trProjectKeyFeatures.GetAll()
                          join y in _msProjectKeyFeaturesCollection.GetAll()
                          on x.keyFeaturesCollectionID equals y.Id
                          join z in _msProjectInfo.GetAll()
                          on y.Id equals z.keyFeaturesCollectionID
                          where z.projectID == projectId
                          select new ListProjectKeyFeaturesResultDto
                          {
                              keyFeatures = x.keyFeatures,
                              ProjectKeyFeaturesID = x.Id,
                              status = x.status
                          }).ToList();
            if (getAll == null)
            {
                var error = new List<ListProjectKeyFeaturesResultDto>();

                var message = new ListProjectKeyFeaturesResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                return new ListResultDto<ListProjectKeyFeaturesResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListProjectKeyFeaturesResultDto>(getAll);
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetListProjectLocation)]
        public ListResultDto<ListProjectLocationResultDto> GetListProjectLocation(int projectId)
        {
            var getAll = (from x in _msProjectLocation.GetAll()
                          join y in _msProjectInfo.GetAll()
                          on x.projectInfoID equals y.Id
                          where y.projectID == projectId
                          select new ListProjectLocationResultDto
                          {
                              projectAddress = x.projectAddress,
                              latitude = x.latitude,
                              locationImageURL = x.locationImageURL,
                              longitude = x.longitude,
                              projectInfoID = x.projectInfoID,
                              ProjectLocationID = x.Id
                          }).ToList();
            if (getAll == null)
            {
                var error = new List<ListProjectLocationResultDto>();

                var message = new ListProjectLocationResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                return new ListResultDto<ListProjectLocationResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListProjectLocationResultDto>(getAll);
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetUnitTypeByCluster)]
        public ListResultDto<ListUnitTypeByClusterResultDto> GetUnitTypeByCluster(int projectId, int clusterId)
        {
            var getArea = (from a in _msUnit.GetAll()
                           join b in _msUnitItem.GetAll()
                           on a.Id equals b.unitID
                           group b by new
                           {
                               a.detailID,
                               a.projectID,
                               a.clusterID,
                               a.Id
                           } into G
                           where G.Key.projectID == projectId && G.Key.clusterID == clusterId
                           select new
                           {
                               G.Key.projectID,
                               G.Key.clusterID,
                               G.Key.detailID,
                               area = G.Sum(x => x.area),
                               unitID = G.Key.Id
                           }).Distinct().ToList();

            var getUnitType = (from a in getArea
                               join b in _msUnit.GetAll().Distinct() on a.unitID equals b.Id
                               join c in _msDetail.GetAll().Distinct() on b.detailID equals c.Id
                               join d in _msUnitItem.GetAll()
                               on b.Id equals d.unitID
                               join e in _msUnitRoomRepo.GetAll()
                               on d.Id equals e.unitItemID
                               group c by new
                               {
                                   c.Id,
                                   c.detailCode,
                                   a.area,
                                   e.bedroom,
                                   c.detailImage
                               } into G
                               select new ListUnitTypeByClusterResultDto
                               {
                                   unitTypeID = G.Key.Id,
                                   unitType = G.Key.detailCode,
                                   area = G.Key.area,
                                   bedroom = G.Key.bedroom,
                                   detailImage = G.Key.detailImage
                               }).Distinct().ToList();

            if (getUnitType == null)
            {
                var error = new List<ListUnitTypeByClusterResultDto>();

                var message = new ListUnitTypeByClusterResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                //throw new UserFriendlyException("Data not found");
                return new ListResultDto<ListUnitTypeByClusterResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListUnitTypeByClusterResultDto>(getUnitType);
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_Project_GetUnitTypeByProjectId)]
        public ListResultDto<ListUnitTypeByProjectIdResultDto> GetUnitTypeByProjectId(int projectId)
        {
            var getUnitType = (from x in _msDetail.GetAll()
                               join y in _msUnit.GetAll()
                               on x.Id equals y.detailID
                               join z in _msCluster.GetAll()
                               on y.clusterID equals z.Id
                               join a in _msUnitItem.GetAll()
                               on y.Id equals a.unitID
                               where y.projectID == projectId
                               select new ListUnitTypeByProjectIdResultDto
                               {
                                   unitTypeID = x.Id,
                                   unitType = x.detailCode,
                                   clusterID = y.clusterID,
                                   clusterName = z.clusterName,
                                   area = a.area,
                                   image = x.detailImage
                               }).Distinct().ToList();

            if (getUnitType == null)
            {
                var error = new List<ListUnitTypeByProjectIdResultDto>();

                var message = new ListUnitTypeByProjectIdResultDto
                {
                    message = "data not found"
                };

                error.Add(message);

                //throw new UserFriendlyException("Data not found");
                return new ListResultDto<ListUnitTypeByProjectIdResultDto>(error);
            }
            else
            {
                return new ListResultDto<ListUnitTypeByProjectIdResultDto>(getUnitType);
            }
        }
    }
}
