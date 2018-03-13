using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Companies.Dto
{
    public class UpdateMsCompanyInput
    {
        public int Id { get; set; }
        public string coCode { get; set; }
        public string coName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string faxNo { get; set; }
        public string npwp { get; set; }
        public string npwpAddress { get; set; }
        public string kppName { get; set; }
        public string kppTTD { get; set; }
        public string pkp { get; set; }
        public DateTime pkpDate { get; set; }
        public bool isActive { get; set; }
        public int countryID { get; set; }
        public int townID { get; set; }
        public int postCodeID { get; set; }
        public string fileName { get; set; }
        public string fileNameNew { get; set; }
        public string image { get; set; }
    }
}
