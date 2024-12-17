using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Database.InitialData
{
    public static class ResidenceDataSeed
    {
        public static List<Residence> Residences => new()
        {
            GetResidence1()
        };

        private static Residence GetResidence1()
        {
            return new Residence
            {
                Id = 1,
                City = "City1",
                Street = "Street1",
                HouseNumber = 1,
                RoomNumber = 1,
                PersonInfoId = 1
            };
        }
    }
}
