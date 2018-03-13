using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class ListProjectInfoResultDto
    {
        public int projectInfoID { get; set; }

        public int projectID { get; set; }

        public string projectDeveloper { get; set; }

        public string projectWebsite { get; set; }

        public string projectMarketingOffice { get; set; }

        public string projectMarketingPhone { get; set; }

        public string sitePlansImageUrl { get; set; }

        public string sitePlansLegend { get; set; }

        public bool projectStatus { get; set; }

        public string projectDesc { get; set; }

        public string message { get; set; }
    }
}
