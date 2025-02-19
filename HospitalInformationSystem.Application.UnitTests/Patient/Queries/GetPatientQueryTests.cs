using HospitalInformationSystem.Application.Queries;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using AutoMapper;
using HospitalInformationSystem.Application.Queries.Patient;
using NUnit.Framework;

namespace HospitalInformationSystem.Application.UnitTests.Queries
{
    public class GetPatientsQueryTests
    {
        [Fact]
        public async Task Handle_ShouldReturnPaginatedPatients_WhenValidCriteriaIsProvided()
        {
            // Arrange
            var mockDbContext = new Mock<ApplicationDbContext>();
            var mockDbSet = new Mock<DbSet<Patient>>();

            var patients = new List<Patient>
            {
                new Patient { Name = "John Doe", FileNo = 12345, PhoneNumber = "123-456-7890" },
                new Patient { Name = "Jane Doe", FileNo = 67890, PhoneNumber = "987-654-3210" }
            }.AsQueryable();

            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
            mockDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

            mockDbContext.Setup(db => db.Patients).Returns(mockDbSet.Object);

            var query = new GetPatientsQuery
            {
                Name = "John",
                PageNumber = 1,
                PageSize = 10
            };

            var handler = new GetPatientsQueryHandler(mockDbContext.Object, Mock.Of<IMapper>());

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result.Items.Count, 1);
            Assert.AreEqual("John Doe", result.Items.First().Name);
        }
    }
}