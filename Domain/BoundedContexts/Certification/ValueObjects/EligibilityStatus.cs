using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Certification.ValueObjects
{
	/// <summary>
	/// Represents the eligibility status of a student for certificate issuance.
	/// 
	/// States:
	/// - Pending: StudentEnrolled received, waiting for CourseCompleted
	/// - Eligible: CourseCompleted received, ready to issue certificate
	/// - Ineligible: Validations failed (e.g., expired, duplicate)
	/// - Revoked: Certificate was revoked after issuance
	/// </summary>
	public record EligibilityStatus(string Value) : IValueObject<EligibilityStatus, string>
	{
		public const string PENDING = nameof(PENDING);
		public const string ELIGIBLE = nameof(ELIGIBLE);
		public const string INELIGIBLE = nameof(INELIGIBLE);
		public const string REVOKED = nameof(REVOKED);

		public static EligibilityStatus From(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new DomainException("EligibilityStatus cannot be empty.");

			var status = value.ToUpperInvariant();

			if (status != PENDING && status != ELIGIBLE && status != INELIGIBLE && status != REVOKED)
				throw new DomainException(
					$"Invalid EligibilityStatus '{value}'. Valid values: {PENDING}, {ELIGIBLE}, {INELIGIBLE}, {REVOKED}");

			return new EligibilityStatus(status);
		}

		// Factory methods for better semantics
		public static EligibilityStatus Pending() => new(PENDING);
		public static EligibilityStatus Eligible() => new(ELIGIBLE);
		public static EligibilityStatus Ineligible() => new(INELIGIBLE);
		public static EligibilityStatus Revoked() => new(REVOKED);

		// State transition validation
		public bool IsPending => Value == PENDING;
		public bool IsEligible => Value == ELIGIBLE;
		public bool IsIneligible => Value == INELIGIBLE;
		public bool IsRevoked => Value == REVOKED;

		public bool CanTransitionToEligible => IsPending;
		public bool CanTransitionToIneligible => IsPending;
		public bool CanTransitionToRevoked => IsEligible || IsIneligible;

		public override string ToString() => Value;
	}
}
