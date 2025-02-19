using MediatR;
using HospitalInformationSystem.Application.DTOs;
using HospitalInformationSystem.Domain.Enums;

namespace HospitalInformationSystem.Application.Commands
{
    public class UpdatePatientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public UpdatePatientDto Patient { get; set; }
    }

    public class UpdatePatientDto
    {
        public string Name { get; set; }
        public int FileNo { get; set; }
        public string CitizenId { get; set; }
        public DateTime Birthdate { get; set; }
        public HumanGender Gender { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ContactPerson { get; set; }
        public string ContactRelation { get; set; }
        public string ContactPhone { get; set; }
        public DateTime FirstVisitDate { get; set; }
    }
}