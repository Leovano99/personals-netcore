using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Officers.Dto
{
    public class GetCreateMsOfficerListDto
    {
        public int officerID { get; set; }
        public int departmentID { get; set; }
        public string departmentName { get; set; }
    }
}
