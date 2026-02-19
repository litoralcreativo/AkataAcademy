using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Repositories
{
	public interface IEnrollmentRepository : IRepository<CourseEnrollment>
	{
	}
}