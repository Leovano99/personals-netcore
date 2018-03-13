using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class UpdateReleaseDateInputDto
    {
        public int unitID { get; set; }

        public int termID { get; set; }

        public int renovID { get; set; }

        public string psCode { get; set; }
    }
}
