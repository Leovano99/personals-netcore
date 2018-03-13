using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class GetAllMsTermListDto
    {
        public int termMainID { get; set; }
        public int termID { get; set; }
        public string termCode { get; set; }
        public short termNo { get; set; }
        public short PPJBDue { get; set; }
        public string remarks { get; set; }
        public string projectName { get; set; }
        public bool isActive { get; set; }
    }
}
