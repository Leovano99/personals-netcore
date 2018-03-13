using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataTasklistTRSoldUnitListDto
    {
        public int soldUnitID { get; set; }

        public string propName { get; set; }

        public string projectName { get; set; }

        public string developerName { get; set; }

        public string bookNo { get; set; }

        public DateTime bookDate { get; set; }

        public int dueDateComm { get; set; }

        public string clusterName { get; set; }

        public string unitNo { get; set; }

        public string unitCode { get; set; }

        public string status { get; set; }
    }
}
