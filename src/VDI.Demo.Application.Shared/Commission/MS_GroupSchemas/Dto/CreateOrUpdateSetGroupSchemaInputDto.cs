using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_GroupSchemas.Dto
{
    public class CreateOrUpdateSetGroupSchemaInputDto
    {
        public string groupSchemaCode { get; set; }

        public string groupSchemaName { get; set; }

        public DateTime validFrom { get; set; }

        public string documentGrouping { get; set; }

        public string documentGroupingDelete { get; set; }

        public string statusDocument { get; set; } //update, delete, nothing

        public int projectID { get; set; }

        public int clusterID { get; set; }

        public List<ReturnMsGroupSchemaDto> setSchema { get; set; }

        public bool isComplete { get; set; }

        public bool isActive { get; set; }
    }
}
