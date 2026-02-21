using System;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.DomainEvents;
using System.Collections.Generic;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities
{
	public class Student : AggregateRoot
	{
		public FullName Name { get; private set; } = default!;
		public Email Email { get; private set; } = default!;
		public DateOfBirth DateOfBirth { get; private set; } = default!;
		public StudentStatus Status { get; private set; } = StudentStatus.Active;

		protected Student() { }

		public Student(Guid id, FullName name, Email email, DateOfBirth dateOfBirth)
		{
			Id = id;
			Name = name;
			Email = email;
			DateOfBirth = dateOfBirth;
			Status = StudentStatus.Active;
			AddDomainEvent(new StudentRegistered(Id));
		}

		public void UpdatePersonalInfo(FullName name, Email email, DateOfBirth dateOfBirth)
		{
			Name = name;
			Email = email;
			DateOfBirth = dateOfBirth;
			AddDomainEvent(new StudentUpdated(Id));
		}

		public Result Activate()
		{
			var result = Status.Activate();
			if (!result.IsSuccess) return result;

			Status = result.Value;
			AddDomainEvent(new StudentActivated(Id));
			return Result.Success();
		}

		public Result Suspend()
		{
			var result = Status.Suspend();
			if (!result.IsSuccess) return result;

			Status = result.Value;
			AddDomainEvent(new StudentSuspended(Id));
			return Result.Success();
		}

		public Result Delete()
		{
			var result = Status.Delete();
			if (!result.IsSuccess) return result;

			Status = result.Value;
			AddDomainEvent(new StudentDeleted(Id));
			return Result.Success();
		}
	}
}
