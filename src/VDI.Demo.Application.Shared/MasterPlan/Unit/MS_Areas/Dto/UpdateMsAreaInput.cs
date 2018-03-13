using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Areas.Dto
{
    public class UpdateMsAreaInput
    {
        public int Id { get; set; }
        public string areaCode { get; set; }
        public int cityID { get; set; }
        public string regionName { get; set; }
    }
}
