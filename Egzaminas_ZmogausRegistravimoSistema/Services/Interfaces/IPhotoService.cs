namespace Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces
{
    public interface IPhotoService
    {
        string GetPhotoPath(IFormFile photo, string folder);
    }
}
