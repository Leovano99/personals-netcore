using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class GetDetailMsGroupSchemaListDto
    {
        public string groupSchemaCode { get; set; }

        public string groupSchemaName { get; set; }

        public int projectID { get; set; }

        public string projectName { get; set; }

        public int clusterID { get; set; }

        public string clusterName { get; set; }

        public DateTime validFrom { get; set; }

        public string documentGrouping { get; set; }

        public bool isActive { get; set; }

        public List<getDataSchema> getDataSchema { get; set; }
    }

    public class getDataSchema
    {
        public int groupSchemaID { get; set; }

        public int schemaID { get; set; }

        public string scmCode { get; set; }

        public string scmName { get; set; }
    }
}
