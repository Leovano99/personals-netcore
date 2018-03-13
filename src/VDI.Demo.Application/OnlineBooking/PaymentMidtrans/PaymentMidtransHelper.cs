using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.OnlineBooking.PaymentMidtrans;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;

namespace VDI.Demo.OnlineBooking.PaymentOB
{
    public class PaymentMidtransHelper : IPaymentMidtransHelper
    {
        public PaymentOnlineBookingResponse ValidateResponseStatus(PaymentOnlineBookingResponse input)
        {
            switch (input.status_code)
            {
                case "404":
                    input.status_message = "transaction is not found";
                    break;
                case "406":
                    input.status_message = "Duplicate order Code. Order Code has already been utilized previously.";
                    break;
                case "400":
                    if(input.validation_messages.Count > 0)
                    {
                        input.status_message = input.validation_messages[0];
                        break;
                    }
                    else
                    {
                        break;
                    }
                default:
                    break;
            }

            return input;
        }

        public RequestTokenResultDto ValidateResponseToken(RequestTokenResultDto input)
        {
            return input;
        }
    }
}
