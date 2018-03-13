using System;
using System.Collections.Generic;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateUniversalSystemInputDto
    {
        public int projectID { get; set; }
        public int productID { get; set; }
        public int? renovationID { get; set; }
        public int unitStatusID { get; set; }
        public string generateType { get; set; }
        public string clusterCode { get; set; }
        public string clusterName { get; set; }
        public DateTime? dueDateDevelopment { get; set; }
        public string dueDateRemarks { get; set; }
        public string handOverPeriod { get; set; }
        public string gracePeriod { get; set; }
        public List<UnitDtoForSystem> unit { get; set; }
    }

    public class UnitDtoForSystem
    {
        public string unitTypeName { get; set; }

        public string roadName { get; set; }

        public float area { get; set; }

        public string remarks { get; set; }

        public DateTime? dueDate { get; set; }

        public int facingID { get; set; }

        public int zoningID { get; set; }

        public string floorName { get; set; }

        public string unitNo { get; set; }

        public string itemID { get; set; }

        public List<string> layout { get; set; }
    }
}
