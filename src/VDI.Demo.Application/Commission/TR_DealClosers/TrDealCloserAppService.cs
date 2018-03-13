using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.TR_DealClosers.Dto;
using VDI.Demo.NewCommDB;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using Abp.UI;
using System.Data;
using Abp.AutoMapper;
using VDI.Demo.Commission.TR_SoldUnits;
using VDI.Demo.Commission.TR_SoldUnits.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;

namespace VDI.Demo.Commission.TR_DealClosers
{
    public class TrDealCloserAppService : DemoAppServiceBase, ITrDealCloserAppService
    {
        private readonly IRepository<PERSONALS_MEMBER, string> _personalMemberRepo;
        private readonly IRepository<PERSONALS, string> _personalRepo;
        private readonly IRepository<TR_SoldUnit> _trSoldUnitRepo;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<TR_SoldUnit> _soldUnitRepo;
        private readonly IRepository<MS_Unit> _unitRepo;
        private readonly IRepository<MS_Project> _projectRepo;
        private readonly IRepository<MS_Cluster> _clusterRepo;
        private readonly IRepository<TR_SoldUnitRequirement> _soldUnitRequirementRepo;
        private readonly IRepository<TR_CommPct> _trCommPctRepo;
        private readonly ITrSoldUnitAppService _trSoldUnitAppService;
        private readonly IRepository<MS_Developer_Schema> _msDeveloperSchemaRepo;
        private readonly IRepository<MS_Schema> _msSchemaRepo;
        private readonly IRepository<MS_Property> _msPropertyRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;

        public TrDealCloserAppService(
            IRepository<PERSONALS_MEMBER, string> personalMemberRepo,
            IRepository<PERSONALS, string> personalRepo,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<TR_SoldUnit> trSoldUnitRepo,
            IRepository<TR_SoldUnit> soldUnitRepo,
            IRepository<MS_Unit> unitRepo,
            IRepository<MS_Project> projectRepo,
            IRepository<MS_Cluster> clusterRepo,
            IRepository<TR_SoldUnitRequirement> soldUnitRequirementRepo,
            IRepository<TR_CommPct> trCommPctRepo,
            IRepository<MS_Developer_Schema> msDeveloperSchemaRepo,
            IRepository<MS_Schema> msSchemaRepo,
            IRepository<MS_Property> msPropertyRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            ITrSoldUnitAppService trSoldUnitAppService
        )
        {
            _msUnitCodeRepo = msUnitCodeRepo;
            _msUnitRepo = msUnitRepo;
            _msSchemaRepo = msSchemaRepo;
            _msDeveloperSchemaRepo = msDeveloperSchemaRepo;
            _personalMemberRepo = personalMemberRepo;
            _personalRepo = personalRepo;
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _trSoldUnitRepo = trSoldUnitRepo;
            _trCommPctRepo = trCommPctRepo;
            _soldUnitRepo = soldUnitRepo;
            _unitRepo = unitRepo;
            _projectRepo = projectRepo;
            _clusterRepo = clusterRepo;
            _soldUnitRequirementRepo = soldUnitRequirementRepo;
            _msPropertyRepo = msPropertyRepo;
            _trSoldUnitAppService = trSoldUnitAppService;
        }

        public List<GetTasklistDealCloserByProjectListDto> GetTasklistDealCloserByProject(int ProjectId)
        {
            var statusNew = "New";
            var dataSoldUnit = (from A in _soldUnitRepo.GetAll()
                                join E in _msDeveloperSchemaRepo.GetAll() on A.developerSchemaID equals E.Id
                                join F in _msPropertyRepo.GetAll() on E.propertyID equals F.Id
                                where !A.holdDate.HasValue &&
                                !A.cancelDate.HasValue
                                select new
                                {
                                    soldUnitId = A.Id,
                                    memberCode = A.memberCode,
                                    propCode = F.propCode,
                                    devCode = E.devCode,
                                    bookingCode = A.bookNo,
                                    unitCode = A.roadCode,
                                    unitNo = A.unitNo,
                                    status = statusNew,
                                    unitID = A.unitID
                                }).ToList();

            var tasklistDealCloserByProject = (from A in dataSoldUnit
                                               join B in _unitRepo.GetAll() on A.unitID equals B.Id
                                               join C in _projectRepo.GetAll() on B.projectID equals C.Id
                                               join D in _clusterRepo.GetAll() on B.clusterID equals D.Id
                                               where C.Id == ProjectId
                                               select new GetTasklistDealCloserByProjectListDto
                                               {
                                                   soldUnitId = A.soldUnitId,
                                                   projectName = C.projectName,
                                                   memberCode = A.memberCode,
                                                   propCode = A.propCode,
                                                   devCode = A.devCode,
                                                   bookingCode = A.bookingCode,
                                                   clusterName = D.clusterName,
                                                   unitCode = A.unitCode,
                                                   unitNo = A.unitNo,
                                                   status = statusNew
                                               }).ToList();

            foreach (var a in tasklistDealCloserByProject)
            {
                var unitRequirements = (from E in _soldUnitRequirementRepo.GetAll()
                                        where E.bookNo == a.bookingCode

                                        select new GetSoldUnitRequirementInSoldUnitListDto
                                        {
                                            bookNo = E.bookNo,
                                            processDate = E.processDate
                                        }).ToList();

                foreach (var b in unitRequirements)
                {
                    if (b.processDate.HasValue)
                    {
                        a.status = "Not New";
                    }
                }
            }
            return tasklistDealCloserByProject.Where(x => x.status == statusNew).ToList();
        }

