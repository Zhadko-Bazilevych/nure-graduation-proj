using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Xml.Linq;
using Microsoft.Extensions.Hosting;

namespace recipes_backend.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _hostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveImage(IFormFile imageFile, params string[] subfolders)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '_');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);

            var imageRelativePath = imageName;
            for(int i = subfolders.Length - 1; i >= 0; i--)
            {
                imageRelativePath = Path.Combine(subfolders[i], imageRelativePath);
            }
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, imageRelativePath);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageRelativePath;
        }

        public void DeleteImage(string imageName, params string[] subfolders)
        {
            var imagePath = imageName;
            for (int i = subfolders.Length - 1; i > 0; i--)
            {
                imagePath = Path.Combine(subfolders[i], imagePath);
            }
            imagePath = Path.Combine(_hostEnvironment.ContentRootPath, imagePath);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
