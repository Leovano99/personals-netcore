using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Renovations.Dto
{
    public class GetAllMsRenovationListDto
    {
        public int Id { get; set; }
        public int projectID { get; set; }

        public string renovationName { get; set; }

        public string renovationCode { get; set; }

        public string detail { get; set; }

        public bool isActive { get; set; }
        public string projectName { get; set; }
    }
}
