using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class CreateMsStatusMemberInputDto
    {
        public int schemaID { get; set; }

        public List<setStatusMember> setStatusMember { get; set; }
    }

    public class setStatusMember
    {
        public int? statusMemberID { get; set; }

        public string statusCode { get; set; }

        public string statusName { get; set; }

        public decimal pointMin { get; set; }

        public decimal pointToKeepStatus { get; set; }

        public int reviewTimeYear { get; set; }

        public int reviewStartMonth { get; set; }

        public byte statusStar { get; set; }

        public bool isComplete { get; set; }
    }
}
