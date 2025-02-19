using HospitalInformationSystem.Application.Common.Models;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Application.Common.Interfaces
{
    public interface IPatientRepository
    {
        Task<Guid> AddAsync(Patient patient);
        Task DeleteAsync(Guid id);
        Task<PaginatedList<PatientDto>> GetPatientsAsync(string name, int fileNo, string phoneNumber, int page, int pageSize);
    }
}