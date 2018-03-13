using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataUnitInfoListDto
    {
        public string project { get; set; }
        public string category { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
    }
}
