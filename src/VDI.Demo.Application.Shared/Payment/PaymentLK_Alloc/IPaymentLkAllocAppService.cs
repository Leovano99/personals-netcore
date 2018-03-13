using Abp.Application.Services;
using System.Collections.Generic;
using VDI.Demo.Payment.PaymentLK_Alloc.Dto;

namespace VDI.Demo.Payment.PaymentLK_Alloc
{
    public interface IPaymentLkAllocAppService : IApplicationService
    {
        void CreateOrUpdateLkAlloc(CreateOrUpdateLkAllocInputDto input);

        List<GetLkAllocListDto> GetAllLkAlloc();

        List<GetLkAllocDropdownListDto> GetLkAllocDropdown();
    }
}
