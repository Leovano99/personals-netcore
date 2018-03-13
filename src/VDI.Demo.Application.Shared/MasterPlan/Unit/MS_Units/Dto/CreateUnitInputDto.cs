using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Units.Dto
{
    public class CreateUnitInputDto
    {
        public int unitCodeID { get; set; }
        public int unitTypeID { get; set; }
        public int? blockID { get; set; }
        public int? towerID { get; set; }
        public int facingID { get; set; }
        public int zoningID { get; set; }
        public int productID { get; set; }
        public int unitStatusID { get; set; }
        public int clusterID { get; set; }
        public int projectID { get; set; }
        public string unitNo { get; set; }
        public string CombinedUnitNo { get; set; }
        public int areaID { get; set; }
        public int categoryID { get; set; }
        public int detailID { get; set; }
        public int rentalStatusID { get; set; }
        public string unitCertCode { get; set; }
        public int termMainID { get; set; }
        public string remarks { get; set; }
    }
}
