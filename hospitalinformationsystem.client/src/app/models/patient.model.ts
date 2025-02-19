export interface Patient {
  id: string; // Unique identifier for the patient (GUID)
  name: string; // Full name of the patient
  fileNo: number; // File number of the patient
  citizenId: string; // National ID or citizenship number
  birthdate: Date; // Date of birth
  gender: number; // 0 for Male, 1 for Female
  nationality: string; // Nationality of the patient
  phoneNumber: string; // Contact phone number
  email: string; // Email address
  country: string; // Country of residence
  city: string; // City of residence
  street: string; // Street address
  address1: string; // Address line 1
  address2: string; // Address line 2
  contactPerson: string; // Name of the emergency contact person
  contactRelation: string; // Relationship to the patient
  contactPhone: string; // Phone number of the emergency contact
  firstVisitDate: Date; // Date of the first visit
  recordCreationDate: Date; // Date when the record was created
}
export interface PatientDto {
  id: string;
  fullName: string;
  fileNumber: number;
  nationalId: string;
  dateOfBirth: string;
  gender: number;
  nationality: string;
  phone: string;
  emailAddress: string;
  country: string;
  city: string;
  street: string;
  addressLine1: string;
  addressLine2: string;
  emergencyContactName: string;
  emergencyContactRelationship: string;
  emergencyContactPhone: string;
  firstVisit: string;
  createdAt: string;
}

// Function to map DTO to Patient model
export function mapToPatient(dto: PatientDto): Patient {
  return {
    id: dto.id,
    name: dto.fullName,
    fileNo: dto.fileNumber,
    citizenId: dto.nationalId,
    birthdate: new Date(dto.dateOfBirth),
    gender: dto.gender,
    nationality: dto.nationality,
    phoneNumber: dto.phone,
    email: dto.emailAddress,
    country: dto.country,
    city: dto.city,
    street: dto.street,
    address1: dto.addressLine1,
    address2: dto.addressLine2,
    contactPerson: dto.emergencyContactName,
    contactRelation: dto.emergencyContactRelationship,
    contactPhone: dto.emergencyContactPhone,
    firstVisitDate: new Date(dto.firstVisit),
    recordCreationDate: new Date(dto.createdAt)
  };
}
