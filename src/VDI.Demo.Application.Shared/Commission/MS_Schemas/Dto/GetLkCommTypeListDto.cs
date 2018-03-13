using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class GetLkCommTypeListDto
    {
        public int? commTypeID { get; set; }

        public string commTypeCode { get; set; }

        public string commTypeName { get; set; }
    }
}
