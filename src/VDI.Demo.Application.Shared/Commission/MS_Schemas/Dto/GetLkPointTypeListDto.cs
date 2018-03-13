using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class GetLkPointTypeListDto
    {
        public int? pointTypeID { get; set; }

        public string pointTypeCode { get; set; }

        public string pointTypeName { get; set; }
    }
}
