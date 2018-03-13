using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class CreateOrUpdateMsCommPctInputDto
    {
        public int schemaID { get; set; }

        public List<setCommPct> setCommPct { get; set; }
    }

    public class setCommPct
    {
        public int? commPctID { get; set; }

        public DateTime validDate { get; set; }

        public int commTypeID { get; set; }

        public int statusMemberID { get; set; }

        public Byte uplineNo { get; set; }

        public double? commPctPaid { get; set; }

        public decimal? nominal { get; set; }

        public Boolean isComplete { get; set; }

    }
}
