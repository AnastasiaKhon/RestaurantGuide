using Microsoft.AspNetCore.Http;
using RestaurantGuide.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace RestaurantGuide.Services
{
    public class FileUploadService
    {
        public async void Upload(string path, string fileName, IFormFile file)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
