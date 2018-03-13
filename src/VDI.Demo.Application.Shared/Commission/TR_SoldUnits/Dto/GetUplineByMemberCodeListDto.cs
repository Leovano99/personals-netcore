using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetUplineByMemberCodeListDto
    {
        public string scmCode { get; set; }
        public string memberCode { get; set; }
        public string memberName { get; set; }
        public string parentMemberCode { get; set; }
        public string parentMemberName { get; set; }
        public string ACDCode { get; set; }
        public short asUplineNo { get; set; }
        public int memberStatusID { get; set; }
        public string memberStatusCode { get; set; }
        public int commTypeID { get; set; }
        public string commTypeCode { get; set; }
        public int pointTypeID { get; set; }
        public int pphRangeID { get; set; }
        public int pphRangeInsID { get; set; }
    }
}