        public List<GetMemberFromPersonalListDto> GetDropdownMemberFromPersonal()
        {
            var dataMember = (from A in _personalMemberRepo.GetAll()
                              join B in _personalRepo.GetAll() on A.psCode equals B.psCode
                              select new GetMemberFromPersonalListDto
                              {
                                  psCode = A.psCode,
                                  memberCode = A.memberCode,
                                  memberName = B.name
                              }).ToList();

            return dataMember;
        }

        public void UpdateDealCloser(MemberFromPersonalInputDto input, int limitAsUplineNo)
        {
            Logger.Info("UpdateDealCloser() - Started.");

            Logger.DebugFormat("UpdateDealCloser() - Start get data before update Deal Closer. Parameters sent:{0}" +
                        "bookNo = {1}{0}"
                        , Environment.NewLine, input.bookNo);

            var getDealCloser = (from A in _trSoldUnitRepo.GetAll()
                                 where A.bookNo == input.bookNo
                                 select A).FirstOrDefault();

            Logger.DebugFormat("UpdateDealCloser() - Ended get data before update Deal Closer. Result = {0}", getDealCloser);

            if (getDealCloser != null)
            {
                Logger.DebugFormat("UpdateDealCloser() - Start get data Member before update Deal Closer. Parameters sent:{0}" +
                        "memberCode = {1}{0}"
                        , Environment.NewLine, input.memberCode);

                var getDataMember = (from A in _personalMemberRepo.GetAll()
                                     where A.memberCode == input.memberCode
                                     select A).FirstOrDefault();

                Logger.DebugFormat("UpdateDealCloser() - Ended get data Member before update Deal Closer. Result = {0}", getDataMember);

                Logger.DebugFormat("UpdateDealCloser() - Start get data Unit ID before update Deal Closer. Parameters sent:{0}" +
                        "unitNo = {1}{0}" +
                        "unitCode = {2}{0}"
                        , Environment.NewLine, getDealCloser.unitNo, getDealCloser.roadCode);

                var unitID = (from x in _msUnitRepo.GetAll()
                              join a in _msUnitCodeRepo.GetAll() on x.unitCodeID equals a.Id
                              where x.unitNo == getDealCloser.unitNo && a.unitCode == getDealCloser.roadCode
                              select x.Id).FirstOrDefault();

                Logger.DebugFormat("UpdateDealCloser() - Ended get data Unit ID before update Deal Closer. Result = {0}", unitID);

                Logger.DebugFormat("UpdateDealCloser() - Start get data TR Booking Header before update Deal Closer. Parameters sent:{0}" +
                        "unitID = {1}{0}"
                        , Environment.NewLine, unitID);

                var getTrBookingHeader = (from A in _trBookingHeaderRepo.GetAll()
                                          where A.unitID == unitID
                                          select A).FirstOrDefault();

                Logger.DebugFormat("UpdateDealCloser() - Ended get data TR Booking Header before update Deal Closer. Result = {0}", getTrBookingHeader);

                Logger.DebugFormat("UpdateDealCloser() - Start get data Schema ID before update Deal Closer. Parameters sent:{0}" +
                        "scmCode = {1}{0}"
                        , Environment.NewLine, getDataMember.scmCode);

                var getIDSchema = (from A in _msSchemaRepo.GetAll()
                                   where getDataMember.scmCode == A.scmCode
                                   select A.Id).FirstOrDefault();

                Logger.DebugFormat("UpdateDealCloser() - Ended get data Schema ID before update Deal Closer. Result = {0}", getIDSchema);

                //Update TR_BookingHeader
                var updateTrBookingHeader = getTrBookingHeader.MapTo<TR_BookingHeader>();
                updateTrBookingHeader.schemaID = getIDSchema;
                updateTrBookingHeader.memberCode = input.memberCode;
                updateTrBookingHeader.remarks = input.reason;
                //Update TR_SoldUnit
                var updateDealCloser = getDealCloser.MapTo<TR_SoldUnit>();
                updateDealCloser.memberCode = input.memberCode;
                updateDealCloser.Remarks = input.reason;
                updateDealCloser.schemaID = getIDSchema;
                //Insert TR_CommPct
                GetDataMemberUplineInsertInputDto inputTrCommPct = new GetDataMemberUplineInsertInputDto();
                inputTrCommPct.bookNo = input.bookNo;
                inputTrCommPct.devCode = input.devCode;
                inputTrCommPct.developerSchemaID = input.developerSchemaID;
                inputTrCommPct.memberCode = input.memberCode;

                var dataUpline = _trSoldUnitAppService.GetDataMemberUplineInsert(inputTrCommPct, limitAsUplineNo);

                if (dataUpline.Any())
                {
                    var dataUplineExist = dataUpline.Select(x => new TR_CommPct
                    {
                        bookNo = x.bookNo,
                        developerSchemaID = x.developerSchemaID,
                        asUplineNo = (short)x.asUplineNo,
                        commPctPaid = (double)x.commPctPaid,
                        commTypeID = x.commTypeID,
                        memberCodeN = x.memberCodeN,
                        memberCodeR = x.memberCodeR,
                        pointTypeID = x.pointTypeID,
                        pphRangeID = x.pphRangeID,
                        pphRangeInsID = x.pphRangeInsID,
                        statusMemberID = x.statusMemberID
                    }).ToList();

                    try
                    {
                        Logger.DebugFormat("UpdateDealCloser() - Start update TR Booking Header. Parameters sent:{0}" +
                                            "scmCode = {1}{0}" +
                                            "memberCode = {2}{0}" +
                                            "remarks = {3}{0}"
                                            , Environment.NewLine, getDataMember.scmCode, input.memberCode, input.reason);

                        _trBookingHeaderRepo.Update(updateTrBookingHeader);

                        Logger.DebugFormat("UpdateDealCloser() - Ended update TR Booking Header.");

                        Logger.DebugFormat("UpdateDealCloser() - Start update TR Sold Unit. Parameters sent:{0}" +
                                            "memberCode = {1}{0}" +
                                            "Remarks = {2}{0}" +
                                            "schemaID = {3}{0}"
                                            , Environment.NewLine, input.memberCode, input.reason, getIDSchema);

                        _trSoldUnitRepo.Update(updateDealCloser);

                        Logger.DebugFormat("UpdateDealCloser() - Ended update TR Sold Unit.");

                        var insertData = dataUplineExist.MapTo<List<TR_CommPct>>();
                        //_trCommPctRepo.BulkInsertOrUpdate(insertData);

                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("UpdateDealCloser() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("UpdateDealCloser() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("UpdateDealCloser() - ERROR. Result = {0}", "Member with commission not exist !");
                    throw new UserFriendlyException("Member with commission not exist !");
                }


            }
            Logger.Info("UpdateDealCloser() - Finished.");
        }

        public GetDataEditDealCloserListDto GetDataEditDealCloser(string bookNo)
        {
            var dataSoldUnit = (from A in _soldUnitRepo.GetAll()
                                join E in _msDeveloperSchemaRepo.GetAll() on A.developerSchemaID equals E.Id
                                join F in _msPropertyRepo.GetAll() on E.propertyID equals F.Id
                                where A.bookNo == bookNo
                                select new
                                {
                                    propCode = F.propCode,
                                    devCode = E.devCode,
                                    bookingCode = A.bookNo,
                                    unitCode = A.roadCode,
                                    unitNo = A.unitNo,
                                    unitPrice = A.unitPrice,
                                    ppjbDate = A.PPJBDate,
                                    changeDealClosureReason = A.changeDealClosureReason,
                                    memberCode = A.memberCode,
                                    developerSchemaId = A.developerSchemaID,
                                    unitID = A.unitID,
                                }).ToList();

            var dataDealCloser = (from A in dataSoldUnit
                                  join B in _unitRepo.GetAll() on A.unitID equals B.Id
                                  join C in _projectRepo.GetAll() on B.projectID equals C.Id
                                  join D in _clusterRepo.GetAll() on B.clusterID equals D.Id
                                  select new GetDataEditDealCloserListDto
                                  {
                                      propCode = A.propCode,
                                      devCode = A.devCode,
                                      bookingCode = A.bookingCode,
                                      unitCode = A.unitCode,
                                      unitNo = A.unitNo,
                                      unitPrice = A.unitPrice,
                                      ppjbDate = A.ppjbDate,
                                      changeDealClosureReason = A.changeDealClosureReason,
                                      memberCode = A.memberCode,
                                      projectName = C.projectName,
                                      developerSchemaId = A.developerSchemaId
                                  }).FirstOrDefault();

            if (dataDealCloser != null)
            {
                var unitID = (from x in _msUnitRepo.GetAll()
                              join a in _msUnitCodeRepo.GetAll() on x.unitCodeID equals a.Id
                              where x.unitNo == dataDealCloser.unitCode && a.unitCode == dataDealCloser.unitNo
                              select x.Id).FirstOrDefault();

                var getTrBookingHeader = (from A in _trBookingHeaderRepo.GetAll()
                                          where A.unitID == unitID
                                          select A).FirstOrDefault();

                var getPersonalsMember = (from A in _personalMemberRepo.GetAll()
                                          where A.memberCode == dataDealCloser.memberCode
                                          select A).FirstOrDefault();

                var getPersonal = (from A in _personalRepo.GetAll()
                                   where A.psCode == getPersonalsMember.psCode
                                   select A).FirstOrDefault();

                dataDealCloser.termRemarks = getTrBookingHeader.termRemarks;
                dataDealCloser.name = getPersonal.name;
            }

            return dataDealCloser;
        }
    }
}
