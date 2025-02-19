using HospitalInformationSystem.Domain.Entities;
using HospitalInformationSystem.Domain.Enums;
using NUnit.Framework;
using Xunit;

namespace HospitalInformationSystem.Domain.UnitTests.Entities
{
    public class PatientTests
    {
        [Fact]
        public void CreatePatient_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "John Doe";
            var fileNo = 12345;
            var citizenId = "123456789";
            var birthdate = new DateTime(1990, 1, 1);
            var gender = HumanGender.Male; // Male
            var nationality = "American";
            var phoneNumber = "123-456-7890";
            var email = "john.doe@example.com";
            var country = "USA";
            var city = "New York";
            var street = "5th Avenue";
            var address1 = "Apt 101";
            var address2 = "Near Central Park";
            var contactPerson = "Jane Doe";
            var contactRelation = "Spouse";
            var contactPhone = "987-654-3210";
            var firstVisitDate = new DateTime(2023, 10, 1);
            var recordCreationDate = DateTime.UtcNow;

            // Act
            var patient = new Patient
            {
                Id = id,
                Name = name,
                FileNo = fileNo,
                CitizenId = citizenId,
                Birthdate = birthdate,
                Gender = gender,
                Nationality = nationality,
                PhoneNumber = phoneNumber,
                Email = email,
                Country = country,
                City = city,
                Street = street,
                Address1 = address1,
                Address2 = address2,
                ContactPerson = contactPerson,
                ContactRelation = contactRelation,
                ContactPhone = contactPhone,
                FirstVisitDate = firstVisitDate,
                RecordCreationDate = recordCreationDate
            };

            // Assert
            Assert.AreEqual(id, patient.Id);
            Assert.AreEqual(name, patient.Name);
            Assert.AreEqual(fileNo, patient.FileNo);
            Assert.AreEqual(citizenId, patient.CitizenId);
            Assert.AreEqual(birthdate, patient.Birthdate);
            Assert.AreEqual(gender, patient.Gender);
            Assert.AreEqual(nationality, patient.Nationality);
            Assert.AreEqual(phoneNumber, patient.PhoneNumber);
            Assert.AreEqual(email, patient.Email);
            Assert.AreEqual(country, patient.Country);
            Assert.AreEqual(city, patient.City);
            Assert.AreEqual(street, patient.Street);
            Assert.AreEqual(address1, patient.Address1);
            Assert.AreEqual(address2, patient.Address2);
            Assert.AreEqual(contactPerson, patient.ContactPerson);
            Assert.AreEqual(contactRelation, patient.ContactRelation);
            Assert.AreEqual(contactPhone, patient.ContactPhone);
            Assert.AreEqual(firstVisitDate, patient.FirstVisitDate);
            Assert.AreEqual(recordCreationDate, patient.RecordCreationDate);
        }
    }
}