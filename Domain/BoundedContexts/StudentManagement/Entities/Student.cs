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
		public StudentStatus Status { get; private set; } = default!;

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

		public void Activate()
		{
			Status = StudentStatus.Active;
			AddDomainEvent(new StudentActivated(Id));
		}

		public void Suspend()
		{
			Status = StudentStatus.Suspended;
			AddDomainEvent(new StudentSuspended(Id));
		}

		public void Delete()
		{
			Status = StudentStatus.Deleted;
			AddDomainEvent(new StudentDeleted(Id));
		}
	}

	public enum StudentStatus
	{
		Active,
		Inactive,
		Suspended,
		Deleted
	}
}
