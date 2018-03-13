using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class DetailProjectResultDto
    {
        public int projectID { get; set; }

        public string projectCode { get; set; }

        public string projectName { get; set; }

        public List<ClusterResultDto> cluster { get; set; }



        public string message { get; set; }

    }

    public class ClusterResultDto
    {
        public int clusterId { get; set; }

        public string clusterCode { get; set; }

        public string clusterName { get; set; }
    }
}
