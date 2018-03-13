using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Projects.Dto
{
    public class MpCompanyProjectInputDto
    {
        public int projectID { get; set; }
        public int companyID { get; set; }
        public bool mainStatus { get; set; }
    }
}
