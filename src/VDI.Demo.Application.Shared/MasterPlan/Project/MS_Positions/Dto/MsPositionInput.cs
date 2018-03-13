using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Positions.Dto
{
    public class MsPositionInput
    {
        public int? Id { get; set; }
        public string positionName { get; set; }
        public string positionCode { get; set; }
        public int departmentID { get; set; }
        public Boolean isActive { get; set; }
    }
}
