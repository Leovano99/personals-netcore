using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.UI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Project.MS_Departments;
using VDI.Demo.MasterPlan.Project.MS_Officers.Dto;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;

namespace VDI.Demo.MasterPlan.Project.MS_Officers
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterOfficer)]
    public class MsOfficerAppService : DemoAppServiceBase, IMsOfficerAppService
    {
        private readonly IRepository<MS_Officer> _msOfficerRepo;
        private readonly IRepository<MS_Position> _msPositionRepo;
        private readonly IRepository<MS_Department> _msDepartmentRepo;
        private readonly IRepository<MS_Project> _msProjectRepo;
        private readonly IRepository<MP_OfficerProject> _mpOfficerProjectRepo;
        private readonly IRepository<MS_KuasaDireksiPeople> _msKuasaDireksiPeopleRepo;
        private readonly IMsDepartmentAppService _msDepartmentService;

        public MsOfficerAppService
        (
            IRepository<MS_Officer> msOfficerRepo,
            IRepository<MS_Position> msPositionRepo,
            IRepository<MS_Department> msDepartmentRepo,
            IRepository<MS_Project> msProjectRepo,
            IMsDepartmentAppService msDepartmentService,
            IRepository<MP_OfficerProject> mpOfficerProjectRepo,
            IRepository<MS_KuasaDireksiPeople> msKuasaDireksiPeopleRepo
        )
        {
            _msOfficerRepo = msOfficerRepo;
            _msPositionRepo = msPositionRepo;
            _msDepartmentRepo = msDepartmentRepo;
            _msProjectRepo = msProjectRepo;
            _msDepartmentService = msDepartmentService;
            _mpOfficerProjectRepo = mpOfficerProjectRepo;
            _msKuasaDireksiPeopleRepo = msKuasaDireksiPeopleRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterOfficer_Create)]
        [UnitOfWork]
        public GetCreateMsOfficerListDto CreateMsOfficer(CreateMsOfficerInput input)
        {
            Logger.Info("CreateMsOfficer() - Started.");

            var data = new MS_Officer
            {
                officerName = input.officerName,
                officerEmail = input.email,
                officerPhone = input.handphone,
                departmentID = input.departmentID,
                positionID = input.positionID,
                isActive = input.isActive
            };

            try
            {
                Logger.DebugFormat("CreateMsOfficer() - Start insert Officer. Parameters sent:{0}" +
                        "officerName = {1}{0}" +
                        "officerEmail = {2}{0}" +
                        "officerPhone = {3}{0}" +
                        "departmentID = {4}{0}" +
                        "positionID = {5}{0}" +
                        "isActive = {6}{0}"
                        , Environment.NewLine, input.officerName, input.email, input.handphone, input.departmentID
                        , input.positionID, input.isActive);

                int id = _msOfficerRepo.InsertAndGetId(data);
                CurrentUnitOfWork.SaveChanges();

                Logger.DebugFormat("CreateMsOfficer() - Ended insert Officer.");

                var dataReturn = (from A in _msOfficerRepo.GetAll()
                                  join B in _msDepartmentRepo.GetAll() on A.departmentID equals B.Id
                                  where A.Id == id
                                  select new GetCreateMsOfficerListDto
                                  {
                                      officerID = id,
                                      departmentName = B.departmentName,
                                      departmentID = B.Id
                                  }).FirstOrDefault();


                Logger.Info("CreateMsOfficer() - Finished.");

                return dataReturn;
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreateMsOfficer() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreateMsOfficer() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterOfficer_Delete)]
        public void DeleteMsOfficer(int Id)
        {
            Logger.Info("DeleteMsOfficer() - Started.");
            Logger.DebugFormat("DeleteMsOfficer() - Start get data before delete Officer. Parameters sent:{0}" +
                        "officerID = {1}{0}"
                        , Environment.NewLine, Id);

            var checkId = _msProjectRepo.GetAll().Where(x => x.SADStaffID == Id
           || x.SADManagerID == Id || x.PGStaffID == Id || x.SADBMID == Id || x.bankRelationManagerID == Id
           || x.bankRelationStaffID == Id || x.callCenterManagerID == Id || x.callCenterStaffID == Id
           || x.financeManagerID == Id || x.financeStaffID == Id).Any();

            Logger.DebugFormat("DeleteMsOfficer() - Ended checking before delete Officer. Result = {0}", checkId);

            if (checkId)
            {
                Logger.ErrorFormat("DeleteMsOfficer() - ERROR. Result = {0}", "Officer is used by another project!");
                throw new UserFriendlyException("Officer is used by another project!");
            }
            else
            {
                try
                {
                    Logger.DebugFormat("DeleteMsOfficer() - Start delete Officer. Parameters sent:{0}" +
                        "officerID = {1}{0}"
                        , Environment.NewLine, Id);

                    _msOfficerRepo.Delete(Id);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("DeleteMsOfficer() - Ended delete Officer.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsOfficer() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsOfficer() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }

            Logger.Info("DeleteMsOfficer() - Finished.");
        }

        public ListResultDto<GetAllOfficerListDto> GetAllMsOfficer()
        {
            var getAllMsOfficer = (from officer in _msOfficerRepo.GetAll()
                                   join position in _msPositionRepo.GetAll() on officer.positionID equals position.Id
                                   join department in _msDepartmentRepo.GetAll() on officer.departmentID equals department.Id
                                   orderby officer.Id descending
                                   select new GetAllOfficerListDto
                                   {
                                       officerID = officer.Id,
                                       officerName = officer.officerName,
                                       email = officer.officerEmail,
                                       handphone = officer.officerPhone,
                                       departmentID = department.Id,
                                       departmentName = department.departmentName,
                                       positionID = position.Id,
                                       positionName = position.positionName,
                                       isActive = officer.isActive
                                   }).ToList();

            return new ListResultDto<GetAllOfficerListDto>(getAllMsOfficer);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterOfficer_Edit)]
        public JObject UpdateMsOfficer(CreateMsOfficerInput input)
        {
            JObject obj = new JObject();
            Logger.Info("UpdateMsOfficer() - Started.");
            Logger.DebugFormat("UpdateMsOfficer() - Start get data before update Officer. Parameters sent:{0}" +
                        "officerID = {1}{0}"
                        , Environment.NewLine, input.id);

            var checkProject = _msProjectRepo.GetAll().Where(x => x.SADStaffID == input.id
             || x.SADManagerID == input.id || x.PGStaffID == input.id || x.SADBMID == input.id
             || x.bankRelationManagerID == input.id || x.bankRelationStaffID == input.id || x.callCenterManagerID == input.id
             || x.callCenterStaffID == input.id || x.financeManagerID == input.id || x.financeStaffID == input.id).Any();


            var getMsOfficer = (from a in _msOfficerRepo.GetAll()
                                where a.Id == input.id
                                select a).FirstOrDefault();

            var updateMsOfficer = getMsOfficer.MapTo<MS_Officer>();

            updateMsOfficer.officerEmail = input.email;
            updateMsOfficer.officerPhone = input.handphone;
            updateMsOfficer.isActive = input.isActive;

            if (!checkProject)
            {
                updateMsOfficer.officerName = input.officerName;
                updateMsOfficer.positionID = input.positionID;
                updateMsOfficer.departmentID = input.departmentID;

                obj.Add("message", "Edit Successfully");
            }
            else
            {
                obj.Add("message", "Edit Successfully, but can't change Name, Department & Position");
            }

            Logger.Info("UpdateMsOfficer() - Finished.");
            return obj;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject_Create, AppPermissions.Pages_Tenant_MasterProject_Edit)]
        public GetOfficerDivDto GetMsOfficerPerDepartmentDropdown()
        {
            DepartmentDto psas = new DepartmentDto();
            DepartmentDto pg = new DepartmentDto();
            DepartmentDto br = new DepartmentDto();
            DepartmentDto cc = new DepartmentDto();
            DepartmentDto fin = new DepartmentDto();
            DepartmentDto bm = new DepartmentDto();

            var getDepartment = _msDepartmentService.GetMsDepartmentPICInformation();

            foreach (var item in getDepartment.Items)
            {
                var getManager = (from a in _msOfficerRepo.GetAll()
                                  join b in _msPositionRepo.GetAll() on a.positionID equals b.Id
                                  where b.departmentID == item.departmentID && b.isActive == true && (b.positionName.ToLower() == "manager" || b.positionCode.ToLower() == "mng")
                                  orderby a.Id descending
                                  select new ManagerDto
                                  {
                                      managerID = a.Id,
                                      managerName = a.officerName
                                  }).ToList();

                var getStaff = (from a in _msOfficerRepo.GetAll()
                                join b in _msPositionRepo.GetAll() on a.positionID equals b.Id
                                where b.departmentID == item.departmentID && b.isActive == true && b.positionName.ToLower() == "staff"
                                orderby a.Id descending
                                select new StaffDto
                                {
                                    staffID = a.Id,
                                    staffName = a.officerName
                                }).ToList();

                if (item.departmentName == "PSAS")
                {
                    psas.manager = getManager;
                    psas.staff = getStaff;
                    psas.departmentID = item.departmentID;
                }
                else if (item.departmentName == "Product General")
                {
                    pg.staff = getStaff;
                    pg.departmentID = item.departmentID;
                }
                else if (item.departmentName == "Bank Relation")
                {
                    br.manager = getManager;
                    br.staff = getStaff;
                    br.departmentID = item.departmentID;
                }
                else if (item.departmentName == "Call Center")
                {
                    cc.manager = getManager;
                    cc.staff = getStaff;
                    cc.departmentID = item.departmentID;
                }
                else if (item.departmentName == "Finance")
                {
                    fin.manager = getManager;
                    fin.staff = getStaff;
                    fin.departmentID = item.departmentID;
                }
                else if (item.departmentName == "Building Management")
                {
                    bm.manager = getManager;
                    bm.staff = getStaff;
                    bm.departmentID = item.departmentID;
                }
            }

            var listResult = new GetOfficerDivDto
            {
                PSAS = psas,
                ProductGeneral = pg,
                BankRelation = br,
                CallCenter = cc,
                Finance = fin,
                buildingManagement = bm
            };

            return listResult;
        }

        public string GetOfficerPhoneByOfficerId(GetOfficerPhoneByOfficerIdInput input)
        {
            var result = (from x in _msOfficerRepo.GetAll()
                          where x.Id == input.staffID && x.departmentID == input.departmentID
                          select x.officerPhone).FirstOrDefault();

            return result;
        }

        public List<GetOfficerByNameOrPositionListDto> GetOfficerByNameOrPosition(string name = null, string position = null)
        {
            var getOfficer = (from A in _msOfficerRepo.GetAll()
                              join B in _msPositionRepo.GetAll() on A.positionID equals B.Id
                              select new GetOfficerByNameOrPositionListDto
                              {
                                  officerID = A.Id,
                                  officerName = A.officerName,
                                  officerPhone = A.officerPhone,
                                  officerEmail = A.officerEmail,
                                  officerPosition = B.positionName

                              })
                              .WhereIf(!name.IsNullOrWhiteSpace(), item => item.officerName.ToLower().Contains(name.ToLower()))
                              .WhereIf(!position.IsNullOrWhiteSpace(), item => item.officerPosition.ToLower().Contains(position.ToLower()))
                              .ToList();

            return getOfficer;
        }
    }
}
