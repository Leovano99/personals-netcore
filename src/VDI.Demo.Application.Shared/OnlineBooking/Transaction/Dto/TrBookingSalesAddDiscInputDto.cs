﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Transaction.Dto
{
    public class TrBookingSalesAddDiscInputDto
    {
        public int unitID { get; set; }

        public int termID { get; set; }

        public int bookingHeaderID { get; set; }
    }
}
