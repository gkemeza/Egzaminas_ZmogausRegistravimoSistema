
using Egzaminas_ZmogausRegistravimoSistema.Database;
using Egzaminas_ZmogausRegistravimoSistema.Mappers;
using Egzaminas_ZmogausRegistravimoSistema.Mappers.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Repositories;
using Egzaminas_ZmogausRegistravimoSistema.Repositories.Interfaces;
using Egzaminas_ZmogausRegistravimoSistema.Services;
using Egzaminas_ZmogausRegistravimoSistema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Egzaminas_ZmogausRegistravimoSistema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserMapper, UserMapper>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PersonRegistrationContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("Database")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder
                //.WithOrigins("https://example1.com")
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
