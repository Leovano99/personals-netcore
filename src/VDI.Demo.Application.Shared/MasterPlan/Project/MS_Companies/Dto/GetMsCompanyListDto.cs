using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Companies.Dto
{
    public class GetMsCompanyListDto
    {
        public int Id { get; set; }
        public string coName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public bool isActive { get; set; }
    }
}
