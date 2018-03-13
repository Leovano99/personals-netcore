using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_BankBranches.Dto
{
    public class GetBankBranchListDto
    {
        public int Id { get; set; }
        public int? entityID { get; set; }
        public string bankBranchCode { get; set; }
        public string bankBranchName { get; set; }
        public string bankBranchType { get; set; }
        public int bankBranchTypeID { get; set; }
        public string PICName { get; set; }
        public string PICPosition { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Boolean isActive { get; set; }
        public int? bankID { get; set; }
        public string bankName { get; set; }
        public string jenisKantorBank { get; set; }
    }
}
