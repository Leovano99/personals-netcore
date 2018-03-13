using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Officers.Dto
{
    public class GetAllOfficerListDto
    {
        public int officerID { get; set; }
        public string officerName { get; set; }
        public string email { get; set; }
        public string handphone { get; set; }
        public string fax { get; set; }
        public int? departmentID { get; set; }
        public string departmentName { get; set; }
        public int? positionID { get; set; }
        public string positionName { get; set; }
        public Boolean isActive { get; set; }
    }
}
