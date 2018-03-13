using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Project.Dto
{
    public class GetListPromotionResultDto
    {
        public int projectID { get; set; }
        
        public string promoFile { get; set; }
        
        public string promoAlt { get; set; }
        
        public string promoDataType { get; set; }
        
        public string targetURL { get; set; }

        public bool isActive { get; set; }
    }
}
