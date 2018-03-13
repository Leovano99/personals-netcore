using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateOrUpdateMsUnitRoomInputDto
    {
        public int unitItemID { get; set; }
        public string bathroom { get; set; }
        public string bedroom { get; set; }
    }
}
