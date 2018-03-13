using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_UnitCodes.Dto
{
    public class CreateOrUpdateMsUnitCodeInputDto
    {
        public int? Id { get; set; }
        public string unitCode { get; set; }
        public string unitName { get; set; }
        public int projectID { get; set; }
    }
}
