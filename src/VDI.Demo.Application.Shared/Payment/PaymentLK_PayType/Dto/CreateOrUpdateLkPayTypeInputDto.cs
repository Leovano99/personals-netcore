using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentLK_PayType.Dto
{
    public class CreateOrUpdateLkPayTypeInputDto
    {
        public int? Id { get; set; }
        public string payTypeCode { get; set; }
        public string payTypeDesc { get; set; }
        public bool isIncome { get; set; }
        public bool isInventory { get; set; }
        public bool isBooking { get; set; }
        public bool isActive { get; set; }
    }
}
