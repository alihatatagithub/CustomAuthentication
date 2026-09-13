using ECommerce.Contract.MediaService;
using ECommerce.Data.DTO;
using ECommerce.Ground;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Presistance.MediaService
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;
        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<GenericUploadedFileDTO>> UploadAsync(List<IFormFile> files, string folderName)
        {
            List<GenericUploadedFileDTO> uploads = new List<GenericUploadedFileDTO>();
            foreach (var file in files)
            {
                Guid mediaId = Guid.NewGuid();
                var filePath = Path.Combine(Constants.Media.UploadsFolderName, folderName, mediaId.ToString());
                var uploadsFolder = Path.Combine(_environment.WebRootPath, filePath);

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var serverFilePath = Path.Combine(uploadsFolder,file.FileName);

                using (FileStream fileStream = File.Create(serverFilePath))
                {
                    await file.CopyToAsync(fileStream);
                }
                uploads.Add(new GenericUploadedFileDTO { MediaId = mediaId, FilePath = Path.Combine(filePath, file.FileName) });
            }
            return uploads;
        }
    }
}
