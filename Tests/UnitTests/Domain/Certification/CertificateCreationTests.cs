using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.Events;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Domain.Common;
using static AkataAcademy.UnitTests.Domain.Certification.CertificationTestElements;

namespace AkataAcademy.UnitTests.Domain.Certification
{
	/// <summary>
	/// Test suite for Certification Value Objects
	/// Tests validation and factory methods for StudentId, CourseId, IssueDate, ExpirationDate
	/// </summary>
	public class CertificationValueObjectTests
	{
		[Fact]
		public void StudentId_FromEmptyGuid_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => StudentId.From(Guid.Empty));
		}

		[Fact]
		public void StudentId_FromValidGuid_ShouldSucceed()
		{
			var id = Guid.NewGuid();
			var studentId = StudentId.From(id);
			Assert.Equal(id, studentId.Value);
		}

		[Fact]
		public void CourseId_FromEmptyGuid_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => CourseId.From(Guid.Empty));
		}

		[Fact]
		public void CourseId_FromValidGuid_ShouldSucceed()
		{
			var id = Guid.NewGuid();
			var courseId = CourseId.From(id);
			Assert.Equal(id, courseId.Value);
		}

		[Fact]
		public void IssueDate_FromMinValue_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => IssueDate.From(DateTime.MinValue));
		}

		[Fact]
		public void IssueDate_FromValidDateTime_ShouldSucceed()
		{
			var date = new DateTime(2026, 2, 21);
			var issueDate = IssueDate.From(date);
			Assert.Equal(date, issueDate.Value);
		}

		[Fact]
		public void ExpirationDate_FromMinValue_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => ExpirationDate.From(DateTime.MinValue));
		}

		[Fact]
		public void ExpirationDate_FromValidDateTime_ShouldSucceed()
		{
			var date = new DateTime(2027, 2, 21);
			var expirationDate = ExpirationDate.From(date);
			Assert.Equal(date, expirationDate.Value);
		}
	}

	/// <summary>
	/// Test suite for Certificate Aggregate Root creation
	/// Tests factory method (Issue), invariants, and validation
	/// </summary>
	public class CertificateCreationTests
	{
		[Fact]
		public void Create_WithNullStudentId_ShouldFail()
		{
			var result = Certificate.Issue(null!, ValidCourseId, ValidIssueDate, ValidExpirationDate);
			Assert.True(result.IsFailure);
		}

		[Fact]
		public void Create_WithNullCourseId_ShouldFail()
		{
			var result = Certificate.Issue(ValidStudentId, null!, ValidIssueDate, ValidExpirationDate);
			Assert.True(result.IsFailure);
		}

		[Fact]
		public void Create_WithNullIssueDate_ShouldFail()
		{
			var result = Certificate.Issue(ValidStudentId, ValidCourseId, null!, ValidExpirationDate);
			Assert.True(result.IsFailure);
		}

		[Fact]
		public void Create_WithNullExpirationDate_ShouldFail()
		{
			var result = Certificate.Issue(ValidStudentId, ValidCourseId, ValidIssueDate, null!);
			Assert.True(result.IsFailure);
		}

		[Fact]
		public void Create_WithExpirationBeforeIssue_ShouldFail()
		{
			var expirationDate = ExpirationDate.From(ValidIssueDate.Value.AddDays(-1));
			var result = Certificate.Issue(ValidStudentId, ValidCourseId, ValidIssueDate, expirationDate);
			Assert.True(result.IsFailure);
		}

		[Fact]
		public void Create_WithExpirationEqualToIssue_ShouldFail()
		{
			var expirationDate = ExpirationDate.From(ValidIssueDate.Value);
			var result = Certificate.Issue(ValidStudentId, ValidCourseId, ValidIssueDate, expirationDate);
			Assert.True(result.IsFailure);
		}

		[Theory]
		[MemberData(nameof(ValidCertificationData), MemberType = typeof(CertificationTestElements))]
		public void Create_WithValidData_ShouldSucceed(StudentId studentId, CourseId courseId, IssueDate issueDate, ExpirationDate expirationDate)
		{
			var result = Certificate.Issue(studentId, courseId, issueDate, expirationDate);
			Assert.True(result.IsSuccess);
			Assert.NotNull(result.Value);
			Assert.Equal(studentId, result.Value.StudentId);
			Assert.Equal(courseId, result.Value.CourseId);
			Assert.Equal(issueDate, result.Value.IssuedOn);
			Assert.Equal(expirationDate, result.Value.ExpiresOn);
		}

		[Theory]
		[MemberData(nameof(ValidCertificationData), MemberType = typeof(CertificationTestElements))]
		public void Create_WithValidData_ShouldHaveUniqueId(StudentId studentId, CourseId courseId, IssueDate issueDate, ExpirationDate expirationDate)
		{
			var result1 = Certificate.Issue(studentId, courseId, issueDate, expirationDate);
			var result2 = Certificate.Issue(studentId, courseId, issueDate, expirationDate);

			Assert.True(result1.IsSuccess);
			Assert.True(result2.IsSuccess);
			Assert.NotEqual(result1.Value.Id, result2.Value.Id);
		}
	}

	/// <summary>
	/// Test suite for Certificate domain events
	/// Tests CertificateIssued event emission and correctness
	/// </summary>
	public class CertificateEventTests
	{
		[Theory]
		[MemberData(nameof(ValidCertificationData), MemberType = typeof(CertificationTestElements))]
		public void Create_ShouldEmit_CertificateIssuedDomainEvent(StudentId studentId, CourseId courseId, IssueDate issueDate, ExpirationDate expirationDate)
		{
			var result = Certificate.Issue(studentId, courseId, issueDate, expirationDate);
			Assert.True(result.IsSuccess);
			var certificate = result.Value;
			Assert.Contains(certificate.DomainEvents, e => e is CertificateIssued);
		}

		[Theory]
		[MemberData(nameof(ValidCertificationData), MemberType = typeof(CertificationTestElements))]
		public void Create_ShouldEmit_CertificateIssuedDomainEvent_WithCorrectData(StudentId studentId, CourseId courseId, IssueDate issueDate, ExpirationDate expirationDate)
		{
			var result = Certificate.Issue(studentId, courseId, issueDate, expirationDate);
			Assert.True(result.IsSuccess);
			var certificate = result.Value;
			var domainEvent = certificate.DomainEvents.OfType<CertificateIssued>().FirstOrDefault();
			Assert.NotNull(domainEvent);
			Assert.Equal(certificate.Id, domainEvent.CertificateId);
			Assert.Equal(studentId.Value, domainEvent.StudentId);
			Assert.Equal(courseId.Value, domainEvent.CourseId);
			Assert.Equal(issueDate.Value, domainEvent.IssuedOn);
		}

		[Theory]
		[MemberData(nameof(ValidCertificationData), MemberType = typeof(CertificationTestElements))]
		public void Create_ShouldEmit_CertificateIssuedDomainEvent_WithValidTimestamp(StudentId studentId, CourseId courseId, IssueDate issueDate, ExpirationDate expirationDate)
		{
			var before = DateTime.UtcNow;
			var result = Certificate.Issue(studentId, courseId, issueDate, expirationDate);
			var after = DateTime.UtcNow.AddMilliseconds(1);

			Assert.True(result.IsSuccess);
			var certificate = result.Value;
			var domainEvent = certificate.DomainEvents.OfType<CertificateIssued>().FirstOrDefault();
			Assert.NotNull(domainEvent);
			Assert.True(domainEvent.OccurredOn >= before);
			Assert.True(domainEvent.OccurredOn <= after);
		}
	}
}
