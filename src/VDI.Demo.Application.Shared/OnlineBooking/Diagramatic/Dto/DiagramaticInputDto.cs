using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class DiagramaticInputDto
    {
        public int projectId { get; set; }

        public int clusterId { get; set; }

        public int? detailID { get; set; }
    }
    public class DiagramaticMobileInputDto
    {
        public int projectId { get; set; }

        public int clusterId { get; set; }

        public string bedroom { get; set; }

        public string unitType { get; set; }

        public int zoningID { get; set; }
    }
}
