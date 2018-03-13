using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.PSAS.Schedule.Dto;

namespace VDI.Demo.PSAS.Main.Dto
{
    public class GetPSASMainScheduleDto
    {
        public int allocID { get; set; }
        public DateTime dueDate { get; set; }
        public string allocCode { get; set; }
        public decimal netAmount { get; set; }
        public decimal VATAmount { get; set; }
        public decimal totalAmount { get; set; }
        public decimal netOutstanding { get; set; }
        public decimal VATOutstanding { get; set; }
        public decimal paymentAmount { get; set; }
        public decimal totalOutstanding { get; set; }
        public short schedNo { get; set; }
        public string remarks { get; set; }
        public int? penaltyAge { get; set; }
        public decimal? penaltyAmount { get; set; }
        public List<DataPaymentListDto> dataPayment { get; set; }
    }
}
