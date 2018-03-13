using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using VDI.Demo.Authorization;
using VDI.Demo.Configuration;
using VDI.Demo.Files;
using VDI.Demo.MasterPlan.Project.MS_Projects.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using VDI.Demo.SqlExecuter;

namespace VDI.Demo.MasterPlan.Project.MS_Projects
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject)]
    public class MsProjectAppService : DemoAppServiceBase, IMsProjectAppService
    {
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Entity> _msEntityRepo;
        private readonly IRepository<MS_Department> _msDepartmentRepo;
        private readonly IRepository<MS_Officer> _msOfficerRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly ISqlExecuter _sqlExecuter;
        private readonly FilesHelper _filesHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigurationRoot _appConfiguration;

        public MsProjectAppService(
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Entity> msEntityRepo,
            IRepository<MS_Department> msDepartmentRepo,
            IRepository<MS_Officer> msOfficerRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_Term> msTermRepo,
            ISqlExecuter sqlExecuter,
            FilesHelper filesHelper,
            IHostingEnvironment hostingEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _msEntityRepo = msEntityRepo;
            _msDepartmentRepo = msDepartmentRepo;
            _msOfficerRepo = msOfficerRepo;
            _msProjectRepo = msProjectRepo;
            _msUnitRepo = msUnitRepo;
            _msTermRepo = msTermRepo;
            _sqlExecuter = sqlExecuter;
            _filesHelper = filesHelper;
            _hostingEnvironment = hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
        }

        private string UploadImage(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\ProjectImage\", @"Assets\Upload\ProjectImage\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject_Delete)]
        private void DeleteImage(string filename)
        {
            var imageToDelete = filename;
            var filenameToDelete = imageToDelete.Split(@"/");
            var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
            var deletePath = @"\Assets\Upload\ProjectImage\";

            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

            if (File.Exists(deleteImage))
            {
                var file = new FileInfo(deleteImage);
                file.Delete();
            }
        }

        public ListResultDto<GetAllProjectListDto> GetAllMsProject()
        {
            var getAllProject = (from project in _msProjectRepo.GetAll()
                                 orderby project.CreationTime descending
                                 select new GetAllProjectListDto
                                 {
                                     Id = project.Id,
                                     projectCode = project.projectCode,
                                     projectName = project.projectName,
                                     mapping = project.isDMT ? "DMT" : "NON-DMT",
                                     isPublish = project.isPublish
                                 }).ToList();
            return new ListResultDto<GetAllProjectListDto>(getAllProject);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject_Create)]
        public void CreateMsProject(CreateMsProjectInputDto input)
        {
            Logger.Info("CreateMsProject() - Started.");

            Logger.DebugFormat("CreateMsProject() - Start checking exiting code and name. Params sent:{0}" +
                "projectCode   = {1}{0}" +
                "projectName   = {2}"
                , Environment.NewLine, input.projectCode, input.projectName);
            var checkNameCode = (from x in _msProjectRepo.GetAll()
                                 where x.projectCode == input.projectCode || x.projectName == input.projectName
                                 select x).Any();
            Logger.DebugFormat("CreateMsProject() - End checking exiting code and name. Result: {0}", checkNameCode);

            if (!checkNameCode)
            {
                // insert to MS_Project
                string imageUrl;
                if (input.image == null)
                {
                    imageUrl = "-";
                }
                else
                {
                    imageUrl = UploadImage(input.image);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                }

                var dataMsProject = new MS_Project
                {
                    entityID = input.entityID,
                    projectCode = input.projectCode,
                    projectName = input.projectName,
                    image = imageUrl,
                    //webImage = webImage,
                    isPublish = input.isPublish,
                    OperationalGroup = input.OperationalGroup,
                    TaxGroup = input.TaxGroup,
                    isDMT = input.isDMT,
                    DMT_ProjectGroupCode = input.DMT_ProjectGroupCode,
                    DMT_ProjectGroupName = input.DMT_ProjectGroupName,
                    callCenterManagerID = input.callCenterManagerID,
                    callCenterStaffID = input.callCenterStaffID,
                    bankRelationManagerID = input.bankRelationManagerID,
                    bankRelationStaffID = input.bankRelationStaffID,
                    SADBMID = input.SADBMID,
                    SADManagerID = input.SADManagerID,
                    SADStaffID = input.SADStaffID,
                    PGManagerID = input.PGManagerID,
                    PGStaffID = input.PGStaffID,
                    financeManagerID = input.financeManagerID,
                    financeStaffID = input.financeStaffID,
                    webSort = 0, //hardcode for not null field
                    SADManager = "-", //hardcode for not null field
                    SADStaff = "-", //hardcode for not null field
                    SADContact = "-", //hardcode for not null field
                    SADPhone = "-", //hardcode for not null field
                    SADFax = "-", //hardcode for not null field
                    SADBM = "-", //hardcode for not null field
                    isConfirmSP = false, //hardcode for not null field
                    //penaltyRate = input.penaltyRate, pindah ke ms_cluster
                    //startPenaltyDay = input.startPenaltyDay, pindah ke ms_cluster
                    isBookingCashier = false, //hardcode for not null field
                    isBookingSMS = false, //hardcode for not null field
                    unitNoGroupLength = 0, //hardcode for not null field
                    DIV_ID = "-", //hardcode for not null field
                    parentProjectName = "-", //hardcode for not null field
                    SADBMStaffID = input.SADBMStaffID
                };


                try
                {
                    Logger.DebugFormat("CreateMsProject() - Start insert project. Params sent:{0}" +
                        "	entityID	= {1}{0}" +
                        "	projectCode	= {2}{0}" +
                        "	projectName	= {3}{0}" +
                        "	image	= {4}{0}" +
                        "	isPublish	= {5}{0}" +
                        "	OperationalGroup	= {6}{0}" +
                        "	TaxGroup	= {7}{0}" +
                        "	isDMT	= {8}{0}" +
                        "	DMT_ProjectGroupCode	= {9}{0}" +
                        "	DMT_ProjectGroupName	= {10}{0}" +
                        "	callCenterManagerID	= {11}{0}" +
                        "	callCenterStaffID	= {12}{0}" +
                        "	bankRelationManagerID	= {13}{0}" +
                        "	bankRelationStaffID	= {14}{0}" +
                        "	SADBMID	= {15}{0}" +
                        "	SADManagerID	= {16}{0}" +
                        "	SADStaffID	= {17}{0}" +
                        "	PGManagerID	= {18}{0}" +
                        "	PGStaffID	= {19}{0}" +
                        "	financeManagerID	= {20}{0}" +
                        "	financeStaffID	= {21}{0}" +
                        "	webSort	= {22}{0}" +
                        "	SADManager	= {23}{0}" +
                        "	SADStaff	= {24}{0}" +
                        "	SADContact	= {25}{0}" +
                        "	SADPhone	= {26}{0}" +
                        "	SADFax	= {27}{0}" +
                        "	SADBM	= {28}{}" +
                        "	isConfirmSP	= {29}{0}" +
                        "	penaltyRate	= {30}{0}" +
                        "	startPenaltyDay	= {31}{0}" +
                        "	isBookingCashier	= {32}{0}" +
                        "	isBookingSMS	= {33}{0}" +
                        "	unitNoGroupLength	= {34}{0}" +
                        "	DIV_ID	= {35}{0}" +
                        "	parentProjectName	= {36}{0}" +
                        "   SADBMStaffID = {37}{0}"
                        , Environment.NewLine, input.entityID, input.projectCode, input.projectName, imageUrl, input.isPublish, input.OperationalGroup, input.TaxGroup, input.isDMT,
                        input.DMT_ProjectGroupCode, input.DMT_ProjectGroupName, input.callCenterManagerID, input.callCenterStaffID, input.bankRelationManagerID,
                        input.bankRelationStaffID, input.SADBMID, input.SADManagerID, input.SADStaffID, input.PGManagerID, input.PGStaffID, input.financeManagerID,
                        input.financeStaffID, 0, "-", "-", "-", "-", "-", "-", false, input.penaltyRate, input.startPenaltyDay, false, false, 0, "-", "-", input.SADBMStaffID);
                    _msProjectRepo.Insert(dataMsProject);
                    CurrentUnitOfWork.SaveChanges();
                    Logger.DebugFormat("CreateMsProject() - End insert project.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("CreateMsProject() ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsProject() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }

            }
            else
            {
                Logger.ErrorFormat("CreateMsProject() ERROR. Result = {0}", "Project Code or Project Name Already Exist !");
                throw new UserFriendlyException("Project Code or Project Name Already Exist !");
            }
            Logger.Info("CreateMsProject() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject_Edit)]
        public void UpdateMsProject(CreateMsProjectInputDto input)
        {
            Logger.Info("UpdateMsProject() - Started.");

            Logger.DebugFormat("UpdateMsProject() - Start checking existing code and name. Params sent:{0}" +
                "projectCode    = {1}{0}" +
                "projectName    = {2}{0}" +
                "projectID      = {3}",
                Environment.NewLine, input.projectCode, input.projectName, input.Id);
            var checkNameCode = (from x in _msProjectRepo.GetAll()
                                 where x.Id != input.Id && (x.projectCode == input.projectCode || x.projectName == input.projectName)
                                 select x).Any();
            Logger.DebugFormat("UpdateMsProject() - End checking existing code and name.");

            if (!checkNameCode)
            {
                var getMsProject = (from x in _msProjectRepo.GetAll()
                                    where x.Id == input.Id
                                    select x).FirstOrDefault();

                var updateMsProject = getMsProject.MapTo<MS_Project>();

                updateMsProject.entityID = input.entityID;
                updateMsProject.projectCode = input.projectCode;
                updateMsProject.projectName = input.projectName;
                updateMsProject.isPublish = input.isPublish;
                updateMsProject.OperationalGroup = input.OperationalGroup;
                updateMsProject.TaxGroup = input.TaxGroup;
                updateMsProject.isDMT = input.isDMT;
                updateMsProject.DMT_ProjectGroupCode = input.DMT_ProjectGroupCode;
                updateMsProject.DMT_ProjectGroupName = input.DMT_ProjectGroupName;
                updateMsProject.callCenterManagerID = input.callCenterManagerID;
                updateMsProject.callCenterStaffID = input.callCenterStaffID;
                updateMsProject.bankRelationManagerID = input.bankRelationManagerID;
                updateMsProject.bankRelationStaffID = input.bankRelationStaffID;
                updateMsProject.SADBMID = input.SADBMID;
                updateMsProject.SADManagerID = input.SADManagerID;
                updateMsProject.SADStaffID = input.SADStaffID;
                updateMsProject.PGStaffID = input.PGStaffID;
                updateMsProject.financeManagerID = input.financeManagerID;
                updateMsProject.financeStaffID = input.financeStaffID;
                //updateMsProject.penaltyRate = input.penaltyRate;
                //updateMsProject.startPenaltyDay = input.startPenaltyDay;

                updateMsProject.SADStaff = "-"; //hardcode for not null field
                updateMsProject.SADContact = "-"; //hardcode for not null field
                updateMsProject.SADFax = "-"; //hardcode for not null field
                updateMsProject.SADBM = "-"; //hardcode for not null field
                updateMsProject.SADBMStaffID = input.SADBMStaffID;

                if (input.image == "updated")
                {
                    var imageToDelete = getMsProject.image;
                    DeleteImage(imageToDelete);

                    var imageUrl = UploadImage(input.imageNew);
                    GetURLWithoutHost(imageUrl, out imageUrl);
                    updateMsProject.image = imageUrl;
                }
                else if (input.image == "removed")
                {
                    var imageToDelete = getMsProject.image;
                    DeleteImage(imageToDelete);

                    updateMsProject.image = "-";
                }
                else
                {
                    updateMsProject.image = getMsProject.image;
                }

                try
                {
                    Logger.DebugFormat("UpdateMsProject() - Start update project. Params sent:{0}" +
                    "entityID	= {1}{0}" +
                    "projectCode	= {2}{0}" +
                    "projectName	= {3}{0}" +
                    "isPublish	= {4}{0}" +
                    "OperationalGroup	= {5}{0}" +
                    "TaxGroup	= {6}{0}" +
                    "isDMT	= {7}{0}" +
                    "DMT_ProjectGroupCode	= {8}{0}" +
                    "DMT_ProjectGroupName	= {9}{0}" +
                    "callCenterManagerID	= {10}{0}" +
                    "callCenterStaffID	= {11}{0}" +
                    "bankRelationManagerID	= {12}{0}" +
                    "bankRelationStaffID	= {13}{0}" +
                    "SADBMID	= {14}{0}" +
                    "SADManagerID	= {15}{0}" +
                    "SADStaffID	= {16}{0}" +
                    "PGStaffID	= {17}{0}" +
                    "financeManagerID	= {18}{0}" +
                    "financeStaffID	= {19}{0}" +
                    "penaltyRate	= {20}{0}" +
                    "startPenaltyDay	= {21}{0}" +
                    "SADStaff   = {22}{0}" +
                    "SADContact = {23}{0}" +
                    "SADFax     = {24}{0}" +
                    "SADBM      = {25}{0}" +
                    "SADBMStaffID = {26}{0}",
                    Environment.NewLine, input.entityID, input.projectCode, input.projectName, input.isPublish, input.OperationalGroup, input.TaxGroup, input.isDMT,
                    input.DMT_ProjectGroupCode, input.DMT_ProjectGroupName, input.callCenterManagerID, input.callCenterStaffID, input.bankRelationManagerID,
                    input.bankRelationStaffID, input.SADBMID, input.SADManagerID, input.SADStaffID, input.PGStaffID, input.financeManagerID, input.financeStaffID,
                    input.penaltyRate, input.startPenaltyDay, "-", "-", "-", "-", input.SADBMStaffID);

                    _msProjectRepo.Update(updateMsProject);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("UpdateMsProject() - End update project.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateMsProject() ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsProject() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsProject() ERROR. Result = {0}", "Project Code or Project Name Already Exist !");
                throw new UserFriendlyException("Project Code or Project Name Already Exist !");
            }
            Logger.Info("UpdateMsProject() - Finished.");
        }

        public GetDetailMsProjectListDto GetDetailMsProject(int Id)
        {
            var projectInformation = (from A in _msProjectRepo.GetAll()
                                      join B in _msEntityRepo.GetAll() on A.entityID equals B.Id
                                      where A.Id == Id
                                      select new ProjectInformationListDto
                                      {
                                          entityID = A.entityID,
                                          entityName = B.entityName,
                                          //image = A.image,
                                          image = (A != null && A.image != null) ? (!A.image.Equals("-")) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.image) : null : null, //TODO link + ip host
                                          isPublish = A.isPublish,
                                          isDMT = A.isDMT,
                                          //penaltyRate = A.penaltyRate,
                                          projectCode = A.projectCode,
                                          projectName = A.projectName,
                                          projectGroupCode = A.DMT_ProjectGroupCode,
                                          projectGroupName = A.DMT_ProjectGroupName,
                                          //startPenaltyDay = A.startPenaltyDay,
                                          operationalGroupEntityCode = A.OperationalGroup,
                                          operationalGroup = (from x in _msEntityRepo.GetAll() where x.entityCode == A.OperationalGroup select x.entityName).FirstOrDefault(),
                                          taxGroupEntityCode = A.TaxGroup,
                                          taxGroup = (from x in _msEntityRepo.GetAll() where x.entityCode == A.TaxGroup select x.entityName).FirstOrDefault()
                                      }).FirstOrDefault();

            //Set StaffName PSAS
            var staffNamePSAS = (from A in _msProjectRepo.GetAll()
                                 join B in _msOfficerRepo.GetAll() on A.SADStaffID equals B.Id
                                 where A.Id == Id
                                 select new { B.Id, B.officerName }).FirstOrDefault();

            var psas = (from A in _msProjectRepo.GetAll()
                        join B in _msOfficerRepo.GetAll() on A.SADManagerID equals B.Id
                        join C in _msDepartmentRepo.GetAll() on B.departmentID equals C.Id
                        where A.Id == Id
                        select new PSASListDto
                        {
                            departementEmail = C.departmentEmail,
                            departementWhatsapp = C.departmentWhatsapp,
                            officerPhone = B.officerPhone,
                            managerName = B.officerName,
                            managerID = B.Id,
                            staffName = staffNamePSAS.officerName,
                            staffID = staffNamePSAS.Id
                        }).FirstOrDefault();

            var productGeneral = (from A in _msProjectRepo.GetAll()
                                  join B in _msOfficerRepo.GetAll() on A.PGStaffID equals B.Id
                                  join C in _msDepartmentRepo.GetAll() on B.departmentID equals C.Id
                                  where A.Id == Id
                                  select new PGListDto
                                  {
                                      departementEmail = C.departmentEmail,
                                      departementWhatsapp = C.departmentWhatsapp,
                                      officerPhone = B.officerPhone,
                                      PIC = B.officerName,
                                      picID = B.Id
                                  }).FirstOrDefault();

            var staffNameFinance = (from A in _msProjectRepo.GetAll()
                                    join B in _msOfficerRepo.GetAll() on A.financeStaffID equals B.Id
                                    where A.Id == Id
                                    select new { B.Id, B.officerName }).FirstOrDefault();

            var finance = (from A in _msProjectRepo.GetAll()
                           join B in _msOfficerRepo.GetAll() on A.financeManagerID equals B.Id
                           join C in _msDepartmentRepo.GetAll() on B.departmentID equals C.Id
                           where A.Id == Id
                           select new FinanceListDto
                           {
                               departementEmail = C.departmentEmail,
                               departementWhatsapp = C.departmentWhatsapp,
                               officerPhone = B.officerPhone,
                               managerName = B.officerName,
                               managerID = B.Id,
                               staffName = staffNameFinance.officerName,
                               staffID = staffNameFinance.Id
                           }).FirstOrDefault();

            var staffNameBankRelation = (from A in _msProjectRepo.GetAll()
                                         join B in _msOfficerRepo.GetAll() on A.bankRelationStaffID equals B.Id
                                         where A.Id == Id
                                         select new { B.Id, B.officerName }).FirstOrDefault();

            var bankRelation = (from A in _msProjectRepo.GetAll()
                                join B in _msOfficerRepo.GetAll() on A.bankRelationManagerID equals B.Id
                                join C in _msDepartmentRepo.GetAll() on B.departmentID equals C.Id
                                where A.Id == Id
                                select new BankRelationListDto
                                {
                                    departementEmail = C.departmentEmail,
                                    departementWhatsapp = C.departmentWhatsapp,
                                    officerPhone = B.officerPhone,
                                    managerName = B.officerName,
                                    managerID = B.Id,
                                    staffName = staffNameBankRelation.officerName,
                                    staffID = staffNameBankRelation.Id
                                }).FirstOrDefault();

            var staffNameCallCenter = (from A in _msProjectRepo.GetAll()
                                       join B in _msOfficerRepo.GetAll() on A.callCenterStaffID equals B.Id
                                       where A.Id == Id
                                       select new { B.Id, B.officerName }).FirstOrDefault();

            var callCenter = (from A in _msProjectRepo.GetAll()
                              join B in _msOfficerRepo.GetAll() on A.callCenterManagerID equals B.Id
                              join C in _msDepartmentRepo.GetAll() on B.departmentID equals C.Id
                              where A.Id == Id
                              select new CallCenterListDto
                              {
                                  departementEmail = C.departmentEmail,
                                  departementWhatsapp = C.departmentWhatsapp,
                                  officerPhone = B.officerPhone,
                                  managerName = B.officerName,
                                  managerID = B.Id,
                                  staffName = staffNameCallCenter.officerName,
                                  staffID = staffNameCallCenter.Id
                              }).FirstOrDefault();

            var staffNameBuilding = (from A in _msProjectRepo.GetAll()
                                     join B in _msOfficerRepo.GetAll() on A.SADBMStaffID equals B.Id
                                     where A.Id == Id
                                     select new { B.Id, B.officerName }).FirstOrDefault();

            var buildingManager = (from A in _msProjectRepo.GetAll()
                                   join B in _msOfficerRepo.GetAll() on A.SADBMID equals B.Id
                                   join C in _msDepartmentRepo.GetAll() on B.departmentID equals C.Id
                                   where A.Id == Id
                                   select new BuildingManagerListDto
                                   {
                                       departementEmail = C.departmentEmail,
                                       departementWhatsapp = C.departmentWhatsapp,
                                       officerPhone = B.officerPhone,
                                       managerID = B.Id,
                                       managerName = B.officerName,
                                       staffName = staffNameBuilding.officerName,
                                       staffID = staffNameBuilding.Id
                                   }).FirstOrDefault();

            var dataPIC = new PICInformationListDto
            {
                PSAS = psas,
                BankRelation = bankRelation,
                BuildingManager = buildingManager,
                CallCenter = callCenter,
                Finance = finance,
                PG = productGeneral
            };

            var dataFinal = new GetDetailMsProjectListDto
            {
                ProjectInformation = projectInformation,
                PICInformation = dataPIC
            };

            return dataFinal;
        }

        public void ModifyDMT(GetUpdateDmtValueInputDto input)
        {
            //Modify Key
            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var webConfigApp = (JObject)appsettingsjson["App"];
            webConfigApp.Property("queryDMT").Value = input.query;

            ////var webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            ////webConfigApp.AppSettings.Settings["queryDMT"].Value = input.query;
            //webConfigApp.Save();
            //ConfigurationManager.RefreshSection("appSettings");

            ////Modify ConnString
            //var connectionStringsSection = (ConnectionStringsSection)webConfigApp.GetSection("connectionStrings");
            //connectionStringsSection.ConnectionStrings["ModifyWebConfigDMT"].ConnectionString = "Data Source=" + input.serverName + ";Initial Catalog=" + input.dbName + ";User ID=" + input.credentialUser + ";password=" + input.credentialPass + ";";
            //webConfigApp.Save();
            //ConfigurationManager.RefreshSection("connectionStrings");
        }

        public GetUpdateDmtValueInputDto GetDMT()
        {
            GetUpdateDmtValueInputDto data = new GetUpdateDmtValueInputDto();

            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var webConfigApp = (JObject)appsettingsjson["App"];
            var queryDMT = webConfigApp.Property("queryDMT").Value.ToString();
            data.query = queryDMT;

            var connectionStringsSection = (JObject)appsettingsjson["ConnectionStrings"];
            var connString = connectionStringsSection.Property("ModifyWebConfigDMT").ToString();

            //var webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            //var queryDMT = webConfigApp.AppSettings.Settings["queryDMT"].Value;
            //data.query = queryDMT;

            //var connectionStringsSection = (ConnectionStringsSection)webConfigApp.GetSection("connectionStrings");
            //var connString = connectionStringsSection.ConnectionStrings["ModifyWebConfigDMT"].ToString();

            if (connString != "" && connString != null)
            {
                string serverName = null;
                string dbName = null;
                string credentialUser = null;
                string credentialPass = null;
                string pwd = "password=";

                string dataSource = "Data Source=";
                string initialCatalog = "Initial Catalog=";
                string userID = "User ID=";
                string password = pwd;

                //start index
                int iServerName = connString.IndexOf(dataSource);
                int iInitialCatalog = connString.IndexOf(initialCatalog);
                int iUserID = connString.IndexOf(userID);
                int iPassword = connString.IndexOf(password);

                //end index
                int eServerName = connString.IndexOf(";", iServerName);
                int eInitialCatalog = connString.IndexOf(";", iInitialCatalog);
                int eUserID = connString.IndexOf(";", iUserID);
                int ePassword = connString.IndexOf(";", iPassword);

                serverName = connString.Substring(iServerName, eServerName - iServerName);
                dbName = connString.Substring(iInitialCatalog, eInitialCatalog - iInitialCatalog);
                credentialUser = connString.Substring(iUserID, eUserID - iUserID);
                credentialPass = connString.Substring(iPassword, ePassword - iPassword);

                serverName = serverName.Substring(dataSource.Length);
                dbName = dbName.Substring(initialCatalog.Length);
                credentialUser = credentialUser.Substring(userID.Length);
                credentialPass = credentialPass.Substring(password.Length);

                data = new GetUpdateDmtValueInputDto
                {
                    query = queryDMT,
                    credentialUser = credentialUser,
                    credentialPass = credentialPass,
                    serverName = serverName,
                    dbName = dbName
                };
            }

            return data;
        }

        public List<GetMappingDMTListDto> GetMappingDMT()
        {
            var result = GetDMT();

            var query = _sqlExecuter.GetFromEngin3<GetMappingDMTListDto>(result.query).ToList();

            return query;
        }

        public List<GetMappingCorsecListDto> GetMappingCorsec()
        {
            var result = GetCorsec();

            var query = _sqlExecuter.GetFromEngin3<GetMappingCorsecListDto>(result.query).ToList();

            return query;
        }

        public void ModifyCorsec(GetUpdateDmtValueInputDto input)
        {
            ////Modify Key
            //var webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            //webConfigApp.AppSettings.Settings["queryCorsec"].Value = input.query;
            //webConfigApp.Save();
            //ConfigurationManager.RefreshSection("appSettings");

            ////Modify ConnString
            //var connectionStringsSection = (ConnectionStringsSection)webConfigApp.GetSection("connectionStrings");
            //connectionStringsSection.ConnectionStrings["ModifyWebConfigCorsec"].ConnectionString = "Data Source=" + input.serverName + ";Initial Catalog=" + input.dbName + ";User ID=" + input.credentialUser + ";password=" + input.credentialPass + ";";
            //webConfigApp.Save();
            //ConfigurationManager.RefreshSection("connectionStrings");
        }

        public GetUpdateDmtValueInputDto GetCorsec()
        {
            GetUpdateDmtValueInputDto data = new GetUpdateDmtValueInputDto();


            var appsettingsjson = JObject.Parse(File.ReadAllText("appsettings.json"));
            var webConfigApp = (JObject)appsettingsjson["App"];
            var queryCorsec = webConfigApp.Property("queryCorsec").Value.ToString();
            data.query = queryCorsec;

            var connectionStringsSection = (JObject)appsettingsjson["ConnectionStrings"];
            var connString = connectionStringsSection.Property("ModifyWebConfigCorsec").Value.ToString();

            //var webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            //var queryCorsec = webConfigApp.AppSettings.Settings["queryCorsec"].Value;
            //data.query = queryCorsec;

            //var connectionStringsSection = (ConnectionStringsSection)webConfigApp.GetSection("connectionStrings");
            //var connString = connectionStringsSection.ConnectionStrings["ModifyWebConfigCorsec"].ToString();

            if (connString != "" && connString != null)
            {
                string serverName = null;
                string dbName = null;
                string credentialUser = null;
                string credentialPass = null;
                string pwd = "password=";

                string dataSource = "Data Source=";
                string initialCatalog = "Initial Catalog=";
                string userID = "User ID=";
                string password = pwd;

                //start index
                int iServerName = connString.IndexOf(dataSource);
                int iInitialCatalog = connString.IndexOf(initialCatalog);
                int iUserID = connString.IndexOf(userID);
                int iPassword = connString.IndexOf(password);

                //end index
                int eServerName = connString.IndexOf(";", iServerName);
                int eInitialCatalog = connString.IndexOf(";", iInitialCatalog);
                int eUserID = connString.IndexOf(";", iUserID);
                int ePassword = connString.IndexOf(";", iPassword);

                serverName = connString.Substring(iServerName, eServerName - iServerName);
                dbName = connString.Substring(iInitialCatalog, eInitialCatalog - iInitialCatalog);
                credentialUser = connString.Substring(iUserID, eUserID - iUserID);
                credentialPass = connString.Substring(iPassword, ePassword - iPassword);

                serverName = serverName.Substring(dataSource.Length);
                dbName = dbName.Substring(initialCatalog.Length);
                credentialUser = credentialUser.Substring(userID.Length);
                credentialPass = credentialPass.Substring(password.Length);

                data = new GetUpdateDmtValueInputDto
                {
                    query = queryCorsec,
                    credentialUser = credentialUser,
                    credentialPass = credentialPass,
                    serverName = serverName,
                    dbName = dbName
                };
            }

            return data;
        }

        public void DeleteMsProject(int Id)
        {
            Logger.Info("DeleteMsProject() - Started.");

            Logger.DebugFormat("DeleteMsProject() - Start checking data unit with projectID: {0}", Id);
            bool checkUnit = (from unit in _msUnitRepo.GetAll()
                              where unit.projectID == Id
                              select unit.Id).Any();
            Logger.DebugFormat("DeleteMsProject() - End checking data unit. Result: {0}", checkUnit);

            Logger.DebugFormat("DeleteMsProject() - Start checking data term with projectID: {0}", Id);
            bool checkTerm = (from term in _msTermRepo.GetAll()
                              where term.projectID == Id
                              select term.Id).Any();
            Logger.DebugFormat("DeleteMsProject() - End checking data term. Result: {0}", checkTerm);

            if (!checkUnit && !checkTerm)
            {
                var getImage = (from x in _msProjectRepo.GetAll()
                                where x.Id == Id
                                select x.image).FirstOrDefault();
                try
                {
                    DeleteImage(getImage);
                    Logger.DebugFormat("DeleteMsProject() - Start delete project. Params sent: {0}", Id);
                    _msProjectRepo.Delete(Id);
                    Logger.DebugFormat("DeleteMsProject() - End delete project");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteMsProject() ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsProject() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }

            }
            else
            {
                Logger.ErrorFormat("DeleteMsProject() ERROR. Result = {0}", "This Project is used!");
                throw new UserFriendlyException("This Project is used!");
            }
            Logger.Info("DeleteMsProject() - Finished.");
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
    }
}
