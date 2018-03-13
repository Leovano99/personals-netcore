using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataMemberUplineInsertListDto
    {
        public string entityCode { get; set; }
        public string devCode { get; set; }
        public string bookNo { get; set; }
        public string memberCodeR { get; set; }
        public string memberCodeN { get; set; }
        public string commTypeCode { get; set; }
        public double? commPctPaid { get; set; }
        public double? pctPaid { get; set; }
        public int commTypeID { get; set; }
        public int developerSchemaID { get; set; }
        public int statusMemberID { get; set; }
        public int pointTypeID { get; set; }
        public int pphRangeInsID { get; set; }
        public int pphRangeID { get; set; }
        public short? asUplineNo { get; set; }
        //hanya untuk keperluan bentukan data tidak di insert
        public int schemaID { get; set; }
        public int groupSchemaID { get; set; }
        public bool? isStandart { get; set; }
        public string memberName { get; set; }
    }
}
