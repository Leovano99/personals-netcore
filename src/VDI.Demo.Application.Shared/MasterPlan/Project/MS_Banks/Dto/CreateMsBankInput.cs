using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Banks.Dto
{
    public class CreateMsBankInput
    {
        public int? entityID { get; set; }
        public string bankName { get; set; }
        public string bankCode { get; set; }
        public string bankLevelCode { get; set; }
        public string parentBankCode { get; set; }
        public Boolean divertToRO { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string headName { get; set; }
        public string deputyName1 { get; set; }
        public string deputyName2 { get; set; }
        public string att { get; set; }
        public string groupBankCode { get; set; }
        public string relationOfficerEmail { get; set; }
        public bool isActive { get; set; }
        public int bankTypeID { get; set; }
        public string swiftCode { get; set; }
    }
}
