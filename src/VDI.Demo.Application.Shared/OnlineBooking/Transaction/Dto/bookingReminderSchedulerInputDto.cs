using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Email.Dto
{
    public class bookingReminderSchedulerInputDto
    {
        public string projectImage { get; set; }

        public string customerName { get; set; }

        public string unitCode { get; set; }

        public string unitNo { get; set; }

        public DateTime orderDate { get; set; }

        public decimal bookingFee { get; set; }

        public string bankName { get; set; }

        public string vaNumber { get; set; }

        public string memberName { get; set; }

        public string memberPhone { get; set; }

        public string devPhone { get; set; }

        public string marketingOffice { get; set; }

        public string projectName { get; set; }
    }
}
