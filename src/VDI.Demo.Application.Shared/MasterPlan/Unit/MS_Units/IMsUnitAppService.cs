using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Newtonsoft.Json.Linq;
using VDI.Demo.MasterPlan.Unit.MS_Units.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Units
{
    public interface IMsUnitAppService : IApplicationService
    {
        ListResultDto<GetFacingDropdownListDto> GetMsFacingDropdown();
        ListResultDto<GetZoningDropdownListDto> GetMsZoningDropdown();
        ListResultDto<GetProjectDropdownListDto> GetMsProjectDropdown();
        void CreateOrUpdateMsUnitItem(CreateUnitItemInputDto input);

        //Task CreateUniversalSystem(CreateUniversalSystemInputDto input);
        JObject CreateUniversalExcel(CreateUniversalExcelInputDto input);
        ListResultDto<GetUnavailableUnitNoListDto> GetUnavailableUnitNo(List<GetUnavailableUnitNoInputDto> inputs);
        ListResultDto<GetUnitByFloorListDto> GetUnitByFloor(GetUnitByFloorInputDto input);
        ListResultDto<GetProjectCodeByUnitCodeUnitNoListDto> GetProductCodeByUnitCodeUnitNo(List<GetProductCodeByUnitCodeUnitNoDto> input);
        ListResultDto<GetUnitByProjectClusterTermCodeTermNoDto> GetUnitByProjectClusterTermCodeTermNo(int projectID, int clusterID, string termCode, short termNo);
        ListResultDto<GetUnitCodeByProjectListDto> GetUnitCodeByProject(int projectId);
        ListResultDto<GetFloorByClusterListDto> GetFloorByCluster(int clusterID);
        ListResultDto<GetUnitNoByUnitCodeListDto> GetUnitNoByUnitCode(string unitCode);
        ListResultDto<GetUnitByProjectCategoryListDto> GetUnitByProjectCategory(GetUnitByProjectCategoryInput input);
        ListResultDto<GetUnitByClusterListDto> GetUnitByCluster(int clusterID);
        ListResultDto<GetUnitDropdownListDto> GetUnitByClusterProjectDropdown(List<int> clusterID);
        void ManageStatusMsUnit(List<ManageStatusMsUnitInput> input);

        void CreateUnitTaskList(CreateUnitTaskListInputDto input);
        List<GetUnitTaskListDto> GetUnitTaskList(int projectID);
    }
}
