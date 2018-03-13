using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class GetDiagramaticDto
    {
        public int unitID { get; set; }

        public string unitNo { get; set; }

        public string floor { get; set; }

        public string unitStatusCode { get; set; }

        public string unitStatusName { get; set; }

        public int? bedroom { get; set; }

        public string zoningName { get; set; }

        public int unitRoomID { get; set; }

        public string unitType { get; set; }

        public int zoningID { get; set; }

        public int clusterID { get; set; }

        public int projectID { get; set; }

        public string message { get; set; }
    }
}
