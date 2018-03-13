using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Commission.TR_DealClosers.Dto
{
    public class GetTasklistDealCloserByProjectListDto
    {
        public int soldUnitId { get; set; }
        public string projectName { get; set; }
        public string memberCode { get; set; }
        public string propCode { get; set; }
        public string devCode { get; set; }
        public string bookingCode { get; set; }
        public string clusterName { get; set; }
        public string unitCode { get; set; }
        public string unitNo { get; set; }
        public string status { get; set; }
    }
}
