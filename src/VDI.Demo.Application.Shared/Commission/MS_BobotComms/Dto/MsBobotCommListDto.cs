using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_BobotComms.Dto
{
    public class MsBobotCommListDto
    {
        public int Id { get; set; }
        public string entityCode { get; set; }
        public string scmCode { get; set; }
        public string projectCode { get; set; }
        public string clusterCode { get; set; }
        public int clusterID { get; set; }
        public int projectID { get; set; }
        public int schemaID { get; set; }
        public int entityID { get; set; }
        public double pctBobot { get; set; }
        public bool isActive { get; set; }
        public bool isComplete { get; set; }
        public string clusterName { get; set; }
        public string projectName { get; set; }
    }
}
