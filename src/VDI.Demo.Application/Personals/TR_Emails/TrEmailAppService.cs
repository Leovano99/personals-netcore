using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Emails.Dto;
using VDI.Demo.PersonalsDB;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_Emails
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrEmail)]
    public class TrEmailAppService : DemoAppServiceBase, ITrEmailAppService
    {
        #region constructor
        private readonly IRepository<TR_Email, string> _trEmailRepo;

        public TrEmailAppService
            (
                IRepository<TR_Email, string> trEmailRepo
            )
        {
            _trEmailRepo = trEmailRepo;
        }
        #endregion

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrEmail_Edit)]
        public void UpdateEmail(List<GetUpdateEmailInputDto> inputs)
        {
            foreach (var input in inputs)
            {
                var getSetPhone = (from email in _trEmailRepo.GetAll()
                                   where email.entityCode == "1"
                                   && email.psCode == input.psCode
                                   && email.refID == input.refID
                                   select email).FirstOrDefault();

                if (getSetPhone != null)
                {
                    var data = getSetPhone.MapTo<TR_Email>();
                    data.email = input.email;

                    try
                    {
                        _trEmailRepo.Update(data);
                        CurrentUnitOfWork.SaveChanges();
                    }
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
                    throw new UserFriendlyException("Phone that you search is not exist!");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrEmail_Delete)]
        public void DeleteEmail(GetDeleteEmailInputDto input)
        {
            var getSetPhone = (from email in _trEmailRepo.GetAll()
                               where email.entityCode == "1"
                               && email.psCode == input.psCode
                               && email.refID == input.refID
                               select email).FirstOrDefault();

            if (getSetPhone != null)
            {
                try
                {
                    _trEmailRepo.Delete(getSetPhone);
                    CurrentUnitOfWork.SaveChanges();
                }
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
                throw new UserFriendlyException("Phone that you search is not exist!");
            }
        }
    }
}

