using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Documents.Dto;
using VDI.Demo.PersonalsDB;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using System.IO;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using VDI.Demo.Files;
using VDI.Demo.Authorization;
using Abp.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace VDI.Demo.Personals.TR_Documents
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrDocument)]
    public class TrDocumentAppService : DemoAppServiceBase, ITrDocumentAppService
    {
        private readonly IRepository<TR_Document, string> _documentRepo;
        private static IHttpContextAccessor _HttpContextAccessor;
        private readonly FilesHelper _filesHelper;
        private readonly IHostingEnvironment _hostingEnvironment;

        #region constructor
        public TrDocumentAppService
            (
                IRepository<TR_Document, string> documentRepo,
                IHttpContextAccessor httpContextAccessor,
                FilesHelper filesHelper,
                 IHostingEnvironment environment
            )
        {
            _documentRepo = documentRepo;
            _HttpContextAccessor = httpContextAccessor;
            _filesHelper = filesHelper;
            _hostingEnvironment = environment;
        }
        #endregion

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrDocument_Edit)]
        public void UpdateDocument(UpdateDocumentDto input)
        {
            var getGetDoc = (from document in _documentRepo.GetAll()
                             where document.entityCode == "1"
                             && document.psCode == input.psCode
                             && document.documentType == input.documentType
                             select document).FirstOrDefault();

            var update = getGetDoc.MapTo<TR_Document>();

            if (input.documentBinary == "updated")
            {
                update.documentBinary = input.documentBinaryNew;

            }
            else if (input.documentBinary == "existing")
            {
                update.documentBinary = getGetDoc.documentBinary;
            }

            try
            {
                _documentRepo.UpdateAsync(update);
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

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrDocument_Delete)]
        public void deleteDocument(string psCode, string documentType)
        {
            var getGetDoc = (from document in _documentRepo.GetAll()
                             where document.entityCode == "1"
                             && document.psCode == psCode
                             && document.documentType == documentType
                             select document).FirstOrDefault();

            if (getGetDoc != null)
            {
                //var fileToDelete = getGetDoc.documentBinary;

                //if (fileToDelete != null && fileToDelete != "")
                //{
                //    DeleteFile(fileToDelete);
                //}

                try
                {
                    _documentRepo.Delete(getGetDoc);
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
                throw new UserFriendlyException("Document that you looking for, is not exist");
            }

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

        #region function helper : UploadImage

        private string UploadFile(string filename)
        {
            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\PersonalFile\", @"Assets\Upload\PersonalFile\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        private void DeleteFile(string filename)
        {
            var filenameToDelete = filename.Split(@"/");
            var nameFileToDelete = filenameToDelete[filenameToDelete.Count() - 1];
            var deletePath = @"\Assets\Upload\PersonalFile\";

            var deleteImage = _hostingEnvironment.WebRootPath + deletePath + nameFileToDelete;

            if (File.Exists(deleteImage))
            {
                var file = new FileInfo(deleteImage);
                file.Delete();
            }
        }
        #endregion
    }
}
