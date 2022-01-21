
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Helpers.File
{
    public class FileHelper
    {
        private static IHostingEnvironment _env;
        public static FileModel Upload(IFormFile file, IHostingEnvironment root)
        {
            _env = root;
            try
            {
                if (file.Length > 0)
                {
                    string fullPath;
                    string extension = Path.GetExtension(file.FileName);
                    Guid guid = Guid.NewGuid();
                    var nameWithGuid = guid.ToString() + extension;
                    var extensionToControl = extension.ToLower();

                    if (extensionToControl == ".jpg" || extensionToControl == ".png")
                    {
                        string folder = "Images";
                        fullPath = CreateFolder(folder, nameWithGuid);
                        WriteStream(fullPath, file);
                    }

                    else if (extensionToControl == ".pdf" || extensionToControl == ".odt" ||
                        extensionToControl == ".txt" || extensionToControl == ".7z" ||
                        extensionToControl == ".zip")
                    {
                        string folder = "Docs";
                        fullPath = CreateFolder(folder, nameWithGuid);
                        WriteStream(fullPath, file);

                    }

                    else
                    {
                        throw new NotSupportedException("Bu dosya türü desteklenmemektedir.");
                    }
                    var model = CreateFileModel(fullPath);
                    return model;
                }
                else
                {
                    throw new NullReferenceException("Dosya boş olamaz");
                }
            }
            catch (Exception)
            {

                throw new NullReferenceException("Dosya boş olamaz");
            }
          
        }
        public static string CreateFolder(string folder, string guid)
        {
            var root = _env.WebRootPath;
            var newfolder = Path.Combine(root, folder);
            if (!Directory.Exists(newfolder))
            {
                Directory.CreateDirectory(newfolder);
            }
            var fullPath = Path.Combine(newfolder, guid);
            return fullPath;
        }
        public static void WriteStream(string fullPath, IFormFile file)
        {
            using (var stream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                file.CopyTo(stream);
            }
        }
        public static FileModel CreateFileModel(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            FileModel model = new FileModel
            {
                Name = fileInfo.Name,
                FullName = fileInfo.FullName,
                Length = fileInfo.Length,
                Extension = fileInfo.Extension,
                DirectoryName = fileInfo.DirectoryName,
                Directory = fileInfo.Directory,
                CreationTime = fileInfo.CreationTime
            };
            return model;
        }

        public static async Task<FileResponseModel> DownloadFileAsync(FileDocument fileDocument)
        {
            string path = fileDocument.FullPath;

            if (!System.IO.File.Exists(path)) 
                throw new Exception("Dosya Bulunamadı");

            var bytes = await System.IO.File.ReadAllBytesAsync(path);
            return new FileResponseModel(bytes, fileDocument.ContentType, fileDocument.DocumentTitle);

        }
    }
}
