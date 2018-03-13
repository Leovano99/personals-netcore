using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_BankBranches.Dto
{
    public class CreateMsBankBranchInputDto
    {
        public string bankBranchCode { get; set; }

        public string bankBranchName { get; set; }

        public string PICName { get; set; }

        public string PICPosition { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public Boolean isActive { get; set; }

        public int bankID { get; set; }

        public int bankBranchTypeID { get; set; }
    }
}
