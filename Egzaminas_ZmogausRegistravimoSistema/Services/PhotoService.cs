using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;

namespace Egzaminas_ZmogausRegistravimoSistema.Services
{
    public class PhotoService : IPhotoService
    {
        public string GetPhotoPath(IFormFile photo, string folder)
        {
            if (photo == null || photo.Length == 0)
            {
                throw new ArgumentException("Photo file is invalid.");
            }

            Directory.CreateDirectory(folder);
            var extension = Path.GetExtension(photo.FileName).ToLower();
            var fileName = Path.GetFileNameWithoutExtension(photo.FileName);
            string filePath = Path.Combine(folder, $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return filePath;
        }

        public byte[] GetPhotoAsByteArray(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }

            return File.ReadAllBytes(filePath);
        }
    }
}
