using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Departments.Dto
{
    public class GetMsDepartmentDropdownListDto
    {
        public int departmentID { get; set; }
        public string departmentName { get; set; }
        public string departmentCode { get; set; }
        public string departmentEmail { get; set; }
        public string departmentWhatsapp { get; set; }
    }
}
