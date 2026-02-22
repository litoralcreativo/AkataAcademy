using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;

namespace AkataAcademy.UnitTests.Domain.Certification
{
	public static class CertificationTestElements
	{
		public static readonly StudentId ValidStudentId = StudentId.From(Guid.NewGuid());
		public static readonly CourseId ValidCourseId = CourseId.From(Guid.NewGuid());
		public static readonly IssueDate ValidIssueDate = IssueDate.From(new DateTime(2026, 2, 21));
		public static readonly ExpirationDate ValidExpirationDate = ExpirationDate.From(new DateTime(2027, 2, 21));
	}
}
