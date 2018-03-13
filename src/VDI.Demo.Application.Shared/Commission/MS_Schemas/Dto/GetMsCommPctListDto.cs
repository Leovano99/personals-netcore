using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class GetMsCommPctListDto
    {
        public int commPctID { get; set; }

        public DateTime validDate { get; set; }

        public int commTypeID { get; set; }

        public string commTypeCode { get; set; }

        public string commTypeName { get; set; }

        public int statusMemberID { get; set; }

        public string statusCode { get; set; }

        public string statusName { get; set; }

        public Byte uplineNo { get; set; }

        public double? commPctPaid { get; set; }

        public decimal? nominal { get; set; }
    }
}
