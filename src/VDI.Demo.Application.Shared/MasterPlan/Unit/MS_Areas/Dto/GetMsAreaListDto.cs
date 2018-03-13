using System;

namespace VDI.Demo.MasterPlan.Unit.MS_Areas.Dto
{
    public class GetMsAreaListDto
    {
        public int Id { get; set; }
        public string areaCode { get; set; }
        public int territoryID { get; set; }
        public string territoryName { get; set; }
        public int countyID { get; set; }
        public string countyName { get; set; }
        public int cityID { get; set; }
        public string cityName { get; set; }
        public string regionName { get; set; }
    }
}
