using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_PointPercentage.Dto
{
    public class GetAllPointPctListDto
    {
        public int Id { get; set; }
        public string statusName { get; set; }
        public string statusCode { get; set; }
        public int statusMemberID { get; set; }
        public string pointTypeCode { get; set; }
        public string pointTypeName { get; set; }
        public int pointTypeID { get; set; }
        public Byte uplineNo { get; set; }
        public double pointPct { get; set; }
        public Decimal pointKonvert { get; set; }
        public int? schemaID { get; set; }
        public bool isActive { get; set; }
    }
}
