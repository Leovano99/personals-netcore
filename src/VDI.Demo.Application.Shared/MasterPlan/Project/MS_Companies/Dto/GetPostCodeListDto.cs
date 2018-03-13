using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Companies.Dto
{
    public class GetPostCodeListDto
    {
        public int Id { get; set; }
        public string postCode { get; set; }
        public string regency { get; set; }
        public string village { get; set; }
    }
}
