using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class ListProjectImageGalleryResultDto
    {
        public int ProjectImageGalleryID { get; set; }

        public int projectInfoID { get; set; }

        public string imageURL { get; set; }

        public string imageAlt { get; set; }

        public bool imageStatus { get; set; }

        public string message { get; set; }
    }
}
