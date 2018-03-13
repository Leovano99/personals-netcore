using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateUnitCodeInputDto
    {
        public string floor { get; set; }
        public string roadName { get; set; }
        public string generateType { get; set; }
        public string clusterCode { get; set; }
        public string unitCode { get; set; }
    }
}
