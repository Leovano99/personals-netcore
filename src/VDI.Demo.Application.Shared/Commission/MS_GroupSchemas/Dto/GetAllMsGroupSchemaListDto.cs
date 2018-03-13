using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetAllMsGroupSchemaListDto
    {
        public string groupSchemaCode { get; set; }

        public string groupSchemaName { get; set; }

        public string projectName { get; set; }

        public bool isActive { get; set; }
    }
}
