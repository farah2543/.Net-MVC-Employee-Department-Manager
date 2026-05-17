using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Services.AttachmentServices
{
    public interface IAttachmentService
    {
        public Task<string?> UploadAsync(IFormFile file, string folderName);

        public bool DeleteAsync(string FilePath);



    }
}
