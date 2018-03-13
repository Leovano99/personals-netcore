using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Officers.Dto
{
    public class GetOfficerDivDto
    {
        public DepartmentDto PSAS { get; set; }
        public DepartmentDto ProductGeneral { get; set; }
        public DepartmentDto Finance { get; set; }
        public DepartmentDto BankRelation { get; set; }
        public DepartmentDto CallCenter { get; set; }
        public DepartmentDto buildingManagement { get; set; }
    }

    public class DepartmentDto
    {
        public int departmentID { get; set; }
        public List<ManagerDto> manager { get; set; }
        public List<StaffDto> staff { get; set; }
    }

    public class StaffDto
    {
        public int staffID { get; set; }
        public string staffName { get; set; }
    }

    public class ManagerDto
    {
        public int managerID { get; set; }
        public string managerName { get; set; }
    }
}
