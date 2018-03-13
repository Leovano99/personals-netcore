using System;
using System.Collections.Generic;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateUnitTypeInputDto
    {
        public int facingID { get; set; }

        public int zoningID { get; set; }

        public string unitTypeCode { get; set; }

        public string unitTypeName { get; set; }

        public float? area { get; set; }

        public string remarks { get; set; }

        public DateTime? dueDate { get; set; }

        public List<String> layout { get; set; }
    }
}
