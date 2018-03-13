using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentMS_Account.Dto
{
    public class UpdateMsAccountInputDto
    {
        public int accID { get; set; }
        public int? accountEmailID { get; set; }
        public string emailTo { get; set; }
        public string emailCc { get; set; }
        public string natureAccountBank { get; set; }
        public string natureAccountDep { get; set; }
        public string org { get; set; }
        public string province { get; set; }
        public bool isActive { get; set; }
    }
}
