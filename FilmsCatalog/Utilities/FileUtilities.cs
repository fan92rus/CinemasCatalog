using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.Services
{
    class FileUtilities
    {
        public static bool TryUploadedFile(IFormFile file, string directory, out string path)
        {
            path = null;
            if (file == null) return false;

            var webRootPath = (string)AppDomain.CurrentDomain.GetData("ContentRootPath");

            var uploadsFolder = Path.Combine(webRootPath, "wwwroot", directory);

            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);
            path = $"~/{directory}/{uniqueFileName}";
            return true;
        }
    }
}