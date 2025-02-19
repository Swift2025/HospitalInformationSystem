using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalInformationSystem.Application.Common.Interfaces;
using HospitalInformationSystem.Application.Common.Models;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IMapper mapper;
        private readonly HISContext _context;

        public PatientRepository(IMapper mapper, HISContext context)
        {
            this.mapper = mapper;
            _context = context;
        }

        public async Task<Guid> AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedList<PatientDto>> GetPatientsAsync(string name, int fileNo, string phoneNumber, int page, int pageSize)
        {
            var query = _context.Patients.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.Contains(name));

            if (fileNo > 0)
                query = query.Where(p => p.FileNo == fileNo);

            if (!string.IsNullOrEmpty(phoneNumber))
                query = query.Where(p => p.PhoneNumber.Contains(phoneNumber));
            return await PaginatedList<PatientDto>.CreateAsync(query.ProjectTo<PatientDto>(mapper.ConfigurationProvider), page, pageSize);
        }
    }
}