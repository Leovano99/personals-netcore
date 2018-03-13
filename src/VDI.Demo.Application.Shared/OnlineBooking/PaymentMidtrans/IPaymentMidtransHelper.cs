using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans
{
    public interface IPaymentMidtransHelper : IApplicationService
    {
        PaymentOnlineBookingResponse ValidateResponseStatus(PaymentOnlineBookingResponse orderCode);

        RequestTokenResultDto ValidateResponseToken(RequestTokenResultDto orderCode);
    }
}
