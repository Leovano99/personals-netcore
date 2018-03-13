using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Renovations.Dto
{
    public class CreateMsRenovationInput
    {
        public int projectID { get; set; }
        public string renovationName { get; set; }

        public string renovationCode { get; set; }

        public string detailName { get; set; }

        public bool isActive { get; set; }
    }
}
