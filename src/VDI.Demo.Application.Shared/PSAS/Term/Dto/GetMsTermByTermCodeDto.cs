using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Term.Dto
{
    public class GetMsTermByTermCodeDto
    {
        public int termID { get; set; }
        public short termNo { get; set; }
        public string remarks { get; set; }
    }
}
