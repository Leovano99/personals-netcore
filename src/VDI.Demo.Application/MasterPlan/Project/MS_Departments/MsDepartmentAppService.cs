using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Project.MS_Departments.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.MasterPlan.Project.MS_Departments
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDepartment)]
    public class MsDepartmentAppService : DemoAppServiceBase, IMsDepartmentAppService
    {
        private readonly IRepository<MS_Department> _msDepartmentRepo;
        private readonly IRepository<MS_Officer> _msOfficerRepo;
        private readonly IRepository<MS_Position> _msPositionRepo;

        public MsDepartmentAppService
        (
            IRepository<MS_Department> msDepartmentRepo,
            IRepository<MS_Officer> msOfficer,
            IRepository<MS_Position> msPosition
        )
        {
            _msDepartmentRepo = msDepartmentRepo;
            _msOfficerRepo = msOfficer;
            _msPositionRepo = msPosition;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDepartment)]
        public ListResultDto<GetAllDepartmentListDto> GetAllMsDepartment()
        {
            var getAllDepartment = (from A in _msDepartmentRepo.GetAll()
                                    orderby A.Id descending
                                    select new GetAllDepartmentListDto
                                    {
                                        departmentID = A.Id,
                                        departmentName = A.departmentName,
                                        departmentCode = A.departmentCode,
                                        departmentWhatsapp = A.departmentWhatsapp,
                                        departmentEmail = A.departmentEmail,
                                        isActive = A.isActive
                                    }).ToList();

            return new ListResultDto<GetAllDepartmentListDto>(getAllDepartment);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterProject)]
        public ListResultDto<GetMsDepartmentDropdownListDto> GetMsDepartmentPICInformation()
        {
            var listResult = (from x in _msDepartmentRepo.GetAll()
                              where
                              x.departmentName == "PSAS" ||
                              x.departmentName == "Product General" ||
                              x.departmentName == "Bank Relation" ||
                              x.departmentName == "Call Center" ||
                              x.departmentName == "Finance" ||
                              x.departmentName == "Building Management"
                              orderby x.departmentName ascending
                              select new GetMsDepartmentDropdownListDto
                              {
                                  departmentID = x.Id,
                                  departmentName = x.departmentName,
                                  departmentCode = x.departmentCode,
                                  departmentEmail = x.departmentEmail,
                                  departmentWhatsapp = x.departmentWhatsapp
                              }).ToList();

            return new ListResultDto<GetMsDepartmentDropdownListDto>(listResult);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDepartment_Create)]
        public void CreateMsDepartment(CreateOrUpdateMsDepartmentInput input)
        {
            Logger.Info("CreateMsDepartment() - Started.");

            Logger.DebugFormat("CreateMsDepartment() - Start checking exiting code and name. Params sent:{0}" +
                "departmentCode   ={1}{0}" +
                "departmentName   ={2}"
                , Environment.NewLine, input.departmentCode, input.departmentName);
            var checkDepartmentCodeName = (from A in _msDepartmentRepo.GetAll()
                                           where A.departmentCode == input.departmentCode || A.departmentName == input.departmentName
                                           select A).Any();

            Logger.DebugFormat("CreateMsDepartment() - End checking exiting code and name. Result: {0}", checkDepartmentCodeName);

            if (!checkDepartmentCodeName)
            {
                var createMsdepartment = new MS_Department
                {
                    departmentName = input.departmentName,
                    departmentCode = input.departmentCode,
                    departmentEmail = input.departmentEmail,
                    departmentWhatsapp = input.departmentWhatsapp,
                    isActive = input.isActive
                };

                try
                {
                    Logger.DebugFormat("CreateMsDepartment() - Start insert depertment. Params sent:{0}" +
                    "departmentName   = {1}{0}" +
                    "departmentCode   = {2}{0}" +
                    "departmentEmail  = {3}{0}" +
                    "departmentWhatsapp = {4}{0}" +
                    "isActive         = {5}"
                    , Environment.NewLine, input.departmentName, input.departmentCode, input.departmentEmail, input.departmentWhatsapp, input.isActive);
                    _msDepartmentRepo.Insert(createMsdepartment);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("CreateMsDepartment() - End insert department.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMsDepartment() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMsDepartment() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                Logger.ErrorFormat("CreateMsDepartment() ERROR . Result = {0}", "department Code or department Name Already Exist !");
                throw new UserFriendlyException("Department Code or Department Name Already Exist !");
            }
            Logger.Info("CreateMsDepartment() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDepartment_Edit)]
        public JObject UpdateMsDepartment(CreateOrUpdateMsDepartmentInput input)
        {
            Logger.Info("UpdateMsDepartment() - Started.");
            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMsDepartment() - Start checking exiting code and name. Params sent:{0}" +
                "departmentCode   = {1}{0}" +
                "departmentName   = {2}{0}" +
                "departmentID     = {3}"
                , Environment.NewLine, input.departmentCode, input.departmentName, input.departmentID);

            var checkdepartmentNameCode = (from A in _msDepartmentRepo.GetAll()
                                           where A.Id != input.departmentID && (A.departmentCode == input.departmentCode || A.departmentName == input.departmentName)
                                           select A).Any();

            var checkPosition = (from A in _msPositionRepo.GetAll()
                                 where A.departmentID == input.departmentID
                                 select A).Any();

            var checkOfficer = (from A in _msOfficerRepo.GetAll()
                                where A.departmentID == input.departmentID
                                select A).Any();

            Logger.DebugFormat("UpdateMsDepartment() - End checking exiting code and name. Result: {0}", checkdepartmentNameCode);

            if (!checkdepartmentNameCode)
            {
                var getMsDepartment = (from A in _msDepartmentRepo.GetAll()
                                       where input.departmentID == A.Id
                                       select A).FirstOrDefault();

                var updateMsdepartment = getMsDepartment.MapTo<MS_Department>();

                updateMsdepartment.departmentEmail = input.departmentEmail;
                updateMsdepartment.departmentWhatsapp = input.departmentWhatsapp;
                updateMsdepartment.isActive = input.isActive;

                if (!checkPosition && !checkOfficer)
                {
                    if (getMsDepartment.departmentName == "PSAS" || getMsDepartment.departmentName == "Product General" ||
                    getMsDepartment.departmentName == "Bank Relation" || getMsDepartment.departmentName == "Call Center" ||
                    getMsDepartment.departmentName == "Finance" || getMsDepartment.departmentName == "Building Management")
                    {
                        obj.Add("message", "Edit Successfully, but can't change is Active, Department Name & Code");
                    }
                    else
                    {
                        updateMsdepartment.departmentName = input.departmentName;
                        updateMsdepartment.departmentCode = input.departmentCode;
                        obj.Add("message", "Edit Successfully");
                    }

                }
                else
                {
                    obj.Add("message", "Edit Successfully, but can't change Department Code & Name");
                }

                try
                {
                    Logger.DebugFormat("UpdateMsDepartment() - Start update department. Params sent:{0}" +
                    "departmentName   = {1}{0}" +
                    "departmentCode   = {2}{0}" +
                    "departmentEmail  = {3}{0}" +
                    "departmentWhatsapp = {4}{0}" +
                    "isActive         = {5}"
                    , Environment.NewLine, input.departmentName, input.departmentCode, input.departmentEmail, input.departmentWhatsapp, input.isActive);
                    _msDepartmentRepo.Update(updateMsdepartment);
                    CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                    Logger.DebugFormat("UpdateMsDepartment() - End update department.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("UpdateMsDepartment() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMsDepartment() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMsDepartment() Department Code Or Name already exist. Result = {0}", checkdepartmentNameCode);
                throw new UserFriendlyException("Department Code Or Name already exist!");
            }

            Logger.Info("UpdateMsDepartment() - Finished.");
            return obj;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterDepartment_Delete)]
        public void DeleteMsDepartment(int Id)
        {
            Logger.Info("DeleteMsDepartment() - Started.");

            Logger.DebugFormat("DeleteMsDepartment() - Start checking data position with departmentID: {0}", Id);
            var checkPosition = _msPositionRepo.GetAll().Where(x => x.departmentID == Id).Any();
            Logger.DebugFormat("DeleteMsDepartment() - End checking data position. Result: {0}", checkPosition);

            Logger.DebugFormat("DeleteMsDepartment() - Start checking data officer with departmentID: {0}", Id);
            var checkOfficer = _msOfficerRepo.GetAll().Where(x => x.departmentID == Id).Any();
            Logger.DebugFormat("DeleteMsDepartment() - End checking data officer. Result: {0}", checkOfficer);

            if (!checkPosition && !checkOfficer)
            {
                try
                {
                    var getMsDepartment = (from A in _msDepartmentRepo.GetAll()
                                           where A.Id == Id
                                           select A).FirstOrDefault();

                    if (getMsDepartment.departmentName == "PSAS" || getMsDepartment.departmentName == "Product General" || getMsDepartment.departmentName == "Bank Relation" ||
                        getMsDepartment.departmentName == "Call Center" || getMsDepartment.departmentName == "Finance" || getMsDepartment.departmentName == "Building Management")
                    {
                        throw new UserFriendlyException("Data cannot be deleted!!");
                    }
                    else
                    {
                        Logger.DebugFormat("DeleteMsDepartment() - Start delete department. Params sent:{0}", Id);
                        _msDepartmentRepo.Delete(Id);
                        CurrentUnitOfWork.SaveChanges(); //execution saved inside try
                        Logger.DebugFormat("DeleteMsDepartment() - End delete department.");
                    }
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("DeleteMsDepartment() ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteMsDepartment() ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }

            }
            else
            {
                Logger.ErrorFormat("DeleteMsDepartment() ERROR. Result = {0}", "This department is used!");
                throw new UserFriendlyException("This department is used!");
            }
            Logger.Info("DeleteMsDepartment() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterOfficer, AppPermissions.Pages_Tenant_MasterPosition)]
        public ListResultDto<GetMsDepartmentDropdownListDto> GetMsDepartmentDropdown()
        {
            var listResult = (from x in _msDepartmentRepo.GetAll()
                              orderby x.Id descending
                              where x.isActive == true
                              select new GetMsDepartmentDropdownListDto
                              {
                                  departmentID = x.Id,
                                  departmentName = x.departmentName
                              }).ToList();

            return new ListResultDto<GetMsDepartmentDropdownListDto>(listResult);
        }

    }
}
