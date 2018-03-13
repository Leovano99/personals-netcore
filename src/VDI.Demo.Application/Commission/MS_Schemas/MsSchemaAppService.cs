using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.Authorization;
using VDI.Demo.Commission.MS_Schemas.Dto;
using VDI.Demo.NewCommDB;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.UI;
using System.Data.Common;
using Abp.AutoMapper;
using System.Data;
using Microsoft.AspNetCore.Http;
using VDI.Demo.Files;
using Abp.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace VDI.Demo.Commission.MS_Schemas
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema)]
    public class MsSchemaAppService : DemoAppServiceBase, IMsSchemaAppService
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
        private readonly IRepository<TR_CommPayment> _trCommPaymentRepo;
        private readonly IRepository<TR_CommPaymentPph> _trCommPaymentPphRepo;
        private readonly IRepository<TR_ManagementFee> _trManagementFeeRepo;
        private readonly IRepository<TR_SoldUnit> _trSoldUnitRepo;
        private readonly IRepository<TR_SoldUnitRequirement> _trSoldUnitReqRepo;
        private static IHttpContextAccessor _HttpContextAccessor;
        private readonly FilesHelper _filesHelper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MsSchemaAppService(
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
            IRepository<TR_CommPayment> trCommPaymentRepo,
            IRepository<TR_CommPaymentPph> trCommPaymentPphRepo,
            IRepository<TR_ManagementFee> trManagementFeeRepo,
            IRepository<TR_SoldUnit> trSoldUnitRepo,
            IRepository<TR_SoldUnitRequirement> trSoldUnitReqRepo,
            IHttpContextAccessor httpContextAccessor,
            FilesHelper filesHelper,
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
            _trCommPaymentRepo = trCommPaymentRepo;
            _trCommPaymentPphRepo = trCommPaymentPphRepo;
            _trManagementFeeRepo = trManagementFeeRepo;
            _trSoldUnitRepo = trSoldUnitRepo;
            _trSoldUnitReqRepo = trSoldUnitReqRepo;
            _HttpContextAccessor = httpContextAccessor;
            _filesHelper = filesHelper;
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
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\SchemaFile\", @"Assets\Upload\SchemaFile\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        public async Task<PagedResultDto<GetAllMsSchemaListDto>> GetAllMsSchema(GetMsSchemaListInput input)
        {
            var getSchema = (from x in _msSchemaRepo.GetAll()
                             where x.isComplete == true
                             select new GetAllMsSchemaListDto
                             {
                                 schemaID = x.Id,
                                 scmCode = x.scmCode,
                                 scmName = x.scmName,
                                 dueDateComm = x.dueDateComm,
                                 isActive = x.isActive
                             })
                             .WhereIf(
                                !input.Filter.IsNullOrWhiteSpace(),
                                u =>
                                    u.scmCode.Contains(input.Filter) ||
                                    u.scmName.Contains(input.Filter) ||
                                    u.dueDateComm.Equals(input.Filter)
                            );

            var dataCount = await getSchema.AsQueryable().CountAsync();

            var resultList = await getSchema.AsQueryable()
                .OrderByDescending(s => s.schemaID)
                .PageBy(input)
                .ToListAsync();

            var listDtos = resultList;

            return new PagedResultDto<GetAllMsSchemaListDto>(
                dataCount,
                listDtos);
        }

        public List<GetMsSchemaRequirementListDto> GetMsSchemaRequirementBySchemaID(int schemaID, bool isComplete)
        {
            var listResult = (from x in _msSchemaRequirementRepo.GetAll()
                              where x.schemaID == schemaID && x.isComplete == isComplete
                              orderby x.reqNo
                              select new GetMsSchemaRequirementListDto
                              {
                                  schemaRequirementID = x.Id,
                                  reqNo = x.reqNo,
                                  reqDesc = x.reqDesc,
                                  pctPaid = x.pctPaid
                              }).ToList();

            return listResult;
        }

        public List<GetLkCommTypeListDto> GetLkCommTypeBySchemaID(int schemaID, bool isComplete)
        {
            var listResult = (from x in _lkCommTypeRepo.GetAll()
                              where x.schemaID == schemaID && x.isComplete == isComplete
                              orderby x.Id descending
                              select new GetLkCommTypeListDto
                              {
                                  commTypeID = x.Id,
                                  commTypeCode = x.commTypeCode,
                                  commTypeName = x.commTypeName
                              }).ToList();

            return listResult;
        }

        public List<GetMsStatusMemberListDto> GetMsStatusMemberBySchemaID(int schemaID, bool isComplete)
        {
            var listResult = (from x in _msStatusMemberRepo.GetAll()
                              where x.schemaID == schemaID && x.isComplete == isComplete
                              orderby x.Id descending
                              select new GetMsStatusMemberListDto
                              {
                                  statusMemberID = x.Id,
                                  statusCode = x.statusCode,
                                  statusName = x.statusName,
                                  pointMin = x.pointMin,
                                  reviewTimeYear = x.reviewTimeYear,
                                  reviewStartMonth = x.reviewStartMonth,
                                  pointToKeepStatus = x.pointToKeepStatus,
                                  statusStar = x.statusStar
                              }).ToList();

            return listResult;
        }

        public List<GetLkPointTypeListDto> GetLkPointTypeBySchemaID(int schemaID, bool isComplete)
        {
            var listResult = (from x in _lkPointTypeRepo.GetAll()
                              where x.schemaID == schemaID && x.isComplete == isComplete
                              orderby x.Id descending
                              select new GetLkPointTypeListDto
                              {
                                  pointTypeID = x.Id,
                                  pointTypeCode = x.pointTypeCode,
                                  pointTypeName = x.pointTypeName
                              }).ToList();

            return listResult;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Create)]
        public void CreateOrUpdateMsSchemaRequirement(CreateMsSchemaRequirementInputDto input)
        {
            Logger.Info("CreateOrUpdateMsSchemaRequirement() Started.");

            foreach (var item in input.setCommReq)
            {
                if (item.schemaRequirementID == 0 || item.schemaRequirementID == null)
                {
                    var createMsSchemaRequirement = new MS_SchemaRequirement
                    {
                        reqNo = item.reqNo,
                        reqDesc = item.reqDesc,
                        pctPaid = item.pctPaid,
                        orPctPaid = item.pctPaid,
                        schemaID = input.schemaID,
                        isComplete = item.isComplete,
                        entityID = 1

                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsSchemaRequirement() - Start insert Schema Requirement. Parameters sent:{0}" +
                            "reqNo      = {1}{0}" +
                            "reqDesc    = {2}{0}" +
                            "pctPaid    = {3}{0}" +
                            "orPctPaid  = {4}{0}" +
                            "schemaID   = {5}{0}" +
                            "isComplete = {6}{0}" +
                            "entityID   = {7}"
                            , Environment.NewLine, item.reqNo, item.reqDesc, item.pctPaid, item.pctPaid, input.schemaID, item.isComplete, 1);

                        _msSchemaRequirementRepo.Insert(createMsSchemaRequirement);

                        Logger.DebugFormat("CreateOrUpdateMsSchemaRequirement() - Ended insert Schema Requirement.");
                    }
                    catch (DbException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsSchemaRequirement() ERROR DbException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("DB Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsSchemaRequirement() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }

                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateMsSchemaRequirement() - Start get data for update. Parameters sent:{0}" +
                            "schemaRequirementID      = {1}", item.schemaRequirementID);

                    var getMsSchemaRequirement = (from A in _msSchemaRequirementRepo.GetAll()
                                                  where item.schemaRequirementID == A.Id
                                                  select A).FirstOrDefault();

                    var update = getMsSchemaRequirement.MapTo<MS_SchemaRequirement>();

                    update.reqDesc = item.reqDesc;
                    update.pctPaid = item.pctPaid;
                    update.orPctPaid = item.pctPaid;

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsSchemaRequirement() - Start update Schema Requirement. Parameters sent:{0}" +
                            "reqDesc    = {1}{0}" +
                            "pctPaid    = {2}{0}" +
                            "orPctPaid  = {3}{0}"
                            , Environment.NewLine, item.reqDesc, item.pctPaid, item.pctPaid);

                        _msSchemaRequirementRepo.Update(update);

                        Logger.DebugFormat("CreateOrUpdateMsSchemaRequirement() - Ended update Schema Requirement.");
                    }
                    catch (DbException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsSchemaRequirement() ERROR DbException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("DB Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsSchemaRequirement() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }

            }
            Logger.Info("CreateOrUpdateMsSchemaRequirement() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Create)]
        public void CreateOrUpdateMsStatusMember(CreateMsStatusMemberInputDto input)
        {
            Logger.Info("CreateOrUpdateMsStatusMember() Started.");

            foreach (var item in input.setStatusMember)
            {
                if (item.statusMemberID == 0 || item.statusMemberID == null)
                {
                    var createMsStatusMember = new MS_StatusMember
                    {
                        statusCode = item.statusCode,
                        statusName = item.statusName,
                        pointMin = item.pointMin,
                        pointToKeepStatus = item.pointToKeepStatus,
                        reviewTimeYear = item.reviewTimeYear,
                        reviewStartMonth = item.reviewStartMonth,
                        statusStar = item.statusStar,
                        schemaID = input.schemaID,
                        isComplete = item.isComplete
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsStatusMember() - Start insert Status Member. Parameters sent:{0}" +
                            "statusCode         = {1}{0}" +
                            "statusName         = {2}{0}" +
                            "pointMin           = {3}{0}" +
                            "pointToKeepStatus  = {4}{0}" +
                            "reviewTimeYear     = {5}{0}" +
                            "reviewStartMonth   = {6}{0}" +
                            "statusStar         = {7}{0}" +
                            "schemaID           = {8}{0}" +
                            "isComplete         = {9}{0}"
                            , Environment.NewLine, item.statusCode, item.statusName, item.pointMin, item.pointToKeepStatus, item.reviewTimeYear, item.reviewStartMonth, item.statusStar, input.schemaID, item.isComplete);

                        _msStatusMemberRepo.Insert(createMsStatusMember);

                        Logger.DebugFormat("CreateOrUpdateMsStatusMember() - Ended insert Status Member.");
                    }
                    catch (DbException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR DbException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("DB Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }

                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateMsStatusMember() - Start get data for update. Parameters sent:{0}" +
                            "statusMemberID      = {1}", item.statusMemberID);

                    var getMsStatusMember = (from A in _msStatusMemberRepo.GetAll()
                                             where item.statusMemberID == A.Id
                                             select A).FirstOrDefault();

                    var update = getMsStatusMember.MapTo<MS_StatusMember>();

                    update.statusCode = item.statusCode;
                    update.statusName = item.statusName;
                    update.pointMin = item.pointMin;
                    update.pointToKeepStatus = item.pointToKeepStatus;
                    update.reviewTimeYear = item.reviewTimeYear;
                    update.reviewStartMonth = item.reviewStartMonth;
                    update.statusStar = item.statusStar;

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsStatusMember() - Start update Status Member. Parameters sent:{0}" +
                            "statusCode         = {1}{0}" +
                            "statusName         = {2}{0}" +
                            "pointMin           = {3}{0}" +
                            "pointToKeepStatus  = {4}{0}" +
                            "reviewTimeYear     = {5}{0}" +
                            "reviewStartMonth   = {6}{0}" +
                            "statusStar         = {7}{0}"
                            , Environment.NewLine, item.statusCode, item.statusName, item.pointMin, item.pointToKeepStatus, item.reviewTimeYear, item.reviewStartMonth, item.statusStar);

                        _msStatusMemberRepo.Update(update);

                        Logger.DebugFormat("CreateOrUpdateMsStatusMember() - Ended update Status Member.");
                    }
                    catch (DbException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR DbException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("DB Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }

            }
            Logger.Info("CreateOrUpdateMsStatusMember() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Create)]
        public int? CreateOrUpdateMsSchema(CreateOrUpdateSetSchemaInputDto input)
        {
            int scmID;
            short i = 0;
            Logger.Info("CreateOrUpdateMsSchema() - Started.");

            if (input.schemaID == 0)
            {
                Logger.DebugFormat("CreateOrUpdateMsSchema() - Start checking existing name and code. Parameters sent:{0}" +
                            "scmCode   = {1}{0}" +
                            "scmName   = {2}{0}"
                            , Environment.NewLine, input.scmCode, input.scmName);

                bool checkNameCode = (from x in _msSchemaRepo.GetAll()
                                      where x.scmCode == input.scmCode || x.scmName == input.scmName
                                      select x).Any();

                Logger.DebugFormat("CreateOrUpdateMsSchema() - End checking existing name and code. Resuly ={0}", checkNameCode);

                if (!checkNameCode)
                {
                    string docPath = null;

                    if (input.uploadDocument != null)
                    {
                        docPath = uploadFile(input.uploadDocument);
                        GetURLWithoutHost(docPath, out docPath);
                    }

                    var createSetSchema = new MS_Schema
                    {
                        scmCode = input.scmCode,
                        scmName = input.scmName,
                        digitMemberCode = input.scmCode.Substring(1, 2),
                        budgetPct = input.budgetPct,
                        document = docPath,
                        accACDPeriod = 0,
                        accCDPeriod = 0,
                        accPeriod = 0,
                        dueDateComm = input.dueDateComm,
                        isAcc = false,
                        isAccACD = false,
                        isAccCD = false,
                        isACDGetComm = false,
                        isActive = input.isActive,
                        isAutomaticMemberStatus = false,
                        isBudget = false,
                        isCapacity = false,
                        isCDGetComm = false,
                        isClub = false,
                        isCommHold = false,
                        isFix = false,
                        isFixACD = false,
                        isFixCD = false,
                        isHaveACD = false,
                        isHaveCD = false,
                        isOverRiding = false,
                        isPointCalc = false,
                        isSendSMSPaid = false,
                        isTeam = false,
                        accACDPeriodType = "-",
                        accCDPeriodType = "-",
                        accPeriodType = "-",
                        isComplete = input.isComplete,
                        entityID = 1
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsSchema() - Start insert Schema. Parameters sent:{0}" +
                            "scmCode        = {1}{0}" +
                            "scmName        = {2}{0}" +
                            "digitMemberCode= {3}{0}" +
                            "budgetPct      = {4}{0}" +
                            "document       = {5}{0}" +
                            "accACDPeriod   = {6}{0}" +
                            "accCDPeriod    = {7}{0}" +
                            "accPeriod      = {8}{0}" +
                            "dueDateComm    = {9}{0}" +
                            "isAcc          = {10}{0}" +
                            "isAccACD       = {11}{0}" +
                            "isAccCD        = {12}{0}" +
                            "isACDGetComm   = {13}{0}" +
                            "isActive       = {14}{0}" +
                            "isAutomaticMemberStatus    = {15}{0}" +
                            "isBudget       = {16}{0}" +
                            "isCapacity     = {17}{0}" +
                            "isCDGetComm    = {18}{0}" +
                            "isClub         = {19}{0}" +
                            "isCommHold     = {20}{0}" +
                            "isFix          = {21}{0}" +
                            "isFixACD       = {22}{0}" +
                            "isFixCD        = {23}{0}" +
                            "isHaveACD      = {24}{0}" +
                            "isHaveCD       = {25}{0}" +
                            "isOverRiding   = {26}{0}" +
                            "isPointCalc    = {27}{0}" +
                            "isSendSMSPaid  = {28}{0}" +
                            "isTeam         = {29}{0}" +
                            "accACDPeriodType   = {30}{0}" +
                            "accCDPeriodType    = {31}{0}" +
                            "accPeriodType      = {32}{0}" +
                            "isComplete         = {33}{0}" +
                            "entityID           = {34}{0}"
                            , Environment.NewLine, input.scmCode, input.scmName, input.scmCode.Substring(1, 2), input.budgetPct, docPath, 0, 0, 0, input.dueDateComm,
                            false, false, false, false, input.isActive, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false,
                            "-", "-", "-", input.isComplete, 1);

                        scmID = _msSchemaRepo.InsertAndGetId(createSetSchema);

                        Logger.DebugFormat("CreateOrUpdateMsSchema() - Ended insert Schema.");

                        do
                        {
                            var createLKUpline = new LK_Upline
                            {
                                schemaID = scmID,
                                uplineNo = i
                            };
                            Logger.DebugFormat("CreateOrUpdateMsSchema() - Start insert LK_Upline. Parameters sent:{0}" +
                            "schemaID        = {1}{0}" +
                            "uplineNo        = {2}{0}"
                            , Environment.NewLine, scmID, i);

                            _lkUplineRepo.Insert(createLKUpline);

                            Logger.DebugFormat("CreateOrUpdateMsSchema() - Ended insert LK_Upline.");

                            i++;
                        } while (i <= input.upline);
                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }

                else
                {
                    Logger.ErrorFormat("CreateOrUpdateMsStatusMember() ERROR. Result = {0}", "Schema Code or Schema Name Already Exist!");
                    throw new UserFriendlyException("Schema Code or Schema Name Already Exist!");
                }

                return scmID;
            }
            else
            {
                Logger.DebugFormat("CreateOrUpdateMsSchema() - Start checking existing code or name. Parameters sent:{0}" +
                            "schemaID        = {1}{0}" +
                            "uplineNo        = {2}{0}"
                            , Environment.NewLine, input.scmCode, input.scmName);

                var checkSchema = (from A in _msSchemaRepo.GetAll()
                                   where A.Id != input.schemaID && (A.scmCode == input.scmCode || A.scmName == input.scmName)
                                   select A).Any();

                Logger.DebugFormat("CreateOrUpdateMsSchema() - End checking existing code or name.");

                if (!checkSchema)
                {

                    var getMsSchema = (from A in _msSchemaRepo.GetAll()
                                       where input.schemaID == A.Id
                                       select A).FirstOrDefault();

                    var updateMsSchema = getMsSchema.MapTo<MS_Schema>();

                    updateMsSchema.scmName = input.scmName;
                    updateMsSchema.budgetPct = input.budgetPct;
                    updateMsSchema.isActive = input.isActive;
                    updateMsSchema.dueDateComm = input.dueDateComm;

                    var getUpline = (from upline in _lkUplineRepo.GetAll()
                                     where upline.schemaID == input.schemaID
                                     select upline.Id).ToList();

                    var fileToDelete = input.uploadDocumentDelete;

                    if (fileToDelete != null)
                    {
                        var docPath = uploadFile(input.uploadDocument);
                        GetURLWithoutHost(docPath, out docPath);
                        updateMsSchema.document = docPath;

                        var filenameToDelete = fileToDelete.Split(@"/");
                        var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
                        var deletePath = @"\Assets\Upload\SchemaFile\";

                        var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

                        if (File.Exists(deleteImage))
                        {
                            var file = new FileInfo(deleteImage);
                            file.Delete();
                        }
                    }
                    else
                    {
                        updateMsSchema.document = input.uploadDocument;
                    }


                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsSchema() - Start update Schema. Parameters sent:{0}" +
                            "scmName      = {1}{0}" +
                            "budgetPct    = {2}{0}" +
                            "isActive    = {3}{0}" +
                            "dueDateComm  = {4}{0}" +
                            "document   = {5}"
                            , Environment.NewLine, input.scmName, input.budgetPct, input.isActive, input.dueDateComm, input.uploadDocument);
                        _msSchemaRepo.Update(updateMsSchema);
                        Logger.DebugFormat("CreateOrUpdateMsSchema() - End update Schema.");

                        foreach (var item in getUpline)
                        {
                            try
                            {
                                Logger.DebugFormat("CreateOrUpdateMsSchema() - Start DELETE LK_Upline. Params sent:{0}", item);
                                _lkUplineRepo.Delete(item);
                                Logger.DebugFormat("CreateOrUpdateMsSchema() - End DELETE LK_Upline.");
                            }
                            catch (DataException ex)
                            {
                                Logger.ErrorFormat("CreateOrUpdateMsSchema() ERROR DataException. Result = {0}", ex.Message);
                                throw new UserFriendlyException("Db Error: " + ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Logger.ErrorFormat("CreateOrUpdateMsSchema() ERROR Exception. Result = {0}", ex.Message);
                                throw new UserFriendlyException("Error: " + ex.Message);
                            }
                        }

                        do
                        {
                            var createLKUpline = new LK_Upline
                            {
                                schemaID = input.schemaID,
                                uplineNo = i
                            };

                            Logger.DebugFormat("CreateOrUpdateMsSchema() - Start INSERT LK_Upline. Params sent:{0}" +
                                "schemaID   ={1}{0}" +
                                "uplineNo   ={2}"
                                , Environment.NewLine, input.schemaID, i);
                            _lkUplineRepo.Insert(createLKUpline);
                            Logger.InfoFormat("INSERT LK_Upline Data: {0} {1}", input.schemaID, i);

                            Logger.DebugFormat("CreateOrUpdateMsSchema() - End INSERT LK_Upline.");

                            i++;
                        } while (i <= input.upline);

                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsSchema() ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsSchema() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("CreateOrUpdateMsSchema() ERROR. Result = {0}", "Schema Code or Schema Name Already Exist!");
                    throw new UserFriendlyException("Schema Code or Schema Name Already Exist!");
                }
                Logger.Info("CreateOrUpdateMsSchema() - Finished.");
                return input.schemaID;
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Create)]
        public void CreateOrUpdateLkPointType(CreateOrUpdateSetPointTypeInputDto input)
        {
            Logger.Info("CreateOrUpdateLkPointType() - Started.");

            foreach (var item in input.setPointType)
            {
                if (item.pointTypeID == 0 || item.pointTypeID == null)
                {
                    Logger.DebugFormat("CreateOrUpdateLkPointType() - Start checking before insert LK Point Type. Parameters sent:{0}" +
                                "schemaID = {1}{0}" +
                                "pointTypeCode = {2}{0}" +
                                "pointTypeName = {3}{0}" +
                                "isComplete = {4}{0}"
                                , Environment.NewLine, input.schemaID, item.pointTypeCode, item.pointTypeName, item.isComplete);

                    var check = (from x in _lkPointTypeRepo.GetAll()
                                 where x.schemaID == input.schemaID &&
                                 (x.pointTypeCode == item.pointTypeCode || x.pointTypeName == item.pointTypeName) &&
                                 x.isComplete == item.isComplete
                                 select x).Any();

                    Logger.DebugFormat("CreateOrUpdateLkPointType() - Ended checking before insert LK Point Type. Result = {0}", check);

                    if (!check)
                    {
                        var createLkPointType = new LK_PointType
                        {
                            pointTypeCode = item.pointTypeCode,
                            pointTypeName = item.pointTypeName,
                            schemaID = input.schemaID,
                            isComplete = item.isComplete
                        };

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateLkPointType() - Start insert LK Point Type. Parameters sent:{0}" +
                                        "pointTypeCode = {1}{0}" +
                                        "pointTypeName = {2}{0}" +
                                        "schemaID = {3}{0}" +
                                        "isComplete = {4}{0}"
                                        , Environment.NewLine, item.pointTypeCode, item.pointTypeName, input.schemaID, item.isComplete);

                            _lkPointTypeRepo.Insert(createLkPointType);

                            Logger.DebugFormat("CreateOrUpdateLkPointType() - Ended insert LK Point Type.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkPointType() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkPointType() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {

                        Logger.ErrorFormat("CreateOrUpdateLkPointType() - ERROR. Result = {0}", "Point Type Code or Point Type Name Already Exist !");
                        throw new UserFriendlyException("Point Type Code or Point Type Name Already Exist !");
                    }

                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateLkPointType() - Start checking before update LK Point Type. Parameters sent:{0}" +
                                "schemaID = {1}{0}" +
                                "pointTypeCode = {2}{0}" +
                                "pointTypeName = {3}{0}" +
                                "isComplete = {4}{0}" +
                                "pointTypeID = {5}{0}"

                                , Environment.NewLine, input.schemaID, item.pointTypeCode, item.pointTypeName, item.isComplete, item.pointTypeID);

                    var check = (from x in _lkPointTypeRepo.GetAll()
                                 where x.Id != item.pointTypeID && x.schemaID == input.schemaID &&
                                 (x.pointTypeCode == item.pointTypeCode || x.pointTypeName == item.pointTypeName) &&
                                 x.isComplete == item.isComplete
                                 select x).Any();

                    Logger.DebugFormat("CreateOrUpdateLkPointType() - Ended checking before update LK Point Type. Result = {0}", check);

                    if (!check)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkPointType() - Start get data before update LK Point Type. Parameters sent:{0}" +
                                    "pointTypeID = {1}{0}"
                                    , Environment.NewLine, item.pointTypeID);

                        var getLkPointType = (from A in _lkPointTypeRepo.GetAll()
                                              where item.pointTypeID == A.Id
                                              select A).FirstOrDefault();

                        Logger.DebugFormat("CreateOrUpdateLkPointType() - Ended get data before update LK Point Type.");

                        var update = getLkPointType.MapTo<LK_PointType>();

                        update.pointTypeCode = item.pointTypeCode;
                        update.pointTypeName = item.pointTypeName;


                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateLkPointType() - Start update LK Point Type. Parameters sent:{0}" +
                                        "pointTypeCode = {1}{0}" +
                                        "pointTypeName = {2}{0}"
                                        , Environment.NewLine, item.pointTypeCode, item.pointTypeName);

                            _lkPointTypeRepo.Update(update);

                            Logger.DebugFormat("CreateOrUpdateLkPointType() - Ended update LK Point Type.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkPointType() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkPointType() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        Logger.ErrorFormat("CreateOrUpdateLkPointType() - ERROR. Result = {0}", "Point Type Code or Point Type Name Already Exist !");
                        throw new UserFriendlyException("Point Type Code or Point Type Name Already Exist !");
                    }
                }

            }

            Logger.Info("CreateOrUpdateLkPointType() - Finished.");

        }

        public List<GetMsCommPctListDto> GetMsCommPctBySchemaID(int schemaID, bool isComplete)
        {
            var listResult = (from commPct in _msCommPctRepo.GetAll()
                              join commType in _lkCommTypeRepo.GetAll() on commPct.commTypeID equals commType.Id
                              join status in _msStatusMemberRepo.GetAll() on commPct.statusMemberID equals status.Id
                              where commPct.schemaID == schemaID && commPct.isComplete == isComplete
                              select new GetMsCommPctListDto
                              {
                                  commPctID = commPct.Id,
                                  validDate = commPct.validDate,
                                  commTypeID = commType.Id,
                                  commTypeCode = commType.commTypeCode,
                                  commTypeName = commType.commTypeName,
                                  uplineNo = commPct.asUplineNo,
                                  statusMemberID = status.Id,
                                  statusCode = status.statusCode,
                                  statusName = status.statusName,
                                  commPctPaid = commPct.commPctPaid,
                                  nominal = commPct.nominal
                              }).ToList();
            return new List<GetMsCommPctListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Create)]
        public void CreateOrUpdateLkCommType(CreateOrUpdateSetCommTypeInputDto input)
        {
            Logger.Info("CreateOrUpdateLkCommType() - Started.");

            foreach (var item in input.setCommType)
            {

                if (item.commTypeID == 0 || item.commTypeID == null)
                {
                    Logger.DebugFormat("CreateOrUpdateLkCommType() - Start checking before insert LK Comm Type. Parameters sent:{0}" +
                                "schemaID = {1}{0}" +
                                "commTypeCode = {2}{0}" +
                                "commTypeName = {3}{0}" +
                                "isComplete = {4}{0}"
                                , Environment.NewLine, input.schemaID, item.commTypeCode, item.commTypeName, item.isComplete);

                    var check = (from x in _lkCommTypeRepo.GetAll()
                                 where x.schemaID == input.schemaID &&
                                 (x.commTypeCode == item.commTypeCode || x.commTypeName == item.commTypeName) &&
                                 x.isComplete == item.isComplete
                                 select x).Any();

                    Logger.DebugFormat("CreateOrUpdateLkCommType() - Ended checking before insert LK Comm Type. Result = {0}", check);

                    if (!check)
                    {
                        var createLkCommType = new LK_CommType
                        {
                            commTypeCode = item.commTypeCode,
                            commTypeName = item.commTypeName,
                            schemaID = input.schemaID,
                            isComplete = item.isComplete
                        };

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateLkCommType() - Start insert LK Comm Type. Parameters sent:{0}" +
                                        "commTypeCode = {1}{0}" +
                                        "pointTypeName = {2}{0}" +
                                        "schemaID = {3}{0}" +
                                        "isComplete = {4}{0}"
                                        , Environment.NewLine, item.commTypeCode, item.commTypeName, input.schemaID, item.isComplete);

                            _lkCommTypeRepo.Insert(createLkCommType);

                            Logger.DebugFormat("CreateOrUpdateLkCommType() - Ended insert LK Comm Type.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkCommType() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkCommType() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {

                        Logger.ErrorFormat("CreateOrUpdateLkCommType() - ERROR. Result = {0}", "Comm Type Code or Comm Type Name Already Exist !");
                        throw new UserFriendlyException("Comm Type Code or Comm Type Name Already Exist !");
                    }

                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateLkCommType() - Start checking before update LK Comm Type. Parameters sent:{0}" +
                                "schemaID = {1}{0}" +
                                "commTypeCode = {2}{0}" +
                                "commTypeName = {3}{0}" +
                                "isComplete = {4}{0}" +
                                "commTypeID = {5}{0}"
                                , Environment.NewLine, input.schemaID, item.commTypeCode, item.commTypeName, item.isComplete, item.commTypeID);

                    var check = (from x in _lkCommTypeRepo.GetAll()
                                 where x.Id != item.commTypeID && x.schemaID == input.schemaID &&
                                 (x.commTypeCode == item.commTypeCode || x.commTypeName == item.commTypeName) &&
                                 x.isComplete == item.isComplete
                                 select x).Any();

                    Logger.DebugFormat("CreateOrUpdateLkCommType() - Ended checking before update LK Comm Type. Result = {0}", check);

                    if (!check)
                    {
                        Logger.DebugFormat("CreateOrUpdateLkCommType() - Start get data before update LK Comm Type. Parameters sent:{0}" +
                                    "commTypeID = {1}{0}"
                                    , Environment.NewLine, item.commTypeID);

                        var getLkCommType = (from A in _lkCommTypeRepo.GetAll()
                                             where item.commTypeID == A.Id
                                             select A).FirstOrDefault();

                        Logger.DebugFormat("CreateOrUpdateLkCommType() - Ended get data before update LK Comm Type.");

                        var update = getLkCommType.MapTo<LK_CommType>();

                        update.commTypeCode = item.commTypeCode;
                        update.commTypeName = item.commTypeName;

                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateLkCommType() - Start update LK Comm Type. Parameters sent:{0}" +
                                        "commTypeCode = {1}{0}" +
                                        "commTypeName = {2}{0}"
                                        , Environment.NewLine, item.commTypeCode, item.commTypeName);

                            _lkCommTypeRepo.Update(update);

                            Logger.DebugFormat("CreateOrUpdateLkCommType() - Ended update LK Comm Type.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkCommType() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateLkCommType() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {

                        Logger.ErrorFormat("CreateOrUpdateLkCommType() - ERROR. Result = {0}", "Comm Type Code or Comm Type Name Already Exist !");
                        throw new UserFriendlyException("Comm Type Code or Comm Type Name Already Exist !");
                    }
                }

            }
            Logger.Info("CreateOrUpdateLkCommType() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Create)]
        public void CreateOrUpdateMsCommPct(CreateOrUpdateMsCommPctInputDto input)
        {
            Logger.Info("CreateOrUpdateMsCommPct() - Started.");

            foreach (var item in input.setCommPct)
            {
                if (item.commPctID == 0 || item.commPctID == null)
                {
                    var createMsCommPct = new MS_CommPct
                    {
                        schemaID = input.schemaID,
                        validDate = item.validDate,
                        commTypeID = item.commTypeID,
                        statusMemberID = item.statusMemberID,
                        asUplineNo = item.uplineNo,
                        commPctPaid = item.commPctPaid,
                        nominal = item.nominal,
                        isComplete = item.isComplete
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsCommPct() - Start insert Comm Pct. Parameters sent:{0}" +
                                        "schemaID = {1}{0}" +
                                        "validDate = {2}{0}" +
                                        "commTypeID = {3}{0}" +
                                        "statusMemberID = {4}{0}" +
                                        "asUplineNo = {5}{0}" +
                                        "commPctPaid = {6}{0}" +
                                        "nominal = {7}{0}" +
                                        "isComplete = {8}{0}"
                                        , Environment.NewLine, input.schemaID, item.validDate, item.commTypeID, item.statusMemberID
                                        , item.uplineNo, item.commPctPaid, item.nominal, item.isComplete);

                        _msCommPctRepo.Insert(createMsCommPct);

                        Logger.DebugFormat("CreateOrUpdateMsCommPct() - Ended insert Comm Pct.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsCommPct() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsCommPct() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }

                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateMsCommPct() - Start get data before update Comm Pct. Parameters sent:{0}" +
                                    "commPctID = {1}{0}"
                                    , Environment.NewLine, item.commPctID);

                    var getMsCommPct = (from A in _msCommPctRepo.GetAll()
                                        where item.commPctID == A.Id
                                        select A).FirstOrDefault();

                    Logger.DebugFormat("CreateOrUpdateMsCommPct() - Ended get data before update Comm Pct.");

                    var update = getMsCommPct.MapTo<MS_CommPct>();

                    update.commTypeID = item.commTypeID;
                    update.statusMemberID = item.statusMemberID;
                    update.asUplineNo = item.uplineNo;
                    update.commPctPaid = item.commPctPaid;
                    update.nominal = item.nominal;

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateMsCommPct() - Start update Comm Pct. Parameters sent:{0}" +
                                        "commTypeID = {1}{0}" +
                                        "statusMemberID = {2}{0}" +
                                        "asUplineNo = {3}{0}" +
                                        "commPctPaid = {4}{0}" +
                                        "nominal = {5}{0}"
                                        , Environment.NewLine, item.commTypeID, item.statusMemberID
                                        , item.uplineNo, item.commPctPaid, item.nominal);

                        _msCommPctRepo.Update(update);

                        Logger.DebugFormat("CreateOrUpdateMsCommPct() - Ended update Comm Pct.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsCommPct() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateMsCommPct() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }

            }

            Logger.Info("CreateOrUpdateMsCommPct() - Finished.");
        }

        public CreateOrUpdateSetSchemaInputDto GetDetailMsSchema(int schemaID)
        {
            var listSchema = (from x in _msSchemaRepo.GetAll()
                              join y in _lkUplineRepo.GetAll()
                              on x.Id equals y.schemaID
                              where x.Id == schemaID
                              orderby x.Id descending
                              select new CreateOrUpdateSetSchemaInputDto
                              {
                                  schemaID = schemaID,
                                  scmCode = x.scmCode,
                                  scmName = x.scmName,
                                  budgetPct = x.budgetPct,
                                  dueDateComm = x.dueDateComm,
                                  uploadDocument = (x != null && x.document != null) ? getAbsoluteUriWithoutTail() + GetURLWithoutHost(x.document) : null, //TODO link + ip host
                                  isActive = x.isActive
                              }).FirstOrDefault();

            var countUpline = (from z in _lkUplineRepo.GetAll()
                               where z.schemaID == schemaID
                               select z).Count() - 1;

            var listResult = new CreateOrUpdateSetSchemaInputDto()
            {
                schemaID = schemaID,
                scmCode = listSchema.scmCode,
                scmName = listSchema.scmName,
                upline = (short)countUpline,
                budgetPct = listSchema.budgetPct,
                dueDateComm = listSchema.dueDateComm,
                uploadDocument = listSchema.uploadDocument,
                isActive = listSchema.isActive
            };

            return listResult;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Delete)]
        public void DeleteLkPointType(int Id, string flag)
        {
            Logger.Info("DeleteLkPointType() - Started.");

            //delete-add
            if (flag == "add")
            {
                try
                {
                    Logger.DebugFormat("DeleteLkPointType() - Start hard delete LK Point Type. Parameters sent:{0}" +
                        "pointTypeID = {1}{0}"
                        , Environment.NewLine, Id);

                    _lkPointTypeRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteLkPointType() - Ended hard delete LK Point Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteLkPointType() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteLkPointType() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            //delete-edit
            else
            {
                //LkPointType
                var getLkPointType = (from x in _lkPointTypeRepo.GetAll()
                                      where x.Id == Id
                                      select x).FirstOrDefault();


                var updateLkPointType = getLkPointType.MapTo<LK_PointType>();

                updateLkPointType.isComplete = false;

                Logger.DebugFormat("DeleteLkPointType() - Start soft delete LK Point Type. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                _lkPointTypeRepo.Update(updateLkPointType);

                Logger.DebugFormat("DeleteLkPointType() - Ended soft delete LK Point Type.");

                //PointPct
                var getPointPct = (from x in _msPointPctRepo.GetAll()
                                   where x.pointTypeID == Id
                                   select x).ToList();

                foreach (var item in getPointPct)
                {
                    var updatePointPct = item.MapTo<MS_PointPct>();

                    updatePointPct.isComplete = false;

                    Logger.DebugFormat("DeleteLkPointType() - Start soft delete Point Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msPointPctRepo.Update(updatePointPct);

                    Logger.DebugFormat("DeleteLkPointType() - Ended soft delete Point Pct.");
                }
            }
            Logger.Info("DeleteLkPointType() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Delete)]
        public void DeleteMsSchema(int Id)
        {
            Logger.Info("DeleteMsSchema() - Started.");

            Logger.DebugFormat("DeleteMsSchema() - Start checking before delete TR Comm Pay. Parameters sent:{0}" +
                        "schemaID = {1}{0}"
                        , Environment.NewLine, Id);

            var cekTrCommPay = (from a in _trCommPaymentRepo.GetAll()
                                where a.schemaID == Id
                                select a).Any();

            Logger.DebugFormat("DeleteMsSchema() - Ended checking before delete TR Comm Pay. Result = {0}", cekTrCommPay);

            Logger.DebugFormat("DeleteMsSchema() - Start checking before delete TR Comm Pay PPH. Parameters sent:{0}" +
                        "schemaID = {1}{0}"
                        , Environment.NewLine, Id);

            var cekTrCommPayPph = (from b in _trCommPaymentPphRepo.GetAll()
                                   where b.schemaID == Id
                                   select b).Any();

            Logger.DebugFormat("DeleteMsSchema() - Ended checking before delete TR Comm Pay PPH. Result = {0}", cekTrCommPayPph);

            Logger.DebugFormat("DeleteMsSchema() - Start checking before delete TR Management Fee. Parameters sent:{0}" +
                        "schemaID = {1}{0}"
                        , Environment.NewLine, Id);

            var cekTrManagementFee = (from c in _trManagementFeeRepo.GetAll()
                                      where c.schemaID == Id
                                      select c).Any();

            Logger.DebugFormat("DeleteMsSchema() - Ended checking before delete TR Management Fee. Result = {0}", cekTrManagementFee);

            Logger.DebugFormat("DeleteMsSchema() - Start checking before delete TR Sold Unit. Parameters sent:{0}" +
                        "schemaID = {1}{0}"
                        , Environment.NewLine, Id);

            var cekTrSoldUnit = (from d in _trSoldUnitRepo.GetAll()
                                 where d.schemaID == Id
                                 select d).Any();

            Logger.DebugFormat("DeleteMsSchema() - Ended checking before delete TR Sold Unit. Result = {0}", cekTrSoldUnit);

            Logger.DebugFormat("DeleteMsSchema() - Start checking before delete TR Sold Unit Req. Parameters sent:{0}" +
                        "schemaID = {1}{0}"
                        , Environment.NewLine, Id);

            var cekTrSoldUnitReq = (from e in _trSoldUnitReqRepo.GetAll()
                                    where e.schemaID == Id
                                    select e).Any();

            Logger.DebugFormat("DeleteMsSchema() - Ended checking before delete TR Sold Unit Req. Result = {0}", cekTrSoldUnitReq);

            if (!cekTrCommPay && !cekTrCommPayPph && !cekTrManagementFee && !cekTrSoldUnit && !cekTrSoldUnitReq)
            {
                //Schema
                var getSchema = (from x in _msSchemaRepo.GetAll()
                                 where x.Id == Id && x.isComplete == true
                                 select x).FirstOrDefault();

                var updateSchema = getSchema.MapTo<MS_Schema>();

                updateSchema.isComplete = false;

                Logger.DebugFormat("DeleteMsSchema() - Start soft delete Schema. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                _msSchemaRepo.Update(updateSchema);

                Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Schema.");

                //SchemaRequirement
                var getSchemaRequirement = (from x in _msSchemaRequirementRepo.GetAll()
                                            where x.schemaID == Id && x.isComplete == true
                                            select x).ToList();

                foreach (var item in getSchemaRequirement)
                {
                    var updateSchemaRequirement = item.MapTo<MS_SchemaRequirement>();

                    updateSchemaRequirement.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete Schema Req. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msSchemaRequirementRepo.Update(updateSchemaRequirement);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Schema Req.");
                }

                //Property
                var getProperty = (from x in _msPropertyRepo.GetAll()
                                   where x.schemaID == Id && x.isComplete == true
                                   select x).ToList();

                foreach (var item in getProperty)
                {
                    var updateProperty = item.MapTo<MS_Property>();

                    updateProperty.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete Property. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msPropertyRepo.Update(updateProperty);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Property.");
                }

                //DevSchema
                var getDevSchema = (from x in _msDeveloperSchemaRepo.GetAll()
                                    where x.schemaID == Id && x.isComplete == true
                                    select x).ToList();

                foreach (var item in getDevSchema)
                {
                    var updateDevSchema = item.MapTo<MS_Developer_Schema>();

                    updateDevSchema.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete Developer Schema. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msDeveloperSchemaRepo.Update(updateDevSchema);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Developer Schema.");
                }

                //GroupSchema
                var getGroupSchema = (from x in _msGroupSchemaRepo.GetAll()
                                      where x.schemaID == Id && x.isComplete == true
                                      select x).ToList();

                foreach (var item in getGroupSchema)
                {
                    var updateGroupSchema = item.MapTo<MS_GroupSchema>();

                    updateGroupSchema.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete Group Schema. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msGroupSchemaRepo.Update(updateGroupSchema);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Group Schema.");
                }

                //Status
                var getStatus = (from x in _msStatusMemberRepo.GetAll()
                                 where x.schemaID == Id && x.isComplete == true
                                 select x).ToList();

                foreach (var item in getStatus)
                {
                    DeleteMsStatusMember(item.Id, "edit");
                }

                //CommType
                var getCommType = (from x in _lkCommTypeRepo.GetAll()
                                   where x.schemaID == Id && x.isComplete == true
                                   select x).ToList();

                foreach (var item in getCommType)
                {
                    DeleteLkCommType(item.Id, "edit");
                }

                ////GroupCommPct
                //var getGroupCommPct = (from x in _msGroupCommPctRepo.GetAll()
                //                       where x.schemaID == Id && x.isComplete == true
                //                       select x).ToList();

                //foreach (var item in getGroupCommPct)
                //{
                //    var updateGroupCommPct = item.MapTo<MS_GroupCommPct>();

                //    updateGroupCommPct.isComplete = false;

                //    _msGroupCommPctRepo.Update(updateGroupCommPct);
                //}

                ////GroupCommPctNonStd
                //var getGroupCommPctNonStd = (from x in _msGroupCommPctNonStdRepo.GetAll()
                //                             where x.schemaID == Id && x.isComplete == true
                //                             select x).ToList();

                //foreach (var item in getGroupCommPctNonStd)
                //{
                //    var updateGroupCommPctNonStd = item.MapTo<MS_GroupCommPctNonStd>();

                //    updateGroupCommPctNonStd.isComplete = false;

                //    _msGroupCommPctNonStdRepo.Update(updateGroupCommPctNonStd);
                //}

                //Upline
                var getUpline = (from x in _lkUplineRepo.GetAll()
                                 where x.schemaID == Id && x.isComplete == true
                                 select x).ToList();

                foreach (var item in getUpline)
                {
                    var updateUpline = item.MapTo<LK_Upline>();

                    updateUpline.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete Upline. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _lkUplineRepo.Update(updateUpline);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Upline.");
                }

                //CommPct
                var getCommPct = (from x in _msCommPctRepo.GetAll()
                                  where x.schemaID == Id && x.isComplete == true
                                  select x).ToList();

                foreach (var item in getCommPct)
                {
                    DeleteMsCommPct(item.Id, "edit");
                }

                //PPhRange
                var getPPhRange = (from x in _msPPhRangeRepo.GetAll()
                                   where x.schemaID == Id && x.isComplete == true
                                   select x).ToList();

                foreach (var item in getPPhRange)
                {
                    var updatePPhRange = item.MapTo<MS_PPhRange>();

                    updatePPhRange.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete PPH Range. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msPPhRangeRepo.Update(updatePPhRange);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete PPH Range.");
                }

                //PPhRangeIns
                var getPPhRangeIns = (from x in _msPPhRangeInsRepo.GetAll()
                                      where x.schemaID == Id && x.isComplete == true
                                      select x).ToList();

                foreach (var item in getPPhRangeIns)
                {
                    var updatePPhRangeIns = item.MapTo<MS_PPhRangeIns>();

                    updatePPhRangeIns.isComplete = false;

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete PPH Range Ins. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msPPhRangeInsRepo.Update(updatePPhRangeIns);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete PPH Range Ins.");
                }

                //PointType
                var getPointType = (from x in _lkPointTypeRepo.GetAll()
                                    where x.schemaID == Id && x.isComplete == true
                                    select x).ToList();

                foreach (var item in getPointType)
                {
                    DeleteLkPointType(item.Id, "edit");
                }

                //PointPct
                var getPointPct = (from x in _msPointPctRepo.GetAll()
                                   where x.schemaID == Id && x.isComplete == true
                                   select x).ToList();

                foreach (var item in getPointPct)
                {
                    var updatePointPct = item.MapTo<MS_PointPct>();

                    Logger.DebugFormat("DeleteMsSchema() - Start soft delete Point Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    updatePointPct.isComplete = false;

                    _msPointPctRepo.Update(updatePointPct);

                    Logger.DebugFormat("DeleteMsSchema() - Ended soft delete Point Pct.");
                }

            }
            else
            {
                Logger.ErrorFormat("DeleteMsSchema() - ERROR. Result = {0}", "This Schema is used in transaction");
                throw new UserFriendlyException("This Schema is used in transaction");
            }
            Logger.Info("DeleteMsSchema() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Delete)]
        public void DeleteMsSchemaRequirement(int Id, string flag)
        {
            Logger.Info("DeleteMsSchemaRequirement() - Started.");

            //delete-add
            if (flag == "add")
            {
                try
                {
                    Logger.DebugFormat("DeleteMsSchemaRequirement() - Start hard delete Schema Requirement. Parameters sent:{0}" +
                        "pointTypeID = {1}{0}"
                        , Environment.NewLine, Id);

                    _msSchemaRequirementRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteMsSchemaRequirement() - Ended hard delete Schema Requirement.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsSchemaRequirement() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsSchemaRequirement() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            //delete-edit
            else
            {
                var getMsSchemaRequirement = (from A in _msSchemaRequirementRepo.GetAll()
                                              where A.Id == Id
                                              select A).FirstOrDefault();

                var update = getMsSchemaRequirement.MapTo<MS_SchemaRequirement>();

                update.isComplete = false;

                try
                {
                    Logger.DebugFormat("DeleteMsSchemaRequirement() - Start soft delete Schema Requirement. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msSchemaRequirementRepo.Update(update);

                    Logger.DebugFormat("DeleteMsSchemaRequirement() - Ended soft delete Schema Requirement.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsSchemaRequirement() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsSchemaRequirement() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("DeleteMsSchemaRequirement() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterSchema_Delete)]
        public void DeleteMsStatusMember(int Id, string flag)
        {
            Logger.Info("DeleteMsStatusMember() - Started.");

            //delete-add
            if (flag == "add")
            {
                try
                {
                    Logger.DebugFormat("DeleteMsStatusMember() - Start hard delete Status Member. Parameters sent:{0}" +
                        "statusMemberID = {1}{0}"
                        , Environment.NewLine, Id);

                    _msStatusMemberRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteMsStatusMember() - Ended hard delete Status Member.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsStatusMember() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsStatusMember() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            //delete-edit
            else
            {

                //StatusMember
                var getStatusMember = (from x in _msStatusMemberRepo.GetAll()
                                       where x.Id == Id && x.isComplete == true
                                       select x).FirstOrDefault();


                var updateStatusMember = getStatusMember.MapTo<MS_StatusMember>();

                updateStatusMember.isComplete = false;

                Logger.DebugFormat("DeleteMsStatusMember() - Start soft delete Status Member. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                _msStatusMemberRepo.Update(updateStatusMember);

                Logger.DebugFormat("DeleteMsStatusMember() - Ended soft delete Status Member.");

                //CommPct
                var getCommPct = (from x in _msCommPctRepo.GetAll()
                                  where x.statusMemberID == Id && x.isComplete == true
                                  select x).ToList();

                foreach (var item in getCommPct)
                {
                    var updateCommPct = item.MapTo<MS_CommPct>();

                    updateCommPct.isComplete = false;

                    Logger.DebugFormat("DeleteMsStatusMember() - Start soft delete Comm Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msCommPctRepo.Update(updateCommPct);

                    Logger.DebugFormat("DeleteMsStatusMember() - Ended soft delete Comm Pct.");
                }

                //GroupCommPct
                var getGroupCommPct = (from x in _msGroupCommPctRepo.GetAll()
                                       where x.statusMemberID == Id && x.isComplete == true
                                       select x).ToList();

                foreach (var item in getGroupCommPct)
                {
                    var updateGroupCommPct = item.MapTo<MS_GroupCommPct>();

                    updateGroupCommPct.isComplete = false;

                    Logger.DebugFormat("DeleteMsStatusMember() - Start soft delete Group Comm Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msGroupCommPctRepo.Update(updateGroupCommPct);

                    Logger.DebugFormat("DeleteMsStatusMember() - Ended soft delete Group Comm Pct.");
                }

                //GroupCommPctNonStd
                var getGroupCommPctNonStd = (from x in _msGroupCommPctNonStdRepo.GetAll()
                                             where x.statusMemberID == Id && x.isComplete == true
                                             select x).ToList();

                foreach (var item in getGroupCommPctNonStd)
                {
                    var updateGroupCommPctNonStd = item.MapTo<MS_GroupCommPctNonStd>();

                    updateGroupCommPctNonStd.isComplete = false;

                    Logger.DebugFormat("DeleteMsStatusMember() - Start soft delete Group Comm Pct Non STD. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msGroupCommPctNonStdRepo.Update(updateGroupCommPctNonStd);

                    Logger.DebugFormat("DeleteMsStatusMember() - Ended soft delete Group Comm Pct Non STD.");
                }

                //PointPct
                var getPointPct = (from x in _msPointPctRepo.GetAll()
                                   where x.statusMemberID == Id && x.isComplete == true
                                   select x).ToList();

                foreach (var item in getPointPct)
                {
                    var updatePointPct = item.MapTo<MS_PointPct>();

                    updatePointPct.isComplete = false;

                    Logger.DebugFormat("DeleteMsStatusMember() - Start soft delete Point Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msPointPctRepo.Update(updatePointPct);

                    Logger.DebugFormat("DeleteMsStatusMember() - Ended soft delete Point Pct.");
                }

            }
            Logger.Info("DeleteMsStatusMember() - Finished.");
        }

        public void UpdateIsComplete(int schemaID, bool flag)
        {
            Logger.Info("UpdateIsComplete() - Started.");

            //flag 1 = create
            if (flag == true)
            {
                //Schema
                var getSetSchema = (from schema in _msSchemaRepo.GetAll()
                                    where schema.Id == schemaID
                                    select schema).FirstOrDefault();
                var update = getSetSchema.MapTo<MS_Schema>();

                update.isComplete = true;

                Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Schema. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                _msSchemaRepo.Update(update);

                Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Schema.");

                //Upline
                var getSetUpline = (from upline in _lkUplineRepo.GetAll()
                                    where upline.schemaID == schemaID
                                    select upline).ToList();

                foreach (var item in getSetUpline)
                {
                    var updateUpline = item.MapTo<LK_Upline>();

                    updateUpline.isComplete = true;

                    Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Upline. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                    _lkUplineRepo.Update(updateUpline);

                    Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Upline.");
                }

                //Schema Req
                var getSetRequirementSchema = (from schemaReq in _msSchemaRequirementRepo.GetAll()
                                               where schemaReq.schemaID == schemaID
                                               select schemaReq).ToList();

                foreach (var item in getSetRequirementSchema)
                {
                    var updateReq = item.MapTo<MS_SchemaRequirement>();

                    updateReq.isComplete = true;

                    Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Schema Req. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                    _msSchemaRequirementRepo.Update(updateReq);

                    Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Schema Req.");
                }

                //Comm Type
                var getSetCommType = (from commType in _lkCommTypeRepo.GetAll()
                                      where commType.schemaID == schemaID
                                      select commType).ToList();

                foreach (var item in getSetCommType)
                {
                    var updateCommtype = item.MapTo<LK_CommType>();

                    updateCommtype.isComplete = true;

                    Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Comm Type. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                    _lkCommTypeRepo.Update(updateCommtype);

                    Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Comm Type.");
                }

                //Status Member
                var getSetStatusMember = (from status in _msStatusMemberRepo.GetAll()
                                          where status.schemaID == schemaID
                                          select status).ToList();

                foreach (var item in getSetStatusMember)
                {
                    var updateStatus = item.MapTo<MS_StatusMember>();

                    updateStatus.isComplete = true;

                    Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Status Member. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                    _msStatusMemberRepo.Update(updateStatus);

                    Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Status Member.");
                }

                //Point Type
                var getSetPointType = (from pointType in _lkPointTypeRepo.GetAll()
                                       where pointType.schemaID == schemaID
                                       select pointType).ToList();

                foreach (var item in getSetPointType)
                {
                    var updatePoint = item.MapTo<LK_PointType>();

                    updatePoint.isComplete = true;

                    Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Point Type. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                    _lkPointTypeRepo.Update(updatePoint);

                    Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Point Type.");
                }

                //CommPct
                var getCommPct = (from x in _msCommPctRepo.GetAll()
                                  where x.schemaID == schemaID
                                  select x).ToList();

                foreach (var item in getCommPct)
                {
                    var updateCommPct = item.MapTo<MS_CommPct>();
                    updateCommPct.isComplete = true;

                    Logger.DebugFormat("UpdateIsComplete() - Start update isComplete Comm Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, true);

                    _msCommPctRepo.Update(updateCommPct);

                    Logger.DebugFormat("UpdateIsComplete() - Ended update isComplete Comm Pct.");
                }
            }
            Logger.Info("UpdateIsComplete() - Finished.");
        }

        public List<GetLkUplineListDto> GetLkUpline(int schemaID)
        {
            var listResult = (from x in _lkUplineRepo.GetAll()
                              where x.schemaID == schemaID
                              select new GetLkUplineListDto
                              {
                                  uplineID = x.Id,
                                  uplineNo = x.uplineNo
                              }).ToList();

            return listResult;
        }

        public void DeleteLkCommType(int Id, string flag)
        {
            Logger.Info("DeleteLkCommType() - Started.");

            //delete-add
            if (flag == "add")
            {
                try
                {
                    Logger.DebugFormat("DeleteLkCommType() - Start hard delete LK Comm Type. Parameters sent:{0}" +
                        "commTypeID = {1}{0}"
                        , Environment.NewLine, Id);

                    _lkCommTypeRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteLkCommType() - Ended hard delete LK Point Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteLkCommType() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteLkCommType() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            //delete-edit
            else
            {
                //CommPct
                var getCommPct = (from x in _msCommPctRepo.GetAll()
                                  where x.commTypeID == Id && x.isComplete == true
                                  select x).ToList();

                foreach (var item in getCommPct)
                {
                    var updateCommPct = item.MapTo<MS_CommPct>();

                    updateCommPct.isComplete = false;

                    Logger.DebugFormat("DeleteLkCommType() - Start soft delete Comm Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msCommPctRepo.Update(updateCommPct);

                    Logger.DebugFormat("DeleteLkCommType() - Ended soft delete Comm Pct.");
                }

                //GroupCommPct
                var getGroupCommPct = (from x in _msGroupCommPctRepo.GetAll()
                                       where x.commTypeID == Id && x.isComplete == true
                                       select x).ToList();

                foreach (var item in getGroupCommPct)
                {
                    var updateGroupCommPct = item.MapTo<MS_GroupCommPct>();

                    updateGroupCommPct.isComplete = false;

                    Logger.DebugFormat("DeleteLkCommType() - Start soft delete Group Comm Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msGroupCommPctRepo.Update(updateGroupCommPct);

                    Logger.DebugFormat("DeleteLkCommType() - Ended soft delete Group Comm Pct.");
                }

                //GroupCommPctNonStd
                var getGroupCommPctNonStd = (from x in _msGroupCommPctNonStdRepo.GetAll()
                                             where x.commTypeID == Id && x.isComplete == true
                                             select x).ToList();

                foreach (var item in getGroupCommPctNonStd)
                {
                    var updateGroupCommPctNonStd = item.MapTo<MS_GroupCommPctNonStd>();

                    updateGroupCommPctNonStd.isComplete = false;

                    Logger.DebugFormat("DeleteLkCommType() - Start soft delete Group Comm Pct Non STD. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msGroupCommPctNonStdRepo.Update(updateGroupCommPctNonStd);

                    Logger.DebugFormat("DeleteLkCommType() - Ended soft delete Group Comm Pct Non STD.");
                }

                //CommType
                var getLkCommType = (from A in _lkCommTypeRepo.GetAll()
                                     where A.Id == Id
                                     select A).FirstOrDefault();

                var update = getLkCommType.MapTo<LK_CommType>();

                update.isComplete = false;

                try
                {
                    Logger.DebugFormat("DeleteLkCommType() - Start soft delete LK Comm Type. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _lkCommTypeRepo.Update(update);

                    Logger.DebugFormat("DeleteLkCommType() - Ended soft delete LK Comm Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteLkCommType() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteLkCommType() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("DeleteLkCommType() - Finished.");
        }

        public void DeleteMsCommPct(int Id, string flag)
        {
            Logger.Info("DeleteMsCommPct() - Started.");

            //delete-add
            if (flag == "add")
            {
                try
                {
                    Logger.DebugFormat("DeleteMsCommPct() - Start hard delete Comm Pct. Parameters sent:{0}" +
                        "commPctID = {1}{0}"
                        , Environment.NewLine, Id);

                    _msCommPctRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try

                    Logger.DebugFormat("DeleteMsCommPct() - Ended hard delete LK Point Type.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsCommPct() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsCommPct() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            //delete-edit
            else
            {
                //CommPct
                var getCommPct = (from x in _msCommPctRepo.GetAll()
                                  where x.Id == Id && x.isComplete == true
                                  select x).FirstOrDefault();

                var updateCommPct = getCommPct.MapTo<MS_CommPct>();

                updateCommPct.isComplete = false;

                try
                {
                    Logger.DebugFormat("DeleteMsCommPct() - Start soft delete Comm Pct. Parameters sent:{0}" +
                        "isComplete = {1}{0}"
                        , Environment.NewLine, false);

                    _msCommPctRepo.Update(updateCommPct);

                    Logger.DebugFormat("DeleteMsCommPct() - Ended soft delete Comm Pct.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsCommPct() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsCommPct() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("DeleteMsCommPct() - Finished.");
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
    }
}
