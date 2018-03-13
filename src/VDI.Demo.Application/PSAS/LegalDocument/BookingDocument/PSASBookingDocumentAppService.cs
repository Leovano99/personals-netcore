using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VDI.Demo.Files;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PSAS.Dto;
using VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto;
using VDI.Demo.PSAS.Price;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.AutoMapper;
using System.Globalization;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.Pricing;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.EntityFrameworkCore;
using static VDI.Demo.PSAS.LegalDocument.BookingDocument.Dto.KonfirmasiPesananLegalDocDto;
using System.Net.Http;
using System.Net.Http.Headers;
using Abp.AspNetZeroCore.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;
using VDI.Demo.OnlineBooking.PaymentMidtrans.Dto;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.PSAS.LegalDocument.BookingDocument
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_BookingDocument)]
    public class PSASBookingDocumentAppService : DemoAppServiceBase, IPSASBookingDocumentAppService
    {
        private readonly IRepository<MS_KuasaDireksi> _msKuasaDireksiRepo;
        private readonly IRepository<MS_KuasaDireksiPeople> _msKuasaDireksiPeopleRepo;
        private readonly IRepository<MS_MappingTemplate> _msMappingTemplateRepo;
        private readonly IRepository<MS_DocumentPS> _msDocumentPsRepo;
        private readonly IRepository<MS_DocTemplate> _msDocTemplateRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IFilesHelper _iFilesHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetailRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<TR_BookingDocument> _trBookingDocumentRepo;
        private readonly IRepository<MS_Company> _msCompanyRepo;
        private readonly IRepository<DocNo_Counter> _docNoCounterRepo;
        private readonly IRepository<PERSONALS, string> _personalsRepo;
        private readonly IRepository<TR_Address, string> _trAddressRepo;
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;
        private readonly IRepository<TR_ID, string> _trIdRepo;
        private readonly IRepository<TR_Email, string> _trEmailRepo;
        private readonly IPSASPriceAppService _ipriceAppService;

        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_TermMain> _msTermMainRepo;
        private readonly IRepository<TR_UnitOrderDetail> _trUnitOrderDetailRepo;
        private readonly IRepository<TR_UnitOrderHeader> _trUnitOrderHeaderRepo;
        private readonly IRepository<LK_AddrType, string> _lkAddrTypeRepo;
        private readonly IRepository<LK_IDType, string> _lkIdTypeRepo;
        private readonly IRepository<TR_BookingDetailSchedule> _trBookingDetailScheduleRepo;
        private readonly IRepository<MS_UnitItem> _msUnitItemRepo;

        private readonly PersonalsNewDbContext _personalContext;
        private readonly PropertySystemDbContext _propertySystemContext;
        private readonly NewCommDbContext _newCommContext;

        private readonly IConfigurationRoot _appConfiguration;

        public PSASBookingDocumentAppService(
            IRepository<MS_KuasaDireksi> msKuasaDireksiRepo,
            IRepository<MS_KuasaDireksiPeople> msKuasaDireksiPeopleRepo,
            IRepository<MS_MappingTemplate> msMappingTemplateRepo,
            IRepository<MS_DocumentPS> msDocumentPsRepo,
            IRepository<MS_DocTemplate> msDocTemplateRepo,
            IRepository<MS_Project> msProjectRepo,
            IFilesHelper iFilesHelper,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<TR_BookingDetail> trBookingDetailRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<TR_BookingDocument> trBookingDocumentRepo,
            IRepository<MS_Company> msCompanyRepo,
            IRepository<DocNo_Counter> docNoCounterRepo,
            IRepository<PERSONALS, string> personalsRepo,
            IRepository<TR_Address, string> trAddressRepo,
            IRepository<TR_Phone, string> trPhoneRepo,
            IRepository<TR_ID, string> trIdRepo,
            IRepository<TR_Email, string> trEmailRepo,
            IPSASPriceAppService ipriceAppService,

            IRepository<MS_Term> msTermRepo,
            IRepository<MS_TermMain> msTermMainRepo,
            IRepository<TR_UnitOrderDetail> trUnitOrderDetailRepo,
            IRepository<TR_UnitOrderHeader> trUnitOrderHeaderRepo,
            IRepository<LK_AddrType, string> lkAddrTypeRepo,
            IRepository<LK_IDType, string> lkIdTypeRepo,
            IRepository<TR_BookingDetailSchedule> trBookingDetailScheduleRepo,
            IRepository<MS_UnitItem> msUnitItemRepo,

            PersonalsNewDbContext personalContext,
            PropertySystemDbContext propertySystemContext,
            NewCommDbContext newCommContext
            )
        {
            _msKuasaDireksiRepo = msKuasaDireksiRepo;
            _msKuasaDireksiPeopleRepo = msKuasaDireksiPeopleRepo;
            _trBookingDocumentRepo = trBookingDocumentRepo;
            _msUnitRepo = msUnitRepo;
            _msClusterRepo = msClusterRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _trBookingDetailRepo = trBookingDetailRepo;
            _ipriceAppService = ipriceAppService;
            _msMappingTemplateRepo = msMappingTemplateRepo;
            _msDocumentPsRepo = msDocumentPsRepo;
            _msDocTemplateRepo = msDocTemplateRepo;
            _msProjectRepo = msProjectRepo;
            _msCompanyRepo = msCompanyRepo;
            _docNoCounterRepo = docNoCounterRepo;
            _personalsRepo = personalsRepo;
            _trAddressRepo = trAddressRepo;
            _trPhoneRepo = trPhoneRepo;
            _trIdRepo = trIdRepo;
            _iFilesHelper = iFilesHelper;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;

            _msTermRepo = msTermRepo;
            _msTermMainRepo = msTermMainRepo;
            _trUnitOrderDetailRepo = trUnitOrderDetailRepo;
            _trUnitOrderHeaderRepo = trUnitOrderHeaderRepo;
            _lkAddrTypeRepo = lkAddrTypeRepo;
            _lkIdTypeRepo = lkIdTypeRepo;
            _trBookingDetailScheduleRepo = trBookingDetailScheduleRepo;
            _msUnitItemRepo = msUnitItemRepo;

            _newCommContext = newCommContext;
            _propertySystemContext = propertySystemContext;
            _personalContext = personalContext;

            _appConfiguration = hostingEnvironment.GetAppConfiguration();
        }

        public GetBookingDocumentListDto GetBookingDocument(GetPSASParamsDto input)
        {
            var UnitID = _ipriceAppService.GetParameter(input);
            var getBookingDocument = (from x in _trBookingHeaderRepo.GetAll()
                                            join a in _msUnitRepo.GetAll() on x.unitID equals a.Id
                                            join b in _msUnitCodeRepo.GetAll() on a.unitCodeID equals b.Id
                                            join c in _msProjectRepo.GetAll() on a.projectID equals c.Id
                                            join d in _msClusterRepo.GetAll() on a.clusterID equals d.Id
                                            where x.Id == UnitID.bookingHeaderID
                                            select new GetBookingDocumentListDto
                                            {
                                                bookCode = x.bookCode,
                                                projectID = c.Id,
                                                projectName = c.projectName,
                                                clusterName = d.clusterName,
                                                unitCode = b.unitCode,
                                                unitNo = a.unitNo,
                                                bookingDate = x.bookDate
                                            }).FirstOrDefault();

            var getBookingDocumentData = (from x in _trBookingDocumentRepo.GetAll()
                                          join a in _msDocumentPsRepo.GetAll() on x.docID equals a.Id
                                          join b in _msMappingTemplateRepo.GetAll() on a.Id equals b.docID
                                          where b.projectID == getBookingDocument.projectID &&
                                          x.docDate >= b.activeFrom && 
                                          ((b.activeTo != null && x.docDate <= b.activeTo) ? (b.activeTo != null && x.docDate <= b.activeTo) : b.activeTo == null) &&
                                          b.isActive && x.bookingHeaderID == UnitID.bookingHeaderID
                                          select new BookingDocumentListDto
                                          {
                                              bookingDocumentId = x.Id,
                                              docCode = a.docCode,
                                              docName = a.docName,
                                              docNo = x.docNo,
                                              docDate = x.docDate,
                                              remarks = x.remarks,
                                              isTandaTerima = b.isTandaTerima,
                                              fileTandaTerima = (x != null && x.tandaTerimaFile != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.tandaTerimaFile) : null,
                                          })
                                          .ToList();

            getBookingDocument.bookingDoc = getBookingDocumentData;

            return getBookingDocument;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_PSAS_LegalDocument_MappingTemplate_Create)]
        public void CreateBookingDocument(CreateBookingDocumentInputDto input)
        {
            Logger.InfoFormat("CreateBookingDocument() Started.");

            Logger.InfoFormat("CreateBookingDocument() Check DocCode.");
            
            GetPSASParamsDto inputPSAS = new GetPSASParamsDto();
            inputPSAS.bookCode = input.bookCode;
            inputPSAS.unitCode = input.unitCode;
            inputPSAS.unitNo = input.unitNo;
            var UnitID = _ipriceAppService.GetParameter(inputPSAS);

            var checkTrBookingDocument = (from A in _trBookingDocumentRepo.GetAll()
                                        where A.entityID == 1 && 
                                        A.docID == input.docID &&
                                        A.bookingHeaderID == UnitID.bookingHeaderID
                                        select A).OrderByDescending(a => a.LastModificationTime != null ? a.LastModificationTime : a.CreationTime);

            var checkTrBookingDocumentDocDate = (from A in _trBookingDocumentRepo.GetAll()
                                          where A.entityID == 1 &&
                                          A.docID == input.docID &&
                                          A.bookingHeaderID == UnitID.bookingHeaderID &&
                                          A.docDate.Date == input.docDate.Date
                                          select A).OrderByDescending(a => a.LastModificationTime != null ? a.LastModificationTime : a.CreationTime);

            Logger.InfoFormat("CreateBookingDocument() End Check DocCode.");

            var getProjectID = (from A in _msUnitRepo.GetAll()
                                where A.Id == UnitID.unitID
                                select A.projectID).FirstOrDefault();
            var getBookingDetail = (from A in _trBookingDetailRepo.GetAll()
                                    where A.bookingHeaderID == UnitID.bookingHeaderID && A.refNo == 1
                                    select A).FirstOrDefault();
            var getCoID = (from A in _msCompanyRepo.GetAll()
                           where A.coCode == getBookingDetail.coCode
                           select A).FirstOrDefault();
            if (getCoID.Id == 0)
            {
                throw new UserFriendlyException("Company with CoCode: " + getBookingDetail.coCode + " not found !");
            }

            var param = new TR_BookingDocument
            {
                entityID = 1,
                bookingHeaderID = (int)UnitID.bookingHeaderID,
                docID = input.docID,
                oldDocNo = input.oldDocNo,
                remarks = input.remarks,
                tandaTerimaBy = input.tandaTerimaBy,
                tandaTerimaDate = input.tandaTerimaDate,
                tandaTerimaFile = input.tandaTerimaFile,
                docDate = DateTime.Now
            };

            var getMappingTemplate = (from a in _msMappingTemplateRepo.GetAll()
                                      where a.entityID == 1 &&
                                      a.projectID == getProjectID &&
                                      a.docID == input.docID &&
                                      input.docDate >= a.activeFrom &&
                                      ((a.activeTo != null && input.docDate <= a.activeTo) ? (a.activeTo != null && input.docDate <= a.activeTo) : a.activeTo == null) &&
                                      a.isActive
                                      select new
                                      {
                                          a.Id,
                                          a.isTandaTerima,
                                          a.CreationTime,
                                          a.LastModificationTime
                                      })
                                      .OrderByDescending(a => a.LastModificationTime != null ? a.LastModificationTime : a.CreationTime);

            if (!checkTrBookingDocumentDocDate.Any())
            {
                //TODO: OldDocNo jika kena validasi tgl tanda terima lebih dr doc date, throw error, update remarks jadi invalid, docno.
                if (checkTrBookingDocument.Any())
                {
                    var checkRemarks = (from A in _trBookingDocumentRepo.GetAll()
                                        where A.entityID == 1 &&
                                        A.bookingHeaderID == UnitID.bookingHeaderID &&
                                        A.docID == input.docID
                                        select A).OrderByDescending(a => a.LastModificationTime != null ? a.LastModificationTime : a.CreationTime).FirstOrDefault();

                    if (getMappingTemplate.Any() && getMappingTemplate.FirstOrDefault().isTandaTerima)
                    {
                        if (checkTrBookingDocument.FirstOrDefault().tandaTerimaFile != null)
                        {
                            throw new UserFriendlyException("Transaction already exist !");
                        }
                        else
                        {
                            param.oldDocNo = checkRemarks.docNo;
                        }
                    }
                    else
                    {
                        throw new UserFriendlyException("Transaction already exist !");
                    }
                }

                var docCounter = CounterDocNo(input.projectID, input.docID, getCoID.Id);
                param.docNo = docCounter.docNo;
                param.tandaTerimaNo = docCounter.tandaTerimaNo;

                Logger.DebugFormat("CreateBookingDocument() - Start Preparation Data For Insert TR_BookingDocument. Parameters sent: {0} " +
                    "   entityID = 1" +
                    "   bookingHeaderID = {1}{0}" +
                    "   docID = {2}{0}" +
                    "   docNo = {3}{0}" +
                    "   oldDocNo = {4}{0}" +
                    "   remarks = {5}{0}" +
                    "   tandaTerimaBy = {5}{0}" +
                    "   tandaTerimaDate = {5}{0}" +
                    "   tandaTerimaNo = {5}{0}" +
                    "   tandaTerimaFile = {5}{0}"
                    , Environment.NewLine, UnitID.bookingHeaderID, input.docID, input.docNo, input.oldDocNo, input.remarks, input.tandaTerimaBy, input.tandaTerimaDate, input.tandaTerimaNo, input.tandaTerimaFile);
                _trBookingDocumentRepo.Insert(param);
                Logger.DebugFormat("CreateBookingDocument() - End Insert TR_BookingDocument");
            }
            else
            {
                var oldData = checkTrBookingDocumentDocDate.FirstOrDefault();
                param.docNo = oldData.docNo;
                param.tandaTerimaNo = oldData.tandaTerimaNo;
            }
            
            //TODO: Generate File
            Logger.DebugFormat("GeneratePPPU() - Start Generate. Parameters sent: {0} " +
                "   docNo = {1}{0}" +
                "   bookingHeaderID = {2}{0}" +
                "   procurationName = {3}{0}" +
                "   procurationType = {4}{0}"
                , Environment.NewLine, param.docNo, param.bookingHeaderID, input.procurationName, input.procurationType);
            
            var base64Image = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAgEBLAEsAAD/7QaCUGhvdG9zaG9wIDMuMAA4QklNA+0KUmVzb2x1dGlvbgAAAAAQASwAAAABAAEBLAAAAAEAAThCSU0EDRhGWCBHbG9iYWwgTGlnaHRpbmcgQW5nbGUAAAAABAAAAB44QklNBBkSRlggR2xvYmFsIEFsdGl0dWRlAAAAAAQAAAAeOEJJTQPzC1ByaW50IEZsYWdzAAAACQAAAAAAAAAAAQA4QklNBAoOQ29weXJpZ2h0IEZsYWcAAAAAAQAAOEJJTScQFEphcGFuZXNlIFByaW50IEZsYWdzAAAAAAoAAQAAAAAAAAACOEJJTQP1F0NvbG9yIEhhbGZ0b25lIFNldHRpbmdzAAAASAAvZmYAAQBsZmYABgAAAAAAAQAvZmYAAQChmZoABgAAAAAAAQAyAAAAAQBaAAAABgAAAAAAAQA1AAAAAQAtAAAABgAAAAAAAThCSU0D+BdDb2xvciBUcmFuc2ZlciBTZXR0aW5ncwAAAHAAAP////////////////////////////8D6AAAAAD/////////////////////////////A+gAAAAA/////////////////////////////wPoAAAAAP////////////////////////////8D6AAAOEJJTQQIBkd1aWRlcwAAAAAQAAAAAQAAAkAAAAJAAAAAADhCSU0EHg1VUkwgb3ZlcnJpZGVzAAAABAAAAAA4QklNBBoGU2xpY2VzAAAAAGkAAAAGAAAAAAAAAAAAAACWAAAA7QAAAAQAQwBDAEIAMgAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAAA7QAAAJYAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAOEJJTQQREUlDQyBVbnRhZ2dlZCBGbGFnAAAAAQEAOEJJTQQUF0xheWVyIElEIEdlbmVyYXRvciBCYXNlAAAABAAAAAE4QklNBAwVTmV3IFdpbmRvd3MgVGh1bWJuYWlsAAAC5QAAAAEAAABwAAAARwAAAVAAAF0wAAACyQAYAAH/2P/gABBKRklGAAECAQBIAEgAAP/uAA5BZG9iZQBkgAAAAAH/2wCEAAwICAgJCAwJCQwRCwoLERUPDAwPFRgTExUTExgRDAwMDAwMEQwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwBDQsLDQ4NEA4OEBQODg4UFA4ODg4UEQwMDAwMEREMDAwMDAwRDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDP/AABEIAEcAcAMBIgACEQEDEQH/3QAEAAf/xAE/AAABBQEBAQEBAQAAAAAAAAADAAECBAUGBwgJCgsBAAEFAQEBAQEBAAAAAAAAAAEAAgMEBQYHCAkKCxAAAQQBAwIEAgUHBggFAwwzAQACEQMEIRIxBUFRYRMicYEyBhSRobFCIyQVUsFiMzRygtFDByWSU/Dh8WNzNRaisoMmRJNUZEXCo3Q2F9JV4mXys4TD03Xj80YnlKSFtJXE1OT0pbXF1eX1VmZ2hpamtsbW5vY3R1dnd4eXp7fH1+f3EQACAgECBAQDBAUGBwcGBTUBAAIRAyExEgRBUWFxIhMFMoGRFKGxQiPBUtHwMyRi4XKCkkNTFWNzNPElBhaisoMHJjXC0kSTVKMXZEVVNnRl4vKzhMPTdePzRpSkhbSVxNTk9KW1xdXl9VZmdoaWprbG1ub2JzdHV2d3h5ent8f/2gAMAwEAAhEDEQA/APVUkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJSkkkklP/0PVUkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJSkkkklP/0fVUkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJSkkkklP/0vVUkkklKSSSSUpJJJJSkkkklKSSSSUpJJJJSkkkklP/0/VUl8qpJKfqpJfKqSSn6qSXyqkkp+qkl8qpJKfqpJfKqSSn6qSXyqkkp+qkl8qpJKf/2QA4QklNBCEaVmVyc2lvbiBjb21wYXRpYmlsaXR5IGluZm8AAAAAVQAAAAEBAAAADwBBAGQAbwBiAGUAIABQAGgAbwB0AG8AcwBoAG8AcAAAABMAQQBkAG8AYgBlACAAUABoAG8AdABvAHMAaABvAHAAIAA2AC4AMAAAAAEAOEJJTQQGDEpQRUcgUXVhbGl0eQAAAAAHAAgBAQABAQD/7gAhQWRvYmUAZEAAAAABAwAQAwIDBgAAAAAAAAAAAAAAAP/bAIQAAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQICAgICAgICAgICAwMDAwMDAwMDAwEBAQEBAQEBAQEBAgIBAgIDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMDAwMD/8IAEQgAlgDtAwERAAIRAQMRAf/EAF0AAQEAAAAAAAAAAAAAAAAAAAAKAQEAAAAAAAAAAAAAAAAAAAAAEAEAAAAAAAAAAAAAAAAAAACQEQEAAAAAAAAAAAAAAAAAAACQEgEAAAAAAAAAAAAAAAAAAACQ/9oADAMBAQIRAxEAAAC/gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAH//2gAIAQIAAQUADr//2gAIAQMAAQUADr//2gAIAQEAAQUADr//2gAIAQICBj8ADr//2gAIAQMCBj8ADr//2gAIAQEBBj8ADr//2Q==";
            StringBuilder base64SignatureImage1 = new StringBuilder();
            if ((input.signatureImage1 != null && input.signatureImage1 != "") && input.signatureImage1 != "null")
            {
                var fileSignature = _hostingEnvironment.ContentRootPath + @"\wwwroot" + input.signatureImage1.Replace("/", @"\");
                Logger.DebugFormat("Link fileSignature : {0}", fileSignature);
                Byte[] bytesSignatureImage = File.ReadAllBytes(fileSignature);
                var convertingBase64 = Convert.ToBase64String(bytesSignatureImage);
                var getTypeImage = new FileInfo(input.signatureImage1.Split('/')[5]);
                base64SignatureImage1.Append("data:image/" + getTypeImage.Extension.Replace(".","") + ";base64," + convertingBase64);
            }
            else
            {
                base64SignatureImage1.Append(base64Image);
            }

            StringBuilder base64SignatureImage2 = new StringBuilder();
            if ((input.signatureImage2 != null && input.signatureImage2 != "") && input.signatureImage2 != "null")
            {
                var fileSignature = _hostingEnvironment.ContentRootPath + @"\wwwroot" + input.signatureImage2.Replace("/", @"\");
                Logger.DebugFormat("Link fileSignature 2 : {0}", fileSignature);
                Byte[] bytesSignatureImage2 = File.ReadAllBytes(fileSignature);
                var convertingBase64 = Convert.ToBase64String(bytesSignatureImage2);
                var getTypeImage = new FileInfo(input.signatureImage2.Split('/')[5]);
                base64SignatureImage2.Append("data:image/" + getTypeImage.Extension.Replace(".", "") + ";base64," + convertingBase64);
            }
            else
            {
                base64SignatureImage2.Append(base64Image);
            }

            var getDataGenerate = GetDataGenerate(param.docNo, param.bookingHeaderID, input.procurationName, input.procurationType);
            getDataGenerate.DocDate = param.docDate.ToString("dd/MM/yyyy");
            getDataGenerate.PPDate = DateTime.Now.ToString("dd/MM/yyyy");
            getDataGenerate.PrintDay = DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("id-ID"));
            getDataGenerate.PrintDate = DateTime.Now.ToString("dd/MM/yyyy");
            getDataGenerate.NameOfProcuration = input.procurationName;
            getDataGenerate.TypeOfProcuration = input.procurationType == 1 ? "selaku wali dari anak yang masih di bawah umur "
                                                : input.procurationType == 2 ? "selaku kuasa dari"
                                                : input.procurationType == 3 ? "" : null;
            //TypeOfProcuration = suratType != 0 ? suratType == 1 ? " selaku wali dari anak dibawah umur bernama " : " selaku kuasa dari " : "",
            getDataGenerate.TandaTerimaNo = param.tandaTerimaNo;
            getDataGenerate.KadasterErrorMessage = "File Kadaster with UnitCode: " + getDataGenerate.UnitCode + " and UnitNo: " + getDataGenerate.UnitNo;
            getDataGenerate.Block = getDataGenerate.UnitCode != null ? getDataGenerate.UnitCode.Split('-')[0] : null;
            getDataGenerate.Tower = getDataGenerate.UnitCode != null ? getDataGenerate.UnitCode.Split('-')[1] : null;
            getDataGenerate.signatureImage1 = base64SignatureImage1.ToString();
            getDataGenerate.signatureImage2 = base64SignatureImage2.ToString();
            getDataGenerate.isTandaTerima = getMappingTemplate.Any() ? getMappingTemplate.FirstOrDefault().isTandaTerima : false;
            //isTandaTerima harus dikirim
            Logger.DebugFormat("GeneratePPPU() - End Generate");

            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var webConfigApp = (JObject)appsettingsjson["App"];
            var configuration = webConfigApp.Property("apiPdfUrl").Value.ToString();

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(10);
                var url = configuration + "api/Pdf/P3UPdf";

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MimeTypeNames.ApplicationJson));
                request.Content = new StringContent(JsonConvert.SerializeObject(getDataGenerate), Encoding.UTF8, MimeTypeNames.ApplicationJson);

                var responseFile = ReadResponse(url, client, request);

                var webRootPath = _hostingEnvironment.WebRootPath;
                var oldLinkPath = @"Assets\Upload\LegalDocument\PPPU\" + getDataGenerate.BookCode;
                var oldPath = Path.Combine(webRootPath, oldLinkPath);
                var filePath = oldPath + @"\" + param.docNo.Replace("/","-") + ".pdf";

                if (!Directory.Exists(oldPath))
                {
                    Directory.CreateDirectory(oldPath);
                }
                var getBytes = Convert.FromBase64String(responseFile.Result);
                System.IO.File.WriteAllBytes(filePath, getBytes);
            }

            Logger.InfoFormat("CreateBookingDocument() Ended.");
        }

        private async Task<string> ReadResponse(string url, HttpClient client, HttpRequestMessage request)
        {
            var response = await client.SendAsync(request);

            var success = await response.Content.ReadAsStringAsync();
            var indexFirstSlash = success.IndexOf(@"\");
            var trimString = success.Substring(indexFirstSlash + 1, success.Length).Trim(new char[1] { '"' });

            return trimString;
        }

        public void UploadTandaTerima(UpdateBookingDocumentInputDto input)
        {
            Logger.InfoFormat("UploadTandaTerima() Started.");
            var getDate = DateTime.Now;
            var getBookingDocument = (from x in _trBookingDocumentRepo.GetAll()
                                   where x.Id == input.trBookingDocumentID
                                   select x).FirstOrDefault();
            var updateBookingDocument = getBookingDocument.MapTo<TR_BookingDocument>();
            if (getDate.Date > getBookingDocument.docDate.Date)
            {
                //TODO: Update remarks to invalid
                updateBookingDocument.remarks = "Invalid";
                Logger.DebugFormat("UploadTandaTerima() - Start Update Remarks TR_BookingDocument. Parameters sent: {0} "
                , Environment.NewLine, updateBookingDocument.remarks);
                _trBookingDocumentRepo.Update(updateBookingDocument);
                Logger.DebugFormat("UploadTandaTerima() - End Update Remarks TR_BookingDocument");
                
                Logger.InfoFormat("UploadTandaTerima() Tanda Terima Date must be the same with the Document Date. Please re-generate the document.");
                throw new UserFriendlyException("Tanda Terima Date must be the same with the Document Date. Please re-generate the document.");
            }
            else
            {
                if (input.tandaTerimaFile != null)
                {
                    var tandaTerimaFile = uploadFile(input.tandaTerimaFile, @"Temp\Downloads\LegalDocument\TandaTerima\", @"Assets\Upload\LegalDocument\TandaTerima\");
                    GetURLWithoutHost(tandaTerimaFile, out tandaTerimaFile);
                    updateBookingDocument.tandaTerimaFile = tandaTerimaFile;
                }

                updateBookingDocument.tandaTerimaNo = input.tandaTerimaNo;
                updateBookingDocument.tandaTerimaDate = getDate;
                updateBookingDocument.tandaTerimaBy = input.tandaTerimaBy;

                Logger.DebugFormat("UploadTandaTerima() - Start Preparation Data For Update TR_BookingDocument. Parameters sent: {0} " +
                "   entityID = 1" +
                "   bookingDocumentID = {1}{0}" +
                "   tandaTerimaFile = {2}{0}" +
                "   tandaTerimaNo = {3}{0}" +
                "   tandaTerimaDate = {4}{0}" +
                "   tandaTerimaBy = {5}{0}"
                , Environment.NewLine, input.trBookingDocumentID, updateBookingDocument.tandaTerimaFile, input.tandaTerimaNo, input.tandaTerimaDate, input.tandaTerimaBy);
                _trBookingDocumentRepo.Update(updateBookingDocument);
                Logger.DebugFormat("UploadTandaTerima() - End Update TR_BookingDocument");
            }
            Logger.InfoFormat("UploadTandaTerima() Ended.");
        }

        private CounterDocNoListDto CounterDocNo(int projectID, int docID, int coID)
        {
            CounterDocNoListDto counterNo = new CounterDocNoListDto();

            string docCodeNumber = null;
            int docIncInt;
            var getCounter = (from A in _docNoCounterRepo.GetAll()
                              where A.projectID == projectID && A.docID == docID && A.coID == coID
                              select A).FirstOrDefault();

            var getLastDate = (from A in _trBookingDocumentRepo.GetAll()
                               where A.entityID == 1 && A.docID == docID
                               orderby A.CreationTime descending
                               select A).FirstOrDefault();

            var getDocCode = (from A in _msDocumentPsRepo.GetAll()
                              where A.Id == docID
                              select A).FirstOrDefault();

            var getProjectCode = (from A in _msProjectRepo.GetAll()
                                  where A.Id == projectID
                                  select A).FirstOrDefault();

            if (getDocCode != null && getProjectCode != null)
            {
                var monthCompare = DateTime.Now.ToString("MM");
                var month = getLastDate != null ? getLastDate.docNo.Split('/')[2] : null;
                if (getCounter != null && getLastDate != null && month == monthCompare)
                {
                    var lastInc = Int32.Parse(getLastDate.docNo.Split("/")[0].TrimStart(new Char[] { '0' }));
                    var inc = lastInc + 1;
                    docIncInt = inc;
                    docCodeNumber = _iFilesHelper.ConvertDocIdToDocCode(inc);
                }
                else
                {
                    docIncInt = 1;
                    docCodeNumber = _iFilesHelper.ConvertDocIdToDocCode(1);
                }
                var docNo = docCodeNumber + "/" + getDocCode.docCode + "-" + getProjectCode.projectCode + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString();
                var tandaTerimaNo = docCodeNumber + "/" + getDocCode.docCode + "-" + getProjectCode.projectCode + "-TandaTerima/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString();
                counterNo.docNo = docNo;
                counterNo.tandaTerimaNo = tandaTerimaNo;

                var param = new DocNo_Counter
                {
                    docID = docID,
                    docNo = docIncInt,
                    coID = coID,
                    projectID = projectID
                };

                Logger.DebugFormat("CreateBookingDocument() - Start Insert DocNo_Counter. Parameters sent: {0} " +
                    "   docID = 1" +
                    "   docNo = {1}{0}" +
                    "   coID = {2}{0}" +
                    "   projectID = {3}{0}"
                    , Environment.NewLine, docID, docIncInt, coID, projectID);
                _docNoCounterRepo.Insert(param);
                Logger.DebugFormat("CreateBookingDocument() - End Inserting data");

                return counterNo;
            }
            else
            {
                throw new UserFriendlyException("Project Or Document is not found !");
            }
        }

        private string uploadFile(string filename, string pathTemp, string pathAsset)
        {
            try
            {
                return _iFilesHelper.MoveFilesLegalDoc(filename, pathTemp, pathAsset);
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Upload Error !", ex.Message);
            }
        }

        private void GetURLWithoutHost(string path, out string finalpath)
        {
            finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
        }

        private string GetURLWithoutHost(string path)
        {
            string finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
            return finalpath;
        }

        private string getAbsoluteUriWithoutTail()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");
            int position = result.LastIndexOf('/');
            if (position > -1)
                result = result.Substring(0, result.Length - 1);
            return result + _appConfiguration["App:VirtualDirectory"];
        }

        public P32DocumentDto GetDataGenerate(string docNo, int bookingHeaderID, string atasNama, int procurationType)
        {
            //The KTP address or if null, any 
            //provided address.
            StringBuilder dynamicIdAddress = new StringBuilder();
            StringBuilder dynamicIdNo = new StringBuilder();

            //The corespondence address or if null, any 
            //provided address.
            StringBuilder correspondenceAddress = new StringBuilder();
            
            var trbooking = (from A in _trBookingHeaderRepo.GetAll()
                                    join B in _msUnitRepo.GetAll() on A.unitID equals B.Id
                                    join C in _msUnitCodeRepo.GetAll() on B.unitCodeID equals C.Id
                                    join D in _msTermMainRepo.GetAll() on B.termMainID equals D.Id
                                    join E in _msTermRepo.GetAll() on D.termCode equals E.termCode
                                    join F in _trBookingDetailRepo.GetAll() on A.Id equals F.bookingHeaderID
                                    where A.entityID == 1 
                                    && A.Id == bookingHeaderID 
                                    && A.cancelDate == null
                                    && F.itemID == 1
                                    select new {
                                        bookingHeaderID = A.Id,
                                        bookCode = A.bookCode,
                                        bookDate = A.bookDate,
                                        unitCode = C.unitCode,
                                        unitNo = B.unitNo,
                                        psCode = A.psCode,
                                        termCode = D.termCode,
                                        termNo = E.termNo,
                                        coCode = F.coCode,
                                        unitID = B.Id,
                                        unitCodeID = C.Id
                                    }).FirstOrDefault();

            if (trbooking == null) throw new ArgumentException($"Could not find {nameof(trbooking)}.");

            var unitOrder = (from A in _trUnitOrderDetailRepo.GetAll()
                             join B in _trUnitOrderHeaderRepo.GetAll() on A.UnitOrderHeaderID equals B.Id
                             where A.bookingHeaderID == trbooking.bookingHeaderID
                             orderby A.CreationTime descending
                             select new
                             {
                                 bookCode = trbooking.bookCode,
                                 orderCode = B.orderCode,
                                 unitID = A.unitID,
                                 renovID = A.renovID,
                                 termID = A.termID,
                                 bfAmount = A.BFAmount,
                                 sellingPrice = A.sellingPrice,
                                 ppNo = A.PPNo,
                                 remarks = A.remarks,
                                 disc1 = A.disc1,
                                 disc2 = A.disc2,
                                 specialDiscType = A.specialDiscType,
                                 groupBU = A.groupBU
                             }).FirstOrDefault();

            if (unitOrder == null)
            {
                throw new InvalidOperationException(String.Format("Could not find a UnitOrderDetail with the BookCode '{0}'.", trbooking.bookCode));
            }

            string PPNo = unitOrder.ppNo;
            string orderCode = unitOrder.orderCode;

            var PPDate = trbooking.bookDate.ToString("dd/MM/yyyy");

            //TODO: EntityID ?
            var personalInfo = (from A in _personalContext.PERSONAL
                                join B in _personalContext.TR_Address on A.psCode equals B.psCode into BB
                                from B in BB.DefaultIfEmpty()
                                join LkAddr in _personalContext.LK_AddrType on B.addrType equals LkAddr.addrType into LkAddrr
                                from LkAddr in LkAddrr.DefaultIfEmpty()
                                join C in _personalContext.TR_Phone on A.psCode equals C.psCode into CC
                                from C in CC.DefaultIfEmpty()
                                join D in _personalContext.TR_ID on A.psCode equals D.psCode into DD
                                from D in DD.DefaultIfEmpty()
                                join LkID in _personalContext.LK_IDType on D.idType equals LkID.idType into LkIDD
                                from LkID in LkIDD.DefaultIfEmpty()
                                join E in _personalContext.TR_Email on A.psCode equals E.psCode into EE
                                from E in EE.DefaultIfEmpty()
                                where A.psCode == trbooking.psCode //&& B.refID == 1
                                select new
                                {
                                    personalName = A.name,
                                    noIdentitas = D.idNo,
                                    personalAddress = B.address,
                                    personalNpwp = A.NPWP,
                                    personalEmail = E.email,
                                    personalPhone = C.number,
                                    bookCode = trbooking.bookCode,
                                    bookingDate = trbooking.bookDate.Date,
                                    tanggalGenerate = DateTime.Now.Date,
                                    addrType = B.addrType,
                                    addrName = LkAddr.addrTypeName,
                                    idType = D.idType,
                                    idName = LkID.idTypeName
                                }).ToList();

            if (personalInfo != null)
            {
                var personalAddressId = personalInfo.Any(x => x.addrName != null) ? personalInfo.Any(x => x.addrName.Equals("ID")) : false;
                var personalAddressCorress = personalInfo.Any(x => x.addrName != null) ? personalInfo.Any(x => x.addrName.Equals("Corress")) : false;
                if (personalAddressId)
                {
                    var dataAddr = personalInfo.Where(x => x.addrName.Equals("ID")).Select(x => x.personalAddress).FirstOrDefault();
                    dynamicIdAddress.Append(dataAddr);
                }
                else if (personalAddressCorress)
                {
                    var dataAddr = personalInfo.Where(x => x.addrName.Equals("Corress")).Select(x => x.personalAddress).FirstOrDefault();
                    dynamicIdAddress.Append(dataAddr);
                }
                else
                {
                    dynamicIdAddress.Append("-");
                }

                if (personalAddressCorress)
                {
                    var dataAddr = personalInfo.Where(x => x.addrName.Equals("Corress")).Select(x => x.personalAddress).FirstOrDefault();
                    correspondenceAddress.Append(dataAddr);
                }
                else if (personalAddressId)
                {
                    var dataAddr = personalInfo.Where(x => x.addrName.Equals("ID")).Select(x => x.personalAddress).FirstOrDefault();
                    correspondenceAddress.Append(dataAddr);
                }
                else
                {
                    correspondenceAddress.Append("-");
                }

                var personalIdKtp = personalInfo.Any(x => x.idName != null) ? personalInfo.Any(x => x.idName.Equals("KTP")) : false;
                var personalIdSim = personalInfo.Any(x => x.idName != null) ? personalInfo.Any(x => x.idName.Equals("SIM")) : false;
                var personalIdKitas = personalInfo.Any(x => x.idName != null) ? personalInfo.Any(x => x.idName.Equals("KITAS")) : false;
                if (personalIdKtp)
                {
                    var dataIdentitas = personalInfo.Where(x => x.idName.Equals("KTP")).Select(x => x.noIdentitas).FirstOrDefault();
                    dynamicIdNo.Append(dataIdentitas);
                }
                else if (personalIdSim)
                {
                    var dataIdentitas = personalInfo.Where(x => x.idName.Equals("SIM")).Select(x => x.noIdentitas).FirstOrDefault();
                    dynamicIdNo.Append(dataIdentitas);
                }
                else if (personalIdKitas)
                {
                    var dataIdentitas = personalInfo.Where(x => x.idName.Equals("KITAS")).Select(x => x.noIdentitas).FirstOrDefault();
                    dynamicIdNo.Append(dataIdentitas);
                }
                else
                {
                    dynamicIdNo.Append("-");
                }
            }

            //var msUnitVA

            var msCompany = _msCompanyRepo
                .GetAll()
                .Where(p => p.coCode == trbooking.coCode)
                .OrderBy(p => p.coCode)
                .ThenBy(p => p.coName)
                .FirstOrDefault();

            var msTerm = _msTermRepo
                .GetAll()
                .Where(p => p.termCode == trbooking.termCode && p.termNo == trbooking.termNo)
                .ToList();

            var trBookingDetails = (from A in _trBookingDetailRepo.GetAll()
                                  join B in _trBookingHeaderRepo.GetAll() on A.bookingHeaderID equals B.Id
                                  where B.bookCode == trbooking.bookCode
                                  select A);

            var bookingDetails = trBookingDetails.Sum(x => (double)x.netNetPrice + (x.pctTax * (double)x.netNetPrice));

            var bfAmount = trBookingDetails.Sum(x => x.BFAmount);

            var clusterCode = (from A in _msUnitRepo.GetAll()
                               join C in _msClusterRepo.GetAll() on A.clusterID equals C.Id
                               where A.Id == trbooking.unitID
                               select C.clusterCode).FirstOrDefault();

            var msCluster = _msClusterRepo
                .GetAll()
                .Where(p => p.clusterCode == clusterCode)
                .ToList();

            //var bookingDetailID = (from A in _trBookingDetailRepo.GetAll()
            //                       join B in _trBookingHeaderRepo.GetAll() on A.bookingHeaderID equals B.Id
            //                       where B.bookCode == trbooking.bookCode
            //                       select B.Id).FirstOrDefault();

            //var listBookingSchedule = _trBookingDetailScheduleRepo.GetAll().Where(x => x.bookingDetailID == bookingDetailID).ToList();

            //var groupingHeaders = listBookingSchedule
            //    .GroupBy(x => new { x.schedNo, x.allocID, x.dueDate });

            //if (!(msTerm[0].remarks.Contains("KPA") || msTerm[0].remarks.Contains("CASH")))
            //{
            //    groupingHeaders = listBookingSchedule
            //   .Where(x => x.allocID != 4)
            //   .GroupBy(x => new { x.schedNo, x.allocID, x.dueDate });
            //}

            //var groupingDetails = listBookingSchedule
            //        .Where(x => x.allocID != 4)
            //        .GroupBy(x => new { x.schedNo, x.allocID, x.dueDate });

            //var headers = groupingHeaders.Select(header =>
            //{
            //    var jenisPembayaran = "";

            //    if (header.Key.allocID == 2)
            //    {
            //        jenisPembayaran = "Booking Fee";
            //    }
            //    else if (header.Key.allocID == 3)
            //    {
            //        jenisPembayaran = "Down Payment";
            //    }
            //    else if (header.Key.allocID == 4 && msTerm[0].remarks.Contains("KPA"))
            //    {
            //        jenisPembayaran = "Pencairan KPA";
            //    }
            //    else if (header.Key.allocID == 4 && msTerm[0].remarks.Contains("CASH"))
            //    {
            //        jenisPembayaran = "Pelunasan";
            //    }

            //    return new
            //    {
            //        AllocationCode = jenisPembayaran,
            //        DueDate = header.Key.dueDate.ToString("dd/MM/yyyy"),
            //        TotalAmount = header.Sum(v => (v.netAmt + v.vatAmt)).ToString("#,###")
            //    };
            //});

            //var nomorAngsuran = 1;
            //var installments = groupingDetails.OrderBy(x => x.Key.schedNo).Select(x =>
            //{
            //    var jenisAngsuran = "";

            //    if (!(msTerm[0].remarks.Contains("KPA") || msTerm[0].remarks.Contains("CASH")))
            //    {
            //        jenisAngsuran = "Ke-" + nomorAngsuran;
            //        nomorAngsuran++;

            //        return new
            //        {
            //            AllocationCode = jenisAngsuran,
            //            DueDate = x.Key.dueDate.ToString("dd/MM/yyyy"),
            //            TotalAmount = x.Sum(v => (v.netAmt + v.vatAmt)).ToString("#,###")
            //        };
            //    }

            //    return new
            //    {
            //        AllocationCode = "",
            //        DueDate = "",
            //        TotalAmount = ""
            //    };
            //});

            //var unitItem = _msUnitItemRepo.FirstOrDefault(x => x.unitID == trbooking.unitID && x.itemID == 2);

            var getKP = GetKP(orderCode);
            if (getKP != null)
            {
                getKP.bfAmount = bfAmount.ToString();
            }

            P32DocumentDto returningData = new P32DocumentDto();
            returningData.DocNo = docNo;
            returningData.DocDate = DateTime.Now.Date.ToString();
            returningData.Nama = personalInfo.Any() ? personalInfo.FirstOrDefault().personalName : "";
            returningData.AlamatKTP = dynamicIdAddress.ToString();
            returningData.Email = personalInfo.Any() ? personalInfo.FirstOrDefault().personalEmail : "";
            returningData.NoKTP = dynamicIdNo.ToString();
            returningData.NoNPWP = personalInfo.Any() ? personalInfo.FirstOrDefault().personalNpwp : "";
            returningData.Handphone = personalInfo.Any() ? personalInfo.FirstOrDefault().personalPhone : "";
            returningData.BookCode = trbooking.bookCode;
            returningData.UnitCode = trbooking.unitCode;
            returningData.UnitNo = trbooking.unitNo;
            //returningData.VirtualAccountNumber = msUnitVA.Items.Any() ? msUnitVA.Items[0].VA_BankAccNo : "";
            returningData.CompanyName = msCompany.coName.ToUpper();
            returningData.SpecialCicilan = msTerm.Any() ? msTerm.FirstOrDefault().remarks : "";
            returningData.SpecialCicilanAmount = bookingDetails.ToString("#;###");
            returningData.HandOverPeriod = msCluster.Any() ? msCluster.FirstOrDefault().handOverPeriod : "";
            returningData.GracePeriod = msCluster.Any() ? msCluster.FirstOrDefault().gracePeriod : "";
            returningData.NameOfProcuration = atasNama != "" ? atasNama + " " : "";
            returningData.TypeOfProcuration = procurationType != 0 ? procurationType == 1 ? " selaku wali dari anak dibawah umur bernama " : " selaku kuasa dari " : "";
            returningData.PPNo = PPNo;
            returningData.PPDate = PPDate;
            returningData.OrderCode = orderCode;
            returningData.AlamatCorrespondence = correspondenceAddress.ToString();
            returningData.KP = getKP;

            return returningData;
        }

        private KonfirmasiPesananDto GetKP(string orderCode)
        {
            var dataKP = (from x in _propertySystemContext.TR_UnitOrderHeader
                          join a in _propertySystemContext.TR_UnitOrderDetail on x.Id equals a.UnitOrderHeaderID
                          join b in _propertySystemContext.MS_Unit on a.unitID equals b.Id
                          join d in _personalContext.PERSONAL.ToList() on x.psCode equals d.psCode
                          join e in _propertySystemContext.TR_BookingHeader on a.bookingHeaderID equals e.Id
                          join f in _propertySystemContext.MS_TujuanTransaksi on e.tujuanTransaksiID equals f.Id
                          join g in _propertySystemContext.MS_SumberDana on e.sumberDanaID equals g.Id
                          join h in _personalContext.TR_ID.ToList() on x.psCode equals h.psCode into iden
                          from h in iden.DefaultIfEmpty()
                          //join i in _newCommContext.MS_Schema.ToList() on e.scmCode equals i.scmCode
                          join j in _personalContext.PERSONALS_MEMBER.ToList() on new { e.memberCode, e.scmCode } equals new { j.memberCode, j.scmCode }
                          join k in _personalContext.PERSONAL.ToList() on j.psCode equals k.psCode
                          join l in _personalContext.TR_Phone.ToList() on k.psCode equals l.psCode into phone
                          from l in phone.DefaultIfEmpty()
                          join m in _propertySystemContext.MS_Project on b.projectID equals m.Id
                          join p in _propertySystemContext.MS_Detail on b.detailID equals p.Id
                          join s in _propertySystemContext.MS_Term on e.termID equals s.Id
                          where x.orderCode == orderCode && new string[] { "1", "5", "7" }.Contains(h.idType)
                          select new KonfirmasiPesananDto
                          {
                              orderCode = x.orderCode,
                              kodePelanggan = x.psCode,
                              tanggalBooking = e.bookDate.ToString(),
                              psName = x.psName,
                              birthDate = d.birthDate.ToString(),
                              noHpPembeli = x.psPhone,
                              noIdentitas = (h == null ? "-" : h.idNo),
                              noNPWP = d.NPWP,
                              email = x.psEmail,
                              BookCode = e.bookCode,
                              hargaJual = a.sellingPrice.ToString(),
                              bfAmount = a.BFAmount.ToString(),
                              imageProject = m.image,
                              noHp = l.number,
                              noDealCloser = k.psCode,
                              namaDealCloser = k.name,
                              caraPembayaran = s.remarks,
                              tujuanTransaksi = f.tujuanTransaksiName,
                              sumberDanaPembelian = g.sumberDanaName,
                              unitID = e.unitID
                          }).FirstOrDefault();

            //var dataUnit = (from a in _propertySystemContext.MS_Unit
            //                join b in _propertySystemContext.MS_Project on a.projectID equals b.Id
            //                join c in _propertySystemContext.MS_Cluster on a.clusterID equals c.Id
            //                join d in _propertySystemContext.MS_UnitItem on a.Id equals d.unitID
            //                join e in _propertySystemContext.MS_Detail on a.detailID equals e.Id
            //                join f in _propertySystemContext.MS_Category on a.categoryID equals f.Id
            //                join g in _propertySystemContext.MS_UnitCode on a.unitCodeID equals g.Id
            //                join h in _propertySystemContext.TR_BookingHeader on a.Id equals h.unitID
            //                join i in _propertySystemContext.TR_BookingItemPrice on new { bookingHeaderID = h.Id, itemID = d.itemID } equals new { bookingHeaderID = i.bookingHeaderID, itemID = i.itemID }
            //                join j in _propertySystemContext.MS_Renovation on i.renovCode equals j.renovationCode
            //                join k in _propertySystemContext.TR_UnitOrderDetail on new { h.unitID, renovID = j.Id } equals new { k.unitID, k.renovID }
            //                group d by new
            //                {
            //                    d.unitID,
            //                    a.unitNo,
            //                    g.unitCode,
            //                    c.clusterName,
            //                    f.categoryName,
            //                    e.detailName,
            //                    j.renovationName
            //                } into G
            //                where G.Key.unitID == dataKP.unitID
            //                select new unitDto
            //                {
            //                    UnitNo = G.Key.unitNo,
            //                    UnitCode = G.Key.unitCode.Contains("-") ? G.Key.unitCode : null,
            //                    category = G.Key.categoryName,
            //                    cluster = G.Key.clusterName,
            //                    luas = G.Sum(d => d.area).ToString(),
            //                    renovation = G.Key.renovationName,
            //                    tipe = G.Key.detailName
            //                }).ToList();

            //dataKP.listUnit = dataUnit;

            //var dataBank = (from bank in _propertySystemContext.MS_BankOLBooking
            //                join unit in _propertySystemContext.MS_Unit on new { bank.projectID, bank.clusterID } equals new { unit.projectID, unit.clusterID }
            //                join header in _propertySystemContext.TR_BookingHeader on unit.Id equals header.unitID
            //                where unit.Id == dataKP.unitID
            //                select new listBankDto
            //                {
            //                    bankName = bank.bankName,
            //                    noVA = bank.bankRekNo
            //                }).ToList();

            //dataKP.listBank = dataBank;

            return dataKP;
        }

        public GetFilePPPUListDto GetFilePPPU(GetFilePPPUInputDto input)
        {
            GetFilePPPUListDto getFilePPPU = new GetFilePPPUListDto();

            var getBookingDocumentData = (from x in _trBookingDocumentRepo.GetAll()
                                          join a in _msDocumentPsRepo.GetAll() on x.docID equals a.Id
                                          join b in _msMappingTemplateRepo.GetAll() on a.Id equals b.docID
                                          where x.Id == input.bookingDocumentID &&
                                          x.docDate >= b.activeFrom &&
                                          ((b.activeTo != null && x.docDate <= b.activeTo) ? (b.activeTo != null && x.docDate <= b.activeTo) : b.activeTo == null) &&
                                          b.isActive
                                          select new BookingDocumentListDto
                                          {
                                              bookingDocumentId = x.Id,
                                              docCode = a.docCode,
                                              docName = a.docName,
                                              docNo = x.docNo,
                                              docDate = x.docDate,
                                              remarks = x.remarks,
                                              isTandaTerima = b.isTandaTerima,
                                              fileTandaTerima = (x != null && x.tandaTerimaFile != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.tandaTerimaFile) : null,
                                          });

            var cekLinkFilePPPU = _hostingEnvironment.WebRootPath + @"\Assets\Upload\LegalDocument\PPPU\" + input.bookCode + @"\" + getBookingDocumentData.FirstOrDefault().docNo.Replace("/", "-") + ".pdf";

            StringBuilder linkFileBuild = new StringBuilder();
            linkFileBuild.Append("/Assets/Upload/LegalDocument/PPPU/" + input.bookCode + "/" + getBookingDocumentData.FirstOrDefault().docNo.Replace("/", "-") + ".pdf");
            var linkFilePPPU = getAbsoluteUriWithoutTail() + GetURLWithoutHost(linkFileBuild.ToString());
            if (getBookingDocumentData.Any())
            {
                if (File.Exists(cekLinkFilePPPU))
                {
                    getFilePPPU.linkFilePPPU = linkFilePPPU;
                }
                else
                {
                    throw new UserFriendlyException("File is not found!");
                }
            }

            return getFilePPPU;
        }
    }
}
