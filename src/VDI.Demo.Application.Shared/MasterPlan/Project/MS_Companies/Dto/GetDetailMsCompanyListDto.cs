using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.MasterPlan.Project.MS_Companies.Dto
{
    public class GetDetailMsCompanyListDto
    {
        public CompanyInformationDto CompanyInformationDto { get; set; }
        public DocumentInformationDto DocumentInformationDto { get; set; }
        public List<BankInformationDto> BankInformationDto { get; set; }
    }

    public class CompanyInformationDto
    {
        public string coCode { get; set; }
        public string coName { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string faxNo { get; set; }
        public bool isActive { get; set; }
        public string image { get; set; }
        public int countryID { get; set; }
        public int townID { get; set; }
        public int postCodeID { get; set; }
        public string countryName { get; set; }
        public string townName { get; set; }
        public string postCode { get; set; }
    }

    public class DocumentInformationDto
    {
        public string npwp { get; set; }
        public string npwpAddress { get; set; }
        public string kppName { get; set; }
        public string kppTTD { get; set; }
        public string pkp { get; set; }
        public DateTime pkpDate { get; set; }
    }

    public class BankInformationDto
    {
        public string bankName { get; set; }
        public string branchName { get; set; }
        public string accName { get; set; }
        public string accNo { get; set; }
    }
}
