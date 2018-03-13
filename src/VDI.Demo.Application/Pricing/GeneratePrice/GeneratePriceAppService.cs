using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Pricing.GeneratePrice.Dto;
using VDI.Demo.Pricing.GeneratePrice.Exporter;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using Abp.UI;
using VDI.Demo.Dto;
using Abp.Authorization;
using VDI.Demo.Authorization;
using VDI.Demo.Files;
using Visionet_Backend_NetCore.Komunikasi;
using System.Threading.Tasks;
using Abp.Localization;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.IO;
using VDI.Demo.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;

namespace VDI.Demo.Pricing.GeneratePrice
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_GeneratePrice)]
    public class GeneratePriceAppService : DemoAppServiceBase, IGeneratePriceAppService
    {
        private readonly IGeneratePriceListExcelExporter _generatePriceListExcelExporter;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_TermMain> _msTermMainRepo;
        private readonly IRepository<MS_TermDP> _msTermDpRepo;
        private readonly IRepository<MS_TermAddDisc> _msTermAddDiscRepo;
        private readonly IRepository<MS_TermPmt> _msTermPmtRepo;
        private readonly IRepository<LK_FinType> _lkFinTypeRepo;
        private readonly IRepository<MS_Product> _msProductRepo;
        private readonly IRepository<MS_PriceTaskList> _msPriceTaskListRepo;
        private readonly FilesHelper _filesHelper;
        private readonly ILocalizationManager _localizationManager;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigurationRoot _appConfiguration;

        public GeneratePriceAppService(
             IGeneratePriceListExcelExporter generatePriceListExcelExporter,
             IRepository<MS_Unit> msUnitRepo,
             IRepository<MS_UnitCode> msUnitCodeRepo,
             IRepository<MS_Term> msTermRepo,
             IRepository<MS_TermMain> msTermMainRepo,
             IRepository<MS_TermAddDisc> msTermAddDiscRepo,
             IRepository<MS_TermDP> msTermDpRepo,
             IRepository<MS_TermPmt> msTermPmtRepo,
             IRepository<LK_FinType> lkFinTypeRepo,
             IRepository<MS_Product> msProductRepo,
             IRepository<MS_PriceTaskList> msPriceTaskListRepo,
             FilesHelper filesHelper,
             ILocalizationManager localizationManager,
             IHostingEnvironment environment,
             PropertySystemDbContext contextProp,
             IHttpContextAccessor httpContextAccessor)
        {
            _generatePriceListExcelExporter = generatePriceListExcelExporter;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _msTermRepo = msTermRepo;
            _msTermMainRepo = msTermMainRepo;
            _msTermDpRepo = msTermDpRepo;
            _msTermAddDiscRepo = msTermAddDiscRepo;
            _msTermPmtRepo = msTermPmtRepo;
            _lkFinTypeRepo = lkFinTypeRepo;
            _msProductRepo = msProductRepo;
            _msPriceTaskListRepo = msPriceTaskListRepo;
            _filesHelper = filesHelper;
            _localizationManager = localizationManager;
            _hostingEnvironment = environment;
            _contextProp = contextProp;
            _hostingEnvironment = environment;
            _httpContextAccessor = httpContextAccessor;
            _appConfiguration = environment.GetAppConfiguration();
        }

        public List<GetMsUnitByProjectClusterDto> GetMsUnitByProjectCluster(int projectID, int clusterID)//passed
        {
            var getData = (from x in _contextProp.MS_Unit
                           join z in _contextProp.MS_UnitCode on x.unitCodeID equals z.Id
                           where x.projectID == projectID && x.clusterID == clusterID
                           select new GetMsUnitByProjectClusterDto
                           {
                               unitNo = x.unitNo,
                               unitCode = z.unitCode
                           }).ToList();

            if (getData.Any()) return getData;
            else throw new UserFriendlyException("No Unit Found!");
        }

        public List<GetMsUnitByProjectClusterDto> GetMsUnitByProjectClusterCategoryProductTermMain(GetMsUnitByProjectClusterCategoryProductTermMain input)
        {
            var getData = (from x in _contextProp.MS_Unit
                           join z in _contextProp.MS_UnitCode on x.unitCodeID equals z.Id
                           where x.projectID == input.projectID && x.clusterID == input.clusterID && 
                           x.categoryID == input.categoryID && x.productID == input.productID && x.termMainID == input.termMainID
                           select new GetMsUnitByProjectClusterDto
                           {
                               unitNo = x.unitNo,
                               unitCode = z.unitCode
                           }).ToList();

            if (getData.Any()) return getData;
            else throw new UserFriendlyException("No Unit Found!");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GeneratePrice_UploadPriceList)]
        public FileDto GenerateExcelUploadPriceList(GetMsUnitByProjectIdClusterIdDto input)//passed
        {
            var msUnitProjectCluster = GetMsUnitByProjectCluster(input.projectID, input.clusterID);

            var dataFinal = (from x in msUnitProjectCluster
                             select new GenerateTemplateExcelListDto
                             {
                                 unitCode = x.unitCode,
                                 unitNo = x.unitNo
                             }).ToList();

            return _generatePriceListExcelExporter.GenerateExcelUploadPriceList(dataFinal);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GeneratePrice_UploadExcel)]
        public FileDto GenerateExcelUploadGrossPrice(GetMsUnitByProjectClusterCategoryProductTermMain input)
        {
            var msUnitCodeNo = GetMsUnitByProjectClusterCategoryProductTermMain(input);

            var dataFinal = (from A in msUnitCodeNo
                             select new GenerateTemplateExcelListDto
                             {
                                 unitCode = A.unitCode,
                                 unitNo = A.unitNo
                             }).ToList();

            return _generatePriceListExcelExporter.GenerateExcelUploadGrossPrice(dataFinal);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GeneratePrice_UploadExcel)]
        public FileDto ExportToExcelUploadGrossPrice(ExportToExcelUploadGrossPriceListDto input)//passed
        {
            FileDto fileExcel = null;
            try
            {
                Debug.WriteLine("Trigger:" + _hostingEnvironment.ContentRootPath);
                Debug.WriteLine("Trigger:" + L("PriceList"));
                List<ExportToExcelUploadPriceListDto> result = new List<ExportToExcelUploadPriceListDto>();

                #region versi old - hindari mengeksekusi query didalam foreach
                /*
                foreach (var X in input.priceList)
                {
                    var prepDP = (from A in _contextProp.MS_Unit
                                  join B in _contextProp.MS_TermMain on X.termMainID equals B.Id
                                  join C in _contextProp.MS_Product on A.productID equals C.Id
                                  join D in _contextProp.MS_UnitCode on A.unitCodeID equals D.Id
                                  where D.unitCode == X.unitCode && A.unitNo == X.unitNo
                                  orderby C.productName
                                  select new GetDpListDto
                                  {
                                      unitCode = D.unitCode,
                                      unitNo = A.unitNo,
                                      bfAmount = B.BFAmount,
                                      productName = C.productName
                                  }).FirstOrDefault();

                    decimal bookingFee = 0;
                    string productName = null;

                    if (prepDP != null)
                    {
                        bookingFee = prepDP != null ? prepDP.bfAmount : 0;
                        productName = prepDP.productName != null ? prepDP.productName : null;
                    }

                    var prepDatumExportData = new ExportToExcelUploadPriceListDto
                    {
                        productName = productName,
                        unitCode = X.unitCode,
                        unitNo = X.unitNo,
                        price = X.price,
                        termName = X.termName,
                        bookingFee = bookingFee
                    };
                    result.Add(prepDatumExportData);
                }*/
                #endregion

                var dataToExport = (
                              from mu in _contextProp.MS_Unit
                              join X in input.priceList.ToList() on mu.unitNo equals X.unitNo
                              join mtm in _contextProp.MS_TermMain on X.termMainID equals mtm.Id
                              join mp in _contextProp.MS_Product on mu.productID equals mp.Id
                              join muc in _contextProp.MS_UnitCode on mu.unitCodeID equals muc.Id
                              where muc.unitCode == X.unitCode && mu.unitNo == X.unitNo && 
                              X.termMainID == mu.termMainID && mu.productID == X.productID && 
                              mu.clusterID == X.clusterID && mu.categoryID == X.categoryID && 
                              mu.projectID == X.projectID
                              orderby mp.productName
                              select new ExportToExcelUploadPriceListDto
                              {
                                  productName = mp.productName,
                                  unitCode = X.unitCode,
                                  unitNo = X.unitNo,
                                  renovCode = X.renovCode,
                                  price = X.price,
                                  termName = X.termName,
                                  bookingFee = mtm.BFAmount
                              }).ToList();

                result.AddRange(dataToExport);

                if (!result.Any()) { throw new UserFriendlyException("Data is Required !"); }
                if (string.IsNullOrEmpty(result[0].termName)) { throw new UserFriendlyException("Term Name is Required !"); }

                fileExcel = _generatePriceListExcelExporter.ExportToExcelUploadGrossPrice(result);
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
                throw new UserFriendlyException(e.Message);
            }

            CreateUnitItemPrice(input.unitItemPrice);

            var filePath = _hostingEnvironment.WebRootPath + @"\Temp\Downloads\" + fileExcel.FileToken;
            if (!File.Exists(filePath))
            {
                throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
            }
            else
            {
                var fileBytes = File.ReadAllBytes(filePath);
                string DestinationDir = _hostingEnvironment.WebRootPath + @"\Temp\Downloads\GrossPriceFile\";
                string DestinationPath = DestinationDir + fileExcel.FileName;

                if (!Directory.Exists(DestinationDir))
                {
                    Directory.CreateDirectory(DestinationDir);
                }
                File.WriteAllBytes(DestinationPath, fileBytes);

                if (File.Exists(DestinationPath))
                {
                    var priceListFile = moveFile(fileExcel.FileName, "grossPrice");
                    var dtoPriceTaskList = new CreatePriceTaskListInputDto
                    {
                        projectID = input.priceList.FirstOrDefault().projectID,
                        priceListFile = priceListFile
                    };

                    var checkAvailibilityProject = (from x in _contextProp.MS_Project
                                                    where x.Id == input.priceList.FirstOrDefault().projectID
                                                    select x).Any();

                    if (!checkAvailibilityProject) { throw new UserFriendlyException("Master Project Code Unavailable !"); }

                    CreatePriceTaskList(dtoPriceTaskList);
                }
                else
                {
                    throw new UserFriendlyException("File is Required to be Uploaded !");
                }
            }

            return fileExcel;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_GeneratePrice_UploadExcel)]
        public FileDto ExportToExcelUploadPriceList(List<ExportToExcelUploadPriceListDto> input)//passed
        {
            FileDto fileExcel = null;
            try
            {
                Debug.WriteLine("Trigger:" + _hostingEnvironment.ContentRootPath);
                Debug.WriteLine("Trigger:" + L("PriceList"));
                List<ExportToExcelUploadPriceListDto> result = new List<ExportToExcelUploadPriceListDto>();

                #region versi old - hindari mengeksekusi query didalam foreach
                /*
                foreach (var X in input)
                {
                    var prepDP = (from A in _contextProp.MS_Unit
                                  join B in _contextProp.MS_TermMain on X.termMainID equals B.Id
                                  join C in _contextProp.MS_Product on A.productID equals C.Id
                                  join D in _contextProp.MS_UnitCode on A.unitCodeID equals D.Id
                                  where D.unitCode == X.unitCode && A.unitNo == X.unitNo
                                  orderby C.productName
                                  select new GetDpListDto
                                  {
                                      unitCode = D.unitCode,
                                      unitNo = A.unitNo,
                                      bfAmount = B.BFAmount,
                                      productName = C.productName
                                  }).FirstOrDefault();

                    decimal bookingFee = 0;
                    string productName = null;

                    if (prepDP != null)
                    {
                        bookingFee = prepDP != null ? prepDP.bfAmount : 0;
                        productName = prepDP.productName != null ? prepDP.productName : null;
                    }

                    var prepDatumExportData = new ExportToExcelUploadPriceListDto
                    {
                        productName = productName,
                        unitCode = X.unitCode,
                        unitNo = X.unitNo,
                        price = X.price,
                        termName = X.termName,
                        bookingFee = bookingFee
                    };
                    result.Add(prepDatumExportData);
                }
                */
                #endregion

                var dataToExport = (
                              from mu in _contextProp.MS_Unit
                              join X in input.ToList() on mu.unitNo equals X.unitNo
                              join mtm in _contextProp.MS_TermMain on X.termMainID equals mtm.Id
                              join mp in _contextProp.MS_Product on mu.productID equals mp.Id
                              join muc in _contextProp.MS_UnitCode on mu.unitCodeID equals muc.Id
                              where muc.unitCode == X.unitCode && mu.unitNo == X.unitNo && X.termMainID == mtm.Id
                              orderby mp.productName
                              select new ExportToExcelUploadPriceListDto
                              {
                                  productName = mp.productName,
                                  unitCode = X.unitCode,
                                  unitNo = X.unitNo,
                                  price = X.price,
                                  termName = X.termName,
                                  bookingFee = mtm.BFAmount
                              }).ToList();

                result.AddRange(dataToExport);

                if (!result.Any()) { throw new UserFriendlyException("Data is Required !"); }
                if (string.IsNullOrEmpty(result[0].termName)) { throw new UserFriendlyException("Term Name is Required !"); }

                fileExcel = _generatePriceListExcelExporter.ExportToExcelUploadPriceList(result); //TODO ExportToExcelUploadPriceList 

                var filePath = _hostingEnvironment.WebRootPath + @"\Temp\Downloads\" + fileExcel.FileToken;
                if (!File.Exists(filePath))
                {
                    throw new UserFriendlyException(L("RequestedFileDoesNotExists"));
                }
                else
                {
                    var fileBytes = File.ReadAllBytes(filePath);
                    string DestinationDir = _hostingEnvironment.WebRootPath + @"\Temp\Downloads\PriceListFile\";
                    string DestinationPath = DestinationDir + fileExcel.FileName;

                    if (!Directory.Exists(DestinationDir))
                    {
                        Directory.CreateDirectory(DestinationDir);
                    }
                    File.WriteAllBytes(DestinationPath, fileBytes);

                    if (File.Exists(DestinationPath))
                    {
                        var priceListFile = moveFile(fileExcel.FileName, "priceList");
                        var dtoPriceTaskList = new CreatePriceTaskListInputDto
                        {
                            projectID = input.FirstOrDefault().projectID,
                            priceListFile = priceListFile
                        };

                        var checkAvailibilityProject = (from x in _contextProp.MS_Project
                                                        where x.Id == input.FirstOrDefault().projectID
                                                        select x).Any();

                        if (!checkAvailibilityProject) { throw new UserFriendlyException("Master Project Code Unavailable !"); }

                        CreatePriceTaskList(dtoPriceTaskList);
                    }
                    else
                    {
                        throw new UserFriendlyException("File is Required to be Uploaded !");
                    }
                }


            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
                throw new UserFriendlyException(e.Message);
            }

            return fileExcel;
        }

        public List<GetGeneratePriceListTermListDto> GetGeneratePriceListTermByTermID(int termID)//passed
        {
            var dataGroup = (from A in _msTermRepo.GetAll()
                             join B in _msTermAddDiscRepo.GetAll() on A.Id equals B.termID into BB
                             from B in BB.DefaultIfEmpty()
                             where A.Id == termID
                             orderby B.addDiscNo ascending
                             select new GetGeneratePriceListTermListDto
                             {
                                 termRemarks = A.remarks,
                                 discPct = B == null ? 0 : B.addDiscPct / 100,
                                 discAmt = B == null ? 0 : B.addDiscAmt,
                             })
                             .ToList();

            var dataResult = dataGroup.Select(x => new GetGeneratePriceListTermListDto
            {
                termRemarks = x.termRemarks,
                discPct = Convert.ToDouble(x.discPct.ToString("0.##")),
                discAmt = x.discAmt
            }).ToList();

            return dataResult;
        }

        public void CreatePriceTaskList(CreatePriceTaskListInputDto input)
        {
            Logger.InfoFormat("CreatePriceTaskList() - Started.");
            var data = new MS_PriceTaskList
            {
                projectID = input.projectID,
                priceListFile = input.priceListFile                
            };

            Logger.DebugFormat("CreatePriceTaskList() - Start Insert MS_PriceTaskList. Parameters sent: {0} " +
                 "projectID        = {1}{0} " +
                 "priceListFile         = {2}{0}"
                , Environment.NewLine, input.projectID, input.priceListFile);

            _contextProp.MS_PriceTaskList.Add(data);
            _contextProp.SaveChanges();

            Logger.InfoFormat("CreatePriceTaskList() - Finished.");
        }

        public List<GetPriceTaskListDto> GetPriceTaskList(int projectID)//passed
        {
            var getData = (from A in _contextProp.MS_PriceTaskList
                               where A.projectID == projectID
                               orderby A.Id descending
                               select new GetPriceTaskListDto
                               {
                                   Id = A.Id,
                                   priceListFile = (A != null && A.priceListFile != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.priceListFile) : null, //TODO link + ip host
                                   creationTime = A.CreationTime,
                               }).ToList();            

            return getData;
        }

        private string moveFile(string filename, string typeUpload)
        {
            try
            {
                string oldPath = "";
                string moveToPath = "";

                if (typeUpload == "priceList")
                {
                    oldPath = @"Temp\Downloads\PriceListFile\";
                    moveToPath = @"Assets\Upload\PriceListFile\";
                }
                else if (typeUpload == "grossPrice")
                {
                    oldPath = @"Temp\Downloads\GrossPriceFile\";
                    moveToPath = @"Assets\Upload\GrossPriceFile\";
                }

                return _filesHelper.MoveFiles(filename, oldPath, moveToPath);
            }
            catch (Exception ex)
            {
                SendConsole("" + ex.Message + " " + ex.StackTrace);
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Upload Error, " + ex.Message, ex.Message);
            }
        }

        private string AddOrdinal(int number)
        {
            if (number <= 0) return number.ToString();
            string GetIndicator(int num)
            {
                switch (num % 100)
                {
                    case 11:
                    case 12:
                    case 13:
                        return "th";
                }
                switch (num % 10)
                {
                    case 1:
                        return "st";
                    case 2:
                        return "nd";
                    case 3:
                        return "rd";
                    default:
                        return "th";
                }
            }
            return number + GetIndicator(number);
        }

        public bool CheckAndValidateFileNotBlank(List<CheckAndValidateExcelFileDto> inputs)
        {
            if (!inputs.Any()) { throw new UserFriendlyException("Data is Required !"); }
            int row = 1;
            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input.unitCode))
                {
                    throw new UserFriendlyException("Unit Code in " + AddOrdinal(row) + " data cannot be blank!");
                }

                if (string.IsNullOrWhiteSpace(input.unitNo))
                {
                    throw new UserFriendlyException("Unit No in " + AddOrdinal(row) + " data cannot be blank!");
                }

                if (string.IsNullOrWhiteSpace(input.renovCode))
                {
                    throw new UserFriendlyException("Renovation Code in " + AddOrdinal(row) + " data cannot be blank!");
                }
                row++;
            }
            return true;
        }

        public bool CheckAndValidateFileToBeUpload(ExportToExcelUploadGrossPriceListDto inputs)
        {
            var dataToExport = (
                                 from mu in _contextProp.MS_Unit
                                 join X in inputs.priceList.ToList() on mu.unitNo equals X.unitNo
                                 join mtm in _contextProp.MS_TermMain on X.termMainID equals mtm.Id
                                 join mp in _contextProp.MS_Product on mu.productID equals mp.Id
                                 join muc in _contextProp.MS_UnitCode on mu.unitCodeID equals muc.Id
                                 where muc.unitCode == X.unitCode && mu.unitNo == X.unitNo && X.termMainID == mtm.Id
                                 orderby mp.productName
                                 select new ExportToExcelUploadPriceListDto
                                 {
                                     productName = mp.productName,
                                     unitCode = X.unitCode,
                                     unitNo = X.unitNo,
                                     price = X.price,
                                     termName = X.termName,
                                     bookingFee = mtm.BFAmount
                                 }).ToList();
            

            if (!dataToExport.Any()) { throw new UserFriendlyException("Data is Required !"); }
            if (string.IsNullOrEmpty(dataToExport[0].termName)) { throw new UserFriendlyException("Term Name is Required !"); }
            int row = 1;
            foreach (var input in inputs.unitItemPrice.inputUnitItemPrice)
            {
                if (string.IsNullOrWhiteSpace(input.unitCode))
                {
                    throw new UserFriendlyException("Unit Code in "+ AddOrdinal(row) + " data cannot be blank!");
                }

                if (string.IsNullOrWhiteSpace(input.unitNo))
                {
                    throw new UserFriendlyException("Unit No in " + AddOrdinal(row) + " data cannot be blank!");
                }

                if (string.IsNullOrWhiteSpace(input.renovCode))
                {
                    throw new UserFriendlyException("Renovation Code in " + AddOrdinal(row) + " data cannot be blank!");
                }

                var getUnitCodeID = (from A in _contextProp.MS_UnitCode
                                     where A.entityID == 1
                                     && A.unitCode == input.unitCode
                                     select A.Id);

                if (!getUnitCodeID.Any())
                {
                    throw new UserFriendlyException("Unit Code ID with EntityId: " + 1 + " and UnitCode: " + input.unitCode + " not found!");
                }

                var getUnitID = (from A in _contextProp.MS_Unit
                                 where A.entityID == 1
                                 && A.unitCodeID == getUnitCodeID.FirstOrDefault()
                                 && A.unitNo == input.unitNo
                                 && A.projectID == inputs.unitItemPrice.projectId
                                 && A.clusterID == inputs.unitItemPrice.clusterId
                                 select A.Id);

                if (!getUnitID.Any())
                {
                    throw new UserFriendlyException("Unit ID with EntityId: " + 1 + ", UnitCodeID: "
                        + getUnitCodeID.FirstOrDefault() + ", UnitNo: "
                        + input.unitNo + ", Project ID "
                        + inputs.unitItemPrice.projectId + " and Cluster ID "
                        + inputs.unitItemPrice.clusterId + " not found!");
                }

                var getRenovId = (from A in _contextProp.MS_Renovation
                                  where A.renovationCode == input.renovCode
                                  && A.projectID == inputs.unitItemPrice.projectId
                                  select A.Id);

                if (!getRenovId.Any())
                {
                    throw new UserFriendlyException("Renovation ID with RenovationCode: "
                        + input.renovCode + " and Project ID "
                        + inputs.unitItemPrice.projectId + " not found!");
                }

                var getItemId = (from A in _contextProp.LK_Item
                                 where A.itemCode == input.itemCode
                                 select A.Id);

                if (!getItemId.Any())
                {
                    throw new UserFriendlyException("Item ID with ItemCode: "
                        + input.itemCode + " not found!");
                }
                row++;
            }
            return true;
        }

        /*
        public void CreateUnitItemPrice0(CreateUnitItemPriceInputDto inputs)
        {
            foreach (var input in inputs.inputEtc)
            {
                var getUnitCodeID = (from A in _contextProp.MS_UnitCode
                                     where A.entityID == 1
                                     && A.unitCode == input.unitCode
                                     select A.Id);

                if (!getUnitCodeID.Any())
                {
                    throw new UserFriendlyException("Unit Code ID with EntityId: " + 1 + " and UnitCode: " + input.unitCode + " not found!");
                }

                var getUnitID = (from A in _contextProp.MS_Unit
                                 where A.entityID == 1
                                 && A.unitCodeID == getUnitCodeID.FirstOrDefault()
                                 && A.unitNo == input.unitNo
                                 && A.projectID == inputs.projectId
                                 && A.clusterID == inputs.clusterId
                                 select A.Id);

                if (!getUnitID.Any())
                {
                    throw new UserFriendlyException("Unit ID with EntityId: " + 1 + ", UnitCodeID: "
                        + getUnitCodeID.FirstOrDefault() + ", UnitNo: "
                        + input.unitNo + ", Project ID "
                        + inputs.projectId + " and Cluster ID "
                        + inputs.clusterId + " not found!");
                }

                var getRenovId = (from A in _contextProp.MS_Renovation
                                  where A.renovationCode == input.renovCode
                                  && A.projectID == inputs.projectId
                                  select A.Id);

                if (!getRenovId.Any())
                {
                    throw new UserFriendlyException("Renovation ID with RenovationCode: "
                        + input.renovCode + " and Project ID "
                        + inputs.projectId + " not found!");
                }

                var getItemId = (from A in _contextProp.LK_Item
                                 where A.itemCode == input.itemCode
                                 select A.Id);

                if (!getItemId.Any())
                {
                    throw new UserFriendlyException("Item ID with ItemCode: "
                        + input.itemCode + " not found!");
                }

                var msUnitItemPrice = new MS_UnitItemPrice
                {
                    entityID = 1,
                    termID = inputs.termId,
                    itemID = getItemId.FirstOrDefault(),
                    renovID = getRenovId.FirstOrDefault(),
                    unitID = getUnitID.FirstOrDefault(),
                    grossPrice = input.grossPrice
                };

                // _msUnitItemPriceRepository.Insert(msUnitItemPrice);
                _contextProp.MS_UnitItemPrice.Add(msUnitItemPrice);
                _contextProp.SaveChanges();
            }
        }
        */

        public void CreateUnitItemPrice(CreateUnitItemPriceInputDto inputs)
        {
            Logger.InfoFormat("CreateUnitItemPrice() - Started.");
            var countLoop = inputs.inputUnitItemPrice.ToList().Count;
            int inc = 1;
            foreach (var input in inputs.inputUnitItemPrice)
            {
                Logger.InfoFormat("CreateUnitItemPrice() - Started : " + inc + " of " + countLoop);
                var getUnitCodeID = (from A in _contextProp.MS_UnitCode
                                     where A.entityID == 1
                                     && A.unitCode == input.unitCode
                                     select A.Id);

                if (!getUnitCodeID.Any())
                {
                    Logger.InfoFormat("Unit Code ID with EntityId: " + 1 + " and UnitCode: " + input.unitCode + " not found!");
                    throw new UserFriendlyException("Unit Code ID with EntityId: " + 1 + " and UnitCode: " + input.unitCode + " not found!");
                }

                var getUnitID = (from A in _contextProp.MS_Unit 
                                 where A.entityID == 1
                                 && A.unitCodeID == getUnitCodeID.FirstOrDefault()
                                 && A.unitNo == input.unitNo
                                 && A.projectID == inputs.projectId
                                 && A.clusterID == inputs.clusterId
                                 select A.Id);

                if (!getUnitID.Any())
                {
                    Logger.InfoFormat("Unit ID with EntityId: " + 1 + ", UnitCodeID: "
                        + getUnitCodeID.FirstOrDefault() + ", UnitNo: "
                        + input.unitNo + ", Project ID "
                        + inputs.projectId + " and Cluster ID "
                        + inputs.clusterId + " not found!");
                    throw new UserFriendlyException("Unit ID with EntityId: " + 1 + ", UnitCodeID: "
                        + getUnitCodeID.FirstOrDefault() + ", UnitNo: "
                        + input.unitNo + ", Project ID "
                        + inputs.projectId + " and Cluster ID "
                        + inputs.clusterId + " not found!");
                }

                var getRenovId = (from A in _contextProp.MS_Renovation
                                  where A.renovationCode == input.renovCode
                                  && A.projectID == inputs.projectId
                                  select A.Id);

                if (!getRenovId.Any())
                {
                    Logger.InfoFormat("Renovation ID with RenovationCode: "
                        + input.renovCode + " and Project ID "
                        + inputs.projectId + " not found!");
                    throw new UserFriendlyException("Renovation ID with RenovationCode: "
                        + input.renovCode + " and Project ID "
                        + inputs.projectId + " not found!");
                }

                var getItemId = (from A in _contextProp.LK_Item 
                                 where A.itemCode == input.itemCode
                                 select A.Id);

                if (!getItemId.Any())
                {
                    Logger.InfoFormat("Item ID with ItemCode: "
                        + input.itemCode + " not found!");
                    throw new UserFriendlyException("Item ID with ItemCode: "
                        + input.itemCode + " not found!");
                }

                var checkUnitItemPrice = (from A in _contextProp.MS_UnitItemPrice
                                          where A.entityID == 1
                                          && A.unitID == getUnitID.FirstOrDefault()
                                          && A.termID == inputs.termId
                                          && A.renovID == getRenovId.FirstOrDefault()
                                          && A.itemID == getItemId.FirstOrDefault()
                                          select A).Any();
                if (!checkUnitItemPrice)
                {
                    var msUnitItemPrice = new MS_UnitItemPrice
                    {
                        entityID = 1,
                        termID = inputs.termId,
                        itemID = getItemId.FirstOrDefault(),
                        renovID = getRenovId.FirstOrDefault(),
                        unitID = getUnitID.FirstOrDefault(),
                        grossPrice = input.grossPrice
                    };

                    Logger.DebugFormat("CreateUnitItemPrice() - Start Insert MS_UnitItemPrice. Parameters sent: {0} " +
                     "entityID       = 1 " +
                     "termID         = {1}{0} " +
                     "itemID         = {2}{0} " +
                     "renovID        = {3}{0} " +
                     "unitID         = {4}{0} " +
                     "grossPrice     = {5}{0}"
                    , Environment.NewLine, inputs.termId, getItemId.FirstOrDefault(), getRenovId.FirstOrDefault(), getUnitID.FirstOrDefault(), input.grossPrice);

                    _contextProp.MS_UnitItemPrice.Add(msUnitItemPrice);
                    _contextProp.SaveChanges();
                    //_msUnitItemPriceRepository.Insert(msUnitItemPrice);
                    Logger.InfoFormat("CreateUnitItemPrice() - Finished : " + inc + " of " + countLoop);
                    inc++;
                }
                else
                {
                    Logger.DebugFormat("CheckUnitItemPrice() - Start Check MS_UnitItemPrice. Parameters sent: {0} " +
                     "entityID       = 1 " +
                     "termID         = {1}{0} " +
                     "itemID         = {2}{0} " +
                     "renovID        = {3}{0} " +
                     "unitID         = {4}{0} " +
                     "grossPrice     = {5}{0}"
                    , Environment.NewLine, inputs.termId, getItemId.FirstOrDefault(), getRenovId.FirstOrDefault(), getUnitID.FirstOrDefault(), input.grossPrice);
                    throw new UserFriendlyException("Data already exist in MS_UnitItemPrice. EntityId: 1" +
                        " TermId: " + inputs.termId +
                        " ItemId: " + getItemId.FirstOrDefault() +
                        " RenovId: " + getRenovId.FirstOrDefault() +
                        " UnitId: " + getUnitID.FirstOrDefault());
                }
            }

            Logger.InfoFormat("CreateUnitItemPrice() - Finished.");
        }

        public bool CheckDataUploadGrossPrice(CheckDataUploadGrossPriceInputDto inputs)
        {
            bool NotFound = false;
            Logger.InfoFormat("CheckDataUploadGrossPrice() - Started.");
            var countLoop = inputs.DataUnit.ToList().Count;
            int inc = 1;
            List<MS_UnitItemPrice> ListUnitItemPrice = new List<MS_UnitItemPrice>();
            foreach (var input in inputs.DataUnit)
            {
                Logger.InfoFormat("CheckDataUploadGrossPrice() - Started : " + inc + " of " + countLoop);
                var getUnitCodeID = (from A in _contextProp.MS_UnitCode
                                     where A.entityID == 1
                                     && A.unitCode == input.UnitCode
                                     select A.Id);

                if (!getUnitCodeID.Any())
                {
                    Logger.InfoFormat("Unit Code ID with EntityId: " + 1 + " and UnitCode: " + input.UnitCode + " not found!");
                    throw new UserFriendlyException("Unit Code ID with EntityId: " + 1 + " and UnitCode: " + input.UnitCode + " not found!");
                }

                var getUnitID = (from A in _msUnitRepo.GetAll()
                                 where A.entityID == 1
                                 && A.unitCodeID == getUnitCodeID.FirstOrDefault()
                                 && A.unitNo == input.UnitNo
                                 && A.projectID == inputs.ProjectId
                                 && A.clusterID == inputs.ClusterId
                                 select A.Id);

                if (!getUnitID.Any())
                {
                    Logger.InfoFormat("Unit ID with EntityId: " + 1 + ", UnitCodeID: "
                        + getUnitCodeID.FirstOrDefault() + ", UnitNo: "
                        + input.UnitNo + ", Project ID "
                        + inputs.ProjectId + " and Cluster ID "
                        + inputs.ClusterId + " not found!");
                    throw new UserFriendlyException("Unit ID with EntityId: " + 1 + ", UnitCodeID: "
                        + getUnitCodeID.FirstOrDefault() + ", UnitNo: "
                        + input.UnitNo + ", Project ID "
                        + inputs.ProjectId + " and Cluster ID "
                        + inputs.ClusterId + " not found!");
                }

                var getRenovId = (from A in _contextProp.MS_Renovation
                                  where A.renovationCode == input.RenovCode
                                  && A.projectID == inputs.ProjectId
                                  select A.Id);

                if (!getRenovId.Any())
                {
                    Logger.InfoFormat("Renovation ID with RenovationCode: "
                        + input.RenovCode + " and Project ID "
                        + inputs.ProjectId + " not found!");
                    throw new UserFriendlyException("Renovation ID with RenovationCode: "
                        + input.RenovCode + " and Project ID "
                        + inputs.ProjectId + " not found!");
                }

                var getItemId = (from A in _contextProp.LK_Item
                                 where A.itemCode == input.ItemCode
                                 select A.Id);

                if (!getItemId.Any())
                {
                    Logger.InfoFormat("Item ID with ItemCode: "
                        + input.ItemCode + " not found!");
                    throw new UserFriendlyException("Item ID with ItemCode: "
                        + input.ItemCode + " not found!");
                }

                var msUnitItemPrice = new MS_UnitItemPrice
                {
                    entityID = 1,
                    itemID = getItemId.FirstOrDefault(),
                    renovID = getRenovId.FirstOrDefault(),
                    unitID = getUnitID.FirstOrDefault()
                };

                Logger.DebugFormat("CheckDataUploadGrossPrice() - Start Add Into List UnitItemPrice. Parameters sent: {0} " +
                 "entityID       = 1" +
                 "itemID         = {1}{0}" +
                 "renovID        = {2}{0}" +
                 "unitID         = {3}{0}"
                , Environment.NewLine, getItemId.FirstOrDefault(), getRenovId.FirstOrDefault(), getUnitID.FirstOrDefault());

                ListUnitItemPrice.Add(msUnitItemPrice);
                Logger.InfoFormat("CheckDataUploadGrossPrice() - Finished : " + inc + " of " + countLoop);
                inc++;
            }

            if (ListUnitItemPrice.Any())
            {
                NotFound = true;
            }
            var founded = NotFound == false ? "not Found." : "Founded.";
            Logger.InfoFormat("CheckDataUploadGrossPrice() - Ended, Data " + founded);
            return NotFound;
        }

        public bool CheckDuplicateDataExcel(List<CheckDuplicateDataInputDto> input)
        {
            bool isDuplicate = true;

            var checkDuplicateData = input.GroupBy(x => new
            {
                x.renovCode,
                x.unitCode,
                x.unitNo
            })
            .Where(g => g.Count() > 1)
            .Select(s => new
            {
                s.Key.renovCode,
                s.Key.unitCode,
                s.Key.unitNo,
                DuplicateCount = s.Count()
            }).ToList();

            if (checkDuplicateData.Count > 1)
            {
                var inc = 1;
                foreach (var X in checkDuplicateData)
                {
                    Logger.DebugFormat(inc + " of " + checkDuplicateData.Count + " Data Excel is Duplicate. Result = {0}", X);
                    inc++;
                }
                var firstData = checkDuplicateData.FirstOrDefault();

                throw new UserFriendlyException("Data Excel is Duplicate ! UnitCode: " + firstData.unitCode + ", UnitNo: " + firstData.unitNo + ", RenovCode: " + firstData.renovCode);
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;
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

        #region debug console
        protected void SendConsole(string msg)
        {
            if (Setting_variabel.enable_tcp_debug == true)
            {
                if (Setting_variabel.Komunikasi_TCPListener == null)
                {
                    Setting_variabel.Komunikasi_TCPListener = new Visionet_Backend_NetCore.Komunikasi.Komunikasi_TCPListener(17000);
                    Task.Run(() => StartListenerLokal());
                }

                if (Setting_variabel.Komunikasi_TCPListener != null)
                {
                    if (Setting_variabel.Komunikasi_TCPListener.IsRunning())
                    {
                        if (Setting_variabel.ConsoleBayangan != null)
                        {
                            Setting_variabel.ConsoleBayangan.Send("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + msg);
                        }
                    }
                }
            }

        }
        #endregion

        #region listener tcp debug
        protected void StartListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StartListener();
        }

        protected void StopListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StopListener();
        }
        #endregion
        private string L(string name)
        {
            return _localizationManager.GetString(DemoConsts.LocalizationSourceName, name);
        }
    }
}
