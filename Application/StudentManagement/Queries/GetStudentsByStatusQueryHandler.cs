using AkataAcademy.Application.Common;
using AkataAcademy.Application.StudentManagement.DTOs;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Application.StudentManagement.Queries
{
	public class GetStudentsByStatusQueryHandler : IQueryHandler<GetStudentsByStatusQuery, IEnumerable<StudentDto>>
	{
		private readonly IStudentReadRepository _readRepository;

		public GetStudentsByStatusQueryHandler(IStudentReadRepository readRepository)
		{
			_readRepository = readRepository;
		}

		public async Task<Result<IEnumerable<StudentDto>>> Handle(GetStudentsByStatusQuery query)
		{
			var students = await _readRepository.GetByStatusAsync(query.Status);
			return Result.Success(students);
		}
	}
}
