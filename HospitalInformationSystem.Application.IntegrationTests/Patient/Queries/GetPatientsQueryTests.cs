using HospitalInformationSystem.Application.Queries;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Xunit;
using HospitalInformationSystem.Application.Queries.Patient;
using NUnit.Framework;
using HospitalInformationSystem.Infrastructure.Persistance;
using HospitalInformationSystem.Application.IntegrationTests.Fixtures;

namespace HospitalInformationSystem.Application.IntegrationTests.Queries
{
    public class GetPatientsQueryTests : IClassFixture<HISContextFixture>
    {
        private readonly HISContext _context;
        private readonly IMediator _mediator;

        public GetPatientsQueryTests(HISContextFixture fixture)
        {
            _context = fixture.Context;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task Handle_ShouldReturnPaginatedPatients_WhenValidCriteriaIsProvided()
        {
            // Arrange
            var patients = new List<Patient>
            {
                new Patient { Name = "John Doe", FileNo = 12345, PhoneNumber = "123-456-7890" },
                new Patient { Name = "Jane Doe", FileNo = 67890, PhoneNumber = "987-654-3210" }
            };

            _context.Patients.AddRange(patients);
            await _context.SaveChangesAsync();

            var query = new GetPatientsQuery
            {
                Name = "John",
                PageNumber = 1,
                PageSize = 10
            };

            // Act
            var result = await _mediator.Send(query);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Items.Count, 1);
            Assert.AreEqual("John Doe", result.Items.First().Name);
        }
    }
}