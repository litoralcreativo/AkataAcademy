using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.ValueObjects;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public class ActivateStudentCommandHandler : ICommandHandler<ActivateStudentCommand, StudentStatus>
	{
		private readonly IStudentRepository _studentRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ActivateStudentCommandHandler(
			IStudentRepository studentRepository,
			IUnitOfWork unitOfWork)
		{
			_studentRepository = studentRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<StudentStatus>> Handle(ActivateStudentCommand command)
		{
			var student = await _studentRepository.GetByIdAsync(command.StudentId);
			if (student is null)
				return Result.Failure<StudentStatus>(Error.NotFound(ErrorCodes.Student.NotFound, "Student not found."));

			var result = student.Activate();
			if (result.IsFailure)
				return result;

			await _unitOfWork.SaveChangesAsync();

			return Result.Success(result.Value);
		}
	}
}
