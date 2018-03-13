using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Accounts.Dto
{
    public class GetAllAccountListDto
    {
        public int accountID { get; set; }
        public string projectName { get; set; }
        public int projectID { get; set; }
        public string accName { get; set; }
        public string accNo { get; set; }
        public int companyID { get; set; }
        public string companyName { get; set; }
        public int bankBranchID { get; set; }
        public string bankBranchName { get; set; }
        public int bankID { get; set; }
        public string bankName { get; set; }
        public bool isActive { get; set; }
        public string accCode { get; set; }
    }
}
