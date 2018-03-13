using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetMsGroupSchemaRequirementListDto
    {
        public int groupSchemaID { get; set; }

        public int groupSchemaRequirementID { get; set; }

        public short reqNo { get; set; }

        public string reqDesc { get; set; }

        public double pctPaid { get; set; }

        public bool isComplete { get; set; }
    }
}
