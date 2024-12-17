using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

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

            int newWidth = 200;
            int newHeight = 200;

            var extension = Path.GetExtension(photo.FileName).ToLower();
            ImageFormat imageFormat = GetImageFormat(extension);

            byte[] resizedPhoto = ResizeImage(photo, newWidth, newHeight, imageFormat);

            Directory.CreateDirectory(folder);
            var fileName = Path.GetFileNameWithoutExtension(photo.FileName);
            string filePath = Path.Combine(folder, $"{fileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}");

            try
            {
                File.WriteAllBytes(filePath, resizedPhoto);
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while saving the photo: {ex.Message}", ex);
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

        public byte[] ResizeImage(IFormFile file, int width, int height, ImageFormat imageFormat)
        {
            using (var stream = file.OpenReadStream())
            using (var image = Image.FromStream(stream))
            {
                // Create a new bitmap with the desired dimensions
                var resizedImage = new Bitmap(width, height);

                // Draw the original image onto the resized image
                using (var graphics = Graphics.FromImage(resizedImage))
                {
                    graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    graphics.DrawImage(image, 0, 0, width, height);
                }

                // Save the resized image to a memory stream
                using (var outputStream = new MemoryStream())
                {
                    resizedImage.Save(outputStream, imageFormat);
                    return outputStream.ToArray();
                }
            }
        }

        public ImageFormat GetImageFormat(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                throw new ArgumentException("File extension cannot be null or empty.", nameof(extension));
            }

            string ext = extension.TrimStart('.').ToLowerInvariant();

            return ext switch
            {
                "jpg" or "jpeg" => ImageFormat.Jpeg,
                "png" => ImageFormat.Png,
                "bmp" => ImageFormat.Bmp,
                "gif" => ImageFormat.Gif,
                _ => throw new NotSupportedException($"Unsupported file extension: {extension}")
            };
        }
    }
}
