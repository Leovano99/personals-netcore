using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Term.Dto
{
    public class GetPSASTermDto
    {
        public string termCode { get; set; }
        public short termNo { get; set; }
        public string remarksTerm { get; set; }
        public short PPJBDue { get; set; }
        public string DPCalcType { get; set; }
        public string finType { get; set; }
        public short finStatrtDue { get; set; }
        public string bankName { get; set; }
        public int unitID { get; set; }
        public int termID { get; set; }
    }
}
