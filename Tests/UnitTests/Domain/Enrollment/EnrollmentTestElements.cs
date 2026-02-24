using AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects;

namespace AkataAcademy.UnitTests.Domain.Enrollment
{
	/// <summary>
	/// Test data elements for Enrollment BC tests
	/// </summary>
	public static class EnrollmentTestElements
	{
		public static readonly StudentId EnrollmentStudentId = StudentId.From(Guid.NewGuid());
		public static readonly CourseId EnrollmentCourseId = CourseId.From(Guid.NewGuid());
	}
}
