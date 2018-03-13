using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_Developer_Schemas.Dto
{
    public class CreateMsDeveloperSchemasInputDto
    {
        public string propCode { get; set; }
        public string propName { get; set; }
        public int schemaID { get; set; }
        public List<DataDev> setDataDev { get; set; }
    }

    public class DataDev
    {
        public string devCode { get; set; }
        public string devName { get; set; }
        public string bankCode { get; set; }
        public string bankAccountName { get; set; }
        public string bankBranchName { get; set; }
        public bool isActive { get; set; }
    }
}
