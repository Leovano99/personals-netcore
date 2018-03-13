using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Email.Dto
{
    public class AfterReservedInputDto
    {
        public string customerName { get; set; }

        public string orderCode { get; set; }

        public string unitCode { get; set; }

        public string unitNo { get; set; }

        public DateTime? orderDate { get; set; }

        public string projectName { get; set; }

        public string clusterName { get; set; }

        public decimal BFAmount { get; set; }

        public string bankName { get; set; }

        public string vaNumber { get; set; }

        public DateTime? expiredDate { get; set; }

        public string memberName { get; set; }

        public string memberPhone { get; set; }

        public string devPhone { get; set; }

        public string marketingOffice { get; set; }

        public string projectImage { get; set; }


    }
}
