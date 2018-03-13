using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class CreateOrUpdateSetCommTypeInputDto
    {
        public int schemaID { get; set; }

        public List<setCommType> setCommType { get; set; }
    }

    public class setCommType
    {
        public int? commTypeID { get; set; }

        public string commTypeCode { get; set; }

        public string commTypeName { get; set; }

        public bool isComplete { get; set; }
    }
}
