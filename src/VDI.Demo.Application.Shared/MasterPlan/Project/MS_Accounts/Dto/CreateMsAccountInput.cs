using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Accounts.Dto
{
    public class CreateMsAccountInput
    {
        public int? ID { get; set; }
        public int entityID { get; set; }
        public int projectID { get; set; }
        public string accName { get; set; }
        public string accCode { get; set; }
        public string accNo { get; set; }
        public int companyID { get; set; }
        public int bankBranchID { get; set; }
        public int bankID { get; set; }
        public string projectName { get; set; }
        public bool isActive { get; set; }
    }
}
