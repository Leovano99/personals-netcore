using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateUniversalExcelInputDto
    {
        public int projectID { get; set; }
        public int categoryID { get; set; }
        public int clusterID { get; set; }
        public int unitStatusID { get; set; }
        public int termMainID { get; set; }
        public string generateType { get; set; }
        public string excelFile { get; set; }
        public List<UnitDto> unit { get; set; }
    }

    public class UnitDto
    {
        public string unitNo { get; set; }

        public string unitCode { get; set; }

        public string combinedUnitNo { get; set; }

        public string areaCode { get; set; }

        public string productCode { get; set; }

        public string detailCode { get; set; }

        public string facingCode { get; set; }

        public string zoningCode { get; set; }

        public List<UnitItemDto> unitItem { get; set; }
    }

    public class UnitItemDto
    {
        public string dimension { get; set; }

        public string itemCode { get; set; }

        public double area { get; set; }

        [StringLength(50)]
        public string jumlahKamarTidur { get; set; }

        [StringLength(50)]
        public string jumlahKamarMandi { get; set; }
    }
}
