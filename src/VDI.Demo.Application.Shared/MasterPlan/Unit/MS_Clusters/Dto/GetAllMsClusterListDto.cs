using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_Clusters.Dto
{
    public class GetAllMsClusterListDto
    {
        public int Id { get; set; }
        public string clusterCode { get; set; }
        public string clusterName { get; set; }
        public DateTime? dueDateDevelopment { get; set; }
        public string dueDateRemarks { get; set; }
        public string gracePeriod { get; set; }
        public string handOverPeriod { get; set; }
        public int sortNo { get; set; }
        public double penaltyRate { get; set; }
        public string projectName { get; set; }
        public int startPenaltyDay { get; set; }
    }
}
