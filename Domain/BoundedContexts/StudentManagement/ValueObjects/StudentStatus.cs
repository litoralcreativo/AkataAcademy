using System;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects
{
	public record StudentStatus
	{
		public static readonly StudentStatus Active = new("Active");
		public static readonly StudentStatus Inactive = new("Inactive");
		public static readonly StudentStatus Suspended = new("Suspended");
		public static readonly StudentStatus Deleted = new("Deleted");

		private StudentStatus(string value) { }

		public bool IsActive() => this == Active;
		public bool IsSuspended() => this == Suspended;
		public bool IsDeleted() => this == Deleted;
		public bool CanEnroll() => IsActive();

		public static StudentStatus From(string value)
		{
			return value switch
			{
				"Active" => Active,
				"Inactive" => Inactive,
				"Suspended" => Suspended,
				"Deleted" => Deleted,
				_ => throw new ArgumentException($"Invalid student status: {value}")
			};
		}

		public Result<StudentStatus> Activate()
		{
			if (IsDeleted())
				return Result.Failure<StudentStatus>(Error.Validation(ErrorCodes.Student.StatusChange, "Cannot activate a deleted student."));
			return Result.Success(Active);
		}

		public Result<StudentStatus> Suspend()
		{
			if (IsDeleted())
				return Result.Failure<StudentStatus>(Error.Validation(ErrorCodes.Student.StatusChange, "Cannot suspend a deleted student."));
			if (IsSuspended())
				return Result.Failure<StudentStatus>(Error.Validation(ErrorCodes.Student.StatusChange, "Student is already suspended."));
			return Result.Success(Suspended);
		}

		public Result<StudentStatus> Delete()
		{
			if (IsDeleted())
				return Result.Failure<StudentStatus>(Error.Validation(ErrorCodes.Student.StatusChange, "Student is already deleted."));
			return Result.Success(Deleted);
		}
	}
}
