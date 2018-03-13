using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.OnlineBooking.Diagramatic.Dto
{
    public class GetDiagramaticForWebListDto
    {
        public int unitID { get; set; }

        public int unitCodeId { get; set; }

        public int detailID { get; set; }

        public string unitCode { get; set; }

        public string floor { get; set; }

        public List<UnitsDto> units { get; set; }

        public string message { get; set; }
    }

    public class UnitsDto
    {
        public int unitID { get; set; }

        public string unitNo { get; set; }

        public string unitStatusCode { get; set; }

        public string unitStatusName { get; set; }

        public string bedroom { get; set; }

        public string zoningName { get; set; }

        public string detailCode { get; set; }

        public string detailImage { get; set; }

        public int detailID { get; set; }

    }
}