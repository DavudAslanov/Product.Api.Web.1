using Microsoft.AspNetCore.Http;

namespace Core.Extenstion
{
    public static class PictureHelper
    {
        public static string UploadImage(this IFormFile formFile, string webRootPath)
        {

            var path = "/Images/" + Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName); 
            var imagePath = Path.Combine(webRootPath, "Images"); 
            var fullPath = Path.Combine(imagePath, Path.GetFileName(path)); 

            if (!Directory.Exists(imagePath)) 
            {
                Directory.CreateDirectory(imagePath);
            }

            using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return path;
        }

        
    }
}
