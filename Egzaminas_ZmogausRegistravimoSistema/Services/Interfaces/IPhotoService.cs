using Egzaminas_ZmogausRegistravimoSistema.Entities;
using System.Drawing.Imaging;

namespace Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces
{
    public interface IPhotoService
    {
        ImageFormat GetImageFormat(string extension);
        byte[] GetPhotoAsByteArray(string filePath);
        string GetPhotoPath(IFormFile photo, string folder);
        byte[] ResizeImage(IFormFile file, int width, int height, ImageFormat imageFormat);
        void UpdateUserPhoto(User user, IFormFile newPhoto, string uploadDirectory);
    }
}
