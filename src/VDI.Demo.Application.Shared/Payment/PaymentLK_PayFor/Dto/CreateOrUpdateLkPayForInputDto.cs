using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.PaymentLK_PayFor.Dto
{
    public class CreateOrUpdateLkPayForInputDto
    {
        public int? Id { get; set; }
        public string payForCode { get; set; }
        public string payForName { get; set; }
        public bool isSched { get; set; }
        public bool isIncome { get; set; }
        public bool isInventory { get; set; }
        public bool isSDH { get; set; }
        public bool isActive { get; set; }
    }
}
