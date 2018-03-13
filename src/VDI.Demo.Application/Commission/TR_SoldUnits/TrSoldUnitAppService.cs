using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.TR_SoldUnits.Dto;
using VDI.Demo.NewCommDB;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.Pricing;
using Abp.UI;
using Abp.AutoMapper;
using System.Data;
using VDI.Demo.Helper;
using VDI.Demo.PropertySystemDB.LippoMaster;
using Abp.Application.Services.Dto;
using VDI.Demo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Abp.Domain.Uow;
using Visionet_Backend_NetCore.Komunikasi;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VDI.Demo.Commission.TR_SoldUnits
{
    public class TrSoldUnitAppService : DemoAppServiceBase, ITrSoldUnitAppService
    {
        private readonly IRepository<TR_SoldUnit> _trSoldUnitRepo;
        private readonly IRepository<TR_SoldUnitRequirement> _trSoldUnitRequirementRepo;
        private readonly IRepository<TR_CommPayment> _trCommPaymentRepo;
        private readonly IRepository<MS_Schema> _msSchemaRepo;
        private readonly IRepository<MS_GroupSchema> _msGroupSchemaRepo;
        private readonly IRepository<MS_SchemaRequirement> _msSchemaRequirementRepo;
        private readonly IRepository<MS_Unit> _msUnitRepo;
        private readonly IRepository<MS_UnitCode> _msUnitCodeRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MS_Cluster> _msClusterRepo;
        private readonly IRepository<MS_Developer_Schema> _msDeveloperSchemaRepo;
        private readonly IRepository<MS_Property> _msPropertyRepo;
        private readonly IRepository<PERSONALS, string> _personalRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalsMemberRepo;
        private readonly IRepository<LK_CommType> _lkCommTypeRepo;
        private readonly IRepository<TR_SoldUnitFlag> _trSoltUnitFlagRepo;
        private readonly IRepository<TR_BookingHeader> _trBookingHeaderRepo;
        private readonly IRepository<TR_CommPct> _trCommPctRepo;
        private readonly IRepository<LK_PointType> _lkPointTypeRepo;
        private readonly IRepository<TR_BookingDetail> _trBookingDetail;
        private readonly IRepository<MS_PointPct> _msPointPctRepo;
        private readonly IRepository<MS_StatusMember> _msStatusMemberRepo;
        private readonly IRepository<MS_GroupCommPct> _msGroupCommPctRepo;
        private readonly IRepository<MS_GroupCommPctNonStd> _msGroupCommPctNonStdRepo;
        private readonly IRepository<MS_CommPct> _msCommPctRepo;
        private readonly IRepository<MS_PPhRange> _msPphRangeRepo;
        private readonly IRepository<MS_PPhRangeIns> _msPphRangeInsRepo;
        private readonly IRepository<MS_BobotComm> _msBobotCommRepo;
        private readonly IRepository<TR_BookingDocument> _trBookingDocRepo;
        private readonly IRepository<MS_Term> _msTermRepo;
        private readonly IRepository<MS_GroupSchemaRequirement> _msGroupSchemaRequirementRepo;
        private readonly IRepository<TR_PaymentDetailAlloc> _trPaymentDetailAllocRepo;
        private readonly IRepository<MS_Country> _msCountryRepo;
        private readonly NewCommDbContext _context;
        private readonly PropertySystemDbContext _contextProp;

        public TrSoldUnitAppService(
            IRepository<TR_SoldUnit> trSoldUnitRepo,
            IRepository<TR_SoldUnitRequirement> trSoldUnitRequirementRepo,
            IRepository<TR_CommPayment> trCommPaymentRepo,
            IRepository<MS_Schema> msSchemaRepo,
            IRepository<MS_GroupSchema> msGroupSchemaRepo,
            IRepository<MS_SchemaRequirement> msSchemaRequirementRepo,
            IRepository<MS_Unit> msUnitRepo,
            IRepository<MS_UnitCode> msUnitCodeRepo,
            IRepository<MS_Project> msProjectRepo,
            IRepository<MS_Cluster> msClusterRepo,
            IRepository<MS_Developer_Schema> msDeveloperSchemaRepo,
            IRepository<MS_Property> msPropertyRepo,
            IRepository<PERSONALS, string> personalRepo,
            IRepository<PERSONALS_MEMBER, string> personalsMemberRepo,
            IRepository<LK_CommType> lkCommTypeRepo,
            IRepository<TR_SoldUnitFlag> trSoltUnitFlagRepo,
            IRepository<TR_BookingHeader> trBookingHeaderRepo,
            IRepository<TR_CommPct> trCommPctRepo,
            IRepository<LK_PointType> lkPointTypeRepo,
            IRepository<MS_PointPct> msPointPct,
            IRepository<TR_BookingDetail> trBookingDetail,
            IRepository<MS_PointPct> msPointPctRepo,
            IRepository<MS_StatusMember> msStatusMember,
            IRepository<MS_GroupCommPct> msGroupCommPctRepo,
            IRepository<MS_GroupCommPctNonStd> msGroupCommPctNonStdRepo,
            IRepository<MS_CommPct> msCommPctRepo,
            IRepository<MS_PPhRange> msPphRangeRepo,
            IRepository<MS_PPhRangeIns> msPphRangeInsRepo,
            IRepository<MS_BobotComm> msBobotCommRepo,
            IRepository<TR_BookingDocument> trBookingDocRepo,
            IRepository<MS_Term> msTermRepo,
            IRepository<MS_GroupSchemaRequirement> msGroupSchemaRequirementRepo,
            IRepository<TR_PaymentDetailAlloc> trPaymentDetailAllocRepo,
            IRepository<MS_Country> msCountryRepo,
            NewCommDbContext context,
            PropertySystemDbContext contextProp
        )
        {
            _msTermRepo = msTermRepo;
            _trBookingDocRepo = trBookingDocRepo;
            _msBobotCommRepo = msBobotCommRepo;
            _trBookingDetail = trBookingDetail;
            _trBookingHeaderRepo = trBookingHeaderRepo;
            _trSoltUnitFlagRepo = trSoltUnitFlagRepo;
            _trSoldUnitRepo = trSoldUnitRepo;
            _trSoldUnitRequirementRepo = trSoldUnitRequirementRepo;
            _msSchemaRepo = msSchemaRepo;
            _msGroupSchemaRepo = msGroupSchemaRepo;
            _msSchemaRequirementRepo = msSchemaRequirementRepo;
            _msUnitRepo = msUnitRepo;
            _msUnitCodeRepo = msUnitCodeRepo;
            _msProjectRepo = msProjectRepo;
            _msClusterRepo = msClusterRepo;
            _msDeveloperSchemaRepo = msDeveloperSchemaRepo;
            _msPropertyRepo = msPropertyRepo;
            _trCommPaymentRepo = trCommPaymentRepo;
            _personalRepo = personalRepo;
            _personalsMemberRepo = personalsMemberRepo;
            _lkCommTypeRepo = lkCommTypeRepo;
            _trCommPctRepo = trCommPctRepo;
            _lkPointTypeRepo = lkPointTypeRepo;
            _msPointPctRepo = msPointPctRepo;
            _msStatusMemberRepo = msStatusMember;
            _msGroupCommPctRepo = msGroupCommPctRepo;
            _msGroupCommPctNonStdRepo = msGroupCommPctNonStdRepo;
            _msCommPctRepo = msCommPctRepo;
            _msPphRangeRepo = msPphRangeRepo;
            _msPphRangeInsRepo = msPphRangeInsRepo;
            _msGroupSchemaRequirementRepo = msGroupSchemaRequirementRepo;
            _trPaymentDetailAllocRepo = trPaymentDetailAllocRepo;
            _msCountryRepo = msCountryRepo;
            _context = context;
            _contextProp = contextProp;
        }

        public GetDataDealCloserDto GetSetCommissionDataDealCloser(string bookNo)
        {
            var getDataMember = (from SU in _trSoldUnitRepo.GetAll()
                                 join S in _msSchemaRepo.GetAll() on SU.schemaID equals S.Id
                                 join CP in _trCommPctRepo.GetAll() on new { SU1 = SU.bookNo, SU2 = SU.memberCode } equals new { SU1 = CP.bookNo, SU2 = CP.memberCodeR }
                                 join CT in _lkCommTypeRepo.GetAll() on CP.commTypeID equals CT.Id
                                 join PT in _lkPointTypeRepo.GetAll() on CP.pointTypeID equals PT.Id into ps
                                 from PT in ps.DefaultIfEmpty()
                                 join PP in _msPointPctRepo.GetAll() on PT.Id equals PP.pointTypeID into pppt
                                 from PP in pppt.DefaultIfEmpty()
                                 join SM in _msStatusMemberRepo.GetAll() on CP.statusMemberID equals SM.Id
                                 where SU.bookNo == bookNo
                                 select new GetDataDealCloserDto
                                 {
                                     schemaID = S.Id,
                                     scmCode = S.scmCode,
                                     scmName = S.scmName,
                                     commTypeName = CT.commTypeName,
                                     memberCode = SU.memberCode,
                                     statusCode = SM.statusName,
                                     commission = CP.commPctPaid * 100,
                                     pointType = PT == null ? null : PT.pointTypeName,
                                     pointPct = PT == null ? 0 : PP.pointPct
                                 }
                                 ).FirstOrDefault();

            return getDataMember;
        }

        public List<GetDataSchemaRequirementListDto> GetDataSchemaRequirement(string bookNo)
        {
            var dataPrepReq = (from A in _trSoldUnitRequirementRepo.GetAll()
                               where A.bookNo == bookNo
                               select new GetDataSchemaRequirementListDto
                               {
                                   reqNo = A.reqNo,
                                   reqName = A.reqDesc,
                                   paid = A.pctPaid,
                                   processDate = A.processDate
                               }).ToList();

            if (!dataPrepReq.Any())
            {
                Logger.Warn("BookNo doesn't exist in the Sold Unit");
                throw new UserFriendlyException("BookNo doesn't exist in the Sold Unit");
            }

            return dataPrepReq;
        }

        public void HoldSoldUnit(string bookNo, string holdReason)
        {
            Logger.Info("HoldSoldUnit() - Started.");

            var getDataSoldUnit = (from x in _trSoldUnitRepo.GetAll()
                                   where x.bookNo == bookNo
                                   select x).FirstOrDefault();

            var updateHoldSoldUnit = getDataSoldUnit.MapTo<TR_SoldUnit>();

            var getDate = DateTime.Now;
            var flagInsert = false;
            //check data holdDate - decide to hold or unhold
            if (getDataSoldUnit.holdDate != null)
            {
                updateHoldSoldUnit.holdDate = null;
                flagInsert = false;
            }
            else
            {
                updateHoldSoldUnit.holdDate = getDate;
                updateHoldSoldUnit.holdReason = holdReason;
                flagInsert = true;
            }

            var createMsGroupCommPct = new TR_SoldUnitFlag()
            {
                entityID = 1,
                bookNo = getDataSoldUnit.bookNo,
                flagCode = "1", //hardcode
                flagDate = getDate,
                remarks = holdReason,
                flagID = 1
            };


            try
            {
                Logger.DebugFormat("HoldSoldUnit() - Start update TR_SoldUnit. Params sent:{0}" +
                    "holdDate       = {1}{0}" +
                    "holdReason     = {2}{0}" +
                    "flagInsert     = {3}",
                    Environment.NewLine, getDate, holdReason, true);
                _trSoldUnitRepo.Update(updateHoldSoldUnit);
                Logger.DebugFormat("HoldSoldUnit() - End update TR_SoldUnit.");

                if (flagInsert)
                {
                    Logger.DebugFormat("HoldSoldUnit() - Start insert TR_SoldUnitFlag. Params sent:{0}" +
                    "entityID   = {1}{0}" +
                    "bookNo     = {2}{0}" +
                    "flagCode   = {3}{0}" +
                    "flagDate   = {4}{0}" +
                    "remarks    = {5}{0}" +
                    "flagID     = {6}",
                    Environment.NewLine, 1, getDataSoldUnit.bookNo, "1", getDate, holdReason, 1);
                    _trSoltUnitFlagRepo.Insert(createMsGroupCommPct);
                    Logger.DebugFormat("HoldSoldUnit() - End insert TR_SoldUnitFlag.");
                }
                CurrentUnitOfWork.SaveChanges();
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("HoldSoldUnit() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("HoldSoldUnit() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("HoldSoldUnit() - Finished.");
        }

        public string GetMemberByUnit(string roadCode, string unitNo)
        {
            var unitID = (from x in _msUnitRepo.GetAll()
                          join a in _msUnitCodeRepo.GetAll() on x.unitCodeID equals a.Id
                          where x.unitNo == unitNo && a.unitCode == roadCode
                          select x.Id).FirstOrDefault();

            var dataMember = (from x in _trBookingHeaderRepo.GetAll()
                              where x.unitID == unitID
                              select x.memberCode).FirstOrDefault();

            if (dataMember == null)
            {
                throw new UserFriendlyException("Unit doesn't exist in the booking header");
            }

            return dataMember;
        }

        public List<GetDataTasklistTRSoldUnitListDto> GetDataTasklistTRSoldUnit(int projectID)
        {
            var dataSoldUnit = (from soldUnit in _trSoldUnitRepo.GetAll()
                                join schema in _msSchemaRepo.GetAll() on soldUnit.schemaID equals schema.Id
                                join dev in _msDeveloperSchemaRepo.GetAll() on soldUnit.developerSchemaID equals dev.Id
                                join prop in _msPropertyRepo.GetAll() on dev.propertyID equals prop.Id
                                select new
                                {
                                    soldUnit.Id,
                                    soldUnit.unitID,
                                    soldUnit.bookNo,
                                    soldUnit.bookDate,
                                    schema.dueDateComm,
                                    dev.devName,
                                    prop.propName
                                }).ToList();

            var dataTasklist = (from resultSoldUnit in dataSoldUnit
                                join unit in _msUnitRepo.GetAll() on resultSoldUnit.unitID equals unit.Id
                                join unitCode in _msUnitCodeRepo.GetAll() on unit.unitCodeID equals unitCode.Id
                                join project in _msProjectRepo.GetAll() on unit.projectID equals project.Id
                                join cluster in _msClusterRepo.GetAll() on unit.clusterID equals cluster.Id
                                where unit.projectID == projectID
                                select new GetDataTasklistTRSoldUnitListDto
                                {
                                    soldUnitID = resultSoldUnit.Id,
                                    unitNo = unit.unitNo,
                                    unitCode = unitCode.unitCode,
                                    bookNo = resultSoldUnit.bookNo,
                                    bookDate = resultSoldUnit.bookDate,
                                    dueDateComm = resultSoldUnit.dueDateComm,
                                    clusterName = cluster.clusterName,
                                    projectName = project.projectName,
                                    developerName = resultSoldUnit.devName,
                                    propName = resultSoldUnit.propName
                                }).ToList();

            foreach (var item in dataTasklist)
            {

                var cekSoldUnit = (from a in _trSoldUnitRepo.GetAll()
                                   where item.bookNo == a.bookNo
                                   select a).FirstOrDefault();

                if (cekSoldUnit.cancelDate != null)
                {
                    item.status = "Cancel";
                }
                else if (cekSoldUnit.holdDate != null)
                {
                    item.status = "Hold";
                }
                else
                {
                    int i = 0;
                    int j = 0;
                    var cekCommPayment = new List<TR_CommPayment>();
                    var cekSoldUnitReq = (from b in _trSoldUnitRequirementRepo.GetAll()
                                          where item.bookNo == b.bookNo
                                          select b).ToList();


                    foreach (var req in cekSoldUnitReq)
                    {
                        cekCommPayment = (from c in _trCommPaymentRepo.GetAll()
                                          where item.bookNo == c.bookNo && req.reqNo == c.reqNo
                                          select c).ToList();

                        if (req.processDate != null && cekCommPayment.Any())
                        {
                            i++;
                            j++;
                        }
                    }

                    if (cekSoldUnitReq.Any())
                    {
                        if (cekSoldUnitReq.Count == i && cekCommPayment.Count == j)
                        {
                            item.status = "Paid";
                        }
                        else if (i >= 1 && cekSoldUnitReq.Count != i && cekCommPayment.Count != j)
                        {
                            item.status = "Paid Uncomplete";
                        }
                        else
                        {
                            item.status = "New";
                        }
                    }
                    else
                    {
                        item.status = "New";
                    }
                }
            }

            return dataTasklist;
        }

        public List<GetDataAllMemberListDto> GetSetCommissionDataMember(string bookNo)
        {

            //Query To NewComm
            var queryGetDataNewComm = (from CP in _trCommPctRepo.GetAll()
                                       join SUR in _trSoldUnitRequirementRepo.GetAll() on CP.bookNo equals SUR.bookNo
                                       join SU in _trSoldUnitRepo.GetAll() on CP.bookNo equals SU.bookNo
                                       join CT in _lkCommTypeRepo.GetAll() on CP.commTypeID equals CT.Id
                                       join SC in _msSchemaRepo.GetAll() on SU.schemaID equals SC.Id
                                       where CP.bookNo == bookNo
                                       select new
                                       {
                                           asUplineNo = CP.asUplineNo,
                                           memberCode = CP.memberCodeR,
                                           commPctPaid = CP.commPctPaid,
                                           unitPrice = SU.unitPrice,
                                           actualPctComm = SUR.pctPaid, //hardcode
                                           pctPaid = SUR.pctPaid,
                                           reqDate = SUR.reqDate,
                                           prodDate = SUR.processDate,
                                           bookNo = CP.bookNo,
                                           commTypeCode = CT.commTypeCode,
                                           commTypeID = CT.Id,
                                           schemaID = SU.schemaID,
                                           scmCode = SC.scmCode
                                       }).ToList();

            var getData = new List<GetDataAllMemberListDto>();

            if (!queryGetDataNewComm.Any())
            {
                return getData;
            }

            var getDataNewComm = queryGetDataNewComm.Select(x => new
            {
                asUplineNo = x.asUplineNo,
                memberCode = x.memberCode,
                commPctPaid = x.commPctPaid * 100,
                commPaid = x.unitPrice * (decimal)x.commPctPaid,
                unitPrice = x.unitPrice,
                actualPctComm = x.actualPctComm * 100,
                actualPaid = (x.unitPrice * (decimal)x.commPctPaid) * (decimal)x.pctPaid,
                pctPaid = x.pctPaid,
                reqDate = x.reqDate,
                prodDate = x.prodDate,
                bookNo = x.bookNo,
                commTypeCode = x.commTypeCode,
                commTypeID = x.commTypeID,
                schemaID = x.schemaID,
                scmCode = x.scmCode
            }).ToList();

            //From NewComm Join To Personals
            getData = (from A in getDataNewComm
                       join PM in _personalsMemberRepo.GetAll() on A.memberCode equals PM.memberCode
                       join P in _personalRepo.GetAll() on PM.psCode equals P.psCode
                       where A.scmCode == PM.scmCode
                       select new GetDataAllMemberListDto
                       {
                           asUplineNo = A.asUplineNo,
                           memberCode = A.memberCode,
                           memberName = P.name,
                           commPctPaid = A.commPctPaid,
                           commPaid = A.commPaid,
                           actualPctComm = A.prodDate == null ? 0 : A.actualPctComm,
                           actualPaid = A.prodDate == null ? 0 : A.actualPaid,
                           reqDate = A.reqDate,
                           prodDate = A.prodDate,
                           commTypeCode = A.commTypeCode,
                           commTypeID = A.commTypeID
                       }).ToList();

            getData = (from A in getData orderby A.asUplineNo, A.commPctPaid select A).ToList();

            return getData;
        }

        public GetDataSetCommissionUniversalListDto GetSetCommissionUniversal(string bookNo)
        {
            var dataResult = new GetDataSetCommissionUniversalListDto()
            {
                dataDealCloser = GetSetCommissionDataDealCloser(bookNo),
                dataRequirement = GetDataSchemaRequirement(bookNo),
                dataMember = GetSetCommissionDataMember(bookNo)
            };

            return dataResult;
        }

        public GetDataDefineUnitListDto GetDataDefineUnit(int soldUnitID)
        {
            //join newcomm
            var getSoldUnit = (from soldUnit in _trSoldUnitRepo.GetAll()
                               join dev in _msDeveloperSchemaRepo.GetAll() on soldUnit.developerSchemaID equals dev.Id
                               join prop in _msPropertyRepo.GetAll() on dev.propertyID equals prop.Id
                               where soldUnit.Id == soldUnitID
                               select new
                               {
                                   soldUnitID = soldUnit.Id,
                                   bookNo = soldUnit.bookNo,
                                   developerName = dev.devName,
                                   developerSchemaID = dev.Id,
                                   devCode = dev.devCode,
                                   propName = prop.propName,
                                   propCode = prop.propCode,
                                   hargaUnit = soldUnit.netNetPrice,
                                   PPJBDate = soldUnit.PPJBDate,
                                   term = soldUnit.termRemarks,
                                   unitID = soldUnit.unitID
                               }).ToList();

            //join Engine3
            var getDataResult = (from soldUnit in getSoldUnit
                                 join unit in _msUnitRepo.GetAll() on soldUnit.unitID equals unit.Id
                                 join project in _msProjectRepo.GetAll() on unit.projectID equals project.Id
                                 join unitCode in _msUnitCodeRepo.GetAll() on unit.unitCodeID equals unitCode.Id
                                 where soldUnit.soldUnitID == soldUnitID
                                 select new GetDataDefineUnitListDto
                                 {
                                     soldUnitID = soldUnit.soldUnitID,
                                     bookNo = soldUnit.bookNo,
                                     developerName = soldUnit.developerName,
                                     developerSchemaID = soldUnit.developerSchemaID,
                                     devCode = soldUnit.devCode,
                                     propName = soldUnit.propName,
                                     propCode = soldUnit.propCode,
                                     hargaUnit = soldUnit.hargaUnit,
                                     PPJBDate = soldUnit.PPJBDate,
                                     term = soldUnit.term
                                 }).FirstOrDefault();

            return getDataResult;
        }

        public List<GetDataPropertyByProjectListDto> GetDataPropertyByProject(int projectID)
        {
            var getProperty = (from groupSchema in _msGroupSchemaRepo.GetAll()
                               join property in _msPropertyRepo.GetAll() on groupSchema.schemaID equals property.schemaID
                               where groupSchema.projectID == projectID && groupSchema.isComplete == true && groupSchema.isActive == true
                               select new GetDataPropertyByProjectListDto
                               {
                                   propID = property.Id,
                                   propName = property.propName,
                                   propCode = property.propCode
                               }).Distinct().ToList();

            return getProperty;
        }

        private string GetNameMemberCode(string memberCode)
        {
            var dataMember = (from A in _personalsMemberRepo.GetAll()
                              join B in _personalRepo.GetAll() on A.psCode equals B.psCode
                              where A.memberCode == memberCode
                              select B.name).FirstOrDefault();

            return dataMember;
        }

        private int GetStatusMemberIDbyStatusCode(string statusCode)
        {
            var statusMemberID = (from A in _msStatusMemberRepo.GetAll()
                                  where A.statusCode == statusCode
                                  select A.Id).FirstOrDefault();

            return statusMemberID;
        }

        private int GetCommTypeIDByCommTypeCode(string scmCode, string commTypeCode)
        {
            var scmID = (from x in _msSchemaRepo.GetAll()
                         where x.scmCode == scmCode
                         select x.Id).FirstOrDefault();

            var commTypeID = (from A in _lkCommTypeRepo.GetAll()
                              where A.commTypeCode == commTypeCode && A.schemaID == scmID
                              select A.Id).FirstOrDefault();

            return commTypeID;
        }

        private int GetPointTypeIDByPointTypeCode(string scmCode, string pointTypeCode)
        {
            var scmID = (from x in _msSchemaRepo.GetAll()
                         where x.scmCode == scmCode
                         select x.Id).FirstOrDefault();
            var pointTypeID = (from A in _lkPointTypeRepo.GetAll()
                               where A.pointTypeCode == pointTypeCode && A.schemaID == scmID
                               select A.Id).FirstOrDefault();

            return pointTypeID;
        }

        private int GetPphRangeIdByScmCode(string scmCode)
        {
            var scmID = (from x in _msSchemaRepo.GetAll()
                         where x.scmCode == scmCode
                         select x.Id).FirstOrDefault();
            var pphRangeID = (from A in _msPphRangeRepo.GetAll()
                              where A.schemaID == scmID
                              select A.Id).FirstOrDefault();

            return pphRangeID;
        }

        private int GetPphRangeInsIdByScmCode(string scmCode)
        {
            var scmID = (from x in _msSchemaRepo.GetAll()
                         where x.scmCode == scmCode
                         select x.Id).FirstOrDefault();
            var pphRangeInsID = (from A in _msPphRangeInsRepo.GetAll()
                                 where A.schemaID == scmID
                                 select A.Id).FirstOrDefault();

            return pphRangeInsID;
        }

        public List<GetUplineByMemberCodeListDto> GetUplineByMemberCode(string memberCode, int countAsUplineNo)
        {
            List<GetUplineByMemberCodeListDto> dataMember = new List<GetUplineByMemberCodeListDto>();

            var dataDealCloser = (from A in _personalsMemberRepo.GetAll()
                                  join B in _personalRepo.GetAll() on A.psCode equals B.psCode
                                  where A.memberCode == memberCode
                                  select new GetUplineByMemberCodeListDto
                                  {
                                      ACDCode = A.ACDCode,
                                      scmCode = A.scmCode,
                                      memberCode = A.memberCode,
                                      memberName = B.name,
                                      parentMemberCode = A.parentMemberCode,
                                      asUplineNo = 0,
                                      memberStatusCode = A.memberStatusCode
                                  }).FirstOrDefault();

            if (dataDealCloser != null)
            {
                var statusMemberID = GetStatusMemberIDbyStatusCode(dataDealCloser.memberStatusCode);
                var parentMemberName = GetNameMemberCode(dataDealCloser.parentMemberCode);
                var scmCode = dataDealCloser.scmCode;
                var commTypeID = GetCommTypeIDByCommTypeCode(scmCode, "PRI");
                var pointTypeID = GetPointTypeIDByPointTypeCode(scmCode, "PRI");
                var pphRangeID = GetPphRangeIdByScmCode(dataDealCloser.scmCode);
                var pphRangeInsID = GetPphRangeInsIdByScmCode(dataDealCloser.scmCode);

                //AsUpline0
                dataDealCloser.parentMemberName = parentMemberName;
                dataDealCloser.memberStatusID = statusMemberID;
                dataDealCloser.commTypeID = commTypeID;
                dataDealCloser.commTypeCode = "PRI";
                dataDealCloser.pointTypeID = pointTypeID;
                dataDealCloser.pphRangeID = pphRangeID;
                dataDealCloser.pphRangeInsID = pphRangeInsID;
                dataMember.Add(dataDealCloser);

                int asUplineNo = 1;
                int j = 0;
                do
                {
                    if (j < countAsUplineNo)
                    {
                        var parentMemberCodeNextAsUplineNo = dataMember[j].parentMemberCode;

                        if (parentMemberCodeNextAsUplineNo != null && parentMemberCodeNextAsUplineNo != "")
                        {
                            var dataUpline = (from A in _personalsMemberRepo.GetAll()
                                              join B in _personalRepo.GetAll() on A.psCode equals B.psCode
                                              where A.memberCode == parentMemberCodeNextAsUplineNo
                                              select new GetUplineByMemberCodeListDto
                                              {
                                                  ACDCode = A.ACDCode,
                                                  scmCode = A.scmCode,
                                                  memberCode = A.memberCode,
                                                  memberName = B.name,
                                                  parentMemberCode = A.parentMemberCode,
                                                  asUplineNo = (short)asUplineNo,
                                                  memberStatusCode = A.memberStatusCode
                                              }).FirstOrDefault();

                            //AsUpline seterusnya
                            var statusMemberIdNextAsUplineNo = GetStatusMemberIDbyStatusCode(dataUpline.memberStatusCode);
                            var parentMemberNameNextAsUplineNo = GetNameMemberCode(parentMemberCodeNextAsUplineNo);
                            var scmCodeNextAsUplineNo = dataUpline.scmCode;
                            var commTypeIdNextAsUplineNo = GetCommTypeIDByCommTypeCode(scmCodeNextAsUplineNo, "TIM");
                            var pointTypeIdNextAsUplineNo = GetPointTypeIDByPointTypeCode(scmCodeNextAsUplineNo, "TIM");
                            var pphRangeIdNextAsUplineNo = GetPphRangeIdByScmCode(dataUpline.scmCode);
                            var pphRangeInsIdNextAsUplineNo = GetPphRangeInsIdByScmCode(dataUpline.scmCode);

                            dataUpline.memberStatusID = statusMemberIdNextAsUplineNo;
                            dataUpline.parentMemberName = parentMemberNameNextAsUplineNo;
                            dataUpline.commTypeID = commTypeIdNextAsUplineNo;
                            dataUpline.commTypeCode = "TIM";
                            dataUpline.pointTypeID = pointTypeIdNextAsUplineNo;
                            dataUpline.pphRangeID = pphRangeIdNextAsUplineNo;
                            dataUpline.pphRangeInsID = pphRangeInsIdNextAsUplineNo;
                            dataMember.Add(dataUpline);
                        }

                        asUplineNo++;
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
                while (j < dataMember.Count);
            }
            else
            {
                throw new UserFriendlyException("Member doesn't exist in the personals member");
            }

            return dataMember;
        }

        public void UpdateAdjustment(string bookNo, string memberCode, double commPctPaid)
        {
            Logger.Info("UpdateAdjustment() - Started.");

            var getTrCommPct = (from trCommPct in _trCommPctRepo.GetAll()
                                where bookNo == trCommPct.bookNo && memberCode == trCommPct.memberCodeR
                                select trCommPct).FirstOrDefault();

            var updateTrCommPct = getTrCommPct.MapTo<TR_CommPct>();

            updateTrCommPct.commPctPaid = commPctPaid;

            try
            {
                Logger.DebugFormat("UpdateAdjustment() - Start update adjustment(TR_CommPct). Params sent:{0}" +
                    "commPctPaid = {1}"
                    , Environment.NewLine, commPctPaid);
                _trCommPctRepo.Update(updateTrCommPct);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                Logger.DebugFormat("UpdateAdjustment() - End update adjustment(TR_CommPct).");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("UpdateAdjustment() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdateAdjustment() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("UpdateAdjustment() - Finished.");
        }

        public List<TR_SoldUnitListDto> GetDataTrxToInsert(string unitCode, string unitNo) //passed
        {
            List<TR_SoldUnitListDto> dataSoldUnits = new List<TR_SoldUnitListDto>();
            try
            {
                List<GetDataTrxToInsertDto> dataTrx = new List<GetDataTrxToInsertDto>();
                if (unitCode != null && unitNo != null)
                {
                    var unitID = (from x in _msUnitRepo.GetAll()
                                  join a in _msUnitCodeRepo.GetAll() on x.unitCodeID equals a.Id
                                  where x.unitNo.Equals(unitNo) && a.unitCode.Equals(unitCode)
                                  select x.Id).FirstOrDefault();

                    dataTrx = (from x in _trBookingHeaderRepo.GetAll()
                               join bookingDetail in _trBookingDetail.GetAll() on x.Id equals bookingDetail.bookingHeaderID
                               join bookingDoc in _trBookingDocRepo.GetAll() on x.Id equals bookingDoc.bookingHeaderID
                               join a in _msUnitCodeRepo.GetAll() on x.unitID equals a.Id
                               where x.unitID == unitID && x.cancelDate == null &&
                                     x.memberCode != "0000000" && bookingDoc.docCode == "PPPU" && bookingDoc.docDate != null
                               group bookingDetail by new
                               {
                                   x.bookCode,
                                   x.schemaID,
                                   x.memberCode,
                                   x.unitID,
                                   x.bookDate,
                                   x.remarks,
                                   x.termRemarks,
                                   bookingDoc.docDate
                               } into g
                               select new GetDataTrxToInsertDto
                               {
                                   schemaID = g.Key.schemaID,
                                   devCode = g.FirstOrDefault().coCode,
                                   bookNo = g.Key.bookCode,
                                   memberCode = g.Key.memberCode,
                                   unitID = g.Key.unitID,
                                   bookDate = g.Key.bookDate,
                                   netNetPrice = g.Sum(a => a.netNetPrice),
                                   PPJBDate = g.Key.docDate,
                                   remarks = g.Key.remarks,
                                   termRemarks = g.Key.termRemarks
                               }).ToList();
                }
                else
                {
                    var dateNow = DateTime.Now;
                    var dateYesterday = dateNow.AddDays(-1);
                    dataTrx = (from x in _trBookingHeaderRepo.GetAll()
                               join bookingDetail in _trBookingDetail.GetAll() on x.Id equals bookingDetail.bookingHeaderID
                               join bookingDoc in _trBookingDocRepo.GetAll() on x.Id equals bookingDoc.bookingHeaderID
                               where x.cancelDate == null && x.memberCode != "0000000" &&
                                     (x.bookDate >= dateYesterday && x.bookDate <= dateNow) &&
                                     bookingDoc.docCode == "PPPU" && bookingDoc.docDate != null
                               group bookingDetail by new
                               {
                                   x.bookCode,
                                   x.schemaID,
                                   x.memberCode,
                                   x.unitID,
                                   x.bookDate,
                                   x.remarks,
                                   x.termRemarks,
                                   bookingDoc.docDate
                               } into g
                               select new GetDataTrxToInsertDto
                               {
                                   schemaID = g.Key.schemaID,
                                   devCode = g.FirstOrDefault().coCode,
                                   bookNo = g.Key.bookCode,
                                   memberCode = g.Key.memberCode,
                                   unitID = g.Key.unitID,
                                   bookDate = g.Key.bookDate,
                                   netNetPrice = g.Sum(a => a.netNetPrice),
                                   PPJBDate = g.Key.docDate,
                                   remarks = g.Key.remarks,
                                   termRemarks = g.Key.termRemarks
                               }).ToList();
                }


                if (dataTrx.Any())
                {
                    var currentProcess = 0;
                    foreach (var data in dataTrx.ToList())
                    {
                        currentProcess++;
                        Logger.InfoFormat("Form a sold unit data {0}/{1} bookcode {2} ", currentProcess, dataTrx.Count, data.bookNo);


                        //getDataID
                        #region default                        
                        var scmData = (from x in _msSchemaRepo.GetAll()
                                       where x.Id == data.schemaID
                                       select x).FirstOrDefault();
                        var developerID = (from x in _msDeveloperSchemaRepo.GetAll()
                                           where x.devCode == data.devCode
                                           select x.Id).FirstOrDefault();
                        var unit = (from x in _msUnitRepo.GetAll()
                                    join y in _msUnitCodeRepo.GetAll() on x.unitCodeID equals y.Id
                                    where y.unitCode == data.roadCode && x.unitNo == data.unitNo
                                    select x).FirstOrDefault();
                        var pctBobot = (from x in _msBobotCommRepo.GetAll()
                                        where x.schemaID == data.schemaID
                                        select x.pctBobot).FirstOrDefault();
                        #endregion

                        #region testonly without msdtc - change it if you want
                        /*
                        var scmData = (from x in _context.MS_Schema
                                       where x.Id == data.schemaID
                                       select x).FirstOrDefault();
                        var developerID = (from x in _context.MS_Developer_Schema
                                           where x.devCode == data.devCode
                                           select x.Id).FirstOrDefault();
                        var unit = (from x in _contextProp.MS_Unit
                                    join y in _contextProp.MS_UnitCode on x.unitCodeID equals y.Id
                                    where y.unitCode == data.roadCode && x.unitNo == data.unitNo
                                    select x).FirstOrDefault();
                        var pctBobot = (from x in _context.MS_BobotComm
                                        where x.schemaID == data.schemaID
                                        select x.pctBobot).FirstOrDefault();
                                        */
                        #endregion

                        var unitPrice = data.netNetPrice * Convert.ToDecimal(pctBobot);
                        var dateAllow = data.bookDate.AddDays(scmData.dueDateComm);
                        var todayDate = DateTime.Now;

                        /***********  
                         * checking data at master table
                         * skip the data if its doesnt exist,
                         * data will not insert to tr_soldunit if its doesnt exist,
                         * check bookdate must be same as bookdate + duedatecomm(masterschema)
                         ***********/
                        if (scmData == null)
                        {
                            continue;
                        }

                        if (developerID == 0)
                        {
                            continue;
                        }

                        if (unit == null)
                        {
                            continue;
                        }

                        //pending soalnya gak akan masuk data bookcode nya ke trsoldunit
                        //if(todayDate.ToString("dd/MM/yyyy") != dateAllow.ToString("dd/MM/yyyy"))
                        //{
                        //    continue;
                        //}

                        var dataBuild = new TR_SoldUnitListDto()
                        {
                            bookNo = data.bookNo,
                            batchNo = "", //hardcode
                            memberCode = data.memberCode,
                            CDCode = "", //hardcode
                            ACDCode = "", //hardcode
                            roadCode = data.roadCode,
                            roadName = "", //hardcode
                            unitNo = data.unitNo,
                            bookDate = data.bookDate,
                            unitLandArea = 0, //hardcode
                            unitBuildArea = 0, //hardcode
                            netNetPrice = data.netNetPrice,
                            unitPrice = unitPrice,
                            pctComm = 0, //hardcode
                            pctBobot = pctBobot,
                            PPJBDate = data.PPJBDate,
                            xreqInstPayDate = null, //hardcode
                            xprocessDate = null, //hardcode
                            cancelDate = null,
                            Remarks = data.remarks.Length <= 100 ? data.remarks : data.remarks.Substring(0, 100),
                            holdDate = null,
                            calculateUseMaster = true, //hardcode
                            termRemarks = data.termRemarks,
                            holdReason = null,
                            changeDealClosureReason = "", //hardcode
                            schemaID = scmData.Id,
                            developerSchemaID = developerID,
                            unitID = unit.Id,
                            entityID = 1
                        };

                        var getExistData = (from x in _trSoldUnitRepo.GetAll()
                                            where x.bookNo == data.bookNo
                                            select x.Id).FirstOrDefault();

                        if (getExistData != 0)
                        {
                            dataBuild.Id = getExistData;
                        }

                        dataSoldUnits.Add(dataBuild);
                    }

                }

                Logger.InfoFormat("Total Data SoldUnit : {0}", dataSoldUnits.Count);

            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return dataSoldUnits;
        }

        public List<TR_SoldRequirementListDto> GetDataRequirementToInsert(List<TR_SoldUnitListDto> input)
        {
            List<TR_SoldRequirementListDto> dataToInsert = new List<TR_SoldRequirementListDto>();
            var currentProcess = 0;
            foreach (var soldUnit in input)
            {
                //var netAmt = (from DA in _trPaymentDetailAllocRepo.GetAll()
                //                    where DA.bookCode == soldUnit.bookNo
                //                    select DA.netAmt).Sum();
                //var vatAmt = (from DA in _trPaymentDetailAllocRepo.GetAll()
                //              where DA.bookCode == soldUnit.bookNo
                //              select DA.netAmt).Sum();
                currentProcess++;
                Logger.InfoFormat("Form a sold unit requirement data {0}/{1} bookNo {2} ", currentProcess, input.Count, soldUnit.bookNo);
                //CASH / CICILAN 12x
                if (soldUnit.termRemarks.ToLower().Contains("cash") || soldUnit.termRemarks.ToLower().Contains("cicilan 12"))
                {
                    var dataCheck = (from x in _trSoldUnitRequirementRepo.GetAll()
                                     where soldUnit.bookNo == x.bookNo
                                     && soldUnit.developerSchemaID == x.developerSchemaID
                                     && soldUnit.schemaID == x.schemaID
                                     && 1 == x.reqNo
                                     select x.Id).FirstOrDefault();

                    var result = new TR_SoldRequirementListDto
                    {
                        //100%
                        bookNo = soldUnit.bookNo,
                        entityID = 1,
                        developerSchemaID = soldUnit.developerSchemaID,
                        schemaID = soldUnit.schemaID,
                        reqNo = 1,
                        reqDesc = "100 % Payment Done",
                        pctPaid = 1,
                        orPctPaid = 1,
                        reqDate = null,
                        processDate = null
                    };

                    //if(soldUnit.netNetPrice > (netAmt + vatAmt))
                    //{
                    //    result.reqDate = DateTime.Now;
                    //}

                    //edit
                    if (dataCheck != 0)
                    {
                        result.Id = dataCheck;
                    }

                    dataToInsert.Add(result);
                }

                else
                {
                    //schemaID cari dari group schema
                    var schemaID = (from GS in _msGroupSchemaRepo.GetAll() where GS.schemaID == soldUnit.schemaID && GS.isComplete == true select GS.schemaID).FirstOrDefault();


                    var getDataRequirement = (from SR in _msGroupSchemaRequirementRepo.GetAll()
                                              where schemaID == soldUnit.schemaID
                                              && SR.isComplete == true
                                              select new
                                              {
                                                  Id = SR.Id,
                                                  reqNo = SR.reqNo,
                                                  reqDesc = SR.reqDesc,
                                                  pctPaid = SR.pctPaid
                                              }).ToList();

                    if (!getDataRequirement.Any())
                    {
                        getDataRequirement = (from SR in _msSchemaRequirementRepo.GetAll()
                                              where SR.schemaID == soldUnit.schemaID
                                              && SR.isComplete == true
                                              select new
                                              {
                                                  Id = SR.Id,
                                                  reqNo = (byte)SR.reqNo,
                                                  reqDesc = SR.reqDesc,
                                                  pctPaid = SR.pctPaid
                                              }).ToList();
                    }

                    var req1 = getDataRequirement.Where(x => x.reqNo == 1).Select(x => x.Id).FirstOrDefault();
                    var req2 = getDataRequirement.Where(x => x.reqNo == 2).Select(x => x.Id).FirstOrDefault();

                    if (req1 != 0)
                    {
                        var reqDesc1 = getDataRequirement.Where(x => x.reqNo == 1).Select(x => x.reqDesc).FirstOrDefault();
                        var pctPaid1 = getDataRequirement.Where(x => x.reqNo == 1).Select(x => x.pctPaid).FirstOrDefault();

                        var dataCheck = (from x in _trSoldUnitRequirementRepo.GetAll()
                                         where soldUnit.bookNo == x.bookNo
                                         && soldUnit.developerSchemaID == x.developerSchemaID
                                         && soldUnit.schemaID == x.schemaID
                                         && 1 == x.reqNo
                                         select x.Id).FirstOrDefault();

                        var result = new TR_SoldRequirementListDto
                        {
                            //10%
                            bookNo = soldUnit.bookNo,
                            entityID = 1,
                            developerSchemaID = soldUnit.developerSchemaID,
                            schemaID = soldUnit.schemaID,
                            reqNo = 1,
                            reqDesc = reqDesc1,
                            pctPaid = pctPaid1 / 100,
                            orPctPaid = pctPaid1 / 100,
                            reqDate = null,
                            processDate = null
                        };

                        //edit
                        if (dataCheck != 0)
                        {
                            result.Id = dataCheck;
                        }
                        dataToInsert.Add(result);

                    }

                    if (req2 != 0)
                    {
                        var reqDesc2 = getDataRequirement.Where(x => x.reqNo == 2).Select(x => x.reqDesc).FirstOrDefault();
                        var pctPaid2 = getDataRequirement.Where(x => x.reqNo == 2).Select(x => x.pctPaid).FirstOrDefault();

                        var dataCheck = (from x in _trSoldUnitRequirementRepo.GetAll()
                                         where soldUnit.bookNo == x.bookNo
                                         && soldUnit.developerSchemaID == x.developerSchemaID
                                         && soldUnit.schemaID == x.schemaID
                                         && 2 == x.reqNo
                                         select x.Id).FirstOrDefault();

                        var result = new TR_SoldRequirementListDto
                        {
                            //30%
                            bookNo = soldUnit.bookNo,
                            entityID = 1,
                            developerSchemaID = soldUnit.developerSchemaID,
                            schemaID = soldUnit.schemaID,
                            reqNo = 2,
                            reqDesc = reqDesc2,
                            pctPaid = pctPaid2 / 100,
                            orPctPaid = pctPaid2 / 100,
                            reqDate = null,
                            processDate = null
                        };

                        if (dataCheck != 0)
                        {

                            result.Id = dataCheck;
                        }

                        dataToInsert.Add(result);

                    }
                }
            }
            Logger.InfoFormat("Total data sold unit requirement : {0}", dataToInsert.Count);
            return dataToInsert;
        }

        public void CreateTrCommPayment(List<CommPaymentInputDto> input)
        {
            Logger.Info("CreateTrCommPayment() - Started.");
            foreach (var item in input)
            {
                Logger.DebugFormat("CreateTrCommPayment() - Start checking exiting data. Params sent:{0}" +
                "bookNo         ={1}{0}" +
                "memberCode     ={2}{0}" +
                "commTypeCode   ={3}{0}" +
                "commTypeID     ={4}{0}" +
                "schemaID       ={5}{0}" +
                "developerSchemaID  = {6}"
                , Environment.NewLine, item.bookNo, item.memberCode, item.commTypeCode, item.commTypeId, item.schemaId, item.developerSchemaID);
                var cekExistingData = (from a in _trCommPaymentRepo.GetAll()
                                       where
                                       a.bookNo == item.bookNo &&
                                       a.memberCode == item.memberCode &&
                                       a.commTypeCode == item.commTypeCode &&
                                       a.commTypeID == item.commTypeId &&
                                       a.schemaID == item.schemaId &&
                                       a.developerSchemaID == item.developerSchemaID
                                       select a).Any();
                Logger.DebugFormat("CreateTrCommPayment() - End checking exiting data. Result: {0}", cekExistingData);

                if (!cekExistingData)
                {
                    var createCommPayment = new TR_CommPayment
                    {
                        bookNo = item.bookNo,
                        memberCode = item.memberCode,
                        commTypeCode = item.commTypeCode,
                        commTypeID = item.commTypeId,
                        schemaID = item.schemaId,
                        developerSchemaID = item.developerSchemaID,
                        isHold = "0",
                        commPayCode = "-",
                        payOrderNo = "-",
                        desc = "-",
                        bankCode = "-",
                        bankType = "-",
                        bankAccNo = "-",
                        bankAccName = "-",
                        bankBranchName = "-",
                        memberName = "-",
                        NPWP = "-",
                        asUplineNo = 0,
                        commNo = 0,
                        reqNo = 0,
                        schedDate = DateTime.Now,
                        amount = 10000000,
                        pphProcessDate = DateTime.Now,
                        pphAmount = 100000,
                        isAutoCalc = false,
                        isInstitusi = false,
                        oracleInvoiceID = 0

                    };

                    try
                    {
                        Logger.DebugFormat("CreateTrCommPayment() - Start insert CommPayment. Params sent:{0}" +
                        "	bookNo	            = {1}{0}" +
                        "	memberCode	        = {2}{0}" +
                        "	commTypeCode	    = {3}{0}" +
                        "	commTypeID	        = {4}{0}" +
                        "	schemaID	        = {5}{0}" +
                        "	developerSchemaID	= {6}{0}" +
                        "	isHold	            = {7}{0}" +
                        "	commPayCode	        = {8}{0}" +
                        "	payOrderNo	        = {9}{0}" +
                        "	desc	            = {10}{0}" +
                        "	bankCode	        = {11}{0}" +
                        "	bankType	        = {12}{0}" +
                        "	bankAccNo	        = {13}{0}" +
                        "	bankAccName	        = {14}{0}" +
                        "	bankBranchName	    = {15}{0}" +
                        "	memberName	        = {16}{0}" +
                        "	NPWP	            = {17}{0}" +
                        "	asUplineNo	        = {18}{0}" +
                        "	commNo	            = {19}{0}" +
                        "	reqNo	            = {20}{0}" +
                        "	schedDate	        = {21}{0}" +
                        "	amount	            = {22}{0}" +
                        "	pphProcessDate	    = {23}{0}" +
                        "	pphAmount	        = {24}{0}" +
                        "	isAutoCalc	        = {25}{0}" +
                        "	isInstitusi	        = {26}{0}" +
                        "	oracleInvoiceID	    = {27}"
                        , Environment.NewLine, item.bookNo, item.memberCode, item.commTypeCode, item.commTypeId, item.schemaId, item.developerSchemaID,
                        "0", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-",
                        0, 0, 0, DateTime.Now, 10000000, DateTime.Now, 100000, false, false, 0);
                        _trCommPaymentRepo.Insert(createCommPayment);
                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                        Logger.DebugFormat("CreateTrCommPayment() - End insert CommPayment.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateTrCommPayment() ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateTrCommPayment() ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("CreateTrCommPayment() ERROR. Result = {0}", "The Payment Already Exist For the current project and schema!");
                    throw new UserFriendlyException("The Payment Already Exist For the current project and schema!");
                }
            }
            Logger.Info("CreateTrCommPayment() - Finished.");
        }


        public void CancelSoldUnit(string bookNo)
        {
            Logger.Info("CancelSoldUnit() - Started.");

            var getTrSoldUnit = (from x in _trSoldUnitRepo.GetAll()
                                 where bookNo == x.bookNo
                                 select x).FirstOrDefault();

            var updateTrSoldUnit = getTrSoldUnit.MapTo<TR_SoldUnit>();
            var getDate = DateTime.Now;

            updateTrSoldUnit.cancelDate = getDate;

            try
            {
                Logger.DebugFormat("CancelSoldUnit() - Start cancel sold unit. Params sent:{0}" +
                    "cancelDate = {1}",
                    Environment.NewLine, getDate);
                _trSoldUnitRepo.Update(updateTrSoldUnit);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                Logger.DebugFormat("CancelSoldUnit() - End cancel sold unit.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CancelSoldUnit() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CancelSoldUnit() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("CancelSoldUnit() - Finished.");
        }

        public List<string> GetPaymentTermDropdown(string termCode)
        {
            var getPaymentTerm = (from x in _msTermRepo.GetAll()
                                  where x.termCode == termCode
                                  select x.remarks).ToList();

            return getPaymentTerm;
        }

        public List<GetDataMemberUplineInsertListDto> GetDataMemberUplineInsert(GetDataMemberUplineInsertInputDto input, int limitAsUplineNo)  //passed
        {
            List<GetDataMemberUplineInsertListDto> memberWithPaid = new List<GetDataMemberUplineInsertListDto>();
            List<GetUplineByMemberCodeListDto> dataUpline = new List<GetUplineByMemberCodeListDto>();
            try
            {
                try
                {
                    dataUpline = GetUplineByMemberCode(input.memberCode, limitAsUplineNo);
                }
                catch (UserFriendlyException e)
                {
                    SendConsole("" + e.Message + " " + e.StackTrace);
                }

                foreach (var X in dataUpline)
                {
                    var joinGschema = (from A in _msGroupSchemaRepo.GetAll()
                                       join B in _msSchemaRepo.GetAll() on A.schemaID equals B.Id
                                       where B.scmCode == X.scmCode
                                       && A.isActive
                                       && B.isActive
                                       && A.isComplete
                                       && B.isComplete
                                       select new GetDataMemberUplineInsertListDto
                                       {
                                           asUplineNo = X.asUplineNo,
                                           memberCodeR = X.memberCode,
                                           memberCodeN = X.memberCode,
                                           memberName = X.memberName,
                                           schemaID = B.Id,
                                           groupSchemaID = A.Id,
                                           isStandart = A.isStandard,
                                           statusMemberID = X.memberStatusID,
                                           commTypeID = X.commTypeID,
                                           pointTypeID = X.pointTypeID,
                                           pphRangeID = X.pphRangeID,
                                           pphRangeInsID = X.pphRangeInsID,
                                           commTypeCode = X.commTypeCode,
                                       }).FirstOrDefault();

                    #region test only withoud msdct
                    /*
                    var joinGschema = (from A in _context.MS_GroupSchema
                                       join B in _context.MS_Schema on A.schemaID equals B.Id
                                       where B.scmCode == X.scmCode
                                       && A.isActive
                                       && B.isActive
                                       && A.isComplete
                                       && B.isComplete
                                       select new GetDataMemberUplineInsertListDto
                                       {
                                           asUplineNo = X.asUplineNo,
                                           memberCodeR = X.memberCode,
                                           memberCodeN = X.memberCode,
                                           memberName = X.memberName,
                                           schemaID = B.Id,
                                           groupSchemaID = A.Id,
                                           isStandart = A.isStandard,
                                           statusMemberID = X.memberStatusID,
                                           commTypeID = X.commTypeID,
                                           pointTypeID = X.pointTypeID,
                                           pphRangeID = X.pphRangeID,
                                           pphRangeInsID = X.pphRangeInsID,
                                           commTypeCode = X.commTypeCode,
                                       }).FirstOrDefault();
                                       */
                    #endregion

                    if (joinGschema != null) //ketemu di group schema
                    {
                        var schemaID = joinGschema.schemaID;
                        var groupSchemaID = joinGschema.groupSchemaID;
                        var statusMemberID = joinGschema.statusMemberID;
                        var asUplineNo = joinGschema.asUplineNo;
                        var commTypeID = joinGschema.commTypeID;
                        var isStandart = joinGschema.isStandart;

                        if (joinGschema.isStandart == true) //if isStandart true
                        {
                            var joinGschemaPct = (from A in _msGroupCommPctRepo.GetAll()
                                                  where A.groupSchemaID == groupSchemaID
                                                  && A.statusMemberID == statusMemberID
                                                  && A.asUplineNo == asUplineNo
                                                  && A.commTypeID == commTypeID
                                                  && A.validDate <= DateTime.Now
                                                  && A.isComplete
                                                  select new GetDataMemberUplineInsertListDto
                                                  {
                                                      bookNo = input.bookNo,
                                                      developerSchemaID = input.developerSchemaID,
                                                      asUplineNo = X.asUplineNo,
                                                      memberCodeR = X.memberCode,
                                                      memberCodeN = X.memberCode,
                                                      memberName = X.memberName,
                                                      schemaID = schemaID,
                                                      groupSchemaID = groupSchemaID,
                                                      isStandart = isStandart,
                                                      statusMemberID = statusMemberID,
                                                      commTypeID = X.commTypeID,
                                                      pointTypeID = X.pointTypeID,
                                                      pphRangeID = X.pphRangeID,
                                                      pphRangeInsID = X.pphRangeInsID,
                                                      commTypeCode = X.commTypeCode,

                                                      commPctPaid = A.commPctPaid / 100
                                                  }).FirstOrDefault();

                            if (joinGschemaPct != null)
                            {
                                memberWithPaid.Add(joinGschemaPct);
                            }
                        }
                        else
                        {
                            var joinGschemaPctNonStd = (from A in _msGroupCommPctNonStdRepo.GetAll()
                                                        where A.groupSchemaID == groupSchemaID
                                                        && A.statusMemberID == statusMemberID
                                                          && A.asUplineNo == asUplineNo
                                                          && A.commTypeID == commTypeID
                                                          && A.validDate <= DateTime.Now
                                                          && A.isComplete
                                                        select new GetDataMemberUplineInsertListDto
                                                        {
                                                            bookNo = input.bookNo,
                                                            developerSchemaID = input.developerSchemaID,
                                                            asUplineNo = X.asUplineNo,
                                                            memberCodeR = X.memberCode,
                                                            memberCodeN = X.memberCode,
                                                            memberName = X.memberName,
                                                            schemaID = schemaID,
                                                            groupSchemaID = groupSchemaID,
                                                            isStandart = isStandart,
                                                            statusMemberID = statusMemberID,
                                                            commTypeID = X.commTypeID,
                                                            pointTypeID = X.pointTypeID,
                                                            pphRangeID = X.pphRangeID,
                                                            pphRangeInsID = X.pphRangeInsID,
                                                            commTypeCode = X.commTypeCode,

                                                            commPctPaid = A.commPctPaid / 100
                                                        }).FirstOrDefault();

                            if (joinGschemaPctNonStd != null)
                            {
                                memberWithPaid.Add(joinGschemaPctNonStd);
                            }
                        }
                    }
                    else //gak ketemu di group schema
                    {
                        var joinSchema = (from A in _msSchemaRepo.GetAll()
                                          where A.scmCode == X.scmCode
                                          && A.isActive
                                          && A.isComplete
                                          select new GetDataMemberUplineInsertListDto
                                          {
                                              asUplineNo = X.asUplineNo,
                                              memberCodeR = X.memberCode,
                                              memberCodeN = X.memberCode,
                                              memberName = X.memberName,
                                              schemaID = A.Id,
                                              groupSchemaID = A.Id,
                                              statusMemberID = X.memberStatusID,
                                              commTypeID = X.commTypeID,
                                              pointTypeID = X.pointTypeID,
                                              pphRangeID = X.pphRangeID,
                                              pphRangeInsID = X.pphRangeInsID,
                                              commTypeCode = X.commTypeCode
                                          }).FirstOrDefault();

                        #region test only without msdtc
                        /*
                        var joinSchema = (from A in _context.MS_Schema
                                          where A.scmCode == X.scmCode
                                          && A.isActive
                                          && A.isComplete
                                          select new GetDataMemberUplineInsertListDto
                                          {
                                              asUplineNo = X.asUplineNo,
                                              memberCodeR = X.memberCode,
                                              memberCodeN = X.memberCode,
                                              memberName = X.memberName,
                                              schemaID = A.Id,
                                              groupSchemaID = A.Id,
                                              statusMemberID = X.memberStatusID,
                                              commTypeID = X.commTypeID,
                                              pointTypeID = X.pointTypeID,
                                              pphRangeID = X.pphRangeID,
                                              pphRangeInsID = X.pphRangeInsID,
                                              commTypeCode = X.commTypeCode
                                          }).FirstOrDefault();
                                          */
                        #endregion

                        if (joinSchema != null)
                        {
                            var joinSchemaSchemaID = joinSchema.schemaID;
                            var joinGroupSchemaSchemaID = joinSchema.groupSchemaID;
                            var joinSchemaStatusMemberID = joinSchema.statusMemberID;
                            var joinSchemaAsUplineNo = joinSchema.asUplineNo;
                            var joinSchemaCommTypeID = joinSchema.commTypeID;
                            var joinIsStandart = joinSchema.isStandart;
                            var joinStatusMemberID = joinSchema.statusMemberID;

                            //Jika tidak ketemu di group schema , maka lihat ke schema lalu join ke Ms_CommPct                            
                            var joinSchemaPct = (from A in _msCommPctRepo.GetAll()
                                                 where A.schemaID == joinSchemaSchemaID
                                                 && A.statusMemberID == joinSchemaStatusMemberID
                                                   && A.asUplineNo == joinSchemaAsUplineNo
                                                   && A.commTypeID == joinSchemaCommTypeID
                                                   && A.validDate <= DateTime.Now
                                                   && A.isComplete
                                                 select new GetDataMemberUplineInsertListDto
                                                 {
                                                     bookNo = input.bookNo,
                                                     developerSchemaID = input.developerSchemaID,
                                                     asUplineNo = X.asUplineNo,
                                                     memberCodeR = X.memberCode,
                                                     memberCodeN = X.memberCode,
                                                     memberName = X.memberName,
                                                     schemaID = joinSchemaSchemaID,
                                                     groupSchemaID = joinGroupSchemaSchemaID,
                                                     isStandart = joinIsStandart,
                                                     statusMemberID = joinStatusMemberID,
                                                     commTypeID = X.commTypeID,
                                                     pointTypeID = X.pointTypeID,
                                                     pphRangeID = X.pphRangeID,
                                                     pphRangeInsID = X.pphRangeInsID,
                                                     commTypeCode = X.commTypeCode,

                                                     commPctPaid = A.commPctPaid / 100
                                                 }).FirstOrDefault();


                            #region test only without msdtc
                            /*
                            var joinSchemaPct = (from A in _context.MS_CommPct
                                                 where A.schemaID == joinSchemaSchemaID
                                                 && A.statusMemberID == joinSchemaStatusMemberID
                                                   && A.asUplineNo == joinSchemaAsUplineNo
                                                   && A.commTypeID == joinSchemaCommTypeID
                                                   && A.validDate <= DateTime.Now
                                                   && A.isComplete
                                                 select new GetDataMemberUplineInsertListDto
                                                 {
                                                     bookNo = input.bookNo,
                                                     developerSchemaID = input.developerSchemaID,
                                                     asUplineNo = X.asUplineNo,
                                                     memberCodeR = X.memberCode,
                                                     memberCodeN = X.memberCode,
                                                     memberName = X.memberName,
                                                     schemaID = joinSchemaSchemaID,
                                                     groupSchemaID = joinGroupSchemaSchemaID,
                                                     isStandart = joinIsStandart,
                                                     statusMemberID = joinStatusMemberID,
                                                     commTypeID = X.commTypeID,
                                                     pointTypeID = X.pointTypeID,
                                                     pphRangeID = X.pphRangeID,
                                                     pphRangeInsID = X.pphRangeInsID,
                                                     commTypeCode = X.commTypeCode,

                                                     commPctPaid = A.commPctPaid / 100
                                                 }).FirstOrDefault();
                                                 */
                            #endregion

                            if (joinSchemaPct != null)
                            {
                                memberWithPaid.Add(joinSchemaPct);
                            }
                        }
                        else
                        {
                            //TODO: insert into error table, Schema tidak ketemu
                        }

                    }

                }

            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            dataUpline.Clear();
            dataUpline = null;

            return memberWithPaid;
        }

        public List<TR_CommPctListDto> GetIdForUpdateOrInsertMemberUplineTrCommPct(List<TR_CommPctListDto> input)
        {
            List<TR_CommPctListDto> dataUpdatedWithID = new List<TR_CommPctListDto>();

            foreach (var X in input)
            {
                var cekTrCommpCt = (from A in _trCommPctRepo.GetAll()
                                    where A.asUplineNo == X.asUplineNo
                                    && A.memberCodeR == X.memberCodeR
                                    && A.bookNo == X.bookNo
                                    && A.commTypeID == X.commTypeID
                                    && A.developerSchemaID == X.developerSchemaID
                                    && A.statusMemberID == X.statusMemberID
                                    && A.pointTypeID == X.pointTypeID
                                    && A.pphRangeInsID == X.pphRangeInsID
                                    && A.pphRangeID == X.pphRangeID
                                    select A).FirstOrDefault();

                if (cekTrCommpCt != null)
                {
                    var dataFormattingTrCommPctWithID = new TR_CommPctListDto
                    {
                        Id = cekTrCommpCt.Id,
                        bookNo = X.bookNo,
                        developerSchemaID = X.developerSchemaID,
                        asUplineNo = X.asUplineNo,
                        commPctPaid = X.commPctPaid,
                        commTypeID = X.commTypeID,
                        memberCodeN = X.memberCodeN,
                        memberCodeR = X.memberCodeR,
                        pointTypeID = X.pointTypeID,
                        pphRangeID = X.pphRangeID,
                        pphRangeInsID = X.pphRangeInsID,
                        statusMemberID = X.statusMemberID,
                        CreationTime = X.CreationTime
                    };
                    dataUpdatedWithID.Add(dataFormattingTrCommPctWithID);
                }
                else
                {
                    var dataFormattingTrCommPct = new TR_CommPctListDto
                    {
                        bookNo = X.bookNo,
                        developerSchemaID = X.developerSchemaID,
                        asUplineNo = X.asUplineNo,
                        commPctPaid = X.commPctPaid,
                        commTypeID = X.commTypeID,
                        memberCodeN = X.memberCodeN,
                        memberCodeR = X.memberCodeR,
                        pointTypeID = X.pointTypeID,
                        pphRangeID = X.pphRangeID,
                        pphRangeInsID = X.pphRangeInsID,
                        statusMemberID = X.statusMemberID,
                        CreationTime = X.CreationTime
                    };
                    dataUpdatedWithID.Add(dataFormattingTrCommPct);
                }
            }
            return dataUpdatedWithID;
        }

        public List<TR_CommPctListDto> GetMemberToInsert(List<TR_SoldUnitListDto> input, int limitAsUplineNo)
        {
            List<TR_CommPctListDto> dataUplineForInsert = new List<TR_CommPctListDto>();
            var currentProcess = 0;
            foreach (var X in input)
            {
                currentProcess++;
                Logger.InfoFormat("Form a member data {0}/{1} bookNo {2} ", currentProcess, input.Count, X.bookNo);
                GetDataMemberUplineInsertInputDto inputTrCommPct = new GetDataMemberUplineInsertInputDto();
                inputTrCommPct.bookNo = X.bookNo;
                inputTrCommPct.developerSchemaID = X.developerSchemaID;
                inputTrCommPct.entityID = X.entityID;
                inputTrCommPct.memberCode = X.memberCode;

                var dataMemberUpline = GetDataMemberUplineInsert(inputTrCommPct, limitAsUplineNo);

                var dataFormattingTrCommPct = (from A in dataMemberUpline
                                               select new TR_CommPctListDto
                                               {
                                                   bookNo = A.bookNo,
                                                   developerSchemaID = A.developerSchemaID,
                                                   asUplineNo = (short)A.asUplineNo,
                                                   commPctPaid = (double)A.commPctPaid,
                                                   commTypeID = A.commTypeID,
                                                   memberCodeN = A.memberCodeN,
                                                   memberCodeR = A.memberCodeR,
                                                   pointTypeID = A.pointTypeID,
                                                   pphRangeID = A.pphRangeID,
                                                   pphRangeInsID = A.pphRangeInsID,
                                                   statusMemberID = A.statusMemberID,
                                                   CreationTime = DateTime.Now
                                               }).ToList();

                //Per bookNo
                dataUplineForInsert.AddRange(dataFormattingTrCommPct);
            }
            Logger.InfoFormat("Total Data Member : {0}", dataUplineForInsert.Count);
            return dataUplineForInsert;
        }

        public List<GetMemberToInsertManualListDto> GetDataMemberToInsertManual(GetDataMemberToInsertManualInputDto input)
        {
            var bookNo = input.input.bookNo;
            var unitPrice = input.input.unitPrice;

            GetDataMemberUplineInsertInputDto inputTrCommPct = new GetDataMemberUplineInsertInputDto();
            inputTrCommPct.bookNo = input.input.bookNo;
            inputTrCommPct.developerSchemaID = input.input.developerSchemaID;
            inputTrCommPct.memberCode = input.input.memberCode;

            var dataMemberUpline = GetDataMemberUplineInsert(inputTrCommPct, input.limitAsUplineNo);

            var dataUpline = dataMemberUpline.Select(X => new
            {
                bookNo = bookNo,
                asUplineNo = X.asUplineNo,
                commPctPaid = X.commPctPaid,
                memberCodeR = X.memberCodeR,
                memberCodeN = X.memberCodeN,
                memberName = X.memberName,
                schemaID = X.schemaID,
                groupSchemaID = X.groupSchemaID,
                statusMemberID = X.statusMemberID,
                commTypeID = X.commTypeID,
                pointTypeID = X.pointTypeID,
                pphRangeID = X.pphRangeID,
                pphRangeInsID = X.pphRangeInsID,
                commTypeCode = X.commTypeCode,
                unitPrice = unitPrice
            }).ToList();

            var dataFormattingTrCommPct = (from A in dataUpline
                                           join B in input.inputRequirement on A.bookNo equals B.bookNo
                                           select new
                                           {
                                               bookNo = A.bookNo,
                                               asUplineNo = A.asUplineNo,
                                               commPctPaid = A.commPctPaid,
                                               memberCodeR = A.memberCodeR,
                                               memberCodeN = A.memberCodeN,
                                               memberName = A.memberName,
                                               schemaID = A.schemaID,
                                               groupSchemaID = A.groupSchemaID,
                                               statusMemberID = A.statusMemberID,
                                               commTypeID = A.commTypeID,
                                               pointTypeID = A.pointTypeID,
                                               pphRangeID = A.pphRangeID,
                                               pphRangeInsID = A.pphRangeInsID,
                                               commTypeCode = A.commTypeCode,
                                               unitPrice = A.unitPrice,
                                               pctPaid = B.pctPaid
                                           }).ToList();

            var dataTrCommPct = dataFormattingTrCommPct.Select(X => new GetMemberToInsertManualListDto
            {
                commPaid = X.unitPrice * (decimal)X.commPctPaid,
                actualPaid = X.unitPrice * (decimal)X.commPctPaid * (decimal)X.pctPaid,
                commPctPaid = X.commPctPaid,
                asUplineNo = X.asUplineNo,
                memberCode = X.memberCodeR,
                memberName = X.memberName,
                pctPaid = (decimal)X.pctPaid
            }).ToList();

            return dataTrCommPct;
        }

        //BulkInsertOrUpdateMember
        public void BulkInsertOrUpdateMember(List<TR_CommPctListDto> input)
        {
            Logger.Info("BulkInsertOrUpdateMember() - Started.");
            if (input.Any())
            {
                try
                {
                    var options = new DbContextOptions<NewCommDbContext>();
                    var dbContext = new NewCommDbContext(options);

                    var commPct = ObjectMapper.Map<List<TR_CommPct>>(input);

                    dbContext.BulkInsertOrUpdate(_trCommPctRepo, commPct);

                    //_trCommPctRepo.BulkInsertOrUpdate(commPct);
                    Logger.InfoFormat("Bulk INSERT or UPDATE Member Total Member Data: {0}", input.Count);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("BulkInsertOrUpdateMember() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("BulkInsertOrUpdateMember() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("BulkInsertOrUpdateMember() - Finished.");
        }

        [UnitOfWork(isTransactional: false)]
        public void BulkInsertOrUpdateTRSoldUnitReq(List<TR_SoldRequirementListDto> input)
        {
            Logger.Info("BulkInsertOrUpdateTRSoldUnitReq() - Started.");

            try
            {
                //var a = input.MapTo<List<TR_SoldUnitRequirement>>();

                var list = new ListResultDto<TR_SoldRequirementListDto>(
                    ObjectMapper.Map<List<TR_SoldRequirementListDto>>(input)
                    );

                var options = new DbContextOptionsBuilder<NewCommDbContext>();
                var dbContext = new NewCommDbContext(options.Options);


                var soldReq = ObjectMapper.Map<List<TR_SoldUnitRequirement>>(input);

                _context.BulkInsertOrUpdate(_trSoldUnitRequirementRepo, soldReq);

                List<MS_Country> listMsProject = new List<MS_Country>();

                for (int i = 0; i < 2; i++)
                {
                    var msProject = new MS_Country
                    {
                        countryCode = "countryCode" + i,
                        countryName = "contryName" + i
                    };

                    listMsProject.Add(msProject);
                };

                _contextProp.BulkInsertOrUpdate(_msCountryRepo, listMsProject);

                Logger.InfoFormat("Bulk INSERT or UPDATE SoldUnitRequirement Total Data: {0}", input.Count);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("BulkInsertOrUpdateTRSoldUnitReq() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BulkInsertOrUpdateTRSoldUnitReq() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("BulkInsertOrUpdateTRSoldUnitReq() - Finished.");
        }

        public void BulkInsertTrxToSoldUnit(List<TR_SoldUnitListDto> data)
        {
            Logger.Info("BulkInsertTrxToSoldUnit() - Started.");
            try
            {
                var options = new DbContextOptions<NewCommDbContext>();
                var dbContext = new NewCommDbContext(options);

                var soldUnit = ObjectMapper.Map<List<TR_SoldUnit>>(data);

                dbContext.BulkInsertOrUpdate(_trSoldUnitRepo, soldUnit);

                // _trSoldUnitRepo.BulkInsertOrUpdate(soldUnit);

                Logger.InfoFormat("Bulk INSERT or UPDATE SoldUnit Total Data: {0}", data.Count);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("BulkInsertTrxToSoldUnit() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BulkInsertTrxToSoldUnit() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("BulkInsertTrxToSoldUnit() - Finished.");
        }

        public void BulkInsertUniversal(int limitAsUplineNo)
        {
            Logger.Info("BulkInsertUniversal() - Started.");

            var getDataSoldUnit = GetDataTrxToInsert(null, null);

            var getDataSoldUnitRequirement = GetDataRequirementToInsert(getDataSoldUnit);

            var getDataCommPct = GetIdForUpdateOrInsertMemberUplineTrCommPct(GetMemberToInsert(getDataSoldUnit, limitAsUplineNo));
            try
            {
                BulkInsertTrxToSoldUnit(getDataSoldUnit);
                BulkInsertOrUpdateTRSoldUnitReq(getDataSoldUnitRequirement);
                BulkInsertOrUpdateMember(getDataCommPct);
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("BulkInsertUniversal() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BulkInsertUniversal() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("BulkInsertUniversal() - Finished.");
        }

        public void BulkInsertManual(BulkInsertManualInputDto input)
        {
            Logger.Info("BulkInsertManual() - Started.");
            var getDataCommPct = GetIdForUpdateOrInsertMemberUplineTrCommPct(GetMemberToInsert(input.data, input.limitAsUplineNo));
            try
            {
                BulkInsertTrxToSoldUnit(input.data);
                BulkInsertOrUpdateTRSoldUnitReq(input.dataReq);
                BulkInsertOrUpdateMember(getDataCommPct);
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("BulkInsertUniversal() ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("BulkInsertUniversal() ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("BulkInsertUniversal() - Finished.");
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
