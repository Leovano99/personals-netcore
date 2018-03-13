using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentLK_OthersType.Dto
{
    public class CreateOrUpdateLkOthersTypeInputDto
    {
        public int? Id { get; set; }
        public string othersTypeCode { get; set; }
        public string othersTypeDesc { get; set; }
        public bool isOthers { get; set; }
        public bool isOTP { get; set; }
        public bool isPayment { get; set; }
        public bool isAdjSAD { get; set; }
        public short sortNum { get; set; }
        public bool isActive { get; set; }
        public bool isSDH { get; set; }
    }
}
