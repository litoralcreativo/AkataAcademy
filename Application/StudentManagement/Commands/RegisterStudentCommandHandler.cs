using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Entities;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand, Guid>
	{
		private readonly IStudentRepository _studentRepository;
		private readonly IUnitOfWork _unitOfWork;

		public RegisterStudentCommandHandler(
			IStudentRepository studentRepository,
			IUnitOfWork unitOfWork)
		{
			_studentRepository = studentRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<Guid>> Handle(RegisterStudentCommand command)
		{
			var fullName = new FullName(command.FirstName, command.LastName);
			var email = Email.From(command.Email);
			var dateOfBirth = DateOfBirth.From(command.DateOfBirth);

			var existingStudent = await _studentRepository.GetByEmailAsync(email);
			if (existingStudent is not null)
				return Result.Failure<Guid>(Error.Conflict(ErrorCodes.Student.Creation, "A student with this email already exists."));

			var studentResult = Student.Register(fullName, email, dateOfBirth);
			if (studentResult.IsFailure)
				return Result.Failure<Guid>(studentResult.Error);

			await _studentRepository.AddAsync(studentResult.Value);
			await _unitOfWork.SaveChangesAsync();

			return studentResult.Value.Id;
		}
	}
}
