using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Projects.Dto
{
    public class GetAllProjectListDto
    {
        public int Id { get; set; }
        public string projectCode { get; set; }

        public string projectName { get; set; }

        public string cityName { get; set; }

        public string teritoryName { get; set; }

        public bool isPublish { get; set; }

        public string image { get; set; }
        public string mapping { get; set; }
    }
}
