
using Microsoft.EntityFrameworkCore;
using WEB_API_DAY01.Entity;

namespace WEB_API_DAY01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StudentContext>(opt => 
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("con"));

            });
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", crosPolicy =>
                {
                    crosPolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();

                });

            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseStaticFiles(); // added to see the html page static file

            app.UseCors("MyPolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
