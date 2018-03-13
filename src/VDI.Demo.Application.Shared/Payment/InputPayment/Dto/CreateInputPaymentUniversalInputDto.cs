using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Payment.InputPayment.Dto
{
    public class CreateInputPaymentUniversalInputDto
    {
        //paymentHeader
        public int accountID { get; set; }
        public int? bookingHeaderID { get; set; }
        public int? payForID { get; set; }
        public DateTime? clearDate { get; set; }
        public string description { get; set; }
        public DateTime paymentDate { get; set; }
        public string transNo { get; set; }

        public double pctTax { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string psCode { get; set; }
        public string NPWP { get; set; }

        public List<CreatePaymentSchedule> dataSchedule { get; set; }

        //paymentDetail
        public List<CreatePaymentDetail> dataPaymentDetail { get; set; }

    }

    public class CreatePaymentSchedule
    {
        public decimal netAmtSchedule { get; set; }
        public decimal vatAmtSchedule { get; set; }
        public decimal netOutSchedule { get; set; } //biasanya 0
        public decimal vatOutSchedule { get; set; } //biasanya 0
        public short schedNoSchedule { get; set; }
    }

    public class CreatePaymentDetail
    {
        public string chequeNo { get; set; }
        public int payNo { get; set; }
        public string othersTypeCode { get; set; }
        public DateTime dueDate { get; set; }
        public int payTypeID { get; set; }
        public string bankName { get; set; }
        public string ket { get; set; }
        public decimal totalAmountDetail { get; set; }
        //paymentDetailAlloc
        public List<CreatePaymentAlloc> dataAlloc { get; set; }
    }

    public class CreatePaymentAlloc
    {
        public decimal netAmt { get; set; }
        public decimal vatAmt { get; set; }
        public decimal netOut { get; set; } //biasanya 0
        public decimal vatOut { get; set; } //biasanya 0
        public short schedNo { get; set; }
    }
}
