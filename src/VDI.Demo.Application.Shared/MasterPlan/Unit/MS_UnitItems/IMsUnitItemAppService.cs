using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.MasterPlan.Unit.MS_UnitItems.Dto;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitItems
{
    public interface IMsUnitItemAppService
    {
        ListResultDto<GetUnitItemListDto> GetDataMsUnitItem(GetUnitItemInputDto input);
        void UpdateCompanyMsUnitItem(List<UpdateCompanyMsUnitItemInputDto> input);
    }
}
