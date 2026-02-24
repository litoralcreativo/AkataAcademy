using AkataAcademy.Domain.BoundedContexts.Certification.Entities;
using AkataAcademy.Domain.BoundedContexts.Certification.Events;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Domain.Common;
using static AkataAcademy.UnitTests.Domain.Certification.CertificationTestElements;

namespace AkataAcademy.UnitTests.Domain.Certification
{
	/// <summary>
	/// Test suite for EligibilityStatus Value Object
	/// Tests validation, state transitions, and factory methods
	/// </summary>
	public class EligibilityStatusTests
	{
		[Fact]
		public void EligibilityStatus_FromValidPending_ShouldSucceed()
		{
			var status = EligibilityStatus.From("PENDING");
			Assert.NotNull(status);
			Assert.Equal("PENDING", status.Value);
			Assert.True(status.IsPending);
		}

		[Fact]
		public void EligibilityStatus_FromValidEligible_ShouldSucceed()
		{
			var status = EligibilityStatus.From("ELIGIBLE");
			Assert.NotNull(status);
			Assert.Equal("ELIGIBLE", status.Value);
			Assert.True(status.IsEligible);
		}

		[Fact]
		public void EligibilityStatus_FromValidIneligible_ShouldSucceed()
		{
			var status = EligibilityStatus.From("INELIGIBLE");
			Assert.NotNull(status);
			Assert.Equal("INELIGIBLE", status.Value);
			Assert.True(status.IsIneligible);
		}

		[Fact]
		public void EligibilityStatus_FromValidRevoked_ShouldSucceed()
		{
			var status = EligibilityStatus.From("REVOKED");
			Assert.NotNull(status);
			Assert.Equal("REVOKED", status.Value);
			Assert.True(status.IsRevoked);
		}

