using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataTransNoInputDto
    {
        public string transNo { get; set; }
        public string status { get; set; }
        public int accID { get; set; }
        public int payForID { get; set; }
    }
}
