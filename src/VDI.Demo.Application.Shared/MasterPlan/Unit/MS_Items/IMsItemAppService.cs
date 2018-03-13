using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using VDI.Demo.MasterPlan.Unit.MS_Items.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_Items
{
    public interface IMsItemAppService : IApplicationService
    {
        ListResultDto<GetAllMsItemListDto> GetAllMsItem();
        ListResultDto<GetAllMsItemListDto> GetMsItemDropdown();
        ListResultDto<GetMsUnitItemDropdownListDto> GetMsUnitItemDropdown(List<int> unitID);
        void CreateMsItem(CreateMsItemInput input);
        void UpdateMsItem(UpdateMsItemInput input);
        void DeleteMsItem(int Id);
    }
}
