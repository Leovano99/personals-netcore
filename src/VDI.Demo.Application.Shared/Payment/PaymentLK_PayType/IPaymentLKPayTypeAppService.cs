using Abp.Application.Services;
using System.Collections.Generic;
using VDI.Demo.Payment.PaymentLK_PayType.Dto;

namespace VDI.Demo.Payment.PaymentLK_PayType
{
    public interface IPaymentLkPayTypeAppService : IApplicationService
    {
        void CreateOrUpdateLkPayType(CreateOrUpdateLkPayTypeInputDto input);

        List<CreateOrUpdateLkPayTypeInputDto> GetAllLkPayType();

        List<DropdownPayTypeDto> GetLkPayTypeDropdown();

        List<DropdownPayTypeDto> GetLkPayTypeDropdownCheckRole(int userID);
    }
}
