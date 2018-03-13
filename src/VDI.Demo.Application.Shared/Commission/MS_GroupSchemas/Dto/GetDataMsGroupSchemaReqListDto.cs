using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetDataMsGroupSchemaReqListDto
    {
        public short reqNo { get; set; }

        public string reqDesc { get; set; }

        public double pctPaid { get; set; }
    }
}
