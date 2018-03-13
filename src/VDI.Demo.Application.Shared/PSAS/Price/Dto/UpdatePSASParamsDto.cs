using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Dto;

namespace VDI.Demo.PSAS.Price.Dto
{
    public class UpdatePSASParamsDto
    {
        public GetPSASParamsDto paramsCheck { get; set; }

        public decimal grossPrice { get; set; }
    }
}
