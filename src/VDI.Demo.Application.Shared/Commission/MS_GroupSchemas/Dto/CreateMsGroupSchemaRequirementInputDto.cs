using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class CreateMsGroupSchemaRequirementInputDto
    {
        public string flag { get; set; } //add, edit

        public List<SetGroupReq> setGroupReq { get; set; }
    }

    public class SetGroupReq
    {
        public Byte reqNo { get; set; }

        public string reqDesc { get; set; }

        public double pctPaid { get; set; }

        public int groupSchemaID { get; set; }

        public bool isComplete { get; set; }
    }
}
