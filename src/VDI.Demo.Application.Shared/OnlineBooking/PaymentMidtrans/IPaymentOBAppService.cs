using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;

namespace VDI.Demo.OnlineBooking.PaymentMidtrans
{
    public interface IPaymentOBAppService : IApplicationService
    {
        Task<PaymentOnlineBookingResponse> CreatePayment(PaymentOnlineBookingRequest input);
        Task<PaymentOnlineBookingResponse> CheckPaymentStatus(string orderCode);
        //Task PaymentFinish(PaymentOnlineBookingResponse data);
        void PaymentError(PaymentOnlineBookingResponse data);
        void PaymentNotification(PaymentOnlineBookingResponse data);
        //KonfirmasiPesananDto PaymentFinish(PaymentOnlineBookingResponse data);
    }
}
