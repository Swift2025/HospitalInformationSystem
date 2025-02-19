using HospitalInformationSystem.Application.Common.Interfaces;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Application.Commands.Patient
{
    public class CreatePatientCommand : IRequest<Guid>
    {
        public CreatePatientDto Patient { get; set; }
    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Guid>
    {
        private readonly IHISContext _context;

        public CreatePatientCommandHandler(IHISContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            // AutoMapper can be used here to map the CreatePatientDto to Patient entity
            var patient = new Domain.Entities.Patient
            {
                Id = Guid.NewGuid(),
                RecordCreationDate = DateTime.UtcNow,
                Name = request.Patient.Name,
                FileNo = request.Patient.FileNo,
                CitizenId = request.Patient.CitizenId,
                Birthdate = request.Patient.Birthdate,
                Gender = request.Patient.Gender,
                Nationality = request.Patient.Nationality,
                PhoneNumber = request.Patient.PhoneNumber,
                Email = request.Patient.Email,
                Country = request.Patient.Country,
                City = request.Patient.City,
                Street = request.Patient.Street,
                Address1 = request.Patient.Address1,
                Address2 = request.Patient.Address2,
                ContactPerson = request.Patient.ContactPerson,
                ContactRelation = request.Patient.ContactRelation,
                ContactPhone = request.Patient.ContactPhone,
                FirstVisitDate = request.Patient.FirstVisitDate
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync(cancellationToken);

            return patient.Id;
        }
    }
}
