using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class UpdateUnitTypeInputDto
    {
        public int Id { get; set; }

        public int? facingID { get; set; }

        public int? zoningID { get; set; }

        public string unitTypeCode { get; set; }

        public float? area { get; set; }

        public int? jumlahKamarTidur { get; set; }

        public int? jumlahKamarMandi { get; set; }

        public string remarks { get; set; }

        public DateTime? dueDate { get; set; }
    }
}
