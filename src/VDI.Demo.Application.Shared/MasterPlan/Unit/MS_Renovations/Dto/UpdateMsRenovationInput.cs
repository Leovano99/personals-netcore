using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Renovations.Dto
{
    public class UpdateMsRenovationInput
    {
        public int Id { get; set; }
        public int projectID { get; set; }

        public string renovationName { get; set; }

        public string renovationCode { get; set; }

        public string detailName { get; set; }

        public string detailNameNew { get; set; }
        public string detailNameToDelete { get; set; }

        public bool isActive { get; set; }
    }
}
