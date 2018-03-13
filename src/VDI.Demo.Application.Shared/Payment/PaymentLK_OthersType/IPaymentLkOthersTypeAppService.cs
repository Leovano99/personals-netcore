using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Payment.PaymentLK_OthersType.Dto;

namespace VDI.Demo.Payment.PaymentLK_OthersType
{
    public interface IPaymentLkOthersTypeAppService : IApplicationService
    {
        void CreateOrUpdateLkOthersType(CreateOrUpdateLkOthersTypeInputDto input);

        List<CreateOrUpdateLkOthersTypeInputDto> GetAllLkOthersType();

        List<DropdownLkOthersTypeDto> GetLkOthersTypeDropdownCheckRole(int userID, int payForID); 
    }
}
