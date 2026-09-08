using ECommerce.Contract.MediaService;
using ECommerce.Data.DTO;
using ECommerce.Ground;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Presistance.MediaService
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IHostEnvironment _environment;
        public LocalFileStorageService(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<GenericUploadedFileDTO>> UploadAsync(List<IFormFile> files, string folderName)
        {
            List<GenericUploadedFileDTO> uploads = new List<GenericUploadedFileDTO>();
            foreach (var file in files)
            {
                Guid mediaId = Guid.NewGuid();
                var uploadsFolder = Path.Combine(_environment.ContentRootPath, Constants.Media.UploadsFolderName, folderName, mediaId.ToString());

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder,file.FileName);

                using (FileStream fileStream = File.Create(filePath))
                {
                    await file.CopyToAsync(fileStream);
                }
                uploads.Add(new GenericUploadedFileDTO { MediaId = mediaId, FilePath = Path.Combine(uploadsFolder, file.FileName) });
            }
            return uploads;
        }
    }
}
