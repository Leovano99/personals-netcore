using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateClusterInputDto
    {
        public int clusterID { get; set; }
        public string clusterCode { get; set; }
        public string clusterName { get; set; }
        public string generateType { get; set; }
        public string dueDateRemarks { get; set; }
        public short sortNo { get; set; }
    }
}
