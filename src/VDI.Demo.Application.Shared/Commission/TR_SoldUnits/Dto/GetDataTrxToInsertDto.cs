using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataTrxToInsertDto
    {
        public string entityCode { get; set; }

        public int schemaID { get; set; }

        public string propCode { get; set; }

        public string devCode { get; set; }

        public string projectCode { get; set; }

        public string clusterCode { get; set; }

        public string bookNo { get; set; }

        public string batchNo { get; set; }

        public string memberCode { get; set; }

        public string CDCode { get; set; }

        public string ACDCode { get; set; }

        public string roadCode { get; set; }

        public string roadName { get; set; }

        public string unitNo { get; set; }

        public DateTime bookDate { get; set; }

        public float unitLandArea { get; set; }

        public float unitBuildArea { get; set; }

        public decimal netNetPrice { get; set; }

        public decimal unitPrice { get; set; }

        public double pctComm { get; set; }

        public double pctBobot { get; set; }

        public DateTime? PPJBDate { get; set; }

        public DateTime? xreqInstPayDate { get; set; }

        public DateTime? xprocessDate { get; set; }

        public DateTime? cancelDate { get; set; }

        public string remarks { get; set; }

        public DateTime? holdDate { get; set; }

        public bool calculateUseMaster { get; set; }

        public string termRemarks { get; set; }

        public string holdReason { get; set; }

        public string changeDealClosureReason { get; set; }

        public int? developerSchemaID { get; set; }

        public int? unitID { get; set; }
    }
}
