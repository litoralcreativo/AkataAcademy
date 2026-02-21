# Student Management Bounded Context

**Purpose:**
Manages student information and lifecycle, ensuring uniqueness, validity, and up-to-date data before any enrollment process.

## Aggregate Root

- **Student**
  - Unique student identity.
  - Encapsulates business logic for managing personal data and student status.

## Entities

- **Student**
  - `Id` (Guid)
  - `FullName` (Value Object)
  - `Email` (Value Object, with format validation)
  - `DateOfBirth` (Value Object)
  - `Status` (Enum: Active, Inactive, Suspended, Deleted)
  - Methods: `UpdatePersonalInfo()`, `Activate()`, `Suspend()`, `Delete()`

## Value Objects

- **FullName**: FirstName, LastName
- **Email**: Validated format
- **DateOfBirth**: Date only, cannot be in the future

## Domain Events

- `StudentRegistered`
- `StudentUpdated`
- `StudentActivated`
- `StudentSuspended`
- `StudentDeleted`

## Business Rules

- Email must be unique and valid.
- Student must be in "Active" status to enroll in courses.
- Cannot delete a student with active enrollments.
