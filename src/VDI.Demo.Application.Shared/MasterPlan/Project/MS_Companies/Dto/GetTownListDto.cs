using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Companies.Dto
{
    public class GetTownListDto
    {
        public int Id { get; set; }
        public int countryID { get; set; }
        public string townName { get; set; }
        public string townCode { get; set; }
    }
}
