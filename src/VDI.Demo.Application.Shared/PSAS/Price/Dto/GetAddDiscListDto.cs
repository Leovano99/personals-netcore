using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.Price.Dto
{
    public class GetAddDiscListDto
    {
        public string addDiscDesc { get; set; }

        public int addDiscNo { get; set; }

        public bool isAmount { get; set; }

        public double pctAddDisc { get; set; }

        public decimal amtAddDisc { get; set; }
    }
}
