using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class GetAllMsSchemaListDto
    {
        public int schemaID { get; set; }

        public string scmCode { get; set; }

        public string scmName { get; set; }

        public int dueDateComm { get; set; }

        public bool isActive { get; set; }
    }
}
