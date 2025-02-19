using CleanArchitecture.Application.Common.Exceptions;
using HospitalInformationSystem.Application.Common.Interfaces;
using HospitalInformationSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HospitalInformationSystem.Application.Commands
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Unit>
    {
        private readonly IHISContext _context;

        public UpdatePatientCommandHandler(IHISContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _context.Patients.FindAsync(request.Id);
            if (patient == null)
            {
                throw new NotFoundException(nameof(Patient), request.Id);
            }
            // Auto Mapper can be used here
            patient.Name = request.Patient.Name;
            patient.FileNo = request.Patient.FileNo;
            patient.CitizenId = request.Patient.CitizenId;
            patient.Birthdate = request.Patient.Birthdate;
            patient.Gender = request.Patient.Gender;
            patient.Nationality = request.Patient.Nationality;
            patient.PhoneNumber = request.Patient.PhoneNumber;
            patient.Email = request.Patient.Email;
            patient.Country = request.Patient.Country;
            patient.City = request.Patient.City;
            patient.Street = request.Patient.Street;
            patient.Address1 = request.Patient.Address1;
            patient.Address2 = request.Patient.Address2;
            patient.ContactPerson = request.Patient.ContactPerson;
            patient.ContactRelation = request.Patient.ContactRelation;
            patient.ContactPhone = request.Patient.ContactPhone;
            patient.FirstVisitDate = request.Patient.FirstVisitDate;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}