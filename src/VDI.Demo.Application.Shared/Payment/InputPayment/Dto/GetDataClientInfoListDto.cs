using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataClientInfoListDto
    {
        public string psCode { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string NPWP { get; set; }
        public List<GetPhone> listPhone { get; set; }
    }

    public class GetPhone
    {
        public string phone { get; set; }
    }
}
