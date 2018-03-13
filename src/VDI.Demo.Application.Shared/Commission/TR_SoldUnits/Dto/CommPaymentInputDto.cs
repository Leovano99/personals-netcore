using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class CommPaymentInputDto
    {
        public string propCode { get; set; }
        public string devCode { get; set; }
        public string bookNo { get; set; }
        public string memberCode { get; set; }
        public string commTypeCode { get; set; }
        public int schemaId { get; set; }
        public int commTypeId { get; set; }
        public int developerSchemaID { get; set; }
    }
}
