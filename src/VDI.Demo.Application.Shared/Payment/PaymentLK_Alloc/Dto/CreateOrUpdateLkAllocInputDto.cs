using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentLK_Alloc.Dto
{
    public class GetLkAllocListDto
    {
        public int Id { get; set; }
        public string allocCode { get; set; }
        public string allocDesc { get; set; }
        public int payForId { get; set; }
        public bool isVat { get; set; }
        public bool isActive { get; set; }
        public string payFor { get; set; }
    }
}
