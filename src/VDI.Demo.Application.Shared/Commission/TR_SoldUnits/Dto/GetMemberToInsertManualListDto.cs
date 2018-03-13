using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetMemberToInsertManualListDto
    {
        public short? asUplineNo { get; set; }
        public string memberCode { get; set; }
        public string memberName { get; set; }
        public double? commPctPaid { get; set; }
        public decimal pctPaid { get; set; }
        public decimal commPaid { get; set; }
        public decimal actualPctComm { get; set; }
        public decimal actualPaid { get; set; }
        public DateTime reqDate { get; set; }
        public DateTime prodDate { get; set; }
    }
}
