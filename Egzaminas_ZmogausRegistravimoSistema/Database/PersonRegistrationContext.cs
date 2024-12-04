using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Microsoft.EntityFrameworkCore;

namespace Egzaminas_ZmogausRegistravimoSistema.Database
{
    public class PersonRegistrationContext : DbContext
    {
        public DbSet<User> UserInfos { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<Residence> Residences { get; set; }

        public PersonRegistrationContext(DbContextOptions<PersonRegistrationContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB; Database=PersonRegistrationSystem; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO?
        }
    }
}
