using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GenerateJurnalInputDto
    {
        public string coCode { get; set; }
        public string accCode { get; set; }
        public string bookCode { get; set; }
        public string transNo { get; set; }
        public int payNo { get; set; }
    }
}
