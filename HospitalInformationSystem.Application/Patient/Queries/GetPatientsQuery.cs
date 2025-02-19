using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalInformationSystem.Application.Common.Interfaces;
using HospitalInformationSystem.Application.Common.Models;
using HospitalInformationSystem.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Application.Queries.Patient
{
    public class GetPatientsQuery : IRequest<PaginatedList<PatientDto>>
    {
        public string Name { get; set; } = string.Empty;
        public int? FileNo { get; set; } = 0;
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public int PageNumber { get; set; } = 1;
        [Required]
        public int PageSize { get; set; } = 10;
    }

    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, PaginatedList<PatientDto>>
    {
        private readonly IPatientRepository _repository;

        public GetPatientsQueryHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<PatientDto>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetPatientsAsync(request.Name, request.FileNo!.Value, request.PhoneNumber, request.PageNumber, request.PageSize);
        }
    }
}
