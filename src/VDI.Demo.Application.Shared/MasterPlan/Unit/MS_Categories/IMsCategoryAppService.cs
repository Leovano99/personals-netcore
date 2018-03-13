using System;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Categories.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Categories
{
    public interface IMsCategoryAppService : IApplicationService
    {
        ListResultDto<GetAllCategoryListDto> GetAllMsCategory();
        ListResultDto<GetCategoryDropdownListDto> GetMsCategoryDropdown();
        ListResultDto<GetCategoryDropdownListDto> GetMsCategoryDropdownByProjectCluster(int projectID, int clusterID);
        void CreateMsCategory(CreateCategoryInputDto input);
        void UpdateMsCategory(GetAllCategoryListDto input);
        void DeleteMsCategory(int Id);
    }
}
