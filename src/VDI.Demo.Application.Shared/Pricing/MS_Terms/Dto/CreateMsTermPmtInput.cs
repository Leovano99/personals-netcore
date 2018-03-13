using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class CreateMsTermPmtInput
    {
        public int entityID { get; set; }
        public int termID { get; set; }
        public short termNo { get; set; }
        public int finTypeID { get; set; }
        public short finStartDue { get; set; }

    }
}
