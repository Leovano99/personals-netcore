using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetDropDownSchemaByGroupSchemaIdListDto
    {
        public int groupSchemaID { get; set; }

        public int schemaID { get; set; }

        public string schemaCode { get; set; }

        public string schemaName { get; set; }
    }
}
