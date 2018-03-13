using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentMS_Account.Dto
{
    public class GetDetailMsAccountListDto
    {
        public int accID { get; set; }
        public string accCode { get; set; }
        public string accName { get; set; }
        public string accNo { get; set; }
        public string natureAccountBank { get; set; }
        public string natureAccountDep { get; set; }
        public string org { get; set; }
        public string province { get; set; }
        public bool isActive { get; set; }

        public int bankID { get; set; }
        public string bankName { get; set; }
        public int bankBranchID { get; set; }
        public string bankBranchName { get; set; }

        public int projectID { get; set; } 
        public string projectName { get; set; }

        public int coID { get; set; }
        public string companyName { get; set; }

        public int accountEmailID { get; set; }
        public string emailTo { get; set; }
        public string emailCc { get; set; }
    }
}
