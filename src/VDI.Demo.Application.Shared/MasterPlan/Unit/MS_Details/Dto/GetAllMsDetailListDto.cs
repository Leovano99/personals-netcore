using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Unit.MS_Details.Dto
{
    public class GetAllMsDetailListDto
    {
        public int Id { get; set; }
        public string detailCode { get; set; }
        public string detailName { get; set; }
        public string detailImage { get; set; }
    }
}
