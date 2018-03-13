using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class GetDataDefineUnitListDto
    {
        public int soldUnitID { get; set; }

        public string propName { get; set; }

        public string propCode { get; set; }

        public string projectName { get; set; }

        public string developerName { get; set; }

        public string devCode { get; set; }

        public int developerSchemaID { get; set; }

        public string bookNo { get; set; }

        public string unitNo { get; set; }

        public string unitCode { get; set; }

        public decimal hargaUnit { get; set; }

        public string term { get; set; }

        public DateTime? PPJBDate { get; set; }
    }
}
