using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Companies.Dto;
//using VDI.Demo.PropertySystemDB.OnlineBooking.Personal;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using VDI.Demo.PersonalsDB;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_Companies
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrCompany)]
    public class TrCompanyAppService : DemoAppServiceBase, ITrCompanyAppService
    {
        private readonly IRepository<TR_Company, string> _trCompanyRepo;

        #region constructor
        public TrCompanyAppService(
            IRepository<TR_Company, string> trCompanyRepo
            )
        {
            _trCompanyRepo = trCompanyRepo;
        }
        #endregion

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrCompany_Delete)]
        public void DeleteTrCompany(string psCode, int refID)
        {
            var getCompany = (from companies in _trCompanyRepo.GetAll()
                              where companies.entityCode == "1" &&
                                    companies.psCode == psCode &&
                                    companies.refID == refID
                              select companies).FirstOrDefault();

            try
            {
                _trCompanyRepo.Delete(getCompany);
                CurrentUnitOfWork.SaveChanges();
            }
            catch (DataException ex)
            {
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrCompany_Edit)]
        public void UpdateTrCompany(CreateOrUpdateTrCompanyListDto input)
        {
            var getCompany = (from companies in _trCompanyRepo.GetAll()
                              where companies.entityCode == "1"
                              && companies.psCode == input.psCode
                              && companies.refID == input.refID
                              select companies).FirstOrDefault();

            var updateCompany = getCompany.MapTo<TR_Company>();
            if (input.coAddress == null || input.coAddress == "") input.coAddress = "-";
            if (input.coPostCode == null) input.coPostCode = "-";

            updateCompany.coName = input.coName;
            updateCompany.coAddress = String.IsNullOrEmpty(input.coAddress) ? "-" : input.coAddress;
            updateCompany.coCity = input.coCity;
            updateCompany.coPostCode = String.IsNullOrEmpty(input.coPostCode) ? "-" : input.coPostCode;
            updateCompany.coCountry = input.coCountry;
            updateCompany.coType = String.IsNullOrEmpty(input.coType) ? "-" : input.coType;
            updateCompany.jobTitle = input.jobTitle;

            try
            {
                _trCompanyRepo.Update(updateCompany);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
            }
            catch (DataException ex)
            {
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }
    }
}
