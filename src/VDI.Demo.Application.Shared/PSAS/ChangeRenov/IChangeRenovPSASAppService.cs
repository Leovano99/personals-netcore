using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.ChangeRenov.Dto;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.ChangeRenov
{
    public interface IChangeRenovPSASAppService : IApplicationService
    {
        GetRenovListDto GetDataChangeRenov(GetPSASParamsDto input);

        List<GetRenovDropdownListDto> GetRenovDropdown(ItemDropdownInputDto input);

        void CalculationChangeRenov(ChangeRenovInputDto input);
    }
}
