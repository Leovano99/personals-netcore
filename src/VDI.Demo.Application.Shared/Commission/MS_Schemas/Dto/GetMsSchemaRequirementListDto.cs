using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class GetMsSchemaRequirementListDto
    {
        public int? schemaRequirementID { get; set; }

        public short reqNo { get; set; }

        public string reqDesc { get; set; }

        public double pctPaid { get; set; }
    }
}
