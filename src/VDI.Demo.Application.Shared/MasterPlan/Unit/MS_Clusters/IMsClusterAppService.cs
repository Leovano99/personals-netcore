using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using VDI.Demo.MasterPlan.Unit.MS_Clusters.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Clusters
{
    public interface IMsClusterAppService : IApplicationService
    {
        ListResultDto<GetClusterDropdownListDto> GetMsClusterDropdownPerProject(int projectID);
        ListResultDto<GetClusterDropdownListDto> GetMsClusterByProjectDropdown(int projectID);
        ListResultDto<GetAllMsClusterListDto> GetAllMsCluster(int projectID);
        void CreateMsCluster(List<CreateOrUpdateMsClusterInputDto> input);
        JObject UpdateMsCluster(CreateOrUpdateMsClusterInputDto input);
        void DeleteMsCluster(int id);
    }
}
