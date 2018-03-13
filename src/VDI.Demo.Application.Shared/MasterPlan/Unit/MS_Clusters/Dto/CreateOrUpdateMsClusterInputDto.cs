using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_Clusters.Dto
{
    public class CreateOrUpdateMsClusterInputDto
    {
        public int? Id { get; set; }
        public int projectID { get; set; }
        public string clusterCode { get; set; }
        public string clusterName { get; set; }
        public string handOverPeriod { get; set; }
        public string gracePeriod { get; set; }
        public int startPenaltyDay { get; set; }
        public double penaltyRate { get; set; }
        public int sortNo { get; set; }
    }
}
