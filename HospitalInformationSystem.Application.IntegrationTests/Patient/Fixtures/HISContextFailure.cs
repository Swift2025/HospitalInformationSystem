using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Reflection;

namespace HospitalInformationSystem.Application.IntegrationTests.Fixtures
{
    public class HISContextFixture : IDisposable
    {
        public HISContext Context { get; }
        public IMediator Mediator { get; }

        public HISContextFixture()
        {
            var services = new ServiceCollection();
            services.AddDbContext<HISContext>(options =>
                options.UseSqlServer());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePatientCommand).Assembly));

            var serviceProvider = services.BuildServiceProvider();
            Context = serviceProvider.GetRequiredService<HISContext>();
            Mediator = serviceProvider.GetRequiredService<IMediator>();

            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
