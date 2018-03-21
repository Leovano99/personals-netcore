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
        private readonly IRepository<TR_EmailInvalid, string> _trEmailInvalidRepo;

        public TrEmailAppService
            (
                IRepository<TR_Email, string> trEmailRepo,
                IRepository<TR_EmailInvalid, string> trEmailInvalidRepo
            )
        {
            _trEmailRepo = trEmailRepo;
            _trEmailInvalidRepo = trEmailInvalidRepo;
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
            Logger.InfoFormat("DeleteEmail() - Started.");

            if (input.isValid)
            {
                Logger.DebugFormat("DeleteEmail() - Start checking before delete Valid Email. Parameters sent:{0}" +
                        "psCode = {1}{0}" +
                        "refID = {2}{0}"
                        , Environment.NewLine, input.psCode, input.refID);

                var getSetEmail = (from email in _trEmailRepo.GetAll()
                                   where email.entityCode == "1"
                                   && email.psCode == input.psCode
                                   && email.refID == input.refID
                                   select email).FirstOrDefault();

                Logger.DebugFormat("DeleteEmail() - Ended checking before delete Email. Result Email Exist = {0}", getSetEmail != null);

                if (getSetEmail != null)
                {
                    try
                    {
                        Logger.DebugFormat("DeleteEmail() - Start delete Email. Parameters sent:{0}" +
                            "Id = {1}{0}"
                            , Environment.NewLine, getSetEmail.Id);

                        _trEmailRepo.Delete(getSetEmail);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("DeleteEmail() - Ended delete Email.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.ErrorFormat("DeleteEmail() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("DeleteEmail() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("DeleteEmail() - ERROR Result = {0}.", "Email that you search is not exist!");
                    throw new UserFriendlyException("Email is not exist!");
                }
            }
            else
            {
                Logger.DebugFormat("DeleteEmail() - Start checking before delete Invalid Email. Parameters sent:{0}" +
                        "psCode = {1}{0}" +
                        "refID = {2}{0}"
                        , Environment.NewLine, input.psCode, input.refID);

                var getSetEmail = (from email in _trEmailInvalidRepo.GetAll()
                                   where email.entityCode == "1"
                                   && email.psCode == input.psCode
                                   && email.refID == input.refID
                                   select email).FirstOrDefault();

                Logger.DebugFormat("DeleteEmail() - Ended checking before delete Email. Result Email Exist = {0}", getSetEmail != null);

                if (getSetEmail != null)
                {
                    try
                    {
                        Logger.DebugFormat("DeleteEmail() - Start delete Email. Parameters sent:{0}" +
                            "Id = {1}{0}"
                            , Environment.NewLine, getSetEmail.Id);

                        _trEmailInvalidRepo.Delete(getSetEmail);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("DeleteEmail() - Ended delete Email.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.ErrorFormat("DeleteEmail() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("DeleteEmail() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("DeleteEmail() - ERROR Result = {0}.", "Email that you search is not exist!");
                    throw new UserFriendlyException("Email is not exist!");
                }
            }

            Logger.Info("DeleteEmail() - Finished.");
        }
    }
}

