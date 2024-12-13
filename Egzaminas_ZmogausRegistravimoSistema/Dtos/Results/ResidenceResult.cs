namespace Egzaminas_ZmogausRegistravimoSistema.Dtos.Results
{
    public class ResidenceResult
    {
        public long Id { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public int HouseNumber { get; set; }
        public int RoomNumber { get; set; }
    }
}
