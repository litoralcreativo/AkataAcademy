using AkataAcademy.Domain.BoundedContexts.Enrollment.Entities;
using AkataAcademy.Domain.BoundedContexts.Enrollment.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AkataAcademy.Infrastructure.Persistence
{
	/// <summary>
	/// Repository for CourseEnrollment Aggregate Root
	/// Handles persistence operations for student course enrollments with flexible progress tracking
	/// </summary>
	public class CourseEnrollmentRepository : IEnrollmentRepository
	{
		private readonly ApplicationDbContext _context;

		public CourseEnrollmentRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Check if an enrollment exists by ID
		/// </summary>
		public Task<bool> Exists(Guid id)
		{
			return _context.Enrollments.AnyAsync(e => e.Id == id);
		}

		/// <summary>
		/// Get enrollment by ID
		/// </summary>
		public async Task<CourseEnrollment?> GetByIdAsync(Guid id)
		{
			return await _context.Enrollments.FindAsync(id);
		}

		/// <summary>
		/// Add a new enrollment
		/// </summary>
		public Task AddAsync(CourseEnrollment enrollment)
		{
			return Task.FromResult(_context.Enrollments.Add(enrollment) != null);
		}

		/// <summary>
		/// Remove an enrollment
		/// </summary>
		public Task RemoveAsync(CourseEnrollment enrollment)
		{
			return Task.FromResult(_context.Enrollments.Remove(enrollment) != null);
		}

		/// <summary>
		/// Get enrollment by student and course (unique combination)
		/// </summary>
		public async Task<CourseEnrollment?> GetByStudentAndCourseAsync(Guid studentId, Guid courseId)
		{
			return await _context.Enrollments
				.Where(e => e.StudentId.Value == studentId && e.CourseId.Value == courseId)
				.SingleOrDefaultAsync();
		}

		/// <summary>
		/// Get all enrollments for a specific student
		/// </summary>
		public async Task<IReadOnlyList<CourseEnrollment>> GetByStudentAsync(Guid studentId)
		{
			return await _context.Enrollments
				.Where(e => e.StudentId.Value == studentId)
				.ToListAsync();
		}

		/// <summary>
		/// Get all enrollments for a specific course
		/// </summary>
		public async Task<IReadOnlyList<CourseEnrollment>> GetByCourseAsync(Guid courseId)
		{
			return await _context.Enrollments
				.Where(e => e.CourseId.Value == courseId)
				.ToListAsync();
		}

		/// <summary>
		/// Get all active enrollments (enrolled, active, or suspended states)
		/// </summary>
		public async Task<IReadOnlyList<CourseEnrollment>> GetActiveEnrollmentsAsync()
		{
			return await _context.Enrollments
				.Where(e => e.IsOngoing)
				.ToListAsync();
		}

		/// <summary>
		/// Get all completed enrollments
		/// </summary>
		public async Task<IReadOnlyList<CourseEnrollment>> GetCompletedEnrollmentsAsync()
		{
			return await _context.Enrollments
				.Where(e => e.Status.IsCompleted)
				.ToListAsync();
		}

		/// <summary>
		/// Get enrollments by progress percentage range
		/// Useful for reporting on progress distribution
		/// </summary>
		public async Task<IReadOnlyList<CourseEnrollment>> GetByProgressRangeAsync(int minPercentage, int maxPercentage)
		{
			return await _context.Enrollments
				.Where(e => e.Progress.Percentage.Value >= minPercentage && e.Progress.Percentage.Value <= maxPercentage)
				.ToListAsync();
		}

		/// <summary>
		/// Get enrollments with a specific progress increment strategy
		/// Useful for analyzing how different progression speeds affect completion
		/// </summary>
		public async Task<IReadOnlyList<CourseEnrollment>> GetByProgressIncrementAsync(int incrementPercentage)
		{
			return await _context.Enrollments
				.Where(e => e.ProgressIncrement.Value == incrementPercentage)
				.ToListAsync();
		}
	}
}
