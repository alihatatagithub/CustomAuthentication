using ECommerce.Data.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.MediaService
{
    public interface IFileStorageService
    {
        Task<List<GenericUploadedFileDTO>> UploadAsync(List<IFormFile> files, string folderName);
    }
}
