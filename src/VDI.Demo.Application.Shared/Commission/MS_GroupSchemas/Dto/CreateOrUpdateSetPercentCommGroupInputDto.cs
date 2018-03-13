using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class CreateOrUpdateSetPercentCommGroupInputDto
    {
        public int groupSchemaID { get; set; }

        public int? groupCommPctID { get; set; }

        public DateTime validDate { get; set; }

        public int commTypeID { get; set; }

        public int statusMemberID { get; set; }

        public Byte uplineNo { get; set; }

        public double? commPctPaid { get; set; }

        public decimal? nominal { get; set; }

        public bool isComplete { get; set; }

        public bool isStandard { get; set; }
    }
}
