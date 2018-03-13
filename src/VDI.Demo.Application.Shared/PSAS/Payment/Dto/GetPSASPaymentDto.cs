using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Term.Dto
{
    public class GetPSASPaymentDto
    {
        public string bankName { get; set; }
        public string bankBranch { get; set; }
        public string account { get; set; }
        public string transNo { get; set; }
        public DateTime PMTDate { get; set; }
        public TypeDto type { get; set; }
        public DateTime? clearDate { get; set; }
        public decimal netAmount { get; set; }
        public decimal vatAmt { get; set; }
    }

    public class TypeDto
    {
        public string payFor { get; set; }
        public string payType { get; set; }
        public string otherType { get; set; }
    }
}
