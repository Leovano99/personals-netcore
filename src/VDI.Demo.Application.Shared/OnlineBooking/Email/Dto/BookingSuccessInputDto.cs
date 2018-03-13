using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Email.Dto
{
    public class BookingSuccessInputDto
    {
        public string projectImage { get; set; }

        public string customerName { get; set; }

        public DateTime bookDate { get; set; }

        public string devPhone { get; set; }

        public string memberName { get; set; }

        public string memberPhone { get; set; }

        public string projectName { get; set; }
    }
}
