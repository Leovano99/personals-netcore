﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.MS_ManagementPercentages.Dto
{
    public class InputManagementPctDto
    {
        public int? managementPctID { get; set; }

        public double managementPct { get; set; }

        public int schemaID { get; set; }

        public int developerSchemaID { get; set; }

        public bool isActive { get; set; }
    }
}
