using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.PSAS.ChangeRenov.Dto
{
    public class GetRenovListDto
    {
        public string bookCode { get; set; }
        public int bookingHeaderId { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public int itemID { get; set; }
        public string renovCode { get; set; }
        public int termID { get; set; }
    }
}
