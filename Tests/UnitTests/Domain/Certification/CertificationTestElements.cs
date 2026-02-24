using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;

namespace AkataAcademy.UnitTests.Domain.Certification
{
	public static class CertificationTestElements
	{
		// Certificate test data
		public static readonly StudentId ValidStudentId = StudentId.From(Guid.NewGuid());
		public static readonly CourseId ValidCourseId = CourseId.From(Guid.NewGuid());
		public static readonly IssueDate ValidIssueDate = IssueDate.From(new DateTime(2026, 2, 21));
		public static readonly ExpirationDate ValidExpirationDate = ExpirationDate.From(new DateTime(2027, 2, 21));

		// EligibilityRecord test data
		public static readonly StudentId EligibilityStudentId = StudentId.From(Guid.NewGuid());
		public static readonly CourseId EligibilityCourseId = CourseId.From(Guid.NewGuid());
		public static readonly EligibilityStatus EligibilityStatusPending = EligibilityStatus.Pending();
		public static readonly EligibilityStatus EligibilityStatusEligible = EligibilityStatus.Eligible();
		public static readonly EligibilityStatus EligibilityStatusIneligible = EligibilityStatus.Ineligible();
		public static readonly EligibilityStatus EligibilityStatusRevoked = EligibilityStatus.Revoked();

		public static readonly StudentId[] ValidStudentIds = new[]
		{
			StudentId.From(Guid.NewGuid()),
			StudentId.From(Guid.NewGuid()),
			StudentId.From(Guid.NewGuid()),
			StudentId.From(Guid.NewGuid()),
			StudentId.From(Guid.NewGuid())
		};

		public static readonly CourseId[] ValidCourseIds = new[]
		{
			CourseId.From(Guid.NewGuid()),
			CourseId.From(Guid.NewGuid()),
			CourseId.From(Guid.NewGuid()),
			CourseId.From(Guid.NewGuid()),
			CourseId.From(Guid.NewGuid())
		};

		public static readonly IssueDate[] ValidIssueDates = new[]
		{
			IssueDate.From(new DateTime(2026, 2, 21)),
			IssueDate.From(new DateTime(2026, 3, 10)),
			IssueDate.From(new DateTime(2026, 4, 5)),
			IssueDate.From(new DateTime(2026, 5, 15)),
			IssueDate.From(new DateTime(2026, 6, 1))
		};

		public static readonly ExpirationDate[] ValidExpirationDates = new[]
		{
			ExpirationDate.From(new DateTime(2027, 2, 21)),
			ExpirationDate.From(new DateTime(2027, 3, 10)),
			ExpirationDate.From(new DateTime(2027, 4, 5)),
			ExpirationDate.From(new DateTime(2027, 5, 15)),
			ExpirationDate.From(new DateTime(2027, 6, 1))
		};

		public static IEnumerable<object[]> ValidStudentIdsData => ValidStudentIds.Select(id => new object[] { id });
		public static IEnumerable<object[]> ValidCourseIdsData => ValidCourseIds.Select(id => new object[] { id });
		public static IEnumerable<object[]> ValidIssueDatesData => ValidIssueDates.Select(date => new object[] { date });
		public static IEnumerable<object[]> ValidExpirationDatesData => ValidExpirationDates.Select(date => new object[] { date });

		public static IEnumerable<object[]> ValidCertificationData =>
			ValidStudentIds.Zip(ValidCourseIds, (sid, cid) => new { sid, cid })
			.Zip(ValidIssueDates, (pair, issue) => new { pair, issue })
			.Zip(ValidExpirationDates, (trio, exp) => new object[] { trio.pair.sid, trio.pair.cid, trio.issue, exp });
	}
}
