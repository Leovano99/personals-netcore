using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class ReturnMsGroupSchemaDto
    {
        public int? groupSchemaID { get; set; }

        public int schemaID { get; set; }

        public bool? isComplete { get; set; }
    }
}
