using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreateTAXTrFPDetailInputDto
    {
        public string  entityCode   { get; set; }
        public string  coCode       { get; set; }
        public string  FPCode       { get; set; }
        public short   transNo      { get; set; }
        public string  currencyCode { get; set; }
        public decimal currencyRate { get; set; }
        public decimal transAmount  { get; set; }
        public string  transDesc    { get; set; }
    }
}
