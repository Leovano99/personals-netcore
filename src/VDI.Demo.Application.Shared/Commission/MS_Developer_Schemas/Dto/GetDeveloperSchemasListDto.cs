using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Developer_Schemas.Dto
{
    public class GetDeveloperSchemasListDto
    {
        public int Id { get; set; }
        public string entityCode { get; set; }
        public string scmCode { get; set; }
        public string propCode { get; set; }
        public string devCode { get; set; }
        public string devName { get; set; }
        public int? schemaID { get; set; }
        public int? propertyID { get; set; }
        public string propName { get; set; }
        public string schemaName { get; set; }
        public string bankCode { get; set; }
        public string bankAccountName { get; set; }
        public string bankBranchName { get; set; }
        public bool isActive { get; set; }
    }
}
