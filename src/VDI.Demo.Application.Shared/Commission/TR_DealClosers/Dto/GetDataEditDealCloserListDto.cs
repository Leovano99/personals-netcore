using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_DealClosers.Dto
{
    public class GetDataEditDealCloserListDto
    {
        public string propCode { get; set; }
        public string projectName { get; set; }
        public string devCode { get; set; }
        public string bookingCode { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public decimal unitPrice { get; set; }
        public string termRemarks { get; set; }
        public DateTime? ppjbDate { get; set; }
        public string memberCode { get; set; }
        public string name { get; set; }
        public string changeDealClosureReason { get; set; }

        public int? developerSchemaId { get; set; }
        public string entityCode { get; set; }
    }
}
