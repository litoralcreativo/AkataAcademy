using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public class UpdateStudentCommandHandler : ICommandHandler<UpdateStudentCommand>
	{
		private readonly IStudentRepository _studentRepository;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateStudentCommandHandler(
			IStudentRepository studentRepository,
			IUnitOfWork unitOfWork)
		{
			_studentRepository = studentRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(UpdateStudentCommand command)
		{
			var student = await _studentRepository.GetByIdAsync(command.StudentId);
			if (student is null)
				return Result.Failure(Error.NotFound(ErrorCodes.Student.NotFound, "Student not found."));

			var fullName = new FullName(command.FirstName, command.LastName);
			var email = Email.From(command.Email);
			var dateOfBirth = DateOfBirth.From(command.DateOfBirth);

			student.UpdatePersonalInfo(fullName, email, dateOfBirth);

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
	}
}
