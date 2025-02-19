using HospitalInformationSystem.Application.Commands;
using HospitalInformationSystem.Application.Commands.Patient;
using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Infrastructure.Persistance;
using HospitalInformationSystem.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HospitalInformationSystem.Application.UnitTests.Commands
{
    public class DeletePatientCommandTests
    {
        [Fact]
        public async Task Handle_ShouldDeletePatient_WhenPatientExists()
        {
            // Arrange
            var mockDbContext = new Mock<HISContext>();
            var mockDbSet = new Mock<DbSet<Patient>>();

            var patientId = Guid.NewGuid();
            var patient = new Patient { Id = patientId };

            mockDbSet.Setup(db => db.FindAsync(patientId)).ReturnsAsync(patient);
            mockDbContext.Setup(db => db.Patients).Returns(mockDbSet.Object);

            var command = new DeletePatientCommand { Id = patientId };
            var handler = new DeletePatientCommandHandler(mockDbContext.Object);

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockDbSet.Verify(db => db.Remove(patient), Times.Once);
            mockDbContext.Verify(db => db.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}