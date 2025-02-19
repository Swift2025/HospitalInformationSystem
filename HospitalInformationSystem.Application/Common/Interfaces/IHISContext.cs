using HospitalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalInformationSystem.Application.Common.Interfaces;

public interface IHISContext
{
    DbSet<Patient> Patients { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
