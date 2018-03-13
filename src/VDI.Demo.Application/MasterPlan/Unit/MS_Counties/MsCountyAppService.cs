using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Counties.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Counties
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCounty)]
    public class MsCountyAppService : DemoAppServiceBase, IMsCountyAppService
    {
        private readonly IRepository<MS_County> _msCountyRepo;

        public MsCountyAppService(
            IRepository<MS_County> msCountyRepo
            )
        {
            _msCountyRepo = msCountyRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCounty_Create)]
        public void CreateMsCounty(GetCreateMsCountyInputDto input)
        {
            var cekCountyName = (from A in _msCountyRepo.GetAll()
                                 where A.countyName == input.countyName && A.territoryID == input.territoryID
                                 select A).FirstOrDefault();

            if (cekCountyName == null)
            {
                var createMsCounty = new MS_County
                {
                    countyName = input.countyName,
                    territoryID = input.territoryID
                };

                try
                {
                    _msCountyRepo.Insert(createMsCounty);
                    CurrentUnitOfWork.SaveChanges();
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                                            .SelectMany(x => x.ValidationErrors)
                                            .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
                // Handle data errors.
                catch (DataException exDb)
                {
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                throw new UserFriendlyException("County is exist!");
            }
        }

        public ListResultDto<GetMsCountyListDto> GetAllDropdownMsCounty(int territoryID)
        {
            var dataCounty = (from A in _msCountyRepo.GetAll()
                              where A.territoryID == territoryID
                              select new GetMsCountyListDto
                              {
                                  countyID = A.Id,
                                  countyName = A.countyName,
                                  territoryID = territoryID
                              }).ToList();

            return new ListResultDto<GetMsCountyListDto>(dataCounty);
        }
    }
}
