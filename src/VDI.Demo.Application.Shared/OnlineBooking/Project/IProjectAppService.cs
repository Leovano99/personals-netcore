using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.OnlineBooking.Project.Dto;

namespace VDI.Demo.OnlineBooking.Project
{
    public interface IProjectAppService : IApplicationService
    {
        ListResultDto<ListProjectResultDto> GetListProject();

        ListResultDto<ListProjectInfoResultDto> GetListProjectInfo(int projectId);

        ListResultDto<ListProjectLocationResultDto> GetListProjectLocation(int projectId);

        ListResultDto<ListProjectImageGalleryResultDto> GetListProjectImageGallery(int projectId);

        ListResultDto<ListProjectKeyFeaturesResultDto> GetListProjectKeyFeatures(int projectId);
        
        DetailProjectResultDto GetDetailListProject(int projectId);

        ListResultDto<ListUnitTypeByClusterResultDto> GetUnitTypeByCluster(int projectId, int clusterId);

    }
}
