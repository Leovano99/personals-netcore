using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class CreateMsSchemaRequirementInputDto
    {
        public int schemaID { get; set; }

        public List<setCommReq> setCommReq { get; set; }
    }

    public class setCommReq
    {
        public int? schemaRequirementID { get; set; }

        public short reqNo { get; set; }

        public string reqDesc { get; set; }

        public double pctPaid { get; set; }

        public bool isComplete { get; set; }
    }
}
