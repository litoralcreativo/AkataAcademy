using AkataAcademy.Application.Common;
using AkataAcademy.Application.StudentManagement.DTOs;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Queries
{
	public class GetAllStudentsQueryHandler : IQueryHandler<GetAllStudentsQuery, IEnumerable<StudentDto>>
	{
		private readonly IStudentReadRepository _readRepository;

		public GetAllStudentsQueryHandler(IStudentReadRepository readRepository)
		{
			_readRepository = readRepository;
		}

		public async Task<Result<IEnumerable<StudentDto>>> Handle(GetAllStudentsQuery query)
		{
			var students = await _readRepository.GetAllAsync();
			return Result.Success(students);
		}
	}
}
