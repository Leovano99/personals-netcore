using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Departments.Dto
{
    public class CreateOrUpdateMsDepartmentInput
    {
        public int? departmentID { get; set; }
        public string departmentName { get; set; }
        public string departmentCode { get; set; }
        public string departmentWhatsapp { get; set; }
        public string departmentEmail { get; set; }
        public Boolean isActive { get; set; }
    }
}
