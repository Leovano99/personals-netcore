using System;
namespace VDI.Demo.MasterPlan.Unit.MS_Items.Dto
{
    public class UpdateMsItemInput
    {
        public int Id { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public string shortName { get; set; }
        public bool isActive { get; set; }
    }
}
