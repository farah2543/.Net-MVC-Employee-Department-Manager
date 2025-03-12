using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Services.AttachmentServices
{
    public class AttachmentService : IAttachmentService
    {
        public readonly List<string> _allowedExtensions = new() { ".png" , ".jpg" , "jpeg"};

        public const int _maxAllowedSize = 2_097_152;

        public string? Upload(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName);

            if (!_allowedExtensions.Contains(extension))
            {
                return null;
            }
            if(file.Length > _maxAllowedSize)
            {
                return null;
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files",folderName);

            var fileName = $"{Guid.NewGuid()}{extension}";

            var filePath = Path.Combine(folderPath,fileName);

            using var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;

        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

       
    }
}
