using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class CreateMsTermMainInput
    {
        public int entityID { get; set; }
        public string termCode { get; set; }
        public string termDesc { get; set; }
        public string famDiscCode { get; set; }
        public decimal BFAmount { get; set; }
        public string remarks { get; set; }
        public int? termID { get; set; }
    }
}
