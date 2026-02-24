using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.ValueObjects
{
	/// <summary>
	/// Represents the status of a course enrollment.
	/// States: Enrolled -> Active -> Completed
	///         Enrolled -> Suspended -> Active or Dropped
	///         Any -> Dropped (at any time)
	/// </summary>
	public record EnrollmentStatus(string Value) : IValueObject<EnrollmentStatus, string>
	{
		// State constants
		private const string ENROLLED = "ENROLLED";
		private const string ACTIVE = "ACTIVE";
		private const string COMPLETED = "COMPLETED";
		private const string SUSPENDED = "SUSPENDED";
		private const string DROPPED = "DROPPED";

		#region Factory Methods

		public static EnrollmentStatus From(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
				throw new DomainException("EnrollmentStatus value cannot be null or empty.");

			var upperValue = value.ToUpperInvariant();

			return upperValue switch
			{
				ENROLLED => new EnrollmentStatus(ENROLLED),
				ACTIVE => new EnrollmentStatus(ACTIVE),
				COMPLETED => new EnrollmentStatus(COMPLETED),
				SUSPENDED => new EnrollmentStatus(SUSPENDED),
				DROPPED => new EnrollmentStatus(DROPPED),
				_ => throw new DomainException($"Invalid enrollment status: {value}")
			};
		}

		public static EnrollmentStatus Enrolled() => new(ENROLLED);
		public static EnrollmentStatus Active() => new(ACTIVE);
		public static EnrollmentStatus Completed() => new(COMPLETED);
		public static EnrollmentStatus Suspended() => new(SUSPENDED);
		public static EnrollmentStatus Dropped() => new(DROPPED);

		#endregion

		#region State Properties

		public bool IsEnrolled => Value == ENROLLED;
		public bool IsActive => Value == ACTIVE;
		public bool IsCompleted => Value == COMPLETED;
		public bool IsSuspended => Value == SUSPENDED;
		public bool IsDropped => Value == DROPPED;

		#endregion

		#region State Transition Rules

		/// <summary>
		/// Determines if a transition to Active state is allowed.
		/// Rule: Only from Enrolled or Suspended
		/// </summary>
		public bool CanTransitionToActive => IsEnrolled || IsSuspended;

		/// <summary>
		/// Determines if a transition to Completed state is allowed.
		/// Rule: Only from Active
		/// </summary>
		public bool CanTransitionToCompleted => IsActive;

		/// <summary>
		/// Determines if a transition to Suspended state is allowed.
		/// Rule: Only from Active
		/// </summary>
		public bool CanTransitionToSuspended => IsActive;

		/// <summary>
		/// Determines if a transition to Dropped state is allowed.
		/// Rule: From any state except Completed and Dropped
		/// </summary>
		public bool CanTransitionToDropped => !IsCompleted && !IsDropped;

		#endregion
	}
}
