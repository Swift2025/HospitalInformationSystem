using HospitalInformationSystem.Application.Commands;
using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Application.IntegrationTests.Fixtures;
using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Infrastructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Xunit;

namespace HospitalInformationSystem.Application.IntegrationTests.Commands
{
    public class CreatePatientCommandTests : IClassFixture<HISContextFixture>
    {
        private readonly HISContext _context;
        private readonly IMediator _mediator;

        public CreatePatientCommandTests(HISContextFixture fixture)
        {
            _context = fixture.Context;
            _mediator = fixture.Mediator;
        }

        [Fact]
        public async Task Handle_ShouldCreatePatient_WhenValidDataIsProvided()
        {
            // Arrange
            var patientDto = new CreatePatientDto
            {
                Name = "John Doe",
                FileNo = 12345,
                CitizenId = "123456789",
                Birthdate = new DateTime(1990, 1, 1),
                Gender = 0,
                Nationality = "American",
                PhoneNumber = "123-456-7890",
                Email = "john.doe@example.com",
                Country = "USA",
                City = "New York",
                Street = "5th Avenue",
                Address1 = "Apt 101",
                Address2 = "Near Central Park",
                ContactPerson = "Jane Doe",
                ContactRelation = "Spouse",
                ContactPhone = "987-654-3210",
                FirstVisitDate = new DateTime(2023, 10, 1)
            };

            var command = new CreatePatientCommand { Patient = patientDto };

            // Act
            var patientId = await _mediator.Send(command);

            // Assert
            var patient = await _context.Patients.FindAsync(patientId);
            Assert.NotNull(patient);
            Assert.AreEqual(patientDto.Name, patient.Name);
            Assert.AreEqual(patientDto.FileNo, patient.FileNo);
            Assert.AreEqual(patientDto.CitizenId, patient.CitizenId);
        }
    }
}