using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataPersonalsListDto
    {
        public string psCode { get; set; }
        public string name { get; set; }
        public DateTime? birthDate { get; set; }
        public int age { get; set; }
        public List<string> phoneNo { get; set; }
    }
}
