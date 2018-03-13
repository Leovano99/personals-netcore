using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_DealClosers.Dto
{
    public class MemberFromPersonalInputDto
    {
        public string bookNo { get; set; }
        public string memberCode { get; set; }
        public string reason { get; set; }

        public string devCode { get; set; }
        public string entityCode { get; set; }
        public int developerSchemaID { get; set; }
    }
}
