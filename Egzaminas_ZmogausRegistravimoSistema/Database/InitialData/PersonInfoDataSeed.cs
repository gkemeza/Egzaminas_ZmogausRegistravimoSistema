using Egzaminas_ZmogausRegistravimoSistema.Entities;

namespace Egzaminas_ZmogausRegistravimoSistema.Database.InitialData
{
    public static class PersonInfoDataSeed
    {
        public static List<PersonInfo> PersonInfos => new()
        {
            GetPersonInfo1()
        };

        private static PersonInfo GetPersonInfo1()
        {
            return new PersonInfo
            {
                Id = 1,
                FirstName = "User1",
                LastName = "User1",
                PersonalIdNumber = 60001278930,
                PhoneNumber = "123456789",
                Email = "user1@gmail.com",
                PhotoPath = @"..\Database\InitialData\administrator-icon.jpg",
                UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            };
        }
    }
}
