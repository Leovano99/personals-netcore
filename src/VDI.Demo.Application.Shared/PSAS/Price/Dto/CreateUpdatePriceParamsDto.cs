using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.Price.Dto
{
    public class CreateUpdatePriceParamsDto
    {
        public GetPSASParamsDto paramsCheck { get; set; }

        public bool isAmount { get; set; }

        public List<DiscountDto> DiscountList { get; set; }

    }

    public class DiscountDto
    {
        public double pctDisc { get; set; }

        public decimal amountDisc { get; set; }

        public string addDiscDesc { get; set; }

        public short discNo { get; set; }

    }
}
