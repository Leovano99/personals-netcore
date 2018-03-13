using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Payment.PaymentLK_PayFor.Dto;

namespace VDI.Demo.Payment.PaymentLK_PayFor
{
    public interface IPaymentLkPayForAppService : IApplicationService
    {
        void CreateOrUpdateLkPayFor(CreateOrUpdateLkPayForInputDto input);
        List<CreateOrUpdateLkPayForInputDto> GetAllLkPayFor();
        List<DropdownLkPayForDto> LkPayForDropdown();

        List<DropdownLkPayForDto> LkPayForDropdownCheckRole(int userID);
        List<DropdownLkPayForDto> LkPayForDropdownUnknown();
    }
}
