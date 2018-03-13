using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Entities.Dto
{
    public class GetAllMsEntityListDto
    {
        public int? Id { get; set; }
        public string entityName { get; set; }
        public string entityCode { get; set; }
    }
}
