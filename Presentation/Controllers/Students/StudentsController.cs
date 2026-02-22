using AkataAcademy.Application.Common;
using AkataAcademy.Application.StudentManagement.Commands;
using AkataAcademy.Application.StudentManagement.DTOs;
using AkataAcademy.Application.StudentManagement.Queries;
using AkataAcademy.Presentation.Controllers.Students.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AkataAcademy.Presentation.Controllers.Students
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentsController : ControllerBase
	{
		private readonly ILogger<StudentsController> _logger;
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;

		public StudentsController(
			ILogger<StudentsController> logger,
			IQueryDispatcher queryDispatcher,
			ICommandDispatcher commandDispatcher)
		{
			_logger = logger;
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll([FromQuery] string? status = null)
		{
			if (!string.IsNullOrEmpty(status))
			{
				var query = new GetStudentsByStatusQuery(status);
				var result = await _queryDispatcher.Dispatch(query);
				if (result.IsFailure)
					return BadRequest(result.Error);
				return Ok(result.Value);
			}
			else
			{
				var query = new GetAllStudentsQuery();
				var result = await _queryDispatcher.Dispatch(query);
				if (result.IsFailure)
					return BadRequest(result.Error);
				return Ok(result.Value);
			}
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<StudentDto>> GetById(Guid id)
		{
			var query = new GetStudentByIdQuery(id);
			var result = await _queryDispatcher.Dispatch(query);
			if (result.IsFailure)
				return NotFound(result.Error);
			return Ok(result.Value);
		}

		[HttpPost]
		public async Task<ActionResult<Guid>> Register([FromBody] RegisterStudentRequest request)
		{
			var command = new RegisterStudentCommand(
				request.FirstName,
				request.LastName,
				request.Email,
				request.DateOfBirth);

			var result = await _commandDispatcher.Dispatch(command);
			if (result.IsFailure)
				return BadRequest(result.Error);

			return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
		}

		[HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStudentRequest request)
		{
			var command = new UpdateStudentCommand(
				id,
				request.FirstName,
				request.LastName,
				request.Email,
				request.DateOfBirth);

			await _commandDispatcher.Dispatch(command);

			return NoContent();
		}

		[HttpPost("{id:guid}/activate")]
		public async Task<IActionResult> Activate(Guid id)
		{
			var command = new ActivateStudentCommand(id);
			await _commandDispatcher.Dispatch(command);

			return NoContent();
		}

		[HttpPost("{id:guid}/suspend")]
		public async Task<IActionResult> Suspend(Guid id)
		{
			var command = new SuspendStudentCommand(id);
			await _commandDispatcher.Dispatch(command);

			return NoContent();
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var command = new DeleteStudentCommand(id);
			await _commandDispatcher.Dispatch(command);

			return NoContent();
		}
	}
}
