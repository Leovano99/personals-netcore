using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class CreateOrUpdateSetPointTypeInputDto
    {
        public int schemaID { get; set; }

        public List<setPointType> setPointType { get; set; }
    }

    public class setPointType
    {
        public int? pointTypeID { get; set; }

        public string pointTypeCode { get; set; }

        public string pointTypeName { get; set; }

        public bool isComplete { get; set; }
    }
}
