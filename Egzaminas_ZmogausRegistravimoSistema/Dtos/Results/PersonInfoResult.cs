namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Results
{
    public class PersonInfoResult
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public long PersonalIdNumber { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public byte[] PhotoBytes { get; set; } = null!;
        public ResidenceResult Residence { get; set; } = null!;
    }
}
