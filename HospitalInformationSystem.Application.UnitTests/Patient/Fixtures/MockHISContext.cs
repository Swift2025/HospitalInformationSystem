using Microsoft.EntityFrameworkCore;
using Moq;
using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Infrastructure.Persistance;

public static class MockHISContext
{
    public static Mock<HISContext> GetMockContext()
    {
        // Create in-memory database options
        var options = new DbContextOptionsBuilder<HISContext>()
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=HIS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            .Options;

        // Create a mock DbContext
        var mockContext = new Mock<HISContext>(options) { CallBase = true };

        // Mock DbSet for Patients
        var patients = new List<Patient>
        {
            new Patient { Id = Guid.NewGuid(), Name = "John Doe", FileNo = 12345, PhoneNumber = "123-456-7890" },
            new Patient { Id = Guid.NewGuid(), Name = "Jane Doe", FileNo = 67890, PhoneNumber = "987-654-3210" }
        }.AsQueryable();

        var mockPatientSet = new Mock<DbSet<Patient>>();
        mockPatientSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patients.Provider);
        mockPatientSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patients.Expression);
        mockPatientSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patients.ElementType);
        mockPatientSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patients.GetEnumerator());

        mockContext.Setup(c => c.Patients).Returns(mockPatientSet.Object);

        return mockContext;
    }
}
