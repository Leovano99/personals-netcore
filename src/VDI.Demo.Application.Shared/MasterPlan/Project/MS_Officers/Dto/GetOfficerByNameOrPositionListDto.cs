using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Officers.Dto
{
    public class GetOfficerByNameOrPositionListDto
    {
        public int officerID { get; set; }
        public string officerName { get; set; }
        public string officerPhone { get; set; }
        public string officerEmail { get; set; }
        public string officerPosition { get; set; }
    }
}
