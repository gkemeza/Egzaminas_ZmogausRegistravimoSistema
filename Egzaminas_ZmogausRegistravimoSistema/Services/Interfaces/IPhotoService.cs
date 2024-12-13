namespace Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces
{
    public interface IPhotoService
    {
        byte[] GetPhotoAsByteArray(string filePath);
        string GetPhotoPath(IFormFile photo, string folder);
    }
}
