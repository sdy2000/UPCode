using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Core.Security
{
    public static class FileValidator
    {
        public static bool IsImage(this IFormFile file)
        {
            try
            {
                var img = Image.FromStream(file.OpenReadStream());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIfExiclFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

            return (extension == ".xlsx" || extension == ".xls" || extension == ".bad");
        }
    }
}
