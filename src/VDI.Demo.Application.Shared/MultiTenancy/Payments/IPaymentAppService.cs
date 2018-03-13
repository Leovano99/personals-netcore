using System.Threading.Tasks;
using Abp.Application.Services;
using VDI.Demo.MultiTenancy.Dto;
using VDI.Demo.MultiTenancy.Payments.Dto;
using Abp.Application.Services.Dto;

namespace VDI.Demo.MultiTenancy.Payments
{
    public interface IPaymentAppService : IApplicationService
    {
        Task<PaymentInfoDto> GetPaymentInfo(PaymentInfoInput input);

        Task<CreatePaymentResponse> CreatePayment(CreatePaymentDto input);

        Task<ExecutePaymentResponse> ExecutePayment(ExecutePaymentDto input);

        Task<PagedResultDto<SubscriptionPaymentListDto>> GetPaymentHistory(GetPaymentHistoryInput input);
    }
}
