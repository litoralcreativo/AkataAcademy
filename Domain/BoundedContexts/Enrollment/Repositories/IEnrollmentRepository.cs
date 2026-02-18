using Domain.BoundedContexts.Enrollment.Entities;
using Domain.Common;

namespace Domain.BoundedContexts.Enrollment.Repositories
{
	public interface IEnrollmentRepository : IRepository<CourseEnrollment>
	{
	}
}