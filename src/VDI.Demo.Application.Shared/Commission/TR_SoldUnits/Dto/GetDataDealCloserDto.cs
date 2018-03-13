using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataDealCloserDto
    {
        public int soldUnitID { get; set; }

        public int? schemaID { get; set; }

        public string scmCode { get; set; }

        public string scmName { get; set; }

        public string memberCode { get; set; }

        public string commTypeName { get; set; }

        public string statusCode { get; set; }

        public double commission { get; set; }

        public string pointType { get; set; }

        public double? pointPct { get; set; }
    }
}
