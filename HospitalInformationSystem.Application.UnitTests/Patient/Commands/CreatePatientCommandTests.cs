using HospitalInformationSystem.Application.Commands;
using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Infrastructure.Persistance;
using HospitalInformationSystem.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Xunit;

namespace HospitalInformationSystem.Application.UnitTests.Commands
{
    public class CreatePatientCommandTests
    {
        [Fact]
        public async Task Handle_ShouldCreatePatient_WhenValidDataIsProvided()
        {
            // Arrange
            var mockDbContext = new Mock<HISContext>();
            var mockDbSet = new Mock<DbSet<Patient>>();

            mockDbContext.Setup(db => db.Patients).Returns(mockDbSet.Object);

            var command = new CreatePatientCommand
            {
                Patient = new CreatePatientDto
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
                }
            };

            var handler = new CreatePatientCommandHandler(mockDbContext.Object);

            // Act
            var patientId = await handler.Handle(command, CancellationToken.None);

            // Assert
            mockDbSet.Verify(db => db.Add(It.IsAny<Patient>()), Times.Once);
            mockDbContext.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once);
            Assert.AreNotEqual(Guid.Empty, patientId);
        }
    }
}