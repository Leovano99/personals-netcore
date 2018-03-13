using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Renovations.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Renovations
{
    public interface IMsRenovationAppService : IApplicationService
    {
        ListResultDto<GetAllMsRenovationListDto> GetAllMsRenovationIsActive();
        ListResultDto<GetAllMsRenovationListDto> GetAllMsRenovation();
        ListResultDto<GetAllMsRenovationListDto> GetMsRenovationDropdown();
        ListResultDto<GetLkItemListDto> GetItemDropdown();
        ListResultDto<GetAllMsRenovationListDto> GetAllMsRenovationByProject(int projectID);
        void CreateMsRenovation(List<CreateMsRenovationInput> inputs);
        void UpdateMsRenovation(UpdateMsRenovationInput input);
        void DeleteMsRenovation(int Id);
        void DeleteFileTempRenovation(string filename);
        ListResultDto<GetLkItemListDto> GetAvailableItemDropdownByProject(int projectID, int? renovID);
        ListResultDto<GetLkItemListDto> GetItemDropdownByProjectWithoutRenoveCode(int projectID, string renovCode);
    }
}
