using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateMsTermMainInput
    {
        public int Id { get; set; }
        public decimal BFAmount { get; set; }
        public string remarks { get; set; }
    }
}
