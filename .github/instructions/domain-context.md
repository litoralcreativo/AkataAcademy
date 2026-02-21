# Domain Context

AkataAcademy is an educational platform focused on course management, student enrollment, and certification. The system is organized into several bounded contexts, each encapsulating its own business rules and invariants:

- **Catalog**: Manages courses, modules, and publication status. Only published courses are eligible for certification.
- **Enrollment**: Tracks student enrollment and progress in courses. Enforces that a student can only be enrolled once per course and manages completion status.
- **Certification**: Handles certificate issuance, validation, and revocation. Enforces business rules such as one certificate per student per course, certificates require course completion, and certificates have expiration dates.
- **Student Management**: Manages student registration, personal data, and status. Students must be registered and active before enrolling in courses.

## Key Business Rules

- A course must be published before certificates can be issued for it.
- A student can only receive one certificate per course.
- Certificates have an expiration date and cannot be issued with an expiration date before the issue date.
- Enrollment progress is tracked and only completed enrollments are eligible for certification.
- Student email must be unique and valid.
- Student must be Active to enroll in courses.
- Cannot delete a student with active enrollments.

For a detailed overview of aggregates, value objects, and domain events, see the Domain Layer documentation.
