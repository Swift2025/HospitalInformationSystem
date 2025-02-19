using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using HospitalInformationSystem.Infrastructure.Repositories;
using ILogger = Serilog.ILogger;
using HospitalInformationSystem.Domain;
using AutoMapper;
using HospitalInformationSystem.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using FluentValidation.AspNetCore;
using FluentValidation;
using HospitalInformationSystem.Application.Queries.Patient;
using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Application.Common.Interfaces;

namespace HospitalInformationSystem.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<HISContext>((options) => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });
            builder.Services.AddScoped<IHISContext, HISContext>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSerilog(logger => logger.WriteTo.Console());
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(CreatePatientCommand).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(HISContext).Assembly);
            });
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
            builder.Services.AddScoped(provider => 
            {
                var dbContext = provider.GetRequiredService<HISContext>();
                var mapper = provider.GetRequiredService<IMapper>();
                return new PatientRepository(mapper, dbContext);
            });
            builder.Services.AddCors(corsOptions => 
            {
                corsOptions.AddPolicy("Development",corsPolicy => 
                {
                    corsPolicy.AllowAnyHeader();
                    corsPolicy.AllowAnyMethod();
                    corsPolicy.AllowAnyOrigin();
                });
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseSerilogRequestLogging();
            app.UseCors();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
