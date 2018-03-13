using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Territories.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Territories
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterTerritory)]
    public class MsTerritoryAppService : DemoAppServiceBase, IMsTerritoryAppService
    {
        
        private readonly IRepository<MS_Territory> _msTerritoryRepo;

        public MsTerritoryAppService(
            IRepository<MS_Territory> msTerritoryRepo
            )
        {
            _msTerritoryRepo = msTerritoryRepo;
        }

        public ListResultDto<GetMsTerritoryListDto> GetAllDropdownMsTerritory()
        {
            var dataTerritory = (from A in _msTerritoryRepo.GetAll()
                                 select new GetMsTerritoryListDto
                                 {
                                     territoryID = A.Id,
                                     territoryName = A.territoryName
                                 }).ToList();

            return new ListResultDto<GetMsTerritoryListDto>(dataTerritory);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterTerritory_Create)]
        public void CreateMsTerritory(GetCreateMsTerritoryInputDto input)
        {
            var cekTerritoryName = (from A in _msTerritoryRepo.GetAll()
                                    where A.territoryName == input.territoryName
                                    select A).FirstOrDefault();

            if (cekTerritoryName == null)
            {
                var createMsTerritory = new MS_Territory
                {
                    territoryName = input.territoryName
                };

                try
                {
                    _msTerritoryRepo.Insert(createMsTerritory);
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
                throw new UserFriendlyException("Territory that you want to add is exist!");
            }
        }
    }
}
