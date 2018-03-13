using System;
namespace VDI.Demo.MasterPlan.Unit.MS_UnitItems.Dto
{
    public class GetUnitItemListDto
    {
        public int unitItemID { get; set; }
        public int unitID { get; set; }
        public string unitNo { get; set; }
        public int itemID { get; set; }
        public string item { get; set; }
        public int coID { get; set; }
        public string coCode { get; set; }
        public string coName { get; set; }
        public string clusterCode { get; set; }
    }
}
