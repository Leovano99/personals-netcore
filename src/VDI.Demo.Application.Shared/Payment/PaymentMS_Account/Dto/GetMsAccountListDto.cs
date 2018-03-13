using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentMS_Account.Dto
{
    public class GetMsAccountListDto
    {
        public string projectName { get; set; }
        public string companyName { get; set; }
        public string bankName { get; set; }

        public int accID { get; set; }
        public string accName { get; set; }
        public string accNo { get; set; }
        public bool isActive { get; set; }
    }
}
