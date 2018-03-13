using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Projects.Dto
{
    public class GetDetailMsProjectListDto
    {
        public ProjectInformationListDto ProjectInformation { get; set; }

        public PICInformationListDto PICInformation { get; set; }

    }

    public class ProjectInformationListDto
    {
        public int? entityID { get; set; }
        public string entityName { get; set; }
        public bool isDMT { get; set; }
        public string projectGroupCode { get; set; }
        public string projectGroupName { get; set; }
        public string projectCode { get; set; }
        public string projectName { get; set; }
        public string image { get; set; }
        public bool isPublish { get; set; }
        public double penaltyRate { get; set; }
        public int startPenaltyDay { get; set; }
        public string operationalGroupEntityCode { get; set; }
        public string operationalGroup { get; set; }
        public string taxGroupEntityCode { get; set; }
        public string taxGroup { get; set; }
    }

    public class PICInformationListDto
    {
        public PSASListDto PSAS { get; set; }
        public PGListDto PG { get; set; }
        public FinanceListDto Finance { get; set; }
        public BankRelationListDto BankRelation { get; set; }
        public CallCenterListDto CallCenter { get; set; }
        public BuildingManagerListDto BuildingManager { get; set; }
    }

    public class PSASListDto
    {
        public int managerID { get; set; }
        public string managerName { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string departementWhatsapp { get; set; }
        public string departementEmail { get; set; }
        public string officerPhone { get; set; }
    }

    public class PGListDto
    {
        public int picID { get; set; }
        public string PIC { get; set; }
        public string departementWhatsapp { get; set; }
        public string departementEmail { get; set; }
        public string officerPhone { get; set; }
    }

    public class FinanceListDto
    {
        public int managerID { get; set; }
        public string managerName { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string departementWhatsapp { get; set; }
        public string departementEmail { get; set; }
        public string officerPhone { get; set; }
    }

    public class BankRelationListDto
    {
        public int managerID { get; set; }
        public string managerName { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string departementWhatsapp { get; set; }
        public string departementEmail { get; set; }
        public string officerPhone { get; set; }
    }

    public class CallCenterListDto
    {
        public int managerID { get; set; }
        public string managerName { get; set; }
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string departementWhatsapp { get; set; }
        public string departementEmail { get; set; }
        public string officerPhone { get; set; }
    }

    public class BuildingManagerListDto
    {
        public int managerID { get; set; }
        public string managerName { get; set; }
        //manda added
        public int staffID { get; set; }
        public string staffName { get; set; }
        public string departementWhatsapp { get; set; }
        public string departementEmail { get; set; }
        public string officerPhone { get; set; }
    }
}
