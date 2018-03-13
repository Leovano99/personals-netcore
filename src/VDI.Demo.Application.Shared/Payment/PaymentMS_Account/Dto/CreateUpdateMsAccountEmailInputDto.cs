using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentMS_Account.Dto
{
    public class CreateUpdateMsAccountEmailInputDto
    {
        public int accID { get; set; }
        public string emailTo { get; set; }
        public string emailCc { get; set; }
    }
}
