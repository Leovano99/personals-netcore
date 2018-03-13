using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataAllMemberListDto
    {
        public int commPct { get; set; }
        public short asUplineNo { get; set; }
        public string memberCode { get; set; }
        public string memberName { get; set; }
        public double commPctPaid { get; set; }
        public decimal commPaid { get; set; }
        public double actualPctComm { get; set; }
        public decimal actualPaid { get; set; }
        public DateTime? reqDate { get; set; }
        public DateTime? prodDate { get; set; }
        public string commTypeCode { get; set; }
        public int commTypeID { get; set; }
        public string bookNo { get; set; }

    }
}
