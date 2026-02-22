using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public class ActivateStudentCommandHandler : ICommandHandler<ActivateStudentCommand>
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

		public async Task<Result> Handle(ActivateStudentCommand command)
		{
			var student = await _studentRepository.GetByIdAsync(command.StudentId);
			if (student is null)
				return Result.Failure(Error.NotFound(ErrorCodes.Student.NotFound, "Student not found."));

			var result = student.Activate();
			if (result.IsFailure)
				return result;

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
	}
}
