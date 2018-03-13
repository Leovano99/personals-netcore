using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetAllMsGroupCommPctListDto
    {
        public int? groupSchemaID { get; set; }

        public int? schemaID { get; set; }

        public string scmCode { get; set; }

        public DateTime validDate { get; set; }

        public int? commTypeID { get; set; }

        public string commTypeName { get; set; }

        public int? statusMemberID { get; set; }

        public string statusName { get; set; }

        public Byte uplineNo { get; set; }

        public double? commPctPaid { get; set; }

        public decimal? nominal { get; set; }

        public bool? isStandard { get; set; }

        public List<int> groupCommPctID { get; set; }
    }
}
