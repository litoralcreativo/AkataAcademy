using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.Common;

namespace AkataAcademy.Domain.BoundedContexts.Enrollment.Repositories
{
	/// <summary>
	/// Repository interface for CourseEnrollment Aggregate Root
	/// Extends base IRepository with enrollment-specific queries
	/// </summary>
	public interface IEnrollmentRepository : IRepository<CourseEnrollment>
	{
		/// <summary>
		/// Get enrollment by student and course (unique combination)
		/// </summary>
		Task<CourseEnrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId);

		/// <summary>
		/// Get all enrollments for a specific student
		/// </summary>
		Task<IReadOnlyList<CourseEnrollment>> GetByStudentAsync(Guid studentId);

		/// <summary>
		/// Get all enrollments for a specific course
		/// </summary>
		Task<IReadOnlyList<CourseEnrollment>> GetByCourseAsync(Guid courseId);

		/// <summary>
		/// Get all active enrollments (enrolled, active, or suspended states)
		/// </summary>
		Task<IReadOnlyList<CourseEnrollment>> GetActiveEnrollmentsAsync();

		/// <summary>
		/// Get all completed enrollments
		/// </summary>
		Task<IReadOnlyList<CourseEnrollment>> GetCompletedEnrollmentsAsync();

		/// <summary>
		/// Get enrollments by progress percentage range
		/// </summary>
		Task<IReadOnlyList<CourseEnrollment>> GetByProgressRangeAsync(int minPercentage, int maxPercentage);

		/// <summary>
		/// Get enrollments with a specific progress increment strategy
		/// </summary>
		Task<IReadOnlyList<CourseEnrollment>> GetByProgressIncrementAsync(int incrementPercentage);
	}
}