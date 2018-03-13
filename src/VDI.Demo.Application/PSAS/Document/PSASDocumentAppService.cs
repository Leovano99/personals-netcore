using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PSAS.Document.Dto;

namespace VDI.Demo.PSAS.Document
{
    public class PSASDocumentAppService : DemoAppServiceBase, IPSASDocumentAppService
    {
        private readonly IRepository<MS_DocumentPS> _msDocumentRepo;

        public PSASDocumentAppService(
            IRepository<MS_DocumentPS> msDocumentRepo
            )
        {
            _msDocumentRepo = msDocumentRepo;
        }

        public List<GetDocumentDropdownListDto> GetDocumentDropdown()
        {
            var getData = (from A in _msDocumentRepo.GetAll()
                           select new GetDocumentDropdownListDto
                           {
                               docID = A.Id,
                               docCode = A.docCode,
                               docName = A.docName
                           }).ToList();

            return getData;
        }
    }
}
