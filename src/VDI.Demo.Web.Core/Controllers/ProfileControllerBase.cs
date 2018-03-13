using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Extensions;
using Abp.IO.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using VDI.Demo.Authorization.Users.Profile.Dto;
using VDI.Demo.IO;
using VDI.Demo.Web.Helpers;

namespace VDI.Demo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileControllerBase : DemoControllerBase
    {
        private readonly IAppFolders _appFolders;
        private const int MaxProfilePictureSize = 5242880; //5MB

        protected ProfileControllerBase(IAppFolders appFolders)
        {
            _appFolders = appFolders;
        }

        public UploadProfilePictureOutput UploadProfilePicture()
        {
            try
            {
                var profilePictureFile = Request.Form.Files.First();

                //Check input
                if (profilePictureFile == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if (profilePictureFile.Length > MaxProfilePictureSize)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit", AppConsts.MaxProfilPictureBytesUserFriendlyValue));
                }

                byte[] fileBytes;
                using (var stream = profilePictureFile.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                if (!ImageFormatHelper.GetRawImageFormat(fileBytes).IsIn(ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif))
                {
                    throw new Exception("Uploaded file is not an accepted image file !");
                }

                //Delete old temp profile pictures
                AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder, "userProfileImage_" + AbpSession.GetUserId());

                //Save new picture
                var fileInfo = new FileInfo(profilePictureFile.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".jpeg"))
                {
                    var tempFileName = "userProfileImage_" + AbpSession.GetUserId() + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder, tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                    using (var bmpImage = new Bitmap(tempFilePath))
                    {
                        return new UploadProfilePictureOutput
                        {
                            FileName = tempFileName,
                            Width = bmpImage.Width,
                            Height = bmpImage.Height
                        };
                    }
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return new UploadProfilePictureOutput(new ErrorInfo(ex.Message));
            }
        }

        public JsonResult UploadCompanyImage()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.OpenReadStream());
                var acceptedFormats = new List<ImageFormat>
                {
                    ImageFormat.Jpeg, ImageFormat.Png
                };

                if (!acceptedFormats.Contains(fileImage.RawFormat))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\CompanyImage\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\CompanyImage\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp profile pictures
                //AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder+"/CompanyImage/", "companyImage_" + AbpSession.GetUserId());
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");
                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".jpeg"))
                {

                    var tempFileName = "companyImage_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\CompanyImage\\", tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                    using (var bmpImage = new Bitmap(tempFilePath))
                    {
                        return Json(new AjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                    }
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadProjectImage()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.OpenReadStream());
                var acceptedFormats = new List<ImageFormat>
                {
                    ImageFormat.Jpeg, ImageFormat.Png
                };

                if (!acceptedFormats.Contains(fileImage.RawFormat))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\ProjectImage\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\ProjectImage\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp profile pictures
                //AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder+"/CompanyImage/", "companyImage_" + AbpSession.GetUserId());
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");
                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".jpeg"))
                {

                    var tempFileName = "projectImage_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\ProjectImage\\", tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                    using (var bmpImage = new Bitmap(tempFilePath))
                    {
                        return Json(new AjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                    }
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadDetailImage()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.OpenReadStream());
                var acceptedFormats = new List<ImageFormat>
                {
                    ImageFormat.Jpeg, ImageFormat.Png
                };

                if (!acceptedFormats.Contains(fileImage.RawFormat))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\DetailImage\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\DetailImage\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp profile pictures
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");
                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".jpeg"))
                {

                    var tempFileName = "detailImage_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\DetailImage\\", tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                    using (var bmpImage = new Bitmap(tempFilePath))
                    {
                        return Json(new AjaxResponse(new { fileName = tempFileName, width = bmpImage.Width, height = bmpImage.Height }));
                    }
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadLayoutUnitTypeImage()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var tempFilePaths = new List<string>();
                var fileNames = new List<string>();
                var bitmaps = new List<Bitmap>();
                var ajaxResponse = new AjaxResponse();

                var file = Request.Form.Files[0];

                if (Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.OpenReadStream());
                var acceptedFormats = new List<ImageFormat>
                                            {
                                                ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif
                                            };

                if (!acceptedFormats.Contains(fileImage.RawFormat))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\LayoutUnitTypeImage\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\LayoutUnitTypeImage\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp profile pictures
                //AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder+"/CompanyImage/", "companyImage_" + AbpSession.GetUserId());
                var date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".jpeg"))
                {
                    var tempFileName = "layoutUnitTypeImage_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\LayoutUnitTypeImage\\", tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    tempFilePaths.Add(tempFilePath);
                    Bitmap bitmap = new Bitmap(tempFilePath);
                    bitmaps.Add(bitmap);
                    var parentUrl = ConfigurationManager.AppSettings["WebsiteRootAddress"] + "Temp/Downloads/LayoutUnitTypeImage/";
                    ajaxResponse = new AjaxResponse(new { fileName = tempFileName, width = bitmap.Width, height = bitmap.Height, parentUrl = parentUrl });

                    return Json(ajaxResponse);
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadLayoutBuildingTypeImage()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                var tempFilePaths = new List<string>();
                var fileNames = new List<string>();
                var bitmaps = new List<Bitmap>();
                var ajaxResponse = new AjaxResponse();

                var file = Request.Form.Files[0];

                if (Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("ProfilePicture_Change_Error"));
                }

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("ProfilePicture_Warn_SizeLimit"));
                }

                //Check file type & format
                var fileImage = Image.FromStream(file.OpenReadStream());
                var acceptedFormats = new List<ImageFormat>
                                            {
                                                ImageFormat.Jpeg, ImageFormat.Png, ImageFormat.Gif
                                            };

                if (!acceptedFormats.Contains(fileImage.RawFormat))
                {
                    throw new ApplicationException("Uploaded file is not an accepted image file !");
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\LayoutBuildingTypeImage\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\LayoutBuildingTypeImage\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp profile pictures
                //AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TempFileDownloadFolder+"/CompanyImage/", "companyImage_" + AbpSession.GetUserId());
                var date = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //Save new picture
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".jpg") || ext.Equals(".png") || ext.Equals(".gif") || ext.Equals(".jpeg"))
                {
                    var tempFileName = "layoutBuildingTypeImage_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\LayoutBuildingTypeImage\\", tempFileName);
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    tempFilePaths.Add(tempFilePath);
                    Bitmap bitmap = new Bitmap(tempFilePath);
                    bitmaps.Add(bitmap);
                    ajaxResponse = new AjaxResponse(new { fileName = tempFileName, width = bitmap.Width, height = bitmap.Height });

                    return Json(ajaxResponse);
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadPersonalFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > MaxProfilePictureSize)
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }
                
                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\PersonalFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\PersonalFile\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var date = DateTime.Now.ToString("yyyyMMddHHmmss");
                                
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".pdf") || ext.Equals(".jpg") || ext.Equals(".jpeg") || ext.Equals(".png"))
                {
                    var tempFileName = "personalFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\PersonalFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (UserFriendlyException ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        //Upload PDF / Another File
        public JsonResult UploadRenovationFile()//pdf
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\RenovationFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\RenovationFile\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".pdf") || ext.Equals(".jpg") || ext.Equals(".jpeg") || ext.Equals(".png"))
                {
                    var tempFileName = "renovationFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\RenovationFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        //Upload PDF / Another File Schema
        public JsonResult UploadSchemaFile()//pdf xls
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\SchemaFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\SchemaFile\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".pdf") || ext.Equals(".xls") || ext.Equals(".xlsx"))
                {
                    var tempFileName = "schemaFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\SchemaFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        //Upload PDF / Another File Schema Per Project
        public JsonResult UploadSchemaPerProjectFile()//pdf xls
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\SchemaPerProjectFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\SchemaPerProjectFile\\");
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".pdf") || ext.Equals(".xls") || ext.Equals(".xlsx"))
                {

                    var tempFileName = "schemaPerProjectFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\SchemaPerProjectFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
        //Upload PDF / Another File
        public JsonResult UploadUnitFile()//pdf xls
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\UnitFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\UnitFile\\");
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".pdf") || ext.Equals(".xls") || ext.Equals(".xlsx"))
                {

                    var tempFileName = "unitFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\UnitFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        //Upload PDF / Another File
        public JsonResult UploadPriceListFile()//xls
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\PriceListFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\PriceListFile\\");
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".xls") || ext.Equals(".xlsx"))
                {

                    var tempFileName = "priceListFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\PriceListFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        //Upload PDF / Another File
        public JsonResult UploadGrossPriceFile()//xls
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + "\\GrossPriceFile\\"))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + "\\GrossPriceFile\\");
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (ext.Equals(".xls") || ext.Equals(".xlsx"))
                {
                    var tempFileName = "grossPriceFile_" + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + "\\GrossPriceFile\\", tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);
                    //var fileName = Path.GetFileName(tempFilePath);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Uploaded file format is not correct !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }

        public JsonResult UploadKuasaDireksi()
        {
            List<string> typeFile = new List<string>();
            typeFile.Add(".pdf");
            typeFile.Add(".doc");

            var returnNaming = NamingFolderUpload("SK_", "\\LegalDocument\\KuasaDireksi\\", typeFile);

            return returnNaming;
        }

        public JsonResult UploadKuasaDireksiPeople()
        {
            List<string> typeFile = new List<string>();
            typeFile.Add(".pdf");
            typeFile.Add(".jpg");
            typeFile.Add(".jpeg");
            typeFile.Add(".png");

            var returnNaming = NamingFolderUpload("Signature_", "\\LegalDocument\\KuasaDireksiPeople\\", typeFile);

            return returnNaming;
        }

        public JsonResult UploadTandaTerima()
        {
            List<string> typeFile = new List<string>();
            typeFile.Add(".pdf");
            typeFile.Add(".jpg");
            typeFile.Add(".jpeg");
            typeFile.Add(".png");

            var returnNaming = NamingFolderUpload("TandaTerima_", "\\LegalDocument\\TandaTerima\\", typeFile);

            return returnNaming;
        }

        private JsonResult NamingFolderUpload(string fileName, string folderName, List<string> typeFile)
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count <= 0 || Request.Form.Files[0] == null)
                {
                    throw new UserFriendlyException(L("File_Change_Error"));
                }

                var file = Request.Form.Files[0];

                if (file.Length > 1048576) //1MB.
                {
                    throw new UserFriendlyException(L("File_Warn_SizeLimit"));
                }

                if (!Directory.Exists(_appFolders.TempFileDownloadFolder + folderName))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(_appFolders.TempFileDownloadFolder + folderName);
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                //Delete old temp pdf files
                var date = DateTime.Now.ToString("yyyyMMddHHmmss");

                //Save new file
                var fileInfo = new FileInfo(file.FileName);
                string ext = fileInfo.Extension.ToLower().Trim();
                if (typeFile.Contains(ext))
                {
                    var tempFileName = fileName + date + fileInfo.Extension;
                    var tempFilePath = Path.Combine(_appFolders.TempFileDownloadFolder + folderName, tempFileName);
                    tempFilePath.Replace("\\\\", "\\");
                    System.IO.File.WriteAllBytes(tempFilePath, fileBytes);

                    return Json(new AjaxResponse(new { fileName = tempFileName }));
                }
                else
                {
                    throw new UserFriendlyException("Type file " + ext + " is not allowed !");
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResponse(new ErrorInfo(ex.Message)));
            }
        }
    }
}