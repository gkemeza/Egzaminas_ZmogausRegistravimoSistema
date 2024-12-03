using System.ComponentModel.DataAnnotations;

namespace Egzaminas_ZmogausRegistravimoSistema.Entities
{
    public class Residence
    {
        [Key]
        public long Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int HouseNumber { get; set; }
        public int RoomNumber { get; set; }
    }
}
