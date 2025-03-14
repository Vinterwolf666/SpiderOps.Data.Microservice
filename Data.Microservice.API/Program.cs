
using Customer.Identity.Microservice.API.Services;
using Data.Microservice.App;
using Data.Microservice.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Data.Microservice.API
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

            builder.Services.AddScoped<IDataRepository, DataRepository>();
            builder.Services.AddScoped<IDataServices, DataServices>();
            builder.Services.AddScoped<RabbitMQProducer>();

            builder.Services.AddCors(options =>
            {

                options.AddPolicy("nuevaPolitica", app =>
                {

                    app.AllowAnyOrigin();
                    app.AllowAnyHeader();
                    app.AllowAnyMethod();
                });



            });

            var configuracion = builder.Configuration;

            builder.Services.AddDbContext<DataDbContext>(opt => opt.UseSqlServer(configuracion.GetConnectionString("Value"), b => b.MigrationsAssembly("SpiderOps.API")));
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           

            app.UseCors("nuevaPolitica");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}