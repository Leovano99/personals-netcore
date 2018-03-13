using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class CreateOrUpdateTRSoldUnitReqBulkInputDto
    {
        public int? Id { get; set; }
        public string entityCode { get; set; }
        public string devCode { get; set; }
        public string bookNo { get; set; }
        public string scmCode { get; set; }
        public byte reqNo { get; set; }
        public string reqDesc { get; set; }
        public double pctPaid { get; set; }
        public double? orPctPaid { get; set; }
        public DateTime? reqDate { get; set; }
        public DateTime? processDate { get; set; }
        public int? schemaID { get; set; }
        public int? developerSchemaID { get; set; }
    }
}
