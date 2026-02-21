using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;

namespace AkataAcademy.Application.StudentManagement.DTOs
{
	public class StudentDto : DTO<Student, Guid>
	{
		public Guid Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
		public string Status { get; set; } = string.Empty;
	}
}
