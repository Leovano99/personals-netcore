using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Schemas.Dto
{
    public class CreateOrUpdateSetSchemaInputDto
    {
        public int schemaID { get; set; }

        public string scmCode { get; set; }

        public string scmName { get; set; }

        public short upline { get; set; }

        public double? budgetPct { get; set; }

        public int dueDateComm { get; set; }

        public string uploadDocument { get; set; }

        public string uploadDocumentDelete { get; set; }

        public bool isComplete { get; set; }

        public bool isActive { get; set; }
    }
}
