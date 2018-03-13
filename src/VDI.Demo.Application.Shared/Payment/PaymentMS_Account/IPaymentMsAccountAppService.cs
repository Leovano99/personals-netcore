using Abp.Application.Services;
using System.Collections.Generic;
using VDI.Demo.Payment.PaymentLK_PayType.Dto;
using VDI.Demo.Payment.PaymentMS_Account.Dto;

namespace VDI.Demo.Payment.PaymentMS_Account
{
    public interface IPaymentMsAccountAppService : IApplicationService
    {
        void UpdateMsAccount(UpdateMsAccountInputDto input);

        List<GetMsAccountListDto> GetAllMsAccount();

        GetDetailMsAccountListDto GetMsAccountDetail(int Id);

        List<DropdownMsAccountDto> MsAccountDropdown();

        List<DropdownMsAccountDto> GetDropdownMsAccountByCompanyCode(string coCode);
    }
}
