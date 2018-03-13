using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Projects.Dto
{
    public class GetUpdateDmtValueInputDto
    {
        public string query { get; set; }
        public string serverName { get; set; }
        public string dbName { get; set; }
        public string credentialUser { get; set; }
        public string credentialPass { get; set; }
    }
}
