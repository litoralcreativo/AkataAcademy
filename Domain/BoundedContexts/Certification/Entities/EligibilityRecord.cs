using AkataAcademy.Domain.BoundedContexts.Certification.Events;
using AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.Entities
{
	/// <summary>
	/// Aggregate Root representing a student's eligibility for certification in a specific course.
	/// 
	/// Lifecycle:
	/// 1. Created (Pending) when StudentEnrolled event is received from Enrollment BC
	/// 2. Transitions to Eligible when CourseCompleted event is received
	/// 3. Can be Revoked if duplicate found or certificate is revoked
	/// 4. Can be marked Ineligible if validation rules fail
	/// 
	/// Invariants:
	/// - StudentId + CourseId combination must be unique
	/// - Cannot transition from Eligible back to Pending
	/// - Revoked can only come from Eligible or Ineligible states
	/// </summary>
	public class EligibilityRecord : AggregateRoot
	{
		public StudentId StudentId { get; private set; } = null!;
		public CourseId CourseId { get; private set; } = null!;

		public EligibilityStatus Status { get; private set; } = null!;

		// Timestamps for audit trail
		public DateTime EnrolledOn { get; private set; }
		public DateTime? CompletedOn { get; private set; }
		public DateTime? RevokedOn { get; private set; }

		protected EligibilityRecord()
		{
			StudentId = default!;
			CourseId = default!;
			Status = default!;
		} // EF Core

		private EligibilityRecord(
			StudentId studentId,
			CourseId courseId)
		{
			if (studentId == null)
				throw new DomainException("StudentId is required");
			if (courseId == null)
				throw new DomainException("CourseId is required");

			Id = Guid.NewGuid();
			StudentId = studentId;
			CourseId = courseId;
			Status = EligibilityStatus.Pending();
			EnrolledOn = DateTime.UtcNow;
			CompletedOn = null;
			RevokedOn = null;
		}

		/// <summary>
		/// Factory method: Creates a new EligibilityRecord when a student enrolls in a course.
		/// Called by StudentEnrolledIntegrationEventHandler.
		/// Returns Result to handle validation errors explicitly.
		/// </summary>
		public static Result<EligibilityRecord> CreateFromEnrollment(
			StudentId studentId,
			CourseId courseId)
		{
			if (studentId == null)
				return Result.Failure<EligibilityRecord>(
					Error.Validation("StudentId.Required", "StudentId is required"));

			if (courseId == null)
				return Result.Failure<EligibilityRecord>(
					Error.Validation("CourseId.Required", "CourseId is required"));

			try
			{
				return new EligibilityRecord(studentId, courseId);
			}
			catch (DomainException ex)
			{
				return Result.Failure<EligibilityRecord>(
					Error.Validation(ErrorCodes.Eligibility.Creation, ex.Message));
			}
			catch (Exception ex)
			{
				return Result.Failure<EligibilityRecord>(
					Error.Failure("EligibilityRecord.Creation", ex.Message));
			}
		}       /// <summary>
				/// Transitions the eligibility record from Pending to Eligible.
				/// Called when CourseCompletedIntegrationEventHandler receives CourseCompleted event.
				/// 
				/// Invariant: Only Pending records can become Eligible
				/// </summary>
		public void MarkAsEligible()
		{
			if (!Status.CanTransitionToEligible)
				throw new DomainException(
					$"Cannot mark as eligible. Current status '{Status.Value}' cannot transition to Eligible.");

			Status = EligibilityStatus.Eligible();
			CompletedOn = DateTime.UtcNow;

			AddDomainEvent(new StudentBecameEligible(
				Id,
				StudentId.Value,
				CourseId.Value,
				CompletedOn.Value));
		}

		/// <summary>
		/// Transitions the eligibility record to Ineligible.
		/// Used when validation rules fail (e.g., duplicate certificate already exists).
		/// 
		/// Invariant: Only Pending records can become Ineligible
		/// </summary>
		public void MarkAsIneligible(string reason)
		{
			if (string.IsNullOrWhiteSpace(reason))
				throw new DomainException("Reason is required to mark as ineligible");

			if (!Status.CanTransitionToIneligible)
				throw new DomainException(
					$"Cannot mark as ineligible. Current status '{Status.Value}' cannot transition to Ineligible. Reason: {reason}");

			Status = EligibilityStatus.Ineligible();
		}

		/// <summary>
		/// Revokes the eligibility record.
		/// Can be called when a certificate is revoked or duplicate eligibilities are detected.
		/// 
		/// Invariant: Only Eligible or Ineligible records can be Revoked
		/// </summary>
		public void Revoke(string reason)
		{
			if (string.IsNullOrWhiteSpace(reason))
				throw new DomainException("Reason is required to revoke eligibility");

			if (!Status.CanTransitionToRevoked)
				throw new DomainException(
					$"Cannot revoke. Current status '{Status.Value}' cannot transition to Revoked. Reason: {reason}");

			Status = EligibilityStatus.Revoked();
			RevokedOn = DateTime.UtcNow;

			AddDomainEvent(new EligibilityRevoked(
				Id,
				StudentId.Value,
				CourseId.Value,
				reason));
		}

		/// <summary>
		/// Checks if the eligibility is still valid (not expired and not revoked).
		/// This can be used to validate before issuing a certificate.
		/// </summary>
		public bool IsValid => Status.IsEligible && !IsExpired;

		/// <summary>
		/// Checks if the eligibility has expired.
		/// Default: 6 months from completion.
		/// </summary>
		public bool IsExpired
		{
			get
			{
				if (CompletedOn == null)
					return false;

				var expirationDate = CompletedOn.Value.AddMonths(6);
				return DateTime.UtcNow > expirationDate;
			}
		}

		/// <summary>
		/// Gets the remaining days until eligibility expires.
		/// Returns null if not yet completed.
		/// </summary>
		public int? GetRemainingDays()
		{
			if (CompletedOn == null)
				return null;

			var expirationDate = CompletedOn.Value.AddMonths(6);
			var remaining = (expirationDate - DateTime.UtcNow).Days;

			return remaining > 0 ? remaining : 0;
		}
	}
}
