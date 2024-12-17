using Egzaminas_ZmogausRegistravimoSistema.Database.InitialData;
using Egzaminas_ZmogausRegistravimoSistema.Entities;
using Microsoft.EntityFrameworkCore;

namespace Egzaminas_ZmogausRegistravimoSistema.Database
{
    public class PersonRegistrationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PersonInfo> PersonInfos { get; set; }
        public DbSet<Residence> Residences { get; set; }
        public bool SkipSeeding { get; set; } = false;

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
            if (!SkipSeeding)
            {
                modelBuilder.Entity<User>()
                    .HasData(UsersInitialDataSeed.Users);
                modelBuilder.Entity<PersonInfo>()
                    .HasData(PersonInfoDataSeed.PersonInfos);
                modelBuilder.Entity<Residence>()
                    .HasData(ResidenceDataSeed.Residences);
            }

            modelBuilder.Entity<User>()
            .HasOne(u => u.PersonInfo)
            .WithOne(p => p.User)
            .HasForeignKey<PersonInfo>(p => p.UserId)
            .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<PersonInfo>()
                .HasOne(u => u.Residence)
                .WithOne(p => p.PersonInfo)
                .HasForeignKey<Residence>(p => p.PersonInfoId)
                .IsRequired();
        }
    }
}