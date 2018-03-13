using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Price.Dto
{
    public class UpdatePriceInputDto
    {
        public decimal grossPrice { get; set; }

        public decimal NetPrice { get; set; }

        public decimal NetNetPrice { get; set; }

        public double pctDisc { get; set; }
    }
}
