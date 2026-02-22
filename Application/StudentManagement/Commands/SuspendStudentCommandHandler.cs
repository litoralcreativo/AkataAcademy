using AkataAcademy.Application.Common;
using AkataAcademy.Domain.BoundedContexts.StudentManagement.Repositories;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Commands
{
	public class SuspendStudentCommandHandler : ICommandHandler<SuspendStudentCommand>
	{
		private readonly IStudentRepository _studentRepository;
		private readonly IUnitOfWork _unitOfWork;

		public SuspendStudentCommandHandler(
			IStudentRepository studentRepository,
			IUnitOfWork unitOfWork)
		{
			_studentRepository = studentRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(SuspendStudentCommand command)
		{
			var student = await _studentRepository.GetByIdAsync(command.StudentId);
			if (student is null)
				return Result.Failure(Error.NotFound(ErrorCodes.Student.NotFound, "Student not found."));

			var result = student.Suspend();
			if (result.IsFailure)
				return result;

			await _unitOfWork.SaveChangesAsync();

			return Result.Success();
		}
	}
}
