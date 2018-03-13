using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Personals.Personal_Members.Dto
{
    public class GetUpdateMemberInputDto
    {
        public string entityCode { get; set; }
        public string psCode { get; set; }
        public string scmCode { get; set; }
        public string memberCode { get; set; }
        #region member data
        public string parentMemberCode { get; set; }
        public string ptName { get; set; }
        public string princName { get; set; }
        public string spouName { get; set; }
        public string specialistCode { get; set; }
        public string franchiseGroup { get; set; }
        public bool isInstitusi { get; set; }

        public bool isCD { get; set; }
        public string vp { get; set; }

        public bool isACD { get; set; }
        public string ACDCode { get; set; }

        public string remarks { get; set; }
        #endregion
        #region activation
        public string memberStatusCode { get; set; }
        public bool isActive { get; set; }
        public bool isMember { get; set; }
        public string password { get; set; }
        #endregion
        #region bank data
        public string bankType { get; set; }
        public string bankCode { get; set; }
        public string bankBranchName { get; set; }
        public string bankAccountNo { get; set; }
        public string bankAccountName { get; set; }
        #endregion
    }
}
