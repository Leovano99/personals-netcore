using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class GetMsStatusMemberListDto
    {
        public int? statusMemberID { get; set; }

        public string statusCode { get; set; }

        public string statusName { get; set; }

        public decimal pointMin { get; set; }

        public decimal pointToKeepStatus { get; set; }

        public int reviewTimeYear { get; set; }

        public int reviewStartMonth { get; set; }

        public byte statusStar { get; set; }
    }
}
