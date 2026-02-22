using AkataAcademy.Application.Common;
using AkataAcademy.Application.StudentManagement.DTOs;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Queries
{
	public class GetStudentByIdQueryHandler : IQueryHandler<GetStudentByIdQuery, StudentDto>
	{
		private readonly IStudentReadRepository _readRepository;

		public GetStudentByIdQueryHandler(IStudentReadRepository readRepository)
		{
			_readRepository = readRepository;
		}

		public async Task<Result<StudentDto>> Handle(GetStudentByIdQuery query)
		{
			var student = await _readRepository.GetById(query.Id);
			if (student is null)
				return Result.Failure<StudentDto>(Error.NotFound(ErrorCodes.Student.NotFound, "Student not found."));

			return Result.Success(student);
		}
	}
}
