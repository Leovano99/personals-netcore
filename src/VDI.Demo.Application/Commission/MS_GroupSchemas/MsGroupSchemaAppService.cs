using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_GroupSchemas.Dto;
using VDI.Demo.EntityFrameworkCore;
using VDI.Demo.Files;
using VDI.Demo.NewCommDB;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using Visionet_Backend_NetCore.Komunikasi;

namespace VDI.Demo.Commission.MS_GroupSchemas
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject)]
    public class MsGroupSchemaAppService : DemoAppServiceBase, IMsGroupSchemaAppService
    {
        private readonly IRepository<MS_Schema> _msSchemaRepo;
        private readonly IRepository<MS_SchemaRequirement> _msSchemaRequirementRepo;
        private readonly IRepository<LK_CommType> _lkCommTypeRepo;
        private readonly IRepository<MS_StatusMember> _msStatusMemberRepo;
        private readonly IRepository<LK_PointType> _lkPointTypeRepo;
        private readonly IRepository<LK_Upline> _lkUplineRepo;
        private readonly IRepository<MS_CommPct> _msCommPctRepo;
        private readonly IRepository<MS_Property> _msPropertyRepo;
        private readonly IRepository<MS_Developer_Schema> _msDeveloperSchemaRepo;
        private readonly IRepository<MS_GroupSchema> _msGroupSchemaRepo;
        private readonly IRepository<MS_GroupCommPct> _msGroupCommPctRepo;
        private readonly IRepository<MS_GroupCommPctNonStd> _msGroupCommPctNonStdRepo;
        private readonly IRepository<MS_PPhRange> _msPPhRangeRepo;
        private readonly IRepository<MS_PPhRangeIns> _msPPhRangeInsRepo;
        private readonly IRepository<MS_PointPct> _msPointPctRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_GroupSchemaRequirement> _msGrpSchemaReqRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private static IHttpContextAccessor _HttpContextAccessor;
        private readonly FilesHelper _filesHelper;
        private readonly NewCommDbContext _context;
        private readonly PropertySystemDbContext _contextProp;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MsGroupSchemaAppService(
            IRepository<MS_Schema> msSchemaRepo,
            IRepository<MS_SchemaRequirement> msSchemaRequirementRepo,
            IRepository<LK_CommType> lkCommTypeRepo,
            IRepository<MS_StatusMember> msStatusMemberRepo,
            IRepository<LK_PointType> lkPointTypeRepo,
            IRepository<LK_Upline> lkUplineRepo,
            IRepository<MS_CommPct> msCommPctRepo,
            IRepository<MS_Property> msPropertyRepo,
            IRepository<MS_Developer_Schema> msDeveloperSchemaRepo,
            IRepository<MS_GroupSchema> msGroupSchemaRepo,
            IRepository<MS_GroupCommPct> msGroupCommPctRepo,
            IRepository<MS_GroupCommPctNonStd> msGroupCommPctNonStdRepo,
            IRepository<MS_PPhRange> msPPhRangeRepo,
            IRepository<MS_PPhRangeIns> msPPhRangeInsRepo,
            IRepository<MS_PointPct> msPointPctRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_GroupSchemaRequirement> msGrpSchemaReqRepo,
            IRepository<MS_Unit> msUnitRepo,
            IHttpContextAccessor httpContextAccessor,
            FilesHelper filesHelper,
            NewCommDbContext context,
            PropertySystemDbContext contextProp,
             IHostingEnvironment environment
        )
        {
            _msSchemaRepo = msSchemaRepo;
            _msSchemaRequirementRepo = msSchemaRequirementRepo;
            _lkCommTypeRepo = lkCommTypeRepo;
            _msStatusMemberRepo = msStatusMemberRepo;
            _lkPointTypeRepo = lkPointTypeRepo;
            _lkUplineRepo = lkUplineRepo;
            _msCommPctRepo = msCommPctRepo;
            _msPropertyRepo = msPropertyRepo;
            _msDeveloperSchemaRepo = msDeveloperSchemaRepo;
            _msGroupSchemaRepo = msGroupSchemaRepo;
            _msGroupCommPctRepo = msGroupCommPctRepo;
            _msGroupCommPctNonStdRepo = msGroupCommPctNonStdRepo;
            _msPPhRangeRepo = msPPhRangeRepo;
            _msPPhRangeInsRepo = msPPhRangeInsRepo;
            _msPointPctRepo = msPointPctRepo;
            _msProjectRepo = msProjectRepo;
            _msClusterRepo = msClusterRepo;
            _msGrpSchemaReqRepo = msGrpSchemaReqRepo;
            _msUnitRepo = msUnitRepo;
            _HttpContextAccessor = httpContextAccessor;
            _filesHelper = filesHelper;
            _context = context;
            _contextProp = contextProp;
            _hostingEnvironment = environment;
        }

        private static Uri getAbsolutUri()
        {
            var request = _HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }

        private string uploadFile(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\SchemaPerProjectFile\", @"Assets\Upload\SchemaPerProjectFile\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        [UnitOfWork(isTransactional: false)]
        public async Task<PagedResultDto<GetAllMsGroupSchemaListDto>> GetAllMsGroupSchema(GetMsGroupSchemaListInput input, bool isComplete) //passed
        {
            List<GetAllMsGroupSchemaListDto> listResult = new List<GetAllMsGroupSchemaListDto>();
            try
            {
                #region old version
                /*
                var dataGroup = (from groupSchema in _msGroupSchemaRepo.GetAll()
                                 where groupSchema.isComplete == true
                                 select new
                                 {
                                     groupSchema.projectID,
                                     groupSchema.groupSchemaCode,
                                     groupSchema.groupSchemaName,
                                     groupSchema.isActive,
                                     groupSchema.isComplete
                                 })
                                .ToList()
                                 ;

                var getGroupSchema = (from resultGroup in dataGroup
                                      join project in _msProjectRepo.GetAll() on resultGroup.projectID equals project.Id
                                      where resultGroup.isComplete == true
                                      group resultGroup by new { resultGroup.groupSchemaCode, resultGroup.groupSchemaName, project.projectName, resultGroup.isActive } into grp
                                      orderby grp.Key.groupSchemaCode
                                      select new GetAllMsGroupSchemaListDto
                                      {
                                          groupSchemaCode = grp.Key.groupSchemaCode,
                                          groupSchemaName = grp.Key.groupSchemaName,
                                          projectName = grp.Key.projectName,
                                          isActive = grp.Key.isActive,
                                      }).Distinct();
                                      */
                #endregion

                #region compact version
                listResult = await (from resultGroup in _context.MS_GroupSchema.ToList()
                                    join project in _contextProp.MS_Project.ToList() on resultGroup.projectID equals project.Id
                                    where resultGroup.isActive == true && resultGroup.isComplete == isComplete
                                    group resultGroup by new { resultGroup.groupSchemaCode, resultGroup.groupSchemaName, project.projectName, resultGroup.isActive } into grp
                                    orderby grp.Key.groupSchemaCode descending
                                    select new GetAllMsGroupSchemaListDto
                                    {
                                        groupSchemaCode = grp.Key.groupSchemaCode,
                                        groupSchemaName = grp.Key.groupSchemaName,
                                        projectName = grp.Key.projectName,
                                        isActive = grp.Key.isActive,
                                    }).Distinct()
                                      .Skip(input.MaxResultCount * input.SkipCount)
                                      .Take(input.MaxResultCount).ToAsyncEnumerable().ToList();
                #endregion             
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return new PagedResultDto<GetAllMsGroupSchemaListDto>(
                listResult.Count,
                listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Create)]
        public List<ReturnMsGroupSchemaDto> CreateOrUpdateMsGroupSchema(CreateOrUpdateSetGroupSchemaInputDto input)
        {
            Logger.Info("CreateOrUpdateMsGroupSchema() - Started.");

            List<ReturnMsGroupSchemaDto> listID = new List<ReturnMsGroupSchemaDto>();

            foreach (var item in input.setSchema)
            {
                if (item.groupSchemaID == null || item.groupSchemaID == 0)
                {
                    Logger.Info("CreateOrUpdateMsGroupSchema() - Create Started.");

                    Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Start checking before insert Group Schema. Parameters sent:{0}" +
                        "clusterID = {1}{0}" +
                        "isComplete = {2}{0}" +
                        "groupSchemaCode = {3}{0}" +
                        "groupSchemaName = {4}{0}"
                        , Environment.NewLine, input.clusterID, true, input.groupSchemaCode, input.groupSchemaName);

                    var check = (from x in _msGroupSchemaRepo.GetAll()
                                 where x.clusterID != input.clusterID && x.isComplete == true && (x.groupSchemaCode == input.groupSchemaCode || x.groupSchemaName == input.groupSchemaName)
                                 select x).Any();

                    Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Ended checking before insert Group Schema. Result = {0}", check);

                    if (!check)
                    {
                        string docPath = null;

                        if (input.documentGrouping != null)
                        {
                            docPath = uploadFile(input.documentGrouping);
                            GetURLWithoutHost(docPath, out docPath);
                        }

                        var createMsGroupSchema = new MS_GroupSchema
                        {
                            entityID = 1,
                            groupSchemaCode = input.groupSchemaCode,
                            groupSchemaName = input.groupSchemaName,
                            isStandard = true,
                            validFrom = input.validFrom,
                            documentGrouping = docPath,
                            projectID = input.projectID,
                            clusterID = input.clusterID,
                            schemaID = item.schemaID,
                            isComplete = input.isComplete,
                            isActive = input.isActive
                        };

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Start insert Group Schema. Parameters sent:{0}" +
                                                "entityID = {1}{0}" +
                                                "groupSchemaCode = {2}{0}" +
                                                "groupSchemaName = {3}{0}" +
                                                "isStandard = {4}{0}" +
                                                "validFrom = {5}{0}" +
                                                "documentGrouping = {6}{0}" +
                                                "projectID = {7}{0}" +
                                                "clusterID = {8}{0}" +
                                                "schemaID = {9}{0}" +
                                                "isComplete = {10}{0}" +
                                                "isActive = {11}{0}"
                                                , Environment.NewLine, 1, input.groupSchemaCode, input.groupSchemaName, true
                                                , input.validFrom, docPath, input.projectID, input.clusterID, item.schemaID, input.isComplete, input.isActive);

                            var groupSchemaID = _msGroupSchemaRepo.InsertAndGetId(createMsGroupSchema);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Ended insert Group Schema.");

                            var data = new ReturnMsGroupSchemaDto()
                            {
                                groupSchemaID = groupSchemaID,
                                schemaID = item.schemaID
                            };

                            listID.Add(data);
                        }
                        // Handle data errors.
                        catch (DataException exDb)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupSchema() - ERROR DataException. Result = {0}", exDb.Message);
                            throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                        }
                        // Handle all other exceptions.
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupSchema() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error : {0}", ex.Message);
                        }
                    }
                    else
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsGroupSchema() - ERROR. Result = {0}", "The Code or Name Already Exist !");
                        throw new UserFriendlyException("The Code or Name Already Exist !");
                    }
                    Logger.Info("CreateOrUpdateMsGroupSchema() - Create Finished.");
                }
                //update
                else
                {
                    Logger.Info("CreateOrUpdateMsGroupSchema() - Update Started.");

                    Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Start checking before update Group Schema. Parameters sent:{0}" +
                        "clusterID = {1}{0}" +
                        "isComplete = {2}{0}" +
                        "groupSchemaCode = {3}{0}" +
                        "groupSchemaName = {4}{0}" +
                        "groupSchemaID = {5}{0}"
                        , Environment.NewLine, input.clusterID, true, input.groupSchemaCode, input.groupSchemaName, item.groupSchemaID);

                    var check = (from x in _msGroupSchemaRepo.GetAll()
                                 where x.Id != item.groupSchemaID && x.clusterID != input.clusterID && x.isComplete == true && (x.groupSchemaCode == input.groupSchemaCode || x.groupSchemaName == input.groupSchemaName)
                                 select x).Any();

                    Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Ended checking before update Group Schema. Result = {0}", check);

                    if (!check)
                    {
                        Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Start get data before update Group Schema. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}"
                                            , Environment.NewLine, item.groupSchemaID);

                        var getMsGroupSChema = (from x in _msGroupSchemaRepo.GetAll()
                                                where item.groupSchemaID == x.Id
                                                select x).FirstOrDefault();

                        Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Ended get data before update Group Schema. Result = {0}", getMsGroupSChema);

                        var update = getMsGroupSChema.MapTo<MS_GroupSchema>();

                        update.groupSchemaCode = input.groupSchemaCode;
                        update.groupSchemaName = input.groupSchemaName;
                        update.validFrom = input.validFrom;
                        update.documentGrouping = input.documentGrouping;
                        update.projectID = input.projectID;
                        update.clusterID = input.clusterID;
                        update.schemaID = item.schemaID;
                        update.isComplete = input.isComplete;
                        update.isActive = input.isActive;

                        var fileToDelete = input.documentGroupingDelete;

                        if (fileToDelete != null)
                        {
                            var filenameToDelete = fileToDelete.Split(@"/");
                            var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
                            var deletePath = @"\Assets\Upload\GroupSchemaFile\";

                            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

                            if (File.Exists(deleteImage))
                            {
                                var file = new FileInfo(deleteImage);
                                file.Delete();
                            }
                        }

                        if (input.statusDocument == "update")
                        {
                            var docPath = uploadFile(input.documentGrouping);
                            GetURLWithoutHost(docPath, out docPath);
                            update.documentGrouping = docPath;
                        }

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Start update Group Schema. Parameters sent:{0}" +
                                                "groupSchemaCode = {1}{0}" +
                                                "groupSchemaName = {2}{0}" +
                                                "validFrom = {3}{0}" +
                                                "projectID = {4}{0}" +
                                                "clusterID = {5}{0}" +
                                                "schemaID = {6}{0}" +
                                                "isComplete = {7}{0}" +
                                                "isActive = {8}{0}"
                                                , Environment.NewLine, input.groupSchemaCode, input.groupSchemaName
                                                , input.validFrom, input.projectID, input.clusterID, item.schemaID, input.isComplete, input.isActive);

                            _msGroupSchemaRepo.Update(update);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("CreateOrUpdateMsGroupSchema() - Ended update Group Schema.");

                            var data = new ReturnMsGroupSchemaDto()
                            {
                                groupSchemaID = item.groupSchemaID,
                                schemaID = item.schemaID
                            };

                            listID.Add(data);
                        }
                        // Handle data errors.
                        catch (DataException exDb)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupSchema() - ERROR DataException. Result = {0}", exDb.Message);
                            throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                        }
                        // Handle all other exceptions.
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupSchema() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error : {0}", ex.Message);
                        }
                    }
                    else
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsGroupSchema() - ERROR. Result = {0}", "The Code or the Name Already Exist !");
                        throw new UserFriendlyException("The Code or the Name Already Exist !");
                    }

                    Logger.Info("CreateOrUpdateMsGroupSchema() - Update Finished.");
                }
            }
            Logger.Info("CreateOrUpdateMsGroupSchema() - Finished.");

            return listID;

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Create)]
        public void CreateOrUpdateMsGroupCommPct(List<CreateOrUpdateSetPercentCommGroupInputDto> input, string flag)
        {
            Logger.Info("CreateOrUpdateMsGroupCommPct() - Started.");

            foreach (var groupComm in input)
            {
                if (groupComm.groupCommPctID == null || groupComm.groupCommPctID == 0)
                {
                    Logger.Info("CreateOrUpdateMsGroupCommPct() - Insert Started.");


                    var createMsGroupCommPct = new MS_GroupCommPct();
                    var createMsGroupCommPctNonStd = new MS_GroupCommPctNonStd();
                    if (flag == "group")
                    {
                        createMsGroupCommPct = new MS_GroupCommPct
                        {
                            groupSchemaID = groupComm.groupSchemaID,
                            validDate = groupComm.validDate,
                            commTypeID = groupComm.commTypeID,
                            statusMemberID = groupComm.statusMemberID,
                            asUplineNo = groupComm.uplineNo,
                            commPctPaid = groupComm.commPctPaid,
                            nominal = groupComm.nominal,
                            commPctHold = 0,
                            maxAmt = 0,
                            minAmt = 0,
                            isComplete = groupComm.isComplete
                        };
                    }
                    else
                    {
                        createMsGroupCommPctNonStd = new MS_GroupCommPctNonStd
                        {
                            entityID = 1,
                            groupSchemaID = groupComm.groupSchemaID,
                            validDate = groupComm.validDate,
                            commTypeID = groupComm.commTypeID,
                            statusMemberID = groupComm.statusMemberID,
                            asUplineNo = groupComm.uplineNo,
                            commPctPaid = groupComm.commPctPaid,
                            nominal = groupComm.nominal,
                            commPctHold = 0,
                            maxAmt = 0,
                            minAmt = 0,
                            isComplete = groupComm.isComplete
                        };
                    }

                    try
                    {
                        bool isStandard = false;
                        if (flag == "group")
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start insert Group Comm Pct. Parameters sent:{0}" +
                                                "entityID = {1}{0}" +
                                                "groupSchemaID = {2}{0}" +
                                                "validDate = {3}{0}" +
                                                "commTypeID = {4}{0}" +
                                                "statusMemberID = {5}{0}" +
                                                "asUplineNo = {6}{0}" +
                                                "commPctPaid = {7}{0}" +
                                                "nominal = {8}{0}" +
                                                "commPctHold = {9}{0}" +
                                                "maxAmt = {10}{0}" +
                                                "minAmt = {11}{0}" +
                                                "isComplete = {12}{0}"
                                                , Environment.NewLine, 1, groupComm.groupSchemaID, groupComm.validDate, groupComm.commTypeID
                                                , groupComm.statusMemberID, groupComm.uplineNo, groupComm.commPctPaid, groupComm.nominal, 0, 0, 0, groupComm.isComplete);

                            _msGroupCommPctRepo.Insert(createMsGroupCommPct);
                            isStandard = true;

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended insert Group Comm Pct.");

                        }
                        else
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start insert Group Comm Pct Non STD. Parameters sent:{0}" +
                                                "entityID = {1}{0}" +
                                                "groupSchemaID = {2}{0}" +
                                                "validDate = {3}{0}" +
                                                "commTypeID = {4}{0}" +
                                                "statusMemberID = {5}{0}" +
                                                "asUplineNo = {6}{0}" +
                                                "commPctPaid = {7}{0}" +
                                                "nominal = {8}{0}" +
                                                "commPctHold = {9}{0}" +
                                                "maxAmt = {10}{0}" +
                                                "minAmt = {11}{0}" +
                                                "isComplete = {12}{0}"
                                                , Environment.NewLine, 1, groupComm.groupSchemaID, groupComm.validDate, groupComm.commTypeID
                                                , groupComm.statusMemberID, groupComm.uplineNo, groupComm.commPctPaid, groupComm.nominal, 0, 0, 0, groupComm.isComplete);

                            _msGroupCommPctNonStdRepo.Insert(createMsGroupCommPctNonStd);

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended insert Group Comm Pct Non STD.");
                        }

                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start get data before update Group Schema. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}"
                                            , Environment.NewLine, groupComm.groupSchemaID);

                        var getGroupSchema = (from B in _msGroupSchemaRepo.GetAll()
                                              where B.Id == groupComm.groupSchemaID
                                              select B).FirstOrDefault();

                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended get data before update Group Schema.");

                        var update = getGroupSchema.MapTo<MS_GroupSchema>();
                        update.isStandard = isStandard;

                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start update isStandard Group Schema. Parameters sent:{0}" +
                                            "isStandard = {1}{0}"
                                            , Environment.NewLine, isStandard);

                        _msGroupSchemaRepo.Update(update);

                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended update isStandard Group Schema.");


                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }

                    Logger.Info("CreateOrUpdateMsGroupCommPct() - Insert Finished.");
                }
                //update
                else
                {
                    Logger.Info("CreateOrUpdateMsGroupCommPct() - Update Started.");

                    Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start get data before update Group Schema. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}"
                                            , Environment.NewLine, groupComm.groupSchemaID);

                    var getGroupSchema = (from B in _msGroupSchemaRepo.GetAll()
                                          where B.Id == groupComm.groupSchemaID
                                          select B).FirstOrDefault();

                    Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended get data before update Group Schema.");

                    var updateGroupSchema = getGroupSchema.MapTo<MS_GroupSchema>();

                    if (flag == "group")
                    {
                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start checking before update Group Comm Pct. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}" +
                                            "isComplete = {2}{0}"
                                            , Environment.NewLine, groupComm.groupSchemaID, groupComm.isComplete);

                        var cek = (from A in _msGroupCommPctRepo.GetAll()
                                   where A.groupSchemaID == groupComm.groupSchemaID && A.isComplete == groupComm.isComplete
                                   select A).Any();

                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended checking before update Group Comm Pct.");

                        if (cek)
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start get data before update Group Comm Pct. Parameters sent:{0}" +
                                            "groupCommPctID = {1}{0}" +
                                            "isComplete = {2}{0}"
                                            , Environment.NewLine, groupComm.groupCommPctID, groupComm.isComplete);

                            var getGroupCommPct = (from A in _msGroupCommPctRepo.GetAll()
                                                   where A.Id == groupComm.groupCommPctID && A.isComplete == groupComm.isComplete
                                                   select A).FirstOrDefault();

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended get data before update Group Comm Pct.");

                            var updateGroupCommPct = getGroupCommPct.MapTo<MS_GroupCommPct>();
                            updateGroupCommPct.groupSchemaID = groupComm.groupSchemaID;
                            updateGroupCommPct.commTypeID = groupComm.commTypeID;
                            updateGroupCommPct.statusMemberID = groupComm.statusMemberID;
                            updateGroupCommPct.asUplineNo = groupComm.uplineNo;
                            updateGroupCommPct.commPctPaid = groupComm.commPctPaid;
                            updateGroupCommPct.nominal = groupComm.nominal;
                            updateGroupCommPct.commPctHold = 0;
                            updateGroupCommPct.maxAmt = 0;
                            updateGroupCommPct.minAmt = 0;
                            updateGroupCommPct.isComplete = groupComm.isComplete;

                            try
                            {
                                Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start update Group Comm Pct. Parameters sent:{0}" +
                                                "groupSchemaID = {1}{0}" +
                                                "commTypeID = {2}{0}" +
                                                "statusMemberID = {3}{0}" +
                                                "asUplineNo = {4}{0}" +
                                                "commPctPaid = {5}{0}" +
                                                "nominal = {6}{0}" +
                                                "commPctHold = {7}{0}" +
                                                "maxAmt = {8}{0}" +
                                                "minAmt = {9}{0}" +
                                                "isComplete = {10}{0}"
                                                , Environment.NewLine, groupComm.groupSchemaID, groupComm.commTypeID
                                                , groupComm.statusMemberID, groupComm.uplineNo, groupComm.commPctPaid, groupComm.nominal, 0, 0, 0, groupComm.isComplete);

                                _msGroupCommPctRepo.Update(updateGroupCommPct);
                                CurrentUnitOfWork.SaveChanges();

                                Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended update Group Comm Pct.");
                            }
                            catch (DataException ex)
                            {
                                Logger.ErrorFormat("CreateOrUpdateMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                                throw new UserFriendlyException("Db Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Logger.ErrorFormat("CreateOrUpdateMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                                throw new UserFriendlyException("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            var createMsGroupCommPct = new MS_GroupCommPct
                            {
                                entityID = 1,
                                groupSchemaID = groupComm.groupSchemaID,
                                validDate = groupComm.validDate,
                                commTypeID = groupComm.commTypeID,
                                statusMemberID = groupComm.statusMemberID,
                                asUplineNo = groupComm.uplineNo,
                                commPctPaid = groupComm.commPctPaid,
                                nominal = groupComm.nominal,
                                commPctHold = 0,
                                maxAmt = 0,
                                minAmt = 0,
                                isComplete = groupComm.isComplete

                            };

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start insert Group Comm Pct. Parameters sent:{0}" +
                                                "entityID = {1}{0}" +
                                                "groupSchemaID = {2}{0}" +
                                                "validDate = {3}{0}" +
                                                "commTypeID = {4}{0}" +
                                                "statusMemberID = {5}{0}" +
                                                "asUplineNo = {6}{0}" +
                                                "commPctPaid = {7}{0}" +
                                                "nominal = {8}{0}" +
                                                "commPctHold = {9}{0}" +
                                                "maxAmt = {10}{0}" +
                                                "minAmt = {11}{0}" +
                                                "isComplete = {12}{0}"
                                                , Environment.NewLine, 1, groupComm.groupSchemaID, groupComm.validDate, groupComm.commTypeID
                                                , groupComm.statusMemberID, groupComm.uplineNo, groupComm.commPctPaid, groupComm.nominal, 0, 0, 0, groupComm.isComplete);

                            _msGroupCommPctRepo.Insert(createMsGroupCommPct);

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended insert Group Comm Pct.");
                        }

                        updateGroupSchema.isStandard = true;
                        _msGroupSchemaRepo.Update(updateGroupSchema);
                    }
                    else
                    {
                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start checking before update Group Comm Pct Non STD. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}" +
                                            "isComplete = {2}{0}"
                                            , Environment.NewLine, groupComm.groupSchemaID, groupComm.isComplete);

                        var cek = (from A in _msGroupCommPctNonStdRepo.GetAll()
                                   where A.groupSchemaID == groupComm.groupSchemaID && A.isComplete == groupComm.isComplete
                                   select A).Any();

                        Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended checking before update Group Comm Pct Non STD.");

                        if (cek)
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start get data before update Group Comm Pct. Parameters sent:{0}" +
                                            "groupCommPctID = {1}{0}" +
                                            "isComplete = {1}{0}"
                                            , Environment.NewLine, groupComm.groupCommPctID, groupComm.isComplete);

                            var getNonStd = (from A in _msGroupCommPctNonStdRepo.GetAll()
                                             where A.Id == groupComm.groupCommPctID && A.isComplete == groupComm.isComplete
                                             select A).FirstOrDefault();

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended get data before update Group Comm Pct.");

                            var updateNonStd = getNonStd.MapTo<MS_GroupCommPctNonStd>();
                            updateNonStd.groupSchemaID = groupComm.groupSchemaID;
                            updateNonStd.validDate = groupComm.validDate;
                            updateNonStd.commTypeID = groupComm.commTypeID;
                            updateNonStd.statusMemberID = groupComm.statusMemberID;
                            updateNonStd.asUplineNo = groupComm.uplineNo;
                            updateNonStd.commPctPaid = groupComm.commPctPaid;
                            updateNonStd.nominal = groupComm.nominal;
                            updateNonStd.commPctHold = 0;
                            updateNonStd.maxAmt = 0;
                            updateNonStd.minAmt = 0;
                            updateNonStd.isComplete = groupComm.isComplete;

                            try
                            {
                                Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start update Group Comm Pct Non STD. Parameters sent:{0}" +
                                                "groupSchemaID = {1}{0}" +
                                                "commTypeID = {2}{0}" +
                                                "statusMemberID = {3}{0}" +
                                                "asUplineNo = {4}{0}" +
                                                "commPctPaid = {5}{0}" +
                                                "nominal = {6}{0}" +
                                                "commPctHold = {7}{0}" +
                                                "maxAmt = {8}{0}" +
                                                "minAmt = {9}{0}" +
                                                "isComplete = {10}{0}"
                                                , Environment.NewLine, groupComm.groupSchemaID, groupComm.commTypeID
                                                , groupComm.statusMemberID, groupComm.uplineNo, groupComm.commPctPaid, groupComm.nominal, 0, 0, 0, groupComm.isComplete);


                                _msGroupCommPctNonStdRepo.Update(updateNonStd);
                                CurrentUnitOfWork.SaveChanges();

                                Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended update Group Comm Pct.");
                            }
                            catch (DataException ex)
                            {
                                Logger.ErrorFormat("CreateOrUpdateMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                                throw new UserFriendlyException("Db Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Logger.ErrorFormat("CreateOrUpdateMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                                throw new UserFriendlyException("Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            var createMsGroupCommPctNonStd = new MS_GroupCommPctNonStd
                            {
                                entityID = 1,
                                groupSchemaID = groupComm.groupSchemaID,
                                validDate = groupComm.validDate,
                                commTypeID = groupComm.commTypeID,
                                statusMemberID = groupComm.statusMemberID,
                                asUplineNo = groupComm.uplineNo,
                                commPctPaid = groupComm.commPctPaid,
                                nominal = groupComm.nominal,
                                commPctHold = 0,
                                maxAmt = 0,
                                minAmt = 0,
                                isComplete = groupComm.isComplete

                            };

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Start insert Group Comm Pct Non STD. Parameters sent:{0}" +
                                               "entityID = {1}{0}" +
                                               "groupSchemaID = {2}{0}" +
                                               "validDate = {3}{0}" +
                                               "commTypeID = {4}{0}" +
                                               "statusMemberID = {5}{0}" +
                                               "asUplineNo = {6}{0}" +
                                               "commPctPaid = {7}{0}" +
                                               "nominal = {8}{0}" +
                                               "commPctHold = {9}{0}" +
                                               "maxAmt = {10}{0}" +
                                               "minAmt = {11}{0}" +
                                               "isComplete = {12}{0}"
                                               , Environment.NewLine, 1, groupComm.groupSchemaID, groupComm.validDate, groupComm.commTypeID
                                               , groupComm.statusMemberID, groupComm.uplineNo, groupComm.commPctPaid, groupComm.nominal, 0, 0, 0, groupComm.isComplete);

                            _msGroupCommPctNonStdRepo.Insert(createMsGroupCommPctNonStd);

                            Logger.DebugFormat("CreateOrUpdateMsGroupCommPct() - Ended insert Group Comm Pct Non STD.");
                        }

                        updateGroupSchema.isStandard = false;
                        _msGroupSchemaRepo.Update(updateGroupSchema);
                    }
                    Logger.Info("CreateOrUpdateMsGroupCommPct() - Update Finished.");
                }
            }
            Logger.Info("CreateOrUpdateMsGroupCommPct() - Finished.");
        }

        public List<GetDropDownSchemaByGroupSchemaIdListDto> GetDropDownSchemaByGroupSchemaId(List<int> groupSchemaID, bool isComplete) //passed
        {
            var getDropDown = (from A in _msGroupSchemaRepo.GetAll()
                               join B in _msSchemaRepo.GetAll() on A.schemaID equals B.Id
                               where groupSchemaID.Contains(A.Id) && A.isComplete == isComplete
                               select new GetDropDownSchemaByGroupSchemaIdListDto
                               {
                                   groupSchemaID = A.Id,
                                   schemaID = B.Id,
                                   schemaCode = B.scmCode,
                                   schemaName = B.scmName
                               }).ToList();

            return getDropDown;
        }

        public GetDetailMsGroupSchemaListDto GetDetailMsGroupSchema(string groupSchemaCode, bool isComplete) //passed
        {
            GetDetailMsGroupSchemaListDto listData = new GetDetailMsGroupSchemaListDto();
            try
            {
                var getDataGroupSchema = (from A in _context.MS_GroupSchema
                                          where A.groupSchemaCode == groupSchemaCode && A.isComplete == isComplete
                                          select new GetDetailMsGroupSchemaListDto
                                          {
                                              projectID = A.projectID,
                                              clusterID = A.clusterID,
                                              groupSchemaCode = A.groupSchemaCode,
                                              groupSchemaName = A.groupSchemaName,
                                              validFrom = A.validFrom,
                                              documentGrouping = (A != null && A.documentGrouping != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.documentGrouping) : null, //TODO link + ip host
                                              isActive = A.isActive
                                          }).ToList();

                var getDataUniversal = (from A in getDataGroupSchema
                                        join B in _contextProp.MS_Project on A.projectID equals B.Id
                                        join C in _contextProp.MS_Cluster on A.clusterID equals C.Id
                                        select new GetDetailMsGroupSchemaListDto
                                        {
                                            projectID = A.projectID,
                                            projectName = B.projectName,
                                            clusterID = A.clusterID,
                                            clusterName = C.clusterName,
                                            groupSchemaCode = A.groupSchemaCode,
                                            groupSchemaName = A.groupSchemaName,
                                            validFrom = A.validFrom,
                                            documentGrouping = (A != null && A.documentGrouping != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(A.documentGrouping) : null, //TODO link + ip host
                                            isActive = A.isActive
                                        }).FirstOrDefault();

                var getDataSchema = (from A in _context.MS_GroupSchema
                                     join B in _context.MS_Schema on A.schemaID equals B.Id
                                     where A.groupSchemaCode == groupSchemaCode && A.isComplete == isComplete
                                     select new getDataSchema
                                     {
                                         groupSchemaID = A.Id,
                                         schemaID = A.schemaID,
                                         scmCode = B.scmCode,
                                         scmName = B.scmName
                                     }).ToList();

                if (getDataGroupSchema.Count == 0) return listData;

                listData = new GetDetailMsGroupSchemaListDto()
                {
                    projectID = getDataUniversal.projectID,
                    projectName = getDataUniversal.projectName,
                    clusterID = getDataUniversal.clusterID,
                    clusterName = getDataUniversal.clusterName,
                    groupSchemaCode = getDataUniversal.groupSchemaCode,
                    groupSchemaName = getDataUniversal.groupSchemaName,
                    validFrom = getDataUniversal.validFrom,
                    documentGrouping = getDataUniversal.documentGrouping,
                    isActive = getDataUniversal.isActive,
                    getDataSchema = getDataSchema
                };
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return listData;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Create)]
        public void CreateOrUpdateMsGroupRequirement(CreateMsGroupSchemaRequirementInputDto input)
        {
            Logger.Info("CreateOrUpdateMsGroupRequirement() - Started.");

            if (input.flag == "add")
            {
                foreach (var items in input.setGroupReq)
                {

                    var createMsGroupReq = new MS_GroupSchemaRequirement
                    {
                        entityID = 1,
                        groupSchemaID = items.groupSchemaID,
                        reqNo = items.reqNo,
                        reqDesc = items.reqDesc,
                        pctPaid = items.pctPaid,
                        orPctPaid = items.pctPaid,
                        isComplete = items.isComplete
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Start insert Group Schema Req. Parameters sent:{0}" +
                                            "entityID = {1}{0}" +
                                            "groupSchemaID = {2}{0}" +
                                            "reqNo = {3}{0}" +
                                            "reqDesc = {4}{0}" +
                                            "pctPaid = {5}{0}" +
                                            "orPctPaid = {6}{0}" +
                                            "isComplete = {7}{0}"
                                            , Environment.NewLine, 1, items.groupSchemaID, items.reqNo, items.reqDesc
                                            , items.pctPaid, items.pctPaid, items.isComplete);

                        _msGrpSchemaReqRepo.Insert(createMsGroupReq);

                        Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Ended insert Group Schema Req.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsGroupRequirement() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsGroupRequirement() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
            }

            else if (input.flag == "edit")
            {
                foreach (var items in input.setGroupReq)
                {

                    Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Start checking before update Group Schema Req. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}" +
                                            "isComplete = {2}{0}" +
                                            "reqNo = {3}{0}"
                                            , Environment.NewLine, items.groupSchemaID, true, items.reqNo);

                    var checkGroupSchemaReq = (from A in _msGrpSchemaReqRepo.GetAll()
                                               where items.groupSchemaID == A.groupSchemaID && items.reqNo == A.reqNo && A.isComplete == true
                                               select A).Any();

                    Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Ended checking before update Group Schema Req. Result = {0}", checkGroupSchemaReq);

                    //update
                    if (checkGroupSchemaReq)
                    {
                        Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Start get data before update Group Schema Req. Parameters sent:{0}" +
                                            "groupSchemaID = {1}{0}" +
                                            "isComplete = {2}{0}" +
                                            "reqNo = {3}{0}"
                                            , Environment.NewLine, items.groupSchemaID, true, items.reqNo);

                        var getGroupSchemaReq = (from A in _msGrpSchemaReqRepo.GetAll()
                                                 where items.groupSchemaID == A.groupSchemaID && items.reqNo == A.reqNo && A.isComplete == true
                                                 select A).FirstOrDefault();

                        Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Ended get data before update Group Schema Req. Result = {0}", getGroupSchemaReq);

                        var update = getGroupSchemaReq.MapTo<MS_GroupSchemaRequirement>();

                        update.pctPaid = items.pctPaid;
                        update.orPctPaid = items.pctPaid;
                        update.reqDesc = items.reqDesc;

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Start update Group Schema Req. Parameters sent:{0}" +
                                            "reqDesc = {1}{0}" +
                                            "pctPaid = {2}{0}" +
                                            "orPctPaid = {3}{0}"
                                            , Environment.NewLine, items.reqDesc
                                            , items.pctPaid, items.pctPaid);

                            _msGrpSchemaReqRepo.Update(update);

                            Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Ended update Group Schema Req.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupRequirement() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupRequirement() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    //insert
                    else
                    {
                        var createMsGroupReq = new MS_GroupSchemaRequirement
                        {
                            entityID = 1,
                            groupSchemaID = items.groupSchemaID,
                            reqNo = items.reqNo,
                            reqDesc = items.reqDesc,
                            pctPaid = items.pctPaid,
                            orPctPaid = items.pctPaid,
                            isComplete = items.isComplete
                        };

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Start insert Group Schema Req. Parameters sent:{0}" +
                                            "entityID = {1}{0}" +
                                            "groupSchemaID = {2}{0}" +
                                            "reqNo = {3}{0}" +
                                            "reqDesc = {4}{0}" +
                                            "pctPaid = {5}{0}" +
                                            "orPctPaid = {6}{0}" +
                                            "isComplete = {7}{0}"
                                            , Environment.NewLine, 1, items.groupSchemaID, items.reqNo, items.reqDesc
                                            , items.pctPaid, items.pctPaid, items.isComplete);

                            _msGrpSchemaReqRepo.Insert(createMsGroupReq);

                            Logger.DebugFormat("CreateOrUpdateMsGroupRequirement() - Ended insert Group Schema Req.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupRequirement() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateMsGroupRequirement() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }

            }

        }

        public void UpdateIsComplete(List<int> groupSchemas)
        {
            foreach (var groupSchemaID in groupSchemas)
            {
                //GroupSchema
                var getGroupSchema = (from groupSchema in _msGroupSchemaRepo.GetAll()
                                      where groupSchema.Id == groupSchemaID
                                      select groupSchema).FirstOrDefault();
                var update = getGroupSchema.MapTo<MS_GroupSchema>();
                update.isComplete = true;
                _msGroupSchemaRepo.Update(update);

                if (update.isStandard)
                {
                    //GroupCommPct
                    var getGroupCommPct = (from groupCommPct in _msGroupCommPctRepo.GetAll()
                                           where groupCommPct.groupSchemaID == groupSchemaID
                                           select groupCommPct).ToList();

                    foreach (var item in getGroupCommPct)
                    {
                        var updateGrpCommPct = item.MapTo<MS_GroupCommPct>();

                        updateGrpCommPct.isComplete = true;
                        _msGroupCommPctRepo.Update(updateGrpCommPct);
                    }
                }
                else
                {
                    //GroupCommPctNonStd
                    var getGroupCommPctNonStd = (from groupCommPctNonStd in _msGroupCommPctNonStdRepo.GetAll()
                                                 where groupCommPctNonStd.groupSchemaID == groupSchemaID
                                                 select groupCommPctNonStd).ToList();

                    foreach (var item in getGroupCommPctNonStd)
                    {
                        var updateGrpCommPctNonStd = item.MapTo<MS_GroupCommPctNonStd>();

                        updateGrpCommPctNonStd.isComplete = true;
                        _msGroupCommPctNonStdRepo.Update(updateGrpCommPctNonStd);
                    }
                }

                //Group Schema Req
                var getGrpSchemaRequirement = (from grpSchemaReq in _msGrpSchemaReqRepo.GetAll()
                                               where grpSchemaReq.groupSchemaID == groupSchemaID
                                               select grpSchemaReq).ToList();

                foreach (var item in getGrpSchemaRequirement)
                {
                    var updateReq = item.MapTo<MS_GroupSchemaRequirement>();

                    updateReq.isComplete = true;
                    _msGrpSchemaReqRepo.Update(updateReq);
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Delete)]
        public void DeleteMsGroupSchema(string groupSchemaCode)
        {
            var dataGroupSchema = (from x in _msGroupSchemaRepo.GetAll()
                                   where x.groupSchemaCode == groupSchemaCode && x.isComplete == true
                                   select x).ToList();

            foreach (var item in dataGroupSchema)
            {
                DeleteSchemaByGroupSchema(item.Id);
            }

        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Delete)]
        public void DeleteMsGroupSchemaReq(DeleteMsGroupSchemaRequirementInputDto input)
        {
            Logger.Info("DeleteMsGroupSchemaReq() - Started.");

            foreach (var item in input.groupSchemaID)
            {

                Logger.DebugFormat("DeleteMsGroupSchemaReq() - Start get data before delete GROUP SCHEMA REQ. Parameters sent:{0}" +
                            "groupSchemaID = {1}{0}" +
                            "reqNo = {2}{0}" +
                            "isComplete = {3}{0}"
                            , Environment.NewLine, item, input.reqNo, true);

                var getGroupSchemaReq = (from groupSchemaReq in _msGrpSchemaReqRepo.GetAll()
                                         where item == groupSchemaReq.groupSchemaID && input.reqNo == groupSchemaReq.reqNo && groupSchemaReq.isComplete == true
                                         select groupSchemaReq).FirstOrDefault();

                Logger.DebugFormat("DeleteMsGroupSchemaReq() - Ended get data before delete GROUP SCHEMA REQ. Result = {0}", getGroupSchemaReq);

                var updateGroupSchemaReq = getGroupSchemaReq.MapTo<MS_GroupSchemaRequirement>();

                updateGroupSchemaReq.isComplete = false;

                try
                {
                    Logger.DebugFormat("DeleteMsGroupSchemaReq() - Start delete GROUP SCHEMA REQ. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                    _msGrpSchemaReqRepo.Update(updateGroupSchemaReq);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("DeleteMsGroupSchemaReq() - Ended delete GROUP SCHEMA REQ.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsGroupSchemaReq() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsGroupSchemaReq() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("DeleteMsGroupSchemaReq() - Finished.");
        }

        private void DeleteMsGroupSchemaReqChild(int Id)
        {
            Logger.Info("DeleteMsGroupSchemaReqChild() - Started.");

            Logger.DebugFormat("DeleteMsGroupSchemaReqChild() - Start get data before delete GROUP SCHEMA REQ Child. Parameters sent:{0}" +
                            "groupSchemaReqID = {1}{0}"
                            , Environment.NewLine, Id);

            var getGroupSchemaReq = (from groupSchemaReq in _msGrpSchemaReqRepo.GetAll()
                                     where Id == groupSchemaReq.Id
                                     select groupSchemaReq).FirstOrDefault();

            Logger.DebugFormat("DeleteMsGroupSchemaReqChild() - Ended get data before delete GROUP SCHEMA REQ Child. Result = {0}", getGroupSchemaReq);

            var updateGroupSchemaReq = getGroupSchemaReq.MapTo<MS_GroupSchemaRequirement>();

            updateGroupSchemaReq.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsGroupSchemaReqChild() - Start delete GROUP SCHEMA REQ Child. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                _msGrpSchemaReqRepo.Update(updateGroupSchemaReq);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("DeleteMsGroupSchemaReqChild() - Ended delete GROUP SCHEMA REQ Child.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteMsGroupSchemaReqChild() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteMsGroupSchemaReqChild() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("DeleteMsGroupSchemaReqChild() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Delete)]
        private void DeleteMsGroupCommPctChild(int Id)
        {
            Logger.Info("DeleteMsGroupCommPctChild() - Started.");

            Logger.DebugFormat("DeleteMsGroupCommPctChild() - Start get data before delete GROUP Comm Pct Child. Parameters sent:{0}" +
                            "groupCommPctID = {1}{0}"
                            , Environment.NewLine, Id);

            var getGroupCommPct = (from groupCommPct in _msGroupCommPctRepo.GetAll()
                                   where Id == groupCommPct.Id
                                   select groupCommPct).FirstOrDefault();

            Logger.DebugFormat("DeleteMsGroupCommPctChild() - Ended get data before delete GROUP Comm Pct Child");

            var updateGroupCommPct = getGroupCommPct.MapTo<MS_GroupCommPct>();

            updateGroupCommPct.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsGroupCommPctChild() - Start delete GROUP Comm Pct Child. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                _msGroupCommPctRepo.Update(updateGroupCommPct);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("DeleteMsGroupCommPctChild() - Ended delete GROUP Comm Pct Child.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteMsGroupCommPctChild() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteMsGroupCommPctChild() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("DeleteMsGroupCommPctChild() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Delete)]
        private void DeleteMsGroupCommPctNonStdChild(int Id)
        {
            Logger.Info("DeleteMsGroupCommPctNonStdChild() - Started.");

            Logger.DebugFormat("DeleteMsGroupCommPctNonStdChild() - Start get data before delete GROUP Comm Pct Non STD Child. Parameters sent:{0}" +
                            "groupCommPctNonStdID = {1}{0}"
                            , Environment.NewLine, Id);

            var getGroupCommPctNonStd = (from x in _msGroupCommPctNonStdRepo.GetAll()
                                         where Id == x.Id
                                         select x).FirstOrDefault();

            Logger.DebugFormat("DeleteMsGroupCommPctNonStdChild() - Ended get data before delete GROUP Comm Pct Non STD Child");

            var update = getGroupCommPctNonStd.MapTo<MS_GroupCommPctNonStd>();

            update.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteMsGroupCommPctNonStdChild() - Start delete GROUP Comm Pct Non STD Child. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                _msGroupCommPctNonStdRepo.Update(update);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("DeleteMsGroupCommPctNonStdChild() - Ended delete GROUP Comm Pct Non STD Child.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteMsGroupCommPctNonStdChild() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteMsGroupCommPctNonStdChild() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("DeleteMsGroupCommPctNonStdChild() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchemaPerProject_Delete)]
        public void DeleteSchemaByGroupSchema(int Id)
        {
            var dataGrpScmReq = (from x in _msGrpSchemaReqRepo.GetAll()
                                 where x.groupSchemaID == Id
                                 select x).ToList();

            foreach (var itemReq in dataGrpScmReq)
            {
                DeleteMsGroupSchemaReqChild(itemReq.Id);
            }

            var dataGrpCommPct = (from x in _msGroupCommPctRepo.GetAll()
                                  where x.groupSchemaID == Id
                                  select x).ToList();

            foreach (var itemCommPct in dataGrpCommPct)
            {
                DeleteMsGroupCommPctChild(itemCommPct.Id);
            }

            var dataGrpCommPctNonStd = (from x in _msGroupCommPctNonStdRepo.GetAll()
                                        where x.groupSchemaID == Id
                                        select x).ToList();

            foreach (var itemCommPct in dataGrpCommPctNonStd)
            {
                DeleteMsGroupCommPctNonStdChild(itemCommPct.Id);
            }

            var getGroupSchema = (from groupSchema in _msGroupSchemaRepo.GetAll()
                                  where Id == groupSchema.Id
                                  select groupSchema).FirstOrDefault();

            var updateGroupSchema = getGroupSchema.MapTo<MS_GroupSchema>();

            updateGroupSchema.isComplete = false;

            try
            {
                Logger.DebugFormat("DeleteSchemaByGroupSchema() - Start delete GROUP Schema. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                _msGroupSchemaRepo.Update(updateGroupSchema);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("DeleteSchemaByGroupSchema() - Ended delete GROUP Schema.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("DeleteSchemaByGroupSchema() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("DeleteSchemaByGroupSchema() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("DeleteSchemaByGroupSchema() - Finished.");
        }

        public List<GetMsGroupSchemaRequirementListDto> GetMsGroupRequirement(int groupSchemaID, bool isComplete) //passed
        {
            var getDataUniversal = (from A in _msGrpSchemaReqRepo.GetAll()
                                    where A.groupSchemaID == groupSchemaID && A.isComplete == isComplete
                                    orderby A.reqNo
                                    select new GetMsGroupSchemaRequirementListDto
                                    {
                                        groupSchemaID = A.groupSchemaID,
                                        groupSchemaRequirementID = A.Id,
                                        reqNo = A.reqNo,
                                        reqDesc = A.reqDesc,
                                        pctPaid = A.pctPaid
                                    }).ToList();

            return getDataUniversal;
        }

        public List<GetAllMsGroupCommPctListDto> GetAllMsGroupCommPct(List<ReturnMsGroupSchemaDto> input, string flag)
        {
            var listResult = new List<GetAllMsGroupCommPctListDto>();
            var listGroupScmID = (from A in input select A.groupSchemaID).ToList();
            var listScmID = (from A in input select A.schemaID).ToList();

            if (flag == "group")
            {
                var exstGroupSchema = (from B in _msGroupCommPctRepo.GetAll()
                                       where listGroupScmID.Contains(B.groupSchemaID)
                                       select B.groupSchemaID).ToList();

                if (exstGroupSchema.Any())
                {
                    var groupCommPct = (from B in _msGroupCommPctRepo.GetAll()
                                        where B.groupSchemaID == exstGroupSchema.FirstOrDefault()
                                        join C in _lkCommTypeRepo.GetAll() on B.commTypeID equals C.Id
                                        join D in _msStatusMemberRepo.GetAll() on B.statusMemberID equals D.Id
                                        join E in _msGroupSchemaRepo.GetAll() on B.groupSchemaID equals E.Id
                                        join F in _msSchemaRepo.GetAll() on E.schemaID equals F.Id
                                        select new GetAllMsGroupCommPctListDto
                                        {
                                            groupSchemaID = B.groupSchemaID,
                                            schemaID = E.schemaID,
                                            scmCode = F.scmCode,
                                            validDate = B.validDate,
                                            commTypeID = B.commTypeID,
                                            commTypeName = C.commTypeName,
                                            statusMemberID = B.statusMemberID,
                                            statusName = D.statusName,
                                            uplineNo = B.asUplineNo,
                                            commPctPaid = B.commPctPaid,
                                            nominal = B.nominal,
                                            isStandard = E.isStandard,
                                            groupCommPctID = new List<int> { 0 }
                                        }).ToList();

                    foreach (var itemGroupCommPct in groupCommPct)
                    {
                        var listGroupCommPctID = (from A in _msGroupCommPctRepo.GetAll()
                                                  where exstGroupSchema.Contains(A.groupSchemaID)
                                                  select A.Id).ToList();
                        itemGroupCommPct.groupCommPctID = listGroupCommPctID;
                    }

                    listResult.AddRange(groupCommPct);
                }
                else
                {
                    var commPct = (from B in _msCommPctRepo.GetAll()
                                   where B.isComplete == true && B.schemaID == listScmID.FirstOrDefault()
                                   join C in _lkCommTypeRepo.GetAll() on B.commTypeID equals C.Id
                                   join D in _msStatusMemberRepo.GetAll() on B.statusMemberID equals D.Id
                                   join E in _msSchemaRepo.GetAll() on B.schemaID equals E.Id
                                   select new GetAllMsGroupCommPctListDto
                                   {
                                       validDate = B.validDate,
                                       schemaID = B.schemaID,
                                       scmCode = E.scmCode,
                                       commTypeID = B.commTypeID,
                                       commTypeName = C.commTypeName,
                                       statusMemberID = B.statusMemberID,
                                       statusName = D.statusName,
                                       uplineNo = B.asUplineNo,
                                       commPctPaid = B.commPctPaid,
                                       nominal = B.nominal,
                                       isStandard = true,
                                       groupCommPctID = new List<int> { 0 }
                                   }).ToList();

                    listResult.AddRange(commPct);
                }
            }
            else
            {
                foreach (var item in input)
                {
                    var cek = (from B in _msGroupCommPctNonStdRepo.GetAll()
                               where B.groupSchemaID == item.groupSchemaID
                               select B).Any();
                    if (cek)
                    {
                        var groupCommPctNonSTD = (from B in _msGroupCommPctNonStdRepo.GetAll()
                                                  where B.groupSchemaID == item.groupSchemaID
                                                  join C in _lkCommTypeRepo.GetAll() on B.commTypeID equals C.Id
                                                  join D in _msStatusMemberRepo.GetAll() on B.statusMemberID equals D.Id
                                                  join E in _msGroupSchemaRepo.GetAll() on B.groupSchemaID equals E.Id
                                                  join F in _msSchemaRepo.GetAll() on E.schemaID equals F.Id
                                                  orderby E.schemaID
                                                  select new GetAllMsGroupCommPctListDto
                                                  {
                                                      groupSchemaID = B.groupSchemaID,
                                                      schemaID = E.schemaID,
                                                      scmCode = F.scmCode,
                                                      validDate = B.validDate,
                                                      commTypeID = B.commTypeID,
                                                      commTypeName = C.commTypeName,
                                                      statusMemberID = B.statusMemberID,
                                                      statusName = D.statusName,
                                                      uplineNo = B.asUplineNo,
                                                      commPctPaid = B.commPctPaid,
                                                      nominal = B.nominal,
                                                      isStandard = E.isStandard,
                                                      groupCommPctID = new List<int> { B.Id }
                                                  }).ToList();

                        listResult.AddRange(groupCommPctNonSTD);
                    }
                    else
                    {
                        var commPct = (from B in _msCommPctRepo.GetAll()
                                       where B.isComplete == true && B.schemaID == item.schemaID
                                       join C in _lkCommTypeRepo.GetAll() on B.commTypeID equals C.Id
                                       join D in _msStatusMemberRepo.GetAll() on B.statusMemberID equals D.Id
                                       join E in _msSchemaRepo.GetAll() on B.schemaID equals E.Id
                                       orderby B.schemaID
                                       select new GetAllMsGroupCommPctListDto
                                       {
                                           validDate = B.validDate,
                                           schemaID = B.schemaID,
                                           scmCode = E.scmCode,
                                           commTypeID = B.commTypeID,
                                           commTypeName = C.commTypeName,
                                           statusMemberID = B.statusMemberID,
                                           statusName = D.statusName,
                                           uplineNo = B.asUplineNo,
                                           commPctPaid = B.commPctPaid,
                                           nominal = B.nominal,
                                           isStandard = false,
                                           groupCommPctID = new List<int> { 0 }
                                       }).ToList();

                        listResult.AddRange(commPct);
                    }
                }
            }
            return listResult;
        }

        public void BackToLatest(string groupSchemaCode)
        {
            Logger.Info("BackToLatest() - Started.");
            Logger.DebugFormat("BackToLatest() - Start get data before Back To Latest. Parameters sent:{0}" +
                        "groupSchemaCode = {1}{0}" +
                        "isComplete = {2}{0}"
                        , Environment.NewLine, groupSchemaCode, true);

            var getGroupSchema = (from groupSchema in _msGroupSchemaRepo.GetAll()
                                  where groupSchema.groupSchemaCode == groupSchemaCode && groupSchema.isComplete == true
                                  select groupSchema).ToList();

            Logger.DebugFormat("BackToLatest() - Ended get data before Back To Latest.");

            foreach (var item in getGroupSchema)
            {
                var updateGroupSchema = item.MapTo<MS_GroupSchema>();

                updateGroupSchema.isStandard = true;

                try
                {
                    Logger.DebugFormat("BackToLatest() - Start Back To Latest. Parameters sent:{0}" +
                             "isStandard = {1}{0}"
                            , Environment.NewLine, true);

                    _msGroupSchemaRepo.Update(updateGroupSchema);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("BackToLatest() - Ended Back To Latest.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("BackToLatest() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("BackToLatest() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }


            }
            Logger.Info("BackToLatest() - Finished.");
        }

        public List<GetDropDownClusterByProjectListDto> GetDropDownClusterByProject(int projectID) //passed
        {
            var getCluster = (from x in _msUnitRepo.GetAll()
                              join y in _msClusterRepo.GetAll() on x.clusterID equals y.Id
                              where x.projectID == projectID
                              group x by new { x.clusterID, y.clusterCode, y.clusterName } into grp
                              orderby grp.Key.clusterID
                              select new GetDropDownClusterByProjectListDto
                              {
                                  clusterID = grp.Key.clusterID,
                                  clusterCode = grp.Key.clusterCode,
                                  clusterName = grp.Key.clusterName
                              }).Distinct().ToList();

            return getCluster;
        }

        public void DeleteMsGroupCommPct(List<int> Id, bool isStandard, string flag)
        {
            if (flag == "add")
            {
                foreach (var item in Id)
                {
                    if (isStandard)
                    {
                        try
                        {
                            Logger.DebugFormat("DeleteMsGroupCommPct() - Start delete GROUP Comm Pct. Parameters sent:{0}" +
                                             "groupCommPctID = {1}{0}"
                                            , Environment.NewLine, item);

                            _msGroupCommPctRepo.Delete(item);
                            CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                            Logger.DebugFormat("DeleteMsGroupCommPct() - Ended delete GROUP Comm Pct.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            Logger.DebugFormat("DeleteMsGroupCommPct() - Start delete GROUP Comm Pct Non STD. Parameters sent:{0}" +
                                             "groupCommPctNonStdID = {1}{0}"
                                            , Environment.NewLine, item);

                            _msGroupCommPctNonStdRepo.Delete(item);
                            CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                            Logger.DebugFormat("DeleteMsGroupCommPct() - Ended delete GROUP Comm Pct Non STD.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                if (isStandard)
                {
                    Logger.DebugFormat("DeleteMsGroupCommPct() - Start get data before delete GROUP Comm Pct. Parameters sent:{0}" +
                             "groupCommPctID = {1}{0}"
                            , Environment.NewLine, Id);

                    var getGroupCommPct = (from groupCommPct in _msGroupCommPctRepo.GetAll()
                                           where Id.Contains(groupCommPct.Id)
                                           select groupCommPct).FirstOrDefault();

                    Logger.DebugFormat("DeleteMsGroupCommPct() - Ended get data before delete GROUP Comm Pct.");

                    var updateGroupCommPct = getGroupCommPct.MapTo<MS_GroupCommPct>();

                    updateGroupCommPct.isComplete = false;

                    try
                    {
                        Logger.DebugFormat("DeleteMsGroupCommPct() - Start delete GROUP Comm Pct. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                        _msGroupCommPctRepo.Update(updateGroupCommPct);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("DeleteMsGroupCommPct() - Ended delete GROUP Comm Pct.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.DebugFormat("DeleteMsGroupCommPct() - Start get data before delete GROUP Comm Pct Non STD. Parameters sent:{0}" +
                             "groupCommPctNonStdID = {1}{0}"
                            , Environment.NewLine, Id);

                    var getGroupCommPct = (from x in _msGroupCommPctNonStdRepo.GetAll()
                                           where Id.Contains(x.Id)
                                           select x).FirstOrDefault();

                    Logger.DebugFormat("DeleteMsGroupCommPct() - Ended get data before delete GROUP Comm Pct Non STD.");

                    var update = getGroupCommPct.MapTo<MS_GroupCommPctNonStd>();

                    update.isComplete = false;

                    try
                    {
                        Logger.DebugFormat("DeleteMsGroupCommPct() - Start delete GROUP Comm Pct Non STD. Parameters sent:{0}" +
                             "isComplete = {1}{0}"
                            , Environment.NewLine, false);

                        _msGroupCommPctNonStdRepo.Update(update);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("DeleteMsGroupCommPct() - Ended delete GROUP Comm Pct Non STD.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("DeleteMsGroupCommPct() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
            }
            Logger.Info("DeleteMsGroupCommPct() - Finished.");
        }

        public List<GetDataMsGroupSchemaReqListDto> GetDataMsGroupSchemaReq(int schemaID, int projectID)
        {
            var reqruitment = new List<GetDataMsGroupSchemaReqListDto>();

            var cekGroup = (from groupSchema in _msGroupSchemaRepo.GetAll()
                            where groupSchema.schemaID == schemaID && groupSchema.projectID == projectID && groupSchema.isActive == true && groupSchema.isComplete == true
                            select groupSchema).FirstOrDefault();

            if (cekGroup != null)
            {
                reqruitment = (from groupSchemaReq in _msGrpSchemaReqRepo.GetAll()
                               where groupSchemaReq.groupSchemaID == cekGroup.Id && groupSchemaReq.isComplete == true
                               select new GetDataMsGroupSchemaReqListDto
                               {
                                   reqNo = groupSchemaReq.reqNo,
                                   reqDesc = groupSchemaReq.reqDesc,
                                   pctPaid = groupSchemaReq.pctPaid
                               }).ToList();
            }
            else
            {
                reqruitment = (from schemaReq in _msSchemaRequirementRepo.GetAll()
                               where schemaReq.schemaID == schemaID && schemaReq.isComplete == true
                               select new GetDataMsGroupSchemaReqListDto
                               {
                                   reqNo = schemaReq.reqNo,
                                   reqDesc = schemaReq.reqDesc,
                                   pctPaid = schemaReq.pctPaid
                               }).ToList();
            }

            return reqruitment;
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
            var request = _HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");
            int position = result.LastIndexOf('/');
            if (position > -1)
                result = result.Substring(0, result.Length - 1);
            return result;
        }

        #region debug console
        public void SendConsole(string msg)
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
    }
}
