using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Banks.Dto
{
    public class GetAllBankListDto
    {
        public int bankID { get; set; }
        public string bankName { get; set; }
        public string bankCode { get; set; }
        public int bankTypeID { get; set; }
        public string bankTypeName { get; set; }
        public string swiftCode { get; set; }
        public bool isActive { get; set; }
    }
}
