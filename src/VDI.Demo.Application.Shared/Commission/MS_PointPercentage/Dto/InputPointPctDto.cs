using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_PointPercentage.Dto
{
    public class InputPointPctDto
    {
        public int schemaID { get; set; }
        public int? pointPctID { get; set; }
        public Byte asUplineNo { get; set; }
        public double pointPct { get; set; }
        public Decimal pointKonvert { get; set; }
        public int statusMemberID { get; set; }
        public int pointTypeID { get; set; }
        public bool isActive { get; set; }
    }
}
