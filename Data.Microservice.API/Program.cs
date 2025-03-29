
using Customer.Identity.Microservice.API.Services;
using Customer.Notify.Microservice.APP;
using Customer.Notify.Microservice.Infrastructure;
using Data.Microservice.App;
using Data.Microservice.APP;
using Data.Microservice.Infrastructure;
using Data.Microservice.Service;
using Microsoft.EntityFrameworkCore;

namespace Data.Microservice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();
            var configuracion = builder.Configuration;
           


            builder.Services.AddDbContext<DataDbContext>(opt => opt.UseSqlServer(configuracion.GetConnectionString("Value"), b => b.MigrationsAssembly("SpiderOps.API")));
            builder.Services.AddDbContext<NotifyDBContext>(opt => opt.UseSqlServer(configuracion.GetConnectionString("Value2"), b => b.MigrationsAssembly("SpiderOps.API")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IDataRepository, DataRepository>();
            builder.Services.AddScoped<IDataServices, DataServices>();


            builder.Services.AddScoped<INotifyRepository, NotifyRepository>();
            builder.Services.AddScoped<INotifyServices, NotifyService>();


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

            var app = builder.Build();

         
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