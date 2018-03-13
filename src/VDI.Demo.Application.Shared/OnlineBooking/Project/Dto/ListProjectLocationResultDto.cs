using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class ListProjectLocationResultDto
    {
        public int ProjectLocationID { get; set; }

        public int projectInfoID { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public string projectAddress { get; set; }

        public string locationImageURL { get; set; }

        public string message { get; set; }
    }
}
