using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personals.Dto
{
    public class GetMemberAllDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public string scmCode { get; set; }
        public string scmName { get; set; }
        public string memberCode { get; set; }
        public string parentMemberCode { get; set; }
        public string parentMemberName { get; set; }
        public string bankType { get; set; }
        public string bankCode { get; set; }
        public string bankAccNo { get; set; }
        public string bankAccName { get; set; }
        public string bankBranchName { get; set; }
        public long? bankAccountRefID { get; set; }
        public string specCode { get; set; }
        public string CDCode { get; set; }
        public string ACDCode { get; set; }
        public string PTName { get; set; }
        public string PrincName { get; set; }
        public string spouName { get; set; }
        public string password { get; set; }
        public string remarks1 { get; set; }
        public string memberStatusCode { get; set; }
        public bool isCD { get; set; }
        public bool isACD { get; set; }
        public bool isInstitusi { get; set; }
        public bool isPKP { get; set; }
        public bool isActive { get; set; }
        public bool isMember { get; set; }
        public string franchiseGroup { get; set; }
        public long? creatorUserId { get; set; }
        public DateTime? createdTime { get; set; }
        public DateTime? updatedTime { get; set; }
        public long? updatedBy { get; set; }
    }
}
