using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Projects.Dto
{
    public class MpOfficerProjectInputDto
    {
        public int projectID { get; set; }
        public int managerID { get; set; }
        public int staffID { get; set; }
        public string whatsappNo { get; set; }
        public string phoneNo { get; set; }
        public string email { get; set; }
    }
}
