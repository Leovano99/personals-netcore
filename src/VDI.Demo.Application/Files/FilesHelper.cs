using Abp.UI;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace VDI.Demo.Files
{
    public class FilesHelper : IFilesHelper
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilesHelper(
            IHostingEnvironment hostingEnvironment, 
            ILogger logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public string MoveFiles(string filename, string oldPath, string newPath)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(webRootPath, oldPath, filename);
            var newImagePath = Path.Combine(webRootPath, newPath, "m-" + filename);
            var oldFolderPath = Path.Combine(webRootPath, oldPath);
            var newFolderPath = Path.Combine(webRootPath, newPath);
            var newImageUrl = getAbsoluteUri() + newPath + "m-" + filename;
            
            _logger.InfoFormat("uploadFile() Started.");
            try
            {
                if (!Directory.Exists(oldFolderPath))
                {
                    Directory.CreateDirectory(oldFolderPath);
                }

                if (!Directory.Exists(newFolderPath))
                {
                    Directory.CreateDirectory(newFolderPath);
                }

                if (!File.Exists(oldImagePath))
                    throw new FileNotFoundException();

                if (File.Exists(newImagePath))
                    File.Delete(newImagePath);

                _logger.InfoFormat("uploadFile() - Start Move Image to: {0}", newFolderPath);
                var file = new FileInfo(oldImagePath);
                file.MoveTo(newImagePath);
                _logger.InfoFormat("uploadFile() - End Move Image");

                _logger.InfoFormat("uploadFile() - Finished.");
                return newImageUrl.Replace(@"\", "/");
            }
            catch (FileNotFoundException exFn)
            {
                _logger.DebugFormat("MoveFiles() - ERROR FileNotFoundException. Result = {0}", exFn.Message);
                throw new UserFriendlyException("File Not Found !", exFn.Message);
            }
        }

        public string MoveFilesLegalDoc(string filename, string oldPath, string newPath)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(webRootPath, oldPath, filename);
            var newImagePath = Path.Combine(webRootPath, newPath, filename);
            var oldFolderPath = Path.Combine(webRootPath, oldPath);
            var newFolderPath = Path.Combine(webRootPath, newPath);
            var newImageUrl = getAbsoluteUri() + newPath + filename;

            _logger.InfoFormat("uploadFile() Started.");
            try
            {
                if (!Directory.Exists(oldFolderPath))
                {
                    Directory.CreateDirectory(oldFolderPath);
                }

                if (!Directory.Exists(newFolderPath))
                {
                    Directory.CreateDirectory(newFolderPath);
                }

                if (!File.Exists(oldImagePath))
                    throw new FileNotFoundException();

                if (File.Exists(newImagePath))
                    File.Delete(newImagePath);

                _logger.InfoFormat("uploadFile() - Start Move Image to: {0}", newFolderPath);
                var file = new FileInfo(oldImagePath);
                file.MoveTo(newImagePath);
                _logger.InfoFormat("uploadFile() - End Move Image");

                _logger.InfoFormat("uploadFile() - Finished.");
                return newImageUrl.Replace(@"\", "/");
            }
            catch (FileNotFoundException exFn)
            {
                _logger.DebugFormat("MoveFiles() - ERROR FileNotFoundException. Result = {0}", exFn.Message);
                throw new UserFriendlyException("File Not Found !", exFn.Message);
            }
        }

        private string getAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");
            return result;
        }

        public string ConvertIdToCode(int? Id)
        {
            if (Id != null)
            {
                var mappingTemplateCode = Id.ToString();
                return string.Format("M{0}", mappingTemplateCode);
            }
            else
            {
                return null;
            }
        }

        public string ConvertDocIdToDocCode(int Id)
        {
            var docCode = Id.ToString().PadLeft(4, '0');
            if (docCode.Length > 4)
            {
                throw new UserFriendlyException("Maximum Code is 9999!");
            }
            else
            {
                return docCode;
            }
        }
    }
}
