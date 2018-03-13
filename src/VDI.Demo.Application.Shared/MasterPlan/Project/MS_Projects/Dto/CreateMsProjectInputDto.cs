using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Projects.Dto
{
    public class CreateMsProjectInputDto
    {
        public int Id { get; set; }
        public int entityID { get; set; }
        public string projectCode { get; set; }
        public string projectName { get; set; }
        public string image { get; set; }
        public string imageNew { get; set; }
        public bool isPublish { get; set; }
        public string OperationalGroup { get; set; }
        public string TaxGroup { get; set; }
        public bool isDMT { get; set; }
        public string DMT_ProjectGroupCode { get; set; }
        public string DMT_ProjectGroupName { get; set; }
        public int? callCenterManagerID { get; set; }
        public int? callCenterStaffID { get; set; }
        public int? bankRelationManagerID { get; set; }
        public int? bankRelationStaffID { get; set; }
        public int? SADBMID { get; set; }
        public int? SADManagerID { get; set; }
        public int? SADStaffID { get; set; }
        public int? PGManagerID { get; set; }
        public int? PGStaffID { get; set; }
        public int? financeManagerID { get; set; }
        public int? financeStaffID { get; set; }
        public int startPenaltyDay { get; set; }
        public int penaltyRate { get; set; }
        public int? SADBMStaffID { get; set; }
    }
}
