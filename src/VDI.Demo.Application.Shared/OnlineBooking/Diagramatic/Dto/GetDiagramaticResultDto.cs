using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class GetDiagramaticResultDto
    {
        public int unitCodeId { get; set; }

        public string unitCode { get; set; }

        public string floor { get; set; }

        public List<UnitMobileDto> units { get; set; }

        public string message { get; set; }
    }

    public class UnitMobileDto
    {
        public string unitType { get; set; }

        public string unitNo { get; set; }

        public string unitStatusCode { get; set; }

        public string unitStatusName { get; set; }

        public int unitRoomID { get; set; }

        public int unitID { get; set; }

        public string bedroom { get; set; }

        public int zoningID { get; set; }

        public string zoningName { get; set; }

        public int itemID { get; set; }

        public int unitItemID { get; set; }

        public string detailCode { get; set; }

        public string detailImage { get; set; }

        public int detailID { get; set; }
    }
}
