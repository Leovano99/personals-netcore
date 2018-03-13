using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class GetUnitTaskListDto
    {
        public int unitTaskListID { get; set; }
        public string link { get; set; }
        public DateTime creationTime { get; set; }
    }
}
