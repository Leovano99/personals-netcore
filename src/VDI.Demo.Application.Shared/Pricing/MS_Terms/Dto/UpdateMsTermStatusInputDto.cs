using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Pricing.MS_Terms.Dto
{
    public class UpdateMsTermStatusInputDto
    {
        public int termMainID { get; set; }
        public int termNo { get; set; }
        public Boolean isActive { get; set; }
    }
}