		[Fact]
		public void EligibilityStatus_FromInvalidValue_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => EligibilityStatus.From("INVALID_STATUS"));
		}

		[Fact]
		public void EligibilityStatus_FromEmptyString_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => EligibilityStatus.From(""));
		}

		[Fact]
		public void EligibilityStatus_FromNull_ShouldThrowDomainException()
		{
			Assert.Throws<DomainException>(() => EligibilityStatus.From(null!));
		}

		[Fact]
		public void EligibilityStatus_FactoryMethod_Pending_ShouldBeValid()
		{
			var status = EligibilityStatus.Pending();
			Assert.True(status.IsPending);
			Assert.True(status.CanTransitionToEligible);
			Assert.True(status.CanTransitionToIneligible);
			Assert.False(status.CanTransitionToRevoked);
		}

		[Fact]
		public void EligibilityStatus_FactoryMethod_Eligible_ShouldBeValid()
		{
			var status = EligibilityStatus.Eligible();
			Assert.True(status.IsEligible);
			Assert.False(status.CanTransitionToEligible);
			Assert.False(status.CanTransitionToIneligible);
			Assert.True(status.CanTransitionToRevoked);
		}

		[Fact]
		public void EligibilityStatus_FactoryMethod_Ineligible_ShouldBeValid()
		{
			var status = EligibilityStatus.Ineligible();
			Assert.True(status.IsIneligible);
			Assert.False(status.CanTransitionToEligible);
			Assert.False(status.CanTransitionToIneligible);
			Assert.True(status.CanTransitionToRevoked);
		}

		[Fact]
		public void EligibilityStatus_FactoryMethod_Revoked_ShouldBeValid()
		{
			var status = EligibilityStatus.Revoked();
			Assert.True(status.IsRevoked);
			Assert.False(status.CanTransitionToEligible);
			Assert.False(status.CanTransitionToIneligible);
			Assert.False(status.CanTransitionToRevoked);
		}
	}

	/// <summary>
	/// Test suite for EligibilityRecord Aggregate Root creation
	/// Tests factory methods, invariants, and initial state
	/// </summary>
	public class EligibilityRecordCreationTests
	{
		[Fact]
		public void CreateFromEnrollment_WithValidData_ShouldSucceed()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);

			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			Assert.NotNull(eligibility);
			Assert.NotEqual(Guid.Empty, eligibility.Id);
			Assert.Equal(EligibilityStudentId, eligibility.StudentId);
			Assert.Equal(EligibilityCourseId, eligibility.CourseId);
			Assert.True(eligibility.Status.IsPending);
			Assert.NotEqual(default(DateTime), eligibility.EnrolledOn);
			Assert.Null(eligibility.CompletedOn);
			Assert.Null(eligibility.RevokedOn);
		}

		[Fact]
		public void CreateFromEnrollment_WithNullStudentId_ShouldFail()
		{
			var result = EligibilityRecord.CreateFromEnrollment(null!, EligibilityCourseId);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void CreateFromEnrollment_WithNullCourseId_ShouldFail()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, null!);

			Assert.True(result.IsFailure);
		}

		[Fact]
		public void CreateFromEnrollment_ShouldHaveUniqueIds()
		{
			var result1 = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			var result2 = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);

			Assert.True(result1.IsSuccess);
			Assert.True(result2.IsSuccess);
			Assert.NotEqual(result1.Value.Id, result2.Value.Id);
		}
	}

	/// <summary>
	/// Test suite for EligibilityRecord state transitions
	/// Tests MarkAsEligible, MarkAsIneligible, Revoke and event emission
	/// </summary>
	public class EligibilityRecordStateTransitionTests
	{
		[Fact]
		public void MarkAsEligible_FromPending_ShouldSucceed()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			Assert.True(eligibility.Status.IsPending);

			eligibility.MarkAsEligible();

			Assert.True(eligibility.Status.IsEligible);
			Assert.NotNull(eligibility.CompletedOn);
			Assert.Null(eligibility.RevokedOn);
		}

		[Fact]
		public void MarkAsEligible_FromPending_ShouldEmitStudentBecameEligibleEvent()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			eligibility.MarkAsEligible();

			var @event = eligibility.DomainEvents.OfType<StudentBecameEligible>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(eligibility.Id, @event.EligibilityRecordId);
			Assert.Equal(EligibilityStudentId.Value, @event.StudentId);
			Assert.Equal(EligibilityCourseId.Value, @event.CourseId);
		}

		[Fact]
		public void MarkAsEligible_FromEligible_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			Assert.Throws<DomainException>(() => eligibility.MarkAsEligible());
		}

		[Fact]
		public void MarkAsEligible_FromIneligible_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsIneligible("Duplicate certificate exists");

			Assert.Throws<DomainException>(() => eligibility.MarkAsEligible());
		}

		[Fact]
		public void MarkAsIneligible_FromPending_ShouldSucceed()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			Assert.True(eligibility.Status.IsPending);

			eligibility.MarkAsIneligible("Duplicate certificate found");

			Assert.True(eligibility.Status.IsIneligible);
			Assert.Null(eligibility.CompletedOn);
			Assert.Null(eligibility.RevokedOn);
		}

		[Fact]
		public void MarkAsIneligible_WithNullReason_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			Assert.Throws<DomainException>(() => eligibility.MarkAsIneligible(null!));
		}

		[Fact]
		public void MarkAsIneligible_WithEmptyReason_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			Assert.Throws<DomainException>(() => eligibility.MarkAsIneligible(""));
		}

		[Fact]
		public void MarkAsIneligible_FromEligible_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			Assert.Throws<DomainException>(() => eligibility.MarkAsIneligible("Some reason"));
		}

		[Fact]
		public void Revoke_FromEligible_ShouldSucceed()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			eligibility.Revoke("Certificate was revoked");

			Assert.True(eligibility.Status.IsRevoked);
			Assert.NotNull(eligibility.RevokedOn);
		}

		[Fact]
		public void Revoke_FromIneligible_ShouldSucceed()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsIneligible("Some reason");

			eligibility.Revoke("Revoked for compliance");

			Assert.True(eligibility.Status.IsRevoked);
			Assert.NotNull(eligibility.RevokedOn);
		}

		[Fact]
		public void Revoke_FromPending_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			Assert.Throws<DomainException>(() => eligibility.Revoke("Some reason"));
		}

		[Fact]
		public void Revoke_WithNullReason_ShouldThrowDomainException()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			Assert.Throws<DomainException>(() => eligibility.Revoke(null!));
		}

		[Fact]
		public void Revoke_ShouldEmitEligibilityRevokedEvent()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			eligibility.Revoke("Certificate revoked");

			var @event = eligibility.DomainEvents.OfType<EligibilityRevoked>().FirstOrDefault();
			Assert.NotNull(@event);
			Assert.Equal(eligibility.Id, @event.EligibilityRecordId);
			Assert.Equal(EligibilityStudentId.Value, @event.StudentId);
			Assert.Equal(EligibilityCourseId.Value, @event.CourseId);
			Assert.Equal("Certificate revoked", @event.Reason);
		}
	}

	/// <summary>
	/// Test suite for EligibilityRecord expiration logic
	/// Tests IsExpired, IsValid, GetRemainingDays
	/// </summary>
	public class EligibilityRecordExpirationTests
	{
		[Fact]
		public void IsExpired_WhenNotCompleted_ShouldReturnFalse()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			Assert.False(eligibility.IsExpired);
		}

		[Fact]
		public void IsExpired_WhenCompletedWithin6Months_ShouldReturnFalse()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			// CompletedOn was set to DateTime.UtcNow, so it hasn't expired yet
			Assert.False(eligibility.IsExpired);
		}

		[Fact]
		public void IsValid_WhenEligibleAndNotExpired_ShouldReturnTrue()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			Assert.True(eligibility.IsValid);
		}

		[Fact]
		public void IsValid_WhenPending_ShouldReturnFalse()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			Assert.False(eligibility.IsValid);
		}

		[Fact]
		public void IsValid_WhenIneligible_ShouldReturnFalse()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsIneligible("Duplicate");

			Assert.False(eligibility.IsValid);
		}

		[Fact]
		public void IsValid_WhenRevoked_ShouldReturnFalse()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();
			eligibility.Revoke("Compliance");

			Assert.False(eligibility.IsValid);
		}

		[Fact]
		public void GetRemainingDays_WhenNotCompleted_ShouldReturnNull()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;

			Assert.Null(eligibility.GetRemainingDays());
		}

		[Fact]
		public void GetRemainingDays_WhenCompleted_ShouldReturnPositiveNumber()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();

			var remainingDays = eligibility.GetRemainingDays();

			Assert.NotNull(remainingDays);
			Assert.True(remainingDays > 0);
			Assert.True(remainingDays <= 180); // 6 months ~= 180 days
		}
	}

	/// <summary>
	/// Test suite for EligibilityRecord domain invariants
	/// Tests audit trails, timestamp management, and uniqueness constraints
	/// </summary>
	public class EligibilityRecordInvariantsTests
	{
		[Fact]
		public void MultipleCreationsWithSameStudentAndCourse_ShouldHaveDifferentIds()
		{
			var result1 = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			var result2 = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);

			Assert.True(result1.IsSuccess);
			Assert.True(result2.IsSuccess);
			var eligibility1 = result1.Value;
			var eligibility2 = result2.Value;

			Assert.NotEqual(eligibility1.Id, eligibility2.Id);
			Assert.Equal(eligibility1.StudentId, eligibility2.StudentId);
			Assert.Equal(eligibility1.CourseId, eligibility2.CourseId);
		}

		[Fact]
		public void EnrolledOn_ShouldBeSetToUtcNow()
		{
			var before = DateTime.UtcNow;
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			var after = DateTime.UtcNow.AddMilliseconds(1);

			Assert.True(eligibility.EnrolledOn >= before);
			Assert.True(eligibility.EnrolledOn <= after);
		}

		[Fact]
		public void MarkAsEligible_ShouldSetCompletedOnToUtcNow()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			var before = DateTime.UtcNow;

			eligibility.MarkAsEligible();

			var after = DateTime.UtcNow.AddMilliseconds(1);

			Assert.NotNull(eligibility.CompletedOn);
			Assert.True(eligibility.CompletedOn >= before);
			Assert.True(eligibility.CompletedOn <= after);
		}

		[Fact]
		public void Revoke_ShouldSetRevokedOnToUtcNow()
		{
			var result = EligibilityRecord.CreateFromEnrollment(EligibilityStudentId, EligibilityCourseId);
			Assert.True(result.IsSuccess);
			var eligibility = result.Value;
			eligibility.MarkAsEligible();
			var before = DateTime.UtcNow;

			eligibility.Revoke("Test revoke");

			var after = DateTime.UtcNow.AddMilliseconds(1);

			Assert.NotNull(eligibility.RevokedOn);
			Assert.True(eligibility.RevokedOn >= before);
			Assert.True(eligibility.RevokedOn <= after);
		}
	}
}
