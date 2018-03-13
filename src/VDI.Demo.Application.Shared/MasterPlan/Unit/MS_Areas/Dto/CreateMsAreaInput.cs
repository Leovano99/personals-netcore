using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Areas.Dto
{
    public class CreateMsAreaInput
    {
        public string entityCode { get; set; } //Dipakai di unit test saja
        public string areaCode { get; set; }
        public int cityID { get; set; }
        public string regionName { get; set; }
    }
}
