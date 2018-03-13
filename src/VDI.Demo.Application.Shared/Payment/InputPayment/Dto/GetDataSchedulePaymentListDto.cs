using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class GetDataSchedulePaymentListDto
    {
        public int allocID { get; set; }
        public DateTime dueDate { get; set; }
        public string allocCode { get; set; }
        public decimal netAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal netOutstanding { get; set; }
        public decimal VATOutstanding { get; set; }
        public short schedNo { get; set; }
        public double pctTax { get; set; }
    }
}
