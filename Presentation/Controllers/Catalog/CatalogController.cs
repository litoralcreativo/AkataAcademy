using System.Threading.Tasks;
using AkataAcademy.Application.Catalog.Commands;
using AkataAcademy.Application.Catalog.DTOs;
using AkataAcademy.Application.Catalog.Queries;
using AkataAcademy.Application.Common;
using AkataAcademy.Domain.Common;
using AkataAcademy.Presentation.Controllers.Catalog.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AkataAcademy.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CatalogController : ControllerBase
	{
		private readonly ILogger<CatalogController> _logger;
		private readonly IQueryDispatcher _queryDispatcher;
		private readonly ICommandDispatcher _commandDispatcher;

		public CatalogController(
			ILogger<CatalogController> logger,
			IQueryDispatcher queryDispatcher,
			ICommandDispatcher commandDispatcher)
		{
			_logger = logger;
			_queryDispatcher = queryDispatcher;
			_commandDispatcher = commandDispatcher;
		}

		[HttpGet(Name = "GetCatalog")]
		public async Task<ActionResult<IEnumerable<CourseDto>>> Get([FromQuery] bool published = false)
		{

			if (published)
			{
				var query = new GetPublishedCoursesQuery();
				var result = await _queryDispatcher.Dispatch(query);
				return Ok(result.Value);
			}
			else
			{
				var query = new GetNotPublishedCoursesQuery();
				var result = await _queryDispatcher.Dispatch(query);
				return Ok(result.Value);
			}
		}

		[HttpPost(Name = "CreateCourse")]
		public async Task<ActionResult<Guid>> Create([FromBody] CreateCourseRequest request)
		{
			var command = new CreateCourseCommand(request.Title, request.Description);
			var result = await _commandDispatcher.Dispatch(command);
			if (!result.IsSuccess)
				return BadRequest(result.Error);
			return CreatedAtAction(nameof(Get), new { published = true }, result.Value);
		}

		[HttpPost("{courseId}/modules")]
		public async Task<IActionResult> AddModuleToCourse(Guid courseId, [FromBody] AddModuleToCourseRequest request)
		{
			var command = new AddModuleToCourseCommand(courseId, request.ModuleTitle, request.ModuleDuration);
			var result = await _commandDispatcher.Dispatch(command);
			if (result.IsFailure)
				return BadRequest(result.Error);
			return CreatedAtAction(nameof(Get), new { published = false }, result.Value);
		}
	}
}