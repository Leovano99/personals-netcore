using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class GetUnitByClusterListDto
    {
        public int unitID { get; set; }
        public int unitCodeID { get; set; }
        public int zoningID { get; set; }
        public int facingID { get; set; }
        public string unitNo { get; set; }
        public string unitCode { get; set; }
        public string facingName { get; set; }
    }
}
